using Codice.Client.Common;
using Codice.CM.Common.Serialization.Replication;
using CodiceApp;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class SimpleAudioManagerWindow : EditorWindow
{
    private string[] tabNames = new string[2] { "Objects in Scene", "Audio Files" };
    private int activeTabIndex;
    private int _objectIndex;
    private Vector2 scrollPosition;
    private bool[] open;

    [MenuItem("Tools/SimpleAudioManager")]
    static void OpenSimpleAudioManager()
    {
        var window = GetWindow<SimpleAudioManagerWindow>();
        window.titleContent = new GUIContent("SimpleAudioManager");
    }

    private void OnGUI()
    {
        activeTabIndex = GUILayout.SelectionGrid(activeTabIndex, tabNames, 2);

        switch (activeTabIndex)
        {
            case 0:
                // left objects || right scriptable object info (if clicked)

                GUILayout.BeginHorizontal();
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MaxWidth(300));
                GUILayout.BeginVertical(GUILayout.MinWidth(100), GUILayout.MaxWidth(300)); // left objects
                GameObject[] gameObjectsInScene = GetAllGameObjectsInScene();
                //Debug.Log(gameObjectsInScene.Length);
                for (int i = gameObjectsInScene.Length - 1; i >= 0; i--)
                {
                    if (gameObjectsInScene[i].GetComponent<SimpleAudioManager>() != null)
                    {
                        if (GUILayout.Button(gameObjectsInScene[i].name))
                        {
                            _objectIndex = i;
                            Selection.activeGameObject = gameObjectsInScene[i];
                        }
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
                
                GUILayout.BeginVertical(); // right SO info
                if (_objectIndex <= gameObjectsInScene.Length)
                {
                    if (gameObjectsInScene[_objectIndex].GetComponent<SimpleAudioManager>() != null)
                    {
                        ShowSOInfo(gameObjectsInScene[_objectIndex].name, gameObjectsInScene[_objectIndex].GetComponent<SimpleAudioManager>().Files);
                    }
                    else
                    {
                        // Attach new Script if button pressed
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                break;
            case 1:

                break;
            default:
                break;
        }
    }

    private GameObject[] GetAllGameObjectsInScene()
    {
        return (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
    }

    private void ShowSOInfo(string objectName, List<AudioFile> audioFiles)
    {
        Event evt = Event.current;
        Vector2 mousePos = evt.mousePosition;
        var oldColor = GUI.color;

        if (open.Length <= 0)
        {
            open = new bool[audioFiles.Count];
        }

        for (int i = 0; i < audioFiles.Count; i++)
        {
            GUI.color = Color.red;
            open[i] = EditorGUILayout.Foldout(open[i], audioFiles[i].name, true);
            GUI.color = oldColor;
            if (open[i])
            {
                GUI.color = Color.green;
                EditorGUILayout.LabelField(audioFiles[i].Name, EditorStyles.boldLabel);
                GUI.color = oldColor;
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Name", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
                audioFiles[i].Name = EditorGUILayout.TextField(audioFiles[i].Name);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("AudioClip", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
                GUILayout.FlexibleSpace();
                audioFiles[i].AudioClip = (AudioClip)EditorGUILayout.ObjectField(audioFiles[i].AudioClip, typeof(AudioClip), false, GUILayout.Width(200), GUILayout.Height(50));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Audio Volume", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
                audioFiles[i].AudioVolume = EditorGUILayout.Slider(audioFiles[i].AudioVolume, 0f, 100f);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Loop", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
                audioFiles[i].Loop = EditorGUILayout.Toggle(audioFiles[i].Loop);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Loop Amount", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
                audioFiles[i].LoopAmount = EditorGUILayout.IntField(audioFiles[i].LoopAmount);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }
        }
    }
}
