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
        PlayerPronoun1,
        PlayerPronoun2,
        CurrentStation,
        StationDestination,
        StationDistance,
        PlayerMoney,
        GameVolume,

        //Decorations
        Deco1,
        Deco2,
        Deco3,
        Deco4,
        Deco5,
        Deco6,
        Deco7,
        Deco8,
        Deco9,
        Deco10
    }

    private static readonly Dictionary<Variable, object> defaultValues = new Dictionary<Variable, object>
    {
        // default values are set here
        { Variable.PlayerName, "Name" },
        { Variable.PlayerPronoun1, "They" },
        { Variable.PlayerPronoun2, "Them" },
        { Variable.CurrentStation, "None" },
        { Variable.StationDestination, 1 },
        { Variable.StationDistance, 1 },
        { Variable.PlayerMoney, 0.0f },
        { Variable.GameVolume, 1.0f },

        //Decorations
        { Variable.Deco1, 0 },
        { Variable.Deco2, 0 },
        { Variable.Deco3, 0 },
        { Variable.Deco4, 0 },
        { Variable.Deco5, 0 },
        { Variable.Deco6, 0 },
        { Variable.Deco7, 0 },
        { Variable.Deco8, 0 },
        { Variable.Deco9, 0 },
        { Variable.Deco10, 0 },

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

    public static void ClearProfile()
    {
        foreach (Variable variable in defaultValues.Keys)
        {
            Set(variable, defaultValues[variable]);
        }
    }
}
