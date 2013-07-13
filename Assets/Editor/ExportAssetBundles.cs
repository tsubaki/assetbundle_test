using UnityEngine;
using UnityEditor;

public class ExportAssetBundles
{
	[MenuItem("Assets/AssetBundle/pack")]
	static void ExportAssetBundleBoth ()
	{
		string path = EditorUtility.SaveFilePanel ("Save Resource", "", Selection.activeObject.name, "");

		if (path.Length != 0) {
			Object[] selection = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);

			BuildPipeline.BuildAssetBundle (
					Selection.activeObject, 
					selection, path + ".unity3d", 
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.Android);

			Selection.objects = selection;
		}
	}
}