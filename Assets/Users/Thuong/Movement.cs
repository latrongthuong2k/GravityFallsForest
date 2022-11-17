using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Speeds {SuperLow = 0, Slow = 1, Normal = 2, Fast = 3, Faster = 4, Fastest = 5 };
public enum Gamemodes { Cube = 0, Ship = 1 };
public enum Gravity { Upright = 1, Upsidedown = -1 };

public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Gamemodes CurrentGamemode;
    public float force = 26.6581f;
    //                     -1  0   1   2    3    4
    float[] SpeedValues = {3, 9f, 10f, 13f, 15f, 17f };

    [SerializeField]private Transform GroundCheckTransform;
    [SerializeField]private Transform TopCheckTransForm;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    [SerializeField]private Transform Sprite;
    private float ResetSpeedTimer = 3f;
    private bool resetBool;
    SoundManager soundManager;
    Rigidbody2D rb;
    public AudioSource _audio;

    void Start()
    {
        _audio.GetComponent<AudioSource>();
        ResetSpeedTimer = 3f;
        resetBool = false;
        rb = GetComponent<Rigidbody2D>();
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
            if (Input.GetMouseButton(0))
            {
                _audio.Play();
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
            if (Input.GetMouseButton(0))
            {
                _audio.Play();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Longblock")
        {
            Destroy(gameObject);
        }
        else if(collision.collider.tag == "ShortBlock")
        {
            CurrentSpeed = Speeds.SuperLow;
            resetBool = true;
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
