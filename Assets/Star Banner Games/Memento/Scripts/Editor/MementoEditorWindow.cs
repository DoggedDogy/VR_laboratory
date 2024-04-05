using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace SBG.Memento.Editor
{
	public class MementoEditorWindow : EditorWindow
	{
        #region FIELDS

        private const float WINDOW_WIDTH = 400;
		private const float WINDOW_HEIGHT = 200;
		private const string HELPBOX_TEXT = "Every Save File has a Version Number. When trying to load a save, only files with the current version (or the minimum legacy version) will be valid. Otherwise the Save Cache will be empty.";
		private char[] SLASHES = { '/', '\\' };

		private int _cachedVersionNumber;
		private int _cachedMinVersionNumber;
		private bool _legacySupport = false;
		private GUIStyle _centeredLabel;

		#endregion

		#region WINDOW MANAGEMENT

		[MenuItem("Window/Star Banner Games/Memento/Save Manager")]
		public static void ShowWindow()
		{
			var window = GetWindow<MementoEditorWindow>("Memento Save Manager");

			//SET DEFAULT SIZE & POS
			Rect windowRect = new Rect();
			windowRect.size = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
			windowRect.x = (Screen.currentResolution.width / 2) - WINDOW_WIDTH;
			windowRect.y = (Screen.currentResolution.height / 2) - WINDOW_HEIGHT;
			window.position = windowRect;

			window._legacySupport = VersionControl.MIN_FILE_VERSION != VersionControl.CURRENT_FILE_VERSION;
			window._cachedVersionNumber = VersionControl.CURRENT_FILE_VERSION;
			window._cachedMinVersionNumber = VersionControl.MIN_FILE_VERSION;

			window._centeredLabel = EditorStyles.boldLabel;
			window._centeredLabel.alignment = TextAnchor.MiddleCenter;
		}

		private void OnGUI()
		{
			//HELP BOX
			EditorGUILayout.HelpBox(HELPBOX_TEXT, MessageType.Info);
			EditorGUILayout.Space();

			//VERSION NUMBER EDITING
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Current Version NR.", GUILayout.MaxWidth(150));
			if (_cachedVersionNumber == 1) GUI.enabled = false; 
			if (GUILayout.Button("-", GUILayout.MaxWidth(25))) ShiftCurrentVersion(-1);
			if (!GUI.enabled) GUI.enabled = true;
			EditorGUILayout.LabelField($"{_cachedVersionNumber}", _centeredLabel, GUILayout.MaxWidth(35));
			if (GUILayout.Button("+", GUILayout.MaxWidth(25))) ShiftCurrentVersion(1);
			EditorGUILayout.EndHorizontal();

			//LEGACY VERSION NUMBER EDITING
			_legacySupport = EditorGUILayout.Toggle("Backwards Compatibility", _legacySupport);
			if (_legacySupport)
            {
				if (_cachedMinVersionNumber > _cachedVersionNumber)
                {
					_cachedMinVersionNumber = _cachedVersionNumber;
                }

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Min. Version NR.", GUILayout.MaxWidth(150));
				if (_cachedMinVersionNumber <= 1) GUI.enabled = false;
				if (GUILayout.Button("-", GUILayout.MaxWidth(25))) ShiftMinVersion(-1);
				if (!GUI.enabled) GUI.enabled = true;
				EditorGUILayout.LabelField($"{_cachedMinVersionNumber}", _centeredLabel, GUILayout.MaxWidth(35));
				if (_cachedMinVersionNumber >= _cachedVersionNumber) GUI.enabled = false;
				if (GUILayout.Button("+", GUILayout.MaxWidth(25))) ShiftMinVersion(1);
				if (!GUI.enabled) GUI.enabled = true;
				EditorGUILayout.EndHorizontal();
			}
			else if (_cachedMinVersionNumber != _cachedVersionNumber)
            {
				_cachedMinVersionNumber = _cachedVersionNumber;
            }

			//APPLY CHANGES
			if (_cachedVersionNumber == VersionControl.CURRENT_FILE_VERSION &&
				_cachedMinVersionNumber == VersionControl.MIN_FILE_VERSION)
            {
				GUI.enabled = false;
            }
			if (GUILayout.Button("Apply")) UpdateVersionNumber();
			if (!GUI.enabled) GUI.enabled = true;

			//SOURCE FOLDER ACCESS
			if (GUILayout.Button("Open Source Folder")) System.Diagnostics.Process.Start(Application.persistentDataPath);
		}

        #endregion

        #region VERSION NUMBER MANAGEMENT

        private void ShiftCurrentVersion(int increment)
		{
			_cachedVersionNumber += increment;

			if (!_legacySupport)
            {
				_cachedMinVersionNumber = _cachedVersionNumber;
            }
		}

		private void ShiftMinVersion(int increment)
		{
			_cachedMinVersionNumber += increment;
		}

		private void UpdateVersionNumber()
        {
			//GET FULL CODE PATH
			string path = GetProjectFolder(false);
			path = Path.Combine(path, "Scripts", "VersionControl.cs");

			//HARDCODED VERSION UPDATE (Ugly but saves playing around with reading textfiles)
			string[] code =
			{
				"namespace SBG.Memento",
				"{",
				"\tpublic class VersionControl",
				"\t{",
				$"\t\tpublic const int CURRENT_FILE_VERSION = {_cachedVersionNumber};",
				$"\t\tpublic const int MIN_FILE_VERSION = {_cachedMinVersionNumber};",
				"\t}",
				"}"
			};

			File.WriteAllLines(path, code);

			//GET RELATIVE CODE PATH
			path = GetProjectFolder(true);
			path = Path.Combine(path, "Scripts", "VersionControl.cs");

			//RECOMPILE
			AssetDatabase.SaveAssets();
			AssetDatabase.ImportAsset(path);
		}

        #endregion

        #region PATH MANAGEMENT

        private string GetProjectFolder(bool relativePath)
		{
			string sbgPath = GetFolderPathRecursive(Application.dataPath, "Memento");

			if (!relativePath)
            {
				return sbgPath;
            }
            else
            {
				return sbgPath.Replace(Application.dataPath, "Assets"); ;
			}
		}

		private string GetFolderPathRecursive(string rootPath, string targetName)
		{
			if (!Directory.Exists(rootPath)) return null;

			string[] subPaths = Directory.GetDirectories(rootPath);

			if (subPaths == null) return null;

			for (int i = 0; i < subPaths.Length; i++)
			{
				if (GetFolderNameFromPath(subPaths[i]) == targetName)
				{
					return subPaths[i];
				}

				string folderPath = GetFolderPathRecursive(subPaths[i], targetName);

				if (GetFolderNameFromPath(folderPath) == targetName)
				{
					return folderPath;
				}
			}

			return null;
		}

		public string GetFolderNameFromPath(string path)
		{
			if (string.IsNullOrEmpty(path)) return null;

			int nameIndex = path.LastIndexOfAny(SLASHES) + 1;

			return path.Substring(nameIndex);
		}

        #endregion
    }
}