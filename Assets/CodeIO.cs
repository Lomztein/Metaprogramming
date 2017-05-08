using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

	class CodeIO {

	public static T LoadObjectFromFile<T> ( string path ) {
		try {
			if (File.Exists (path)) {

				StreamReader reader = File.OpenText (path);
				JsonTextReader jReader = new JsonTextReader (reader);

				JsonSerializer serializer = new JsonSerializer ();
				object data = serializer.Deserialize<T> (jReader);

				reader.Dispose ();
				jReader.Close ();

				return (T)data;
			}
			return default (T);
		} catch (Exception e) {
			return default (T);
		}
	}

	public static void SaveObjectToFile (string fileName, object obj) {
		try {
			StreamWriter writer = File.CreateText (fileName);

			JsonSerializer serializer = new JsonSerializer ();
			serializer.Serialize (writer, obj);

			writer.Dispose ();
		} catch (Exception e) {
		}
	}

	public static string[] LoadTextFile (string path) {
		StreamReader reader = File.OpenText (path);

		List<string> con = new List<string> ();
		int maxTries = short.MaxValue;

		while (true && maxTries > 0) {
			maxTries--;
			string loc = reader.ReadLine ();
			if (loc == null) {
				break;
			} else {
				con.Add (loc);
			}
		}

		return con.ToArray ();
	}

	public static GameObject LoadLevel (string filePath) {
		GameObject levelObject = GameObject.Instantiate (Resources.Load<GameObject>("LevelPrefab"));
		levelObject.GetComponent<Level>().data = LoadObjectFromFile<Level.LevelData> (filePath);

		return levelObject;
	}
}
