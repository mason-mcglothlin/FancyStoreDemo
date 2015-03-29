<%@ Page Title="Add Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="FancyStoreDemo.PublicWebSite.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1>Add Product</h1>
    </div>
    <div class="form-group">
        <label for="IdTB">Id</label>
        <asp:TextBox runat="server" ID="IdTB" CssClass="form-control" placeholder="Id"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="NameTB">Name</label>
        <asp:TextBox runat="server" ID="NameTB" CssClass="form-control" placeholder="Name"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="DescriptionTB">Description</label>
        <asp:TextBox runat="server" ID="DescriptionTB" CssClass="form-control" placeholder="Description"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="PriceTB">Price</label>
        <asp:TextBox runat="server" ID="PriceTB" CssClass="form-control" placeholder="Price"></asp:TextBox>
    </div>
    <asp:Button runat="server" ID="AddProductBtn" OnClick="AddProductBtn_Click" Text="Submit" CssClass="btn btn-success" />
</asp:Content>
