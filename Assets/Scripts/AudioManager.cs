using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip lostOneLifeSound;
    public AudioClip landingSound;
    public AudioClip winSong;
    public AudioSource sfxAudioSource;
    public AudioSource bgmAudioSource;

    public void PlayCoinSound()
    {
        sfxAudioSource.PlayOneShot(coinSound); // Se prefiere usar PlayOneShot en vez de Play, porque con Play se apaga el sonido en reproducción.
    }

    public void PlayJumpSound()
    {
        sfxAudioSource.PlayOneShot(jumpSound);
    }

    public void PlayLostOneLifeSound()
    {
        sfxAudioSource.PlayOneShot(lostOneLifeSound);
    }

    public void PlayLandingSound()
    {
        // Esta función PlayLandingSound no es usada en el Game porque no se está validando cuando el player colisiona con el suelo
        // Se usa en la animación de la Title Screen
        sfxAudioSource.PlayOneShot(landingSound);
    }

    public void PlayWinSong()
    {
        bgmAudioSource.clip = winSong;
        bgmAudioSource.Play();
    }

}
