<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="Common/Header.xslt"/>
  <xsl:include href="Common/Footer.xslt"/>
  <xsl:output method="html"/>
  <xsl:output doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"
          doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <title>FZone Newsletter</title>
      </head>
      <body style="margin:0; padding:0; background-color: #e7dcdd;" bgcolor="#e7dcdd">
        <div style="width: 100%; color: #CC3333; font-family:Sans-Serif; height:auto;
        margin: 0 auto; background-color: #e7dcdd;">
          <table width="504" cellpadding="0" cellspacing="0" align="center">
              <tr>
                <td width="40%">
                  <a href="http://www.fzone.al" target="_blank">
                    <img style="border: 0; width: 200px; height: 134px;" src="http://www.fzone.al/image/logo_fzone.png" alt="FZone"></img>
                  </a>
                </td>
                <td width="27%">

                </td>
                <td width="33%" style="text-align: right; vertical-align: center">
                  <span style="margin-right: 30px; font-size: 14pt; color: #CC3333; font-family:Sans-Serif;">Kap ofert&#235;n!</span>
                </td>
              </tr>
            <tr>
              <td colspan="3" style="text-align: center; padding-top: 10px;">
                <span style="font-size: 14pt; color: #CC3333; font-family:Sans-Serif;">
                  Mendoni se <a href="http://www.fzone.al" target="_blank" style="color: #CC3333; ">FashionZone</a> ju ka harruar :) ?
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
                          alt="{$Title}" align="top" ></img>
                    </a>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <span style="font-size: 10pt; color: #CC3333; font-family:Sans-Serif;">
                      Fushata e shitjes
                      <!--<xsl:if test="string(./From) != ''">
                        hapet m&#235; <b><xsl:value-of disable-output-escaping="yes" select="./From"></xsl:value-of>
                        </b>
                      </xsl:if>-->

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
            <tr>
              <td colspan="3">
                <hr style="width: 504px; height: 5px; color: #CC3333; margin-top: 10px; background-color: #CC3333;
                        border: 0; text-align: left;">
                </hr>
              </td>
            </tr>
            <tr>
              <td colspan="3">
                <span style="font-size: 9pt; color: #CC3333; font-family:Sans-Serif;">
                  FZone.al &#235;sht&#235; pjes&#235; e Zone Sh.p.k. Adr. Rr. e Bogdan&#235;ve/Gjon Muzaka nr.1, Tiran&#235;
                  <br/>
                  P&#235;r m&#235; shum&#235; info: <a href="http://wwww.fzone.al" target="_blank">www.fzone.al</a> ose na kontaktoni n&#235; info@fzone.al ose n&#235; 044 530 424
                  <br/>
                </span>
              </td>
            </tr>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>