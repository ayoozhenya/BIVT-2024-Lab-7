using System;
using System.Collections.Generic;
using System.Linq;
namespace Lab_7
{
    public abstract class Discipline
    {
        private string _name;
        private Participant[] _participants;

        public string Name => _name;
        public Participant[] Participants => (Participant[])_participants?.Clone();

        protected Discipline(string name)
        {
            _name = name;
            _participants = Array.Empty<Participant>();
        }

        public void Add(Participant participant)
        {
            if (_participants == null)
            {
                _participants = new Participant[] { participant };
            }
            else
            {
                var newParticipants = new Participant[_participants.Length + 1];
                Array.Copy(_participants, newParticipants, _participants.Length);
                newParticipants[^1] = participant;
                _participants = newParticipants;
            }
        }

        public void Add(params Participant[] participants)
        {
            if (_participants == null)
            {
                _participants = (Participant[])participants.Clone();
            }
            else
            {
                var newParticipants = new Participant[_participants.Length + participants.Length];
                Array.Copy(_participants, newParticipants, _participants.Length);
                Array.Copy(participants, 0, newParticipants, _participants.Length, participants.Length);
                _participants = newParticipants;
            }
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

        protected void UpdateParticipant(int index, Participant newParticipant)
        {
            if (_participants != null && index >= 0 && index < _participants.Length)
            {
                _participants[index] = newParticipant;
            }
        }
    }

    public class LongJump : Discipline
    {
        public LongJump() : base("Long jump") { }

        public override void Retry(int index)
        {
            if (Participants == null || index < 0 || index >= Participants.Length)
                return;

            var participant = Participants[index];
            var jumpsList = participant.Jumps.ToList();
            jumpsList.Add(0);
            jumpsList.Add(0);
            
            var newParticipant = new Participant(participant.Name, participant.Surname);
            foreach (var jump in jumpsList)
            {
                if (jump != 0)
                {
                    newParticipant.Jump(jump);
                }
            }
            
            UpdateParticipant(index, newParticipant);
        }
    }

    public class HighJump : Discipline
    {
        public HighJump() : base("High jump") { }

        public override void Retry(int index)
        {
            if (Participants == null || index < 0 || index >= Participants.Length)
                return;

            var participant = Participants[index];
            var jumps = participant.Jumps;
            
            if (jumps.Length > 0)
            {
                var newParticipant = new Participant(participant.Name, participant.Surname);
                for (int i = 0; i < jumps.Length - 1; i++)
                {
                    newParticipant.Jump(jumps[i]);
                }
                
                UpdateParticipant(index, newParticipant);
            }
        }
    }

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
                if (_jumps != null && _jumps.Length > 0)
                {
                    return _jumps.Max();
                }
                return 0;
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
            if (_jumps == null) return;
            for (int i = 0; i < _jumps.Length; i++)
            {
                if (_jumps[i] == 0)
                {
                    _jumps[i] = result;
                    return;
                }
            }
            
            Array.Resize(ref _jumps, _jumps.Length + 1);
            _jumps[^1] = result;
        }

        public static void Sort(Participant[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j].BestJump < array[j + 1].BestJump)
                    {
                        Participant temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"{Name} {Surname} {BestJump}");
        }
    }
}