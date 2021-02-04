using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPScontroller : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    public Camera zoomedCam;
    private float horizontal, vertical, mousex, mousey;
    private bool fire1, fire2, space, esc;
    public float speed;
    public float sensitivity;
    public float sensitivityADS;
    private float sensitivityCont;
    private float clampAngle;
    private float horizontalAngle;
    private bool isGrounded;
    private float t = 0;
    private float t2 = 0;
    private float recoil;
    public Transform ZoomedIn;
    public Transform ZoomedOut;
    public Transform gun;
    public GameObject scopeUI;
    public float TimeADS;
    private bool isShooting, hasRecoil;
    private GameObject shotEnemy;
    public AudioSource sniperShot;
    public AudioSource ding;

    public Text scoreText;
    public int score = 0;

    public Canvas uiMenu;
    
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        sensitivityCont = sensitivity;
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        GetInput();
        //TestJump();
        TestAds();
        TestFire();

        TestOpenUI();

        RotatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void TestJump()
    {
        if (isGrounded == true && space)
        {
            Jump();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.CompareTag("Ground"))
        {
        isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void TestFire()
    {
        if (fire1 && isShooting == false && hasRecoil == false)
        {
            Shoot();
            isShooting = true;
            hasRecoil = true;
        }

        if(hasRecoil == true && t2 < 1)
        {
            t2 += 1 * Time.deltaTime;
        }
        else if(t2 >= 1)
        {
            t2 = 0;
            hasRecoil = false;
        }

        ManageRecoil();
    }

    void ManageRecoil()
    {
        if (isShooting == true && recoil > -9)
        {
            recoil = Mathf.Lerp(recoil, -10, 0.9f);
        }
        else
        {
            isShooting = false;
        }

        recoil = Mathf.Lerp(recoil, 0, 0.02f);
    }

    void Jump()
    {
        rb.velocity = new Vector3(0, 12, 0);
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        fire1 = Input.GetButtonDown("Fire1");
        fire2 = Input.GetButton("Fire2");
        space = Input.GetKey(KeyCode.Space);
        mousex = Input.GetAxis("Mouse X");
        mousey = Input.GetAxis("Mouse Y");
        esc = Input.GetKeyDown(KeyCode.Escape);
    }

    void RotatePlayer()
    {
        horizontalAngle = horizontalAngle + -mousex * sensitivityCont;
        rb.transform.localRotation = Quaternion.Euler(rb.transform.localRotation.x, -horizontalAngle, rb.transform.localRotation.z);

        clampAngle = Mathf.Clamp(clampAngle + -mousey * sensitivityCont, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(clampAngle + recoil, rb.transform.localRotation.y, rb.transform.localRotation.z);
    }

    void MovePlayer()
    {
        rb.transform.Translate(Vector3.forward * vertical * speed);
        rb.transform.Translate(Vector3.right * horizontal * speed);
    }

    void TestAds()
    {
        if(fire2)
        {
            
            gun.position = Vector3.Lerp(gun.position, ZoomedIn.position, TimeADS);
            
            if(t > 0.20F)
            {
                sensitivityCont = sensitivityADS;
                scopeUI.SetActive(true);
                zoomedCam.enabled = true;
                cam.enabled = false;
            }
            else
            {
                t += 1 * Time.deltaTime;
            }

        }
        else if(gun.position != ZoomedOut.position)
        {
            sensitivityCont = sensitivity;
            t = 0;
            scopeUI.SetActive(false);
            zoomedCam.enabled = false;
            cam.enabled = true;
            gun.position = Vector3.Lerp(gun.position, ZoomedOut.position, TimeADS);
        }
    }

    void Shoot()
    {
        sniperShot.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
             {
             if(hit.transform.CompareTag("Enemy"))
             {
                shotEnemy = hit.transform.gameObject;
                Enemy enem = shotEnemy.GetComponent<Enemy>();
                enem.Die();
                ding.Play();
                score += 1;
                scoreText.text = "Score: " + score.ToString();
             }
        }
    }
    void TestOpenUI()
    {
        if(esc)
        {
            uiMenu.enabled = !uiMenu.enabled;   
        }

        if (uiMenu.enabled == false)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
