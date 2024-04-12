using SIMS_Demo.Controllers;

namespace SIMS_Test
{
	public class UnitTest1
	{
		[Fact]
		public void Test_LoadTeacherSuccess()
		{
			TeacherController sut = new TeacherController();
			var result = sut.LoadTeacherFromFile("data.json");
			Assert.Equal(0, result?.Count);
		}
		[Fact]
        public void Test_LoadUsersFromFile()
        {
			AuthenticationController sut = new AuthenticationController();
			var result = sut.LoadUsersFromFile("users.json");
			Assert.Equal(2, result?.Count);
        }
        [Fact]
		public void Test_CreateCourse() 
		{
            var controller = new CourseController();
            var newCourse = new Course { Id = 1, Name = "Test Course" };
            var result = controller.CreateCourse() as ViewResult;
            Assert.NotNull(result);
        }
		[Fact]
		public void Test_IndexTeacher()
		{
			var teachers = new List<Teacher>
	        {
		    new Teacher { Id = 1, Name = "huy" },
		    new Teacher { Id = 2, Name = "quang" }
	        };
        }
		[Fact]
		public void Test_DataAuthentication()
		{
            var users = new List<User>
            {
            new User { Id = 1, UserName = "Lili" },
            new User { Id = 2, UserName = "Layla" }
            };
        }
		[Fact]
		public void Test_IndexCourse()
		{
            var course = new List<Course>
            {
            new Course { Id = 1, Name = "Math" },
            };
        }
		[Fact]
		public void Test_InvalidUserData()
		{
            var controller = new AuthenticationController(); 
            var user = new User { UserName = "peter", Pass = "1111" };

            var result = controller.Login(user);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Login", viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData["error"]);
        }
        [Fact]
		public void Test_DeleteTeacher()
		{
            var controller = new TeacherController();

            var teacherIdToRemove = 1;
            var teachers = new List<Teacher>
        {
        new Teacher { Id = 1, Name = "Teacher 1" },
        new Teacher { Id = 2, Name = "Teacher 2" }
        };

            var result = controller.Delete(teacherIdToRemove) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); 

            var remainingTeachers = teachers.Where(t => t.Id != teacherIdToRemove).ToList();
            Assert.Equal(1, remainingTeachers.Count); 
        }


    }
}
