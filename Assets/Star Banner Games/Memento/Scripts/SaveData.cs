using System.Collections;
using UnityEngine;

namespace SBG.Memento
{
    /// <summary>
    /// Memento saves Files using a SaveData objects.
    /// If you need to create sub packages of data, you can create your own SaveData objects to feed back into the SaveManager.
    /// This can be useful when you have a lot of repeating data, e.g: Npc's that all have the same variables to save.
    /// </summary>
	public class SaveData
	{
		private Hashtable _valueTable = new Hashtable();

        /// <returns>A list of the Keys of all saved values</returns>
		public ArrayList GetKeys() => new ArrayList(_valueTable.Keys);

        #region SET VALUES

        //BASE TYPES
        public void SetValue(string key, int value) => AddValueToTable(key, value);
        public void SetValue(string key, long value) => AddValueToTable(key, value);
        public void SetValue(string key, float value) => AddValueToTable(key, value);
        public void SetValue(string key, double value) => AddValueToTable(key, value);
        public void SetValue(string key, string value) => AddValueToTable(key, value);
        public void SetValue(string key, bool value) => AddValueToTable(key, value);

        //ARRAYS
        public void SetValue(string key, int[] value) => AddValueToTable(key, value);
        public void SetValue(string key, long[] value) => AddValueToTable(key, value);
        public void SetValue(string key, float[] value) => AddValueToTable(key, value);
        public void SetValue(string key, double[] value) => AddValueToTable(key, value);
        public void SetValue(string key, string[] value) => AddValueToTable(key, value);
        public void SetValue(string key, bool[] value) => AddValueToTable(key, value);

        //NESTED DATA
        public void SetValue(string key, SaveData data)
        {
            var serializedData = new SerializedData(data);
            AddValueToTable(key, serializedData);
        }

        public void SetValue(string key, SaveData[] dataArray)
        {
            var serializedArray = new SerializedDataArray(dataArray);
            AddValueToTable(key, serializedArray);
        }
        
        //COMMON STRUCTS
        public void SetValue(string key, Vector2 value)
        {
            float[] serializedValue = new float[] { value.x, value.y };
            AddValueToTable(key, serializedValue);
        }

        public void SetValue(string key, Vector3 value)
        {
            float[] serializedValue = new float[] { value.x, value.y, value.z };
            AddValueToTable(key, serializedValue);
        }

        public void SetValue(string key, Quaternion value)
        {
            float[] serializedValue = new float[] { value.x, value.y, value.z, value.w };
            AddValueToTable(key, serializedValue);
        }

        public void SetValue(string key, Color value)
        {
            float[] serializedValue = new float[] { value.r, value.g, value.b, value.a };
            AddValueToTable(key, serializedValue);
        }

        //BASE FUNCTION
        internal void AddValueToTable(string key, object value)
        {
            if (_valueTable.ContainsKey(key))
            {
                _valueTable[key] = value;
            }
            else
            {
                _valueTable.Add(key, value);
            }
        }

        #endregion

        #region GET VALUES

        //BASE TYPES
        public int GetInt(string key, int defaultValue = 0) => (int)GetValueFromTable(key, defaultValue);
        public long GetLong(string key, long defaultValue = 0) => (long)GetValueFromTable(key, defaultValue);
        public float GetFloat(string key, float defaultValue = 0) => (float)GetValueFromTable(key, defaultValue);
        public double GetDouble(string key, double defaultValue = 0) => (double)GetValueFromTable(key, defaultValue);
        public string GetString(string key, string defaultValue = "") => (string)GetValueFromTable(key, defaultValue);
        public bool GetBool(string key, bool defaultValue = false) => (bool)GetValueFromTable(key, defaultValue);

        //ARRAYS
        public int[] GetIntArray(string key) => (int[])GetValueFromTable(key, null);
        public long[] GetLongArray(string key) => (long[])GetValueFromTable(key, null);
        public float[] GetFloatArray(string key) => (float[])GetValueFromTable(key, null);
        public double[] GetDoubleArray(string key) => (double[])GetValueFromTable(key, null);
        public string[] GetStringArray(string key) => (string[])GetValueFromTable(key, null);
        public bool[] GetBoolArray(string key) => (bool[])GetValueFromTable(key, null);

        //NESTED DATA
        public SaveData GetSubData(string key)
        {
            object data = GetValueFromTable(key, null);

            if (data == null) return null;

            return ((SerializedData)data).Deserialize();
        }

        public SaveData[] GetSubDataArray(string key)
        {
            object dataArray = GetValueFromTable(key, null);

            if (dataArray == null) return null;

            return ((SerializedDataArray)dataArray).Deserialize();
        }

        //COMMON STRUCTS
        public Vector2 GetVector2(string key) => GetVector2(key, Vector2.zero);
        public Vector2 GetVector2(string key, Vector2 defaultValue)
        {
            object values = GetValueFromTable(key, null);

            if (values == null) return defaultValue;

            float[] fValues = values as float[];

            return new Vector2(fValues[0], fValues[1]);
        }

        public Vector3 GetVector3(string key) => GetVector3(key, Vector3.zero);
        public Vector3 GetVector3(string key, Vector3 defaultValue)
        {
            object values = GetValueFromTable(key, null);

            if (values == null) return defaultValue;

            float[] fValues = values as float[];

            return new Vector3(fValues[0], fValues[1], fValues[2]);
        }

        public Quaternion GetQuaternion(string key) => GetQuaternion(key, Quaternion.identity);
        public Quaternion GetQuaternion(string key, Quaternion defaultValue)
        {
            object values = GetValueFromTable(key, null);

            if (values == null) return defaultValue;

            float[] fValues = values as float[];

            return new Quaternion(fValues[0], fValues[1], fValues[2], fValues[3]);
        }

        public Color GetColor(string key) => GetColor(key, Color.clear);
        public Color GetColor(string key, Color defaultValue)
        {
            object values = GetValueFromTable(key, null);

            if (values == null) return defaultValue;

            float[] fValues = values as float[];

            return new Color(fValues[0], fValues[1], fValues[2], fValues[3]);
        }

        //BASE FUNCTION
        internal object GetValueFromTable(string key, object defaultValue)
        {
            if (_valueTable.ContainsKey(key))
            {
                return _valueTable[key];
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion
    }
}