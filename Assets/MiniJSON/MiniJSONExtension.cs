using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public static class MiniJsonExtension
{
	public static string toJson( this Hashtable obj )
	{
		return Json.Serialize( obj );
	}
	
	
	public static string toJson( this Dictionary<string,object> obj )
	{
		return Json.Serialize( obj );
	}
	
	public static List<object> listFromJson( this string json )
	{
		return Json.Deserialize( json ) as List<object>;
	}

	public static ArrayList arrayListFromJson( this string json )
	{
		List<object> list = json.listFromJson();
		return new ArrayList(list);
	}


	public static Dictionary<string,object> dictionaryFromJson( this string json )
	{
		return Json.Deserialize( json ) as Dictionary<string,object>;
	}

	public static Hashtable hashtableFromJson( this string json )
	{
		Dictionary<string,object> dic = json.dictionaryFromJson();
		return new Hashtable(dic);
	}
}
