using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class SSDataEditor : EditorWindow 
{
	static string className = "Quest";
	static int selGridInt = 1;

	static string saveClassFilePath = "Assets/SSData/Demo/Scripts";
	static string masterFileNamePath = "Data/Master/talk";
	static string dummyFileNamePath = "Data/Dummy/quest";
	static string templatePath = string.Empty;
	
	[MenuItem ("SS/Data Editor")]
	static void Init () 
	{
		EditorWindow.GetWindow (typeof (SSDataEditor));

		selGridInt = EditorPrefs.GetInt ("selGridInt", 1);
		className = EditorPrefs.GetString ("className", "Quest");
		saveClassFilePath = EditorPrefs.GetString ("saveClassFilePath", "Assets/SSData/Demo/Scripts");
		masterFileNamePath = EditorPrefs.GetString ("masterFileNamePath", "Data/Master/talk");
		dummyFileNamePath = EditorPrefs.GetString ("dummyFileNamePath", "Data/Dummy/quest");
	}

	void OnGUI () 
	{
		GUILayout.Label ("Data Generator", EditorStyles.boldLabel);
		GUILayout.Space (10);

		GUILayout.BeginHorizontal ();
		className = EditorGUILayout.TextField ("Class name", className);
		GUILayout.Label ("Data");
		GUILayout.EndHorizontal ();

		saveClassFilePath = EditorGUILayout.TextField ("Class file path", saveClassFilePath);

		selGridInt = GUILayout.SelectionGrid (selGridInt, new string[]{ "Local Data", "Server Data" }, 2, "toggle");

		if (selGridInt == 0)
			masterFileNamePath = EditorGUILayout.TextField ("Local data file path", masterFileNamePath);
		else if (selGridInt == 1)
			dummyFileNamePath = EditorGUILayout.TextField ("Dummy data file path", dummyFileNamePath);

		GUILayout.Label ("(In Resources folder)");

		if (GUILayout.Button ("Create")) 
		{
			Create ();
		}
	}

	private void Create()
	{
		DirectoryInfo dir = new DirectoryInfo (saveClassFilePath);
		if (!dir.Exists) 
		{
			Debug.Log ("Directory not exist");
			return;
		}

		FileInfo file = new FileInfo (NewPath ());
		if (file != null && file.Exists) 
		{
			Debug.Log ("File existed");
			return;
		}

		templatePath = GetPathTemplateFile ();
		if (string.IsNullOrEmpty (templatePath)) 
		{
			Debug.Log ("Not found template file");
			return;
		}

		CopyRename();
		ReplaceScripts();

		EditorUtility.DisplayDialog("Data Editor", "Create Finished", "OK");

		EditorPrefs.SetInt ("selGridInt", selGridInt);
		EditorPrefs.SetString ("className", className);
		EditorPrefs.SetString ("saveClassFilePath", saveClassFilePath);
		EditorPrefs.SetString ("masterFileNamePath", masterFileNamePath);
		EditorPrefs.SetString ("dummyFileNamePath", dummyFileNamePath);
	}

	private void CopyRename()
	{
		AssetDatabase.CopyAsset(templatePath, NewPath());
		AssetDatabase.Refresh();
	}
	
	private void ReplaceScripts()
	{
		FileInfo file = new FileInfo (NewPath ());

		ReplaceFile(file.FullName, "SSData", className + "Data");
		ReplaceFile(file.FullName, "SSTemplate", className);
		ReplaceFile(file.FullName, "IS_DUMMY_DATA_SS", "IS_DUMMY_DATA_" + className.ToUpper());

		if (selGridInt == 0) {
			int i = masterFileNamePath.IndexOf (".");
			if (i >= 0) masterFileNamePath = masterFileNamePath.Remove (i);
			ReplaceFile(file.FullName, "string.Empty", "\"" + masterFileNamePath + "\"");
		}
		else if (selGridInt == 1) {
			int i = dummyFileNamePath.IndexOf (".");
			if (i >= 0) dummyFileNamePath = dummyFileNamePath.Remove (i);
			ReplaceFile(file.FullName, "string.Empty", "\"" + dummyFileNamePath + "\"");
			ReplaceFile(file.FullName, "/*", string.Empty);
			ReplaceFile(file.FullName, "*/", string.Empty);

		}

		AssetDatabase.Refresh();
	}

	private void ReplaceFile(string path, string oldString, string newString)
	{
		string fileContents = System.IO.File.ReadAllText(path);
		fileContents = fileContents.Replace(oldString, newString);
		System.IO.File.WriteAllText(path, fileContents);
	}

	private List<FileInfo> DirSearch(DirectoryInfo d, string searchFor)
	{
		List<FileInfo> founditems = d.GetFiles(searchFor).ToList();
		// Add (by recursing) subdirectory items.
		DirectoryInfo[] dis = d.GetDirectories();
		foreach (DirectoryInfo di in dis)
			founditems.AddRange(DirSearch(di, searchFor));

		return (founditems);
	}

	private FileInfo SearchTemplateFile()
	{
		string path = Application.dataPath;
		DirectoryInfo dir = new DirectoryInfo (path);
		List<FileInfo> lst = DirSearch (dir, "SSData.cs");

		if (lst.Count >= 1)
			return lst [0];

		return null;
	}

	private string GetPathTemplateFile()
	{
		FileInfo f = SearchTemplateFile();

		if (f == null)
			return null;

		string path = f.FullName;
		int index = path.IndexOf ("Assets");
		path = path.Substring (index);

		return path;
	}

	private string NewPath()
	{
		return saveClassFilePath + "/" + className + "Data.cs";
	}
}