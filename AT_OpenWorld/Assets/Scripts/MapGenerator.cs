﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public bool autoUpdate;
    public void GenerateMap()
    {
        // Fetch the noise map from noise class
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity);

        // Calling the display map functionality & drawing it onto the plane
        DisplayMap display = FindObjectOfType<DisplayMap>();
        display.DrawNoiseMap(noiseMap);
    }
}
