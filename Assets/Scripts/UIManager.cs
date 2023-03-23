using System;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI jumpsText;
    public GameObject winPanel;
    public int TotalCoins; // No setear a mano: se inicializa contando los objetos de tag "Coin" en el Awake. Es public porque la usa el PlayerTommyController

    private float playerTime;
    private TimeSpan timeSpan;
    private bool playerWin;

    private void Awake()
    {
        // Esta asignacion se tiene que hacer en el Awake para que se ejecute antes del Start de PlayerTommyController
        TotalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    private void Start()
    {
        playerWin = false;
    }

    private void Update()
    {
        if (TimeOut() || playerWin)
            return;

        playerTime -= Time.deltaTime;
        timeSpan = TimeSpan.FromSeconds(playerTime);
        timerText.text = timeSpan.ToString(@"m':'ss");
    }

    public void UpdateCoinsCollected(int coinsCollected)
    {
        coinsText.text = coinsCollected.ToString() + "/" + TotalCoins.ToString();
    }

    public void UpdateLifesRemaining(int lifesRemaining)
    {
        lifesText.text = "x" + lifesRemaining.ToString();
    }

    public void UpdateJumps(int jumps)
    {
        jumpsText.text = "x" + jumps.ToString();
    }

    public void SetPlayerTime(float time)
    {
        // Se suma 1 porque NO se muestran los milisegundos: cuando el valor llegue a 0.99 se muestra en la UI "0:00" y el player pierde.
        playerTime = time + 1;
    }

    public bool TimeOut()
    {
        return playerTime < 1f; // Se compara con 1 y no con 0 por la razón anterior.
    }

    public void PlayerWin()
    {
        winPanel.SetActive(true);
        playerWin = true;
    }
    
}
