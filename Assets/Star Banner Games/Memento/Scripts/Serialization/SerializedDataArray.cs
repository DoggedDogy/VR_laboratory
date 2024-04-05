
namespace SBG.Memento
{
	[System.Serializable]
	internal struct SerializedDataArray
	{
		public SerializedData[] DataArray;

		public SerializedDataArray(SaveData[] dataArray)
		{
			DataArray = new SerializedData[dataArray.Length];

            for (int i = 0; i < DataArray.Length; i++)
            {
				DataArray[i] = new SerializedData(dataArray[i]);
            }
		}

		public SaveData[] Deserialize()
        {
			SaveData[] array = new SaveData[DataArray.Length];

            for (int i = 0; i < array.Length; i++)
            {
				array[i] = DataArray[i].Deserialize();
            }

			return array;
        }
	}
}