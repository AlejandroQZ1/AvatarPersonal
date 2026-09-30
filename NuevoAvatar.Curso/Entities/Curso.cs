namespace NuevoAvatar.Curso.Entities;

public class Curso
{
    public int CursoId { get; set; }
    public int CarreraId { get; set; }
    public byte Nivel { get; set; }
    public string Nombre { get; set; } = string.Empty;
}