using System.Collections.ObjectModel;

namespace CompositeObservableCollection
{
    public class MainWindowViewModel : BindingBase
    {
        public MainWindowViewModel()
        {
            Persons = new ObservableCollection<Person>();

            VirtualPersons = new ObservableCollection<VirtualPerson>();

            SpecialPersons = new ObservableCollection<SpecialPerson>();
        }

        public ObservableCollection<Person> Persons { get; }

        public ObservableCollection<VirtualPerson> VirtualPersons { get; }

        public ObservableCollection<SpecialPerson> SpecialPersons { get; }

        public void AddPerson()
        {
            Persons.Add(new Person("Test" + Persons.Count, "A" + Persons.Count));
        }

        public void AddVirtualPerson()
        {
            VirtualPersons.Add(new VirtualPerson("Virtual Person" + VirtualPersons.Count, "B" + VirtualPersons.Count));
        }

        public void AddSpecialPerson()
        {
            SpecialPersons.Add(new SpecialPerson("Special Person" + VirtualPersons.Count, "C" + VirtualPersons.Count));
        }
    }
}