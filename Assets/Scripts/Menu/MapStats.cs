#region

using UnityEngine;
using UnityEngine.EventSystems;

#endregion

public class MapStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject stationStats;

    void Start()
    {
        stationStats.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        stationStats.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stationStats.SetActive(false);
    }
}