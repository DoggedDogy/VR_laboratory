using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SBG.Memento
{
	public static class InternalProcessing
	{
		public static object GetRawDataEntry(SaveData saveData, string key)
        {
			object entry = saveData.GetValueFromTable(key, null);

            if (entry is SerializedData) return saveData.GetSubData(key);
            if (entry is SerializedDataArray) return saveData.GetSubDataArray(key);
            return entry;
        }

		public static SaveData LoadFromBinaryFile(string path, out int versionNr)
        {
            byte[] byteData = File.ReadAllBytes(path);

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(byteData);

            try
            {
                var fileContent = (SerializedSaveFile)bf.Deserialize(ms);

                versionNr = fileContent.VersionNr;

                //Check File Version Compatibility
                if (fileContent.VersionNr < VersionControl.MIN_FILE_VERSION)
                {
#if UNITY_EDITOR
                    Debug.Log("MEMENTO: Save File is outdated!");
#endif
                    return null;
                }

                return fileContent.RootData.Deserialize();
            }
            catch (Exception errorMsg)
            {
#if UNITY_EDITOR
                Debug.LogError("MEMENTO: Binary Load failed! Printing Error Message:");
                Debug.LogError(errorMsg);
#endif
                versionNr = -1;
                return null;
            }
        }
	}
}