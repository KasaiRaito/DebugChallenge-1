using UnityEngine;

public class WarningHelper : MonoBehaviour
{
    private Player myPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myPlayer = FindAnyObjectByType<Player>();
        
        if(!myPlayer)
            return;

        if (!myPlayer.GetSpeed())
        {
            Debug.LogError("Speed is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Player (Script) /Speed
        }

        if (!myPlayer.GetJumpSpeed())
        {
            Debug.LogError("JumpSpeed is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Player (Script) /Jump Force
        }

        if (!myPlayer.GetGravity())
        {
            Debug.LogError("Gravity is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Player (Script) /Gravity Scale
        }

        if (!myPlayer.GetUseRb())
        {
            Debug.LogError("UseRb is " + myPlayer.GetUseRb()); // Ve en el Unity /en jerarquía /Player /Player (Script) /Use Rb
            
        }
        else if (myPlayer.GetRbComponent() == null)
        {
            Debug.LogError("Player doesn't have Rb Component"); // Ve en el Unity /en jerarquía /Player /Agregar Componente /RigidBody2D
        }
        else
        {
            if (myPlayer.GetRbComponent().gravityScale != 0f)
            {
                Debug.LogError("GravityScale is different to 0.0f"); // Ve en el Unity /en jerarquía /Player /Rigidbody 2D /Gravity Scale
            }
            if (!myPlayer.GetRbComponent().simulated)
            {
                Debug.LogError("Simulated is off"); // Ve en el Unity /en jerarquía /Player /Rigidbody 2D /Simulate
            }
            if ((myPlayer.GetRbComponent().constraints & RigidbodyConstraints2D.FreezeRotation) == 0)
            {
                Debug.LogError("Constrain Rotation is off"); // Ve en el Unity /en jerarquía /Player /Rigidbody 2D /Constraints /Freeze Rotation Z
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
