using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fan_Rotation : MonoBehaviour
{

	public GameObject blade;
	
	public float speed=75;
	
	void Update()
	{
		blade.transform.Rotate((new Vector3(0,0,1)) * Time.deltaTime*speed); 
	}
}
