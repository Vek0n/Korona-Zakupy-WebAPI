using System;
namespace KoronaZakupy.Resources {
    public interface IUserResource {

        public string Name { get; set; }
        public string Photo { get; set; }
        public double Rating { get; set; }
    }
}
