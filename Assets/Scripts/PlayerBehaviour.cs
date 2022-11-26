using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpCount;

    private void Start()
    {
        showHp();
    }

    public void DamagePlayer()
    {
        GameManager.gameManager.playerHealth.Demage(1);
        showHp();
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
}
