<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="FancyStoreDemo.PublicWebSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1>Products</h1>
    </div>
    <div>
        <asp:Repeater runat="server" ID="ProductsRepeater" ItemType="FancyStoreDemo.Models.Product">
            <ItemTemplate>
                <div class="well">
                    <h2><%#: Item.Name %><br /><small>$<%#: Item.Price.ToString("0.00") %></small></h2>
                    <p><%#: Item.Description %></p>
                    <asp:HyperLink runat="server" NavigateUrl='<%#: "ViewProduct/" + Item.Id %>' Text="More Info" CssClass="btn btn-primary" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label runat="server" ID="EmptyProductsLabel" Text="There are currently no products in the system." />
    </div>
    <asp:HyperLink runat="server" NavigateUrl="~/AddProduct" Text="Add Product" CssClass="btn btn-success" />
</asp:Content>
