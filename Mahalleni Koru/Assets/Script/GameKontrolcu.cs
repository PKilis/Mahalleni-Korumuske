using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKontrolcu : MonoBehaviour
{
    [Header("Silah Ayarlarý")]
    public GameObject[] silahlar;
    public AudioSource silahDegisimSesi;
    [Header("Dusman Ayarlarý")]
    public GameObject[] dusmanlar;
    public GameObject[] cikisNoktalar;
    public GameObject[] hedefNoktalar;

    void Start()
    {
        if (!PlayerPrefs.HasKey("OyunBasladiMi"))
        {
             PlayerPrefs.SetInt("Taramali_Mermi", 70);

             PlayerPrefs.SetInt("Pompali_Mermi", 50);
             PlayerPrefs.SetInt("Magnum_Mermi", 30);
             PlayerPrefs.SetInt("Sniper_Mermi", 20);

            PlayerPrefs.SetInt("OyunBasladiMi", 1);
        }
        StartCoroutine(DusmanCikar());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SilahDegistir(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SilahDegistir(1);
        }
    }
    IEnumerator DusmanCikar()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            int dusman = Random.Range(0, 5);
            int cikisYeri = Random.Range(0, 2);
            int hedefNoktasi = Random.Range(0, 2);

           GameObject obje = Instantiate(dusmanlar[dusman], cikisNoktalar[cikisYeri].transform.position, Quaternion.identity);
            obje.GetComponent<Dusman>().HedefBelirle(hedefNoktalar[hedefNoktasi]);
        }
    }
    void SilahDegistir(int siraNumarasi)
    {
        foreach (GameObject silah in silahlar)
        {
            silah.SetActive(false);
        }
        silahDegisimSesi.Play();
        silahlar[siraNumarasi].SetActive(true);
    }
}
