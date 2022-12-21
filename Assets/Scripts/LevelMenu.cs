using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    [SerializeField] private string levelName;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int requiredCoinValue = 0;
    
    public void Start()
    {
        text.text = levelName;
        if (requiredCoinValue > MainManager.Instance.allCoinValue)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(levelName);
    }
}
