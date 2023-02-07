using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;
    public static int currentMoney=0;

    void Start()
    {
        moneyText.text = currentMoney.ToString();
        EventManager.EnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        moneyText.text = currentMoney.ToString();
    }

}
