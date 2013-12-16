using UnityEngine;
using System.Collections;

public class DataTestController : MonoBehaviour 
{
	[SerializeField]
	TextMesh text;

	void OnGUI()
	{
		GUILayout.BeginVertical ();

		if (GUILayout.Button("Local")) LocalData ();

		if (GUILayout.Button("Server")) ServerData ();

		GUILayout.EndVertical ();
	}

	void LocalData()
	{
		string s = string.Empty;

		foreach (TalkData data in Talk.Data)
		{
			s += ("id: " + data.Id + ", talk: " + data.Talk) + "\n";
		}

		text.text = s;
	}

	// If you want to use dummy data, add IS_DUMMY_DATA_ALL to Scripting Define Symbol
	void ServerData()
	{
		// Simulate server data
		string json = GetJsonDataFromServer ();

		// Set data
		Quest.SetData (json);

		// Print
		string s = string.Empty;

		foreach (QuestData data in Quest.Data)
		{
			s += ("id: " + data.Id + ", quest: " + data.Quest) + "\n";
		}

		text.text = s;
	}

	string GetJsonDataFromServer()
	{
		return "[{\"id\":1,\"quest\":\"Find Ifrit\"},{\"id\":2,\"quest\":\"Kill Omega\"}]";
	}
}
