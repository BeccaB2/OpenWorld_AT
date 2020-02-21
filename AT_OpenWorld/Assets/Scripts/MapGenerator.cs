using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh};
    public DrawMode drawMode;

    const int chunkSize = 241;

    public int width = chunkSize;
    public int height = chunkSize;

    [Range(0,6)]
    public int lod;
    public float noiseScale;

    public int octaves;

    [Range(0,1)]
    public float persistance;

    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve heightCurve;

    public bool autoUpdate;

    public TerrainTypes[] areas;

    public void GenerateMap()
    {
        // Fetch the noise map values from noise class
        float[,] noiseMap = Noise.GenerateNoiseMap(chunkSize, chunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        // Array to store colours
        Color[] colourMap = new Color[chunkSize * chunkSize];

        // Searching the expanse of the map to apply the correct colour to each area within certain heights
        for(int y = 0; y < chunkSize; y++)
        {
            for(int x = 0; x < chunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];

                // Looks for all different types of terrain and searches for the heights to apply them to
                for(int i = 0; i < areas.Length; i++)
                {
                    if(currentHeight <= areas[i].height)
                    {
                        // Setting the area to the correct colour
                        colourMap[y * chunkSize + x] = areas[i].colour;
                        break;
                    }
                }
            }
        }

        // Calling the display map functionality & drawing it onto the plane
        DisplayMap display = FindObjectOfType<DisplayMap>();

        if(drawMode == DrawMode.NoiseMap)
        {
            // Display the texture based on the noise map
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColourMap)
        {
            // Display the colours that have been assigned to each area
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, chunkSize, chunkSize));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, heightCurve, lod),
                TextureGenerator.TextureFromColourMap(colourMap, chunkSize, chunkSize));
        }
    }

    void OnValidate()
    {
        // Making sure cannot input values less than 1
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }

        // Octaves cannot go below 0
        if(octaves < 0)
        {
            octaves = 0;
        }
    }
}

// Will show in inspector
[System.Serializable]
public struct TerrainTypes
{
    public string Type;
    public float height;
    public Color colour;
}
