using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SBG.Memento.Editor
{
	public class SaveFileInspectorEditorWindow : EditorWindow
	{
		private readonly string[] seperators = new string[] {"/", "\\"};

		private string _path;
		private string _filename;
		private int _versionNr;
		private SaveData _cache;

		private Vector2 _scrollPos = Vector2.zero;
		private Dictionary<string, bool> _foldouts = new Dictionary<string, bool>();


		[MenuItem("Window/Star Banner Games/Memento/SaveFile Inspector")]
		public static void ShowWindow()
		{
			GetWindow<SaveFileInspectorEditorWindow>("SaveFile Inspector");
		}

		private void OnGUI()
		{
			if (GUILayout.Button("Select File", GUILayout.Height(25)))
            {
				string basePath = $"{Application.persistentDataPath}/Memento";
				_path = EditorUtility.OpenFilePanel("Open Save File", basePath, "bin,st");
				if (File.Exists(_path))
                {
					_cache = InternalProcessing.LoadFromBinaryFile(_path, out _versionNr);
					_foldouts.Clear();

					string[] pathChunks = _path.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
					_filename = pathChunks[pathChunks.Length - 1];
				}
			}

			if (_cache == null) return;

			EditorGUILayout.LabelField(_filename, EditorStyles.boldLabel);
			EditorGUILayout.LabelField($"Version: {_versionNr}");
			EditorGUILayout.LabelField($"Path: {_path}", EditorStyles.miniLabel);
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Save Data:", EditorStyles.boldLabel);
			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
			DisplaySubData(_cache);
			EditorGUILayout.EndScrollView();
		}

		private void DisplaySubData(SaveData data)
        {
			ArrayList dataArray = data.GetKeys();
			string key;

			EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            for (int i = 0; i < dataArray.Count; i++)
            {
				key = dataArray[i] as string;
				object entry = InternalProcessing.GetRawDataEntry(data, key);
				DisplayEntry(key, entry);
            }

			EditorGUILayout.EndVertical();
        }

		private void DisplayEntry(string id, object entry)
        {
			if (entry is SaveData)
			{
				if (!_foldouts.ContainsKey(id)) _foldouts.Add(id, false);
				_foldouts[id] = EditorGUILayout.Foldout(_foldouts[id], id);

				if (!_foldouts[id]) return;

				EditorGUI.indentLevel++;
				DisplaySubData(entry as SaveData);
				EditorGUI.indentLevel--;
			}
			else if (entry is Array)
			{
				Array array = entry as Array;

				if (entry is SaveData[] || array.Length > 4) DisplayArray(id, array);
				else DisplayStructArray(id, array);
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel(id);
				EditorGUILayout.LabelField(entry.ToString());
				EditorGUILayout.EndHorizontal();
			}
		}

		private void DisplayArray(string id, Array array)
        {
			if (!_foldouts.ContainsKey(id)) _foldouts.Add(id, false);
			_foldouts[id] = EditorGUILayout.Foldout(_foldouts[id], id);

			if (!_foldouts[id]) return;

			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			EditorGUI.indentLevel++;
			for (int i = 0; i < array.Length; i++)
			{
				DisplayEntry(i.ToString(), array.GetValue(i));
			}
			EditorGUI.indentLevel--;
			EditorGUILayout.EndVertical();
		}

		private void DisplayStructArray(string id, Array array)
        {
			string valueString = "";

			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0) valueString += "; ";
				valueString += array.GetValue(i).ToString();
			}

			if (array.Length == 0)
            {
				valueString = "[EMPTY ARRAY]";
            }

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(id);
			EditorGUILayout.LabelField(valueString);
			EditorGUILayout.EndHorizontal();
		}
    }
}