using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    [SerializeField] private string levelName;

    public void LoadScene()
    {
        SceneManager.LoadScene(levelName);
    }
}
