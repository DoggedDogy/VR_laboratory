
namespace SBG.Memento
{
	[System.Serializable]
	internal struct SerializedData
	{
		public SerializedDataEntry[] Data;

		public SerializedData(SaveData data)
		{
			var keys = data.GetKeys();

			Data = new SerializedDataEntry[keys.Count];

            for (int i = 0; i < Data.Length; i++)
            {
				string key = (string)keys[i];
				Data[i] = new SerializedDataEntry(key, data.GetValueFromTable(key, null));
            }
		}

		public SaveData Deserialize()
        {
			SaveData saveData = new SaveData();

            foreach (var entry in Data)
            {
				saveData.AddValueToTable(entry.Key, entry.Value);
            }

			return saveData;
        }
	}
}