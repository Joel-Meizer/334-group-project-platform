using System;

namespace _334_group_project_web_api.Models
{
    public enum StaffMemberType
    {
        Administrator, // manages the entire system
        RestrictedEmployee // can view shopping lists and client details, needs approvals to complete certain tasks
    }
    public class GrocerStaff
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string GrocerId { get; set; } // Reference to the grocer this staff member belongs to
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
    }
}