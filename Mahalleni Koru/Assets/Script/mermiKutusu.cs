using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mermiKutusu : MonoBehaviour
{
    string[] silah = { "Magnum", "Keles" };
    int[] mermiSayisi = { 20, 60, 30, 50, 100 };
    public string Olusan_silahin_Turu;
    public int Olusan_mermi_sayisi;

    public List<Sprite> silah_Resimleri = new List<Sprite>();
    public Image silahin_Resmi;

    void Start()
    {
        int gelenRandom = Random.Range(0, silah.Length);
        Olusan_silahin_Turu = silah[gelenRandom];
        Olusan_mermi_sayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length)];

        silahin_Resmi.sprite = silah_Resimleri[gelenRandom];
    }


}
