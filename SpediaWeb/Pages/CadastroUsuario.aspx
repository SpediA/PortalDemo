<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" Inherits="SpediaWeb.Pages.CadastroUsuario" %>
<asp:Content ID="CadastroUsuarioCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="CadastroUsuario" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>Usuários Cadastrados</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-6 col-xs-offset-3">
            <div runat="server" id="DivMensagem" role="alert" visible="false">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label runat="server" ID="LblMensagem" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Repeater runat="server" ID="RptUsuario" OnItemDataBound="RptUsuario_ItemDataBound" OnItemCommand="RptUsuario_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Nome</th>
                            <th>E-mail</th>
                            <th>Perfil</th>
                            <th>Ações</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblNome" /></td>
                        <td><asp:Label runat="server" ID="LblEmail" /></td>
                        <td><asp:Label runat="server" ID="LblPerfil" /></td>
                        <td>
                            <asp:LinkButton runat="server" ID="BtnEditar" CssClass="btn button-yellow" CommandName="Editar">
                                <span class="glyphicon glyphicon-pencil"></span>
                                <span>Editar</span>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="BtnExcluir" CssClass="btn button-red" CommandName="Excluir">
                                <span class="glyphicon glyphicon-trash"></span>
                                <span>Excluir</span>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h3 class="primary-color">
                <span class="glyphicon glyphicon-plus-sign"></span>
                <strong>Cadastrar novo usuário</strong>
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtNome">Nome do usuário</label>
                                <asp:TextBox runat="server" ID="TxtNome" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtEmail">E-mail</label>
                                <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="DdPerfil">Perfil</label>
                                <asp:DropDownList runat="server" ID="DdPerfil" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <asp:LinkButton runat="server" ID="BtnSalvar" CssClass="btn btn-block button-primary vertical-adjustment" OnClick="BtnSalvar_Click">
                                <span class="glyphicon glyphicon-plus-sign"></span>
                                <span>Salvar Usuário</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-9">
                            <div class="form-group">
                                <label for="TxtPermissoes">Lista de permissões</label>
                                <asp:TextBox runat="server" ID="TxtPermissoes" data-role="tagsinput" CssClass="form-control" placeHolder="Digite aqui a lista de CNPJ e/ou nome do agrupamento que deseja fornecer o acesso" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        var atributos = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Valor'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: '<%= SpediaWeb.Presentation.Common.ConstantesGlobais.CAMINHO_ATRIBUTOS_ADICIONAIS %>'
        });
        atributos.clearPrefetchCache();
        atributos.initialize();

        $('#<%= TxtPermissoes.ClientID %>').tagsinput({
            itemValue: 'Id',
            itemText: 'Valor',
            tagClass: 'label button-secondary',
            typeaheadjs: {
                name: 'atributos',
                displayKey: 'Valor',
                source: atributos.ttAdapter()
            }
        });

        function defineAtributos(atributos) {
            $('#<%= TxtPermissoes.ClientID %>').tagsinput('removeAll');

            for (i = 0; i < atributos.length; i++) {
                $('#<%= TxtPermissoes.ClientID %>').tagsinput('add', { "Id": atributos[i].Id, "Valor": atributos[i].Valor });
            }
        }

    </script>
</asp:Content>