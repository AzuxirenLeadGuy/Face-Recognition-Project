using System;
using FaceRecognitionDotNet;
using System.Linq;
namespace FRAttendance
{
    public static class Common
    {
        public static FaceRecognition fr;
        public static void Init(string path){fr=FaceRecognition.Create(path);}
    } 
    public struct Person:IComparable<Person>,IEquatable<Person>
    {
        public string name,roll,course,dept,enroll;
        public FaceEncoding Face;
        public Person(string n,string r,string c,string d,string e,Image i)
        {
            name=n;
            roll=r;
            course=c;
            dept=d;
            enroll=e;
            Face=Common.fr.FaceEncodings(i).First();
        }
        public int CompareTo(Person other)=>roll.CompareTo(other.roll);
        public bool Equals(Person other)=>roll.Equals(other.roll);
        public override int GetHashCode()=>roll.GetHashCode();
    }
    internal struct Subject:IComparable<Subject>,IEquatable<Subject>
    {
        public Person[] Students;
        public string Code,Name,FacultyName;
        public int year;
        public Subject(int size,string code,string n,string fname,int y)
        {
            Students=new Person[size];
            Code=code;
            Name=n;
            year=y;
            FacultyName = fname;
        }
        public Subject(SubjectLoader load)
        {
            Code = load.Code;
            Name = load.Name;
            year = load.year;
            FacultyName = load.Fname;
            var str = load.StudentRolls.Length;
            Students = new Person[str];
            for (int i = 0; i < str; i++)
            {
                string path = AssetLoad.AssetURI + @"\Students\" + load.StudentRolls[i].ToString() + @".uafp";
                Students[i] = AssetLoad.PersonLoad(path);
            }
        }
        public string Roll => Code + year.ToString();
        public int CompareTo(Subject other)=>Roll.CompareTo(other.Roll);
        public bool Equals(Subject other)=>Roll.Equals(other.Roll);
        public override int GetHashCode()=>Roll.GetHashCode();
        public AttendanceReport TakeAttendance(Image img) => TakeAttendance(Common.fr.FaceEncodings(img).ToArray());
        public AttendanceReport TakeAttendance(FaceEncoding[] faces)
        {
            var r = new AttendanceReport();
            r.Date = DateTime.Now;
            r.subject = this;
            var Slen = Students.Length;
            r.Present = new bool[Slen];
            foreach (var face in faces)
            {
                for (int j = 0; j < Slen; j++)
                {
                    if (!r.Present[j])
                    {
                        r.Present[j] = FaceRecognition.CompareFace(Students[j].Face, face);
                    }
                }
            }
            return r;
        }
    }
    internal struct SubjectLoader : IEquatable<SubjectLoader>
    {
        public string Code, Name, Fname;
        public int year;
        public string[] StudentRolls;
        public SubjectLoader(Subject s)
        {
            Code = s.Code;
            year = s.year;
            Name = s.Name;
            Fname = s.FacultyName;
            var st = s.Students.Length;
            StudentRolls = new string[st];
            for(int i = 0; i < st; i++) { StudentRolls[i] = s.Students[i].roll; }
        }
        public string SubRoll => Code + year.ToString();
        public bool Equals(SubjectLoader other) => SubRoll.Equals(other.SubRoll);
    }
    internal struct AttendanceReport:IEquatable<AttendanceReport>
    {
        internal object subject;
        internal bool[] Present;
        internal DateTime Date;
        public bool Equals(AttendanceReport other)=>((Subject)subject).Code.Equals(((Subject)other.subject).Code)&&Date.Equals(other.Date);
        public AttendanceReport(Subject s)
        {
            Date=DateTime.Now;
            Present=new bool[s.Students.Length];
            subject=s;
        }
        public override string ToString()
        {
            int l=Present.Length;
            string output=$"Code\t{((Subject)subject).Code}\nDate\t{Date}\nClass Strength\t{l}\n";
            for(int i=0;i<l;i++)
            {
                output+=$"{((Subject)subject).Students[i].roll}\t{((Subject)subject).Students[i].name}\t";
                output+=Present[i]?"Present\n":"Absent\n";
            }
            return output;
        }
        public System.Collections.Generic.List<Data_Diaplay> ConvertToList()
        {
            System.Collections.Generic.List<Data_Diaplay> list = new System.Collections.Generic.List<Data_Diaplay>();
            Subject s = (Subject)subject;
            String att;
            for (int i=0;i<Present.Length;i++)
            {
                att = Present[i] == true ? "Present" : "Absent";
                list.Add((new Data_Diaplay { roll_no=s.Students[i].roll,name= s.Students[i].name,present= att } ));
            }
            return list;
        }
    }
    public class Data_Diaplay
    {
        internal string roll_no { get; set; }
        internal string name { get; set; }
        internal string present { get; set; }
    }
}