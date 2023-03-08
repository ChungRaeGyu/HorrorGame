using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GhostScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public Transform target;
    private NavMeshAgent nav;
    public Transform clue;
    public Transform center;
    float radius = 8; //Monster's range in the 'morning'
    float Mapradius = 56;
    PhotonView pv;
    Rigidbody rigid;
    Animator Anim;
    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        pv = this.GetComponent<PhotonView>();
        rigid = this.GetComponent<Rigidbody>();
        Anim = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //pathPending : NavMeshAgent�� ��ΰ���� �Ϸ��ϱ� ������ true���� ������. 
        //remaingDistance : �����Ÿ� ���Ϸ��?
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            if (pv.IsMine)
            {
                //if(Check the 'morning' in Gamemanger
                {
                    SetNewRandomDestinationMorning();
                }
                //else--------------------------��
                {
                    SetNewRandomDestinationNight();
                }
            }
        }
    }

    //��ħ�� �� ���� ���? ���? �ϴ°ž�!
    private void SetNewRandomDestinationMorning()
    {
        NavMeshHit navHit;
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += clue.position;
            if (NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas))
                break;
        }
        nav.SetDestination(navHit.position);
        pv.RPC("SetPoint", RpcTarget.OthersBuffered, navHit);
    }
    [PunRPC]
    public void SetPoint(NavMeshHit A)
    {
        nav.SetDestination(A.position);
    }
    //�㿡 ���� ���? ���?
    private void SetNewRandomDestinationNight()
    {
        NavMeshHit navHit; //Save in direction vector of collision surface
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * Mapradius;
            randomDirection += center.position;
            if (NavMesh.SamplePosition(randomDirection, out navHit, Mapradius, NavMesh.AllAreas))
                break;//NavMesh.SamplePosition(randomDirection, out navHit, Mapradius, NavMesh.AllAreas)--> Can go 'randomDirection' return true or false
        }
        nav.SetDestination(navHit.position);
        pv.RPC("SetPoint", RpcTarget.OthersBuffered, navHit);
    }

    void MoveAnim()
    {
        Anim.SetFloat("walk",rigid.velocity.magnitude);
        
    }
    //�����ؾ��ϴ� ������ ����....    
}
