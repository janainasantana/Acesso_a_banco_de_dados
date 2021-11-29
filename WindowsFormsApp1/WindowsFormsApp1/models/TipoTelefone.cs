namespace WindowsFormsApp1
{
    class TipoTelefone
    {
        private int idTipoTelefone { get; set; }
        private string tipo { get; set; }

        public TipoTelefone(int idTipoTelefone, string tipo)
        {
            this.idTipoTelefone = idTipoTelefone;
            this.tipo = tipo;
        }

        public int getIdTipoTelefone()
        {
            return this.idTipoTelefone;
        }

        public void setIdTipoTelefone(int idTipoTelefone)
        {
            this.idTipoTelefone = idTipoTelefone;
        }

        public TipoTelefone(string tipo)
        {
            this.tipo = tipo;
        }

        public string getTipo()
        {
            return this.tipo;
        }
    }
}
