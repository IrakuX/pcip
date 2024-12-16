namespace entities.models.Empleado
{
    public class EmpleadoCmbViewModel
    {
        public EmpleadoCmbViewModel()
        {
            this.empleadoId = 0;
            this.empleadoNombreApellidos = String.Empty;
        }

        public int empleadoId { get; set; }
        public string empleadoNombreApellidos { get; set; }
    }
}