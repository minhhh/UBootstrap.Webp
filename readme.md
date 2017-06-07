# UBootstrap.Webp

Provide an easy way to include Webp images into your project.

## Why

Webp is a very optimized image format. It will produce smaller image size with almost the same quality as other compression format such as: `ETC2`, `DXT5`, `ETC1`, `PVRTC`. Below is some comparison between `Webp` and popular compression format in Unity

| 512x512 Image | Size in KB     |
| ------------- | -------------: |
| Original      | 480            |
| ETC1 4bits    | 128            |
| ETC2 8bits    | 256            |
| Dxt5 Crunched | 64             |
| PVRTC 2 bit   | 64             |
| PVRTC 4 bit   | 128            |
| Webp Lossless | 287            |
| Webp Lossy 80 | 23             |


| 1024x1024 Image | Size in KB     |
| -------------   | -------------: |
| Original        | 1800           |
| ETC1 4bits      | 512            |
| ETC2 8bits      | 1000           |
| Dxt5 Crunched   | 183            |
| PVRTC 2 bit     | 256            |
| PVRTC 4 bit     | 512            |
| Webp Lossless   | 1200           |
| Webp Lossy 80   | 113            |

When Webp images are read in Unity, the result is a full RGBA32 image in memory, therefore, webp is only suitable for scenes in your game where you have a bit of extra memory and performance. Another issue is that it does not integrate with Unity Atlas packer, so you will have to slice your texture manually. You can of course use an external texture packer to facilitate the slicing automatically, however, it's not recommended to apply webp to anything but a few large background textures.

## Usage

The conversion from webp binary data to Unity textures is provided by the open-source project [webp-unity3d](https://github.com/minhhh/webp-unity3d). `UBootstrap.Webp` aims to provide wrapper components so that you can use seemlessly with `UIImage`, `RawImage`, `Sprite`, and `MeshRenderer`. In the case of `UIImage`, there's also a `UV Rect` property that you can use to slice the image, so there's little difference between `UIImage` and `RawImage` in the case of webp.

There are example usage of these components in `Assets/Test/Test` and `Assets/Test/TestUI` scenes.

To include `UBootstrap.Webp` into your project, you can use `npm` method of unity package management described [here](https://github.com/minhhh/UBootstrap). After installing this package, there's one extra step: Adding `--unsafe` to `Assets/smcr.rsp`.

## Changelog

**0.0.3**

* Fix type issue that caused `DllNotFoundException: webp`

**0.0.2**

* Bump version for `webp-unity3d v0.0.2`

**0.0.1**

* Add `SpriteRendererWebp`, `UIImageWebp`, `UIRawImageWebp`, `MeshRendererWebp`

<br/>
