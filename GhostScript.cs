using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

//���� ĳ���� �ĺ��� �޷����� ����, ����
public class GhostScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public Transform target;
    private NavMeshAgent nav;
    public Transform clue;
    public Transform center;
    float radius = 8; //����ĭ ũ��
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
    //Vector3 randomPosition = Random.insideUnitSphere(�������� 1�α����� �������� �� ���� ��´�?.) * sphereRadius; 
    // Update is called once per frame
    void Update()
    {
        
        // ���Ͱ� ��ǥ ������ �����ϸ� ���ο� ��ǥ ������ �����մϴ�.
        //pathPending : NavMeshAgent�� ��ΰ���� �Ϸ��ϱ� ������ true���� ������. 
        //remaingDistance : �����Ÿ� ���Ϸ��?
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            if (pv.IsMine)
            {
                //if(GameManager�� static���� �� �� ������ �ؼ� �װɷ� �Ҳ���) ��
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
        NavMeshHit navHit; //�̵������� �������? Ȯ���Ҷ� navHit�� ���� ���� �����Ѵ�.
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += clue.position;
            if (NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas))
                break;//�̰��� �������� ���� ��Ұ�? �̵������� �������? Ȯ���ϴ� ��
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
        NavMeshHit navHit; //�̵������� �������? Ȯ���Ҷ� navHit�� ���� ���� �����Ѵ�.
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * Mapradius;
            randomDirection += center.position;
            if (NavMesh.SamplePosition(randomDirection, out navHit, Mapradius, NavMesh.AllAreas))
                break;//�̰��� �������� ���� ��Ұ�? �̵������� �������? Ȯ���ϴ� ��
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
