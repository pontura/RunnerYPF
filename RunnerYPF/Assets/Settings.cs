using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Settings : MonoBehaviour
{

	public String[] levelsPath;
	public List<LevelData> levels;

	public LevelSettings[] levelSettings;
	[Serializable]
	public class LevelSettings
	{
		public int id;
		public Color[] topGeneric;
		public Color[] bottomGeneric;
		public Color tile;
	}
	public LevelSettings GetLevelSettings(int levelID)
	{
		return levelSettings [levelID - 1];
	}
	void Awake()
	{
		foreach(String s in levelsPath)
		LoadDataromServer (LoadResourceTextfile (s));
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
		LevelData data = new LevelData();
		data.level = int.Parse(content["level"]);

		if(content ["final"] != null)
			data.final =  true;	

		data.tiles = AddData (content ["tiles"]);

		if(content ["other"] != null)
			data.other = AddData (content ["other"]);	
		
		data.energyAssets = AddData (content ["energ"]);	
		levels.Add(data);
		ShuffleListTexts (levels);
	}
	void ShuffleListTexts(List<LevelData> levelData)
	{
		if (levelData.Count < 2) return;
		for (int a = 0; a < 200; a++)
		{
			int id = UnityEngine.Random.Range(1, levelData.Count);
			LevelData value1 = levelData[0];
			LevelData value2 = levelData[id];
			if (value1.level == value2.level) {
				levelData [0] = value2;
				levelData [id] = value1;
			}
		}
	}
	int[] AddData(JSONNode data)
	{
		int[] arr = new int[data.Count];
		for (int a = 0; a < data.Count; a++) {
			if(data[a] == "_")
				arr [a] = -1;
			else
				arr [a] = int.Parse (data [a]);
		}
		return arr;
	}

}
