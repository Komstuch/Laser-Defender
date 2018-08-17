using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 8.5f;
	public float height = 7.5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = false;
	private float xmax, xmin;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;

        print(xmin + " " + xmax);
		
		SpawnUntillFull();
	}
	
	void SpawnEnemies(){
		foreach (Transform child in transform){ //Instantiate enemy for every child transform in "myself"
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child; //Set each of my "children" (Position objects) as parents for each enemy spawned
		}
	}
	
	void SpawnUntillFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke ("SpawnUntillFull", spawnDelay);
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
	
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight){
			transform.position += Vector3.right*speed*Time.deltaTime;
		} else{
			transform.position += Vector3.left*speed*Time.deltaTime;
		}
		
		// Check if formation is going outside of the playspace
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(leftEdgeOfFormation < xmin){
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax){
			movingRight = false;
		}
		
		if(AllMembersDead()){
			Debug.Log("Empty Formation");
			SpawnUntillFull();
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
