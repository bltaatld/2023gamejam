using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveCharacter : MonoBehaviour{
	[Header("Movement")]
	public float moveForce;
	public Vector2 movement{
		get;
		private set;
	}
	[Header("KeyCodes")]
	[SerializeField] KeyCode rightKeyCode;
	[SerializeField] KeyCode leftKeyCode;
	[SerializeField] KeyCode upKeyCode;
	[SerializeField] KeyCode downKeyCode;
	[SerializeField] KeyCode cursorMoveKeyCode;
	private new Rigidbody2D rigidbody2D;
	private Vector2 targetPosition;
	private bool toTarget = false;
	public float movementTargetDistance;


	protected virtual void Awake(){
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		MoveLoop();
	}

	public void MoveLoop(){
		var movement = Vector2.zero;
		if(Input.GetKey(cursorMoveKeyCode)){
			 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			 toTarget = true;
		}
		else{
			if(Input.GetKey(rightKeyCode)){
				movement.x++;
				toTarget = false;
			}
			if(Input.GetKey(leftKeyCode)){
				movement.x--;
				toTarget = false;
			}
			if(Input.GetKey(upKeyCode)){
				movement.y++;
				toTarget = false;
			}
			if(Input.GetKey(downKeyCode)){
				movement.y--;
				toTarget = false;
			}
		}

		if(toTarget){
			var relative = targetPosition - (Vector2)transform.position;
			if(relative.magnitude < movementTargetDistance){
				toTarget = false;
			}
			movement = relative;
		}
		this.movement = movement;
		AddForce(movement.normalized * Time.fixedDeltaTime * moveForce);
	}

	public void AddForce(Vector2 force){
		rigidbody2D.AddForce(force, ForceMode2D.Impulse);
	}
}

