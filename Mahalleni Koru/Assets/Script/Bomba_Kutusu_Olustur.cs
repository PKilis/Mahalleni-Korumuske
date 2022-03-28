using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Kutusu_Olustur : MonoBehaviour
{
    public List<GameObject> bomba_Kutu_Points = new List<GameObject>();
    public GameObject bomba_Kutusu;

    public static bool bomba_Kutusu_Varmi = false;
    public float bomba_Kutu_Suresi;
    void Start()
    {
        StartCoroutine(bomba_Kutusu_Yap());
    }
    IEnumerator bomba_Kutusu_Yap()
    {
        while (true)
        {
            yield return null;

            if (!bomba_Kutusu_Varmi)
            {
                yield return new WaitForSeconds(bomba_Kutu_Suresi);

                int randomSayim = Random.Range(0, 5);
                Instantiate(bomba_Kutusu, bomba_Kutu_Points[randomSayim].transform.position, bomba_Kutu_Points[randomSayim].transform.rotation);
                bomba_Kutusu_Varmi = true;
            }
        }
    }
}
