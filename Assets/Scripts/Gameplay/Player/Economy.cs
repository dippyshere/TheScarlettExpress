#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class Economy : MonoBehaviour
{
    public AddedMoneyUI addedMoneyUI;
    public float money;

    void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    public void AddMoney(float money)
    {
        addedMoneyUI.MoneyAnimation(money);
        this.money += money;
        ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, this.money);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("money_earned",
                new Dictionary<string, object> { { "money", money }, { "totalMoney", this.money } });
        }
    }
}