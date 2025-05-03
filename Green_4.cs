using System;
using System.Linq;

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
            public double[] Jumps => (double[])_jumps.Clone();
            public double BestJump => _jumps?.Length > 0 ? _jumps.Max() : 0;

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

            public static void Sort(Participant[] array)
            {
                Array.Sort(array, (x, y) => y.BestJump.CompareTo(x.BestJump));
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {BestJump}m");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            public string Name => _name;
            public Participant[] Participants => _participants;

            protected Discipline(string name)
            {
                _name = name;
                _participants = Array.Empty<Participant>();
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(params Participant[] participants)
            {
                int oldLength = _participants.Length;
                Array.Resize(ref _participants, _participants.Length + participants.Length);
                Array.Copy(participants, 0, _participants, oldLength, participants.Length);
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);

            public void Print()
            {
                Console.WriteLine($"Discipline: {Name}");
                Console.WriteLine("Results:");
                foreach (var participant in _participants)
                {
                    participant.Print();
                }
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump") { }

            public override void Retry(int index)
            {
                if (index >= 0 && index < Participants.Length)
                {
                    double best = Participants[index].BestJump;
                    Participants[index].Jump(0);
                    Participants[index].Jump(0);
                    if (Participants[index].BestJump < best)
                    {
                        Participants[index].Jump(best);
                    }
                }
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High jump") { }

            public override void Retry(int index)
            {
                if (index >= 0 && index < Participants.Length)
                {
                    double[] jumps = Participants[index].Jumps;
                    if (jumps.Length > 0)
                    {
                        jumps[^1] = 0;
                        Participants[index].Jump(0);
                    }
                }
            }
        }
    }
}