using UnityEngine;

public class AnimationController : Singleton<AnimationController>
{
    [SerializeField] private Animator animator;
    
    public void NotifyUser()
    {
        animator.SetTrigger("Start");
    }
}
