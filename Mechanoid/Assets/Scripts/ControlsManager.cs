using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlsManager : MonoBehaviour
{
    private Dictionary<string, string> defaultControls = new Dictionary<string, string>();

    void Awake()
    {
        defaultControls.Add("mainFire", "Mouse0");
        defaultControls.Add("secondaryFire", "Mouse1");
        defaultControls.Add("sprint", "LeftShift");
        defaultControls.Add("tiltTowerUp", "E");
        defaultControls.Add("tiltTowerDown", "Q");
        defaultControls.Add("unlockCursor", "Escape");
        defaultControls.Add("jump", "Space");
    }

    public KeyCode getKey(string prefCode)
    {
        return (KeyCode) Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(prefCode, defaultControls[prefCode]));
    }

}