<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <title>Página Pessoas</title>
      <!-- Adicionando bootstrap no projeto para torná-lo responsivo -->
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
   </head>
   <body>
      <div class="container">
         <form class="row g-1 col-12" runat="server">
            <div class="row g-3">
               <div class="col-auto">
                  <label for="cpf" class="form-label">CPF:</label>
               </div>
               <div class="col-auto">
                  <input type="text" class="form-control" id="cpf" placeholder="Ex.: 123.456.789-01"/>
               </div>
               <div class="col-auto">
                  <button type="button" class="btn btn-primary">Buscar</button>
               </div>
            </div>
            <div class="row g-2">
               <div class="col-auto">
                  <label for="nome" class="form-label">Nome:</label>
               </div>
               <div class="col-auto">
                  <input type="text" class="form-control" id="nome" size="50" placeholder="Ex.: Maria dos Santos"/>
               </div>
            </div>
            <div class="row g-2">
               <div class="col-auto">
                  <label for="endereco" class="form-label">Endereço:</label>
               </div>
               <div class="col-auto">
                  <input type="text" class="form-control" id="endereco" size="50" placeholder="Ex.: Rua Amarais, 123, Centro, Limeira-SP"/>
               </div>
            </div>
            <div class="row g-1">
               <div class="row justify-content-end">
                  <div class="col-6">
                     <button type="button" class="btn btn-primary">Criar endereço</button>
                  </div>
               </div>
            </div>
            <div class="col-12">
               <label for="inputAddress2" class="form-label">Telefones</label>
               <table class="table table-striped table-bordered">
                  <thead>
                     <tr>
                        <th scope="col">Tipo</th>
                        <th scope="col">DDD</th>
                        <th scope="col">Número</th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr>
                        <th scope="row">Celular</th>
                        <td>19</td>
                        <td>988887777</td>
                     </tr>
                     <tr>
                        <th scope="row">Residencial</th>
                        <td>19</td>
                        <td>34512233</td>
                     </tr>
                     <tr>
                        <th scope="row">Comercial</th>
                        <td>19</td>
                        <td>34523344</td>
                     </tr>
                  </tbody>
               </table>
            </div>
            <div class="row justify-content-end">
               <div class="col-6">
                  <button type="button" class="btn btn-primary">Criar telefone</button>
               </div>
            </div>
            <div class="row justify-content-evenly mt-1">
               <div class="col-6">
                  <!-- Serve para salvar tanto na criação de uma pessoa nova quanto na edição dos campos -->
                  <button type="submit" class="btn btn-primary">Salvar</button>
                  <button type="submit" class="btn btn-danger">Excluir</button>
               </div>
            </div>
         </form>
      </div>
   </body>
</html>