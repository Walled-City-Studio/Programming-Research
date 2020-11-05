using UnityEngine;

public enum REGION_TYPE
{
    Rich,
    Poor,
    Industrial
}

public class Package : MonoBehaviour
{
    struct BootLocation
    {
        string BootID;
        REGION_TYPE RegionType;
        Vector3 Adress;
    }
    //Content?
    BootLocation Location;
    int PackageSize;
}
