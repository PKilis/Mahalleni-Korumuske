using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class keles : MonoBehaviour
{
    Animator animatorum;


    [Header("Ayarlar")]
    public bool atesEdebilirmi;
    float iceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public float menzil;


    [Header("Sesler")]
    public AudioSource AtesSesi;
    public AudioSource SarjorSesi;
    public AudioSource MermiBitmeSesi;
    public AudioSource MermiAlmaSesi;

    [Header("Efektler")]
    public ParticleSystem AtesEfekti;
    public ParticleSystem MermiIzi;
    public ParticleSystem KanEfekti;

    [Header("Diðerleri")]
    public Camera benimCam;

    [Header("Silah Ayarlarý")]
    public int ToplamMermi;
    public int SarjorKapasite;
    public int KalanMermi;
    public TextMeshProUGUI toplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;
    public GameObject kovanCikis_noktasi;
    public GameObject kovanObjesi;
    public bool kovanCiksinMi;

    void Start()
    {
        KalanMermi = SarjorKapasite;
        sarjorDoldurmaTeknik("NormalYaz");

        animatorum = GetComponent<Animator>();
    }

    void Update()
    {
        kovanCiksinMi = true;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikligi && KalanMermi != 0)
            {
                AtesEt();
                iceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSikligi;
            }
            if (KalanMermi == 0)
            {
                MermiBitmeSesi.Play();
            }


        }
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(reloadYap());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            mermiAl();
        }

    }

    void mermiAl()
    {
        RaycastHit hit;
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, 4))
        {
            if (hit.transform.gameObject.CompareTag("Mermi"))
            {
                mermiKaydet(hit.transform.gameObject.GetComponent<mermiKutusu>().Olusan_silahin_Turu, hit.transform.gameObject.GetComponent<mermiKutusu>().Olusan_mermi_sayisi);
                Mermi_Kutusu_Olustur.mermi_Kutusu_Varmi = false;
                Destroy(hit.transform.parent.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
            mermiKaydet(other.gameObject.GetComponent<mermiKutusu>().Olusan_silahin_Turu, other.gameObject.GetComponent<mermiKutusu>().Olusan_mermi_sayisi);
            Mermi_Kutusu_Olustur.mermi_Kutusu_Varmi = false;
            Destroy(other.transform.parent.gameObject);

        }
    }
    void mermiKaydet(string silahTuru, int mermiSayisi)
    {
        MermiAlmaSesi.Play();
        switch (silahTuru)
        {
            case "Keles":
                ToplamMermi += mermiSayisi;
                sarjorDoldurmaTeknik("NormalYaz");
                break;

            case "Magnum":
                ToplamMermi += mermiSayisi;
                break;

            case "Pompali":
                ToplamMermi += mermiSayisi;
                break;

            case "sniper":
                ToplamMermi += mermiSayisi;
                break;
        }
    }
    void SarjorSes()
    {
        SarjorSesi.Play();
    }
    IEnumerator reloadYap()
    {
        if (KalanMermi < SarjorKapasite)
        {
            animatorum.Play("sarjorDegis");
        }
        yield return new WaitForSeconds(1f);
        if (KalanMermi < SarjorKapasite)
        {
            if (KalanMermi != 0 && ToplamMermi != 0)
            {
                sarjorDoldurmaTeknik("MermiVar");
            }
            else
            {
                sarjorDoldurmaTeknik("MermiYok");
            }

        }
    }
    void sarjorDoldurmaTeknik(string tur)
    {
        switch (tur)
        {
            case "MermiVar":
                if (ToplamMermi <= SarjorKapasite)
                {
                    int distaKalanMermi = ToplamMermi + KalanMermi;
                    if (distaKalanMermi > SarjorKapasite)
                    {
                        KalanMermi = SarjorKapasite;
                        ToplamMermi = distaKalanMermi - SarjorKapasite;
                    }
                    else
                    {
                        ToplamMermi += KalanMermi;
                        ToplamMermi = 0;
                    }
                }
                else
                {
                    ToplamMermi -= SarjorKapasite - KalanMermi;
                    KalanMermi = SarjorKapasite;

                }


                toplamMermi_Text.text = ToplamMermi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "MermiYok":
                if (ToplamMermi <= SarjorKapasite)
                {
                    KalanMermi = ToplamMermi;
                    ToplamMermi = 0;
                }
                else
                {
                    ToplamMermi -= SarjorKapasite;
                    KalanMermi = SarjorKapasite;
                }


                toplamMermi_Text.text = ToplamMermi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "NormalYaz":
                toplamMermi_Text.text = ToplamMermi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;
        }
    }
    void AtesEt()
    {
        atesEtmeTeknikleri();
        RaycastHit hit;
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(KanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("DevrilebilirObje"))
            {
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 60f);
                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }

    }

    void atesEtmeTeknikleri()
    {
        if (kovanCiksinMi)
        {
            GameObject obje = Instantiate(kovanObjesi, kovanCikis_noktasi.transform.position, kovanCikis_noktasi.transform.rotation);
            Rigidbody rb = obje.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(-10, 1, 0) * 60);
        }

        AtesSesi.Play();
        AtesEfekti.Play();
        animatorum.Play("atesEt");
        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();
    }

}
