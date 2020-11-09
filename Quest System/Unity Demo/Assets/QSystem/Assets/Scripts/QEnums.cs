namespace QSystem
{
    public enum REGION_TYPE { Unkown, Rich, Poor, Industrial}

    public enum CRITERIA_TYPE { Delivery, PickUp, PickUpDelivery }

    public enum REWARD_TYPE { Resource, Item }

    public enum PACKAGE_SIZE { Small, Medium, Large }

    public enum RESOURCE_TYPE { Money, Gold, Silver }

    public enum CHALLENGE_TYPE { Easy, Medium, Hard }

    public enum LEGAL_STATUS { Unkown, Legal, Illegal }

    public enum AVAILABLE_WHEN_TYPE { Always, Quest, Region, Resource }

    public enum AVAILABLE_IF_STATUS { Complete, Incomplete, Start, Open, NotAvailable, Discovered, Undiscovered }
}