using UnityEngine;
using DG.Tweening;
using System;

public class Hook : MonoBehaviour
{
    public Transform hookedTransform;

    private Camera mainCamera;
    private Collider2D col;

    private int length;
    private int strength;
    private int fishCount;

    private bool canMove = false;

    // List<Fish>

    private Tweener cameraTween;

    private void Awake()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();

        // List<Fish>
    }

    private void Update()
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
        length = -50; // IdleManager
        strength = 3; // IdleManager
        fishCount = 0;
        float time = (-length) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y <= -11)
            {
                transform.SetParent(mainCamera.transform);
            }
        }).OnComplete(delegate
        {
            col.enabled = true;
            cameraTween = mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate
            {
                if (mainCamera.transform.position.y >= -25f)
                {
                    StopFishing();
                }
            });
        });

        // Screen(GAME)
        col.enabled = false;
        canMove = true;
        // Clear
    }

    private void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 6;
            col.enabled = true;
            int num = 0;
            // Clearing out the hook from the fishes
            // IdleManager totalGain = num
            // ScreenManager EndScreen
        });
    }
}
