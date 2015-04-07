<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StatusPlataforma.aspx.cs" Inherits="SpediaWeb.Pages.StatusPlataforma" %>
<asp:Content ID="StatusPlataformaCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="StatusPlataforma" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-7">
            <h1>Status da Plataforma</h1>
        </div>
        <!--<div class="col-xs-5 vertical-adjustment custom-nav">
            <span class="active"><a href="#">Status da Plataforma</a></span>
            <span><a href="ExtratoUso">Extrato de Uso</a></span>
            <span><a href="#">Logs do Sistema</a></span>
        </div>-->
    </div>
    <div class="row line-space">
        <div class="col-xs-12">
            <asp:Repeater runat="server" ID="RptPlataforma" OnItemDataBound="RptPlataforma_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Descrição</th>
                            <th>Data do Status</th>
                            <th>Situação</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblDescricao" /></td>
                        <td>
                            <span class="glyphicon glyphicon-calendar"></span>
                            <asp:Label runat="server" ID="LblDataStatus" />
                        </td>
                        <td>
                            <div runat="server" id="DivStatusOk" visible="false" class="button-green border-radius button-green-text" >
                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                <span>OPERANTE</span>
                            </div>
                            <div runat="server" id="DivStatusAtencao" visible="false" class="button-yellow border-radius button-yellow-text" >
                                <span class="glyphicon glyphicon-warning-sign" aria-hidden="true"></span>
                                <span>OPERANDO PARCIALMENTE</span>
                            </div>
                            <div runat="server" id="DivStatusErro" visible="false" class="button-red border-radius button-red-text" >
                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                <span>INOPERANTE</span>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
