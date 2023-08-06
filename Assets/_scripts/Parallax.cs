using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds; // Array of background layers
    public float[] parallaxSpeeds; // Parallax speeds for each layer

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCameraPosition.x - cameraTransform.position.x) * parallaxSpeeds[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, Time.deltaTime);
        }

        previousCameraPosition = cameraTransform.position;
    }
}
