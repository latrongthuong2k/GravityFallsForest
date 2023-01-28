using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour 
{
    public float jumpForce = 5;             
    public float gravity = 25;            
    public float moveSpeed = 10;           
    public float maxMoveSpeed = 25;       
    public float increaseValue = 1;         

    public bool enableJump = true;             
    public float swapOffset = 0;           
                                           
    public bool disableOnDeath;            
    public ParticleSystem deathEffect;     

    public AudioClip jumpSFX;               
    public AudioClip swapSFX;               

    private bool bottom;
    private bool grounded;
    private bool dead;

    private float speed;
    private float jumpDir;
    private float swapDifference;
    private float swapDistance;
    private float velocity;

    private Rigidbody2D rb2D;
    private Transform thisT;
    private Vector3 prevPos;
    private Vector3 defaultScale;
    private Vector2 moveDir;
    private GameManager GM;
    private BoxCollider2D playerCol;
    private AudioSource audioSource;


    void OnEnable()
    {
        
        gameObject.tag = "Player";
       
        bottom = false;
    }

	// Use this for initialization
	void Start () 
    {
        
        playerCol = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        thisT = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        GM = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        
        defaultScale = thisT.localScale;
        
        swapDifference = playerCol.bounds.size.y + GM.PlatformHeight() + swapOffset;
       
        speed = moveSpeed;
       
        Physics2D.gravity = new Vector2(0, -gravity);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if(GameManager.isGameOver)
            return;
       
        thisT.localScale = bottom ? new Vector3(defaultScale.x, -defaultScale.y, defaultScale.z) : defaultScale;
        //Inverse jump direction based on the current moving line (bottom or not);
        jumpDir = rb2D.gravityScale = bottom ? -1 : 1;
        //Inverse swap distance;
        swapDistance = bottom ? -swapDifference : swapDifference;

        
        if (grounded)
        {
            if (PlayerInput.Swap)
                Swap();
            else if (PlayerInput.Jump && enableJump)
                Jump();
        }

        
        if (velocity == 0)
            transform.position += Vector3.right * 0.01F;
	}

    //Jump function;
    void Jump()
    {
        Utilities.PlaySFX(audioSource, jumpSFX);                                
        rb2D.AddForce(Vector3.up * jumpForce * jumpDir, ForceMode2D.Impulse);  
        grounded = false;                                                       
    }
    
    //Swap function;
    void Swap()
    {
        Utilities.PlaySFX(audioSource, swapSFX);   
        bottom = !bottom;                           
        SwapPosition();                             
    }

    void FixedUpdate()
    {
       
        velocity = ((thisT.position - prevPos).magnitude) / Time.deltaTime;
        prevPos = transform.position;

        
        if (speed < maxMoveSpeed)
            speed += increaseValue * Time.deltaTime;
        
        moveDir = !dead ? new Vector2(speed * 10 * Time.deltaTime, rb2D.velocity.y) : new Vector2(0, rb2D.velocity.y);
       
        rb2D.velocity = moveDir;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       
        grounded = true;
       
        if (col.gameObject.CompareTag("Obstacle") && !dead)
        {
           
            GM.SetGameOver();
           
            if(deathEffect)
                Instantiate(deathEffect, col.contacts[0].point, Quaternion.identity);
           
            gameObject.SetActive(!disableOnDeath);
           
            dead = true;
        }
    }

    
    void SwapPosition()
    {
        thisT.position = new Vector3(thisT.position.x, thisT.position.y - swapDistance, thisT.position.z);
    }
    
    
    public void Reset()
    {
       
        bottom = false;
       
        rb2D.velocity = Vector2.zero;
        
        speed = moveSpeed;
       
        dead = false;
    }

   
    public bool IsGrounded()
    {
        return grounded;
    }
    public bool IsDead()
    {
        return dead;
    }
    public bool IsBot()
    {
        return bottom;
    }
}
