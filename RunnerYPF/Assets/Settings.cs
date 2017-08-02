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

	public List<LevelData> level1;
	public List<LevelData> level2;
	public List<LevelData> level3;

	public LevelSettings[] levelSettings;
	[Serializable]
	public class LevelSettings
	{
		public int id;
		public int colorXModule;
		public int colorXOffset;
		public int colorZModule;
		public int colorZOffset;
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
		int levelID = 1;
		foreach (String s in levelsPath) {
			LoadDataromServer (LoadResourceTextfile (s), levelID);
			levelID++;
		}
	}
	public static string LoadResourceTextfile(string path)
	{
		string filePath = "Settings/" + path;
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		return targetFile.text;
	}
	public void LoadDataromServer(string json_data, int levelID)
	{
		JSONNode content = SimpleJSON.JSON.Parse(json_data);
		for (int a = 0; a < content.Count; a++)
		{
			AddLevel(content[a], levelID);
		}
	}
	void AddLevel(JSONNode content, int levelID)
	{
		LevelData data = new LevelData();
		data.level = int.Parse(content["level"]);

		if(content ["final"] != null)
			data.final =  true;	

		data.tiles = AddData (content ["tiles"]);

		if(content ["other"] != null)
			data.other = AddData (content ["other"]);	
		
		if(content ["powUp"] != null)
			data.powerUp = AddData (content ["powUp"]);	
		
		data.energyAssets = AddData (content ["energ"]);

		switch (levelID) {
			case 1:
				level1.Add (data);
				ShuffleListTexts (level1);
				break;
			case 2:
				level2.Add (data);
				//ShuffleListTexts (level2);
				break;
			case 3:
				level3.Add (data);
				ShuffleListTexts (level3);
				break;
		}


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
