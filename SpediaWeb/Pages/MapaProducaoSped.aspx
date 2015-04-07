<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapaProducaoSped.aspx.cs" Inherits="SpediaWeb.Pages.MapaProducaoSped" %>
<asp:Content ID="MapaProducaoCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="MapaProducao" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>Escrituração Digital</h1>
        </div>
    </div>
    <div class="row line-space">
        <div class="col-xs-10 col-xs-offset-1">
            <asp:Repeater runat="server" ID="RptMapa">
                <HeaderTemplate>
                    <table class="table">
                        <tr class="active">
                            <th>Tipo</th>
                            <th colspan="3" class="text-right">Situação</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td rowspan="2" class="col-xs-8 vmiddle button-secondary">
                            <asp:LinkButton runat="server" ID="BtnEmpresa" CommandName="SelecaoEmpresa" CssClass="text-color" />
                        </td>
                        <td rowspan="2" class="col-xs-2 button-gray text-center">
                            <asp:LinkButton runat="server" ID="BtnTipoEntidade" Text="ENTIDADE" CommandName="SelecaoTipoEntidade" CssClass="text-color" />
                        </td>
                        <td class="col-xs-1 button-red text-center">
                            <asp:LinkButton runat="server" ID="BtnDocumentoInvalidoEntidade" CommandName="SelecaoInvalido" CssClass="text-color" />
                        </td>
                        <td class="col-xs-1 button-green text-center">
                            <asp:LinkButton runat="server" ID="BtnDocumentoValidoEntidade" CommandName="SelecaoValido" CssClass="text-color" />
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
            <span><strong>Legenda</strong></span>
        </div>
    </div>
    <div class="row line-space">
        <div>
            <span class="glyphicon glyphicon-stop button-red-text"></span><small>INVÁLIDOS</small>
            <span class="glyphicon glyphicon-stop button-green-text left-space"></span><small>VÁLIDOS</small>
        </div>
    </div>
</asp:Content>
