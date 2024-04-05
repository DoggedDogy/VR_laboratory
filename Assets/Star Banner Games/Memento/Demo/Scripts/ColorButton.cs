using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SBG.Memento.Demo
{
	public class ColorButton : MonoBehaviour, ISaveData
	{
        #region BUTTON PRESSING

        public int PressCount { get; private set; }

        [SerializeField] Text _pressCountText;

		private SpriteRenderer _spriteRenderer;

        void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

        private void OnTriggerEnter2D()
        {
            PressCount++;
            _pressCountText.text = PressCount.ToString();
            _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        }

        #endregion


        public SaveData GetData()
        {
            SaveData buttonData = new SaveData();

            buttonData.SetValue("PressCount", PressCount);
            buttonData.SetValue("Color", _spriteRenderer.color);

            return buttonData;
        }

        public void SetData(SaveData data)
        {
            if (data == null) data = new SaveData();

            PressCount = data.GetInt("PressCount", 0);
            _spriteRenderer.color = data.GetColor("Color", Color.green);

            _pressCountText.text = PressCount.ToString();
        }
    }
}