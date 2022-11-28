using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndMenu : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI coinCounterUI;
    [SerializeField] private TextMeshProUGUI timeCounterUI;

    
    void Start()
    {
        coinCounterUI.text = "Coins: " + MainManager.Instance.coinValue;
        timeCounterUI.text = "Time: " + MainManager.Instance.levelTime + " s";
    }

    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
