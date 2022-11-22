using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TrainPiece : MonoBehaviour
{
    public GameObject original;
    public GameObject piece;
    public int pieceNum;
    public bool done = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == piece)
        {
            original.SetActive(true);
            piece.SetActive(false);
            //Destroy(gameObject);
            done = true;
        }
    }
}
 