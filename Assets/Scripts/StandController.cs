using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class StandController : MonoBehaviour
{
    private float soruA;
    private float soruB;
    private float islem;
    private char isaret;
    private float sonuc;
    private float[] cevaplar;
    private float dogrucevap;
    public TextMeshPro soru;
    public TextMeshPro secenekA;
    public TextMeshPro secenekB;
    public TextMeshPro secenekC;
    public TextMeshPro secenekD;

    private void Awake()
    {
        cevaplar = new float[4];
        islem = Random.Range(1, 4);
        dogrucevap = Random.Range(0, 3);
    }
    private void Start()
    {
        switch (islem)
        {
            case 1:
                ToplamaGereklilik();
                sonuc = soruA + soruB;
                isaret = '+';
                break;
            case 2:
                CikarmaGereklilik();
                sonuc = soruA - soruB;
                isaret = '-';
                break;
            case 3:
                CarpmaGereklilik();
                sonuc = soruA * soruB;
                isaret = '*';
                break;
            case 4:
                BolmeGereklilik();
                sonuc = soruA / soruB;
                isaret = '/';
                break;
        }
        for (int i = 0; i < cevaplar.Length; i++)
        {
            if (i != dogrucevap)
            {
                cevaplar[i] = sonuc + Random.Range(-10, 10);
            }
            else
            {
                cevaplar[i] = sonuc;
            }
        }
        soru.text = soruA + " " + isaret + " " + soruB + " = ?";
        secenekA.text = cevaplar[0].ToString();
        secenekB.text = cevaplar[1].ToString();
        secenekC.text = cevaplar[2].ToString();
        secenekD.text = cevaplar[3].ToString();
    }

    void ToplamaGereklilik() 
    {
        soruA = Random.Range(1, 100);
        soruB = Random.Range(1, 100);
    }

    void CikarmaGereklilik()
    {
        do 
        {
            soruA = Random.Range(1, 100);
            soruB = Random.Range(1, 100);
        } while (soruB > soruA);
    }

    void CarpmaGereklilik()
    {
        soruA = Random.Range(1, 10);
        soruB = Random.Range(1, 10);
    }

    void BolmeGereklilik()
    {
        do
        {
            soruA = Random.Range(1, 100);
            soruB = Random.Range(1, 100);
        } while (soruA % soruB != 0);
    }

    public void CevapKontrol(GameObject cevap, GameObject player)
    {
        for (int i = 0; i < cevaplar.Length; i++)
        {
            if (cevap.GetComponentInChildren<TextMeshPro>().text == cevaplar[i].ToString())
            {
                if (cevaplar[i] != sonuc)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    player.GetComponent<PlayerController>().skor += 1;
                    if(PlayerPrefs.GetInt("HighScore") < player.GetComponent<PlayerController>().skor)
                    {
                        PlayerPrefs.SetInt("HighScore", player.GetComponent<PlayerController>().skor);
                    }
                }
            }
        }
    }
}
