namespace ContosoPizza.Entities.Models
{
    public class Pizza
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsGlutenFree { get; set; }
    }
}