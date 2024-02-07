using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthOfFieldController : MonoBehaviour
{
    Ray raycast;
    RaycastHit hit;
    float hitDistance;

    Volume volume;

    DepthOfField depthOfField;

    [Range(0f, 10f)]
    public float focusSpeed;
    public float maxFocusDistance;

    [SerializeField] private LayerMask _depthRaycastMask;
    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out depthOfField);
    }

    public void Update()
    {
        raycast = new Ray(transform.position, transform.forward * maxFocusDistance);

        

        if (Physics.Raycast(raycast, out hit, maxFocusDistance, _depthRaycastMask))
        {
            
            hitDistance = Vector3.Distance(transform.position, hit.point);
        }
        else
        {
            if (hitDistance < maxFocusDistance)
            {
                hitDistance++;
            }
        }

        SetFocus();
    }

    void SetFocus()
    {
        depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, hitDistance, Time.deltaTime * focusSpeed);
    }
}
