<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="Common/Header.xslt"/>
  <xsl:include href="Common/Footer.xslt"/>
  <xsl:output method="html"/>
  <xsl:template match="/">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        <title>FZone Newsletter</title>
      </head>
      <body style="margin:0; padding:0; background-color: #e7dcdd;" bgcolor="#e7dcdd">
        <div style="width: 100%; color: #CC3333; font-family:Sans-Serif; height:auto;
        margin: 0 auto; background-color: #e7dcdd;">
          <table width="504" cellpadding="0" cellspacing="0" align="center">
            <xsl:call-template name="Header"/>
            <tr>
              <td colspan="3" style="text-align: center; padding-top: 10px;">
                <span style="font-size: 14pt; color: #CC3333; font-family:Sans-Serif;">
                  <a href="http://www.fzone.al" target="_blank" style="color: #CC3333; ">www.fzone.al</a> siti i par&#235; shqiptar i shopping online q&#235; ofron markat m&#235; t&#235; njohura t&#235; mod&#235;s me uljet m&#235; t&#235; &#231;m&#235;ndura
                </span>
                <br/>
                <br/>
              </td>
            </tr>
            <xsl:if test="/Root/Campaigns">
              <tr>
                <td colspan="3" style="text-align: center; padding-top: 10px;">
                  <span style="font-size: 18pt; color: #CC3333; font-family:Sans-Serif;">Aktualisht n&#235; FZone</span>
                  <br />
                  <br />
                </td>
              </tr>
              <xsl:for-each select="/Root/Campaigns/Campaign">
                <tr>
                  <td colspan="3" style="text-align: center;">
                    <xsl:variable name="Image" select="./Image"></xsl:variable>
                    <xsl:variable name="Url" select="./Url"></xsl:variable>
                    <xsl:variable name="Title" select="./Title"></xsl:variable>
                    <a href="{$Url}" target="_blank">
                      <img style="border: 0;" width="504" height="160" src="{$Image}"
                          alt="{$Title}" align="top" />
                    </a>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <span style="font-size: 10pt; color: #CC3333; font-family:Sans-Serif;">
                      Fushata e shitjes
                      <xsl:if test="string(./From) != ''">
                        hapet m&#235; <b><xsl:value-of disable-output-escaping="yes" select="./From"></xsl:value-of>
                        </b>
                      </xsl:if>

                      mbyllet m&#235; <b><xsl:value-of disable-output-escaping="yes" select="./To"/>
                      </b>
                      <br />
                    </span>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <br />
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:if>

            <xsl:if test="/Root/FutureCampaigns">
              <tr>
                <td colspan="3">
                  <hr style="width: 504px; height: 5px; color: #CC3333; margin-top: 10px; background-color: #CC3333;
                        border: 0; text-align: left;" />
                </td>
              </tr>
              <tr>
                <td colspan="3" style="text-align: center; padding-top: 10px;">
                  <span style="font-size: 18pt; color: #CC3333; font-family:Sans-Serif;">S&#235; shpejti n&#235; FZone</span>
                  <br />
                  <br />
                </td>
              </tr>
              <xsl:for-each select="/Root/FutureCampaigns/Campaign">
                <tr>
                  <td colspan="3" style="text-align: center;">
                    <xsl:variable name="Image" select="./Image"></xsl:variable>
                    <xsl:variable name="Url" select="./Url"></xsl:variable>
                    <xsl:variable name="Title" select="./Title"></xsl:variable>
                    <a href="{$Url}" target="_blank">
                      <img style="border: 0;" width="504" height="160" src="{$Image}"
                          alt="{$Title}" align="top" />
                    </a>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <span style="font-size: 10pt; color: #CC3333; font-family:Sans-Serif;">
                      Fushata e shitjes
                      fillon m&#235;  <b><xsl:value-of disable-output-escaping="yes" select="./From"/>
                      </b>
                      mbyllet m&#235; <b><xsl:value-of disable-output-escaping="yes" select="./To"/>
                      </b>
                      <br />
                    </span>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <br />
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:if>

            <xsl:call-template name="Footer">
              <xsl:with-param name="UnsubscribeUrl"
              select="/Root/UnsubscribeUrl"/>
            </xsl:call-template>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>