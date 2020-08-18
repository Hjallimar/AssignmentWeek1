using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    Vector3 startPos;
    float speed;
    int direction = 1;
    float length;
    Vector3 newTransX;

    bool dead = false;
    float deadTimer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        speed = Random.Range(5, 10);
        length = Random.Range(5, 10);
        newTransX = new Vector3(startPos.x, startPos.y, startPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer <= 0)
            {
                dead = false;
                GetComponent<Renderer>().enabled = true;
                GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            newTransX.x += direction * (speed * Time.deltaTime);
            transform.position = newTransX;
            if (newTransX.x > startPos.x + length || newTransX.x <= startPos.x - length)
                direction *= -1;
        }
    }

    public void ImHit()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        dead = true;
        deadTimer = Random.Range(0.5f ,3);
    }
}
