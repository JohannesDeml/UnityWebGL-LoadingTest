# Unity WebGL Loading Test

![Preview](./preview.png)

[![](https://img.shields.io/github/release-date/JohannesDeml/UnityWebGL-LoadingTest.svg)](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/releases) [![Tested up to Unity 6.2](https://img.shields.io/badge/tested%20up%20to%20unity-6000.2-green.svg?logo=unity&cacheSeconds=2592000)](https://unity3d.com/get-unity/download/archive)

*Testing Unity's WebGL size and loading time for different versions (2018.4 - 6000.2) and settings*

* [Overview page of all builds](https://deml.io/experiments/unity-webgl/)
* [Implementation in Godot](https://github.com/JohannesDeml/Godot-Web-LoadingTest)
* [Unity Forum Thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/)


## Features

* [Toggle-able In-DOM Debug console](./Documentation/DebugConsole.png)
* [Unity Rich text styling support for browser console & debug console](./Documentation/UnityRichTextSupport.png)
* Easy access to unity functions through the browser console ([Youtube](https://youtu.be/OjypxsD6XMI))
* Handy debug functions for times and memory consumption
* Responsive template layout for maximum mobile compatibility and smooth transition to the game after loading finished
* Github Actions to automatically build the project and deploy it on the server via [Game CI](https://game.ci/)
  * Action to upgrade a branch automatically to a new Unity version with automated builds and a PR
* Tracking differnt Unity versions starting from 2018.4 (over 700 live demo builds for you to evaluate)
* Brotli Compression
* Build targets for webgl1, webgl2 and webgpu with BiRP and URP

## Live Demos ([All Builds](https://deml.io/experiments/unity-webgl/))

### Built-in Renderpipeline WebGL2
Version | Size | Link
--- | --- | ---
6000.2.10f1 | 3.39 MB | https://deml.io/experiments/unity-webgl/6000.2.10f1-webgl2
6000.1.17f1 | 3.33 MB | https://deml.io/experiments/unity-webgl/6000.1.17f1-webgl2
6000.0.62f1 | 3.33 MB | https://deml.io/experiments/unity-webgl/6000.0.62f1-webgl2
2023.2.20f1 | 3.27 MB | https://deml.io/experiments/unity-webgl/2023.2.20f1-webgl2
2023.1.20f1 | 3.19 MB | https://deml.io/experiments/unity-webgl/2023.1.20f1-webgl2
2022.3.62f3 | 3.18 MB | https://deml.io/experiments/unity-webgl/2022.3.62f3-webgl2
2021.3.45f2 | 2.83 MB | https://deml.io/experiments/unity-webgl/2021.3.45f2-webgl2
2020.3.48f1 | 3.01 MB | https://deml.io/experiments/unity-webgl/2020.3.48f1-webgl2

### Built-in Renderpipeline WebGL1
Version | Size | Link
--- | --- | ---
2022.3.62f3 | 3.16 MB | https://deml.io/experiments/unity-webgl/2022.3.62f3-webgl1
2021.3.45f2 | 2.81 MB | https://deml.io/experiments/unity-webgl/2021.3.45f2-webgl1
2020.3.48f1 | 2.99 MB | https://deml.io/experiments/unity-webgl/2020.3.48f1-webgl1

### Built-in Renderpipeline Minimum size
Version | Size | Link
--- | --- | ---
6000.2.10f1 | 2.92 MB | https://deml.io/experiments/unity-webgl/6000.2.10f1-minsize-webgl2
6000.1.17f1 | 2.85 MB | https://deml.io/experiments/unity-webgl/6000.1.17f1-minsize-webgl2
6000.0.62f1 | 2.93 MB | https://deml.io/experiments/unity-webgl/6000.0.62f1-minsize-webgl2
2023.2.20f1 | 2.88 MB | https://deml.io/experiments/unity-webgl/2023.2.20f1-minsize-webgl2
2023.1.20f1 | 2.76 MB | https://deml.io/experiments/unity-webgl/2023.1.20f1-minsize-webgl2
2022.3.62f3 | 2.74 MB | https://deml.io/experiments/unity-webgl/2022.3.62f3-minsize-webgl1
2021.3.45f2 | 2.79 MB | https://deml.io/experiments/unity-webgl/2021.3.45f2-minsize-webgl1
2020.3.48f1 | 2.54 MB | https://deml.io/experiments/unity-webgl/2020.3.48f1-minsize-webgl1

### URP WebGL2
Version | Size | Link
--- | --- | ---
6000.2.10f1 | 8.13 MB | https://deml.io/experiments/unity-webgl/6000.2.10f1-urp-webgl2
6000.1.17f1 | 7.97 MB | https://deml.io/experiments/unity-webgl/6000.1.17f1-urp-webgl2
6000.0.62f1 | 7.94 MB | https://deml.io/experiments/unity-webgl/6000.0.62f1-urp-webgl2
2023.2.20f1 | 6.88 MB | https://deml.io/experiments/unity-webgl/2023.2.20f1-urp-webgl2
2023.1.20f1 | 6.26 MB | https://deml.io/experiments/unity-webgl/2023.1.20f1-urp-webgl2
2022.3.62f3 | 5.98 MB | https://deml.io/experiments/unity-webgl/2022.3.62f3-urp-webgl2
2021.3.45f2 | 6.36 MB | https://deml.io/experiments/unity-webgl/2021.3.45f2-urp-webgl2
2020.3.48f1 | 5.65 MB | https://deml.io/experiments/unity-webgl/2020.3.48f1-urp-webgl2

### URP WebGL1
Version | Size | Link
--- | --- | ---
2022.3.62f3 | 5.93 MB | https://deml.io/experiments/unity-webgl/2022.3.62f3-urp-webgl1
2021.3.45f2 | 6.18 MB | https://deml.io/experiments/unity-webgl/2021.3.45f2-urp-webgl1
2020.3.48f1 | 5.49 MB | https://deml.io/experiments/unity-webgl/2020.3.48f1-urp-webgl1

### URP Minimum Size
Version | Size | Link
--- | --- | ---
6000.1.17f1 | 5.64 MB | https://deml.io/experiments/unity-webgl/6000.1.17f1-urp-minsize-webgl2
6000.0.62f1 | 6.10 MB | https://deml.io/experiments/unity-webgl/6000.0.62f1-urp-minsize-webgl2
2023.2.20f1 | 5.33 MB | https://deml.io/experiments/unity-webgl/2023.2.20f1-urp-minsize-webgl2
2023.1.20f1 | 4.88 MB | https://deml.io/experiments/unity-webgl/2023.1.20f1-urp-minsize-webgl2
2022.3.62f3 | 4.69 MB | https://deml.io/experiments/unity-webgl/2022.3.62f3-urp-minsize-webgl1
2021.3.45f2 | 6.16 MB | https://deml.io/experiments/unity-webgl/2021.3.45f2-urp-minsize-webgl1
2020.3.48f1 | 4.91 MB | https://deml.io/experiments/unity-webgl/2020.3.48f1-urp-minsize-webgl1

## Platform Compatibility

| Platform         | Chrome | Firefox | Edge | Safari | Internet Explorer |
| ----------       | :----: | :-----: | :--: | :----: | :---------------: |
| Windows 10       |   ✔️    |    ✔️    |  ✔️   |   ➖    |         ❌         |
| Linux            |   ✔️    |    ✔️    |  ✔️   |   ➖    |         ➖         |
| Mac              |   ✔️    |    ✔️    |  ✔️   |   ✔️    |         ➖         |
| Android          |   ✔️    |    ✔️    |  ✔️   |   ➖    |         ➖         |
| iOS              |   ✔️    |    ✔️    |  ✔️   |   ✔️    |         ➖         |
| Android Smart TV |   ✔️    |    ➖    |  ➖   |   ➖    |         ➖         |

✔️ *: Supported* | ⚠️ *: Warning , see below* | ❌ *: not supported* | ➖ *: Not applicable*

* For older Unity 2019 builds throw an error on **iOS Firefox** (Does not happen for iOS Safari or iOS Chrome): `An error occurred running the Unity content on this page. See you browser JavaScript console for more info. The error: Script error.` - with iOS 16.2 and Firefox 108 I could not reproduce this problem anymore on 2019.4.
* Internet Explorer does not work for Unity WebGL builds, since it does not support wasm.
* Previous versions of Firefox on Android had some performance problems, they seemed to have fixed those. In general performance on mobile is by far not as good as a native app build, but should be enough for simple games.
* For android smart TVs expect longer loading times (up to ~2 minutes)

## Notes

* There are some combinations for **iOS** that have **problems**: With recent versions **URP with WebGL 1** does not work at all and **builtin render pipeline with WebGL 2** has performance problems or might not load at all. I recommend to either use URP with WebGL2 or builtin with WebGL1, if you are targeting iOS. You can always test the builds on your device, to see which combination might fit your needs: https://deml.io/experiments/unity-webgl/
* If you want to use this project as a basis for your project, be sure to select the release/branch for your unity version. Beta versions oftentimes have problems, therefore I would recommend to use the latest LTS version to run your project.
* The server is configured to support wasm streaming and brotli compression, see [.htaccess 2020](./Configuration/2020/.htaccess)  [.htaccess 2019](./Configuration/2019/.htaccess)
* Due to the odd way that Unity does brotli compression, some servers (such as itch.io) don't support it, you should [learn how to manually compress your game](https://miltoncandelero.github.io/unity-webgl-compression) or use gzip compression instead. If brotli is misconfigured or not supported you will get an error along the lines of:
  ```
  Unable to parse Build/WEBGL.framework.js.br! This can happen if build compression was enabled but web server hosting the content was misconfigured to not serve the file with HTTP Response Header "Content-Encoding: br" present. Check browser Console and Devtools Network tab to debug.
  ```
  You can change the compression in the project settings:
  ![Screenshot of project settings gzip dropdown](./Documentation/ChangeCompressionToGzip.png)
* In order to get rid of the warning on android/iOS it is removed in a post process build step ([File](./Assets/Scripts/Editor/RemoveMobileSupportWarningWebBuild.cs))
* You might need to reload the page on android/iOS when running the first time
* This is a very small example. When building larger WebGL applications, you might run into problems with memory or compile errors. I recommend to build from the start and very often, to catch the changes that create those problems.
* If you want to have the smallest file size possible and faster load times, take a look at [Project Tiny](https://forum.unity.com/forums/project-tiny.151/) or web-specific libraries like [Three.js](https://threejs.org/) or [PlayCanvas](https://playcanvas.com/).
* Removing the default skybox will save ~30kb.
* With 2021.2 Unity added the possibility to use different texture formats. The builds use the default DXT format, but since no texture is used this settings does not make any difference in this project.

### URP (Universal Render Pipeline)
* URP adds additional ~2.5 MB file size compared to the builtin render pipeline.
* In general the performance for URP seems to be better compared to builtin, but it has a larger build size.
* With Unity 2021.2 there is currently a problem with shader compilation and therefore builds fail:
  ```
  Shader error in 'Hidden/Universal/CoreBlit': invalid subscript 'positionCS' at
  UnityWebGL-LoadingTest/Library/PackageCache/com.unity.render-pipelines.core@12.1.0/Runtime/Utilities/Blit.hlsl(92) (on gles)
  Compiling Vertex program with DISABLE_TEXTURE2D_X_ARRAY
  Platform defines: SHADER_API_DESKTOP UNITY_COLORSPACE_GAMMA UNITY_ENABLE_DETAIL_NORMALMAP UNITY_LIGHTMAP_RGBM_ENCODING UNITY_PBS_USE_BRDF1 UNITY_SPECCUBE_BLENDING UNITY_SPECCUBE_BOX_PROJECTION UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS
  Disabled keywords: BLIT_SINGLE_SLICE SHADER_API_GLES30 UNITY_ASTC_NORMALMAP_ENCODING UNITY_ENABLE_NATIVE_SHADOW_LOOKUPS UNITY_ENABLE_REFLECTION_BUFFERS UNITY_FRAMEBUFFER_FETCH_AVAILABLE UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS UNITY_HARDWARE_TIER1 UNITY_HARDWARE_TIER2 UNITY_HARDWARE_TIER3 UNITY_LIGHTMAP_DLDR_ENCODING UNITY_LIGHTMAP_FULL_HDR UNITY_LIGHT_PROBE_PROXY_VOLUME UNITY_METAL_SHADOWS_USE_POINT_FILTERING UNITY_NO_DXT5nm UNITY_NO_FULL_STANDARD_SHADER UNITY_NO_SCREENSPACE_SHADOWS UNITY_PBS_USE_BRDF2 UNITY_PBS_USE_BRDF3 UNITY_PRETRANSFORM_TO_DISPLAY_ORIENTATION UNITY_UNIFIED_SHADER_PRECISION_MODEL UNITY_VIRTUAL_TEXTURING
  Shader error in 'Hidden/kMotion/CameraMotionVectors': SV_VertexID semantic is not supported on GLES 2.0 at line 11 (on gles)
  Compiling Subshader: 0, Pass: , Vertex program with <no keywords>
  Platform defines: SHADER_API_DESKTOP UNITY_COLORSPACE_GAMMA UNITY_ENABLE_DETAIL_NORMALMAP UNITY_LIGHTMAP_RGBM_ENCODING UNITY_PBS_USE_BRDF1 UNITY_SPECCUBE_BLENDING UNITY_SPECCUBE_BOX_PROJECTION UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS
  Disabled keywords: SHADER_API_GLES30 UNITY_ASTC_NORMALMAP_ENCODING UNITY_ENABLE_NATIVE_SHADOW_LOOKUPS UNITY_ENABLE_REFLECTION_BUFFERS UNITY_FRAMEBUFFER_FETCH_AVAILABLE UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS UNITY_HARDWARE_TIER1 UNITY_HARDWARE_TIER2 UNITY_HARDWARE_TIER3 UNITY_LIGHTMAP_DLDR_ENCODING UNITY_LIGHTMAP_FULL_HDR UNITY_LIGHT_PROBE_PROXY_VOLUME UNITY_METAL_SHADOWS_USE_POINT_FILTERING UNITY_NO_DXT5nm UNITY_NO_FULL_STANDARD_SHADER UNITY_NO_SCREENSPACE_SHADOWS UNITY_PBS_USE_BRDF2 UNITY_PBS_USE_BRDF3 UNITY_PRETRANSFORM_TO_DISPLAY_ORIENTATION UNITY_UNIFIED_SHADER_PRECISION_MODEL UNITY_VIRTUAL_TEXTURING
  ```
* URP on mobile runs a lot better with WebGL 2 than WebGL 1 on iOS. For example compare [2021.3.6f1 URP WebGL1](https://deml.io/experiments/unity-webgl/2021.3.6f1-urp) with [2021.3.6f1 URP WebGL2](https://deml.io/experiments/unity-webgl/2021.3.6f1-urp-webgl2) on your iPhone.
* Unity 2022.1 has problems building for URP with WebGL1 (also on desktop), error:
  ```
  Texture creation failed. 'ShadowAuto' is not supported for Render usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.
  NullReferenceException: Object reference not set to an instance of an object.
  ```

### Min Size builds

* Built with `Tools/Build WebGL/minsize`
* Built with Code Optimization: `Size`
* Enable Exceptions: `None`
* C++ Compiler Configuration: `Master`

## Browser Console commands

*This functionality was added 2021-11-21 and is only supported by releases starting at that date*

The script `WebGlBridge` adds an easy to access gameobject that can be called from the browser console through `unityGame.SendMessage("WebGL", "COMMAND_NAME",PARAMETER)`

Currently the following commands are available:

### CommonCommands

```javascript
runUnityCommand("AllocateByteArrayMemory", int mb); -> Allocate memory to test memory usage and limits
runUnityCommand("CheckOnlineStatus"); -> Check if browser is online
runUnityCommand("CopyToClipboard", "string text"); -> Copy text to clipboard
runUnityCommand("DeleteAllPlayerPrefs"); -> PlayerPrefs.DeleteAll
runUnityCommand("DisableCaptureAllKeyboardInput"); -> Disable unity from consuming all keyboard input
runUnityCommand("EnableCaptureAllKeyboardInput"); -> Enable unity from consuming all keyboard input
runUnityCommand("FindGameObjectByName", "string name"); -> Find GameObject by name and log its components
runUnityCommand("LogExampleMessages"); -> Log example messages for Log, warning and error
runUnityCommand("LogInitializationTime"); -> Log initialization time information
runUnityCommand("LogMemory"); -> Logs the current memory
runUnityCommand("LogMessage", "string message"); -> Log a custom message
runUnityCommand("LogShaderCompilation", int enabled); -> GraphicsSettings.logWhenShaderIsCompiled
runUnityCommand("LogTextureSupport"); -> Log supported and unsupported texture formats
runUnityCommand("LogUserAgent"); -> Log User Agent and isMobileDevice
runUnityCommand("ReleaseByteArrayMemory"); -> Release all allocated byte array memory
runUnityCommand("SaveScreenshot"); -> Save current screen as PNG
runUnityCommand("SaveScreenshotSuperSize", int superSize); -> Save current screen as PNG with variable super size
runUnityCommand("SetApplicationRunInBackground", int runInBackground); -> Application.runInBackground
runUnityCommand("SetApplicationTargetFrameRate", int targetFrameRate); -> Application.targetFrameRate
runUnityCommand("SetTimeFixedDeltaTime", float fixedDeltaTime); -> Time.fixedDeltaTime
runUnityCommand("SetTimeTimeScale", float timeScale); -> Time.timeScale
runUnityCommand("ThrowDictionaryException"); -> Throw a dictionary key not found exception
runUnityCommand("ToggleInfoPanel"); -> Toggle develop ui visibility of InfoPanel
runUnityCommand("TriggerGarbageCollection"); -> Trigger garbage collection
runUnityCommand("UnloadUnusedAssets"); -> Resources.UnloadUnusedAssets
```

### ObjectSpawnerCommands

```javascript
runUnityCommand("AddSpawner"); -> Add a spawner
runUnityCommand("PauseSpawning"); -> Pause spawning of cubes
runUnityCommand("RemoveSpawner"); -> Remove a spawner
runUnityCommand("ResumeSpawning"); -> Resume spawning of cubes
```

### WebBridge

```javascript
runUnityCommand("Help"); -> Log all available commands
```


## Github Build Actions

This repository supports continuous Integration through [game.ci](https://game.ci/). There are scripts for creating a unity license, building the project and upgrading the project.

### Automatic builds

The main github actions workflow is [release.yml](./.github/workflows/release.yml). On Unity side a custom build script on the basis of [game.ci's BuildScript](https://github.com/game-ci/documentation/blob/main/example/BuildScript.cs) is used: [BuildScript.cs](./Assets/Scripts/Editor/BuildScript.cs). This script supports defining different build logic through git tags through the following syntax: `UNITY_VERSION`-`TAG1`-`TAG2`... Example: `6000.0.0f1-urp-webgl2`

The following tags are supported:

* `minsize`: Set Code optimization to size and and don't support exceptions
* `debug`: Build a development build with embedded debug symbols and full stack trace
* `webgl1`: Build for WebGL1
* `webgl2`: Build for WebGL2
* `webgpu`: Build for WebGPU

### Upgrade Unity CI

For upgrading Unity [upgrade-unity.yml](./.github/workflows/upgrade-unity.yml) is used. It can be triggered through github actions manually by defining the branch to upgrade and some additional settings
![Upgrade Unity Github Action screenshot](./Documentation/UpgradeUnityGithubAction.png)
It will create a pull request with the newly defined unity version along with tags to trigger automatic builds. Additionally, all packages are updated to their latest version, see also [UnityPackageScripts.cs](./Assets/Scripts/Editor/UnityPackageScripts.cs).

## Older versions

You can find a list of all live builds with their sizes over here: https://deml.io/experiments/unity-webgl/

## License

* MIT (c) Johannes Deml - see [LICENSE](./LICENSE.md)
