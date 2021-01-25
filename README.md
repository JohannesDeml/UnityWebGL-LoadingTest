# Unity WebGL Loading Test

![Preview](./preview.png)

Testing Unity's WebGL capabilities for different versions and platforms

Version | Size | Link
--- | --- | ---
2018.2.3f1 | 2.97 MB | https://deml.io/experiments/unity-webgl/2018.2.3f1/
2019.3.0f6 | 3.28 MB | https://deml.io/experiments/unity-webgl/2019.3.0f6/
2020.0.1f1 | 2.86 MB | https://deml.io/experiments/unity-webgl/2020.1.0f1/
2020.2.0f1 | 2.82 MB | https://deml.io/experiments/unity-webgl/2020.2.0f1/ 
2020.2.0f1 URP | 5.27 MB | https://deml.io/experiments/unity-webgl/2020.2.0f1-urp/ 
2021.1.0b3 | 2.84 MB | https://deml.io/experiments/unity-webgl/2021.1.0b3/ 

## Platforms

| Platform   | Chrome | Firefox | Safari | Edge | Internet Explorer |
| ---------- | :----: | :-----: | :----: | :--: | :---------------: |
| Windows 10 |   ✔️    |    ✔️    |   ➖    |  ✔️   |         ❌         |
| Mac        |   ✔️    |    ✔️    |   ✔️    |  ✔️   |         ➖         |
| Android    |   ✔️    |    ⚠️    |   ➖    |  ✔️   |         ➖         |
| iOS        |   ✔️    |    ✔️    |   ✔️    |  ✔️   |         ➖         |

✔️ *: Supported* | ⚠️ *: Supported, but runs poorly* | ❌ *: not supported* | ➖ *: Not applicable*

## Features

* Physics
* GPU Instancing for materials
* Shadows
* Brotli Compression

## Notes

* The server is configured to support wasm streaming and brotli compression, see [.htaccess 2020](./Configuration/2020/.htaccess)  [.htaccess 2019](./Configuration/2019/.htaccess)
* In order to get rid of the warning on android/iOS it is removed in a post process build step ([File](./Assets/Scripts/Editor/RemoveMobileSupportWarningWebBuild.cs))
* You might need to reload the page on android/iOS when running the first time
* This is a very small example. When building larger WebGL applications, you might run into problems with memory or compile errors. I recommend to build from the start and very often, to catch the changes that create those problems.
* If you want to have the smallest file size possible, take a look at [Project Tiny](https://forum.unity.com/forums/project-tiny.151/) or web-specific libraries like [Three.js](https://threejs.org/).
* URP adds additional 2.5 MB file size compared to the builtin render pipeline.
* Removing the default skybox will save ~30kb.
* In general the performance for URP seems to be better compared to builtin, but it has a larger build size.
* Internet Explorer does not work for Unity WebGL builds, since it does not support wasm.

For further information check out the [forum-thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/).
