using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Green_5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => (int[])_marks?.Clone();
            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    return _marks.Average();
                }
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5) return;
                if (_marks == null) return;
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        return;
                    }
                }
            }
            public void Print()
            {
                System.Console.WriteLine($"{Name} {Surname} {AvgMark}");
            }
        }

        public class Group
        {
            private string _name;
            private Student[] _students;
            private int _studentcount;

            public string Name => _name;
            public Student[] Students => _students;
            public virtual double AvgMark
            {
                get
                {
                    if (_students == null || _studentcount == 0) return 0;
                    double sum = 0;
                    int count = 0;
                    for (int i = 0; i < _studentcount; i++)
                    {
                        if (_students[i].Marks != null && _students[i].Marks.Length > 0)
                        {
                            sum += _students[i].AvgMark;
                            count++;
                        }
                    }
                    return count == 0 ? 0 : sum / count;
                }
            }

            public Group(string name)
            {
                _name = name;
                _students = new Student[100];
                _studentcount = 0;
            }
            public void Add(Student student)
            {
                if (_studentcount < 100)
                {
                    _students[_studentcount++] = student;
                }
                else System.Console.WriteLine("Группа переполнена");
            }
            public void Add(Student[] students)
            {
                foreach (Student student in students)
                {
                    if (_studentcount < 100)
                    {
                        _students[_studentcount++] = student;
                    }
                    else System.Console.WriteLine("Группа переполнена");
                }
            }
            public static void SortByAvgMark(Group[] groups)
            {
                for (int i = 0; i < groups.Length - 1; i++)
                {
                    for (int j = 0; j < groups.Length - i - 1; j++)
                    {
                        if (groups[j].AvgMark < groups[j + 1].AvgMark)
                        {
                            Group temp = groups[j];
                            groups[j] = groups[j + 1];
                            groups[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                System.Console.WriteLine($"{Name} {AvgMark}");
                foreach (var student in _students)
                {
                    student.Print();
                }
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name) : base(name) { }

            public override double AvgMark
            {
                get
                {
                    if (Students == null || Students.Length == 0) return 0;
                    double totalWeightedSum = 0;
                    int studentsWithMarksCount = 0;

                    foreach (var student in Students)
                    {
                        if (student.Marks == null || student.Marks.Length == 0) continue;

                        double studentWeightedSum = 0;
                        int marksCount = 0;

                        foreach (var mark in student.Marks)
                        {
                            if (mark == 0) continue;
                            marksCount++;
                            switch (mark)
                            {
                                case 5: studentWeightedSum += 1; break;
                                case 4: studentWeightedSum += 1.5; break;
                                case 3: studentWeightedSum += 2; break;
                                case 2: studentWeightedSum += 2.5; break;
                            }
                        }

                        if (marksCount > 0)
                        {
                            totalWeightedSum += studentWeightedSum / marksCount;
                            studentsWithMarksCount++;
                        }
                    }

                    return studentsWithMarksCount == 0 ? 0 : totalWeightedSum / studentsWithMarksCount;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string name) : base(name) { }

            public new double AvgMark
            {
                get
                {
                    if (Students == null || Students.Length == 0) return 0;
                    double totalWeightedSum = 0;
                    int studentsWithMarksCount = 0;

                    foreach (var student in Students)
                    {
                        if (student.Marks == null || student.Marks.Length == 0) continue;

                        double studentWeightedSum = 0;
                        int marksCount = 0;

                        foreach (var mark in student.Marks)
                        {
                            if (mark == 0) continue;
                            marksCount++;
                            switch (mark)
                            {
                                case 5: studentWeightedSum += 1; break;
                                case 4: studentWeightedSum += 0.75; break;
                                case 3: studentWeightedSum += 0.5; break;
                                case 2: studentWeightedSum += 0.25; break;
                            }
                        }

                        if (marksCount > 0)
                        {
                            totalWeightedSum += studentWeightedSum / marksCount;
                            studentsWithMarksCount++;
                        }
                    }

                    return studentsWithMarksCount == 0 ? 0 : totalWeightedSum / studentsWithMarksCount;
                }
            }
        }
    }
}