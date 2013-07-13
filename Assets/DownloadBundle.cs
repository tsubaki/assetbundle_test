using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using terasurware;
using UnityEngine;

public class DownloadBundle : MonoBehaviour
{
	[SerializeField]
	string url1 = string.Empty, url2 = string.Empty;
	private string state = string.Empty;
	
	void OnGUI ()
	{
		if (!string.IsNullOrEmpty (state)) {
			GUILayout.Label (state);
			return;
		}
		if (GUILayout.Button ("load 1", GUILayout.Height (60))) {
			Download (url1);
		}
		
		if (GUILayout.Button ("load 2", GUILayout.Height (60))) {
			Download (url2);
		}
		for (int i=1; i<4; i++) {
			if (GUILayout.Button ("loadTexture : " + "img" + i, GUILayout.Height (40))) {
				StartCoroutine (Load ("img" + i));
			}
		}
	}
	
	void Download (string url)
	{
		string path = Application.temporaryCachePath + "/img.unity3d.zip";
		string expotDir = Application.temporaryCachePath;
		state = "download " + url;
		
		WebClient client = new WebClient ();
		client.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs e) {
			state = "unzip";
			Ziper.UnZipFile (path, expotDir);
			File.Delete (path);
			state = string.Empty;
		};
		
		client.DownloadFileAsync (new Uri (url), path);
	}
	
	IEnumerator Load (string fileName)
	{
		string url = string.Format ("file://{0}/{1}.unity3d", Application.temporaryCachePath, fileName);
		state = "load";
		
		using (WWW www = new WWW(url)) {
		
			yield return www;
		
			guiTexture.texture = www.assetBundle.mainAsset as Texture;
			www.assetBundle.Unload (false);
		}
		state = string.Empty;
	}
}
