namespace entities.models.Contacto
{
    public class ContactoViewModel : entities.Contacto
    {
        public string contactoNombreApellidos
        {
            get
            {
                return string.Format("{0} {1}", this.contactoNombre, this.contactoApellidos);
            }
        }
    }
}