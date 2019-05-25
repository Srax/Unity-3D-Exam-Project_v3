
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform InteractionTransform;
    Transform player;
    bool isFocus = false;
    bool hasInteracted = false;

    public virtual void Interact() //https://unity3d.com/learn/tutorials/topics/scripting/overriding
    {
        //Make interacting different by overriding
        print("Interacting with " + transform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, InteractionTransform.position); //Get distance between interactable and player
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(InteractionTransform.position, radius);
    }
}
