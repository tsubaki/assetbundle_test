using UnityEngine;
using System.Collections;
using System.IO;

public class DownloadBundle : MonoBehaviour
{
	[SerializeField]
	string url1 = string.Empty, url2 = string.Empty;
	string state = string.Empty;
		
	void OnGUI ()
	{
		if( !string.IsNullOrEmpty(state ))
		{
			GUILayout.Label(state);
			return;
		}
		
		if (GUILayout.Button ("load 1")) {
			StartCoroutine (Download (url1));
		}
		
		if (GUILayout.Button ("load 2")) {
			StartCoroutine (Download (url2));
		}
		
		if (GUILayout.Button ("loadTexture")) {
			StartCoroutine (Load ());
		}
	}
	
	IEnumerator Download (string url)
	{
		state = "download " + url;
		string path = Application.temporaryCachePath + "/img.unity3d";
		
		using (WWW www = new WWW (url)) {

			yield return www;

			File.Delete (path);
			File.WriteAllBytes (path, www.bytes);
		}
		state = string.Empty;
	}
	
	IEnumerator Load ()
	{
		state = "load";
		string url = "file://" + Application.temporaryCachePath + "/img.unity3d";
		
		using (WWW www = new WWW(url)) {
		
			yield return www;
		
			AssetBundle bundle = www.assetBundle;
			guiTexture.texture = (Texture)bundle.Load("img");
			bundle.Unload (false);
		}
		state = string.Empty;
	}
}
