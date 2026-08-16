namespace JadooTravel.Settings;

public interface IDatabaseSettings
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
    public string CategoryCollectionName { get; }
    public string DestinationCollectionName { get; }
    public string FeatureCollectionName { get; }
    public string TripPlanCollectionName { get; }
    public string TestimonialCollectionName { get; }
    public string PartnerCollectionName { get; }
    public string SubscribeCollectionName { get; }
    public string BookingCollectionName { get; set; }
    
}