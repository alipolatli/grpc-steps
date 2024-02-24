using Google.Protobuf;
using Hello.Grpc;

namespace _01_hello_grpc;
internal class Program
{
    static void Main(string[] args)
    {
        Employee employee = CreateEmployee();
        string filePath = "emp.dat";
        using (var fileStream = File.Create(filePath))
        {
            using (var output = new CodedOutputStream(fileStream))
            {
                employee.WriteTo(output);
            }
        }

        Employee fromFileEmployee;
        using (var input = File.OpenRead(filePath))
        {
            fromFileEmployee = Employee.Parser.ParseFrom(input);
        }
    }

    static Employee CreateEmployee()
    {
        Employee employee = new Employee();
        employee.EmployeeId = Guid.NewGuid().ToString();
        employee.FullName = "Ali Polatlı";
        employee.Age = 24;
        string imageUrl = "https://media.licdn.com/dms/image/D4D03AQHf-9eMe6SPqQ/profile-displayphoto-shrink_800_800/0/1691194217481?e=1714003200&v=beta&t=Wil60z136djmfsgx4IhS5n-U1uptnlchpVexKHipCGw";
        using (HttpClient client = new HttpClient())
        {
            var imageBytes = client.GetByteArrayAsync(imageUrl).Result;
            employee.ProfilePicture = Google.Protobuf.ByteString.CopyFrom(imageBytes);
        }
        for (int i = 1; i <= 10; i++)
        {
            string skillName = $"Skill{i}";
            string skillDescription = $"Description of Skill{i}";

            Skill skill = new Skill
            {
                Name = skillName,
                Description = skillDescription
            };
            employee.Skills.Add(skillName, skill);
        }
        employee.BirthDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
            DateTime.SpecifyKind(new DateTime(1999, 10, 21), DateTimeKind.Utc)
        );
        for (int i = 1; i <= 10; i++)
        {
            string projectId = $"Project{i}";
            string role = $"Role{i}";

            ProjectInvolvement projectInvolvement = new ProjectInvolvement
            {
                ProjectId = projectId,
                Role = role
            };

            employee.ProjectInvolvements.Add(projectInvolvement);
        }
        employee.PrimaryFavoriteProjectId = "1";
        employee.SecondaryFavoriteProjectId = "2";
        return employee;
    }
}