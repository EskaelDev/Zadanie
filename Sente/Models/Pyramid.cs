using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sente.Models
{
    [XmlRoot("piramida")]
    public class Pyramid
    {
        [XmlElement("uczestnik")]
        public Participant Founder { get; set; }
        private SortedSet<Participant> sortedParticipants { get; set; }

        public Pyramid()
        {
            sortedParticipants = new SortedSet<Participant>(Comparer<Participant>.Default);
        }

        public void Init(Dictionary<int, List<decimal>> transfers)
        {
            sortedParticipants.Add(Founder);
            Founder.SubordinatesWithoutSubordinatesCount = Init(Founder, transfers);
        }


        private int Init(Participant current, Dictionary<int, List<decimal>> transfers, int level = 0)
        {
            if (transfers.ContainsKey(current.Id))
            {
                DistributeComission(current, transfers);
            }

            current.Subordinates.ForEach(s =>
            {
                s.Superior = current;
                s.Level = level + 1;
                sortedParticipants.Add(s);
                current.SubordinatesWithoutSubordinatesCount += Init(s, transfers, s.Level);
            });

            return current.SubordinatesWithoutSubordinatesCount == 0 ? 1 : current.SubordinatesWithoutSubordinatesCount;
        }

        public bool NoPyramid()
        {
            return this is null || this.Founder == null;
        }

        public void Print()
        {
            foreach (var item in sortedParticipants)
            {
                Console.WriteLine(item);
            }

        }

        private void Addcommission(Stack<Participant> superiors, List<decimal> values)
        {
            if (superiors.Count == 0)
            {
                values.ForEach(v => Founder.Commission += v);
                return;
            }

            Participant current = superiors.Pop(); ;

            if (superiors.Count == 0)
            {
                values.ForEach(v => current.Commission += v);
                return;
            }

            List<decimal> halved = new List<decimal>();
            values.ForEach(v =>
            {
                decimal commission = Math.Floor(v / 2);
                current.Commission += commission;
                halved.Add(v - commission);
            });

            Addcommission(superiors, halved);

        }

        private void DistributeComission(Participant participant, Dictionary<int, List<decimal>> transfers)
        {
            var superiors = new Stack<Participant>();
            FillSuperiorsStack(superiors, participant);
            Addcommission(superiors, transfers[participant.Id]);
            transfers.Remove(participant.Id);
        }

        private void FillSuperiorsStack(Stack<Participant> stack, Participant participant)
        {
            if (participant.Superior is null)
                return;

            stack.Push(participant.Superior);
            FillSuperiorsStack(stack, participant.Superior);
        }
    }
}
