namespace NuevoAvatar.Curso.Entities;

public class CursoRequest
{
    public int CarreraId { get; set; }
    public byte Nivel { get; set; }
    public string Nombre { get; set; } = string.Empty;
}
