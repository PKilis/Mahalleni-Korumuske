using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameKontrolcu : MonoBehaviour
{
    float health = 100;

    [Header("Silah Ayarlarý")]
    public GameObject[] silahlar;
    public AudioSource silahDegisimSesi;
    public GameObject bomba;
    public GameObject bombaPoint;
    public Camera benimCam;
    [Header("Dusman Ayarlarý")]
    public GameObject[] dusmanlar;
    public GameObject[] cikisNoktalar;
    public GameObject[] hedefNoktalar;
    public TextMeshProUGUI kalanDusman_Text;
    public int baslangic_Dusman_Sayisi;
    public int dusmanCikmaSuresi;
    [Header("Saðlýk Ayarlarý")]
    public Image healthBar;
    [Header("Diðer Ayarlarý")]
    public GameObject gameOverCanvas;
    public GameObject kazandinCanvas;


    public static int kalan_Dusman_Sayisi;


    void Start()
    {
        PlayerPrefs.SetInt("Taramali_Mermi", 500);
        PlayerPrefs.SetInt("Magnum_Mermi", 300);
        kalanDusman_Text.text = baslangic_Dusman_Sayisi.ToString();
        kalan_Dusman_Sayisi = baslangic_Dusman_Sayisi;
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
        else if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject objem = Instantiate(bomba, bombaPoint.transform.position, bombaPoint.transform.rotation);
            Rigidbody rb = objem.GetComponent<Rigidbody>();
            Vector3 acimiz = Quaternion.AngleAxis(90, benimCam.transform.forward) * benimCam.transform.forward;
            rb.AddForce(acimiz * 250);
        }
    }
    IEnumerator DusmanCikar()
    {
        while (true)
        {
            yield return new WaitForSeconds(dusmanCikmaSuresi);

            if (baslangic_Dusman_Sayisi != 0)
            {
                int dusman = Random.Range(0, 4);
                int cikisYeri = Random.Range(0, 2);
                int hedefNoktasi = Random.Range(0, 2);

                GameObject obje = Instantiate(dusmanlar[dusman], cikisNoktalar[cikisYeri].transform.position, Quaternion.identity);
                obje.GetComponent<Dusman>().HedefBelirle(hedefNoktalar[hedefNoktasi]);
                baslangic_Dusman_Sayisi--;
            }

        }
    }
    public void Dusman_Sayisi_Guncelle()
    {
        kalan_Dusman_Sayisi--;
        kalanDusman_Text.text = kalan_Dusman_Sayisi.ToString();
        if (kalan_Dusman_Sayisi <= 0)
        {
            kalanDusman_Text.text = "0";
            kazandinCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void DarbeAl(float darbeGucu)
    {
        health -= darbeGucu;
        healthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            GameOver();
        }
    }
    public void SaglikDoldur()
    {
        health = 100;
        healthBar.fillAmount = health / 100;
    }

    private void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;

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
    public void BastanBasla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
