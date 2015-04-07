<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="SpediaWeb.Pages.Logon" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Logon - SpediA</title>
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
    <asp:PlaceHolder runat="server">
        <%: System.Web.Optimization.Styles.Render("~/styles/bootstrap") %>
        <%: System.Web.Optimization.Styles.Render("~/styles/spedia") %>
        <%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>
        <%: System.Web.Optimization.Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
</head>
<body>
    <form runat="server">
    <div class="login">
        <div class="row">
            <div class="col-md-4 col-md-offset-4 text-center">
                <img src="/spedia/Content/Images/logo.png" class="logo" alt="SpediA logo" width="300">
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="panel panel-default">
                    <div class="panel-body login-panel">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">
								    <span class="glyphicon glyphicon-user"></span>
                                </span> 
                                <asp:TextBox runat="server" ID="TxtLogin" CssClass="form-control input-lg" placeHolder="Usuário" />
					        </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
							    <span class="input-group-addon">
								    <span class="glyphicon glyphicon-lock"></span>
								</span>
                                <asp:TextBox runat="server" ID="TxtSenha" TextMode="Password" CssClass="form-control input-lg" placeHolder="Senha" />
							</div>
						</div>
                        <div class="form-group">
                            <asp:Button runat="server" ID="BtnEntrar" CssClass="btn btn-lg btn-block button-primary" Text="ENTRAR" OnClick="BtnEntrar_Click" />
					    </div>
                        <div class="form-group">
                            <div runat="server" id="DivMensagem" role="alert" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <asp:Label runat="server" ID="LblMensagem" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <a href="#" data-toggle="modal" data-target="#esqueciSenhaModal">Esqueci minha senha</a>
            </div>
        </div>
    </div>
    <div class="modal fade" id="esqueciSenhaModal" tabindex="-1" role="dialog" aria-labelledby="tituloEsqueciSenhaModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="tituloEsqueciSenhaModal">Esqueci minha senha</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <p>Confirme o e-mail para onde sua senha será enviada</p>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon">
							    <span class="glyphicon glyphicon-envelope"></span>
                            </span> 
                            <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control input-lg" placeHolder="E-mail" />
					    </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="BtnEnviarSenha" CssClass="btn btn-lg button" Text="ENVIAR SENHA" OnClick="BtnEnviarSenha_Click" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
