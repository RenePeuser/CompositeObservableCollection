namespace CompositeObservableCollection
{
    public class Person
    {
        public Person(string name, string familyName)
        {
            Name = name;
            FamilyName = familyName;
        }

        public string Name { get; set; }
        public string FamilyName { get; set; }
    }

    public class VirtualPerson : Person
    {
        public VirtualPerson(string name, string familyName) : base(name, familyName)
        {
        }
    }

    public class SpecialPerson : Person
    {
        public SpecialPerson(string name, string familyName) : base(name, familyName)
        {
        }
    }
}
