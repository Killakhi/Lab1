using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    [SerializeField] private Player player;
    

    private Vector3 offset;
    private float maxDIst;

    private void Start()
    {
        offset = player.transform.position - transform.position;
        maxDIst = Vector3.Distance(transform.position, player.transform.position);
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Vector3 rayStart = player.transform.position + new Vector3(0, 1, 0);

        // Hit wall, move to elevatedOffset
        Debug.DrawLine(rayStart, rayStart - offset, Color.magenta);
        if(Physics.Raycast(rayStart, rayStart - offset,out hit, maxDIst))
        {
            Debug.Log(hit.distance);
            transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(hit.point,player.transform.position + player.ElevatedOffset,Mathf.Clamp(2.8f - hit.distance, 0, 2.8f )), Time.deltaTime * 3f);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(80, transform.eulerAngles.y, transform.eulerAngles.z), Time.deltaTime * 2);
        }

        // No wall, move to offset
        else
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position - offset, Time.deltaTime * 7);
            transform.LookAt(player.transform.position + new Vector3(0, 1, 0));
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position + new Vector3(-2, 0, 0), Vector3.up), Time.deltaTime * 4);
        }

        //transform.LookAt(player.transform.position + new Vector3(0,1,0));
        
    }

    
}
