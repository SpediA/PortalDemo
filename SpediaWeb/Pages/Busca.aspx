<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="SpediaWeb.Pages.Busca" %>

<asp:Content ID="BuscaCabecalho" ContentPlaceHolderID="Cabecalho" runat="server">
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

        function alternaBuscaAvancada() {
            $('#DivResultadoBusca').css('display', 'none');
            $('#DivBuscaAvancada').css('display', 'block');
        }

        function openModal() {
            $('#propriedadesArquivo').modal('show');
        }

        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Busca" ContentPlaceHolderID="Conteudo" runat="server">
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
                        <label for="RblTipoEmissao">OPERAÇÃO:</label>
                        <asp:RadioButtonList runat="server" ID="RblTipoEmissao">
                            <asp:ListItem Value="1">Própria</asp:ListItem>
                            <asp:ListItem Value="2">Terceiros</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label for="RblSituacaoDocumento">SITUAÇÃO:</label>
                        <asp:RadioButtonList runat="server" ID="RblSituacaoDocumento">
                            <asp:ListItem Value="3">VÁLIDOS <span class="glyphicon glyphicon-ok-sign button-green-text text-medium"></span></asp:ListItem>
                            <asp:ListItem Value="4">INVÁLIDOS <span class="glyphicon glyphicon-remove-sign button-red-text text-medium"></span></asp:ListItem>
                        </asp:RadioButtonList>
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
                    <asp:LinkButton runat="server" ID="BtnExecutarManifestacao" CssClass="btn button-primary text-small" Visible="false">
                        <span class="glyphicon glyphicon-share-alt"></span>
                        <span>EXECUTAR MANIFESTAÇÃO</span>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="BtnConsultarSefaz" CssClass="btn button-primary text-small" OnClick="BtnConsultarSefaz_Click">
                        <span class="glyphicon glyphicon-transfer"></span>
                        <span>CONSULTAR SEFAZ</span>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="BtnDownload" CssClass="btn button-primary text-small" OnClientClick="setDownload()" OnClick="BtnDownload_Click">
                        <span class="glyphicon glyphicon-download-alt"></span>
                        <span>DOWNLOAD</span>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="BtnSolicitarExclusao" CssClass="btn button-red text-small" Visible="false">
                        <span class="glyphicon glyphicon-remove"></span>
                        <span>SOLICITAR EXCLUSÃO</span>
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
                                        <asp:CheckBox runat="server" ID="CboSelecionarTodos" CssClass="checkbox-all" />
                                    </th>
                                    <th class="text-center"><!-- Situação do documento --></th>
                                    <th class="text-center"><!-- Propriedades do documento --></th>
                                    <th class="text-center">EMISSÃO</th>
                                    <th class="text-center">RECEPÇÃO</th>
                                    <th class="text-center">NÚMERO</th>
                                    <th class="text-center">SÉRIE</th>
                                    <th class="text-center">VALOR</th>
                                    <th class="text-center">EMISSOR</th>
                                    <th class="text-center">DESTINATÁRIO</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="text-center">
                                    <asp:HiddenField runat="server" ID="HfIdResultadoBusca" />
                                    <asp:CheckBox runat="server" ID="CboItemResultadoBusca" />
                                </td>
                                <td>
                                    <span runat="server" id="SpnValido" class="glyphicon glyphicon-ok-sign button-green-text text-medium"></span>
                                    <span runat="server" id="SpnInvalido" class="glyphicon glyphicon-remove-sign button-red-text text-medium"></span>
                                </td>
                                <td><asp:LinkButton runat="server" ID="BtnPropriedadesArquivo" CommandName="ExibirPropriedades"><span class="glyphicon glyphicon-list"></span></asp:LinkButton></td>
                                <td class="text-center"><asp:Label runat="server" ID="LblDataEmissao" /></td>
                                <td class="text-center"><asp:Label runat="server" ID="LblDataRecepcao" /></td>
                                <td class="text-right text-nowrap"><asp:Label runat="server" ID="LblNumero" /></td>
                                <td class="text-right text-nowrap"><asp:Label runat="server" ID="LblSerie" /></td>
                                <td class="text-right text-nowrap"><asp:Label runat="server" ID="LblValorTotal" /></td>
                                <td class="text-left text-nowrap"><asp:Label runat="server" ID="LblEmissor" /></td>
                                <td class="text-left text-nowrap"><asp:Label runat="server" ID="LblDestinatario" /></td>
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
                <legend class="scheduler-border">OPERAÇÃO FISCAL</legend>
                <div class="row">
                    <div class="col-xs-8">
                        <div class="form-group">
                            <label for="TxtChaveAcesso">Chave de Acesso</label>
                            <asp:TextBox runat="server" ID="TxtChaveAcesso" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label for="TxtNaturezaOperacao">Natureza da Operação</label>
                            <asp:TextBox runat="server" ID="TxtNaturezaOperacao" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label>Número do Documento</label>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtNumeroDocumentoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtNumeroDocumentoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label>Série do Documento</label>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtSerieDocumentoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtSerieDocumentoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label>Valor do Documento</label>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtValorDocumentoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtValorDocumentoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row vertical-adjustment-small">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Data da Emissão</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataEmissaoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataEmissaoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <label>Data Entrada/Saída</label>
                                </div>
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataEntradaSaidaInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6 no-padding">
                                <asp:TextBox runat="server" ID="TxtDataEntradaSaidaFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row vertical-adjustment-small">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label for="RblFinalidadeOperacao">Finalidade da Operação</label>
                            <asp:RadioButtonList runat="server" ID="RblFinalidadeOperacao">
                                <asp:ListItem Value="1">Documento Fiscal Normal</asp:ListItem>
                                <asp:ListItem Value="2">Documento Fiscal Complementar</asp:ListItem>
                                <asp:ListItem Value="3">Documento Fiscal de Ajuste</asp:ListItem>
                                <asp:ListItem Value="4">Devolução de Mercadoria</asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label for="RblTipoEmissaoAvancada">Tipo de Emissão</label>
                            <asp:RadioButtonList runat="server" ID="RblTipoEmissaoAvancada">
                                <asp:ListItem Value="1">Própria</asp:ListItem>
                                <asp:ListItem Value="2">Terceiros</asp:ListItem>
                                <asp:ListItem Value="">Ambas</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label for="RblTipoOperacao">Tipo de Operação</label>
                            <asp:RadioButtonList runat="server" ID="RblTipoOperacaoAvancada">
                                <asp:ListItem Value="0">Entrada</asp:ListItem>
                                <asp:ListItem Value="1">Saída</asp:ListItem>
                                <asp:ListItem Value="">Ambas</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">PARTICIPANTES DA OPERAÇÃO FISCAL</legend>
                <div class="row">
                    <div class="col-xs-12">
                        <label>EMISSOR:</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtCnpjEmissor">CNPJ</label>
                            <asp:TextBox runat="server" ID="TxtCnpjEmissor" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtInscricaoEstadualEmissor">I.E.</label>
                            <asp:TextBox runat="server" ID="TxtInscricaoEstadualEmissor" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtRazaoSocialEmissor">Razão Social</label>
                            <asp:TextBox runat="server" ID="TxtRazaoSocialEmissor" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="DdUnidadeFederativaEmissor">U.F.</label>
                            <asp:DropDownList runat="server" ID="DdUnidadeFederativaEmissor" CssClass="form-control" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <label>DESTINATÁRIO:</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtCnpjDestinatario">CNPJ</label>
                            <asp:TextBox runat="server" ID="TxtCnpjDestinatario" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtInscricaoEstadualDestinatario">I.E.</label>
                            <asp:TextBox runat="server" ID="TxtInscricaoEstadualDestinatario" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="TxtRazaoSocialDestinatario">Razão Social</label>
                            <asp:TextBox runat="server" ID="TxtRazaoSocialDestinatario" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label for="DdUnidadeFederativaDestinatario">U.F.</label>
                            <asp:DropDownList runat="server" ID="DdUnidadeFederativaDestinatario" CssClass="form-control" />
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">ITEM DA OPERAÇÃO FISCAL</legend>
                <div class="row">
                    <div class="col-xs-2 vbottom">
                        <div class="form-group">
                            <label for="TxtCodigoProdutoServico">Código do Produto/Serviço</label>
                            <asp:TextBox runat="server" ID="TxtCodigoProdutoServico" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div><!--
                    --><div class="col-xs-2 vbottom">
                        <div class="form-group">
                            <label for="TxtDescricaoProdutoServico">Descrição do Produto/Serviço</label>
                            <asp:TextBox runat="server" ID="TxtDescricaoProdutoServico" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div><!--
                    --><div class="col-xs-2 vbottom">
                        <div class="form-group">
                            <label for="TxtCodigoNcmNbs">Código NCM/NBS</label>
                            <asp:TextBox runat="server" ID="TxtCodigoNcmNbs" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div><!--
                    --><div class="col-xs-2 vbottom">
                        <div class="form-group">
                            <label for="TxtCodigoEan">Código EAN</label>
                            <asp:TextBox runat="server" ID="TxtCodigoEan" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div><!--
                    --><div class="col-xs-2 vbottom">
                        <div class="form-group">
                            <label for="TxtControleFci">Controle FCI</label>
                            <asp:TextBox runat="server" ID="TxtControleFci" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div><!--
                    --><div class="col-xs-2 vbottom">
                        <div class="form-group">
                            <label for="TxtCfop">CFOP</label>
                            <asp:TextBox runat="server" ID="TxtCfop" CssClass="form-control" placeholder="Digite aqui" />
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">VALIDAÇÃO JURÍDICA DO DOCUMENTO FISCAL</legend>
                <div class="row">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <label for="TvSituacaoDocumentoSefaz">Situação do Documento na SEFAZ</label>
                            <asp:TreeView runat="server" ID="TvSituacaoDocumentoSefaz" ShowExpandCollapse="false" ShowCheckBoxes="All" >
                                <Nodes>
                                    <asp:TreeNode Text="Autorizado pela SEFAZ" SelectAction="None" Value="100,101">
                                        <asp:TreeNode Text="Carta de correção eletrônica" SelectAction="None" Value=""></asp:TreeNode>
                                        <asp:TreeNode Text="Cancelada" SelectAction="None" Value="101"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Denegado pela Sefaz" SelectAction="None" Value="110"></asp:TreeNode>
                                    <asp:TreeNode Text="Inutilizado pelo emissor" SelectAction="None"  Value="102"></asp:TreeNode>
                                    <asp:TreeNode Text="Não consta na base de dados da Sefaz" SelectAction="None"  Value="217"></asp:TreeNode>
                                </Nodes>
                            </asp:TreeView>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="form-group">
                            <label for="TvIntegridadeXml">Integridade do Arquivo XML</label>
                            <asp:TreeView runat="server" ID="TvIntegridadeXml" ShowExpandCollapse="false" ShowCheckBoxes="All">
                                <Nodes>
                                    <asp:TreeNode Text="Arquivo inválido" SelectAction="None">
                                        <asp:TreeNode Text="Sem protocolo de autorização" SelectAction="None"></asp:TreeNode>
                                        <asp:TreeNode Text="Com assinatura digital inválida" SelectAction="None"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Arquivo válido" SelectAction="None"></asp:TreeNode>
                                </Nodes>
                            </asp:TreeView>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="scheduler-border padding-sides">
                <legend class="scheduler-border">LASTRO DO DOCUMENTO NO PORTAL</legend>
                <div class="row">
                    <div class="col-xs-6">
                        <div class="row">
                            <div class="col-xs-12">
                                <label>Documento Recepcionado em</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6">
                                <asp:TextBox runat="server" ID="TxtDocumentoRecepcionadoInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox runat="server" ID="TxtDocumentoRecepcionadoFim" Width="90%" CssClass="form-control" placeHolder="Até" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="row">
                            <div class="col-xs-12">
                                <label>Data da Última Consulta na SEFAZ</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6">
                                <asp:TextBox runat="server" ID="TxtDataUltimaConsultaInicio" Width="90%" CssClass="form-control" placeHolder="De" />
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox runat="server" ID="TxtDataUltimaConsultaFim" Width="90%" CssClass="form-control" placeHolder="Até" />
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
                    <h4 class="modal-title" id="titulopropriedadesArquivo">Propriedades do Documento</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeChaveAcesso">Chave de acesso:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeChaveAcesso" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeTipoDocumento">Tipo:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeTipoDocumento" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeNumeroDocumento">Número:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeNumeroDocumento" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDataEmissao">Emissão</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataEmissao" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 line-space">
                            <span><strong>EMISSOR</strong></span>
                        </div>
                    </div>
                    <div class="row line-space">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeEmissorCnpj">CNPJ:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeEmissorCnpj" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeEmissorInscricaoEstadual">I.E.:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeEmissorInscricaoEstadual" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeEmissorRazaoSocial">Razão Social</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeEmissorRazaoSocial" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 line-space">
                            <span><strong>DESTINATÁRIO</strong></span>
                        </div>
                    </div>
                    <div class="row line-space line-division">
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDestinatarioCnpj">CNPJ:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDestinatarioCnpj" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDestinatarioInscricaoEstadual">I.E.:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDestinatarioInscricaoEstadual" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeDestinatarioRazaoSocial">Razão Social</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDestinatarioRazaoSocial" CssClass="form-control background-reset" Enabled="false" />
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
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeSituacaoSefaz">Situação do documento na Sefaz:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeSituacaoSefaz" CssClass="form-control background-reset" Enabled="false"/>
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDataSituacaoSefaz">Data:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataSituacaoSefaz" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-division">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeIntegridadeArquivo">Integridade do arquivo xml:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeIntegridadeArquivoProtocoloAutorizacao" CssClass="form-control background-reset" Enabled="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeIntegridadeArquivoAssinaturaDigital" CssClass="form-control line-space background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDataIntegridadeArquivo">Data:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataIntegridadeArquivoProtocoloAutorizacao" CssClass="form-control background-reset" Enabled="false" />
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataIntegridadeArquivoAssinaturaDigital" CssClass="form-control line-space background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-division">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="TxtPropriedadeManifestacaoDestinatario">Manifestação do destinatário:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeManifestacaoDestinatario" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="form-group">
                                <label for="TxtPropriedadeDataManifestacaoDestinatario">Data:</label>
                                <asp:TextBox runat="server" ID="TxtPropriedadeDataManifestacaoDestinatario" CssClass="form-control background-reset" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row line-space">
                        <div class="col-xs-12">
                            <label class="secondary-color"><strong>Download:</strong></label>
                            <asp:LinkButton runat="server" ID="BtnPropriedadeDownload" Text=".xml" CssClass="button-secondary" OnClientClick="setDownload()" OnClick="BtnPropriedadeDownload_Click" />
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
</asp:Content>
