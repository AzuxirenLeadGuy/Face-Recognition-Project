﻿using Newtonsoft.Json;
using System.IO;
namespace FRAttendance
{
    public class AssetLoad
    {
        /// <summary>
        /// Add "/Students/" for students folder and "/SubjectLoad/ for SubjectLoader" folder
        /// </summary>
        public static string AssetURI;
        public static Person PersonLoad(string path)
        {
            var Person = JsonConvert.DeserializeObject<Person>(File.ReadAllText(path));
            return Person;
        }
        internal static Subject SubjectLoad(string path)
        {
            var Subject = new Subject(JsonConvert.DeserializeObject<SubjectLoader>(File.ReadAllText(path)));
            return Subject;
        }
        internal static void SavePerson(Person p)
        {
            var path = AssetURI + @"/Students/"+p.roll+@".uafp";
            var content = JsonConvert.SerializeObject(p);
            File.WriteAllText(path, content);
        }
        internal static void SaveSubject(Subject s)
        {
            var path = AssetURI + @"/SubjectLoad/"+s.Roll+@".uafs";
            var content = JsonConvert.SerializeObject(new SubjectLoader(s));
            File.WriteAllText(path, content);
        }
        internal static void SaveSubject(SubjectLoader s)
        {
            var path = AssetURI + @"/SubjectLoad/" + s.Code + s.year.ToString() + @".uafs";
            var content = JsonConvert.SerializeObject(s);
            File.WriteAllText(path, content);
        }
    }
}
