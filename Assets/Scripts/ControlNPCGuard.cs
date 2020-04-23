using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPCGuard : MonoBehaviour
{
    public List<GameObject> wayPoints = new List<GameObject>();
    int wayPointIndex = 0;
    [HideInInspector]
    public Animator anim;
    AnimatorStateInfo info;

    bool playerActivated;
    [SerializeField]
    bool canHearPlayer;

    public enum GUARD_TYPE { IDLE = 0, PATROLLER = 1, CHASER = 2, PATTOCHASE = 3, ITEMCOLLECTOR = 4 };
    [SerializeField]
    public GUARD_TYPE guardType;
    [SerializeField]
    public GameObject player;
    [SerializeField] GameObject itemCollector;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (guardType == GUARD_TYPE.IDLE) anim.SetBool("isIdleGuard", true);
        else if (guardType == GUARD_TYPE.PATROLLER) anim.SetBool("isPatrolGuard", true);
        else if (guardType == GUARD_TYPE.PATTOCHASE) anim.SetBool("isPatrolGuard", true);
        else if (guardType == GUARD_TYPE.CHASER) anim.SetBool("isChaserGuard", true);
        else if (guardType == GUARD_TYPE.ITEMCOLLECTOR)
        {
            anim.SetBool("isPatrolGuard", true);
            itemCollector.SetActive(true);
        }

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerActivated) { player = GameObject.Find("Player"); playerActivated = true; }

        info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Idle") || info.IsName("Patrol"))
        {
            if (isNearPlayer() && canHearPlayer) SetGuardType(GUARD_TYPE.CHASER);

        }

        if (isNearPlayer()) anim.SetBool("isWithinAttackingRange", true); else anim.SetBool("isWithinAttackingRange", false);

        if (info.IsName("Patrol"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            if (Vector3.Distance(transform.position, wayPoints[wayPointIndex].transform.position) < 1.5f) wayPointIndex++;
            if (wayPointIndex > wayPoints.Count + 1) wayPointIndex = 0;
            GetComponent<NavMeshAgent>().SetDestination(wayPoints[wayPointIndex].transform.position);

            if(guardType == GUARD_TYPE.PATTOCHASE)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                RaycastHit raycast;
                if(Physics.Raycast(transform.position, direction, out raycast))
                {
                    if(raycast.collider.gameObject == player)
                    {
                        guardType = GUARD_TYPE.CHASER;
                        anim.SetBool("isPatrolGuard", false);
                        anim.SetBool("isChaserGuard", true);
                        GetComponent<NavMeshAgent>().isStopped = true;
                        GetComponent<NavMeshAgent>().ResetPath();
                    }
                }
            }
        }
        if (info.IsName("Chase"))
        {
            GetComponent<NavMeshAgent>().speed = 2.5f;
            GetComponent<NavMeshAgent>().isStopped = false;
            if(player != null)
                GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
        else if (info.IsName("Attack"))
        {

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            GetComponent<NavMeshAgent>().isStopped = true;

            if (info.normalizedTime % 1.0f >= .98 && isVeryNearPlayer())
            {
                if(player.TryGetComponent<ControlPlayer>(out var playerScript))
                {
                    playerScript.DecreaseHealth(10);
                }

            }

        }
    }

    bool isNearPlayer()
    {
        if (player != null)
            if (Vector3.Distance(transform.position, player.transform.position) < 2.0f) return true; else return false;
        else
            return false;
    }

    public void Dies()
    {
        anim.SetTrigger("isDying");
    }

    bool isVeryNearPlayer()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f) return true; else return false;

    }
    public void SetGuardType(GUARD_TYPE newType)
    {

        guardType = newType;
        anim.SetBool("isIdleGuard", false);
        anim.SetBool("isPatrolGuard", false);
        anim.SetBool("isIChaserGuard", false);
        switch (guardType)
        {

            case GUARD_TYPE.IDLE: anim.SetBool("isIdleGuard", true); break;
            case GUARD_TYPE.PATROLLER: anim.SetBool("isPatrolGuard", true); break;
            case GUARD_TYPE.CHASER: anim.SetBool("isChaserGuard", true); break;
            default: anim.SetBool("idleGuard", true); break;

        }



    }
}
