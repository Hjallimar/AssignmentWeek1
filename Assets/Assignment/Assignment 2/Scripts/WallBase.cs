using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WallStats")]
public class WallBase : ScriptableObject
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private AudioClip damageSound = null;
    [SerializeField] private float damageReduction = 0.2f;
    public float Health { get; set; } = 0;
    public float DamageReduction { get; set; } = 0.2f;
    private GameObject myGameObject = null;
    public AudioSource MyAudio { get; set; }  = null;

    void OnEnable()
    {
        Health = maxHealth;
        DamageReduction = damageReduction;
    }

    public void AssingMyGameObject(GameObject gameObject)
    {
        myGameObject = gameObject;
    }

    public void TakeDamage(float damage)
    {        
        Health -= damage;

        if (Health > 0)
        {
            if(MyAudio != null && damageSound != null)
            {
                MyAudio.PlayOneShot(damageSound);
            }
        }
        else
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(myGameObject);
    }
}
