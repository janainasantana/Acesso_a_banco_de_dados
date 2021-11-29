namespace WindowsFormsApp1
{
    class Telefone
    {
        private int idTelefone { get; set; }
        private int ddd { get; set; }
        private  int numero { get; set; }
        private  TipoTelefone tipo { get; set; }

        public Telefone(int idTelefone, int ddd, int numero, TipoTelefone tipo)
        {
            this.idTelefone = idTelefone;
            this.ddd = ddd;
            this.numero = numero;
            this.tipo = tipo;
        }

        public Telefone(int ddd, int numero, TipoTelefone tipo)
        {
            this.ddd = ddd;
            this.numero = numero;
            this.tipo = tipo;
        }

        public int getIdTelefone()
        {
            return this.idTelefone;
        }

        public void setIdTelefone(int idTelefone)
        {
            this.idTelefone = idTelefone;
        }

        public int getDdd()
        {
            return this.ddd;
        }

        public void setDdd(int ddd)
        {
            this.ddd = ddd;
        }

        public int getNumero()
        {
            return this.numero;
        }

        public void setNumero(int numero)
        {
            this.numero = numero;
        }

        public TipoTelefone getTipo()
        {
            return this.tipo;
        }

        public void setTipo(TipoTelefone tipo)
        {
            this.tipo = tipo;
        }
    }
}
