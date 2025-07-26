using System.ComponentModel.DataAnnotations;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.AccountsPayable
{
    public class AccountsPayableViewModel
    {
        public class GetRequest : BaseEntity
        {                   
        }

        public class CreateRequest
        {
            [Required]
            public string Description { get; set; }
            
            [Required]
            public decimal Amount { get; set; }
            
            [Required]
            public DateTime DueDate { get; set; }
            
            [Required]
            public string Status { get; set; }
            
            public string? Company { get; set; }
            public string? InvoiceNumber { get; set; }
            
            [Required]
            public string Category { get; set; }
            
            public string? Notes { get; set; }
            
            [Required]
            public string CondominiumId { get; set; }
            
            public string? FileName { get; set; }
            public string? FileUrl { get; set; }
        }

        public class UpdateRequest
        {
            public string Id { get; set; }
            
            [Required]
            public string Description { get; set; }
            
            [Required]
            public decimal Amount { get; set; }
            
            [Required]
            public DateTime DueDate { get; set; }
            
            [Required]
            public string Status { get; set; }
            
            public string? Company { get; set; }
            public string? InvoiceNumber { get; set; }
            
            [Required]
            public string Category { get; set; }
            
            public string? Notes { get; set; }
            public string? FileName { get; set; }
            public string? FileUrl { get; set; }
        }

        public class DeleteRequest
        {
            public string Id { get; set; }
        }

        public class Response
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public DateTime DueDate { get; set; }
            public string Status { get; set; }
            public string? Company { get; set; }
            public string? InvoiceNumber { get; set; }
            public string Category { get; set; }
            public string? Notes { get; set; }
            public string CondominiumId { get; set; }
            public string? FileName { get; set; }
            public string? FileUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}

