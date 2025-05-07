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
            public double[] Jumps => _jumps != null ? (double[])_jumps.Clone() : null;
            public double BestJump => _jumps != null && _jumps.Length > 0 ? _jumps.Max() : 0;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }

            public void Jump(double result)
            {
                if (_jumps == null) return;

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
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {BestJump}");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            public string Name => _name;
            public Participant[] Participants => _participants?.Clone() as Participant[];

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
                Console.WriteLine("Participants:");
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
                if (_participants == null || index < 0 || index >= _participants.Length)
                    return;

                var participant = _participants[index];
                _participants[index] = new Participant(participant.Name, participant.Surname);
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High jump") { }

            public override void Retry(int index)
            {
                if (_participants == null || index < 0 || index >= _participants.Length)
                    return;

                var jumps = _participants[index].Jumps;
                if (jumps == null || jumps.Length == 0)
                    return;

                var newParticipant = new Participant(_participants[index].Name, _participants[index].Surname);
                for (int i = 0; i < jumps.Length - 1; i++)
                {
                    if (jumps[i] != 0)
                    {
                        newParticipant.Jump(jumps[i]);
                    }
                }
                _participants[index] = newParticipant;
            }
        }
    }
}