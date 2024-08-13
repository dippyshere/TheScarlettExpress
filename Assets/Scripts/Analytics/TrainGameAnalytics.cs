using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class TrainGameAnalytics : MonoBehaviour
{
    [SerializeField] private bool analyticsEnabled = true;
    [HideInInspector, Tooltip("Singleton instance of the analytics manager")] public static TrainGameAnalytics instance;
    [HideInInspector, Tooltip("A list of all recorded game events")] public List<Dictionary<string, object>> gameEvents = new List<Dictionary<string, object>>();
    private DateTime startTime;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        startTime = DateTime.Now;
    }

    private void OnApplicationQuit()
    {
        if (analyticsEnabled)
        {
            SaveGameEvents();
        }
    }

    /// <summary>
    /// Record a game event with a name and data<br></br><br></br>
    /// Example usage:<br></br>
    /// <code>TrainGameAnalytics.instance.RecordGameEvent("passenger_health", new Dictionary&lt;string, object&gt;() { { "passengerType", 0 }, { "foodType", 1 }, { "hungerLevel", 0.5f } });</code>
    /// </summary>
    /// <param name="eventName">A string name of the event type being recorded</param>
    /// <param name="eventData">A dictionary of the event data to record</param>
    public void RecordGameEvent(string eventName, Dictionary<string, object> eventData)
    {
        Dictionary<string, object> gameEvent = new Dictionary<string, object>
        {
            ["eventName"] = eventName,
            ["eventData"] = eventData,
            ["timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        gameEvents.Add(gameEvent);
    }

    private void SaveGameEvents()
    {
        string json = JsonConvert.SerializeObject(gameEvents, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter> { new Vector3Converter(), new QuaternionConverter() }
        });
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + startTime.ToString("yyyy-MM-dd_HH-mm-ss") + "_game_events.json", json);
        Debug.Log("Game events saved to: " + Application.persistentDataPath + "/" + startTime.ToString("yyyy-MM-dd_HH-mm-ss") + "_game_events.json");
    }

    public class Vector3Converter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector3 v = (Vector3)value;
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(v.x);
            writer.WritePropertyName("y");
            writer.WriteValue(v.y);
            writer.WritePropertyName("z");
            writer.WriteValue(v.z);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(Vector3);
        }
    }

    public class QuaternionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Quaternion q = (Quaternion)value;
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(q.x);
            writer.WritePropertyName("y");
            writer.WriteValue(q.y);
            writer.WritePropertyName("z");
            writer.WriteValue(q.z);
            writer.WritePropertyName("w");
            writer.WriteValue(q.w);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(Quaternion);
        }
    }
}
