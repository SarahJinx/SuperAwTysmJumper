using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerController.GetCurrentState())
        {
            case PlayerState.IDLE:
                animator.SetBool("isRunning", false);
                break;
            case PlayerState.RUNNING:
                animator.SetBool("isRunning", true);
                break;
            case PlayerState.JUMPING:
                break;
        }

        //if (playerController.GetDirection().x < 0)
        //{

        //}
        //else if (playerController.GetDirection().x > 0)
        //{

        //}
    }
}
