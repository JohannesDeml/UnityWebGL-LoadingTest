# Unity WebGL Loading Test

![Preview](./preview.png)

[![](https://img.shields.io/github/release-date/JohannesDeml/UnityWebGL-LoadingTest.svg)](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/releases) [![Tested up to Unity 2021.2](https://img.shields.io/badge/tested%20up%20to%20unity-2021.2-green.svg?logo=unity&cacheSeconds=2592000)](https://unity3d.com/get-unity/download/archive)

*Testing Unity's WebGL size and loading time for different versions and platforms*  
[Unity Forum Thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/).


## Features

* Physics
* GPU Instancing for materials
* Shadows
* Brotli Compression
* Togglable In-DOM Debug console ([Example](https://deml.io/experiments/unity-webgl/2021.1.4f1/))
* Handy debug functions for times and memory consumption
* Responsive template layout for maximum mobile compatibility
* Works with [Unity WebGL Publisher](https://play.unity.com/discover/all-showcases) (Use  [2020.3-lts](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/tree/2020-lts) or [2020.3-lts-urp](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/tree/2020-lts-urp) branch)
* Branches for different Unity versions

## Live Demos

### Default
Version | Size | Link
--- | --- | ---
2019.4.28f1 | 2.97 MB | https://deml.io/experiments/unity-webgl/2019.4.28f1/ 
2020.3.12f1 | 2.87 MB | https://deml.io/experiments/unity-webgl/2020.3.12f1/ 
2021.1.0f1 | 2.85 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1/ 
2021.2.0b1 | 2.67 MB | https://deml.io/experiments/unity-webgl/2021.2.0b1/ 

### Minimum size
Version | Size | Link
--- | --- | ---
2019.4.28f1 Min | 2.94 MB | https://deml.io/experiments/unity-webgl/2019.4.28f1-minsize/ 
2020.3.12f1 Min | 2.43 MB | https://deml.io/experiments/unity-webgl/2020.3.12f1-minsize/ 
2021.1.0f1 Min | 2.45 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1-minsize/ 

### URP
Version | Size | Link
--- | --- | ---
2019.4.28f1 URP | 5.48 MB | https://deml.io/experiments/unity-webgl/2019.4.28f1-urp/ 
2020.3.12f1 URP | 5.34 MB | https://deml.io/experiments/unity-webgl/2020.3.12f1-urp/ 
2021.1.0f1 URP | 5.47 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1-urp/ 
2021.2.0b1 URP | 4.73 MB | https://deml.io/experiments/unity-webgl/2021.2.0b1-urp/ 

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


### Min Size builds
* Built from branch [minsize](../../tree/minsize)
* Built with Code Optimization: `Size` and IL2CPP Code Generation: `Faster (smaller) builds`
* Enable Exceptions: `None`
* C++ Compiler Configuration: `Master` 

## Older versions
Version | Size | Link
--- | --- | ---
2018.2.3f1 | 2.97 MB | https://deml.io/experiments/unity-webgl/2018.2.3f1/ 
2019.3.0f6 | 3.28 MB | https://deml.io/experiments/unity-webgl/2019.3.0f6/
2020.1.0f1 | 2.86 MB | https://deml.io/experiments/unity-webgl/2020.1.0f1/
2020.2.0f1 | 2.82 MB | https://deml.io/experiments/unity-webgl/2020.2.0f1/ 