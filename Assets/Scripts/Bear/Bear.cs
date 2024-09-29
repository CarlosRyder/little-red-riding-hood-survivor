using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Bear : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 5f; 
    public float patrolRadius = 10f;  
    public float patrolDelay = 3f;    
    private bool isPatrolling = false;
    private Animator animator;
    private int attackRange = 5;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

       
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateAnimation();

        
        Vector2 direction = (agent.velocity).normalized;

      
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                Debug.Log("attack");
                //implement attack logic
            }
            else{
                agent.SetDestination(player.position);
                isPatrolling = false;
            }

        }
        else if (!isPatrolling)
        {
           
            isPatrolling = true;
            StartCoroutine(Patrol());
        }

        
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    
    void UpdateAnimation()
    {
        
        float speed = agent.velocity.magnitude;

        
        if (speed > 0.1f)  
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

   
    IEnumerator Patrol()
    {
        while (isPatrolling)
        {
            
            yield return new WaitForSeconds(5f);

           
            Vector3 randomPoint = GetRandomPoint(transform.position, patrolRadius);
            agent.SetDestination(randomPoint);
        }
    }

    
    Vector3 GetRandomPoint(Vector3 center, float range)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            return hit.position; 
        }

        return center; 
    }
}
