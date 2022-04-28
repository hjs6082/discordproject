using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEndScirpt : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject clearPanel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SecretDoor"))
        {
            clearPanel.SetActive(true);
            player.GetComponent<Suntail.PlayerController>().enabled = false;
        }
    }
}
