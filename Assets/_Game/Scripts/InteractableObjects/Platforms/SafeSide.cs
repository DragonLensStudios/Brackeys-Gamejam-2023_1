using UnityEngine;

public class SafeSide : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            animator.SetBool("Weight", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            animator.SetBool("Weight", false);
        }
    }
}
