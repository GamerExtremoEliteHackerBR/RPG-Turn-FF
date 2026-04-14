using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroMovement : MonoBehaviour
{

    float moveSpeed = 10f;

    Vector3 curPos, lastPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameManager.instance.nextSpawnPoint != "")
        {
            GameObject spawnPoint = GameObject.Find(GameManager.instance.nextSpawnPoint);
            //transform.position = GameManager.instance.nextHeroPosition;
            transform.position = spawnPoint.transform.position;

            GameManager.instance.nextSpawnPoint = "";
        }
        else if(GameManager.instance.lastHeroPosition != Vector3.zero)
        {
            transform.position = GameManager.instance.lastHeroPosition;
            GameManager.instance.lastHeroPosition = Vector3.zero;

        }
        //transform.position = GameManager.instance.nextHeroPosition;
    }

    void FixedUpdate()
    {
        //float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveX, 0.0f, moveZ) * moveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        //GetComponent<Rigidbody>().AddForce(movement * moveSpeed);
        //GetComponent<Rigidbody>().velocity = movement * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody>().linearVelocity = movement * moveSpeed;// * Time.deltaTime;
        //GetComponent<Rigidbody>().velocity = movement * moveSpeed;// * Time.deltaTime;

        curPos = transform.position;
        if(curPos == lastPos)
        {
            GameManager.instance.isWalking = false;
        }
        else
        {
            GameManager.instance.isWalking = true;
        }
        lastPos = curPos;

    }



    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "teleport")
        {
            CollisionHandler col = other.GetComponent<CollisionHandler>();
            GameManager.instance.nextSpawnPoint = col.spawnPointName;
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }

        /*
        //if(other.tag == "EnterTown")
        //{
        //    CollisionHandler col = other.GetComponent<CollisionHandler>();
        //    //GameManager.instance.nextHeroPosition = other.GetComponent<CollisionHandler>().spawnPoint.transform.position;
        //    //GameManager.instance.nextHeroPosition = col.spawnPoint.transform.position;
        //    GameManager.instance.sceneToLoad = col.sceneToLoad;
        //    GameManager.instance.LoadNextScene();
        //}
        //if(other.tag == "LeaveTown")
        //{
        //    CollisionHandler col = other.GetComponent<CollisionHandler>();
        //    //GameManager.instance.nextHeroPosition = other.GetComponent<CollisionHandler>().spawnPoint.transform.position;
        //    //GameManager.instance.nextHeroPosition = col.spawnPoint.transform.position;
        //    GameManager.instance.sceneToLoad = col.sceneToLoad;
        //    GameManager.instance.LoadNextScene();
        //}
        */
        /*
        if (other.tag == "region1")
        {
            GameManager.instance.curRegion = 0;
        }
        //if (other.tag == "region2")
        //{
        //    GameManager.instance.curRegion = 1;
        //}
        */

        if (other.tag == "EncouterZone")
        {
            RegionData region = other.GetComponent<RegionData>();
            GameManager.instance.curRegion = region;
        }
    }

    
    void OnTriggerStay(Collider other)
    {
        //if(other.tag == "region1" || other.tag == "region2")
        if (other.tag == "EncouterZone")
        {
            GameManager.instance.canGetEncounter = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.tag == "region1" || other.tag == "region2")
        if (other.tag == "EncouterZone")
        {
            GameManager.instance.canGetEncounter = false;
        }

    }


}
