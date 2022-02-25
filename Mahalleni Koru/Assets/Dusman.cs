using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    private NavMeshAgent ajan;
    public GameObject hedef;
    public float health;
    void Start()
    {
        ajan = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        ajan.SetDestination(hedef.transform.position);
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

    void Oldun()
    {
        Destroy(gameObject);
    }
}
