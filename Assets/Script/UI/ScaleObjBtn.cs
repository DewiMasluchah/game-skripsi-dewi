using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObjBtn : MonoBehaviour, IButtonClick
{
    internal enum ScaleType { Up, Down, }

    [SerializeField] ScaleType scaleType;

    [SerializeField] float scaleMultiplier = .001f;

    Vector3 scaleAdder = Vector3.one;

    private void Start()
    {
        scaleAdder *= scaleMultiplier;
    }

    void ScaleObj()
    {
        for (int i = 0; i < PlanetUIHandler.Planets.Count; i++)
        {
            if (i == PlanetUIHandler.CurrentPlanetIndex)
            {
                switch (scaleType)
                {
                    case ScaleType.Up:
                        PlanetUIHandler.Planets[i].planetObj.transform.localScale += scaleAdder;
                        break;
                    case ScaleType.Down:
                        PlanetUIHandler.Planets[i].planetObj.transform.localScale -= (PlanetUIHandler.Planets[i].planetObj.transform.localScale == Vector3.one) ? Vector3.zero : scaleAdder;
                        break;
                }
            }
        }
    }

    public void OnClick()
    {
        ScaleObj();
    }
}
