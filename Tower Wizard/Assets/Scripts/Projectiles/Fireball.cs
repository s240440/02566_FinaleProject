using UnityEngine;
public class Fireball : MonoBehaviour
{
    public float speed = 10f;       // Adjust this to control projectile speed
    public float lifetime = 5f;     // Projectile lifetime before destruction

    void Start()
    {
        // Destroy projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move forward continuously
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
    // Optional: damage something if we hit it 
    private void OnTriggerEnter(Collider other)
    { // Damage logic here, then destroy 
        Destroy(gameObject);
    }
}