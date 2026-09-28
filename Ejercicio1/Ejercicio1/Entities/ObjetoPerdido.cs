public class ObjetoPerdido{
    private int id;
    private string description;
    private string categoria;
    private string lugarEncontrado;
    private DateOnly fechaEncontrado;
    private bool reclamado;
    private string nombrePersonaQueRetiro;

    public int Id { get => id; set => id = value; }
    public string Description { get => description; set => description = value; }
    public string Categoria { get => categoria; set => categoria = value; }
    public string LugarEncontrado { get => lugarEncontrado; set => lugarEncontrado = value; }
    public DateOnly FechaEncontrado { get => fechaEncontrado; set => fechaEncontrado = value; }
    public bool Reclamado { get => reclamado; set => reclamado = value; }
    public string NombrePersonaQueRetiro { get => nombrePersonaQueRetiro; set => nombrePersonaQueRetiro = value; }
}