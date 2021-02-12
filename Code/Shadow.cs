using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject Player;

    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distFromGround = 0f;
        Vector3 offset = new Vector3(0f, 0f, 1f);
        if (Physics.Raycast(Player.transform.position + offset, Vector3.down, out hit))
        {
            distFromGround = hit.distance;
        }

        this.transform.position = new Vector3(
            Player.transform.position.x - 0.9f, 
            Player.transform.position.y - distFromGround,
            Player.transform.position.z);

        this.transform.localScale = new Vector3(
            0.69f * (distFromGround / 3f), 
            0.69f * (distFromGround / 3f), 
            0.69f * (distFromGround / 3f));
    }
}
