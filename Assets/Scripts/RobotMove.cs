using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    public AudioClip damageSE;
    private AudioSource audiosource;
    private Transform target;
    private Animator animator;
    private Collider KickCollider;
    private Animator Player;
    private int Count;

    private ScoreManager sm;
    public int scoreValue;

    public GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Package04_animChanger").GetComponent<Transform>();
        Player = GameObject.Find("Package04_animChanger").GetComponent<Animator>();
        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        audiosource = this.GetComponent<AudioSource>();
        audiosource.clip = damageSE;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Punch", false);
        Vector3 targetPositon = target.position;
        if (transform.position.y != target.position.y)
        {
            targetPositon = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        if (Vector3.Distance(targetPositon, transform.position) > 2.0f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 1.0f);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                StartCoroutine("Attack");
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        animator.SetBool("Punch", true);
        yield break;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (Count >= 3)
            Invoke("Destroy", 2.0f);

        if (col.gameObject.tag == "Player")
        {
            Count += 1;
            animator.SetBool("Damage1", true);
            audiosource.PlayOneShot(damageSE);
            Instantiate(fire, transform.position, transform.rotation);

            if (Player.GetCurrentAnimatorStateInfo(0).IsName("Hikick"))
            {
                animator.SetBool("Damage2", true);
                Count += 2;
            }
            if (Player.GetCurrentAnimatorStateInfo(0).IsName("Spinkick"))
            {
                animator.SetBool("Damage3", true);
                Count += 3;
            }
        }
        if(col.gameObject.tag == "Attack")
        {
            Instantiate(fire, transform.position, transform.rotation);
            animator.SetBool("Damage3", true);
            Count += 3;
            audiosource.Play();
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
        sm.AddScore(scoreValue);
    }
}
