using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WindowsFormsApp1.models;

namespace WindowsFormsApp1
{
    class PessoaDAO
    {
        MySqlConnection Conexao;
        public bool insira(Pessoa pessoa)
        {
            List<Telefone> telefonesDB = new List<Telefone>();
            Endereco enderecoDB = insiraEndereco(pessoa.getEndereco());
            pessoa.getTelefones().ForEach(telefone =>
            {
                Telefone telefoneDB = insiraTelefone(telefone);
                telefonesDB.Add(telefoneDB);
            });

            bool inseriu = false;

            Pessoa pessoaEncontrada = consulte(pessoa.getCpf());

            if (pessoaEncontrada != null)
            {
                telefonesDB.ForEach(telefoneDB => insiraPessoaTelefone(telefoneDB, pessoaEncontrada));
                return inseriu;
            }
            else
            {
                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "INSERT INTO pessoa (nome, cpf, endereco) VALUES (@nome, @cpf, @endereco)";
                    cmd.Parameters.AddWithValue("@nome", pessoa.getNome());
                    cmd.Parameters.AddWithValue("@cpf", pessoa.getCpf());
                    cmd.Parameters.AddWithValue("@endereco", enderecoDB.getIdEndereco());

                    cmd.Prepare();
                    inseriu = cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }
            }

            Pessoa pessoaDB = consulte(pessoa.getCpf());

            telefonesDB.ForEach(telefoneDB => insiraPessoaTelefone(telefoneDB, pessoaDB));

            return inseriu;
        }

        public Pessoa consulte(long cpf)
        {
            Pessoa pessoaEncontrada = null;

            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM pessoa WHERE cpf = @cpf";

                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pessoaEncontrada = new Pessoa(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), new Endereco(reader.GetInt32(3), null, 0, 0, null, null, null), null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }

            if (pessoaEncontrada != null)
            {
                pessoaEncontrada.setEndereco(consulteEnderecoPorId(pessoaEncontrada.getEndereco().getIdEndereco()));
                pessoaEncontrada.setTelefones(buscaTelefonesPorIdPessoa(pessoaEncontrada.getIdPessoa()));
            }

            return pessoaEncontrada;
        }

        public bool altere(Pessoa pessoa)
        {
            Pessoa pessoaEncontrada = consulte(pessoa.getCpf());

            bool salvou = false;

            if (pessoaEncontrada != null)
            {
                altereEndereco(pessoaEncontrada.getEndereco(), pessoa.getEndereco());

                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "UPDATE pessoa SET nome = @nome WHERE idpessoa = @idpessoa";
                    cmd.Parameters.AddWithValue("@idpessoa", pessoaEncontrada.getIdPessoa());
                    cmd.Parameters.AddWithValue("@nome", pessoa.getNome());

                    cmd.Prepare();
                    salvou = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }
            }

            return salvou;

        }

        public bool exclua(Pessoa pessoa)
        {
            bool deletou = false;

            excluaPessoaTelefonePorIdPessoa(pessoa.getIdPessoa());

            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "DELETE FROM pessoa WHERE idpessoa = @idpessoa";
                cmd.Parameters.AddWithValue("@idpessoa", pessoa.getIdPessoa());

                cmd.Prepare();
                deletou = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }

            return deletou;
        }

        private void insiraPessoaTelefone(Telefone telefone, Pessoa pessoa)
        {
            bool pessoaTelefoneEncontrado = consultePessoaTelefone(telefone.getIdTelefone(), pessoa.getIdPessoa());

            if (!pessoaTelefoneEncontrado)
            {
                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "INSERT INTO pessoa_telefone (idtelefone, idpessoa) VALUES (@idtelefone, @idpessoa)";
                    cmd.Parameters.AddWithValue("@idtelefone", telefone.getIdTelefone());
                    cmd.Parameters.AddWithValue("@idpessoa", pessoa.getIdPessoa());

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }
            }
        }

        private bool consultePessoaTelefone(int idTelefone, int idPessoa)
        {
            bool encontrou = false;
            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM pessoa_telefone WHERE idtelefone = @idtelefone AND idpessoa = @idpessoa";

                cmd.Parameters.AddWithValue("@idtelefone", idTelefone);
                cmd.Parameters.AddWithValue("@idpessoa", idPessoa);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    encontrou = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
            return encontrou;
        }

        private Endereco insiraEndereco(Endereco endereco)
        {
            Endereco enderecoEncontrado = consulteEndereco(endereco);

            if (enderecoEncontrado != null)
            {
                return enderecoEncontrado;
            }
            else
            {
                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "INSERT INTO endereco (logradouro, numero, cep, bairro, cidade, estado) VALUES (@logradouro, @numero, @cep, @bairro, @cidade, @estado)";
                    cmd.Parameters.AddWithValue("@logradouro", endereco.getLogradouro());
                    cmd.Parameters.AddWithValue("@numero", endereco.getNumero());
                    cmd.Parameters.AddWithValue("@cep", endereco.getCep());
                    cmd.Parameters.AddWithValue("@bairro", endereco.getBairro());
                    cmd.Parameters.AddWithValue("@cidade", endereco.getCidade());
                    cmd.Parameters.AddWithValue("@estado", endereco.getEstado());

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }
                enderecoEncontrado = consulteEndereco(endereco);
                return enderecoEncontrado;
            }
        }

        private Telefone insiraTelefone(Telefone telefone)
        {
            TipoTelefone tipoTelefone = insiraTipoTelefone(telefone.getTipo());
            telefone.setTipo(tipoTelefone);
            Telefone telefoneEncontrado = consulteTelefone(telefone);

            if (telefoneEncontrado != null)
            {
                return telefoneEncontrado;
            }
            else
            {
                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "INSERT INTO telefone (numero, ddd, tipo) VALUES (@numero, @ddd, @tipo)";
                    cmd.Parameters.AddWithValue("@ddd", telefone.getDdd());
                    cmd.Parameters.AddWithValue("@numero", telefone.getNumero());
                    cmd.Parameters.AddWithValue("@tipo", tipoTelefone.getIdTipoTelefone());

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }
                return consulteTelefone(telefone);
            }
        }

        private Telefone consulteTelefone(Telefone telefone)
        {
            Telefone telefoneEncontrado = null;
            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM telefone WHERE numero = @numero AND ddd = @ddd AND tipo = @tipo";

                cmd.Parameters.AddWithValue("@numero", telefone.getNumero());
                cmd.Parameters.AddWithValue("@ddd", telefone.getDdd());
                cmd.Parameters.AddWithValue("@tipo", telefone.getTipo().getIdTipoTelefone());
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    telefoneEncontrado = new Telefone(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
            return telefoneEncontrado;
        }

        private TipoTelefone insiraTipoTelefone(TipoTelefone tipoTelefone)
        {
            TipoTelefone tipoEncontrado = consulteTipoTelefone(tipoTelefone);

            if (tipoEncontrado != null)
            {
                return tipoEncontrado;
            }
            else
            {
                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "INSERT INTO telefone_tipo (tipo) VALUES (@tipo)";
                    cmd.Parameters.AddWithValue("@tipo", tipoTelefone.getTipo());

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }

                return consulteTipoTelefone(tipoTelefone);
            }
        }

        private TipoTelefone consulteTipoTelefone(TipoTelefone tipoTelefone)
        {
            TipoTelefone tipoEncontrado = null;
            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM telefone_tipo WHERE tipo = @tipo ";

                cmd.Parameters.AddWithValue("@tipo", tipoTelefone.getTipo());
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tipoEncontrado = new TipoTelefone(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
            return tipoEncontrado;
        }

        private Endereco consulteEndereco(Endereco endereco)
        {
            Endereco enderecoEncontrado = null;

            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM endereco WHERE logradouro = @logradouro AND numero = @numero AND cidade = @cidade AND cep = @cep AND bairro = @bairro AND estado = @estado";

                cmd.Parameters.AddWithValue("@logradouro", endereco.getLogradouro());
                cmd.Parameters.AddWithValue("@numero", endereco.getNumero());
                cmd.Parameters.AddWithValue("@cidade", endereco.getCidade());
                cmd.Parameters.AddWithValue("@cep", endereco.getCep());
                cmd.Parameters.AddWithValue("@bairro", endereco.getBairro());
                cmd.Parameters.AddWithValue("@estado", endereco.getEstado());
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    enderecoEncontrado = new Endereco(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
            return enderecoEncontrado;
        }

        private Endereco consulteEnderecoPorId(int idEndereco)
        {
            Endereco enderecoEncontrado = null;

            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM endereco WHERE idendereco = @idendereco";

                cmd.Parameters.AddWithValue("@idendereco", idEndereco);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    enderecoEncontrado = new Endereco(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
            return enderecoEncontrado;
        }

        private List<Telefone> buscaTelefonesPorIdPessoa(int idPessoa)
        {
            List<Telefone> telefones = new List<Telefone>();
            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT idtelefone FROM pessoa_telefone WHERE idpessoa = @id ";

                cmd.Parameters.AddWithValue("@id", idPessoa);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    telefones.Add(buscaTelefonePorIdTelefone(reader.GetInt32(0)));
                }
                return telefones;
            }
            catch (Exception)
            {
                return telefones;
            }
            finally
            {
                Conexao.Close();
            }
        }

        private Telefone buscaTelefonePorIdTelefone(int id)
        {
            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM telefone WHERE idtelefone = @id ";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Telefone(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        buscaTipoTelefonePorId(reader.GetInt32(3))
                        );
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conexao.Close();
            }
        }

        private TipoTelefone buscaTipoTelefonePorId(int id)
        {
            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM telefone_tipo WHERE idtelefone_tipo = @id ";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new TipoTelefone(
                        reader.GetInt32(0),
                        reader.GetString(1)
                        );
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conexao.Close();
            }
        }
        private void altereEndereco(Endereco enderecoAntigo, Endereco enderecoNovo)
        {
            Endereco enderecoEncontrado = consulteEndereco(enderecoAntigo);

            if (enderecoEncontrado != null)
            {
                try
                {
                    Conexao = new GetDatabaseConnection().getDatabaseConnection();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.CommandText = "UPDATE endereco SET logradouro = @logradouro, numero = @numero, cep = @cep, bairro = @bairro, cidade = @cidade, estado = @estado where idendereco = @idendereco";

                    cmd.Parameters.AddWithValue("@idendereco", enderecoAntigo.getIdEndereco());
                    cmd.Parameters.AddWithValue("@logradouro", enderecoNovo.getLogradouro());
                    cmd.Parameters.AddWithValue("@numero", enderecoNovo.getNumero());
                    cmd.Parameters.AddWithValue("@cep", enderecoNovo.getCep());
                    cmd.Parameters.AddWithValue("@bairro", enderecoNovo.getBairro());
                    cmd.Parameters.AddWithValue("@cidade", enderecoNovo.getCidade());
                    cmd.Parameters.AddWithValue("@estado", enderecoNovo.getEstado());
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Conexao.Close();
                }
            }
        }

        private void excluaPessoaTelefonePorIdPessoa(int idPessoa)
        {

            try
            {
                Conexao = new GetDatabaseConnection().getDatabaseConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "DELETE FROM pessoa_telefone WHERE idpessoa = @idpessoa";
                cmd.Parameters.AddWithValue("@idpessoa", idPessoa);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conexao.Close();
            }

        }
    }

}
