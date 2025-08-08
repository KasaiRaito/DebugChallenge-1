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
            Debug.LogWarning("Speed is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Player (Script) /Speed
        }

        if (!myPlayer.GetJumpSpeed())
        {
            Debug.LogWarning("JumpSpeed is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Player (Script) /Jump Force
        }

        if (!myPlayer.GetAcceleration())
        {
            Debug.LogWarning("Acceleration is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Player (Script) /Gravity Scale
        }

        if (!myPlayer.GetDeceleration())
        {
            Debug.LogWarning("Deceleration is equal to 0.0f");
        }

        if (myPlayer.GetRbComponent())
        {
            if (myPlayer.GetRbComponent().gravityScale == 0f)
            {
                Debug.LogWarning("GravityScale is equal to 0.0f"); // Ve en el Unity /en jerarquía /Player /Rigidbody 2D /Gravity Scale
            }
            if (!myPlayer.GetRbComponent().simulated)
            {
                Debug.LogWarning("Simulated is off"); // Ve en el Unity /en jerarquía /Player /Rigidbody 2D /Simulate
            }
            if ((myPlayer.GetRbComponent().constraints & RigidbodyConstraints2D.FreezeRotation) == 0)
            {
                Debug.LogWarning("Constrain Rotation is off"); // Ve en el Unity /en jerarquía /Player /Rigidbody 2D /Constraints /Freeze Rotation Z
            }
        }
        else
        { 
            Debug.LogWarning("Player doesn't have Rb Component"); // Ve en el Unity /en jerarquía /Player /Agregar Componente /RigidBody2D 
        }
    }
}
