using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jump,land,walk;
    private PlayerMovement player;
    private bool isFalling;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<PlayerMovement>();
        isFalling = false;
    }

    void Update()
    {
        if(!PauseMenu.IsGamePaused)
        {
            if(Input.GetKey("space") && player.IsGrounded())
            {
                audioSource.Stop();
                audioSource.clip = jump;
                audioSource.Play();
                isFalling = true;
            }
            else if(isFalling && player.IsGrounded())
            {
                audioSource.Stop();
                audioSource.clip = land;
                audioSource.Play();
                isFalling = false;
            }
            else if(player.isWalking && player.IsGrounded() && 
            (audioSource.clip != walk || !audioSource.isPlaying) && (audioSource.clip != land || !audioSource.isPlaying))
            {
                audioSource.Stop();
                audioSource.clip = walk;
                audioSource.Play();    
            }
            else if(!player.isWalking && player.IsGrounded() && audioSource.clip != land || 
            (audioSource.clip == walk && !player.IsGrounded()))
            {
                if(audioSource.clip == walk && !player.IsGrounded())
                    isFalling = true;
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
    }
}
