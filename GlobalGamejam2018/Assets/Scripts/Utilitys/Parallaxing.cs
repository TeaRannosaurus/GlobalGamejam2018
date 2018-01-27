using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackGround
{
    public Transform background;
    public float parallaxScale;
}

public class Parallaxing : MonoBehaviour
{
    public BackGround[] backGrounds;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cam.position;

        for (int i = 0; i < backGrounds.Length; i++)
        {
            //backGrounds[i].parallaxScale = backGrounds[i].background.position.z;
        }
    }

    private void Update()
    {
        foreach (BackGround backGround in backGrounds)
        {
            float parallax = (previousCamPos.x - cam.position.x) * backGround.parallaxScale;
            float backgroundTargetPosX = backGround.background.position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backGround.background.position.y, backGround.background.position.z);
            backGround.background.position = Vector3.Lerp(backGround.background.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;

    }
}
