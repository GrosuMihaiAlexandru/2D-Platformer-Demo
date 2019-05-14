using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayPlatform : MonoBehaviour
{
    public TilemapCollider2D platform;
    public bool oneWay = false;

    // Update is called once per frame
    void Update()
    {
        platform.enabled = !oneWay;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cacat");
        oneWay = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("cacat");
        oneWay = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        oneWay = false;
    }
}
