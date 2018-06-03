
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool toRight = true;
    public float baseRunSpeed = 5;
    public float jumpForce = 200;
    public int maxJumps = 2;

    [SerializeField]
    private LayerMask groundLayers;

    [SerializeField]
    private bool infiniteJump = false;

    [SerializeField]
    private AudioSource[] audioSources;
    [SerializeField]
    private AudioClip[] jumpUpSounds;
    private int jumpUpSoundsLength;
    [SerializeField]
    private AudioClip jumpLandingSound;
    [SerializeField]
    private AudioClip[] pickUpItem1Sounds;
    private int pickUpItem1SoundsLength;

    private float runSpeed;
    private bool startGame = false;
    private int jumps;
    private bool grounded = true;
    private bool startJump = false;
    private bool onJump = false;
    private bool onJumpOld = false;
    private float groundCheckRadius = 0.25f;

    private Vector2 jumpForceVector;
    private float maxXInCamera;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform groundCheck;

    private Camera cameraMain;

    public delegate void DeadAction();
    public static event DeadAction OnDead;

    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cameraMain = Camera.main;

        UpdateLevelParameters();

        jumpUpSoundsLength = jumpUpSounds.Length;
        pickUpItem1SoundsLength = pickUpItem1Sounds.Length;

        maxXInCamera = cameraMain.aspect * cameraMain.orthographicSize;

        groundCheck = transform.Find("GroundCheck");

        jumpForceVector = new Vector2(0, jumpForce);
    }

    // <-- No es obligatorio, pero es una BUENA PRÁCTICA poner el EventHandler en "OnEnable"
    private void OnEnable() {
        GameParameters.alive = true;
        Item.OnTake += PickUpItem;
    }

    private void OnDisable() {
        GameParameters.alive = false;
        Item.OnTake -= PickUpItem;
    }
    // -->

    private void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
        anim.SetBool("grounded", grounded);
        anim.SetFloat("velY", rb.velocity.y);

        if (Scores.alive) {
            // Checar caida
            if (transform.position.y < -8.5)
                Lose(1);

            // Checar si se queda fuiera de cámara
            float camPosX = cameraMain.transform.position.x;
            if (transform.position.x - camPosX < -maxXInCamera - 1.5) {
                Lose(2);
            }
        }
    }

    public void UpdateLevelParameters() {
        float multiplier = (1.0f + (GameParameters.level - 1.0f) / 10.0f);
        runSpeed = baseRunSpeed * multiplier;
        anim.speed = multiplier;

        Debug.Log("Char speed: " + runSpeed);
    }

    public void Move(float horizontal, bool jump) {

        onJumpOld = onJump;

        // Si el personaje se mueve Iniciar el juego!!!
        if (!startGame) {
            if (horizontal != 0 || jump) {
                startGame = true;
                cameraMain.GetComponent<MoveCamera>().StartGame();
            }
        }

        // Reiniciar el contador de saltos
        if (grounded && !startJump) {
            jumps = maxJumps;
            onJump = false;
        }
        startJump = false;

        float absVelX = Mathf.Abs(horizontal);
        float velX = horizontal * runSpeed;

        anim.SetFloat("velX", absVelX);
        rb.velocity = new Vector2(velX, rb.velocity.y);

        if (horizontal > 0 && !toRight)
            Flip();
        else if (horizontal < 0 && toRight)
            Flip();

        //if(grounded && jump && anim.GetBool("grounded")) {
        if ((jumps > 0 || infiniteJump) && jump) {
            PlaySound(0, ref jumpUpSounds[Random.Range(0, jumpUpSoundsLength)]);
            grounded = false;
            anim.SetBool("grounded", false);
            rb.velocity = new Vector2(velX, 0);
            rb.AddForce(jumpForceVector);
            jumps--;
            startJump = onJump = true;
        }

        // Limita el movimiento del Personaje a la derecha
        float camPosX = cameraMain.transform.position.x;
        if (transform.position.x - camPosX > maxXInCamera) {
            Vector3 maxPosX = transform.position;
            maxPosX.x  = maxXInCamera + camPosX;
            transform.position = maxPosX;
        }

        // Checar si el personaje ha aterrizado.
        if (onJumpOld == true && onJump == false) {
            PlaySound(1, ref jumpLandingSound);
        }
    }

    // Cambia de dirección el sprite según la velocidad
    public void Flip() {
        toRight = !toRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Cuando Recoge un item
    private void PickUpItem() {
        PlaySound(1, ref pickUpItem1Sounds[Random.Range(0, pickUpItem1SoundsLength)]);
    }

    // Si Cae de las plataformas!!
    private void Lose(int type) {
        Scores.alive = false;

        if (OnDead != null) {
            OnDead();
        }

        gameObject.SetActive(false);
    }

    // Reproducir sonido simple
    private void PlaySound(int source, ref AudioClip clip) {
        audioSources[source].clip = clip;
        audioSources[source].Play();
    }
}
