using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AngieBird : MonoBehaviour
{   
    private Rigidbody2D _rb; // Assigining a variable to the RigidBody2D component 
    private CircleCollider2D _circlecollider; // Assigining a variable to the CircleCollider2D component
    private bool _hasbeenlaunched; // Assigining a variable to check if the bird has been launched
    private bool _shouldfacedvelocitydirection;
    public void Awake(){
        _rb=GetComponent<Rigidbody2D>(); // Getting the RigidBody2D component In this case it is talking about Angrie bird
        _circlecollider=GetComponent<CircleCollider2D>(); // Getting the CircleCollider2D component In this case it is talking about Angrie bird
        
    }

    public void Start(){
        _rb.isKinematic=true; // Setting the RigidBody2D component to Kinematic as for physics does not apply to it
        _circlecollider.enabled=false; // Disabling the CircleCollider2D component 
        
    }

    private void FixedUpdate(){// FixedUpdate is called every fixed frame-rate frame and is used for commonly fixed updates such as RigidBody2d
        if(_hasbeenlaunched && _shouldfacedvelocitydirection){
            transform.right=_rb.velocity;

        }
    }
    public void LaunchBird(Vector2 direction,float force){
        _rb.isKinematic=false; // Setting the RigidBody2D component to Kinematic as for physics does apply to it
        _circlecollider.enabled=true; // Enabling the CircleCollider2D component 
        _rb.AddForce(direction*force,ForceMode2D.Impulse);
        _hasbeenlaunched=true;
        _shouldfacedvelocitydirection=true;
    
    }

    private void OnCollisionEnter2D(Collision2D collision){
        _shouldfacedvelocitydirection=false;
       
    }   
}
