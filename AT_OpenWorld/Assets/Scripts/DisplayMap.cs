using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour
{
    // Object to display the created map on
    public Renderer renderTexture;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        // For finding the width/height of the noise map
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        // Create a texture using these dimensions
        Texture2D texture = new Texture2D(width, height);

        // Set the colour of the pixels in the texture
        Color[] colourMap = new Color[width * height];

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                // Creating an array to set the colour of each index by finding the index of the rows & collumns
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        // Apply the colours to texture
        texture.SetPixels(colourMap);
        texture.Apply();

        // Set texture to render
        renderTexture.sharedMaterial.mainTexture = texture;

        // Set size of the plane to same as map
        renderTexture.transform.localScale = new Vector3(width, 1, height);

    }
}
