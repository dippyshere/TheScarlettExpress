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

        //tutorials
        EveTutorialDone,
        RestaurantTutorialDone,
        DecoratingTutorialDone,

        //decorations
        Deco1,
        Deco2,
        Deco3,
        Deco4,
        Deco5,
        Deco6,
        Deco7,
        Deco8,
        Deco9,
        Deco10,
        FreeDeco,
        Bed1Painting1,
        Bed1Painting2,
        Bed1Rug1,
        Bed1Upgrade,
        Restraunt1Table1,
        Restraunt1Table2,
        Restraunt1Table3,
        Restraunt1Table4,
        Restraunt1Table5,
        Restraunt1Table6,
        Restraunt2Table1,
        Restraunt2Table2,
        Restraunt2Table3,
        Restraunt2Table4,
        Restraunt2Table5,
        Restraunt2Table6,
        PassengerStorage
    }

    private static readonly Dictionary<Variable, object> defaultValues = new Dictionary<Variable, object>
    {
        // default values are set here
        { Variable.PlayerName, "Name" },
        { Variable.PlayerPronoun1, "They" },
        { Variable.PlayerPronoun2, "Them" },
        { Variable.CurrentStation, "None" },
        { Variable.StationDestination, 0 },
        { Variable.StationDistance, 1 },
        { Variable.PlayerMoney, 75.0f },
        { Variable.GameVolume, 1.0f },
        { Variable.EveTutorialDone, false },
        { Variable.RestaurantTutorialDone, false },
        { Variable.DecoratingTutorialDone, false },

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
        { Variable.FreeDeco, 0 },

        { Variable.Bed1Painting1, -1 },
        { Variable.Bed1Painting2, -1 },
        { Variable.Bed1Rug1, -1 },
        { Variable.Bed1Upgrade, 0 },
        { Variable.Restraunt1Table1, 0 },
        { Variable.Restraunt1Table2, 0 },
        { Variable.Restraunt1Table3, 0 },
        { Variable.Restraunt1Table4, 0 },
        { Variable.Restraunt1Table5, 0 },
        { Variable.Restraunt1Table6, 0 },
        { Variable.Restraunt2Table1, 0 },
        { Variable.Restraunt2Table2, 0 },
        { Variable.Restraunt2Table3, 0 },
        { Variable.Restraunt2Table4, 0 },
        { Variable.Restraunt2Table5, 0 },
        { Variable.Restraunt2Table6, 0 },
        
        { Variable.PassengerStorage, "{}" }
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
        else if (value is bool boolValue)
        {
            PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
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
        else if (typeof(T) == typeof(bool))
        {
            if ((bool)defaultValues[variable])
            {
                if (PlayerPrefs.GetInt(key, 1) == 1)
                {
                    return (T)(object)true;
                }
                else
                {
                    return (T)(object)false;
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(key, 0) == 1)
                {
                    return (T)(object)true;
                }
                else
                {
                    return (T)(object)false;
                }
            }
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
        else if (value is bool boolValue)
        {
            PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
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
        else if (typeof(T) == typeof(bool))
        {
            if ((bool)defaultValues[variable])
            {
                if (PlayerPrefs.GetInt(key, 1) == 1)
                {
                    return (T)(object)true;
                }
                else
                {
                    return (T)(object)false;
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(key, 0) == 1)
                {
                    return (T)(object)true;
                }
                else
                {
                    return (T)(object)false;
                }
            }
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
