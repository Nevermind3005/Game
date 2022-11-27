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
        showHp();
    }

    public void DamagePlayer()
    {
        GameManager.gameManager.playerHealth.Demage(1);
        showHp();
        if (GameManager.gameManager.playerHealth.CurrentHealth == 0)
        {
            gameObject.GetComponent<CharacterController>().Kill();
            virtualCamera.enabled = false;
            StartCoroutine(ReloadSceneWait(1.5f));
            return;
        }
        var dir = gameObject.GetComponent<Transform>().rotation.y < 0 ? 1 : -1;
        gameObject.GetComponent<CharacterController>().DisablePlayerControls(0.3f);
        gameObject.GetComponent<PlayerBehaviour>().DisableEnemyCollision();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( dir * 22, 7), ForceMode2D.Impulse);
    }

    private void showHp()
    {
        hpCount.text = "HP: " + GameManager.gameManager.playerHealth.CurrentHealth + "/" +
                       GameManager.gameManager.playerHealth.InitialHealth;
    }

    public void DisableEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        StartCoroutine(EnableEnemyCollision());
    }
    

    private IEnumerator EnableEnemyCollision()
    {
        yield return new WaitForSeconds(4);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    private IEnumerator ReloadSceneWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.gameManager = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
