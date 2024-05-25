using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Soal : MonoBehaviour
{
    private const int V = 35;
    [SerializeField] Topsis topsis;
    public TextAsset assetSoal;

    public string[] soal;

    public string[,] soalBag;


    int indexSoal;
    int maxSoal;
    bool ambilSoal;


    [SerializeField] Sprite[] pathSoal;
    Image spriteSoal;
    [SerializeField] GameObject imgSoal;
    int idSoal;
    char kunciJ;

    // public Text txtSoal, txtOpsiA, txtOpsiB, txtOpsiC, txtOpsiD;

    bool isHasil;
    [SerializeField] private float durasi;
    public float durasiPenilaian;

    int jwbBenar, jwbSalah;
    float nilai;

    public float durasiPenilaianTotal;

    public GameObject panel;
    public GameObject imgPenilaian, imgHasil, hasilPanel;
    public Text txtHasil;
    public TextMeshProUGUI txtHasilMateri1, txtHasilMateri2;

    [SerializeField] TestData testData;
    public float[] scores;
    public float[] times;
    public float[] difficulties;
    public int difficulty;
    public int bab;

    public string[] tempSoal;

    public bool isTopsis;

    // Start is called before the first frame update

    private void Awake()
    {
        spriteSoal = imgSoal.GetComponent<Image>();
        soal = assetSoal.ToString().Split('#');
    }
    void Start()
    {

        scores = new float[4];
        times = new float[4];
        difficulties = new float[4];
        difficulty = new int();

        soalBag = new string[soal.Length, 9];
        maxSoal = soal.Length;
        OlahSoal();

        ambilSoal = true;
        TampilkanSoal();

        isTopsis = false;
    }

//olah soal
    private void OlahSoal()
    {
        for (int i = 0; i < soal.Length; i++)
        {
            tempSoal = soal[i].Split('+');
            for (int j = 0; j < tempSoal.Length; j++)
            {
                soalBag[i, j] = tempSoal[j];
            }
        }
    }

//menampilkan soal
    private void TampilkanSoal()
    {
        if (indexSoal < maxSoal)
        {
            if (ambilSoal)
            {

                idSoal = int.Parse(soalBag[indexSoal, 8]);
                kunciJ = soalBag[indexSoal, 5][0];
                imgSoal.SetActive(true);
                spriteSoal.sprite = pathSoal[idSoal];
                difficulty = int.Parse(soalBag[indexSoal, 6]);
                bab = int.Parse(soalBag[indexSoal, 7]);
                ambilSoal = false;
            }
        }
    }

//
    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);

        if (indexSoal == maxSoal - 1)
        {
            isHasil = true;
            isTopsis = true;
        }
        else
        {
            indexSoal++;
            ambilSoal = true;
        }

     /*   panel.SetActive(true);*/
    }

    private float HitungNilai()
    {
        return nilai = (float)jwbBenar / maxSoal * 100;
    }
//ngeck jawaban -> scriptable
    public GameObject BenarObj;
    public GameObject SalahObj;
    private void CheckJawaban(char huruf)
    {
        durasiPenilaian = durasi;
        StartCoroutine(penilaian());
        durasi = 0f;

        if (huruf.Equals(kunciJ))
        {
            BenarObj.SetActive(true);
            SalahObj.SetActive(false);
            jwbBenar++;

            testData.nilai[bab] += 5;
            testData.durasi[bab] += durasiPenilaian;
            testData.kesulitan[bab] += difficulty;
            durasiPenilaianTotal += durasiPenilaian;
        }
        else
        {
            SalahObj.SetActive(true);
            BenarObj.SetActive(false);
            jwbSalah++;
            testData.nilai[bab] += 1;
            durasiPenilaianTotal += durasiPenilaian;
            testData.durasi[bab] += durasiPenilaian;
            testData.kesulitan[bab] += 5;
        }

    }
    IEnumerator penilaian()
    {
        hasilPanel.SetActive(true);
        imgPenilaian.SetActive(true);
        yield return new WaitForSeconds(1f);
        imgPenilaian.SetActive(false);
        hasilPanel.SetActive(false);
    }
    IEnumerator hasilAkhir()
    {
        yield return new WaitUntil(()=>!imgPenilaian.activeSelf);
        hasilPanel.SetActive(true);
        imgHasil.SetActive(true);
    }
    // Update is called once per frame
    //
    void Update()
    {
        if (panel.activeSelf)
        {
            if (isHasil)
            {
                StartCoroutine(hasilAkhir());

                durasiPenilaian = 0;
                if (isTopsis)
                {
                    isTopsis = false;
                    string hasilRanking = topsis.TopsisStart();
                    print("Jumlah benar : " + jwbBenar + "\nJumlah Salah : " + jwbSalah + "\n\nNilai : " + HitungNilai());

                    txtHasilMateri1.SetText("Berdasarkan hasil tes yang kamu lakukan, kamu perlu mempelajari");

                    txtHasilMateri2.SetText(hasilRanking);
                }


            }
            else
            {
                if (!hasilPanel.activeSelf)
                {
                    panel.SetActive(true);


                    TampilkanSoal();
                    durasi += Time.deltaTime;

                }

            }
        }

    }
}