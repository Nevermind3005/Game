using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        MainManager.Instance.SetCoinValue(GameManager.gameManager.coinValue);
        MainManager.Instance.levelTime = GameManager.gameManager.GetLevelTime();
        SceneManager.LoadScene("LevelEnd");
    }
}
