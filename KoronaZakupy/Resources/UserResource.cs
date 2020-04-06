using System;
using KoronaZakupy.Resources.Interfaces;

namespace KoronaZakupy.Resources {
    public class UserResource : IUserResource {

        public string Name { get; set; }
        public string Photo { get; set; }
        public double Rating { get; set; }

    }
}
