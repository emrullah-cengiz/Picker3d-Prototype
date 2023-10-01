using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed = .1f; // Karakterin hareket hızı
    private bool isDragging = false; // Dokunma sürüklendiğinde bu değeri kullanacağız
    private float dragStartPosition; // Dokunma sürükleme başlangıç pozisyonu

    public float smoothTime = 1;

    float currentVelocity;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition.x;
        }

        if (isDragging) 
        {
            float currentMouseX = Input.mousePosition.x;

            float xDelta = currentMouseX - dragStartPosition;

            //xDelta = Mathf.SmoothDamp(xDelta, 0, ref currentVelocity, smoothTime);

            Vector3 newPosition = transform.position + new Vector3(xDelta, 0, 0) * moveSpeed * Time.deltaTime;

            transform.position = newPosition;

            dragStartPosition = currentMouseX;

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }

    //public float clampVal = 1;
    //public float smoothTime = 1;

    //public float maxSpeed = 1;

    //public float firstPos;

    //public float currVelocity;

    //Rigidbody rb;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        firstPos = Input.mousePosition.x;
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        float deltaX = Input.mousePosition.x - firstPos;

    //        float moveStep = Mathf.SmoothDamp(0, deltaX, ref currVelocity, smoothTime, maxSpeed, Time.deltaTime);

    //        moveStep = Mathf.Clamp(transform.position.x + moveStep, -clampVal, clampVal);

    //        var newPos = new Vector3(moveStep, 0, 0);
    //        transform.position = newPos;
    //    }
    //}
}
