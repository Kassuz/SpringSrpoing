﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

    //public Material[] windows;

    public GameObject[] buildingBlock;

    public List<Vector3> buildingBlockPos = new List<Vector3>();
    public List<Quaternion> buildingBlockRot = new List<Quaternion>();
    public List<Rigidbody> buildingBlockRig = new List<Rigidbody>();
    Rigidbody rb, childRB;
    //Renderer rend;

    void Awake()
    {
        for (int i = 0; i < buildingBlock.Length; i++)
        {
            buildingBlockPos.Add(Vector3.zero);
            buildingBlockRot.Add(Quaternion.identity);
        }

        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < buildingBlock.Length; i++)
        {
            buildingBlock[i] = gameObject.transform.GetChild(i).gameObject;
            //rend = buildingBlock[i].GetComponent<Renderer>();
            //rend.material = windows[Random.Range(0, windows.Length)];
            buildingBlockPos[i] = buildingBlock[i].transform.localPosition;
            buildingBlockRot[i] = buildingBlock[i].transform.rotation;
            buildingBlockRig.Add( buildingBlock[i].GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("sdafsfa");
        for (int i = 0; i < buildingBlock.Length; i++)
        {
            //childRB = buildingBlock[i].GetComponent<Rigidbody>();
            buildingBlockRig[i].isKinematic = false;

            if(i < 10)
                buildingBlockRig[i].AddExplosionForce(1000f, transform.position, 10f);
        } 
    }

    private void OnDisable()
    {
        for (int i = 0; i < buildingBlock.Length; i++)
        {
            //childRB = buildingBlock[i].GetComponent<Rigidbody>();
            buildingBlockRig[i].velocity = Vector3.zero;
            buildingBlockRig[i].isKinematic = true;
            buildingBlock[i].transform.localPosition = buildingBlockPos[i];
            buildingBlock[i].transform.rotation = buildingBlockRot[i];
        }
    }

    private void OnEnable()
    {
        StartCoroutine("DisableBlocks");
    }

    IEnumerator DisableBlocks()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

}
