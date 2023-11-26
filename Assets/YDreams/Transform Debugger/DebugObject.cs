using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DebugObject : MonoBehaviour
{
    [HideInInspector] public GameObject CanvasDebugger;

    Vector3 StartPosition;
    Vector3 StartRotation;
    Vector3 StartScale;

    [HideInInspector] public TMP_InputField PosX;
    [HideInInspector] public TMP_InputField PosY;
    [HideInInspector] public TMP_InputField PosZ;

    [HideInInspector] public TMP_InputField RotX;
    [HideInInspector] public TMP_InputField RotY;
    [HideInInspector] public TMP_InputField RotZ;

    [HideInInspector] public TMP_InputField ScaleX;
    [HideInInspector] public TMP_InputField ScaleY;
    [HideInInspector] public TMP_InputField ScaleZ;

    private void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.localEulerAngles;
        StartScale = transform.localScale;

        GetInputs();
        PullSave();
        AddListenersToInput();
    }

    void ChangePosition()
    {
        float x, y, z;

        if (float.TryParse(PosX.text, out x) && float.TryParse(PosY.text, out y) && float.TryParse(PosZ.text, out z))
        {
            transform.position = StartPosition + new Vector3(x, y, z);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "PosX", PosX.text);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "PosY", PosY.text);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "PosZ", PosZ.text);
        }
    }

    void ChangeRotation()
    {
        float x, y, z;

        if (float.TryParse(RotX.text, out x) && float.TryParse(RotY.text, out y) && float.TryParse(RotZ.text, out z))
        {
            transform.eulerAngles = StartRotation + new Vector3(x, y, z);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "RotX", RotX.text);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "RotY", RotY.text);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "RotZ", RotZ.text);
        }
    }

    void ChangeScale()
    {
        float x, y, z;

        if (float.TryParse(ScaleX.text, out x) && float.TryParse(ScaleY.text, out y) && float.TryParse(ScaleZ.text, out z))
        {
            transform.localScale = StartScale + new Vector3(x, y, z);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "ScaleX", ScaleX.text);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "ScaleY", ScaleY.text);
            PlayerPrefs.SetString(GetInstanceID().ToString() + "ScaleZ", ScaleZ.text);
        }
    }

    void GetInputs()
    {
        PosX = CanvasDebugger?.transform.Find("Position").Find("PosX").GetComponent<TMP_InputField>();
        PosY = CanvasDebugger?.transform.Find("Position").Find("PosY").GetComponent<TMP_InputField>();
        PosZ = CanvasDebugger?.transform.Find("Position").Find("PosZ").GetComponent<TMP_InputField>();

        RotX = CanvasDebugger?.transform.Find("Rotation").Find("RotX").GetComponent<TMP_InputField>();
        RotY = CanvasDebugger?.transform.Find("Rotation").Find("RotY").GetComponent<TMP_InputField>();
        RotZ = CanvasDebugger?.transform.Find("Rotation").Find("RotZ").GetComponent<TMP_InputField>();

        ScaleX = CanvasDebugger?.transform.Find("Scale").Find("ScaleX").GetComponent<TMP_InputField>();
        ScaleY = CanvasDebugger?.transform.Find("Scale").Find("ScaleY").GetComponent<TMP_InputField>();
        ScaleZ = CanvasDebugger?.transform.Find("Scale").Find("ScaleZ").GetComponent<TMP_InputField>();
    }

    void AddListenersToInput()
    {
        PosX.onValueChanged.AddListener(delegate { ChangePosition(); });
        PosY.onValueChanged.AddListener(delegate { ChangePosition(); });
        PosZ.onValueChanged.AddListener(delegate { ChangePosition(); });

        RotX.onValueChanged.AddListener(delegate { ChangeRotation(); });
        RotY.onValueChanged.AddListener(delegate { ChangeRotation(); });
        RotZ.onValueChanged.AddListener(delegate { ChangeRotation(); });

        ScaleX.onValueChanged.AddListener(delegate { ChangeScale(); });
        ScaleY.onValueChanged.AddListener(delegate { ChangeScale(); });
        ScaleZ.onValueChanged.AddListener(delegate { ChangeScale(); });
    }

    void PullSave()
    {
        PosX.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "PosX");
        PosY.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "PosY");
        PosZ.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "PosZ");
        ChangePosition();

        RotX.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "RotX");
        RotY.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "RotY");
        RotZ.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "RotZ");
        ChangeRotation();

        ScaleX.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "ScaleX");
        ScaleY.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "ScaleY");
        ScaleZ.text = PlayerPrefs.GetString(GetInstanceID().ToString() + "ScaleZ");
        ChangeScale();
    }


}
