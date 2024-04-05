using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SBG.Memento
{
	public class DemoController : MonoBehaviour
	{
        [SerializeField] Button loadGameButton;
        [SerializeField] Button loadSettingsButton;

        private void Start()
        {
            RefreshLoadGameButton();
            RefreshLoadSettingsButton();
        }

        public void NewGame()
        {
            SaveManager.ClearGameData();
            SaveManager.OnLoadFinished?.Invoke(SaveType.GameFile);
        }

		public void SaveGame()
        {
			SaveManager.SaveGame("SaveGame");
            RefreshLoadGameButton();
        }

		public void LoadGame()
        {
            SaveManager.LoadGame("SaveGame");
        }

		public void DeleteSaveFile()
        {
			SaveManager.DeleteGameFile("SaveGame");
            RefreshLoadGameButton();
            NewGame();
        }

        private void RefreshLoadGameButton()
        {
            loadGameButton.interactable = SaveManager.IsValidGameFile("SaveGame");
        }


        public void DefaultSettings()
        {
            SaveManager.ClearSettingsData();
            SaveManager.OnLoadFinished?.Invoke(SaveType.Settings);
        }

		public void SaveSettings()
        {
            SaveManager.SaveSettings("Settings");
            RefreshLoadSettingsButton();
        }

        public void LoadSettings()
        {
            SaveManager.LoadSettings("Settings");
        }

        public void DeleteSettings()
        {
            SaveManager.DeleteSettingsFile("Settings");
            RefreshLoadSettingsButton();
            DefaultSettings();
        }

        private void RefreshLoadSettingsButton()
        {
            loadSettingsButton.interactable = SaveManager.IsValidSettingsFile("Settings");
        }
    }
}