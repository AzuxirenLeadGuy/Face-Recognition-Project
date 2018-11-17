using System;
using FaceRecognitionDotNet;
using System.Linq;
namespace FRAttendance
{
    public static class Common
    {
        public static FaceRecognition fr;
        public static string FaceRecognitionConnection= @"C:\FRDB ";
        public static string StudentDatabaseConnection;
        public static void Init(){fr=FaceRecognition.Create(FaceRecognitionConnection);}
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
        public string Code,Name;
        public int year;
        public Subject(int size,string code,string n,int y)
        {
            Students=new Person[size];
            Code=code;
            Name=n;
            year=y;
        }
        public int CompareTo(Subject other)=>Code.CompareTo(other.Code);
        public bool Equals(Subject other)=>Code.Equals(other.Code);
        public override int GetHashCode()=>Code.GetHashCode();
        public AttendanceReport TakeAttendance(Image img)
        {
            AttendanceReport r=new AttendanceReport(this);
            int j,Slen;
            r.Date=DateTime.Now;
            var faces=Common.fr.FaceEncodings(img);
            Slen=Students.Length;
            foreach(var face in faces)
            {
                for(j=0;j<Slen;j++)
                {
                    if(!r.Present[j])
                    {
                        r.Present[j]=FaceRecognition.CompareFace(Students[j].Face,face);
                    }
                }
            }
            return r;
        }
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
    }
}