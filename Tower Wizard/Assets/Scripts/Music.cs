using UnityEngine;

public class Music : MonoBehaviour



{
    public AudioClip WinMusic;
    public AudioClip LoseMusic;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        // Get and cache reference to the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Optional: check if AudioSource component exists
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on " + gameObject.name);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayWinSound()
    {
        if (WinMusic != null && audioSource != null)
            audioSource.Stop();
            audioSource.clip = WinMusic;
            audioSource.Play();
    }
    public void PlayLoseSound()
    {
        if (LoseMusic != null && audioSource != null)
            audioSource.Stop();
            audioSource.clip = LoseMusic;
            audioSource.Play();            
    }

}
