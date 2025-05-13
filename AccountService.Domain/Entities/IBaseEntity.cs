using System;

namespace AccountService.Domain.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        string AddedBy { get; set; }
        string? UpdatedBy { get; set; }
        bool Active { get; set; }
    }
} 