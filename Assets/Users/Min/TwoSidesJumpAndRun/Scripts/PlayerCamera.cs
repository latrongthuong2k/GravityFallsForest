using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
    public Vector2 offsets;         

    private Transform target;
    private Transform thisT;
    private Vector3 followPos;

	// Use this for initialization
	void Start () 
    {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        thisT = GetComponent<Transform>();
	}
	
	void LateUpdate () 
    {
        
        followPos = new Vector3(target.position.x + offsets.x, offsets.y, thisT.position.z);
        
        thisT.position = followPos;
	}
}
