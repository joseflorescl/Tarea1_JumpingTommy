using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public ParticleSystem vfxCoinDestroy;

    public void CoinDestroy(Vector3 position)
    {
        vfxCoinDestroy.transform.position = position;
        vfxCoinDestroy.Play();
    }
}
