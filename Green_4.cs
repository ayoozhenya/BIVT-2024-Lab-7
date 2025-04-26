using System;
namespace Lab_7
{
    public class Green_4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps => (double[])_jumps?.Clone();
            
            public double BestJump
            {
                get
                {
                    if (_jumps == null || _jumps.Length == 0) return 0;
                    double max = _jumps[0];
                    foreach (double jump in _jumps)
                    {
                        if (jump > max) max = jump;
                    }
                    return max;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }

            public void Jump(double result)
            {
                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0)
                    {
                        _jumps[i] = result;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] participants)
            {
                for (int i = 0; i < participants.Length - 1; i++)
                {
                    for (int j = 0; j < participants.Length - 1 - i; j++)
                    {
                        if (participants[j].BestJump < participants[j + 1].BestJump)
                        {
                            Participant temp = participants[j];
                            participants[j] = participants[j + 1];
                            participants[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}. Лучший прыжок: {BestJump:F1} м");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            protected Participant[] _participants; 

            public string Name => _name;
            public Participant[] Participants => (Participant[])_participants?.Clone();

            protected Discipline(string disciplineName)
            {
                _name = disciplineName;
                _participants = new Participant[0];
            }

            public void Add(Participant newParticipant)
            {
                Participant[] newList = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    newList[i] = _participants[i];
                }
                newList[_participants.Length] = newParticipant;
                _participants = newList;
            }

            public void Add(params Participant[] newParticipants)
            {
                Participant[] newList = new Participant[_participants.Length + newParticipants.Length];
                
                for (int i = 0; i < _participants.Length; i++)
                {
                    newList[i] = _participants[i];
                }
                
                for (int i = 0; i < newParticipants.Length; i++)
                {
                    newList[_participants.Length + i] = newParticipants[i];
                }
                
                _participants = newList;
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int participantIndex);

            protected void UpdateParticipants(Participant[] newParticipants)
            {
                _participants = newParticipants;
            }

            public void Print()
            {
                Console.WriteLine($"Дисциплина: {Name}");
                Console.WriteLine("Участники:");
                foreach (Participant p in _participants)
                {
                    p.Print();
                }
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Прыжки в длину") { }

            public override void Retry(int index)
            {
                if (index < 0 || index >= _participants.Length)
                {
                    Console.WriteLine("Ошибка: неверный индекс участника");
                    return;
                }

                Participant current = _participants[index];
                Participant updated = new Participant(current.Name, current.Surname);
                
                updated.Jump(current.BestJump);
                
                updated.Jump(0);
                updated.Jump(0);

                Participant[] newList = (Participant[])_participants.Clone();
                newList[index] = updated;
                
                UpdateParticipants(newList);
                
                Console.WriteLine($"{current.Name} получил дополнительные попытки!");
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("Прыжки в высоту") { }

            public override void Retry(int index)
            {
                if (index < 0 || index >= _participants.Length)
                {
                    Console.WriteLine("Ошибка: неверный индекс участника");
                    return;
                }

                Participant current = _participants[index];
                double[] jumps = current.Jumps;
                
                int lastJumpIndex = -1;
                for (int i = jumps.Length - 1; i >= 0; i--)
                {
                    if (jumps[i] != 0)
                    {
                        lastJumpIndex = i;
                        break;
                    }
                }

                if (lastJumpIndex == -1)
                {
                    Console.WriteLine("У участника нет выполненных прыжков");
                    return;
                }

                Participant updated = new Participant(current.Name, current.Surname);
                
                for (int i = 0; i < lastJumpIndex; i++)
                {
                    updated.Jump(jumps[i]);
                }
                
                updated.Jump(0);

                Participant[] newList = (Participant[])_participants.Clone();
                newList[index] = updated;
                
                UpdateParticipants(newList);
                
                Console.WriteLine($"{current.Name} может повторить последний прыжок!");
            }
        }
    }
}