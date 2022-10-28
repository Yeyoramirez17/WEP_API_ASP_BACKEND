namespace API.Models;

public class Student {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Identificacion { get; set; }
    public int Edad { get; set; }
    public string Telefono { get; set; }

    public String toString() {
        return $"N°: {Id} , Nombre: {Nombre}, Identificacion: {Identificacion}, Edad : {Edad}, Telefono: {Telefono}";
    }
}