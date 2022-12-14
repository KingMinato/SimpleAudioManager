using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SimpleAudioManagerWindow : EditorWindow
{
    private string[] tabNames = new string[2] { "Objects in Scene", "Audio Files" };
    private int activeTabIndex;
    private int _objectIndex;
    private int _objectIndex2;
    private Vector2 scrollPosition;
    private Vector2 scrollPosition2;
    private List<bool> open;
    private bool allObjects = false;
    private AudioFile newAudioFile;
    private GUIStyle style;

    private string relativePath;
    private string path;
    private AudioFile[] scriptableObjectsInFolder;

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
                if (allObjects)
                {
                    if (GUILayout.Button("Show Objects with Audio Manager"))
                    {
                        allObjects = false;
                    }
                    ShowAllObjects();
                }
                else
                {
                    if (GUILayout.Button("Show All Objects"))
                    {
                        allObjects = true;
                    }
                    ShowAudioManagerObjects();
                }
                break;
            case 1:
                ShowLocationInput();
                if (path != null)
                {
                    ShowScriptableObjectsInFolder(GetSOFromDir(path));
                }
                break;
            default:
                break;
        }
    }

    #region Tab Objects in Scene
    private GameObject[] GetAllGameObjectsInScene()
    {
        return (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
    }

    private void ShowAllObjects()
    {
        GUILayout.BeginHorizontal();
        Color oldColor = GUI.color;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MaxWidth(300));
        GUILayout.BeginVertical(GUILayout.MinWidth(100), GUILayout.MaxWidth(300));
        GameObject[] gameObjectsInScene = GetAllGameObjectsInScene();
        for (int i = gameObjectsInScene.Length - 1; i >= 0; i--)
        {
            if (_objectIndex == i)
            {
                GUI.color = Color.green;
                if (GUILayout.Button(gameObjectsInScene[i].name))
                {
                    open = null;
                    _objectIndex = i;
                    Selection.activeGameObject = gameObjectsInScene[i];
                }
                GUI.color = oldColor;
            }
            else
            {
                if (GUILayout.Button(gameObjectsInScene[i].name))
                {
                    open = null;
                    _objectIndex = i;
                    Selection.activeGameObject = gameObjectsInScene[i];
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUILayout.BeginVertical();
        if (_objectIndex <= gameObjectsInScene.Length)
        {
            if (gameObjectsInScene[_objectIndex].GetComponent<SimpleAudioManager>() != null)
            {
                ShowSOInfo(gameObjectsInScene[_objectIndex].name, gameObjectsInScene[_objectIndex].GetComponent<SimpleAudioManager>().Files);
            }
            else
            {
                ShowAttachScriptOption(gameObjectsInScene[_objectIndex]);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void ShowAudioManagerObjects()
    {
        GUILayout.BeginHorizontal();
        Color oldColor = GUI.color;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MaxWidth(300));
        GUILayout.BeginVertical(GUILayout.MinWidth(100), GUILayout.MaxWidth(300));
        GameObject[] gameObjectsInScene = GetAllGameObjectsInScene();
        for (int i = gameObjectsInScene.Length - 1; i >= 0; i--)
        {
            if (gameObjectsInScene[i].GetComponent<SimpleAudioManager>() != null)
            {
                if (_objectIndex == i)
                {
                    GUI.color = Color.green;
                    if (GUILayout.Button(gameObjectsInScene[i].name))
                    {
                        open = null;
                        _objectIndex = i;
                        Selection.activeGameObject = gameObjectsInScene[i];
                    }
                    GUI.color = oldColor;
                }
                else
                {
                    if (GUILayout.Button(gameObjectsInScene[i].name))
                    {
                        open = null;
                        _objectIndex = i;
                        Selection.activeGameObject = gameObjectsInScene[i];
                    }
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUILayout.BeginVertical();
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
    }

    private void ShowSOInfo(string objectName, List<AudioFile> audioFiles)
    {
        var oldColor = GUI.color;

        if (open == null)
        {
            open = new List<bool>();
            for (int i = 0; i < audioFiles.Count; i++)
            {
                open.Add(false);
            }
        }

        for (int i = 0; i < audioFiles.Count; i++)
        {
            GUI.color = Color.green;
            open[i] = EditorGUILayout.Foldout(open[i], audioFiles[i].name, true);
            GUI.color = oldColor;
            if (open[i])
            {
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

                EditorGUILayout.BeginHorizontal();
                style = new GUIStyle(GUI.skin.button);
                style.normal.textColor = Color.red;
                if (GUILayout.Button("Delete " + audioFiles[i].Name, style))
                {
                    if (EditorUtility.DisplayDialog("Delete AudioFile", "Are you sure you want to delete AudioFile " + audioFiles[i].Name, "Delete", "Cancel"))
                    {
                        audioFiles.Remove(audioFiles[i]);
                        open.Remove(audioFiles[i]);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add AudioFile"))
        {
            if (newAudioFile == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select an AudioFile", "OK");
            }
            else
            {
                audioFiles.Add(newAudioFile);
                newAudioFile = null;
                open.Add(false);
            }
        }
        newAudioFile = (AudioFile)EditorGUILayout.ObjectField(newAudioFile, typeof(AudioFile), false, GUILayout.Width(200), GUILayout.Height(50));
        GUILayout.EndHorizontal();
    }

    private void ShowAttachScriptOption(GameObject gameObject)
    {
        if (GUILayout.Button("Attach SimpleAudioManager"))
        {
            gameObject.AddComponent<SimpleAudioManager>();
        }
    }
    #endregion

    #region Tab Audio Files
    private void ShowLocationInput()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select Path"))
        {
            //path = "Assets/SimpleAudioManager/Preset Audio Files/AudioFiles";
            relativePath = EditorUtility.SaveFolderPanel("Select Path", "Assets/", "");
            path = relativePath.Substring(relativePath.IndexOf("Assets/"));
        }
        GUILayout.Label(path);
        EditorGUILayout.EndHorizontal();
    }

    private void ShowScriptableObjects(AudioFile audioFile)
    {
        //AudioFile audioFile = AssetDatabase.LoadAssetAtPath<AudioFile>(file);

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
        audioFile.Name = EditorGUILayout.TextField(audioFile.Name);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("AudioClip", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
        GUILayout.FlexibleSpace();
        audioFile.AudioClip = (AudioClip)EditorGUILayout.ObjectField(audioFile.AudioClip, typeof(AudioClip), false, GUILayout.Width(200), GUILayout.Height(50));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Audio Volume", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
        audioFile.AudioVolume = EditorGUILayout.Slider(audioFile.AudioVolume, 0f, 100f);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Loop", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
        audioFile.Loop = EditorGUILayout.Toggle(audioFile.Loop);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Loop Amount", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
        audioFile.LoopAmount = EditorGUILayout.IntField(audioFile.LoopAmount);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private AudioFile[] GetSOFromDir(string path)
    {
        string[] fileDirs = Directory.GetFiles(path, "*.asset", SearchOption.TopDirectoryOnly);
        AudioFile[] audioFiles = new AudioFile[fileDirs.Length];
        for (int i = 0; i < audioFiles.Length; i++)
        {
            audioFiles[i] = AssetDatabase.LoadAssetAtPath<AudioFile>(fileDirs[i]);
        }
        return audioFiles;
    }

    private void ShowScriptableObjectsInFolder(AudioFile[] audioFiles)
    {
        GUILayout.BeginHorizontal();
        Color oldColor = GUI.color;
        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition, GUILayout.MaxWidth(300));
        GUILayout.BeginVertical(GUILayout.MinWidth(100), GUILayout.MaxWidth(300));
        for (int i = audioFiles.Length - 1; i >= 0; i--)
        {
            if (_objectIndex2 == i)
            {
                GUI.color = Color.green;
                if (GUILayout.Button(audioFiles[i].name))
                {
                    open = null;
                    _objectIndex2 = i;
                }
                GUI.color = oldColor;
            }
            else
            {
                if (GUILayout.Button(audioFiles[i].name))
                {
                    open = null;
                    _objectIndex2 = i;
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUILayout.BeginVertical();
        if (_objectIndex2 <= audioFiles.Length)
        {
            ShowScriptableObjects(audioFiles[_objectIndex2]);
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
    #endregion
}