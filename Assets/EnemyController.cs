using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public bool vertical;
    public bool horizontal;
    Rigidbody2D rigidbody2D;
    public ParticleSystem smokeEffect;
    public GameObject HitEffectPrefab;
    public AudioClip FixedClip;
    private float timerValue = 2f;
    private float remainingTimer;
    private float direction;
    private int directionNumber;
    bool broken = true;
    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()

         //1 é vertical, 2 é horizontal
         //o codigo começa com esse direction no 0, e por algum motivo o personagem vai pra cima
         //
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        horizontal = true;
        vertical = false;
        direction = 1;
        remainingTimer = timerValue;

    }

    // Update is called once per frame
    void Update()
    {

            remainingTimer -= Time.deltaTime;
            if (remainingTimer < 0)
            {
                direction = -direction;
                remainingTimer = timerValue;
                directionNumber = Random.Range(1, 3);
            }

        if ( directionNumber == 1 )
        {
            horizontal = true;
            vertical = false;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        else if ( directionNumber == 2)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            vertical = true;
            horizontal = false;
        }
        
    }



    private void FixedUpdate()
    {

        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            position.y = position.y + Speed * direction * Time.deltaTime;

        }
        else if ( horizontal)
        {
            position.x = position.x + Speed * direction * Time.deltaTime;

        }

        //position = position + new Vector2(2,4) * Speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);

    }



    void OnTriggerEnter2D(Collider2D other)
    {

        character_controller player = other.gameObject.GetComponent<character_controller>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }

    }


    public void Fix()
    {
        Debug.Log("enemy fixed");
        animator.SetTrigger("Fixed");
        broken = false;
        rigidbody2D.simulated = false;
        audioSource.Stop();
        smokeEffect.Stop();
        PlaySound(FixedClip);
        GameObject hitParticle = Instantiate(HitEffectPrefab, rigidbody2D.position, Quaternion.identity) ;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
