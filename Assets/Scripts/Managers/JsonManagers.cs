using Newtonsoft.Json;
using UnityEngine;

public class JsonManagers : MonoBehaviour
{
    private static JsonManagers instance;
    public static JsonManagers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonManagers();
            }
            return instance;
        }
    }
    public T GetJson<T>(string path)
    {
        string json = System.IO.File.ReadAllText(path);
        T t = JsonConvert.DeserializeObject<T>(json);

        return t;
    }
}