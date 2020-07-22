using AutoMapper.Configuration.Annotations;

namespace ClassRegistration.Domain.Model
{
    public class StudentModel : BaseBusinessModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { 

            get => FirstName + " " + LastName;
            set {
                var names = value.Split(" ");

                FirstName = names[0];
                LastName = names[1];
            }
        }
    }
}
