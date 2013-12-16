using System.Collections.Generic;

public class QuestData : ISData
{
	public int 			Id		{ get; protected set; }
	public string 		Quest 	{ get; protected set; }

	public void LoadFromDict(Dictionary<string,object> dict) 
	{
		this.Id = (int)	(long)		dict ["id"];
		this.Quest = (string) 		dict ["quest"];
	}
}

public class Quest
{
	public static string FILE_NAME = "Data/Dummy/quest";

	public static SSListData<QuestData> Data { get; protected set; }

	static Quest()
	{
		if (Data == null) 
		{
			Data = new SSListData<QuestData> (FILE_NAME); 
		}
	}
	
	public static void SetData(string json)
	{
		#if !IS_DUMMY_DATA_ALL && !IS_DUMMY_DATA_QUEST
		Data.Set (json);
		#endif
	}
}
