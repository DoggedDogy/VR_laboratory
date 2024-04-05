using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBG.Memento.Demo
{
	public class ButtonController : MonoBehaviour
	{
        private ColorButton[] _colorButtons;


        private void Start()
        {
            _colorButtons = FindObjectsOfType<ColorButton>();
        }

        private void OnEnable()
        {
            SaveManager.OnBeforeSave += OnSave;
            SaveManager.OnLoadFinished += OnLoad;
        }

        private void OnDisable()
        {
            SaveManager.OnBeforeSave -= OnSave;
            SaveManager.OnLoadFinished -= OnLoad;
        }


        public void OnSave(SaveType fileType)
        {
            if (fileType != SaveType.GameFile) return;

            if (_colorButtons == null) return;

            SaveData[] buttonDatas = new SaveData[_colorButtons.Length];

            for (int i = 0; i < _colorButtons.Length; i++)
            {
                buttonDatas[i] = _colorButtons[i].GetData();
            }

            SaveManager.SetValue("Buttons", buttonDatas);
        }

        public void OnLoad(SaveType fileType)
        {
            if (fileType != SaveType.GameFile) return;

            SaveData[] buttonData = SaveManager.GetSubDataArray("Buttons");

            if (buttonData == null) buttonData = new SaveData[_colorButtons.Length];

            for (int i = 0; i < _colorButtons.Length; i++)
            {
                _colorButtons[i].SetData(buttonData[i]);
            }
        }
    }
}