using System;

namespace Entities
{
    public class TimeFieldsWithoutDeclaration
    {
        private DateTime _createdAt;
        private DateTime _updatedAt;
        public DateTime CreatedAt { get { return _createdAt; } }
        public DateTime UpdatedAt { get { return _updatedAt; } }
    }
}