using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private bool soundTriggered;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        soundTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !soundTriggered)
        {
            audioSource.Play();
            animator.SetBool("FlagTriggered", true);
            soundTriggered = true;
        }
    }
}
