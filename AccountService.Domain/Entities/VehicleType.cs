namespace AccountService.Domain.Entities
{
    public class VehicleType : BaseEntity
    {
    public int VehicleTypeId { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();
    }
}