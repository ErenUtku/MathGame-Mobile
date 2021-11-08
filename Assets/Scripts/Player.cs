using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("Character State")]

    public PlayerState curState;
    public AudioClip HitSound;
    

    [Header("Character Values")]      
    
    public float moveSpeed;                 
    public float flyingSpeed;               
    public bool grounded;                   
    public float stunDuration;             
    private float stunStartTime;

    [Header("Character Components")]

    public Rigidbody2D rig;                 
    public Animator anim;                   
    public ParticleSystem jetpackParticle;

    void Update()
    {
        if (IsGrounded() == true)
        {
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("NO");
        }
    }
    void FixedUpdate()
    {
        grounded = IsGrounded();
        CheckInputs();
        if (curState == PlayerState.Stunned)
        {
            if (Time.time - stunStartTime >= stunDuration)
            {
                curState = PlayerState.Idle;
            }
        }
    }

    void Move()
    {
        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        rig.velocity = new Vector2(move * moveSpeed, rig.velocity.y);
    }
    void Fly()
    {
        rig.AddForce(Vector2.up * flyingSpeed, ForceMode2D.Impulse);  
        if (!jetpackParticle.isPlaying)
            jetpackParticle.Play();
    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.3f);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Floor"))
            {
                return true;
            }
           
        }
        return false;
    }

    void SetState()
    { 
            //no-stun
        if (curState != PlayerState.Stunned)
        {
            //idle
            if (rig.velocity.magnitude == 0 && grounded)
                curState = PlayerState.Idle;
            // walking
            if (rig.velocity.x != 0 && grounded)
                curState = PlayerState.Walking;
            // flying
            if (rig.velocity.magnitude != 0 && !grounded)
                curState = PlayerState.Flying;
            
        }
        anim.SetInteger("State", (int)curState);
    }

            // stunning
    public void Stun()
    {
        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, 0.5f);
        curState = PlayerState.Stunned;
        rig.velocity = Vector2.down * 3;
        stunStartTime = Time.time;
        jetpackParticle.Stop();
    }

    void CheckInputs()
    {
        if (curState != PlayerState.Stunned)
        {
            // movement

            Move();

            // flying

            if (CrossPlatformInputManager.GetButton("Jump"))
            {
                Fly();
            }
            else
                jetpackParticle.Stop();
        }
        SetState();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Obstacle
        if (curState != PlayerState.Stunned)
        {
            if (col.CompareTag("Obstacle"))
            {
                Stun();
            }
        }
    }
    
}
public enum PlayerState
{
    Idle,       //     0     //      
    Walking,    //     1     //
    Flying,     //     2     //
    Stunned     //     3     //
}
