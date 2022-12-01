//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CanEditMultipleObjects]
//[CustomEditor(typeof(AudioManager))]
//public class AudioManagerInspector : Editor
//{
//    private AudioManager _audioManager;

//    private void OnEnable()
//    {
//        _audioManager = (AudioManager)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawAudioManagerInspector();
//    }

//    private void DrawAudioManagerInspector()
//    {


//        if (_audioManager.Files.Count != 0)
//        {
//            foreach (AudioFile item in _audioManager.Files)
//            {
//                EditorGUILayout.LabelField(item.Name, EditorStyles.boldLabel);
//                EditorGUILayout.BeginVertical();
//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField("Name", EditorStyles.boldLabel);
//                item.Name = EditorGUILayout.TextField(item.Name);
//                EditorGUILayout.EndHorizontal();

//                item.AudioSource = (AudioSource)EditorGUILayout.ObjectField("Audio Source", item.AudioSource, typeof(AudioSource), false);
//                item.AudioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", item.AudioClip, typeof(AudioClip), false);

//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField("Audio Volume", EditorStyles.boldLabel);
//                item.AudioVolume = EditorGUILayout.Slider(item.AudioVolume, 0f, 100f);
//                EditorGUILayout.EndHorizontal();

//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField("Loop", EditorStyles.boldLabel);
//                item.Loop = EditorGUILayout.Toggle(item.Loop);
//                EditorGUILayout.EndHorizontal();

//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField("Loop Amount", EditorStyles.boldLabel);
//                item.LoopAmount = EditorGUILayout.IntField(item.LoopAmount);
//                EditorGUILayout.EndHorizontal();

//                EditorGUILayout.EndVertical();

//                if (GUILayout.Button("Remove Audio"))
//                {
//                    RemoveObjectFromList(item);
//                }
//            }

//            if (GUILayout.Button("Create New Audio"))
//            {
//                AddObjectToList();
//            }
//        }
//        else
//        {
//            if (GUILayout.Button("Create New Audio"))
//            {
//                AddObjectToList();
//            }
//        }
//    }

//    private void AddObjectToList()
//    {
//        _audioManager.Files.Add(new AudioFile());
//    }

//    private void RemoveObjectFromList(AudioFile file)
//    {
//        _audioManager.Files.Remove(file);
//    }
//}
