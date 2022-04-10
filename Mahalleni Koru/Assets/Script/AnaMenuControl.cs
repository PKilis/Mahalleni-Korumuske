using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenuControl : MonoBehaviour
{

    public GameObject loadingPanel;
    public Slider loadingSlider;
    public GameObject tercihPaneli;

    public void OyunaBasla()
    {
        StartCoroutine(SahneyiYukle());
    }

    IEnumerator SahneyiYukle()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        SceneManager.LoadScene(1);
        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float yuklemeSeviyesi = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = yuklemeSeviyesi;
            yield return null;
        }

    }

    public void OyundanCik()
    {
        tercihPaneli.SetActive(true);
    }

    public void Tercih(string tercihDegeri)
    {
        if (tercihDegeri == "Evet")
        {
            Debug.Log("Çýktý");
            Application.Quit();
        }
        else
        {
            tercihPaneli.SetActive(false);
        }
    }
}
