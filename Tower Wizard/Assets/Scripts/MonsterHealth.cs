using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    private GameObject onDeathEffect;
    public int FireballDamage = 1;
    public int Spell2Damage = 1;
    public int StartHealth = 2;
    private int CurrentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Load the prefab from Resources
        onDeathEffect = Resources.Load<GameObject>("VFX_FloatUp");

        if (onDeathEffect == null)
        {
            Debug.LogError("Prefab not found! Check the Resources folder path.");
        }
    }
    void Start()
    {

        CurrentHealth = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Fireball"))
        {
            // Destroy the collectible
            DamageMonster(FireballDamage);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Spell2"))
        {
            // Destroy the collectible
            CurrentHealth -= Spell2Damage;
            DamageMonster(Spell2Damage);
            Destroy(other.gameObject);
        }

    }

    public void DamageMonster(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(onDeathEffect, transform.position, transform.rotation);
        }
    }
}
