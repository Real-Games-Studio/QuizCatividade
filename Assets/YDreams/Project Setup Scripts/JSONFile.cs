using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class JSONFile
{

    public static configclass Configclass = new configclass();

    static JSONFile()
    {

#if UNITY_EDITOR
        var path = Application.streamingAssetsPath;
#endif

#if !UNITY_EDITOR
        string[] splits = Application.dataPath.Split(Application.productName + "_Data");
        string path = splits[0];
#endif

        var textJson = Path.Combine(path, "appconfig.json");
        // Debug.Log(textJson);
        Configclass = JsonUtility.FromJson<configclass>(File.ReadAllText(textJson));
    }
}

[System.Serializable]
public class configclass
{
    public float timeout;
}

