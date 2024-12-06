#region

using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class UpgradeManager : MonoBehaviour
{
    [FormerlySerializedAs("Car1Chair1")] public GameObject car1Chair1;
    [FormerlySerializedAs("Car1Chair2")] public GameObject car1Chair2;
    [FormerlySerializedAs("Car1Chair3")] public GameObject car1Chair3;
    [FormerlySerializedAs("Car1Chair4")] public GameObject car1Chair4;
    [FormerlySerializedAs("Car1Chair5")] public GameObject car1Chair5;
    [FormerlySerializedAs("Car1Chair6")] public GameObject car1Chair6;

    [FormerlySerializedAs("Car2Chair1")] public GameObject car2Chair1;
    [FormerlySerializedAs("Car2Chair2")] public GameObject car2Chair2;
    [FormerlySerializedAs("Car2Chair3")] public GameObject car2Chair3;
    [FormerlySerializedAs("Car2Chair4")] public GameObject car2Chair4;
    [FormerlySerializedAs("Car2Chair5")] public GameObject car2Chair5;
    [FormerlySerializedAs("Car2Chair6")] public GameObject car2Chair6;

    [FormerlySerializedAs("Car3Chair1")] public GameObject car3Bunk1;
    [FormerlySerializedAs("Car3Chair2")] public GameObject car3Bunk2;
    [FormerlySerializedAs("Car3Chair3")] public GameObject car3Bunk3;
    [FormerlySerializedAs("Car3Chair4")] public GameObject car3Bunk4;
    [FormerlySerializedAs("Car3Chair5")] public GameObject car3Bunk5;
    [FormerlySerializedAs("Car3Chair6")] public GameObject car3Bunk6;

    public void Up1()
    {
        car1Chair1.SetActive(true);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up2()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(true);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up3()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(true);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up4()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(true);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up5()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(true);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up6()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(true);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up7()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(true);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up8()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(true);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up9()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(true);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up10()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(true);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up11()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(true);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up12()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(true);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    //Bunks

    public void Up13()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(true);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up14()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(true);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up15()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(true);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up16()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(true);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(false);
    }

    public void Up17()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(true);
        car3Bunk6.SetActive(false);
    }

    public void Up18()
    {
        car1Chair1.SetActive(false);
        car1Chair2.SetActive(false);
        car1Chair3.SetActive(false);
        car1Chair4.SetActive(false);
        car1Chair5.SetActive(false);
        car1Chair6.SetActive(false);
        car2Chair1.SetActive(false);
        car2Chair2.SetActive(false);
        car2Chair3.SetActive(false);
        car2Chair4.SetActive(false);
        car2Chair5.SetActive(false);
        car2Chair6.SetActive(false);

        car3Bunk1.SetActive(false);
        car3Bunk2.SetActive(false);
        car3Bunk3.SetActive(false);
        car3Bunk4.SetActive(false);
        car3Bunk5.SetActive(false);
        car3Bunk6.SetActive(true);
    }
}