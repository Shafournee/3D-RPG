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
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);

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
                    Debug.Log(raycast.collider.gameObject.name);
                    if(raycast.collider.gameObject == player)
                    {
                        Debug.Log("hitplayer");
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
            Debug.Log("in chase");
            GetComponent<NavMeshAgent>().speed = 2.5f;
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
        else if (info.IsName("Attack"))
        {

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            GetComponent<NavMeshAgent>().isStopped = true;

            if (info.normalizedTime % 1.0f >= .98 && isVeryNearPlayer())
            {

                player.GetComponent<ControlPlayer>().DecreaseHealth(10);

            }

        }
    }

    bool isNearPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2.0f) return true; else return false;
    }

    public void Dies()
    {
        anim.SetTrigger("isDying");
    }

    bool isVeryNearPlayer()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f) return true; else return false;

    }
}
