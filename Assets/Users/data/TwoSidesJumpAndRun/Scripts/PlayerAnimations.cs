using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour 
{
    public Animator animator;       
    private Player player;  

    void Start()
    {
       
        player = GetComponent<Player>();
    }

	void Update () 
    {
       
        animator.SetBool("Jump", PlayerInput.Jump);
        animator.SetBool("Grounded", player.IsGrounded());
        animator.SetBool("Dead", player.IsDead());
	}
}
