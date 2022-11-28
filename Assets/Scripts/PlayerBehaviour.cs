using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpCount;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        ShowHp();
    }

    public void DamagePlayer()
    {
        GameManager.gameManager.playerHealth.Demage(1);
        ShowHp();
        if (GameManager.gameManager.playerHealth.CurrentHealth == 0)
        {
            KillDontFollow();
            return;
        }

        gameObject.GetComponent<CharacterController>().Hit();
    }

    public void ShowHp()
    {
        hpCount.text = "HP: " + GameManager.gameManager.playerHealth.CurrentHealth + "/" +
                       GameManager.gameManager.playerHealth.InitialHealth;
    }

    public void DisableEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        StartCoroutine(EnableEnemyCollision());
    }

    public void KillDontFollow()
    {
        gameObject.GetComponent<CharacterController>().Kill();
        virtualCamera.enabled = false;
        StartCoroutine(ReloadSceneWait(1.5f));
    }

    private IEnumerator EnableEnemyCollision()
    {
        yield return new WaitForSeconds(4);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    private IEnumerator ReloadSceneWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
