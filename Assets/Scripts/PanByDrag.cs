using UnityEngine;
using System.Collections;

//modified based on ref http://wiki.unity3d.com/index.php?title=MouseOrbitImproved#Code_C.23
public class PanByDrag : MonoBehaviour
{
    public Transform target;
    public float distance = 20.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target && Input.GetMouseButton(0))
        {
            float deltaX;
            float deltaY;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                deltaX = -touchDeltaPosition.x * 0.01f;
                deltaY = touchDeltaPosition.y * 0.01f;
            }
            else
            {
                deltaX = Input.GetAxis("Mouse X");
                deltaY = Input.GetAxis("Mouse Y");
            }
            x += deltaX * xSpeed * distance * 0.02f;
            y -= deltaY * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

// if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
// {
//     Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//     transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
// }