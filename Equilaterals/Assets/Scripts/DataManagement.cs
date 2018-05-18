using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class DataManagement : MonoBehaviour {

	public static DataManagement datamanager;

	public int highScore;
	public int totalCollected;


	void Awake () {

		Environment.SetEnvironmentVariable ("MONO_REFELCTION_SERIALIZER", "yes");
		if (datamanager == null) {
			DontDestroyOnLoad (gameObject);
			datamanager = this;
		} else if (datamanager != null) {
			Destroy (gameObject);

		}
	}

	public void SaveData(){
		BinaryFormatter binForm = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
		gameData data = new gameData();
		data.highScore = highScore;
		data.totalCollected = totalCollected;

		binForm.Serialize(file, data);
		file.Close();
	}

	public void LoadData(){
		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) {
			BinaryFormatter binForm = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
			gameData data = (gameData)binForm.Deserialize(file);
			file.Close();
			highScore = data.highScore;
			totalCollected = data.totalCollected;

		}
	}
}

[Serializable]
class gameData{

	public int highScore;
	public int totalCollected;

}










