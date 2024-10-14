#region

using TMPro;
using UnityEngine;

#endregion

public class MoneyUI : MonoBehaviour
{
    Economy _economy;
    public TextMeshProUGUI moneyText;

    public float nomini;
    
    void Update()
    {
        nomini = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        moneyText.text = ": $" + nomini;
    }
}