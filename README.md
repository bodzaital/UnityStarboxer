# UnityStarboxer

![Starfield behind Earth](https://raw.githubusercontent.com/bodzaital/UnityStarboxer/master/header.png)

Dynamically generated starfield skybox for Unity.

## How it works & Performance

For each 6 faces of the skybox, the script generates stars based on a Perlin noise distribution. The first star is drawn randomly between `10` and `1000`, after that, the next star is drawn between `50` and `1000 * (1 / sample)` where the sample is the current Perlin noise value. If this distance is over, the size of the star is generated (66% roughly K-type, 24% roughly F-type, 10% roughly B-type), and another Perlin noise value is sampled.

On my laptop (i5 7440HQ + GeForce 930MX), each face takes roughly 25-30 ms to generate. I guess this could be made faster with compute shaders, but that falls outside my comfort zone. The heavy part is the two nested while loops that go over the `Color[skyboxResolution * skyboxResolution]` array.

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

The Perlin noise will generate a random X and Y origin in the `0` and `1000` range on every `Start()`. If you want to set a fixed origin, check this box.

#### Fixed X Origin

The X origin of the Perlin noise generation.

#### Fixed Y Origin

The Y origin of the Perlin noise generation.
