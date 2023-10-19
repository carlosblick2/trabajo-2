using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    public new Transform camera;
    public float speed = 4.0f; // Corrected the typo here
    public float gravity = -9.8f;
    public bool isJumping = false;
    public bool Shooting;

    //Items

    public PistolController pistol;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero; // Corrected the typo here
        float movementSpeed = 0;

        ItemsControl();

        if (hor != 0 || ver != 0)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = forward * ver + right * hor;
            movementSpeed = Mathf.Clamp01(direction.magnitude);
            direction.Normalize();

            movement = direction * speed * movementSpeed * Time.deltaTime;

            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        }

        movement.y += gravity * Time.deltaTime; // Corrected the typos here

        characterController.Move(movement);
        animator.SetFloat("Speed" , movementSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Si no estamos saltando, activamos la animación de salto
            animator.SetTrigger("Jump");
            isJumping = true;
        }

        // Disparar
        Shooting = Input.GetKeyDown(KeyCode.Mouse0); //Botton Izquierdo del mouse
    }

    // Llamado desde un evento de animación para indicar el final del salto
    public void OnJumpAnimationFinished()
    {
        isJumping = false;
    }

    public void ItemsControl()
    {
        if (pistol != null)
        {
            if(Shooting)
            {
                pistol.Shoot();
            }
        }
    }
}

