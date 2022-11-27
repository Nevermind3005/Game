using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    [SerializeField] private string levelName;
    [SerializeField] private TextMeshProUGUI text;

    public void Start()
    {
        text.text = levelName;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(levelName);
    }
}
