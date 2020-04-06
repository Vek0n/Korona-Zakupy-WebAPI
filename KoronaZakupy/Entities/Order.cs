using System;
namespace KoronaZakupy.Entities {
    public class Order {
    
        public long Id { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
        public DateTime Date { get; set; }
        public bool IsFinished { get; set; }

    }
}
