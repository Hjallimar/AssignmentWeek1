using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playermesh = null;
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform firePoint = null;
    private Vector3 direction = Vector3.zero;
    private Vector3 lookAt = Vector3.zero;
    private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            lookAt = hit.point;
        }
        lookAt.y = playermesh.position.y;
        playermesh.LookAt(lookAt);

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        Move();
    }

    void Move()
    {
        playermesh.position += direction.normalized * speed * Time.deltaTime;
    }

    void Fire()
    {
        GameObject firedBullet = Instantiate(bullet);
        firedBullet.transform.position = firePoint.position;
        firedBullet.transform.rotation = firePoint.rotation;

        firedBullet.transform.SetParent(null);
        firedBullet.GetComponent<BullletBehaviour>().Speed = 50;
    }
}
