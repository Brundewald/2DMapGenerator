using System;
using Controller;
using UnityEditor;
using UnityEngine;
using View;

namespace MyEditor
{
    [CustomEditor(typeof(LevelGeneratorView))]
    public class LevelGenerationEditor : Editor
    {
        private LevelGeneratorController _levelGeneratorController;

        private void OnEnable()
        {
            var levelGeneratorView = (LevelGeneratorView) target;
            _levelGeneratorController = new LevelGeneratorController(levelGeneratorView);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var tileMap = serializedObject.FindProperty("_tileMapGround");

            if (GUI.Button(new Rect(10, 0, 100, 50), "Generate")) 
                _levelGeneratorController.Awake();
            if (GUI.Button(new Rect(10,55,100,50),"Clear"))
                _levelGeneratorController.Clear();
            
            GUILayout.Space(100);

            serializedObject.ApplyModifiedProperties();
        }
    }   
}
