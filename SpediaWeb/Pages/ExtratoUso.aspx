<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExtratoUso.aspx.cs" Inherits="SpediaWeb.Pages.ExtratoUso" %>
<asp:Content ID="ExtratoUsoCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
</asp:Content>
<asp:Content ID="ExtratoUso" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-7">
            <h1>Extrato de Uso</h1>
        </div>
        <div class="col-xs-5 vertical-adjustment custom-nav">
            <span><a href="StatusPlataforma">Status da Plataforma</a></span>
            <span class="active"><a href="#">Extrato de Uso</a></span>
            <span><a href="#">Logs do Sistema</a></span>
        </div>
    </div>
    <div class="row line-space">
        <div class="col-xs-3">
            <span>Plano contratado:</span>
            <strong><asp:Label runat="server" ID="LblPlanoContratado" Text="Spedia Plus" CssClass="secondary-color" /></strong>
        </div>
        <div class="col-xs-4">
            <div class="progress" style="height: 25px">
                <div runat="server" id="DivContaNormal" class="progress-bar progress-bar-success progress-bar-striped" style="width: 60%">
                </div>
                <div runat="server" id="DivContaAlerta" class="progress-bar progress-bar-warning progress-bar-striped" style="width: 30%">
                    <asp:Label runat="server" ID="LblValoresConsumo" />
                </div>
                <div runat="server" id="DivContaExceder" class="progress-bar progress-bar-danger progress-bar-striped" style="width: 1%">
                </div>
            </div>
        </div>
        <div class="col-xs-5">
            <strong><asp:Label runat="server" ID="LblQuantidadeDocumentosEnviados" Text="4532" CssClass="secondary-color" /></strong>
            <span>Documentos enviados no sistema Spedia</span>
        </div>
    </div>
    <div class="row line-space">
        <div class="col-xs-12">
            <asp:Repeater runat="server" ID="RptExtrato" >
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Descrição</th>
                            <th>Usuário</th>
                            <th>Horário</th>
                            <th>Data do Envio</th>
                            <th>Situação</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:Label runat="server" ID="LblDescricao" /></td>
                        <td><asp:Label runat="server" ID="LblUsuario" /></td>
                        <td>
                            <span class="glyphicon glyphicon-time"></span>
                            <asp:Label runat="server" ID="LblHorario" />
                        </td>
                        <td>
                            <span class="glyphicon glyphicon-calendar"></span>
                            <asp:Label runat="server" ID="LblDataEnvio" />
                        </td>
                        <td>
                            <div runat="server" id="DivStatusOk" visible="false" class="button-green border-radius button-green-text" >
                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                <span>ENVIADO PARA O SPEDIA</span>
                            </div>
                            <div runat="server" id="DivStatusAtencao" visible="false" class="button-yellow border-radius button-yellow-text" >
                                <span class="glyphicon glyphicon-warning-sign" aria-hidden="true"></span>
                                <span>EDITADO</span>
                            </div>
                            <div runat="server" id="DivStatusErro" visible="false" class="button-red border-radius button-red-text" >
                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                <span>EXCLUÍDO</span>
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
