using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    Transform cameraRotation;
    
    public float movespeed;
    public GameObject Robot;
    public GameObject rayStart;
    public GameObject obj;
    private ParticleSystem dashEffect;

    private CharacterController characon;
    private RaycastHit hit;
    private Animator animator;
    private Animator Enemy;
    private Vector3 moveDistance = Vector3.zero;
    private Collider handCollider;
    private Collider footCollider;
    private Collider strongAttackCollider;
    private bool DamageSet = true;
    private bool hitPoint = false;
    private bool strongAttack = false;
    private Image target;
    private AudioSource runSE;
    private AudioSource jumpSE;

    public ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(Robot, new Vector3(-2, 0, 0), Quaternion.identity);
        characon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        handCollider = GameObject.Find("Character1_LeftHand").GetComponent<BoxCollider>();
        footCollider = GameObject.Find("Character1_RightToeBase").GetComponent<BoxCollider>();
        strongAttackCollider = GameObject.Find("Character1_Reference").GetComponent<BoxCollider>();
        target = GameObject.Find("target").GetComponent<Image>();
        AudioSource[] audioSource = GetComponents<AudioSource>();
        runSE = audioSource[0];
        jumpSE = audioSource[1];
        dashEffect = GameObject.Find("DashEffect").GetComponent<ParticleSystem>();
        dashEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (characon.isGrounded)
        {
            moveDistance.y = 0.0f;
            if (Input.GetKey(KeyCode.W))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    movespeed = 10;
                    moveDistance.z = movespeed;
                }
                if (Input.GetKey(KeyCode.Z))
                {
                    movespeed = 0;
                    moveDistance.z = movespeed;
                }
            }
            else
            {
                moveDistance.z = 0;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator.SetBool("Jab", true);
                    handCollider.enabled = true;
                    Invoke("ColliderReset", 0.5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jab"))
                {
                    animator.SetBool("Hikick", true);
                    footCollider.enabled = true;
                    Invoke("ColliderReset", 1.2f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hikick"))
                {
                    animator.SetBool("Spinkick", true);
                    footCollider.enabled = true;
                    Invoke("ColliderReset", 0.8f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    animator.SetBool("Jab", true);
                    handCollider.enabled = true;
                    Invoke("ColliderReset", 1.0f);
                }
            }
            cameraRotation = obj.GetComponent<Transform>();
            transform.rotation = Quaternion.Euler(0.0f, cameraRotation.rotation.y * 190, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Land")))
        {
            characon.enabled = false;
            target.enabled = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            if (hitPoint)
                hit.point = this.transform.position;

            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
            {

            }
            if (Input.GetMouseButtonDown(0))
            {
                jumpSE.PlayOneShot(jumpSE.clip);
                dashEffect.Play();
                strongAttack = true;
                hitPoint = false;
                Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward);
                Physics.Raycast(ray,out hit, 30.0f);
            }
        }
        else
        {
            characon.enabled = true;
            target.enabled = false;
            hitPoint = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if(hit.point == new Vector3(0.0f, 0.0f, 0.0f))
        {
            dashEffect.Stop();
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, hit.point, 1.0f);
        
        Vector3 glovalDistance = transform.TransformDirection(moveDistance);
        characon.Move(glovalDistance * Time.deltaTime);
        animator.SetBool("Run", moveDistance.z > 0);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            runSE.Play();
    }

    void ColliderReset()
    {
        handCollider.enabled = false;
        footCollider.enabled = false;
        strongAttackCollider.enabled = false;
        strongAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack" && DamageSet)
        {
            animator.SetBool("DamageDown", true);
            DamageSet = false;
            StartCoroutine("DamageSettrue");
        }
        if (other.gameObject.tag == "Wall1" && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Land", true);
            dashEffect.Stop();
        }
        if (other.gameObject.tag == "Wall2" && strongAttack)
        {
            animator.SetBool("Land", true);
            strongAttackCollider.enabled = true;
            Invoke("ColliderReset", 0.5f);
            dashEffect.Stop();
        }
        if (other.gameObject.tag == "enemy" && strongAttack)
        {
            animator.SetBool("Land", true);
            strongAttackCollider.enabled = true;
            Invoke("ColliderReset", 0.5f);
            dashEffect.Stop();
        }
    }

    IEnumerator DamageSettrue()
    {
        yield return new WaitForSeconds(1.0f);
        DamageSet = true;
        yield break;
    }
}