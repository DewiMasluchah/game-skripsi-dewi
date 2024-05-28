using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Topsis : MonoBehaviour
{

    public TestData test_data;
    MenuAR[] alternatif;
    [SerializeField] string[] category;
    [SerializeField] string[] category_kriteria;
    [SerializeField] float[] bobot_kriteria;
    public float[,] norm_dm;
    public float[,] dm;
    public float[] sum_dm;
    public float[] norm_div;
    public float[,] weighted_norm;
    public float[] ips;
    public float[] ins;
    public float[] sum_ips;
    public float[] sum_ins;
    public float[] dp;
    public float[] dn;
    public float pref;

//perankingan
    public Dictionary<MenuAR, float> pref_v;

    public int maxMateri;

    /*output*/
    string rank1;

    public class MenuAR
    {
        public string menuAR;
        public string materi;

        public MenuAR(string menuAR, string materi)
        {
            this.menuAR = menuAR;
            this.materi = materi;
        }
    }
    public string TopsisStart()
    {
        maxMateri = 4;
        alternatif = new MenuAR[maxMateri];
        alternatif[0] = new MenuAR("Menu AR 1", "PLANET");
        alternatif[1] = new MenuAR("Menu AR 2", "ASTEROID");
        alternatif[2] = new MenuAR("Menu AR 3", "bumi dan satelitnya");
        alternatif[3] = new MenuAR("Menu AR 4", "matahari");
        /*
                print(alternatif[0]);*/
       /* dm = new float[4, 3] { { 25, 57.88873f, 50 }, { 21, 39.443528f, 25 }, { 13, 47.08836f, 75 }, { 5, 67.55346f, 30 } };*/
        dm = new float[alternatif.Length, category.Length];
        norm_dm = new float[alternatif.Length, category_kriteria.Length];
        weighted_norm = new float[alternatif.Length, category_kriteria.Length];
        sum_dm = new float[category.Length];
        norm_div = new float[category.Length];
        ips = new float[category.Length];
        ins = new float[category.Length];
        sum_ips = new float[alternatif.Length];
        sum_ins = new float[alternatif.Length];
        dp = new float[alternatif.Length];
        dn = new float[alternatif.Length];
        pref_v = new Dictionary<MenuAR, float>();





        return topsis();
    }
    void inputData()
    {
        for (int i = 0; i < alternatif.Length; i++)
        {
            for (int j = 0; j < category.Length; j++)
            {
                switch (j)
                {
                    case 0:
                        dm[i, j] = test_data.nilai[i];/*
                        print("i=" + i + "j=" + j + "=" + dm[i, j]);*/
                        break;

                    case 1:

                        dm[i, j] = test_data.durasi[i];/*
                        print("i=" + i + "j=" + j + "=" + dm[i, j]);*/
                        break;

                    case 2:

                        dm[i, j] = test_data.kesulitan[i];/*
                        print("i=" + i + "j=" + j + "=" + dm[i, j]);*/
                        break;

                    default:
                        break;
                }
            }
        }
        print("Matrix Keputusan:");
        for (int i = 0; i < dm.GetLength(0); i++)
        {
            int j = 0;
            string str = "";
            do
            {
                str += " | " + dm[i, j++] + " |";
            } while (j < dm.GetLength(1));
            print(str);
        }
    }

    // Update is called once per frame
    string topsis()
    {
        inputData();
        //normalisasi decision matrix
        for (int i = 0; i < alternatif.Length; i++) //kuadrat dalam akar
        {
            for (int j = 0; j < category.Length; j++)
            {
                sum_dm[j] += dm[i, j] * dm[i, j];/*  //sigma
                print("sum dm [" + j + "]= " + sum_dm[j]);*/
            }
        }
        int item = 0;
        foreach (float sum in sum_dm)
        {
            norm_div[item++] = Mathf.Sqrt(sum);  //pembagi tiap kategori dg mengakarkan
        }
        for (int i = 0; i < alternatif.Length; i++) //normalisasi
        {
            for (int j = 0; j < category.Length; j++)
            {
                norm_dm[i, j] = (dm[i, j] / norm_div[j]);
            }
        }

        print("Normalisasi Matrix Keputusan:");
        for (int i = 0; i < norm_dm.GetLength(0); i++)
        {
            int j = 0;
            string str = "";
            do
            {
                str += " | " + norm_dm[i, j++] + " |";
            } while (j < norm_dm.GetLength(1));
            print(str);
        }

        //normalisasi decision matrix terbobot
        for (int i = 0; i < alternatif.Length; i++)
        {
            for (int j = 0; j < category_kriteria.Length; j++)
            {
                weighted_norm[i, j] = norm_dm[i, j] * bobot_kriteria[j];
            }
        }
        print("Normalisasi Matriks Keputusan Terbobot:");
        for (int i = 0; i < weighted_norm.GetLength(0); i++)
        {
            int j = 0;
            string str = "";
            do
            {
                str += " | " + weighted_norm[i, j++] + " |";
            } while (j < weighted_norm.GetLength(1));
            print(str);
        }

        //solusi ideal positif
        float[] temp = new float[alternatif.Length];
        for (int j = 0; j < category.Length; j++)
        {
            for (int k = 0; k < alternatif.Length; k++)
            {
                temp[k] = weighted_norm[k, j];
           /*     print(temp[k]);*/
            }
            if (category_kriteria[j] == "benefit")
            {
                ips[j] = (from float v in temp select v).Max();

            }
            else if (category_kriteria[j] == "cost")
            {
                ips[j] = (from float v in temp select v).Min();
            }
        }
        print("Solusi Ideal Positif:");
        string str1 = "";
        for (int i = 0; i < ips.GetLength(0); i++)
        {
            str1 += " | " + ips[i] + " |";
        }
        print(str1);

        //solusi ideal negatif
        for (int j = 0; j < category.Length; j++)
        {
            temp = new float[alternatif.Length];
            for (int k = 0; k < alternatif.Length; k++)
            {
                temp[k] = weighted_norm[k, j];
/*                print(temp[k]);*/
            }
            if (category_kriteria[j] == "benefit")
            {
                ins[j] = (from float v in temp select v).Min();

            }
            else if (category_kriteria[j] == "cost")
            {
                ins[j] = (from float v in temp select v).Max();
            }
        }

        print("Solusi Ideal Negatif:");
        str1 = "";
        for (int i = 0; i < ins.GetLength(0); i++)
        {
            str1 += " | " + ins[i] + " |";
        }
        print(str1);

        //euclidean distance positif
        for (int i = 0; i < alternatif.Length; i++)
        {
            for (int j = 0; j < category.Length; j++)
            {
                sum_ips[i] += ((ips[j] - weighted_norm[i, j]) * (ips[j] - weighted_norm[i, j]));
            }
            dp[i] = Mathf.Sqrt(sum_ips[i]);
        }

        print("Jarak Alternatif ke Solusi Ideal Positif:");
        str1 = "";
        for (int i = 0; i < dp.GetLength(0); i++)
        {
            str1 += " | " + dp[i] + " |";
        }
        print(str1);

        //euclidean distance negatif
        for (int i = 0; i < alternatif.Length; i++)
        {
            for (int j = 0; j < category.Length; j++)
            {
                sum_ins[i] += ((ins[j] - weighted_norm[i, j]) * (ins[j] - weighted_norm[i, j]));
            }
            dn[i] = Mathf.Sqrt(sum_ins[i]);
        }
        
        print("Jarak Alternatif ke Solusi Ideal Negatif:");
        str1 = "";
        for (int i = 0; i < dn.GetLength(0); i++)
        {
            str1 += " | " + dn[i] + " |";
        }
        print(str1);


        //nilai preferensi
        for (int i = 0; i < alternatif.Length; i++)
        {
            pref = dn[i] / (dn[i] + dp[i]);
            pref_v.Add(alternatif[i], pref);
            print("A" + (i + 1) + "= " + pref);
        }


        //ranking
        var rank_akhir = pref_v.OrderByDescending(v => v.Value).ToList();

        print("Hasil Ranking");
        int rank = 1;
        foreach (var v in rank_akhir)
        {
            print(rank++ + ". " + v.Key.materi + " = " + v.Value);
        }
        rank1 = rank_akhir[0].Key.menuAR;
        return rank_akhir[0].Key.materi;
        /*      MoveScene(rank_akhir[0].Key);*/
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(rank1);
    }
}
