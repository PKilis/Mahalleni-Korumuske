using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float guc = 10f;
    public float menzil = 5f;
    float yukariGuc = 1f;
    public ParticleSystem patlamaEfekt;
    AudioSource patlamaSesi;
    void Start()
    {
        patlamaSesi = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject, .5f);
            Patlama();
        }
    }

    void Patlama()
    {
        Vector3 patlamaPozisyonu = transform.position;
        Instantiate(patlamaEfekt, patlamaPozisyonu, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(patlamaPozisyonu, menzil);
        // gameFactory gizmozuna bak menzilin nereye kadar etki ettiðini görelim.

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit != null && rb)
            {
                if (hit.gameObject.CompareTag("Dusman"))
                {
                    hit.gameObject.GetComponent<Dusman>().Oldun();
                }
                rb.AddExplosionForce(guc, patlamaPozisyonu, menzil, yukariGuc, ForceMode.Impulse);
                patlamaEfekt.Play();
                patlamaSesi.Play();
            }
        }
    }

}
