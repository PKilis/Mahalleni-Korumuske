using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi_Kutusu_Olustur : MonoBehaviour
{
    public List<GameObject> mermi_Kutu_Points = new List<GameObject>();
    public GameObject mermi_Kutusu;

    public static bool mermi_Kutusu_Varmi = false;
    public float mermi_Kutu_Suresi;
    void Start()
    {
        StartCoroutine(Mermi_Kutusu_Yap());
    }
    IEnumerator Mermi_Kutusu_Yap()
    {
        while (true)
        {
            yield return null;

            if (!mermi_Kutusu_Varmi)
            {
                yield return new WaitForSeconds(mermi_Kutu_Suresi);

                int randomSayim = Random.Range(0, 2);
                Instantiate(mermi_Kutusu, mermi_Kutu_Points[randomSayim].transform.position, mermi_Kutu_Points[randomSayim].transform.rotation);
                mermi_Kutusu_Varmi = true;
            }
        }
    }
}
