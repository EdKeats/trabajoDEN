<%@ Page Title="" Language="C#" MasterPageFile="~/InterfazAdministrador.Master" AutoEventWireup="true" CodeBehind="ConsultarProductoAdministrador.aspx.cs" Inherits="Web_Botilleria.ConsultarProductoAdministrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>
        Consulte el caracteristicas de un producto de un producto
    </h2>
    <br />
    <!--tabla que muestra los filtros disponibles para realizar una busqueda-->
    <table>
        <tr><td>ID</td><td><asp:TextBox ID="txtId" runat="server"></asp:TextBox></td>
        <td>Nombre</td><td><asp:TextBox ID="txtNombre" runat="server"></asp:TextBox></td>
        <td>Marca</td><td><asp:TextBox ID="txtMarca" runat="server"></asp:TextBox></td>
        <td>Monto máximo:</td><td><asp:TextBox ID="txtMonto" runat="server"></asp:TextBox></td>
        
        <td style="text-align:right">
            <asp:Button ID="Button1" runat="server" Text="Buscar" />
            </td>
        </tr>
    </table>

    <br>
    <!--tabla que muestra los productos encontrados según la busqueda realizada-->
    <table border='1' width="100%">
    <tr style="font-weight:bold;"><td>ID</td><td>Nombre</td><td>Marca</td><td>CC</td><td>Precio</td><td>Cantidad</td><td>Total</td><td>Agregar al carrito</td><td>Stock</td></tr>
    <tr><td>00000233</td><td>Coca-Cola Zero</td><td>The Coca-Cola Company</td><td>100CC</td><td>$2.500</td>
    <td><asp:TextBox ID="txtCantidad" runat="server" Width="20px">7</asp:TextBox></td><td>$17.500</td><td><img src="images/add_carro.png" style="width:25px; height:25px;" /></td>
    <td><img src="images/icono_Curso-Gestion-Stock-Control-Almacen.png" style="width:25px; height:25px;" /></td></tr>
    </table>

</asp:Content>
