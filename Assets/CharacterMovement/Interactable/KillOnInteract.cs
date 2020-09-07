using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnInteract : MonoBehaviour, I_Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(Transform trans)
    {
        Destroy(gameObject);
    }
}
