using Dinja.Tests.Registry.Models;

namespace Dinja.Tests.Registry;

[TestFixture]
public class AddConfiguration : TestFixtureBase
{
    [Test]
    public void Should_throw_KeyNotFoundException_if_specified_key_was_none_existent()
    {
        //Arrange
        const string noneExistentKey = "SomeRandomKey";

        //Act
        void Act()
        {
            Registry.AddConfiguration<AppVersion>(noneExistentKey);
        }

        //Assert
        Assert.Throws<KeyNotFoundException>(Act);
    }
    
    [Test]
    public void Should_does_not_throw_if_specified_key_was_correct()
    {
        //Arrange
        const string existentKey = "AppVersion";

        //Act
        void Act()
        {
            Registry.AddConfiguration<AppVersion>(existentKey);
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
            Registry.AddConfiguration<AppVersion>();
        }

        //Assert
        Assert.DoesNotThrow(Act);
    }
}