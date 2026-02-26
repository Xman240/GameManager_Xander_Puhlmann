using TMPro;
using UnityEngine;

public class CoinCountUI : MonoBehaviour
{
    public TMP_Text coinText;

    private void Start()
    {
        RefreshCoinCount();
    }

    private void OnEnable()
    {
        TreasurePickup.PickupTreasure += RefreshCoinCount;
    }

    private void OnDisable()
    {
        TreasurePickup.PickupTreasure -= RefreshCoinCount;
    }
    
    

    private void RefreshCoinCount()
    {
        int coins = GameStateManager.Instance.gameState.coinsCollected;
        coinText.text = $"{coins.ToString()} / 3 coins";
    }
}
