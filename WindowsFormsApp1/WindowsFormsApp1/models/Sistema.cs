using System.Collections.Generic;
using System;

namespace WindowsFormsApp1.models
{
    class Sistema
    {
        static void Main()
        {
            // insere pessoa
            PessoaDAO pessoaDao = new PessoaDAO();

            TipoTelefone celular = new TipoTelefone("Celular");
            TipoTelefone residencial = new TipoTelefone("Casa");
            Telefone telefone1 = new Telefone(19, 988887777, celular);
            Telefone telefone2 = new Telefone(19, 34445555, residencial);
            List<Telefone> telefones = new List<Telefone>();
            telefones.Add(telefone1);
            telefones.Add(telefone2);
            Endereco endereco = new Endereco("Rua Teste", 123, 13480000, "Centro", "Limeira", "SP");
            Pessoa pessoa = new Pessoa("Nome Teste", 12345678, endereco, telefones);

            bool inseriuPessoa = pessoaDao.insira(pessoa);
            if (inseriuPessoa) {
                Console.WriteLine("Inseriu Pessoa com Sucesso!");
            }

            // consulta pessoa
            Pessoa pessoaEncontrada = pessoaDao.consulte(12345678);
            if (pessoaEncontrada != null)
            {
                Console.WriteLine("Pessoa Encontrada com Sucesso!" + " " + pessoaEncontrada.getIdPessoa() + " - " + pessoaEncontrada.getNome() + " - " + pessoaEncontrada.getEndereco().getBairro());
            }

            // altera pessoa
            pessoaEncontrada.setNome("Novo nome para teste");
            pessoaEncontrada.getEndereco().setBairro("Novo Bairro");
            bool alterou = pessoaDao.altere(pessoaEncontrada);
            if (alterou)
            {
                Console.WriteLine("Alterou Pessoa com Sucesso!");
            }
            Pessoa pessoaAposAlteracao = pessoaDao.consulte(12345678);
            Console.WriteLine("Pessoa Alterada Encontrada com Sucesso!" + " " + pessoaAposAlteracao.getIdPessoa() + " - " + pessoaAposAlteracao.getNome() + " - " + pessoaAposAlteracao.getEndereco().getBairro());

            // exclui pessoa
            bool pessoaexcluida = pessoaDao.exclua(pessoaAposAlteracao);
            if (pessoaexcluida)
            {
                Console.WriteLine("Excluiu Pessoa com Sucesso!");
            }
            Pessoa pessoaAposExclusao = pessoaDao.consulte(12345678);
            if (pessoaAposExclusao != null)
            {
                Console.WriteLine("Problema ao excluir pessoa!");
            }
        }

    }
}
