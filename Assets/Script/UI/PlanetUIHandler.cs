using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Planets
{
    public string planetName;
    public GameObject planetObj;
    public Color planetBtnColor;
}

public class PlanetUIHandler : MonoBehaviour, IButtonClick
{
    [SerializeField] List<Planets> planets;

    Image planetBtnImage;
    TextMeshProUGUI planetText;

    int planetIndex = 0;

    public static List<Planets> Planets = new List<Planets>();
    public static int CurrentPlanetIndex = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        planetBtnImage = GetComponent<Image>();
        planetText = GetComponentInChildren<TextMeshProUGUI>();

        CurrentPlanetIndex = planetIndex;

        foreach (var item in planets)
        {
            Planets.Add(item);
        }


        ShowPlanet(planetIndex);
    }

    void ShowPlanet(int index)
    {
        for (int i = 0; i < planets.Count; i++)
        {
            if (i == index)
            {
                planetBtnImage.color = planets[i].planetBtnColor;
                planetText.text = planets[i].planetName;
            }

            planets[i].planetObj.SetActive(i == index);
        }
    }

    void ChangePlanets()
    {
        CurrentPlanetIndex = planetIndex++;

        if (planetIndex >= planets.Count)
            planetIndex = 0;

        ShowPlanet(planetIndex);
    }

    public void OnClick()
    {
        ChangePlanets();
    }
}
