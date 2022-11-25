using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public Health playerHealth = new(3);
    public int coinValue = 0;
    
    void Awake()
    {
        if (gameManager is not null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

}
