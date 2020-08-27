using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullletBehaviour : MonoBehaviour
{
    public float Speed { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, (Speed * Time.deltaTime)))
        {
            MovingTarget target = hit.collider.gameObject.GetComponent<MovingTarget>();
            if ( target != null)
            {
                target.ImHit();
                onHit(); 
            }
        }
        else
            transform.position += transform.forward.normalized * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        MovingTarget target = other.gameObject.GetComponent<MovingTarget>();
        if (target != null)
        {
            target.ImHit();
        }
        
        onHit();
    }

    private void onHit()
    {
        Destroy(gameObject);
    }
}
