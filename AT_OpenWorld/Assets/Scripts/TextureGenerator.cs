using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        // Create a texture using dimensions of map
        Texture2D texture = new Texture2D(width, height);

        // Make the map less blurry & fix wrapping issue
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        // For finding the width/height of the noise map
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // Set the colour of the pixels in the texture
        Color[] colourMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Creating an array to set the colour of each index by finding the index of the rows & collumns in the map
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return TextureFromColourMap(colourMap, width, height);
    }
}
