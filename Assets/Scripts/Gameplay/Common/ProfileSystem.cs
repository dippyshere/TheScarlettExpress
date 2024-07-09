using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProfileSystem
{
    private const int NumSaveSlots = 4;
    private const string CurrentSaveSlotKey = "CurrentSaveSlot";
    private const string GlobalPrefix = "Global_";

    private static int currentSaveSlot = 0;

    public enum Variable
    {
        // add more variables here
        PlayerName,
        CurrentStation,
        StationDestination,
        StationDistance,
        PlayerScore,
        GameVolume
    }

    private static readonly Dictionary<Variable, object> defaultValues = new Dictionary<Variable, object>
    {
        // default values are set here
        { Variable.PlayerName, "Name" },
        { Variable.CurrentStation, "None" },
        { Variable.StationDestination, 1 },
        { Variable.StationDistance, 1 },
        { Variable.PlayerScore, 0 },
        { Variable.GameVolume, 1.0f },
    };

    static ProfileSystem()
    {
        currentSaveSlot = PlayerPrefs.GetInt(CurrentSaveSlotKey, 0);
    }

    public static int CurrentSaveSlot
    {
        get => currentSaveSlot;
        set
        {
            if (value >= 0 && value < NumSaveSlots)
            {
                currentSaveSlot = value;
                PlayerPrefs.SetInt(CurrentSaveSlotKey, currentSaveSlot);
            }
            else
            {
                Debug.LogError("Invalid save slot index");
            }
        }
    }

    public static void Set<T>(Variable variable, T value)
    {
        string key = GetSaveSlotKey(variable.ToString());
        if (value is int intValue)
        {
            PlayerPrefs.SetInt(key, intValue);
        }
        else if (value is float floatValue)
        {
            PlayerPrefs.SetFloat(key, floatValue);
        }
        else if (value is string stringValue)
        {
            PlayerPrefs.SetString(key, stringValue);
        }
        else
        {
            Debug.LogError("Unsupported type");
        }
    }

    public static T Get<T>(Variable variable)
    {
        string key = GetSaveSlotKey(variable.ToString());
        if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, (int)defaultValues[variable]);
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, (float)defaultValues[variable]);
        }
        else if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, (string)defaultValues[variable]);
        }
        else
        {
            Debug.LogError("Unsupported type");
            return default;
        }
    }

    public static void SetGlobal<T>(Variable variable, T value)
    {
        string key = GetGlobalKey(variable.ToString());
        if (value is int intValue)
        {
            PlayerPrefs.SetInt(key, intValue);
        }
        else if (value is float floatValue)
        {
            PlayerPrefs.SetFloat(key, floatValue);
        }
        else if (value is string stringValue)
        {
            PlayerPrefs.SetString(key, stringValue);
        }
        else
        {
            Debug.LogError("Unsupported type");
        }
    }

    public static T GetGlobal<T>(Variable variable)
    {
        string key = GetGlobalKey(variable.ToString());
        if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, (int)defaultValues[variable]);
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, (float)defaultValues[variable]);
        }
        else if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, (string)defaultValues[variable]);
        }
        else
        {
            Debug.LogError("Unsupported type");
            return default;
        }
    }

    private static string GetSaveSlotKey(string key)
    {
        return $"Slot{currentSaveSlot}_{key}";
    }

    private static string GetGlobalKey(string key)
    {
        return $"{GlobalPrefix}{key}";
    }
}
