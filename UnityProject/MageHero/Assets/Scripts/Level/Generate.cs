using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate : MonoBehaviour
{

    private static int mapWidht = 18;
    private static int mapHeight = 32;

    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private int[,] map = new int[mapWidht, mapHeight];


    void Start()
    {

        for (int i = 0; i < mapWidht; i++)
        {

            for (int l = 0; l < mapHeight; l++)
            {

                map[i, l] = Random.Range(0, backgrounds.Length);

            }

        }



        for (int i = 0; i < mapWidht; i++)
        {

            for (int l = 0; l < mapHeight; l++)
            {

                Instantiate(backgrounds[map[i, l]], new Vector3(i * 0.2f, 0, l * 0.2f), Quaternion.identity);

            }

        }

    }


}
