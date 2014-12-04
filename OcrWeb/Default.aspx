<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OcrWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
      <form id="Form1" method="post" role="form" runat="server">
            <asp:Panel ID="inputPanel" runat="server">
                <fieldset>
                    <legend>File Upload</legend>
                    <div class="form-group">
                        <label for="imageFile" runat="server">File:</label>
                        <input class="form-control" type="file" id="imageFile" runat="server" />
                        <span class="help-block">The file to be processed.</span>
                    </div>
                    <button id="submitFile" type="submit" class="btn btn-default" runat="server">Submit</button>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="resultPanel" Visible="False" runat="server">
                <fieldset>
                    <legend>OCR Results</legend>
                    <div class="form-group">
                        <label for="result" runat="server">Mean Confidence:</label>
                        <label class="form-control" id="meanConfidenceLabel" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="result" runat="server">Result:</label>
                        <textarea class="form-control" rows="10" id="resultText" readonly="readonly" runat="server"></textarea>
                    </div>
                    <button id="restartButton" type="submit" class="btn btn-default" runat="server">Restart</button>
                </fieldset>
            </asp:Panel>
        </form>
</body>
</html>
