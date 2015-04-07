<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCaixaPostal.aspx.cs" Inherits="SpediaWeb.Pages.CadastroCaixaPostal" %>
<asp:Content ID="CadastroCaixaPostalCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="CadastroCaixaPostal" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>Caixas Postais</h1>
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
            <asp:Repeater runat="server" ID="RptCaixaPostal" OnItemDataBound="RptCaixaPostal_ItemDataBound" OnItemCommand="RptCaixaPostal_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Nome da Caixa Postal</th>
                            <th>Endereço de E-mail</th>
                            <th>Endereço IMAP</th>
                            <th>Ações</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblNomeCaixaPostal" /></td>
                        <td><asp:Label runat="server" ID="LblEnderecoEmail" /></td>
                        <td><asp:Label runat="server" ID="LblEnderecoServidor" /></td>
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
                <strong>Cadastrar nova caixa postal</strong>
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
                                <label for="TxtNome">Nome da Caixa Postal</label>
                                <asp:TextBox runat="server" ID="TxtNomeCaixaPostal" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtEnderecoServidor">Endereço IMAP</label>
                                <asp:TextBox runat="server" ID="TxtEnderecoServidor" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPorta">Porta</label>
                                <asp:TextBox runat="server" ID="TxtPorta" MaxLength="10" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <asp:LinkButton runat="server" ID="BtnSalvar" CssClass="btn btn-block button-primary vertical-adjustment" OnClick="BtnSalvar_Click">
                                <span class="glyphicon glyphicon-plus-sign"></span>
                                <span>Adicionar Caixa Postal</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtEmail">Endereço de E-mail</label>
                                <asp:TextBox runat="server" ID="TxtEnderecoEmail" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtSenha">Senha</label>
                                <asp:TextBox runat="server" TextMode="Password" ID="TxtSenha" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
