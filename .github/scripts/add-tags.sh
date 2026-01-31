#!/bin/bash
# This script creates tags for the given unity version to trigger the release workflow
# example usage
# sh add-tags.sh "6000.0.0f1" -> Creates tags for non-urp version
# sh add-tags.sh "6000.0.0f1" "true" -> Creates tags for urp version
# sh add-tags.sh "6000.0.0f1" "true" "10" -> Creates tags for urp version with 10 second delay between pushes

# Input parameters
UNITY_VERSION=$1
IS_URP=${2:-"false"}
DELAY_TAGS=${3:-"0"}

echo "Running add_tags.sh with UNITY_VERSION: $UNITY_VERSION, IS_URP: $IS_URP, DELAY_TAGS: $DELAY_TAGS seconds"

# Extract the value before the first dot as an integer
MAJOR_VERSION=$(echo $UNITY_VERSION | cut -d. -f1)
BRANCH_NAME=${GITHUB_REF#refs/heads/}

TAG_PREFIX=$UNITY_VERSION
if [[ "$IS_URP" == "true" ]]
then
TAG_PREFIX=$UNITY_VERSION-urp
fi

# Build array of tags to create and push
TAGS=()

if [[ "$MAJOR_VERSION" -lt "2023" ]]
then
TAGS+=("$TAG_PREFIX-minsize-webgl1")
TAGS+=("$TAG_PREFIX-webgl1")
else
TAGS+=("$TAG_PREFIX-minsize-webgl2")
fi

TAGS+=("$TAG_PREFIX-webgl2")
TAGS+=("$TAG_PREFIX-webgl2-debug")

if [[ "$MAJOR_VERSION" -ge "6000" ]]
then
TAGS+=("$TAG_PREFIX-webgpu")
fi

# Loop through tags, create and push each one with delay
for i in "${!TAGS[@]}"; do
  TAG="${TAGS[$i]}"
  echo "Creating and pushing tag: $TAG"

  # Create the tag
  git tag -a -f "$TAG" -m "[Automated workflow] Created by upgrade-unity"

  # Push the tag
  git push origin -f "$TAG"

  # Wait between pushes if not the last tag and delay is specified
  if [[ $i -lt $((${#TAGS[@]} - 1)) ]] && [[ "$DELAY_TAGS" -gt "0" ]]
  then
    echo "Waiting $DELAY_TAGS seconds before next tag push..."
    sleep $DELAY_TAGS
  fi
done

echo "All tags created and pushed successfully."