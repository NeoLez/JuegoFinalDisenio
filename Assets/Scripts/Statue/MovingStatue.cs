using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MovingStatue : MonoBehaviour
{
    //TP2 Belen Mounier
    public NavMeshAgent ai;
    public Transform player;
    public Animator statueAnim;
    public bool freeze;

    Vector3 _dest;
    
    public Camera playerCam;
    public float aiSpeed;
    public float catchDistance;
    
    void Update()
    {
 
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

  
        float distance = Vector3.Distance(transform.position, player.position);

   
        if(freeze || GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = 0;
            statueAnim.speed = 0;
            ai.SetDestination(transform.position);
        } else {
            ai.speed = aiSpeed;
            statueAnim.speed = 1;
            _dest = player.position;
            ai.destination = _dest;
            
            if(distance <= catchDistance) {
                player.gameObject.GetComponent<PlayerHealth>().Die();
            }
        }
    }
}