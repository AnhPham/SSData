using System.Collections.Generic;

public class SSData : ISData
{
	public int 			Field0	{ get; protected set; }
	public string 		Field1 	{ get; protected set; }
	public float 		Field2 	{ get; protected set; }
	public bool			Field3	{ get; protected set; }
	public List<object> Field4 	{ get; protected set; }

	public void LoadFromDict(Dictionary<string,object> dict) 
	{
		this.Field0 = (int)	(long)		dict ["field0"];
		this.Field1 = (string) 			dict ["field1"];
		this.Field2 = (float)(double)	dict ["field2"];
		this.Field3 = (bool)			dict ["field3"];
		this.Field4 = (List<object>)	dict ["field4"];
	}
}

public class SSTemplate
{
	public static string FILE_NAME = string.Empty;

	public static SSListData<SSData> Data { get; protected set; }

	static SSTemplate()
	{
		if (Data == null) 
		{
			Data = new SSListData<SSData> (FILE_NAME); 
		}
	}
	
	/*public static void SetData(string json)
	{
		#if !IS_DUMMY_DATA_ALL && !IS_DUMMY_DATA_SS
		Data.Set (json);
		#endif
	}*/
}
