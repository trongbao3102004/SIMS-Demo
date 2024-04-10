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
			Assert.Equal(2, result?.Count);
		}
	}
}