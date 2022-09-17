namespace Dinja.Tests.Registry;

[TestFixture]
public class Constructor
{
    [Test]
    public void Should_throw_FileNotFoundException_if_specified_path_was_none_existent()
    {
        //Arrange
        const string noneExistentFilePath = "NoneExistentFilePath.json";

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
        const string existentFilePath = "appsettings.json";

        //Act
        void Act()
        {
            var registry = new Dinja.Registry(existentFilePath);
        }

        //Assert
        Assert.DoesNotThrow(Act);
    }
}