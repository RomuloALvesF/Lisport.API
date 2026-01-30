using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public string Document { get; private set; } = string.Empty;
        public string ResponsibleName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public string? Notes { get; private set; }
        public Guid ClassGroupId { get; private set; }
        public ClassGroup ClassGroup { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        private Student() { }

        public Student(
            string name,
            DateTime birthDate,
            Gender gender,
            string document,
            string responsibleName,
            string phone,
            string address,
            string? notes,
            Guid classGroupId)
        {
            Id = Guid.NewGuid();
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            Document = document;
            ResponsibleName = responsibleName;
            Phone = phone;
            Address = address;
            Notes = notes;
            ClassGroupId = classGroupId;
            CreatedAt = DateTime.Now;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
        }

        public void UpdateGender(Gender gender)
        {
            Gender = gender;
        }

        public void UpdateDocument(string document)
        {
            Document = document;
        }

        public void UpdateResponsibleName(string responsibleName)
        {
            ResponsibleName = responsibleName;
        }

        public void UpdatePhone(string phone)
        {
            Phone = phone;
        }

        public void UpdateAddress(string address)
        {
            Address = address;
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
        }
    }
}
