using UnityEngine;

public class PlayerTitleScreen : MonoBehaviour
{
    public AudioManager audioManager;

    // Las funciones de esta clase se usan en eventos de animación de la Title Screen
    public void Jump()
    {
        audioManager.PlayJumpSound();
    }

    public void Landing()
    {
        audioManager.PlayLandingSound();
    }
}
