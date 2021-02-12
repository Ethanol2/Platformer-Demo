using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilLight : MonoBehaviour
{
    public GameObject Player;
    public GameObject Light;
    public GameObject cylinder;

    public bool chasePlayer = false;
    public bool blockPlayer = false;

    // Use this for initialization
    void Start()
    {
        Light.SetActive(false);
        cylinder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= 135f)
        {
            chasePlayer = true;
            Light.SetActive(true);
            cylinder.SetActive(true);
        }

        if (Player.transform.position.x >= 240f && !blockPlayer)
        {
            blockPlayer = true;
            Light.SetActive(true);
            cylinder.SetActive(true);
            this.transform.position = new Vector3(245f, -3.57144f, 0f);
        }

        if (this.transform.position.x >= 170f && !blockPlayer)
        {
            chasePlayer = false;
            Light.SetActive(false);
            cylinder.SetActive(false);
        }

        if (chasePlayer || blockPlayer)
        {
            this.transform.position += new Vector3(0.17f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("End");
        }

        if (this.transform.position.x + 2f >= Player.transform.position.x
            &&
            this.transform.position.x -2f <= Player.transform.position.x
            &&
            (chasePlayer || blockPlayer))
        {
            SceneManager.LoadScene("Died");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject == Player)
        {
            SceneManager.LoadScene("Died");
        }
    }
}
