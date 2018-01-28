using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public GameObject astroidObject;
    public Transform spawnTransformMin;
    public Transform spawnTransformMax;

    public EventSystem eventSystem;

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Get.gameHasStarted)
        {
            OnTap(Input.mousePosition);
        }
    }

    public void OnTap(Vector3 screenLocation)
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = screenLocation;

        Vector3 spawnPosition = new Vector3(Random.Range(spawnTransformMin.position.x, spawnTransformMax.position.x), spawnTransformMin.position.y);

        Vector2 destination = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        //destination.Normalize();

        //Vector2 finalDestination = destination;



        GameObject newAstroid = Instantiate(astroidObject, spawnPosition, Quaternion.identity);
        AstroidBase astroidScript = newAstroid.GetComponent<AstroidBase>();
        astroidScript.Init(destination);
    }
}
