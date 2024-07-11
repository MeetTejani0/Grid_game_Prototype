using UnityEngine;
using UnityEditor;

public class ObstacleWindow : EditorWindow
{

    private int buttonIndex =0;
    private string[] buttonNames = new string[100];

    private void OnEnable()
    {
        for (int i = 0; i < 100; i++)
        {
            buttonNames[i] = "Button " + (i+1);
        }
    }
/*    private void Awake()
    {
        
    }*/

    [MenuItem("Window/ObstacleWindow Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ObstacleWindow>("Obstacles In Grid");
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        buttonIndex = GUILayout.SelectionGrid(buttonIndex, buttonNames, 10);

        GUILayout.EndHorizontal();
        if (GUILayout.Button("Start Changing Obstacle"))
        {
            Debug.Log("Changes are made " + buttonNames[buttonIndex]);
        }
    }

}
