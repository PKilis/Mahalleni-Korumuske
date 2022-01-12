using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boskovanlar : MonoBehaviour
{
    public AudioSource yereDusmeSesi;
    void Start()
    {
        yereDusmeSesi = GetComponent<AudioSource>();
       // Destroy(gameObject, 4f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Yol"))
        {
            yereDusmeSesi.Play();
        }
    }

    private void OnEnable()
    {
        Destroy(this.gameObject, 5f);
    }
}
