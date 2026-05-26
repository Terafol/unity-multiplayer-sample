using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    private float verticalRotation;
    private float horizontalRotation;

    public Vector3 alignement = new Vector3(0, 4, 0);
    public LayerMask playerLayerMask; // Ajoutez un LayerMask pour ignorer le joueur

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }
        
        // Positionner la caméra au niveau du visage avec un décalage
        transform.position = Target.position + alignement;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }

    void Start()
    {
        // Ignorer le rendu du joueur pour la caméra
    }
}
