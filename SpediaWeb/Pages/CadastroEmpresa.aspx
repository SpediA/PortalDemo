<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroEmpresa.aspx.cs" Inherits="SpediaWeb.Pages.CadastroEmpresa" %>
<asp:Content ID="CadastroEmpresaCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="CadastroEmpresa" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>Empresas Cadastradas</h1>
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
            <asp:Repeater runat="server" ID="RptEmpresa" OnItemDataBound="RptEmpresa_ItemDataBound" OnItemCommand="RptEmpresa_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Razão Social</th>
                            <th>CNPJ</th>
                            <th>I.E.</th>
                            <th>U.F.</th>
                            <th>Ações</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblRazaoSocial" /></td>
                        <td><asp:Label runat="server" ID="LblCnpj" /></td>
                        <td><asp:Label runat="server" ID="LblInscricaoEstadual" /></td>
                        <td><asp:Label runat="server" ID="LblUnidadeFederativa" /></td>
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
                <strong>Cadastrar nova empresa</strong>
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
                                <label for="TxtRazaoSocial">Razão Social</label>
                                <asp:TextBox runat="server" ID="TxtRazaoSocial" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtCnpj">CNPJ</label>
                                <asp:TextBox runat="server" ID="TxtCnpj" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtInscricaoEstadual">I.E.</label>
                                <asp:TextBox runat="server" ID="TxtInscricaoEstadual" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <asp:LinkButton runat="server" ID="BtnSalvar" CssClass="btn btn-block button-primary vertical-adjustment" OnClick="BtnSalvar_Click">
                                <span class="glyphicon glyphicon-plus-sign"></span>
                                <span>Salvar Empresa</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtMunicipio">Município</label>
                                <asp:TextBox runat="server" ID="TxtMunicipio" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="DdUnidadeFederativa">U.F.</label>
                                <asp:DropDownList runat="server" ID="DdUnidadeFederativa" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
