using UnityEngine;

[CreateAssetMenu(fileName = "Test Data", menuName = "Scriptable Object/Player/TestData")]
public class TestData : ScriptableObject
{
    public float[] nilai = new float[4];
    public float[] durasi = new float[4];
    public float[] kesulitan = new float [4];

    public void reset()
    {
        nilai = new float[4];
        durasi = new float[4];
        kesulitan = new float[4];
    }
}