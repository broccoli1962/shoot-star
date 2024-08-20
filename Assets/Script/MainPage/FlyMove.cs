using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    
    public GameObject bullet;
    public float speed = 3.0f;
    public float CoolDown = 0.2f;

    private float timeCheck = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!SystemManager.state)
        {
            movement();
            Attack();
        }
        timeCheck += Time.deltaTime;
    }

    //움직이기
    public void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical);

        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x > 4.25f)
        {
            Vector3 limit = new Vector3(4.25f, transform.position.y, 0);
            transform.position = limit;
        }
        if (transform.position.x < -4.25f)
        {
            Vector3 limit = new Vector3(-4.25f, transform.position.y, 0);
            transform.position = limit;
        }
        if (transform.position.y < -5f)
        {
            Vector3 limit = new Vector3(transform.position.x, -5f, 0);
            transform.position = limit;
        }
        if (transform.position.y > 4.5f)
        {
            Vector3 limit = new Vector3(transform.position.x, 4.5f, 0);
            transform.position = limit;
        }
    }

    //총알 생성
    private void Attack()
    {
        if (Input.GetKey(KeyCode.Space) && timeCheck >= CoolDown)
        {
            GameObject newBullet = Instantiate(bullet, transform.position + Vector3.forward * 5.0f, transform.rotation);
            timeCheck = 0.0f;
        }
    }
}
