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
    public TextMeshProUGUI saglik_Sayisi_Text;
    public TextMeshProUGUI bomba_Sayisi_Text;
    public AudioSource itemYok;



    public static int kalan_Dusman_Sayisi;


    void Start()
    {
        BaslangicIslemleri();

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
            BombaAt();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (healthBar.fillAmount < 1)
            {
                SaglikDoldur();
            }
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

    void BaslangicIslemleri()
    {

        if (!PlayerPrefs.HasKey("OyunBasladiMi"))
        {
            PlayerPrefs.SetInt("Taramali_Mermi", PlayerPrefs.GetInt("Taramali_Mermi"));
            PlayerPrefs.SetInt("Pompali_Mermi", 50);
            PlayerPrefs.SetInt("Magnum_Mermi", PlayerPrefs.GetInt("Magnum_Mermi"));
            PlayerPrefs.SetInt("Sniper_Mermi", 20);
            PlayerPrefs.SetInt("Saglik_Sayisi", PlayerPrefs.GetInt("Saglik_Sayisi"));
            PlayerPrefs.SetInt("Bomba_Sayisi", PlayerPrefs.GetInt("Bomba_Sayisi"));

            PlayerPrefs.SetInt("OyunBasladiMi", 1);
        }
        kalanDusman_Text.text = baslangic_Dusman_Sayisi.ToString();
        kalanDusman_Text.text = baslangic_Dusman_Sayisi.ToString();


        saglik_Sayisi_Text.text = PlayerPrefs.GetInt("Saglik_Sayisi").ToString();
        bomba_Sayisi_Text.text = PlayerPrefs.GetInt("Bomba_Sayisi").ToString();

        kalan_Dusman_Sayisi = baslangic_Dusman_Sayisi;


        StartCoroutine(DusmanCikar());
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

        if (PlayerPrefs.GetInt("Saglik_Sayisi") != 0)
        {
            health = 100;
            healthBar.fillAmount = health / 100;
            PlayerPrefs.SetInt("Saglik_Sayisi", PlayerPrefs.GetInt("Saglik_Sayisi") - 1);
            saglik_Sayisi_Text.text = PlayerPrefs.GetInt("Saglik_Sayisi").ToString();

        }
        else
        {
            itemYok.Play();
        }
    }
    public void Saglik_Al()
    {
        PlayerPrefs.SetInt("Saglik_Sayisi", PlayerPrefs.GetInt("Saglik_Sayisi") + 1);
        saglik_Sayisi_Text.text = PlayerPrefs.GetInt("Saglik_Sayisi").ToString();
    }
    public void Bomba_Al()
    {
        PlayerPrefs.SetInt("Bomba_Sayisi", PlayerPrefs.GetInt("Bomba_Sayisi") + 1);
        bomba_Sayisi_Text.text = PlayerPrefs.GetInt("Bomba_Sayisi").ToString();

    }
    private void BombaAt()
    {
        if (PlayerPrefs.GetInt("Bomba_Sayisi") != 0)
        {
            GameObject objem = Instantiate(bomba, bombaPoint.transform.position, bombaPoint.transform.rotation);
            Rigidbody rb = objem.GetComponent<Rigidbody>();
            Vector3 acimiz = Quaternion.AngleAxis(90, benimCam.transform.forward) * benimCam.transform.forward;
            rb.AddForce(acimiz * 250);

            PlayerPrefs.SetInt("Bomba_Sayisi", PlayerPrefs.GetInt("Bomba_Sayisi") - 1);
            bomba_Sayisi_Text.text = PlayerPrefs.GetInt("Bomba_Sayisi").ToString();
        }
        else
        {
            itemYok.Play();
        }

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