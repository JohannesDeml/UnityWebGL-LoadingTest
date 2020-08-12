# Unity WebGL Loadin Test

![Preview](./preview.png)

Testing Unity's WebGL capabilities for different versions and platforms

Version | Size | Link
--- | --- | ---
2018.2.3f1 | 2.97 MB (3,121,037 bytes) | https://deml.io/experiments/unity-webgl/2018.2.3f1/
2019.3.0f6 | 3.28 MB (3,448,034 bytes) | https://deml.io/experiments/unity-webgl/2019.3.0f6/
2020.0.1f1 | 2.86 MB (3,007,240 bytes) | https://deml.io/experiments/unity-webgl/2020.1.0f1/

## Platforms

| Platform   | Chrome | Firefox | Safari | Edge | Internet Explorer |
| ---------- | :----: | :-----: | :----: | :--: | :---------------: |
| Windows 10 |   ✔️    |    ✔️    |   ➖    |  ✔️   |         ❌         |
| Mac        |   ✔️    |    ✔️    |   ✔️    |  ✔️   |         ➖         |
| Android    |   ✔️    |    ❌    |   ➖    |  ❌   |         ➖         |
| iOS        |   ✔️    |    ✔️    |   ✔️    |  ✔️   |         ➖         |

## Features

* Physics
* GPU Instancing for materials
* Shadows
* Brotli Compression

## Notes

* The server is configured to support wasm streaming and brotli compression, see [.htaccess 2020](./Configuration/2020/.htaccess)  [.htaccess 2019](./Configuration/2019/.htaccess)
* In order to get rid of the warning on iOS it is removed in a post process build step ([File](./Assets/Scripts/Editor/RemoveMobileSupportWarningWebBuild.cs))
* You might need to reload the page on iOS when running the first time

For further information check out the [forum-thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/).
