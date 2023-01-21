using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atis : MonoBehaviour
{
    public Rigidbody2D rb;
    public float shot = 0.12f;
    public bool ispressed = false;
    public GameObject hook;
    public Rigidbody2D hookk;
    public float maxDragDistance = 5f;

    public GameObject rock;
    public int RockHealth = 20;
    public GameObject Wood;

    void Update()
    {
        if (ispressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hookk.position) > maxDragDistance)
            {
                rb.position = hookk.position + (mousePos - hookk.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        ispressed = true;
        rb.isKinematic = true;
    }
    private void OnMouseUp()
    {
        ispressed = false;
        rb.isKinematic = false;
        StartCoroutine(releaseTime());
        GetComponent<Atis>().enabled = false;
    }

    IEnumerator releaseTime()
    {
        yield return new WaitForSeconds(shot);
        GetComponent<SpringJoint2D>().enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            if (collision.relativeVelocity.magnitude > 30)
            {
                RockHealth = RockHealth - 10;
                Debug.Log(RockHealth);
                if (RockHealth == 0)
                {
                   Destroy(rock);
                }
            }

        }
        if (collision.gameObject.CompareTag("Wood"))
        {
            if (collision.relativeVelocity.magnitude >20)
            {
                Destroy(Wood);
            }
        }
    }
}
