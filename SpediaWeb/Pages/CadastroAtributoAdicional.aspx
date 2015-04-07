<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroAtributoAdicional.aspx.cs" Inherits="SpediaWeb.Pages.CadastroAtributoAdicional" %>
<asp:Content ID="AtributoAdicionalCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="AtributoAdicional" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>Atributos Adicionais Cadastrados</h1>
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
            <asp:Repeater runat="server" ID="RptAtributoAdicional" OnItemDataBound="RptAtributoAdicional_ItemDataBound" OnItemCommand="RptAtributoAdicional_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Nome</th>
                            <th>Ações</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblNome" /></td>
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
                <strong>Cadastrar novo atributo adicional</strong>
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-9">
                            <div class="form-group">
                                <label for="TxtNome">Nome do atributo</label>
                                <asp:TextBox runat="server" ID="TxtNome" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <asp:LinkButton runat="server" ID="BtnSalvar" CssClass="btn btn-block button-primary vertical-adjustment" OnClick="BtnSalvar_Click">
                                <span class="glyphicon glyphicon-plus-sign"></span>
                                <span>Salvar Atributo</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-9">
                            <div class="form-group">
                                <label for="TxtValores">Lista de valores</label>
                                <asp:TextBox runat="server" ID="TxtValores" data-role="tagsinput" CssClass="form-control" placeHolder="Digite aqui a lista valores para o atributo adicional" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $('#<%= TxtValores.ClientID %>').tagsinput({
            tagClass: 'label button-secondary'
        });

    </script>
</asp:Content>
