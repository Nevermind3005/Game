using System;
using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpCount;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.g, 0.5f);
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
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.g, 1f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    private IEnumerator ReloadSceneWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
