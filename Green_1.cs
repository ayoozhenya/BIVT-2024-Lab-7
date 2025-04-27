using System;
using System.Linq;
namespace Lab_7
{
    public class Green_1
    {
        public abstract class Participant
        {
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;
            
            protected double standard;
            private static int passedcount;
            
            public Participant(string surname, string group, string trainer)
            {
                _surname = surname; 
                _group = group;
                _trainer = trainer;
                _result = 0;
            }

            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;
            public static int PassedTheStandard => passedcount;
            public bool HasPassed => (_result > 0 && _result <= standard);
            
            public void Run(double result)
            {
                if (_result == 0)
                {
                    _result = result;
                    if (HasPassed)
                    {
                        passedcount++;
                    }
                }
            }
            
            public void Print()
            {
                Console.WriteLine($"{Surname} {Group} {Trainer} {Result} {HasPassed}");
                Console.WriteLine(PassedTheStandard);
            }
            
            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                return participants.Where(p => p.GetType() == participantType && p.Trainer == trainer).ToArray();
            }
        }
        
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                standard = 12;
            }
        }
        
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                standard = 90;
            }
        }
    }
}