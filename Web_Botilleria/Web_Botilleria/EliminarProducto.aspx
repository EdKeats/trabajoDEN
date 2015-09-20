<%@ Page Title="" Language="C#" MasterPageFile="~/InterfazAdministrador.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="Web_Botilleria.EliminarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Ingrese ID para eliminar</p>
    <table>
        <tr><td>ID</td><td><asp:TextBox ID="txtIdAdm" runat="server"></asp:TextBox></td>
        <td> <asp:ImageButton ID="ImageButton1" runat="server" Height="30px" 
                ImageAlign="Bottom" ImageUrl="~/images/trash.jpg" Width="45px" /> </td>

        </tr>
    </table>

</asp:Content>
