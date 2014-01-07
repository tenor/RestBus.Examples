<%@ Page Title="RestBus RabbitMQ Client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClientExample._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <br />
                <h2>Welcome to the RestBus web client example</h2>
            </hgroup>
            <p>
                Enter a value in the text field below and hit send.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:Label ID="LabelName" runat="server" Text="Name: "></asp:Label>
    <asp:TextBox ID="TextBoxName" runat="server">random</asp:TextBox>&nbsp;<asp:Button ID="ButtonSend" runat="server" Text="Send" OnClick="ButtonSend_Click" />

    <asp:Panel ID="PanelResponse" runat="server" Height="108px" Visible="false">
        <h3>Response:</h3>
        <br />
        <asp:Label ID="LabelResponse" runat="server" Text="Label"></asp:Label>
    </asp:Panel>

</asp:Content>
