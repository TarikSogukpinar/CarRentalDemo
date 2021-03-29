using Core.Entities;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string FuelType { get; set; }
        public int ProductionYear { get; set; }
        public string Description { get; set; }
    }
}