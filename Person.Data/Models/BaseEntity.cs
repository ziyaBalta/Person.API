using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string? UpdateUserId { get; set; }

        public void SetCreate(string createUserId)
        {
            this.CreatedDate = DateTime.UtcNow;
            this.CreateUserId = createUserId;
        }
        public void SetUpdate(string updateUserId, BaseEntity original)
        {
            this.UpdatedDate = DateTime.UtcNow;
            this.UpdateUserId = updateUserId;

            this.CreateUserId = original.CreateUserId;
            this.CreatedDate = original.CreatedDate;
            this.Id = original.Id;
        }
    }
}
