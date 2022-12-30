using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum Speeds {SuperLow = 0, Slow = 1, Normal = 2, Fast = 3, Faster = 4, Fastest = 5 };
public enum Gamemodes { Cube = 0, Ship = 1 };
public enum Gravity { Upright = 1, Upsidedown = -1 };

public class Movement : MonoBehaviour
{
    private static Movement instance;
    public static Movement Instance => instance;

    public Speeds CurrentSpeed;
    public Gamemodes CurrentGamemode;

    public bool isClear = false;
    public float force = 26.6581f;
    //                     -1  0   1   2    3    4
    float[] SpeedValues = {3, 9f, 10f, 15f, 25f };

    [SerializeField]private Transform GroundCheckTransform;
    [SerializeField]private Transform TopCheckTransForm;
    [SerializeField]private Transform Sprite;
    public LayerMask GroundMask;
    public float GroundCheckRadius;
    private float ResetSpeedTimer = 3f;
    private bool resetBool;
    Rigidbody2D rb;

    [SerializeField] AudioSource JumpSound;
    void Start()
    {
        instance = this;
        JumpSound = GetComponent<AudioSource>();
        ResetSpeedTimer = 3f;
        resetBool = false;
        rb = GetComponent<Rigidbody2D>();
        CurrentSpeed = MainMenu.Instance.CurrentSpeed;
    }

    void Update()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        //transform.position += Vector3.right * SpeedValues[(int)MainMenu.modeNum] * Time.deltaTime;
        if (resetBool == true)
        {
            ResetSpeedTimer -= Time.deltaTime;
        }
        Timer();
        
        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);
            if (Input.GetKey(KeyCode.Space))
            {
                JumpSound.Play();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                return;
            }
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    rb.velocity = Vector2.zero;
            //    rb.AddForce(Vector2.up * 28f, ForceMode2D.Impulse);
            //    return;
            //}
        }
        else
        {
            Sprite.Rotate(Vector3.back *2);
        }
        if (OnTopGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);
            if (Input.GetKey(KeyCode.Space))
            {
                JumpSound.Play();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
                return;
            }
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    rb.velocity = Vector2.zero;
            //    rb.AddForce(Vector2.down * 28f, ForceMode2D.Impulse);
            //    return;
            //}
        }
    }

    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }
    bool OnTopGround()
    {
        return Physics2D.OverlapCircle(TopCheckTransForm.position, GroundCheckRadius, GroundMask);
    }

    public void ChangeThroughPortal(Gamemodes Gamemode, Speeds Speed, Gravity gravity, int State)
    {
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                CurrentGamemode = Gamemode;
                break;
            case 2:
                rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Longblock")
        {
            Destroy(this.gameObject);
        }
        else if(collision.tag == "ShortBlock")
        {
            CurrentSpeed = Speeds.SuperLow;
            resetBool = true;
        }else if(collision.tag == "Goald")
        {
            isClear = true;
        }
    }
    
    private void Timer()
    {
       
        if (ResetSpeedTimer <= 0)
        {
            ResetSpeedTimer = 3f;
            resetBool = false;
            CurrentSpeed = Speeds.Slow;
        }
    }
}
