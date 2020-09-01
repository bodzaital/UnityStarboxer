using UnityEngine;

public class Starboxer : MonoBehaviour
{
    public enum SkyboxResolutionLOD
    {
        Low = 128,
        Medium = 512,
        High = 1024,
        Ultra = 2048,
        _4K = 4096,
    }

    [Tooltip("The resolution of the skybox.")]
    public SkyboxResolutionLOD SkyboxLOD;

    [Tooltip("Scale of the Perlin noise.")]
    public float Scale;

    [Header("Origins")]
    [Tooltip("Check to set a fixed origin, or uncheck to generate a random origin every time.")]
    public bool FixedOrigin;

    [Tooltip("The X origin of the Perlin noise.")]
    public float FixedXOrigin;

    [Tooltip("The Y origin of the Perlin noise.")]
    public float FixedYOrigin;

    int skyboxResolution;

    void Start()
    {
        skyboxResolution = (int)SkyboxLOD;

        float xOrg = FixedOrigin ? FixedXOrigin : Random.Range(0, 1000);
        float yOrg = FixedOrigin ? FixedYOrigin : Random.Range(0, 1000);

        Material mat = new Material(Shader.Find("Skybox/6 Sided"));

        for (int i = 0; i < 6; i++)
        {
            Texture2D tex = new Texture2D(skyboxResolution, skyboxResolution);
            Color[] col = new Color[skyboxResolution * skyboxResolution];

            col.GetSkyboxFace(xOrg + i * Scale, yOrg - i * Scale, skyboxResolution, Scale);
            
            tex.SetPixels(col);
            tex.Apply();

            switch (i)
            {
                case 0:
                    mat.SetTexture("_FrontTex", tex);
                    break;
                case 1:
                    mat.SetTexture("_BackTex", tex);
                    break;
                case 2:
                    mat.SetTexture("_LeftTex", tex);
                    break;
                case 3:
                    mat.SetTexture("_RightTex", tex);
                    break;
                case 4:
                    mat.SetTexture("_UpTex", tex);
                    break;
                case 5:
                    mat.SetTexture("_DownTex", tex);
                    break;
            }
        }

        if (this.GetComponent<Skybox>() == null)
        {
            this.gameObject.AddComponent<Skybox>();
        }

        this.GetComponent<Skybox>().material = mat;
    }
}

public static class ColorExtensions
{
    public static void GetSkyboxFace(this Color[] c, float xOrg, float yOrg, int resolution, float scale)
    {
        int buffer = Random.Range(10, 100);

        float y = 0.0f;
        while (y < resolution)
        {
            float x = 0.0f;
            while (x < resolution)
            {
                buffer--;

                if (buffer == 0)
                {
                    int pos = Mathf.RoundToInt(y) * resolution + Mathf.RoundToInt(x);
                    float star_dist = Random.Range(0.0f, 1.0f);

                    if (star_dist < 0.66f)
                    {
                        c.DrawStar(pos, resolution, new Color(1.0f, 0.7f, 0.4f), false);
                    }
                    else if (star_dist < 0.9f)
                    {
                        c.DrawStar(pos, resolution, new Color(0.9f, 0.9f, 1.0f), false);
                    }
                    else
                    {
                        c.DrawStar(pos, resolution, new Color(0.6f, 0.7f, 1.0f), true);
                    }

                    float xCoord = xOrg + x / resolution * scale;
                    float yCoord = yOrg + y / resolution * scale;

                    float sample = Mathf.PerlinNoise(xCoord, yCoord);

                    buffer = (int)Random.Range(50, 1000 * (1 / sample));
                }

                x++;
            }

            y++;
        }
    }

    private static void DrawStar(this Color[] c, int pos, int resolution, Color color, bool large)
    {
        c[pos] = color;

        if (!large)
        {
            return;
        }

        if (pos - 1 >= 0)
        {
            c[pos - 1] = color;
        }

        if (pos + 1 < c.Length)
        {
            c[pos + 1] = color;
        }

        if (pos - resolution >= 0)
        {
            c[pos - resolution] = color;
        }

        if (pos + resolution < c.Length)
        {
            c[pos + resolution] = color;
        }
    }
}