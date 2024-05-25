using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
   public TestData testData;
    private void OnApplicationQuit()
    {
        testData.reset();
    }
}
