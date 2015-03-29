<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="FancyStoreDemo.PublicWebSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Products</h1>
    <div>
        <ul>
            <asp:Repeater runat="server" ID="ProductsRepeater" ItemType="FancyStoreDemo.Models.Product">
                <ItemTemplate>
                    <li><%#: Item.Name %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <asp:Label runat="server" ID="EmptyProductsLabel" Text="There are currently no products in the system." />
    </div>
    <asp:HyperLink runat="server" NavigateUrl="~/AddProduct" Text="Add Product" CssClass="btn btn-primary" />
</asp:Content>
