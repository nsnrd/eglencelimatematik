using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float direction;
    public int skor;
    public TextMeshProUGUI skorText;
    public TextMeshProUGUI bestSkorText;

    private void Awake()
    {
        skor = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        skorText.text = "Skor: " + skor.ToString();
        bestSkorText.text = "En Yüksek Skor: " + PlayerPrefs.GetInt("HighScore").ToString();
        if (rb.velocity.y > 7)
            rb.velocity = new Vector3(rb.velocity.x, 7, rb.velocity.z);
        direction = Input.GetAxis("Horizontal");
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.5f, 4.5f), transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(direction * Time.fixedDeltaTime * 400, rb.velocity.y, 750 * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            rb.AddForce(Vector3.up * 250 * Time.fixedDeltaTime * 75);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("A") || other.gameObject.CompareTag("B") 
            || other.gameObject.CompareTag("C") || other.gameObject.CompareTag("D"))
        {
            other.GetComponentInParent<Transform>().gameObject.GetComponentInParent<StandController>().CevapKontrol(other.gameObject, this.gameObject);
        }
    }
}
