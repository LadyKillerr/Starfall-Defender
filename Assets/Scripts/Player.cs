﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] float paddingTop = 1f;
    [SerializeField] float paddingBottom = 1f;
    [SerializeField] float paddingLeft = 1f;
    [SerializeField] float paddingRight = 1f;

    Vector2 moveInput;

    // Vector toạ độ để giới hạn sự di chuyển của người chơi
    Vector2 minBounds;
    Vector2 maxBounds;

    Rigidbody2D playerRigidbody;

    Shooter shooter;
    private void Awake()
    {
        shooter = FindObjectOfType<Shooter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        // kiểm tra giới hạn ngay từ frame đầu tiên 
        InitializationBounds();



        // lấy ra 
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    void InitializationBounds()
    {
        Camera mainCamera = Camera.main;

        // mặc dù phải truyền vào Vector3 nhưng nếu ta truyền vào Vector2 thì default toạ độ trục Z sẽ =  0 luôn
        // cta bỏ qua trục Z vì đây là game 2D, nên chỉ cần Vector2 là đủ

        // điểm dưới trái của ViewPort (0, 0)
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        // điểm trên phải của ViewPort (1,1)
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();


    }

    void Fly()
    {
        ;

        // di chuyển player = velocity của rigidbody
        playerRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);

        // tạo một biến và gán vị trí của player vào đó để kiểm tra xem có vượt ra ngoài ViewPort không
        Vector2 newPos = new Vector2();

        // kiểm tra giá trị toạ độ trục X - Hàm Clamp là sẽ loại bỏ tất cả giá trị truyền vào vượt quá 
        newPos.x = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);

        // kiểm tra giá trị toạ độ trục Y
        newPos.y = Mathf.Clamp(transform.position.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;




    }

    void OnFire(InputValue value)
    {
        //if (value.isPressed)
        //{
        //    shooter.isFiring = true;
        //}
        //else
        //{
        //    shooter.isFiring = false;
        //}

        shooter.isFiring = value.isPressed;
    }
}
