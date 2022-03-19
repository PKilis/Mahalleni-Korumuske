using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saglik_Kutusu_Olustur : MonoBehaviour
{
    public List<GameObject> saglik_Kutu_Points = new List<GameObject>();
    public GameObject saglik_Kutusu;

    public static bool saglik_Kutusu_Varmi = false;
    public float saglik_Kutu_Suresi;
    void Start()
    {
        StartCoroutine(saglik_Kutusu_Yap());
    }
    IEnumerator saglik_Kutusu_Yap()
    {
        while (true)
        {
            yield return null;

            if (!saglik_Kutusu_Varmi)
            {
                yield return new WaitForSeconds(saglik_Kutu_Suresi);

                int randomSayim = Random.Range(0, 5);
                Instantiate(saglik_Kutusu, saglik_Kutu_Points[randomSayim].transform.position, saglik_Kutu_Points[randomSayim].transform.rotation);
                saglik_Kutusu_Varmi = true;
            }
        }
    }
}
