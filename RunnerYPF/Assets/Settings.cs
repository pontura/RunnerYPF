using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Settings : MonoBehaviour
{
	public List<LevelData> levels;

	void Awake()
	{
		LoadDataromServer (LoadResourceTextfile ("levels"));
	}
	public static string LoadResourceTextfile(string path)
	{
		string filePath = "Settings/" + path;
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		return targetFile.text;
	}
	public void LoadDataromServer(string json_data)
	{
		JSONNode content = SimpleJSON.JSON.Parse(json_data);
		for (int a = 0; a < content.Count; a++)
		{
			AddLevel(content[a]);
		}
	}
	void AddLevel(JSONNode content)
	{
		print (content);
		LevelData data = new LevelData();
		data.level = int.Parse(content["level"]);
		data.tiles = AddData (content ["tiles"]);	
		data.energyAssets = AddData (content ["energ"]);	
		levels.Add(data);
	}
	int[] AddData(JSONNode data)
	{
		int[] arr = new int[data.Count];
		for (int a = 0; a < data.Count; a++)
			arr[a] = int.Parse(data[a]);
		return arr;
	}

}
