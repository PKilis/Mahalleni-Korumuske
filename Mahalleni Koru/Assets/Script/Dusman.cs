using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    private NavMeshAgent ajan;
    public GameObject hedef;
    public float health;
    public float dusmanDarbeGucu;
    GameObject anaKontrolcum;
    public Animator animatorum;
    AudioSource sesKaynagim;

    void Start()
    {
        animatorum = GetComponent<Animator>();
        sesKaynagim = GetComponent<AudioSource>();
        anaKontrolcum = GameObject.FindWithTag("AnaKontrolcum");
        ajan = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        ajan.SetDestination(hedef.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Korunacak"))
        {
            anaKontrolcum.GetComponent<GameKontrolcu>().DarbeAl(dusmanDarbeGucu);
            Oldun();
        }
    }
    public void HedefBelirle(GameObject obje)
    {
        hedef = obje;
    }

    public void DarbeAl(float darbeGucu)
    {
        health -= darbeGucu;
        if (health <= 0)
        {
            Oldun();
        }
    }

    public void Oldun()
    {
        anaKontrolcum.GetComponent<GameKontrolcu>().Dusman_Sayisi_Guncelle();
        animatorum.SetTrigger("Oldur");
        sesKaynagim.volume = 0f;
        Destroy(gameObject, 5f);
    }
}
