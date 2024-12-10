#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public static class ProfileSystem
{
    const int NumSaveSlots = 5;
    const string CurrentSaveSlotKey = "CurrentSaveSlot";
    const string GlobalPrefix = "Global_";

    static int _currentSaveSlot;

    static readonly Dictionary<Variable, object> DefaultValues = new()
    {
        // default values are set here
        { Variable.PlayerName, "Name" },
        { Variable.PlayerPronoun1, "They" },
        { Variable.PlayerPronoun2, "Them" },
        { Variable.LastPlayed, "" },
        { Variable.LastScene, "_Onboarding" },
        { Variable.CurrentStation, "None" },
        { Variable.StationDestination, 0 },
        { Variable.StationDistance, 1 },
        { Variable.PlayerMoney, 75.0f },
        { Variable.GameVolume, 1.0f },
        { Variable.EveTutorialDone, false },
        { Variable.RestaurantTutorialDone, false },
        { Variable.DecoratingTutorialStarted, false },
        { Variable.DecoratingTutorialDone, false },
        { Variable.UpgradeTutorialDone, false },
        { Variable.HasBeenToFurrowood, false },
        { Variable.AcquiredTheBanks, false },
        { Variable.BanksHomed, false },
        { Variable.HasBeenToFernValley, false },
        { Variable.AcquiredPaints, false },
        { Variable.EveQuest2Started, false },
        { Variable.HasRenovatedBedroom1, false },
        { Variable.HasRenovatedEntertainment, false },
        { Variable.StartedUpgradeTutorial, false },

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
        { Variable.Deco11, 0 },
        { Variable.Deco12, 0 },
        { Variable.Deco13, 0 },
        { Variable.Deco14, 0 },
        { Variable.Deco15, 0 },

        { Variable.Deco16, 0 },
        { Variable.Deco17, 0 },
        { Variable.Deco18, 0 },
        { Variable.Deco19, 0 },
        { Variable.Deco20, 0 },
        { Variable.Deco21, 0 },
        { Variable.Deco22, 0 },
        { Variable.Deco23, 0 },
        { Variable.Deco24, 0 },
        { Variable.Deco25, 0 },
        { Variable.Deco26, 0 },
        { Variable.Deco27, 0 },
        { Variable.Deco28, 0 },
        { Variable.Deco29, 0 },
        { Variable.Deco30, 0 },
        { Variable.FreeDeco, 0 },

        { Variable.Bed1Painting1, -1 },
        { Variable.Bed1Painting2, -1 },
        { Variable.Bed1Rug1, -1 },
        { Variable.Bed2Painting1, -1 },
        { Variable.Bed2Painting2, -1 },
        { Variable.Bed2Rug1, -1 },
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

        { Variable.PassengerStorage, "{}" },

        //Eve Quest
        { Variable.EveQuestStarted, false },
        { Variable.RetrievedBroccoliSoup, false },
        { Variable.EveQuestFinished, false },

        //Banks Quest
        { Variable.BanksQuestStarted,  false },
        { Variable.RetrievedYellowSpringSalad, false },
        { Variable.BanksQuestFinished,  false },

        //Bunk Upgrades
        { Variable.Bedroom1Bunk1, 0  },
        { Variable.Bedroom1Bunk2, 0  },
        { Variable.Bedroom1Bunk3, 0  },
        { Variable.Bedroom1Bunk4, 0  },
        { Variable.Bedroom1Bunk5, 0  },
        { Variable.Bedroom1Bunk6, 0  }
    };

    static ProfileSystem()
    {
        _currentSaveSlot = PlayerPrefs.GetInt(CurrentSaveSlotKey, 0);
    }

    public static int CurrentSaveSlot
    {
        get { return _currentSaveSlot; }
        set
        {
            if (value >= 0 && value < NumSaveSlots)
            {
                _currentSaveSlot = value;
                PlayerPrefs.SetInt(CurrentSaveSlotKey, _currentSaveSlot);
            }
            else
            {
                Debug.LogError("Invalid save slot index");
            }
        }
    }

    public static void Set<T>(Variable variable, T value, bool updateLastScene = true)
    {
        string lastPlayedKey = GetSaveSlotKey(Variable.LastPlayed.ToString());
        // set last played time using datetime with the day, month, year format of the current system locale, then followed by a " - " and the time in the current system locale
        PlayerPrefs.SetString(lastPlayedKey, System.DateTime.Now.ToString("d", System.Globalization.CultureInfo.CurrentCulture) + " - " + System.DateTime.Now.ToString("t", System.Globalization.CultureInfo.CurrentCulture));
        if (updateLastScene)
        {
            string lastSceneKey = GetSaveSlotKey(Variable.LastScene.ToString());
            PlayerPrefs.SetString(lastSceneKey, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        string key = GetSaveSlotKey(variable.ToString());
        switch (value)
        {
            case int intValue:
                PlayerPrefs.SetInt(key, intValue);
                break;
            case float floatValue:
                PlayerPrefs.SetFloat(key, floatValue);
                break;
            case string stringValue:
                PlayerPrefs.SetString(key, stringValue);
                break;
            case bool boolValue:
                PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
                break;
            default:
                Debug.LogError("Unsupported type");
                break;
        }
    }

    public static T Get<T>(Variable variable, int slot = -1)
    {
        int currentSlot = _currentSaveSlot;
        if (slot >= 0)
        {
            _currentSaveSlot = slot;
        }
        string key = GetSaveSlotKey(variable.ToString());
        if (slot >= 0)
        {
            _currentSaveSlot = currentSlot;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, (int)DefaultValues[variable]);
        }

        if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, (float)DefaultValues[variable]);
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, (string)DefaultValues[variable]);
        }

        if (typeof(T) == typeof(bool))
        {
            if ((bool)DefaultValues[variable])
            {
                if (PlayerPrefs.GetInt(key, 1) == 1)
                {
                    return (T)(object)true;
                }

                return (T)(object)false;
            }

            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                return (T)(object)true;
            }

            return (T)(object)false;
        }

        Debug.LogError("Unsupported type");
        return default;
    }

    public static void SetGlobal<T>(Variable variable, T value)
    {
        string key = GetGlobalKey(variable.ToString());
        switch (value)
        {
            case int intValue:
                PlayerPrefs.SetInt(key, intValue);
                break;
            case float floatValue:
                PlayerPrefs.SetFloat(key, floatValue);
                break;
            case string stringValue:
                PlayerPrefs.SetString(key, stringValue);
                break;
            case bool boolValue:
                PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
                break;
            default:
                Debug.LogError("Unsupported type");
                break;
        }
    }

    public static T GetGlobal<T>(Variable variable)
    {
        string key = GetGlobalKey(variable.ToString());
        if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, (int)DefaultValues[variable]);
        }

        if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, (float)DefaultValues[variable]);
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, (string)DefaultValues[variable]);
        }

        if (typeof(T) == typeof(bool))
        {
            if ((bool)DefaultValues[variable])
            {
                if (PlayerPrefs.GetInt(key, 1) == 1)
                {
                    return (T)(object)true;
                }

                return (T)(object)false;
            }

            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                return (T)(object)true;
            }

            return (T)(object)false;
        }

        Debug.LogError("Unsupported type");
        return default;
    }

    static string GetSaveSlotKey(string key)
    {
        return $"Slot{_currentSaveSlot}_{key}";
    }

    static string GetGlobalKey(string key)
    {
        return $"{GlobalPrefix}{key}";
    }

    public static void ClearProfile(int slot = -1)
    {
        if (slot == -1)
        {
            for (int i = 0; i < NumSaveSlots; i++)
            {
                foreach (Variable variable in DefaultValues.Keys)
                {
                    string key = GetSaveSlotKey(variable.ToString());
                    PlayerPrefs.DeleteKey(key);
                }
            }
        }
        else
        {
            int currentSlot = _currentSaveSlot;
            _currentSaveSlot = slot;
            foreach (string key in DefaultValues.Keys.Select(variable => GetSaveSlotKey(variable.ToString())))
            {
                PlayerPrefs.DeleteKey(key);
            }
            _currentSaveSlot = currentSlot;
        }
    }

    public enum Variable
    {
        // add more variables here
        PlayerName,
        PlayerPronoun1,
        PlayerPronoun2,
        LastPlayed,
        LastScene,
        CurrentStation,
        StationDestination,
        StationDistance,
        PlayerMoney,
        GameVolume,

        //tutorials
        EveTutorialDone,
        RestaurantTutorialDone,
        DecoratingTutorialStarted,
        DecoratingTutorialDone,
        UpgradeTutorialDone,

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
        Deco11,
        Deco12,
        Deco13,
        Deco14,
        Deco15,
        Deco16,
        Deco17,
        Deco18,
        Deco19,
        Deco20,
        Deco21,
        Deco22,
        Deco23,
        Deco24,
        Deco25, 
        Deco26,
        Deco27,
        Deco28,
        Deco29,
        Deco30,
        FreeDeco,
        Bed1Painting1,
        Bed1Painting2,
        Bed2Painting1,
        Bed2Painting2,
        Bed1Rug1,
        Bed2Rug1,
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
        PassengerStorage,

        //Eve Quest
        EveQuestStarted,
        RetrievedBroccoliSoup,
        EveQuestFinished,

        HasBeenToFernValley,
        AcquiredPaints,
        EveQuest2Started,

        //Banks Quest
        HasBeenToFurrowood,
        AcquiredTheBanks,
        BanksHomed,
        RetrievedYellowSpringSalad,
        BanksQuestStarted,
        BanksQuestFinished,

        //Renovation
        HasRenovatedBedroom1,
        HasRenovatedEntertainment,

        StartedUpgradeTutorial,

        //Bunk Upgrades
        Bedroom1Bunk1,
        Bedroom1Bunk2,
        Bedroom1Bunk3,
        Bedroom1Bunk4,
        Bedroom1Bunk5,
        Bedroom1Bunk6
    }
}