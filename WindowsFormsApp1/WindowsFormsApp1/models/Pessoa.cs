using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.models
{
    class Pessoa
    {
        private int idPessoa { get; set; }
        private string nome { get; set; }
        private int cpf { get; set; }
        private Endereco endereco { get; set; }
        private List<Telefone> telefones { get; set; }

        public Pessoa(int idPessoa, string nome, int cpf, Endereco endereco, List<Telefone> telefones)
        {
            this.idPessoa = idPessoa;
            this.nome = nome;
            this.cpf = cpf;
            this.endereco = endereco;
            this.telefones = telefones;
        }

        public Pessoa(string nome, int cpf, Endereco endereco, List<Telefone> telefones)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.endereco = endereco;
            this.telefones = telefones;
        }

        public int getIdPessoa()
        {
            return this.idPessoa;
        }

        public void setIdPessoa(int idPessoa)
        {
            this.idPessoa = idPessoa;
        }

        public string getNome()
        {
            return this.nome;
        }

        public void setNome(string nome)
        {
            this.nome = nome;
        }

        public int getCpf()
        {
            return this.cpf;
        }

        public void setCpf(int cpf)
        {
            this.cpf = cpf;
        }

        public Endereco getEndereco()
        {
            return this.endereco;
        }

        public void setEndereco(Endereco endereco)
        {
            this.endereco = endereco;
        }

        public List<Telefone> getTelefones()
        {
            return this.telefones;
        }

        public void setTelefones(List<Telefone> telefones)
        {
            this.telefones = telefones;
        }
    }
}
