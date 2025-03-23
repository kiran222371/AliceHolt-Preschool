using UnityEngine;
using UnityEngine.Animations;

public class CameraSpace : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParentConstraint>().SetSource(0, new ConstraintSource { sourceTransform = Camera.main.transform, weight = 1 });
    }
}