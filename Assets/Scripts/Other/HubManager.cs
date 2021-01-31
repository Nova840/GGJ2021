using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HubManager : MonoBehaviour {

    [SerializeField]
    private Portal[] levelPortals = default;

    private static string[] allLevelNames = null;
    public static void ForEachLevelName(Action<string> action) {
        if (allLevelNames == null) return;
        foreach (string levelName in allLevelNames) {
            action.Invoke(levelName);
        }
    }
    public static int NumberOfLevels() {
        if (allLevelNames == null) return 0;
        return allLevelNames.Length;
    }

    private void Awake() {
        if (allLevelNames == null) {
            allLevelNames = levelPortals.Select(l => l.DestinationSceneName).ToArray();
        }
    }

}