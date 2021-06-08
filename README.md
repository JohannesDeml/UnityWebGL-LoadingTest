# Unity WebGL Loading Test

![Preview](./preview.png)

[![](https://img.shields.io/github/release-date/JohannesDeml/UnityWebGL-LoadingTest.svg)](https://github.com/JohannesDeml/UnityWebGL-LoadingTest/releases) [![Tested up to Unity 2020.2](https://img.shields.io/badge/tested%20up%20to%20unity-2021.1-green.svg?logo=unity&cacheSeconds=2592000)](https://unity3d.com/get-unity/download/archive)

*Testing Unity's WebGL capabilities for different versions and platforms*


## Features

* Physics
* GPU Instancing for materials
* Shadows
* Brotli Compression
* Togglable In-DOM Debug console ([Example](https://deml.io/experiments/unity-webgl/2021.1.4f1/))
* Handy debug functions for times and memory consumption
* Responsive template layout for maximum mobile compatibility

## Live Demos

Version | Size | Link
--- | --- | ---
2019.3.0f6 | 3.28 MB | https://deml.io/experiments/unity-webgl/2019.3.0f6/
2019.4.25f1 | 2.96 MB | https://deml.io/experiments/unity-webgl/2019.4.25f1/ 
2020.1.0f1 | 2.86 MB | https://deml.io/experiments/unity-webgl/2020.1.0f1/
2020.2.0f1 | 2.82 MB | https://deml.io/experiments/unity-webgl/2020.2.0f1/ 
2020.2.0f1 URP | 5.27 MB | https://deml.io/experiments/unity-webgl/2020.2.0f1-urp/ 
2020.3.0f1 | 2.83 MB | https://deml.io/experiments/unity-webgl/2020.3.0f1/ 
2020.3.0f1 URP | 5.27 MB | https://deml.io/experiments/unity-webgl/2020.3.0f1-urp/ 
2021.1.0f1 | 2.85 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1/ 
2021.1.0f1 URP | 5.47 MB | https://deml.io/experiments/unity-webgl/2021.1.0f1-urp/ 
2021.2.0a19 | 2.95 MB | https://deml.io/experiments/unity-webgl/2021.2.0a19/ 

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

* The server is configured to support wasm streaming and brotli compression, see [.htaccess 2020](./Configuration/2020/.htaccess)  [.htaccess 2019](./Configuration/2019/.htaccess)
* The builds are optimized for speed. When switching to Size and changing the C++ config to Master, the size decreases by ~40 KB ( Example: [2021.1.0f1 Min Size (2.45MB)](https://deml.io/experiments/unity-webgl/2021.1.0f1-minsize/) ).
* In order to get rid of the warning on android/iOS it is removed in a post process build step ([File](./Assets/Scripts/Editor/RemoveMobileSupportWarningWebBuild.cs))
* You might need to reload the page on android/iOS when running the first time
* This is a very small example. When building larger WebGL applications, you might run into problems with memory or compile errors. I recommend to build from the start and very often, to catch the changes that create those problems.
* If you want to have the smallest file size possible, take a look at [Project Tiny](https://forum.unity.com/forums/project-tiny.151/) or web-specific libraries like [Three.js](https://threejs.org/).
* URP adds additional ~2.5 MB file size compared to the builtin render pipeline.
* Removing the default skybox will save ~30kb.
* In general the performance for URP seems to be better compared to builtin, but it has a larger build size.

For further information check out the [forum-thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/).