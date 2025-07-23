namespace PersonManagement.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;


        public void SetUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            SetUpdated();
        }
    }
}
