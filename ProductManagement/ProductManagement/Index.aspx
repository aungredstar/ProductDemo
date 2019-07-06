<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Index.aspx.cs" EnableEventValidation="false" Inherits="ProductManagement.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!--===============================================================================================-->
        <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css" />
        <!--===============================================================================================-->
        <link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
        <!--===============================================================================================-->
        <link rel="stylesheet" type="text/css" href="vendor/animate/animate.css" />
        <!--===============================================================================================-->
        <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css" />
        <!--===============================================================================================-->
        <link rel="stylesheet" type="text/css" href="vendor/perfect-scrollbar/perfect-scrollbar.css" />
        <!--===============================================================================================-->
        <link rel="stylesheet" type="text/css" href="css/util.css" />
        <link rel="stylesheet" type="text/css" href="css/main.css" />
        <!--===============================================================================================-->


        <div class="limiter">
            <div class="table100 ver1" style="padding-top: 0px !important; margin-top:20px; margin-bottom: 20px;">
                <table border="0" cell-spacing="2">
                    <tr>
                        <th colspan="2" style="text-align:center;"> Project Management</th>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label">Name</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hidId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label">Quantity</label></td>
                        <td>
                            <asp:TextBox ID="txtQuanity" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label">Sale Amount</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSaleAmount" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-wrap: none;">
                            <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-light" OnClick="btnSave_Click" Visible="false" />
                            <asp:Button ID="btnAddNew" Text="Add New" runat="server" CssClass="btn btn-light" OnClick="btnAddNew_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>
            </div>

            <div class="table100 ver1" style="padding-top: 0px !important; margin-bottom: 20px;">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" BorderWidth="0" OnRowCommand="gv_RowCommand" RowStyle-CssClass="row100 body">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" CommandName="CHANGE" Style="display: normal !important; margin-left: 4px;" CommandArgument='<%# Eval("Id") %>' OnClientClick="" ImageUrl="Images/edit.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDel" runat="server" CommandName="DEL" Style="display: normal !important;" CommandArgument='<%# Eval("Id") %>' Width="16" Height="16" OnClientClick="" ImageUrl="Images/f_delete.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sale Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblSaleAmount" runat="server" Text='<%# Eval("SaleAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </form>
</body>
</html>
