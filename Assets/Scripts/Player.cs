using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public CharacterController CC;
    public Vector3 ElevatedOffset { get { return elevatedOffset; } }

    [SerializeField] private Animator ani;
    [SerializeField] private Vector3 elevatedOffset;

    public float MoveSpeed;
    public float Gravity = -9.8f;
    public float JumpSpeed;

    public float verticalSpeed;

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        // X/Z movement
        float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        float forwardMovement = Input.GetAxis("Vertical") * -MoveSpeed * Time.deltaTime;

        if(Mathf.Abs(sideMovement) > 0.01f || Mathf.Abs(forwardMovement) > 0.01f)
        {
            Vector3 move = new Vector3(sideMovement, 0, -forwardMovement).normalized;

            Quaternion rot = Quaternion.LookRotation(move, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 15f);

            Debug.DrawLine(transform.position, transform.position + move * 5, Color.red);
        }
        

        if (sideMovement != 0 || forwardMovement != 0)
        {
            ani.SetInteger("state", 1);
        }

        else
        {
            ani.SetInteger("state", 0);
        }


        //movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);
        movement = new Vector3(-sideMovement, 0, forwardMovement);

        if (CC.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = JumpSpeed;
                ani.SetInteger("state", 2);

            }
        }
        verticalSpeed += (Gravity * Time.deltaTime);
        movement += (transform.up * verticalSpeed * Time.deltaTime);

        CC.Move(movement);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Green Cube"))
        {
            LoadScene();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + elevatedOffset, 0.2f);
    }
}
