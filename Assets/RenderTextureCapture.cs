using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureCapture : MonoBehaviour
{
    public Camera captureCamera;
    public RenderTexture renderTexture;
    public Image targetImage;

    void Start()
    {
        if (captureCamera == null || renderTexture == null || targetImage == null)
        {
            Debug.LogError("Capture Camera, Render Texture, or Target Image not assigned!");
            return;
        }

        // Set the target texture of the capture camera to the render texture
        captureCamera.targetTexture = renderTexture;

        // Render the GameObject to the render texture
        captureCamera.Render();

        // Convert the render texture to a Texture2D
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;

        // Convert the Texture2D to a Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        // Set the sprite to the target image
        targetImage.sprite = sprite;
    }
}
