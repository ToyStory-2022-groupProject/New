using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TrainPiece : MonoBehaviour
{
    public GameObject original;
    public GameObject piece;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == piece)
        {
            original.SetActive(true);
            Destroy(piece);
            Destroy(gameObject);
        }
    }
}
