using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidBodys : MonoBehaviour
{

    public float pushPower = 2.0f;

    private float targetMass;
    

    private void OnControllerColliderHit(ControllerColliderHit hit) { // esta funcion actuara cuando collisionemos con un objeto.
        
        Rigidbody body = hit.collider.attachedRigidbody; //Una vez detectemos el objeto almacenamos su rigidbody.

        if (body == null || body.isKinematic) // si el objeto no tiene rigidbody o esta asignado como Kinematic, no hacemos nada.
        {
            return;
        }

        if (hit.moveDirection.y < -0.3) // si el contecto se realiza en el eje vertical, tampoco hacemos nada. podemos poner el numero negativo que queramos.
        {
            return;
        }

        targetMass = body.mass; //almacenamos la massa del objeto en contacto

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z); // almacenamos la direccion en la que estamos empujando el objeto
        
        body.velocity = pushDir * pushPower / targetMass;
        
    }
}
