# UnityStarboxer

![Starfield behind Earth](https://raw.githubusercontent.com/bodzaital/UnityStarboxer/master/header.png)

Dynamically generated starfield skybox for Unity.

## How to use

1. Download the [latest release](https://github.com/bodzaital/UnityStarboxer/releases).
2. Add the script to your Unity project.
3. Attach the script to a Camera that will draw the skybox.

## Settings

![Starfield behind Earth](https://raw.githubusercontent.com/bodzaital/UnityStarboxer/master/inspector.png)

### Skybox LOD

The level of detail, sets the resolution of the skybox texture.

- Low: 128x128
- Medium: 512x512
- High: 1024x1024
- Ultra: 2048x2048
- 4K: 4096x4096

The higher the resolution, the smaller the stars are and slower the generation goes, and vice versa.

### Scale

A scale factor for the Perlin noise generation.

### Origins

#### Fixed Origin

The Perlin noise will generate a random X and Y origin in the 0-1000 range on every Start(). If you want to set a fixed origin, check this box.

#### Fixed X Origin

The X origin of the Perlin noise generation.

#### Fixed Y Origin

The Y origin of the Perlin noise generation.
