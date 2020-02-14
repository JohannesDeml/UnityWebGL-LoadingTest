# Introduction

![Preview](./preview.png)

Testing Unity's WebGL capabilities for different versions and Platforms

Version | Size | Link
--- | --- | ---
2018.2.3f1 | 2.97 MB (3,121,037 bytes) | https://deml.io/experiments/unity-webgl/2018.2.3f1/
2019.3.0b8 | 4.06 MB (4,257,932 bytes) | https://deml.io/experiments/unity-webgl/2019.3.0b8/
2019.3.0f6 | 3.28 MB (3,448,034 bytes) | https://deml.io/experiments/unity-webgl/2019.3.0f6/

## Notes
* The server is configured to support wasm streaming, see [.htaccess file](./Configuration/.htaccess)  
* In order to get rid of the warning on iOS it is removed in a post process build step ([File](./Assets/Scripts/Editor/RemoveMobileSupportWarningWebBuild.cs))

For further information check out the [forum-thread](https://forum.unity.com/threads/webgl-builds-for-mobile.545877/).
