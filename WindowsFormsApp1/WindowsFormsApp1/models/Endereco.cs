namespace WindowsFormsApp1
{
    class Endereco
    {
        private int idEndereco { get; set; }
        private string logradouro { get; set; }
        private int numero { get; set; }
        private int cep { get; set; }
        private string bairro { get; set; }
        private string cidade { get; set; }
        private string estado { get; set; }

        public Endereco(int idEndereco, string logradouro, int numero, int cep, string bairro, string cidade, string estado)
        {
            this.idEndereco = idEndereco;
            this.logradouro = logradouro;
            this.numero = numero;
            this.cep = cep;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
        }

        public Endereco(string logradouro, int numero, int cep, string bairro, string cidade, string estado)
        {
            this.logradouro = logradouro;
            this.numero = numero;
            this.cep = cep;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
        }

        public int getIdEndereco()
        {
            return this.idEndereco;
        }

        public void setIdEndereco(int idEndereco)
        {
            this.idEndereco = idEndereco;
        }

        public string getLogradouro()
        {
            return this.logradouro;
        }

        public void setLogradouro(string logradouro)
        {
            this.logradouro = logradouro;
        }

        public int getNumero()
        {
            return this.numero;
        }

        public void setNumero(int numero)
        {
            this.numero = numero;
        }

        public int getCep()
        {
            return this.cep;
        }

        public void setCep(int cep)
        {
            this.cep = cep;
        }

        public string getBairro()
        {
            return this.bairro;
        }

        public void setBairro(string bairro)
        {
            this.bairro = bairro;
        }

        public string getCidade()
        {
            return this.cidade;
        }

        public void setCidade(string cidade)
        {
            this.cidade = cidade;
        }

        public string getEstado()
        {
            return this.estado;
        }

        public void setEstado(string estado)
        {
            this.estado = estado;
        }
    }
}
