using UnityEngine;

public class PlayerTommyController : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 10;
    public int totalLifes = 3;
    public int maxSeconds = 120;
    public int maxJumps = 10;
    public Transform respawnPosition;

    public AudioManager audioManager;
    public UIManager uiManager;
    public SceneController sceneController;
    public VFXManager vfxManager;
    public CameraManager cameraManager;

    private int coinsCollected = 0;
    private int lifesRemaining = 0;
    private int jumpsCount = 0;
    private float horizontal;
    private float vertical;    
    private bool isGameOver;
    private bool isWinner;
    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    
    void Start()
    {
        lifesRemaining = totalLifes;
        uiManager.UpdateLifesRemaining(lifesRemaining);

        coinsCollected = 0;
        uiManager.UpdateCoinsCollected(coinsCollected);

        uiManager.SetPlayerTime(maxSeconds);

        jumpsCount = 0;
        uiManager.UpdateJumps(maxJumps);

        isGameOver = false;
        isWinner = false;
    }
    
    void Update()
    {
        if (isGameOver || isWinner)
            return;

        if (coinsCollected == uiManager.TotalCoins)
            PlayerWin();

        if ( (jumpsCount > maxJumps) || uiManager.TimeOut())
            PlayerDead();

        // El player no se moverá usando transform.Translate, sino que modificando la velocidad del Rigidbody en el FixedUpdate para mejorar la precisión de las colisiones
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        anim.SetFloat("Move Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        jumpsCount++;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("Jump");
        audioManager.PlayJumpSound();
        uiManager.UpdateJumps(maxJumps - jumpsCount);
    }

    private void FixedUpdate()
    {
        if (isGameOver || isWinner)
            return;

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
            CoinCollected(other.gameObject);
        else if (other.CompareTag("Dead Zone"))
            PlayerDead();
    }

    private void CoinCollected(GameObject other)
    {
        Destroy(other);
        coinsCollected++;
        audioManager.PlayCoinSound();
        uiManager.UpdateCoinsCollected(coinsCollected);
        vfxManager.CoinDestroy(other.transform.position);
    }

    private void PlayerDead()
    {
        lifesRemaining--;
        rb.velocity = Vector3.zero;
        transform.position = respawnPosition.position;

        uiManager.UpdateLifesRemaining(lifesRemaining);

        if (lifesRemaining > 0)
        {
            audioManager.PlayLostOneLifeSound();
            uiManager.SetPlayerTime(maxSeconds);
            uiManager.UpdateJumps(maxJumps);
            jumpsCount = 0;
        }
        else
        {
            isGameOver = true;
            sceneController.ShowGameOver();
        }
    }

    private void PlayerWin()
    {
        isWinner = true;
        anim.SetTrigger("Victory");
        audioManager.PlayWinSong();
        uiManager.PlayerWin();
        cameraManager.PlayerWin();
    }
}
