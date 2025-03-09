using UnityEngine;

public class Foilage : MonoBehaviour
{
    public float swaySpeed = 1;
    public float swayAmount = 1;
    public float swayOffset = 0;
    public Vector3 swayAxis = Vector3.up;
    private Vector3 startPos;
    private Material foilageMat;
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.position = startPos + swayAxis * Mathf.Sin((Time.time + swayOffset) * swaySpeed) * swayAmount;
    }
}