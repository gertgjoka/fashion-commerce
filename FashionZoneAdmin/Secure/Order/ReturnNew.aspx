<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReturnNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Order.ReturnNew" %>

<%@ Register TagPrefix="custom" Namespace="FashionZone.Admin.CustomControl" Assembly="FashionZone.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ViewStateMode="Enabled">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="box">
        <h3>
            Returns</h3>
        <div id="form">
            <label for="Customer">
                Customer :
            </label>
                    <asp:TextBox ID="txtCustomer" runat="server" ReadOnly="True" ViewStateMode="Enabled" />
                    <asp:Button ID="btnSelectCustomer" 
                                runat="server" 
                                Height="25px" 
                                Text="Select" 
                                Width="63px"
                                CausesValidation="False" 
                                OnClick="btnSelectCustomer_Click" 
                                TabIndex="60" 
                                ViewStateMode="Enabled" />
                    <br />
            <asp:Panel Visible="False" ID="pnlCustomer" CssClass="modalPopup" runat="server" style="width: 500px; margin-left: 110px;">
                <div>
                    <asp:Label ID="Label1" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtSearchName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                        CausesValidation="False" />
                </div>
                <custom:FZGrid ID="gridCustomer" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridCustomer_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridCustomer_PageIndexChanging" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader"
                    CustomVirtualItemCount="-1">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkSelect_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
                <br />
                <div align="center">
                    <asp:Button ClientIDMode="Static" ID="btnClose" Text="Close" ToolTip="Close dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnClose_Click" /><br />
                </div>
        </asp:Panel>
        <div runat="server" ID="divDatail" Visible="False">
            <fieldset id="FildOrderDat" runat="server" Visible="False">
                <legend>Order data</legend>
                    
                    <asp:Repeater ID="RepeatOrder" runat="server" ViewStateMode="Enabled">
                        <HeaderTemplate>
                            <table cellspacing="0" border="1" style="border-collapse:collapse;" rules="all">
                                <tr class="GridHeader">
                                    <th scope="col">Choose</th>
                                    <th scope="col">Numero Ordine</th>
                                    <th scope="col">Data </th>
                                    <th scope="col">Total Amount</th>
                                    <th scope="col">Campign Name</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a id="MainContent_gridCustomer_lnkSelect_0" href="javascript:__doPostBack('lnkSelectOrder','<%# DataBinder.Eval(Container.DataItem, "ID") %>')">Select</a>
                                </td>
                                <td><%# DataBinder.Eval(Container.DataItem, "ID") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "DateCreated")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "TotalAmount")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "CampaignName")%></td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
            </fieldset>
                    <br />
                <fieldset id="FieldsetOrderDett" runat="server" Visible="False">
                    <legend>Order details</legend>
                        <asp:PlaceHolder runat="server" ID="plhOrdertet">
                            <label for="Number">
                                Number :</label>
                            <asp:Literal runat="server" ID="litNumber" ></asp:Literal>
                            <br />
                            <label for="Status">
                                Status :</label>
                            <asp:DropDownList runat="server" ID="ddlStatus" TabIndex="20" DataValueField="ID"
                                DataTextField="Name" ViewStateMode="Enabled" Enabled="False">
                            </asp:DropDownList>
                            <br /><br />
                            <label for="Verified">
                                Verified :</label>
                            <asp:CheckBox runat="server" ID="chkVerified" TabIndex="30" CssClass="customCheck" Enabled="False"/>
                            <br /><br />
                            <label for="Completed">
                                Completed :</label>
                            <asp:CheckBox runat="server" ID="chkCompleted" TabIndex="40" CssClass="customCheck" Enabled="False"/>
                            <br /><br />
                            <label for="Canceled">
                                Canceled :</label>
                            <asp:CheckBox runat="server" ID="chkCanceled" TabIndex="50" CssClass="customCheck" Enabled="False"/>
                            <br /><br />
                            <label for="Amount">
                                Amount :</label>
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="7" Style="text-align: right;" TabIndex="40" Enabled="False"></asp:TextBox> 
                        </asp:PlaceHolder>
                </fieldset>
                <%--Return details--%>
                        <fieldset id="FieldRetDet" runat="server" Visible="False">
                            <legend>Return details</legend>
                            <label for="ReturnID">
                                Return ID :</label>
                            <asp:Literal runat="server" ID="litReturnID" ></asp:Literal>
                            <br /><br />
                            <label for="ReturnID">
                                Order ID :</label>
                            <asp:Literal runat="server" ID="litOrderID" ></asp:Literal>
                            <br /><br />
                            <label for="VerificationNumber">
                                Verification Num :</label>
                            <asp:Literal runat="server" ID="litVerificationNumber" ></asp:Literal>
                            <br />
                        </fieldset>
                        <%--Return details Fine--%>
                        <asp:Repeater ID="repOrderDett" 
                                  runat="server" 
                                  onitemdatabound="repOrderDett_ItemDataBound">
                            <HeaderTemplate>
                                <table cellspacing="0" border="1" style="border-collapse:collapse;" rules="all">
                                    <tr class="GridHeader">
                                        <th scope="col">Choose</th>
                                        <th scope="col">Unit Price</th>
                                        <th scope="col">Size</th>
                                        <th scope="col">Product Name</th>
                                        <th scope="col">Quantity</th>
                                        <th scope="col">Motivation</th>
                                        <th scope="col">Campaign Name</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkbSlect" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID ="lblUnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label>
                                    </td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ProductAttribute")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ProductName")%></td>
                                    <td>
                                        <asp:DropDownList ID="ddlQuantity" runat="server">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'>
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMotivation" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "CampaignName")%></td>                                
                                </tr>
                                <asp:HiddenField ID="hdnOrderDettID" Value='<%# DataBinder.Eval(Container.DataItem, "ID")%>' runat="server"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    <br />
                    <asp:PlaceHolder ID="plhdateRequest" runat="server" Visible="True">
                        <asp:Repeater ID="repRetDet" runat="server" onitemdatabound="repRetDet_ItemDataBound">
                            <HeaderTemplate>
                            <table cellspacing="0" border="1" style="border-collapse:collapse;width:80%" rules="all">
                                <tr class="GridHeader">
                                    <th scope="col">Return ID</th>
                                    <th scope="col">Order Detail ID</th>
                                    <th scope="col">Quantity</th>
                                    <th scope="col">Price</th>
                                    <th scope="col">Motivation</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# DataBinder.Eval(Container.DataItem, "ReturnID")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "OrderDetailID")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "Quantity")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "Price")%></td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlMotivRet" runat="server" Enabled="False">
                                    </asp:DropDownList>--%>
                                    <asp:Label runat="server" 
                                               ID="lblMotRet" 
                                               Text='<%# DataBinder.Eval(Container.DataItem, "MotivationID")%>'>
                                    </asp:Label>
                                </td>                              
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                        </asp:Repeater>

                    </asp:PlaceHolder>
                    <label for="ReceivedDate">
                            Request date : </label>
                    <asp:TextBox ID="txtRequestDate" TabIndex="31" runat="server" Enabled="False"/>
                    <br />
                    <label for="ReceivedDate">
                        Received date : </label>
                    <asp:TextBox ID="txtReceivedDate" TabIndex="35" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
                                                  runat="server" 
                                                  TargetControlID="txtReceivedDate"
                                                  FirstDayOfWeek="Monday" 
                                                  Format="dd/MM/yyyy" >
                    </ajaxToolkit:CalendarExtender>
                    <br />
                    <label for="Coments">
                        Comments : </label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" MaxLength="7" TabIndex="100" ></asp:TextBox> 
                    <br />
                    <br />
                    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                    <div align="center">
                    <asp:Button id="btn_save" Text="Save" runat="server" TabIndex="80" 
                            onclick="btn_save_Click" /> 
                    <%--<asp:Button id="btn_reset" Text="Reset" runat="server" CausesValidation="False" 
                            onclick="btnReset_Click" TabIndex="90"/>--%>
                    </div>
        </div>
        </div>
    </div>    
    </ContentTemplate>
</asp:UpdatePanel>
    
    
</asp:Content>
