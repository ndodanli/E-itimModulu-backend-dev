using System;

namespace Entities
{
    public class TimeFieldsWithDeclaration
    {
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private string _updatedBy;
        public DateTime CreatedAt { get { return _createdAt; } }
        public DateTime UpdatedAt { get { return _updatedAt; } }
        public string UpdatedBy { get { return _updatedBy; } }
    }
}