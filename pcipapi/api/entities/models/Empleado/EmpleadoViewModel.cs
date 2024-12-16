namespace entities.models.Empleado
{
    public class EmpleadoViewModel : entities.Empleado
    {
        public string empleadoApellidoNombre
        {
            get
            {
                return string.Format("{0} {1}", this.empleadoApellidos, this.empleadoNombre);
            }
        }

        public string empleadoNombreApellido
        {
            get
            {
                return string.Format("{0} {1}", this.empleadoNombre, this.empleadoApellidos);
            }
        }

        public string puestoNombre { get; set; } = string.Empty;
        public int? totalRegistros { get; set; } = null;
    }
}