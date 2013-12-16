using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public interface ISData
{
	void LoadFromDict (Dictionary<string,object> dict);
}

public class SSListData<T> : List<T> where T:ISData, new()
{
	public SSListData(string fileName) : base()
	{
		if (!string.IsNullOrEmpty(fileName))
		{
			SetList(GetListFromJsonFile(fileName));
		}
	}

	public void Set(string data)
	{
		Clear ();
		SetList(GetListFromJsonString(data));
	}

	protected void SetList(List<object> list)
	{
		if (list == null) return;

		foreach (Dictionary<string, object> dict in list)
		{
			T item = new T();
			item.LoadFromDict(dict);
			Add(item);
		}
	}

	protected List<object> GetListFromJsonFile(string fileName)
	{
		if (string.IsNullOrEmpty(fileName)) return null;

		string jsonFilePath = fileName;
		TextAsset textAsset = Resources.Load(jsonFilePath) as TextAsset;

		if (textAsset == null)
		{
			#if IS_DUMMY_DATA_ALL
			Debug.LogWarning("Loading JSON file failed:" + jsonFilePath);
			#endif
			return null;
		}

		return GetListFromJsonString(textAsset.text);
	}

	protected List<object> GetListFromJsonString(string json)
	{
		List<object> list = json.listFromJson();
		return list;
	}
}
