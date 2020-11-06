using UnityEngine;

//Get from enviroment when implemented
public enum REGION_TYPE
{
    Rich,
    Poor,
    Industrial
}
struct BootLocation
{
    string BootID;
    REGION_TYPE RegionType;
    Vector3 Adress;
}

public class Package : MonoBehaviour
{
    public enum PACKAGE_SIZE
    {
        Small,
        Medium,
        Large
    }

    [SerializeField] private BootLocation Location;
    [SerializeField] private PACKAGE_SIZE PackageSize;
    [SerializeField] private bool Illegal;
}
