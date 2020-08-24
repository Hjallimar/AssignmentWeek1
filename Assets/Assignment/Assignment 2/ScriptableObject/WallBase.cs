using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "WallStats")]
public class WallBase : ScriptableObject
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private AudioClip damageSound = null;
    [SerializeField] private AudioClip deathSound = null;
    private float health = 0;
    private GameObject myGameObject = null;
    private AudioSource myAudio = null;
    private bool dead = false;

    void OnEnable()
    {
        myAudio = new AudioSource();
        health = maxHealth;
    }

    public void AssingMyGameObject(GameObject gameObject)
    {
        myGameObject = gameObject;
    }

    public void TakeDamage(float damage)
    {        
        health -= damage;
        
        if(health > 0)
        {
            if(myAudio != null && damageSound != null)
            {
                myAudio.PlayOneShot(damageSound);
            }
        }
        else
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        dead = true;
        if (myAudio != null && damageSound != null)
        {
            myAudio.clip = deathSound;
            myAudio.Play();
            Destroy(myGameObject, myAudio.clip.length);
        }
        myGameObject.GetComponent<Collider>().enabled = false;
        Destroy(myGameObject);
    }

}
