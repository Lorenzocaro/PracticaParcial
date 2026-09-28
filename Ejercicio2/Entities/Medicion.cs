public class Medicion
{
    private int id;
    private int estacionId;
    private float temperatura;
    private float humedad;
    private float velocidadViento;
    private DateTime fechaHora;

    public int Id { get => id; set => id = value; }
    public int EstacionId { get => estacionId; set => estacionId = value; }
    public float Temperatura { get => temperatura; set => temperatura = value; }
    public float Humedad { get => humedad; set => humedad = value; }
    public float VelocidadViento { get => velocidadViento; set => velocidadViento = value; }
    public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
}
