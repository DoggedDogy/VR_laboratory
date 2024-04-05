
namespace SBG.Memento
{
	[System.Serializable]
	internal struct SerializedSaveFile
	{
		public int VersionNr;
		public SerializedData RootData;

		public SerializedSaveFile(int version, SaveData root)
		{
			VersionNr = version;
			RootData = new SerializedData(root);
		}
    }
}