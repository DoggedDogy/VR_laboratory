using UnityEngine;
using UnityEngine.UI;

namespace SBG.Memento.Demo
{
	public class SettingsController : MonoBehaviour
	{
        [SerializeField] private Toggle toggle;
        [SerializeField] private Slider slider1;
        [SerializeField] private Slider slider2;
        [SerializeField] private InputField inputField;


        private void OnEnable()
        {
            SaveManager.OnBeforeSave += SaveState;
            SaveManager.OnLoadFinished += LoadState;
        }

        private void OnDisable()
        {
            SaveManager.OnBeforeSave -= SaveState;
            SaveManager.OnLoadFinished -= LoadState;
        }

        private void LoadState(SaveType saveType)
        {
            if (saveType != SaveType.Settings) return;

            toggle.isOn = SaveManager.GetBool("toggle", true, SaveType.Settings);
            slider1.value = SaveManager.GetFloat("slider1", 0, SaveType.Settings);
            slider2.value = SaveManager.GetFloat("slider2", 0, SaveType.Settings);
            inputField.text = SaveManager.GetString("inputField", "", SaveType.Settings);
        }

        private void SaveState(SaveType saveType)
        {
            if (saveType != SaveType.Settings) return;

            SaveManager.SetValue("toggle", toggle.isOn, SaveType.Settings);
            SaveManager.SetValue("slider1", slider1.value, SaveType.Settings);
            SaveManager.SetValue("slider2", slider2.value, SaveType.Settings);
            SaveManager.SetValue("inputField", inputField.text, SaveType.Settings);
        }
    }
}