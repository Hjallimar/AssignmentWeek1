using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private Slider myHealthSlider;
    [SerializeField] private WallBase myStats = null;
    private WallBase myInstance = null;
    // Start is called before the first frame update
    void Start()
    {
        myInstance = Instantiate(myStats);
        myInstance.AssingMyGameObject(gameObject);
        myHealthSlider.maxValue = myInstance.health;
        myHealthSlider.value = myHealthSlider.maxValue;
    }

    public void OnDamageTaken(float damage)
    {
        myInstance.TakeDamage(damage);
        myHealthSlider.value -= damage;
    }

}
