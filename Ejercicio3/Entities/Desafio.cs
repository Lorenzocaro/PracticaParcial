//● Id ● Titulo ● Dificultad ● PuntajeMaximo ● Activo 

public class Desafio
{
    private int id;
    private string titulo;
    private string dificultad;
    private int puntajeMaximo;
    private bool activo;

    public int Id { get => id; set => id = value; }
    public string Titulo { get => titulo; set => titulo = value; }
    public string Dificultad { get => dificultad; set => dificultad = value; }
    public int PuntajeMaximo { get => puntajeMaximo; set => puntajeMaximo = value; }
    public bool Activo { get => activo; set => activo = value; }
}