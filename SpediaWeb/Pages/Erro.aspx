<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="SpediaWeb.Pages.Erro" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Erro</title>
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
    <div class="primary-color ">
        <div class="row">
            <div class="col-md-12">
                <img src="/spedia/Content/Images/bg_dfe_1.jpg" alt="Fundo" style="width:100%">
            </div>
        </div>
        <div class="row line-space">
            <div class="col-md-10 col-md-offset-2 error">
                <p>Puxa, que vergonha!</p>
                <p>Desculpe-nos, mas tivemos um problema. Tente refazer a operação e, se o problema persistir, entre em contato com o suporte.</p>
                <p>Obrigado!</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <a href="/spedia/Pages/Index.aspx">
                    <img src="/spedia/Content/Images/spedia-duvida.jpg" alt="SpediA" width="400">
                </a>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
