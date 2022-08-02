using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
public class SceneEditor : EditorWindow
{
    [MenuItem("Tools/³¡¾°±à¼­Æ÷")]
    static void Init()
    {
        SceneEditor window = GetWindow<SceneEditor>();
        if (window!=null)
        {
            window.Show();
        }
    }
    public List<string> typesName = new List<string> { "A","B","C","D","E"};
    public Dictionary<string, ModelData> prefabdic = new Dictionary<string, ModelData>();
    void AddPrefab(Transform parent,int index)
    {
        ModelData data = new ModelData();
        Transform go = parent.GetChild(index);
        data.name= go.name;
        data.x=go.position.x;
        data.y=go.position.y;
        data.z=go.position.z;
        data.type = go.tag;
        data.isShow = go.gameObject.activeSelf;
        for (int i = 0; i < typesName.Count; i++)
        {
            if (go.tag==typesName[i])
            {
                data.typeindex = i;
                break;
            }
        }
        prefabdic[go.name] = data;
    }
    private void OnGUI()
    {
        GameObject root = GameObject.Find("Root");
        GameObject jsonName1 = GameObject.Find("Scene1");
        if (root!=null&&root.transform.childCount!=prefabdic.Count)
        {
            prefabdic.Clear();
            for (int i = 0; i < root.transform.childCount; i++)
            {
                AddPrefab(root.transform,i);
            }
        }
        foreach (var item in prefabdic.Values)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.name);
            bool flag = EditorGUILayout.Toggle(item.isShow);
            if (flag!=item.isShow)
            {
                item.isShow = flag;
                root.transform.Find(item.name).gameObject.SetActive(item.isShow);
            }
            Vector3 oldpos = new Vector3(item.x,item.y,item.z);
            Vector3 newpos = EditorGUILayout.Vector3Field(item.name+".pos",oldpos);
            if (oldpos!=newpos)
            {
                item.x = newpos.x;
                item.y = newpos.y;
                item.z = newpos.z;
                root.transform.Find(item.name).position = newpos;
            }
            GUILayout.EndHorizontal();
        }
    }

}
public class ModelData
{
    public string name;
    public float x;
    public float y;
    public float z;
    public int typeindex;
    public string type;
    public bool isShow;
}


