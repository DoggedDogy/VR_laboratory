
namespace SBG.Memento
{
	/// <summary>
	/// Implement this interface on classes that contain sub-data you want to save/load
	/// </summary>
	public interface ISaveData
	{
		public SaveData GetData();
		public void SetData(SaveData data);
	}
}