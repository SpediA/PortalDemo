<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notificacao.aspx.cs" Inherits="SpediaWeb.Pages.Notificacao" %>
<asp:Content ID="NotificacaoCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="Notificacao" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1>Notificações</h1>
        </div>
    </div>
    <div class="row line-space">
        <div class="col-xs-12">
            <asp:Repeater runat="server" ID="RptNotificacao" OnItemDataBound="RptNotificacao_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Descrição</th>
                            <th>Usuário</th>
                            <th>Horário</th>
                            <th>Data</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblDescricao" /></td>
                        <td><asp:Label runat="server" ID="LblUsuario" /></td>
                        <td>
                            <span class="glyphicon glyphicon-time"></span>
                            <asp:Label runat="server" ID="LblHoraNotificacao" />
                        </td>
                        <td>
                            <span class="glyphicon glyphicon-calendar"></span>
                            <asp:Label runat="server" ID="LblDataNotificacao" />
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
