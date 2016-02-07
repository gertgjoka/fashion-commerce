<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="Common/Header.xslt"/>
  <xsl:include href="Common/Footer.xslt"/>
  <xsl:output method="html"/>
  <xsl:template match="/">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        <title>FZone Order</title>
      </head>
      <body style="margin:0; padding:0; background-color: #e7dcdd;" bgcolor="#e7dcdd">
        <div style="width: 100%; color: #CC3333; font: Myraid Pro; height:auto;
        margin: 0 auto; background-color: #e7dcdd;">
          <table width="504" cellpadding="0" cellspacing="0" align="center">
            <xsl:call-template name="Header"/>
            <tr>
              <td colspan="3" style="text-align: center; padding-top: 10px;">
                <span style="font-size: 17pt; color: #CC3333; font: Myraid Pro;">Porosia juaj</span>
                <br />
                <br />
              </td>
            </tr>

            <tr>
              <td colspan="3">
                <span style="font-size: 12pt; color: #CC3333; font: Myraid Pro;">
                  <xsl:value-of disable-output-escaping="yes" select="/Root/Customer/Name"/>, porosia juaj u krye me sukses.
                  <br />
                  P&#235;r t&#235; par&#235; detajet e porosis&#235; klikoni k&#235;tu <a href="http://www.fzone.al/personal/order/" target="_blank">www.fzone.al</a>
                  <br />
                  Ju faleminderit.
                </span>
              </td>
            </tr>
            <tr>
              <td colspan="3">
                <br />
              </td>
            </tr>

            <xsl:call-template name="Footer">
            </xsl:call-template>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>