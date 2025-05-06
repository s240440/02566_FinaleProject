using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public ParticleSystem brazierParticleSystem;
    
    private void Start()
    {
        // Turn off particles 
        if (brazierParticleSystem != null)
        {
            brazierParticleSystem.Stop();
        }
    }
    
    // Activate brazier particles
    public void ActivateBrazier(float duration)
    {
        if (brazierParticleSystem != null)
        {
            StartCoroutine(PlayBrazierEffect(duration));
        }
        else
        {
            Debug.LogWarning($"Spawn point {name} should be assigned a brazier Particle System");
        }
    }
    
    private IEnumerator PlayBrazierEffect(float duration)
    {
        brazierParticleSystem.Play();
        yield return new WaitForSeconds(duration);
        brazierParticleSystem.Stop();
    }
}
