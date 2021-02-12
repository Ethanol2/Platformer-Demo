using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingUFOs : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] UFOs;

    // Use this for initialization
    void Start()
    {
        for (int k = 0; k < UFOs.Length; k++)
        {
            UFOs[k].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= 240f)
        {
            for (int k = 0; k < UFOs.Length; k++)
            {
                UFOs[k].SetActive(true);
            }

            this.transform.position += new Vector3(1f, 0f, 0f);
        }
    }
}
