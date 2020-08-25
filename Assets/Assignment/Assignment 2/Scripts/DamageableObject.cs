using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private Slider myHealthSlider;
    [SerializeField] private WallBase myStats = null;
    [SerializeField] private AudioSource audioSource;
    public WallBase myInstance { get; set; } = null;

    void Start()
    {
        myInstance = Instantiate(myStats);
        myInstance.AssingMyGameObject(gameObject);
        myInstance.MyAudio = audioSource;
        myHealthSlider.maxValue = myInstance.Health;
        myHealthSlider.value = myHealthSlider.maxValue;
    }

    public void OnDamageTaken(float damage)
    {
        myInstance.TakeDamage(damage);
        myHealthSlider.value -= damage;
    }
}
