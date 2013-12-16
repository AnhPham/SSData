using System.Collections.Generic;

public class TalkData : ISData
{
	public int 			Id		{ get; protected set; }
	public string 		Talk 	{ get; protected set; }

	public void LoadFromDict(Dictionary<string,object> dict) 
	{
		this.Id = (int)	(long)		dict ["id"];
		this.Talk = (string) 		dict ["talk"];
	}
}

public class Talk
{
	public static string FILE_NAME = "Data/Master/talk";

	public static SSListData<TalkData> Data { get; protected set; }

	static Talk()
	{
		if (Data == null) 
		{
			Data = new SSListData<TalkData> (FILE_NAME); 
		}
	}
}
