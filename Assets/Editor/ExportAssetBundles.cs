using UnityEngine;
using UnityEditor;

public class ExportAssetBundles
{
	[MenuItem("Assets/AssetBundle/pack")]
	static void ExportAssetBundleBoth ()
	{

		// Bring up save panel
		string path = EditorUtility.SaveFilePanel ("Save Resource", "", Selection.activeObject.name, "");

		if (path.Length != 0) {
			Object[] selection = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);

			BuildPipeline.BuildAssetBundle (
					Selection.activeObject, 
					selection, path + "_iOS.unity3d", 
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.iPhone);

			BuildPipeline.BuildAssetBundle (
					Selection.activeObject, 
					selection, path + "_android.unity3d", 
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.Android);

			Selection.objects = selection;
		}
	}

	[MenuItem("Assets/AssetBundle/any")]
	static void ExportAssetBundleAny ()
	{
			
		foreach (Object obj in  Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets)) {
				

			BuildPipeline.BuildAssetBundle (
					Selection.activeObject, 
					new Object[]{obj}, "Assets/" + obj.name + "_iOS.unity3d", 
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.iPhone);

			BuildPipeline.BuildAssetBundle (
					Selection.activeObject, 
					new Object[]{obj}, "Assets/" + obj.name + "_android.unity3d", 
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.Android);

		}
	}	
}