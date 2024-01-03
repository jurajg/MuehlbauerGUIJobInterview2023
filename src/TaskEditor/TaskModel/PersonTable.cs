using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    class PersonTable : Table
    {
        List<Person> data;

        public PersonTable() : base("person")
        {
            Clear();
        }

        public override void Clear()
        {
            data = new();
        }

        public Person CreatePerson()
        {
            Person person = new();
            person.Id = CreateUniqueId();
            data.Add(person);
            return person;
        }

        public override long CreateUniqueId()
        {
            var maxId = data.Aggregate((agg, next) =>
                                       next.Id > agg.Id ? next : agg).Id;
            return maxId + 1;
        }

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
