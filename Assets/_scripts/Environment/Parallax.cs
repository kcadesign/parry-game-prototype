using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] Backgrounds; // Array of background layers
    public float[] ParallaxSpeeds; // Parallax speeds for each layer

    private Transform _cameraTransform;
    private Vector3 _previousCameraPosition;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _previousCameraPosition = _cameraTransform.position;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallax = (_previousCameraPosition.x - _cameraTransform.position.x) * ParallaxSpeeds[i];
            float backgroundTargetPosX = Backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Time.fixedDeltaTime);
        }

        _previousCameraPosition = _cameraTransform.position;
    }
}
