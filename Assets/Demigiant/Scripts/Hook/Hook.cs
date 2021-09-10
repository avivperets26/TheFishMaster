using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Hook : MonoBehaviour
{
    public Transform hookedTransform;

    private Camera mainCamera;

    private int length, strength, fishCount;

    private Collider2D coll;

    private bool canMove = false;

    //List<fish>

    private Tweener cameraTween;

    void Awake()
    {
        mainCamera = Camera.main;
        coll = GetComponent<Collider2D>();
        //List<fish>
    }
    
    void Update()
    {
        if (canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;

            position.x = vector.x;

            transform.position = position;

        }
    }

    public void StartFishing()
    {
        length = -50; //IdleManager
        strength = 3; //IdleManager
        fishCount = 0;
        float time = (-length) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y <= -1)
                transform.SetParent(mainCamera.transform);
        }
        ).OnComplete(delegate {
            coll.enabled = true;
            cameraTween = mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate
            {
                if (mainCamera.transform.position.y >= -25f)
                    StopFishing();
            });
        });

        //Screen (Game)
        coll.enabled = false;
        canMove = true;
        //Clear

    }

    void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if(mainCamera.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 6;
            coll.enabled = true;
            int num = 0;
            //clearing out the hook from the fishes
            //IdleManager totalgain = num
            //Screenmanager
        }
        );

        
    }
}
