using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject stationStats;

    private void Start()
    {
        stationStats.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("WOAH, STATION STATS!");
        stationStats.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stationStats.SetActive(false);
    }



}
