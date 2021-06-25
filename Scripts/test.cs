using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int[] input = new int[4];
    public int[] output = new int[4];
    public int temp;

    private void Start()
    {

        input = new int[4] { 1, 2, 3, 4 };
        Debug.Log("Input > " + input[0] + ", " + input[1] + ", " + input[2] + ", " + input[3]);

        for (int i = 0; i < input.Length; i++)
        {
            int j = Random.Range(i, input.Length);
            temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
        output = new int[4] { input[0], input[1], input[2], input[3] };
        Debug.Log("Output > " + output[0] + ", " + output[1] + ", " + output[2] + ", " + output[3]);
    }

}
