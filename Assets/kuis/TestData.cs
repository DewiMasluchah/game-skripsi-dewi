using UnityEngine;

[CreateAssetMenu(fileName = "Test Data", menuName = "Scriptable Object/Player/TestData")]
public class TestData : ScriptableObject
{
    public float[] nilai = new float[4];
    public float[] durasi = new float[4];
    public float[] kesulitan = new float [4];

    public int[] rightAnswer = new int[4];
    public int[] wrongAnswer = new int[4];

    public int getRightAns(bool[] answers)
    {
        int total = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i] == true)
            {
                total += 1;
            }
        }
        return total;
    }

    public void reset()
    {
        nilai = new float[4];
        durasi = new float[4];
        kesulitan = new float[4];
        rightAnswer = new int[4];
        wrongAnswer = new int[4];
    }
    
}