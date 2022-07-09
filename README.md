# Unity WebGL Loading Test

![Preview](./preview.png)

[![](https://img.shields.io/github/release-date/JohannesDeml/UnityWebGL-LoadingTest.svg)](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/releases) [![Tested up to Unity 2022.1](https://img.shields.io/badge/tested%20up%20to%20unity-2022.1-green.svg?logo=unity&cacheSeconds=2592000)](https://unity3d.com/get-unity/download/archive)

*Testing Unity's WebGL size and loading time for different versions and platforms*  
[Unity Forum Thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/).


## Features

* Physics
* GPU Instancing for materials
* Shadows
* Brotli Compression
* Togglable In-DOM Debug console ([Demo](https://deml.io/experiments/unity-webgl/2021.1.4f1/))
* Easy access to unity functions through the browser console ([Demo](https://deml.io/experiments/unity-webgl/2020.3.23f1/)|[Youtube](https://youtu.be/OjypxsD6XMI))
* Handy debug functions for times and memory consumption
* Responsive template layout for maximum mobile compatibility
* Github Actions to automatically build the project and deploy it on the server via [Game CI](https://game.ci/)
* Works with [Unity WebGL Publisher](https://play.unity.com/discover/all-showcases) (Use  [2020.3-lts](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/tree/2020-lts) or [2020.3-lts-urp](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/tree/2020-lts-urp) branch)
* Tracking multiple Unity versions starting from 2018.4

## Live Demos

### Built-in Renderpipeline WebGL1
Version | Size | Link
--- | --- | ---
2018.4.36f1 | 2.80 MB | https://deml.io/experiments/unity-webgl/2018.4.36f1/ 
2019.4.28f1 | 2.97 MB | https://deml.io/experiments/unity-webgl/2019.4.28f1/ 
2020.3.30f1 | 2.89 MB | https://deml.io/experiments/unity-webgl/2020.3.30f1/ 
2021.1.0f1 | 2.85 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1/ 
2021.2.0f1 | 2.69 MB | https://deml.io/experiments/unity-webgl/2021.2.0f1/ 
2021.3.6f1 | 2.69 MB | https://deml.io/experiments/unity-webgl/2021.3.6f1/ 
2022.1.8f1 | 2.75 MB | https://deml.io/experiments/unity-webgl/2022.1.8f1-webgl1/ 

### Built-in Renderpipeline WebGL2
Version | Size | Link
--- | --- | ---
2021.3.6f1 | 2.71 MB | https://deml.io/experiments/unity-webgl/2021.3.6f1-webgl2/ 
2022.1.8f1 | 2.77 MB | https://deml.io/experiments/unity-webgl/2022.1.8f1-webgl2/ 

### Built-in Renderpipeline WebGL1 Minimum size
Version | Size | Link
--- | --- | ---
2018.4.36f1 Min | 2.77 MB | https://deml.io/experiments/unity-webgl/2018.4.36f1-minsize/ 
2019.4.28f1 Min | 2.94 MB | https://deml.io/experiments/unity-webgl/2019.4.28f1-minsize/ 
2020.3.30f1 Min | 2.45 MB | https://deml.io/experiments/unity-webgl/2020.3.30f1-minsize/ 
2021.1.0f1 Min | 2.45 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1-minsize/ 
2021.2.0f1 Min | 2.43 MB | https://deml.io/experiments/unity-webgl/2021.2.0f1-minsize/ 
2021.3.6f1 Min | 2.54 MB | https://deml.io/experiments/unity-webgl/2021.3.6f1-minsize/ 
2022.1.8f1 Min | 2.61 MB | https://deml.io/experiments/unity-webgl/2022.1.8f1-minsize-webgl1/ 

### URP WebGL1
Version | Size | Link
--- | --- | ---
2018.4.36f1 LWRP | --- | https://deml.io/experiments/unity-webgl/2018.4.36f1-lwrp/ - Broken ([Notes](https://forum.unity.com/threads/exporting-to-webgl-unity-lwrp.549718/#post-4770827)) 
2019.4.28f1 URP | 5.48 MB | https://deml.io/experiments/unity-webgl/2019.4.28f1-urp/ 
2020.3.30f1 URP | 5.36 MB | https://deml.io/experiments/unity-webgl/2020.3.30f1-urp/ 
2021.1.0f1 URP | 5.47 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1-urp/ 
2021.2.0f1 URP | --- | https://issuetracker.unity3d.com/product/unity/issues/guid/1360175 
2021.3.6f1 URP | 5.98 MB | https://deml.io/experiments/unity-webgl/2021.3.6f1-urp/ 
2022.1.8f1 URP | 6.26 MB | https://deml.io/experiments/unity-webgl/2022.1.8f1-urp-webgl1/ - Broken 

### URP WebGL2
Version | Size | Link
--- | --- | ---
2021.3.0f1 URP | 5.80 MB | https://deml.io/experiments/unity-webgl/2021.3.0f1-urp-webgl2/ 
2021.3.0f1 URP | 6.16 MB | https://deml.io/experiments/unity-webgl/2021.3.6f1-urp-webgl2/ 
2022.1.8f1 URP | 6.42 MB | https://deml.io/experiments/unity-webgl/2022.1.8f1-urp-webgl2/ 

## Platform Compatibility

| Platform   | Chrome | Firefox | Edge | Safari | Internet Explorer |
| ---------- | :----: | :-----: | :--: | :----: | :---------------: |
| Windows 10 |   ✔️    |    ✔️    |  ✔️   |   ➖    |         ❌         |
| Linux      |   ✔️    |    ✔️    |  ✔️   |   ➖    |         ➖         |
| Mac        |   ✔️    |    ✔️    |  ✔️   |   ✔️    |         ➖         |
| Android    |   ✔️    |    ✔️    |  ✔️   |   ➖    |         ➖         |
| iOS        |   ✔️    |    ⚠️    |  ✔️   |   ✔️    |         ➖         |

✔️ *: Supported* | ⚠️ *: Warning , see below* | ❌ *: not supported* | ➖ *: Not applicable*

* For Unity 2019 builds throw an error on **iOS Firefox** (Does not happen for iOS Safari or iOS Chrome): `An error occured running the Unity content on this page. See you browser JavaScript console for more info. The error: Script error.`
* Internet Explorer does not work for Unity WebGL builds, since it does not support wasm.
* Previous versions of Firefox on Android had some performance problems, they seemed to have fixed those. In general performance on mobile is by far not as good as a native app build, but should be enough for simple games.

## Notes

* If you want to use this project as a basis for your project, be sure to select the release/branch for your unity version. Beta versions oftentimes have problems, therefore I would recommend to use the latest LTS version to run your project.
* The server is configured to support wasm streaming and brotli compression, see [.htaccess 2020](./Configuration/2020/.htaccess)  [.htaccess 2019](./Configuration/2019/.htaccess)
* In order to get rid of the warning on android/iOS it is removed in a post process build step ([File](./Assets/Scripts/Editor/RemoveMobileSupportWarningWebBuild.cs))
* You might need to reload the page on android/iOS when running the first time
* This is a very small example. When building larger WebGL applications, you might run into problems with memory or compile errors. I recommend to build from the start and very often, to catch the changes that create those problems.
* If you want to have the smallest file size possible, take a look at [Project Tiny](https://forum.unity.com/forums/project-tiny.151/) or web-specific libraries like [Three.js](https://threejs.org/).
* Removing the default skybox will save ~30kb.
* With 2021.2 Unity added the possibility to use different texture formats. The builds use the default DXT format, but since no texture is used this settings does not make any difference in this project.

### URP
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

* `LogMemory()` : logs the current memory consumption
* `SetApplicationRunInBackground(int runInBackground)` : [Application.runInBackground](https://docs.unity3d.com/ScriptReference/Application-runInBackground.html)
* `SetApplicationTargetFrameRate(int targetFrameRate)` : [Application.targetFrameRate](https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html)
* `SetTimeFixedDeltaTime(float fixedDeltaTime)` : [Time.fixedDeltaTime](https://docs.unity3d.com/ScriptReference/Time-fixedDeltaTime.html)
* `SetTimeTimeScale(float timeScale)` : [Time.timeScale](https://docs.unity3d.com/ScriptReference/Time-timeScale.html)

## Older versions
| Version    | Size    | Link                                                |
| ---------- | ------- | --------------------------------------------------- |
| 2018.2.3f1 | 2.97 MB | https://deml.io/experiments/unity-webgl/2018.2.3f1/ |
| 2019.3.0f6 | 3.28 MB | https://deml.io/experiments/unity-webgl/2019.3.0f6/ |
| 2020.1.0f1 | 2.86 MB | https://deml.io/experiments/unity-webgl/2020.1.0f1/ |
| 2020.2.0f1 | 2.82 MB | https://deml.io/experiments/unity-webgl/2020.2.0f1/ |