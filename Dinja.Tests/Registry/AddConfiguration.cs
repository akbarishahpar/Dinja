namespace Dinja.Tests.Registry;

[TestFixture]
public class AddConfiguration
{
    private Dinja.Registry _registry;

    [SetUp]
    public void SetUp()
    {
        _registry = new Dinja.Registry("appsettings.json");
    }

    [Test]
    public void Should_throw_KeyNotFoundException_if_specified_key_was_none_existent()
    {
        //Arrange
        const string noneExistentKey = "SomeRandomKey";

        //Act
        void Act()
        {
            _registry.AddConfiguration<Version>(noneExistentKey);
        }

        //Assert
        Assert.Throws<KeyNotFoundException>(Act);
    }
    
    [Test]
    public void Should_does_not_throw_if_specified_key_was_correct()
    {
        //Arrange
        const string existentKey = "Version";

        //Act
        void Act()
        {
            _registry.AddConfiguration<Version>(existentKey);
        }

        //Assert
        Assert.DoesNotThrow(Act);
    }
    
    [Test]
    public void Should_does_not_throw_if_mapped_key_from_type_was_correct()
    {
        //Act
        void Act()
        {
            _registry.AddConfiguration<Version>();
        }

        //Assert
        Assert.DoesNotThrow(Act);
    }
    
    [Test]
    public void Should_does_not_throw_if_specified_key_for_shallow_configuration_was_correct()
    {
        //Arrange
        const string existentKey = "Name";

        //Act
        void Act()
        {
            _registry.AddConfiguration<string>(existentKey);
        }

        //Assert
        Assert.DoesNotThrow(Act);
    }
}