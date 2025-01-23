using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    void Update()
    {
        // Update delta time
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Calculate FPS
        float fps = 1.0f / deltaTime;
        string text = Mathf.Ceil(fps).ToString() + " FPS";

        // Set text style
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.white;

        // Display on screen
        GUI.Label(new Rect(10, 10, 200, 50), text, style);
    }
}
