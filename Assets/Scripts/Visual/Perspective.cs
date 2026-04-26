using UnityEngine;

public class GroundPerspective : MonoBehaviour
{
    public Material groundMaterial;
    public Transform persPoint;
    public SpriteRenderer sprite;
    private Vector3 groundCenterPosition;
    private  float groundWidth;
    private float leftSide;
    private float rightSide;
    private float centerPoint;

    private void Update()
    {
        groundCenterPosition = sprite.bounds.center;
        groundWidth = sprite.bounds.size.x;

        GroundMapping();

        centerPoint = (persPoint.position.x - leftSide) / groundWidth;

        groundMaterial.SetFloat("_Center_X", centerPoint);
    }
    private void GroundMapping()
    {
        leftSide = groundCenterPosition.x - sprite.bounds.extents.x;
        rightSide = groundCenterPosition.x + sprite.bounds.extents.x;
    }
}
