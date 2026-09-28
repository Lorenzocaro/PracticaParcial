public class Estacion
{
    private  int id; 
    private string nombre; 
    private string localidad; 
    private bool activa;

    public int Id { get => Id; set => Id = value; }
    public string Nombre { get => Nombre; set => Nombre = value; }
    public string Localidad { get => Localidad; set => Localidad = value; }
    public bool Activa { get => Activa; set => Activa = value; }
}