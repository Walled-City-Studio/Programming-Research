using UnityEngine;

//Get from enviroment when implemented

struct BootLocation
{
    public string BootID;
    public REGION_TYPE RegionType;
    public Vector3 Adress;
}

public class Package : MonoBehaviour
{
    public enum PACKAGE_SIZE
    {
        Small,
        Medium,
        Large
    }

    [SerializeField] private BootLocation PickUpLocation;
    [SerializeField] private BootLocation DropOffLocation;
    [SerializeField] private float MaxInteractionDistanceFromLocation;
    [SerializeField] private PACKAGE_SIZE PackageSize;
    [SerializeField] private bool Illegal;
    
    public bool IsDelivered()
    {
        return (Vector3.Distance(DropOffLocation.Adress, transform.position) <= MaxInteractionDistanceFromLocation);
    }
}