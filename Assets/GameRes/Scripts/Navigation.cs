﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {
    private NavMeshAgent pet;
    public Transform target;
	// Use this for initialization
	void Start () {
        pet = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        pet.SetDestination(target.position);
	}
}
