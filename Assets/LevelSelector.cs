using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public static string selectedLevel;
	public string[] files;

	// Use this for initialization
	void Start () {
		string directory = Application.dataPath + "/Levels/";
		files = Directory.GetFiles (directory, "*.json");
		for (int i = 0; i < files.Length; i++) {
			GameObject button = (GameObject)GameObject.Instantiate (Resources.Load<GameObject>("LevelButton"), transform);
			Button butt = button.GetComponent<Button>();

			butt.GetComponentInChildren<Text>().text = files[i].Substring (files[i].LastIndexOf ("/") + 1);
			AddListener (butt, files[i]);
		}
	}

	void AddListener (Button butt, string filePath) {
		butt.onClick.AddListener (() => { SelectLevel (filePath); });
	}

	public void SelectLevel (string levelPath) {
		selectedLevel = levelPath;
		SceneManager.LoadScene (1);
	}
}
