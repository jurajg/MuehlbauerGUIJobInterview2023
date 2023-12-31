﻿using CSVStorageLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    public class PersonTable : Table
    {
        public List<Person> data;

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
            if (data.Count == 0) return 1;
            var maxId = data.Aggregate((agg, next) =>
                                       next.Id > agg.Id ? next : agg).Id;
            return maxId + 1;
        }

        public Person GetPersonById(long id)
        {
            List<Person> persons = data.Where(item => item.Id == id).ToList();
            if (persons.Count > 0) return persons[0];
            else return null;
        }

        public void DeletePersonById(long id)
        {
            data.RemoveAll(item => item.Id == id);
        }
        public void DeletePerson(Person person)
        {
            data.Remove(person);
        }

        public List<string> GetNames()
        {
            return data.Select(item => item.Name).ToList();
        }

        public override void Load()
        {
            string fPath = CSVStorage.GetTableFPath(TableName);

            if (!File.Exists(fPath))
            {
                Clear();
            }
            else
            {
                Person personType = new();
                List<ICSVRow> result = CSVStorage.ReadCSV(fPath, personType);
                data = new(result.Cast<Person>());
            }
        }

        public override void Save()
        {
            string fPath = CSVStorage.GetTableFPath(TableName);

            CSVStorage.CreateTablePath(TableName);

            List<ICSVRow> rows = new(data.Cast<ICSVRow>());
            Person personType = new();
            CSVStorage.WriteCSV(fPath, rows, personType);
        }
    }
}
