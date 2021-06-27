## TLDR;
1. We hate invalid inputs: `null`s, too short, too long, empty, ...
2. We fail early and fast: fail means throwing an suitable Exception type for each situation.
3. We return validated values (Arguments): value-or-throw pattern.


### Common method arguments checks
```cs
public void SomeMethod(Guid id, string name, ISet<Tag> tags) {
  //throws System.ArgumentException if constraint is violated
  Arguments.NotEmpty(id, nameof(id));
  
  //throws ArgumentNullException, ArgumentOutOfRangeException, or Triplex.Validations.Exceptions.ArgumentFormatException depending on the violation
  Arguments.NotNullEmptyOrWhiteSpaceOnly(name, nameof(name));
  
  //throws ArgumentNullException if tags is null, ArgumentException if at least one element is null.
  Arguments.AllNotNull(tags, nameof(tags));
  
  //Here you confidently can use id, name and tags knowing that ther are valid.
}
```

### Help to construct an always valid, immutable object
```cs
using System
using Triplex.Validations; //root namespace

namespace My.Project {
  public sealed class SomeClass {
  
    //Instances of this class are either null (blame c#), or valid (your responsiblity as developer)
    public SomeClass(Guid id, string name, ISet<Tag> tags) {
      Id = Arguments.NotEmpty(id, nameof(id));
      Name = Arguments.NotNullEmptyOrWhiteSpaceOnly(name, nameof(name));
      Tags = new ReadOnlySet<Tag>( Arguments.AllNotNull(tags, nameof(tags)) );
    }

    public Guid Id { get; }
    public string Name { get; }
    public ReadOnlySet<Tag> Tags { get; }
  }
}
```
