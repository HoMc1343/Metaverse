using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsJumping = Animator.StringToHash("IsJump");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > 0.1f;
        animator.SetBool(IsMoving, isMoving);
    }

    public void Jump(bool isJumping)
    {
        animator.SetBool(IsJumping, isJumping);
    }
}
