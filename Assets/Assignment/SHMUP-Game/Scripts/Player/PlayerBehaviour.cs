using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private MovementBehaviour myMovement = null;



    //private Weapon currentWeapon;
    //private List<Weapon> myWeapons = new List<Weapon>();
    Coroutine playerFire;
    private Vector3 direction;
    // Start is called before the first frame update
    void Awake()
    {
        myMovement = GetComponent<MovementBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerFire = StartCoroutine(FireMyWeapon());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(playerFire);
        }

        myMovement.MovePlayer(direction);
    }

    IEnumerator FireMyWeapon()
    {
        while (true)
        {
            Debug.Log("Hell yea, I'm blasting");
        }
    }



}
