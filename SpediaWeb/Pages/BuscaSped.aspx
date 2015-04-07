<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscaSped.aspx.cs" Inherits="SpediaWeb.Pages.BuscaSped" %>
<asp:Content ID="BuscaSpedCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('#Conteudo_RptResultadoBusca_CboSelecionarTodos').click(function () {
                if ($(this).is(':checked')) {
                    $("#TbResultadoBusca tbody tr td input:checkbox:not(:checked)").each(function () {
                        $(this).prop('checked', true);
                    });
                }
                else {
                    $("#TbResultadoBusca tbody tr td input:checkbox:checked").each(function () {
                        $(this).prop('checked', false);
                    });
                }
            });
        });

        function buildFileInputDigitalCertificate() {
            $('#' + '<%= FuCertificadoDigital.ClientID %>').fileinput({
                browseClass: "btn button-primary",
                browseLabel: "<span style=\"margin-left: 5px;\">Escolha o Arquivo</span>",
                browseIcon: '<span class="glyphicon glyphicon-folder-open"></span>',
                removeClass: "btn button-red",
                removeLabel: "<span style=\"margin-left: 5px;\">Excluir</span>",
                removeIcon: '<span class="glyphicon glyphicon-trash"></span>',
                showPreview: false
            });
        };

        function alternaBuscaAvancada() {
            $('#DivResultadoBusca').css('display', 'none');
            $('#DivBuscaAvancada').css('display', 'block');
        }

        function openModal() {
            $('#propriedadesArquivo').modal('show');
        }

        function openModalDigitalCertificate() {
            buildFileInputDigitalCertificate();
            $('#certificadoDigital').modal('show');
        }

    </script>
</asp:Content>
<asp:Content ID="BuscaSped" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-xs-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="TxtBusca" CssClass="form-control input-lg" placeHolder="Busca..." />
                            <span class="input-group-addon">
                                <asp:LinkButton runat="server" ID="BtnBusca" OnClick="BtnBusca_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="TxtCnpj">CNPJ</label>
                        <asp:TextBox runat="server" ID="TxtCnpj" CssClass="form-control" placeHolder="Digite o número do CNPJ aqui" />
                    </div>
                    <div class="form-group">
                        <label for="TxtInscricaoEstadual">I.E.</label>
                        <asp:TextBox runat="server" ID="TxtInscricaoEstadual" CssClass="form-control" placeHolder="Digite o número da I.E. aqui" />
                    </div>
                    <div class="form-group">
                        <label for="CblTipoEscrituracao">Tipo de Escrituração:</label>
                        <asp:CheckBoxList runat="server" ID="CblTipoEscrituracao">
                            <asp:ListItem Value="2">EFD ICMS / IPI</asp:ListItem>
                            <asp:ListItem Value="6">EFD Contribuições</asp:ListItem>
                            <asp:ListItem Value="1" Enabled="false">ECD</asp:ListItem>
                            <asp:ListItem Value="0" Enabled="false">ECF</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="form-group">
                        <label for="CblSituacaoArquivo">Situação do Arquivo:</label>
                        <asp:CheckBoxList runat="server" ID="CblSituacaoArquivo">
                            <asp:ListItem Value="1">Validado com Sucesso <span class="glyphicon glyphicon-ok-sign button-green-text text-medium"></span></asp:ListItem>
                            <asp:ListItem Value="2">Validado com Advertência <span class="glyphicon glyphicon-exclamation-sign button-yellow-text text-medium"></span></asp:ListItem>
                            <asp:ListItem Value="3">Validado com Erros <span class="glyphicon glyphicon-remove-sign button-red-text text-medium"></span></asp:ListItem>
                            <asp:ListItem Value="4">Rejeitado <span class="glyphicon glyphicon-ban-circle button-black-text text-medium"></span></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="form-group">
                        <label for="DdRecepcionado">RECEPCIONADO</label>
                        <asp:DropDownList runat="server" ID="DdRecepcionado">
                            <asp:ListItem Value="Ultimas24Horas">Últimas 24 horas</asp:ListItem>
                            <asp:ListItem Value="Ultimos7Dias">Últimos 7 dias</asp:ListItem>
                            <asp:ListItem Value="Ultimos30Dias">Últimos 30 dias</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Button runat="server" ID="BtnFiltrarResultados" Text="FILTRAR RESULTADOS" CssClass="btn btn-block button-primary" OnClick="BtnFiltrarResultados_Click" />
                    </div>
                    <div class="form-group">
                        <button class="btn btn-block button-secondary" onclick="alternaBuscaAvancada(); return false;">BUSCA AVANÇADA</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-9">
            <div class="row">
                <div>
                    <div runat="server" id="DivMensagem" role="alert" visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <asp:Label runat="server" ID="LblMensagem" />
                     </div>
                </div>
            </div>
        </div>
        <div id="DivResultadoBusca" class="col-xs-9">
            <div runat="server" id="DivAcoes" visible="false" class="row">
                <div class="col-xs-12 line-space">
                    <label class="btn gray-background"><b>AÇÕES</b></label>
                    <asp:LinkButton runat="server" ID="BtnAssinar" CssClass="btn button-primary text-small" OnClick="BtnAssinar_Click">
                        <span class="glyphicon glyphicon-pencil"></span>
                        <span>ASSINAR</span>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="BtnDownload" CssClass="btn button-primary text-small" OnClientClick="setDownload()" OnClick="BtnDownload_Click">
                        <span class="glyphicon glyphicon-download-alt"></span>
                        <span>DOWNLOAD</span>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row overflow">
                <div class="col-xs-12">
                    <asp:Repeater runat="server" ID="RptResultadoBusca" OnItemDataBound="RptResultadoBusca_ItemDataBound" OnItemCommand="RptResultadoBusca_ItemCommand">
                        <HeaderTemplate>
                            <table id="TbResultadoBusca" class="table table-hover table-striped">
                                <tr class="text-center">
                                    <th class="text-center">
                                        <asp:CheckBox runat="server" ID="CboSelecionarTodos" />
                                    </th>
                                    <th class="text-center">PVA</th>
                                    <th class="text-center"><!-- Propriedades do documento --></th>
                                    <th class="text-center">Tipo de Escrituração</th>
                                    <th class="text-center">Finalidade</th>
                                    <th colspan="2" class="text-center">Competência</th>
                                    <th class="text-center">Efetiva Entrega</th>
                                    <th class="text-center">Entidade</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="text-center">
                                    <asp:HiddenField runat="server" ID="HfIdResultadoBusca" />
                                    <asp:CheckBox runat="server" ID="CboItemResultadoBusca" />
                                </td>
                                <td>
                                    <span runat="server" id="SpnSucesso" class="glyphicon glyphicon-ok-sign button-green-text text-medium"></span>
                                    <span runat="server" id="SpnAdvertencia" class="glyphicon glyphicon-exclamation-sign button-yellow-text text-medium"></span>
                                    <span runat="server" id="SpnErro" class="glyphicon glyphicon-remove-sign button-red-text text-medium"></span>
                                    <span runat="server" id="SpnRejeitado" class="glyphicon glyphicon-ban-circle button-black-text text-medium"></span>
                                </td>
                                <td><asp:LinkButton runat="server" ID="BtnPropriedadesArquivo" CommandName="ExibirPropriedades"><span class="glyphicon glyphicon-list"></span></asp:LinkButton></td>
                                <td class="text-left text-nowrap"><asp:Label runat="server" ID="LblTipoEscrituracao" /></td>
                                <td class="text-nowrap"><asp:Label runat="server" ID="LblFinalidade" /></td>
                                <td class="text-center text-nowrap"><b>De:</b> <asp:Label runat="server" ID="LblCompetenciaInicio" /></td>
                                <td class="text-center text-nowrap"><b>Até:</b> <asp:Label runat="server" ID="LblCompetenciaFim" /></td>
                                <td class="text-center text-nowrap"><asp:Label runat="server" ID="LblDataEntrega" /></td>
                                <td class="text-left text-nowrap"><asp:Label runat="server" ID="LblEntidade" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-xs-12 text-center">
                    <asp:Repeater runat="server" ID="RptPaginacao" OnItemDataBound="RptPaginacao_ItemDataBound" OnItemCommand="RptPaginacao_ItemCommand">
                        <HeaderTemplate>
                            <ul class="pagination">
                                <li>
                                    <asp:LinkButton runat="server" ID="BtnPrimeiraPagina" CommandName="SelecionaPrimeiraPagina">
                                        <span aria-hidden="true">&laquo;</span>
                                    </asp:LinkButton>
                                </li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton runat="server" ID="BtnPagina" CommandName="SelecionaPagina" />
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                                <li>
                                    <asp:LinkButton runat="server" ID="BtnUltimaPagina" CommandName="SelecionaUltimaPagina">
                                       <span aria-hidden="true">&raquo;</span>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div id="DivBuscaAvancada" class="col-xs-9" style="display: none;">
            <div class="row">
                <div class="col-xs-12">
                    <h1>Busca Avançada</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button runat="server" ID="BtnExecutarBuscaAvancadaTopo" Text="EXECUTAR BUSCA" CssClass="btn button-primary line-space" OnClick="BtnExecutarBuscaAvancadaTopo_Click" />
                </div>
            </div>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">ESCRITURAÇÃO</legend>
                <div class="row">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <label for="TxtNomeArquivo">Nome do arquivo</label>
                            <asp:TextBox runat="server" ID="TxtNomeArquivo" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="CblTipoEscrituracao">Tipo de Escrituração</label>
                            <asp:CheckBoxList runat="server" ID="CblTipoEscrituracaoAvancada">
                                <asp:ListItem Value="2">EFD I/I</asp:ListItem>
                                <asp:ListItem Value="6">EFD CONTR.</asp:ListItem>
                                <asp:ListItem Value="1" Enabled="false">ECD</asp:ListItem>
                                <asp:ListItem Value="0" Enabled="false">ECF</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="RblFinalidade">Finalidade</label>
                            <asp:RadioButtonList runat="server" ID="RblFinalidade">
                                <asp:ListItem Value="0">0 - Original</asp:ListItem>
                                <asp:ListItem Value="1">1 - Retificador</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label class="width-100">Competência</label>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtCompetenciaInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtCompetenciaFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group disabled-section">
                            <label>Período da Entrega</label>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtPeriodoEntregaInicio" Width="90%" CssClass="form-control" placeHolder="De" Enabled="false" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtPeriodoEntregaFim" Width="90%" CssClass="form-control" placeHolder="Até"  Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">CONTRIBUINTES</legend>
                <div class="row">
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtCnpjContribuinte">CNPJ</label>
                            <asp:TextBox runat="server" ID="TxtTxtCnpjContribuinte" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtInscricaoEstadualContribuinte">I.E.</label>
                            <asp:TextBox runat="server" ID="TxtInscricaoEstadualContribuinte" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtRazaoSocialContribuinte">Razão Social</label>
                            <asp:TextBox runat="server" ID="TxtRazaoSocialContribuinte" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="DdUnidadeFederativaContribuinte">U.F.</label>
                            <asp:DropDownList runat="server" ID="DdUnidadeFederativaContribuinte" CssClass="form-control" />
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">PROCESSAMENTO E ARMAZENAMENTO DA ESCRITURAÇÃO</legend>
                <div class="row">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label for="CblSituacaoAvancada">Situação do Arquivo</label>
                            <asp:CheckBoxList runat="server" ID="CblSituacaoAvancada">
                                <asp:ListItem Value="1">Validado com Sucesso <span class="glyphicon glyphicon-ok-sign button-green-text text-medium"></span></asp:ListItem>
                                <asp:ListItem Value="2">Validado com Advertência <span class="glyphicon glyphicon-exclamation-sign button-yellow-text text-medium"></span></asp:ListItem>
                                <asp:ListItem Value="3">Validado com Erros <span class="glyphicon glyphicon-remove-sign button-red-text text-medium"></span></asp:ListItem>
                                <asp:ListItem Value="4">Rejeitado <span class="glyphicon glyphicon-ban-circle button-black-text text-medium"></span></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label for="CblSituacaoSPED">Situação SPED</label>
                            <asp:CheckBoxList runat="server" ID="CblSituacaoSPED">
                                <asp:ListItem Value="2">PVA executado</asp:ListItem>
                                <asp:ListItem Value="3">Assinado digitalmente</asp:ListItem>
                                <asp:ListItem Value="4">Transmitido</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group disabled-section">
                            <label for="TvArmazenamento">Armazenamento</label>
                            <asp:TreeView runat="server" ID="TvArmazenamento" ShowExpandCollapse="false" ShowCheckBoxes="All" Enabled="false">
                                <Nodes>
                                    <asp:TreeNode Text="Arquivos transmitidos pela SpediA" SelectAction="None">
                                        <asp:TreeNode Text="Evidências do PVA" SelectAction="None"></asp:TreeNode>
                                        <asp:TreeNode Text="Arquivo transmitido" SelectAction="None"></asp:TreeNode>
                                        <asp:TreeNode Text="Termo de entrega" SelectAction="None"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Arquivos não transmitidos pela SpediA" SelectAction="None">
                                        <asp:TreeNode Text="Evidências do PVA" SelectAction="None"></asp:TreeNode>
                                        <asp:TreeNode Text="Arquivo transmitido" SelectAction="None"></asp:TreeNode>
                                        <asp:TreeNode Text="Termo de entrega" SelectAction="None"></asp:TreeNode>
                                    </asp:TreeNode>
                                </Nodes>
                            </asp:TreeView>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">LASTRO DO ARQUIVO NO PORTAL</legend>
                <div class="row">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Arquivo Recepcionado Em</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataRecepcionadoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataRecepcionadoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Arquivo Validado Em</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataValidadoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataValidadoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Arquivo Conciliado Em</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataConciliadoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataConciliadoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row vertical-adjustment">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Arquivo Assinado Em</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataAssinadoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataAssinadoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Arquivo Transmitido Em</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataTransmitidoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataTransmitidoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button runat="server" ID="BtnExecutarBuscaAvancadaBase" Text="EXECUTAR BUSCA" CssClass="btn button-primary line-space" OnClick="BtnExecutarBuscaAvancadaBase_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="propriedadesArquivo" tabindex="-1" role="dialog" aria-labelledby="titulopropriedadesArquivo" aria-hidden="true">
        <div class="modal-dialog" style="width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="titulopropriedadesArquivo">Propriedades da Escrituração</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeNomeArquivo">Nome do Arquivo:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeNomeArquivo" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label class="width-100">Competência:</label>
                                <div class="col-xs-6 no-padding">
                                    <asp:TextBox runat="server" ID="TxtPropriedadeCompetenciaInicial" Width="90%" CssClass="form-control background-reset" Enabled="false" />
                                </div>
                                <div class="col-xs-6 no-padding">
                                    <asp:TextBox runat="server" ID="TxtPropriedadeCompetenciaFinal" Width="90%" CssClass="form-control background-reset" Enabled="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeTipoEscrituracao">Tipo:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeTipoEscrituracao" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeFinalidade">Finalidade:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeFinalidade" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-space">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeParticipanteCnpj">CNPJ:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeParticipanteCnpj" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeParticipanteInscricaoEstadual">I.E.:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeParticipanteInscricaoEstadual" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeParticipanteRazaoSocial">Razão Social</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeParticipanteRazaoSocial" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-division">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeOrigem">Origem:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeOrigem" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDataRecepcao">Recepção no Portal:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataRecepcao" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-division">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeArquivoValidado">Processamento</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeArquivoValidado" CssClass="form-control background-reset" Enabled="false" Visible="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeArquivoConciliadoIndiciosErro" CssClass="form-control line-space background-reset" Enabled="false" Visible="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeArquivoAssinadoSucesso" Text="Arquivo Assinado com Sucesso pelo PVA" CssClass="form-control line-space background-reset" Enabled="false" Visible="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeArquivoTransmitidoSucesso" Text="Arquivo Transmitido com Sucesso pelo PVA" CssClass="form-control line-space background-reset" Enabled="false" Visible="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDataArquivoValidado">Data:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataArquivoValidado" CssClass="form-control background-reset" Enabled="false" Visible="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataArquivoConciliadoIndiciosErro" CssClass="form-control line-space background-reset" Enabled="false" Visible="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataArquivoAssinadoSucesso" CssClass="form-control line-space background-reset" Enabled="false" Visible="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataArquivoTransmitidoSucesso" CssClass="form-control line-space background-reset" Enabled="false" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-division">
                        <div class="col-xs-12 line-space">
                            <asp:Repeater runat="server" ID="RptArquivo" OnItemDataBound="RptArquivo_ItemDataBound">
                                <HeaderTemplate>
                                    <label class="secondary-color"><strong>Download:</strong></label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:LinkButton runat="server" ID="BtnPropriedadeDownloadArquivo" CssClass="button-secondary" OnClientClick="setDownload()" OnClick="BtnPropriedadeDownloadArquivo_Click" />
                                        <asp:TextBox runat="server" ID="TxtPropriedadeDataAcao" Width="100px" CssClass="form-control background-reset display-inline" Enabled="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row line-space">
                        <div class="col-xs-12">
                            <span><strong>Último Acesso</strong></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6">
                            <asp:TextBox runat="server" ID="TxtPropriedadeUsuarioUltimoAcesso" CssClass="form-control background-reset" Enabled="false" />
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox runat="server" ID="TxtPropriedadeDataUltimoAcesso" CssClass="form-control background-reset" Enabled="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="certificadoDigital" tabindex="-1" role="dialog" aria-labelledby="tituloCertificadoDigital" aria-hidden="true">
        <div class="modal-dialog" style="width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="tituloCertificadoDigital">Certificado Digital</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label for="FuCertificadoDigital">Selecione o seu Certificado Digital</label>
                                <asp:FileUpload runat="server" ID="FuCertificadoDigital" data-show-upload="false" data-show-caption="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label for="TxtSenhaCertificadoDigital">Senha do Certificado Digital</label>
                                <asp:TextBox runat="server" ID="TxtSenhaCertificadoDigital" TextMode="Password" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="BtnSolicitarAssinatura" CssClass="btn button-primary" Text="SOLICITAR ASSINATURA" OnClick="BtnSolicitarAssinatura_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
