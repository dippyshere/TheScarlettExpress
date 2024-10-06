#region

using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class StoveSelect : MonoBehaviour
{
    [FormerlySerializedAs("Select1"),SerializeField] GameObject select1;
    [FormerlySerializedAs("Select10"),SerializeField] GameObject select10;
    [FormerlySerializedAs("Select11"),SerializeField] GameObject select11;
    [FormerlySerializedAs("Select12"),SerializeField] GameObject select12;

    [FormerlySerializedAs("Select13"),SerializeField] GameObject select13;
    [FormerlySerializedAs("Select2"),SerializeField] GameObject select2;
    [FormerlySerializedAs("Select3"),SerializeField] GameObject select3;
    [FormerlySerializedAs("Select4"),SerializeField] GameObject select4;

    [FormerlySerializedAs("Select5"),SerializeField] GameObject select5;
    [FormerlySerializedAs("Select6"),SerializeField] GameObject select6;
    [FormerlySerializedAs("Select7"),SerializeField] GameObject select7;
    [FormerlySerializedAs("Select8"),SerializeField] GameObject select8;

    [FormerlySerializedAs("Select9"),SerializeField] GameObject select9;

    public void Select1GO()
    {
        select1.SetActive(true);
        select2.SetActive(false);
        select3.SetActive(false);
        select4.SetActive(false);
    }

    public void Select2GO()
    {
        select1.SetActive(false);
        select2.SetActive(true);
        select3.SetActive(false);
        select4.SetActive(false);
    }

    public void Select3GO()
    {
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(true);
        select4.SetActive(false);
    }

    public void Select4GO()
    {
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);
        select4.SetActive(true);
    }

    public void Select5GO()
    {
        select5.SetActive(true);
        select6.SetActive(false);
        select7.SetActive(false);
        select8.SetActive(false);
    }

    public void Select6GO()
    {
        select5.SetActive(false);
        select6.SetActive(true);
        select7.SetActive(false);
        select8.SetActive(false);
    }

    public void Select7GO()
    {
        select5.SetActive(false);
        select6.SetActive(false);
        select7.SetActive(true);
        select8.SetActive(false);
    }

    public void Select8GO()
    {
        select5.SetActive(false);
        select6.SetActive(false);
        select7.SetActive(false);
        select8.SetActive(true);
    }

    public void Select9GO()
    {
        select9.SetActive(true);
        select10.SetActive(false);
        select11.SetActive(false);
        select12.SetActive(false);
    }

    public void Select10GO()
    {
        select9.SetActive(false);
        select10.SetActive(true);
        select11.SetActive(false);
        select12.SetActive(false);
    }

    public void Select11GO()
    {
        select9.SetActive(false);
        select10.SetActive(false);
        select11.SetActive(true);
        select12.SetActive(false);
    }

    public void Select12GO()
    {
        select9.SetActive(false);
        select10.SetActive(false);
        select11.SetActive(false);
        select12.SetActive(true);
    }

    public void Select13GO()
    {
        select13.SetActive(true);
    }

    public void Close()
    {
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);
        select4.SetActive(false);
        select5.SetActive(false);
        select6.SetActive(false);
        select7.SetActive(false);
        select8.SetActive(false);
        select9.SetActive(false);
        select10.SetActive(false);
        select11.SetActive(false);
        select12.SetActive(false);
        select13.SetActive(false);
    }
}