
namespace SBG.Memento
{
	[System.Serializable]
	internal struct SerializedDataEntry
	{
		public string Key;
		public object Value;
	
		public SerializedDataEntry(string key, object value)
		{
			Key = key;
			Value = value;
		}
	}
}