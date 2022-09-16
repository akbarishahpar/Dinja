namespace Dinja.Tests.Registry;

[TestFixture]
public class Registry
{
    [Test]
    public void Should_throw_FileNotFoundException_if_it_was_none_existent()
    {
        //Arrange
        var noneExistentFilePath = "NoneExistentFilePath.json";

        //Act
        void Act()
        {
            var registry = new Dinja.Registry(noneExistentFilePath);
        }

        //Assert
        Assert.Throws<FileNotFoundException>(Act);
    }
    
    [Test]
    public void Should_does_not_throw_if_correct_json_file_was_supplied()
    {
        //Arrange
        var existentFilePath = "appsettings.json";

        //Act
        void Act()
        {
            var registry = new Dinja.Registry(existentFilePath);
        }

        //Assert
        Assert.DoesNotThrow(Act);
    }
}