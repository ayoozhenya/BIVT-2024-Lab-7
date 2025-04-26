using System;
namespace Lab_7
{
    public class Green_3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private int _countex;
            private bool _sessionclosed;
            private static int _nextId = 0;
            private readonly int _id;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => (int[])_marks?.Clone();
            public int ID => _id;
            
            public double AvgMark
            {
                get
                {
                    if (_marks == null || _countex == 0) return 0;
                    double sum = 0;
                    for (int i = 0; i < _countex; i++)
                    {
                        sum += _marks[i];
                    }
                    return sum / _countex;
                }
            }

            public bool IsExpelled
            {
                get
                {
                    if (_countex == 0) return false;
                    for (int i = 0; i < _countex; i++)
                    {
                        if (_marks[i] <= 2) return true;
                    }
                    return false;
                }
            }

            static Student()
            {
                _nextId = 1;
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _sessionclosed = false;
                _countex = 0;
                _id = _nextId++;
            }

            public void Exam(int mark)
            {
                if (_marks.Length == 0) return;
                if (_countex >= 3) return;
                if (!_sessionclosed)
                {
                    if (mark > 2)
                    {
                        _marks[_countex] = mark;
                        _countex++;
                    }
                    else
                    {
                        _marks[_countex] = mark;
                        _countex++;
                        _sessionclosed = true;
                    }
                }
            }

            public void Restore()
            {
                if (IsExpelled)
                {
                    _sessionclosed = false;
                }
            }

            public static void SortByAvgMark(Student[] array)
            {
                if (array == null) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i].AvgMark <= array[i - 1].AvgMark)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Student temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {AvgMark} {IsExpelled} ID: {ID}");
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null) return;
                
                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - i - 1; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }

            public static Student[] Expel(ref Student[] students)
            {
                if (students == null) return new Student[0];
                
                int expelledCount = 0;
                foreach (var student in students)
                {
                    if (student.IsExpelled) expelledCount++;
                }

                Student[] expelled = new Student[expelledCount];
                Student[] remaining = new Student[students.Length - expelledCount];
                
                int e = 0, r = 0;
                foreach (var student in students)
                {
                    if (student.IsExpelled)
                    {
                        expelled[e++] = student;
                    }
                    else
                    {
                        remaining[r++] = student;
                    }
                }

                students = remaining;
                return expelled;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (restored == null || students == null) return;
                bool found = false;
                foreach (var student in students)
                {
                    if (student.ID == restored.ID)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return;
                foreach (var student in students)
                {
                    if (student.ID == restored.ID)
                    {
                        return;
                    }
                }

                restored.Restore();

                Student[] newStudents = new Student[students.Length + 1];
                Array.Copy(students, newStudents, students.Length);
                newStudents[students.Length] = restored;

                Sort(newStudents);

                students = newStudents;
            }
        }
    }
}