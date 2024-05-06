#!/bin/bash
# This script creates tags for the given unity version to trigger the release workflow
# example usage
# sh add-tags.sh "6000.0.0f1" -> Creates tags for non-urp version
# sh add-tags.sh "6000.0.0f1" "true" -> Creates tags for urp version

# Input parameters
UNITY_VERSION=$1
IS_URP=${2:-"false"}
echo "Running add_tags.sh with UNITY_VERSION: $UNITY_VERSION, IS_URP: $IS_URP"

# Extract the first four characters of UNITY_VERSION
MAJOR_VERSION=${UNITY_VERSION:0:4}
BRANCH_NAME=${GITHUB_REF#refs/heads/}

TAG_PREFIX=$UNITY_VERSION
if [[ "$IS_URP" == "true" ]]
then
TAG_PREFIX=$UNITY_VERSION-urp
fi

if [[ "$MAJOR_VERSION" -lt "2023" ]]
then
git tag -a -f $TAG_PREFIX-minsize-webgl1 -m "[Automated workflow] Created by upgrade-unity"
git tag -a -f $TAG_PREFIX-webgl1 -m "[Automated workflow] Created by upgrade-unity"
else
git tag -a -f $TAG_PREFIX-minsize-webgl2 -m "[Automated workflow] Created by upgrade-unity"
fi
# Push tags in between - pushing more than 3 tags won't trigger tag workflows
git push origin -f --tags

git tag -a -f $TAG_PREFIX-webgl2 -m "[Automated workflow] Created by upgrade-unity"
git tag -a -f $TAG_PREFIX-webgl2-debug -m "[Automated workflow] Created by upgrade-unity"

if [[ "$MAJOR_VERSION" -ge "6000" ]]
then
git tag -a -f $TAG_PREFIX-webgpu -m "[Automated workflow] Created by upgrade-unity"
fi

git push origin -f --tags