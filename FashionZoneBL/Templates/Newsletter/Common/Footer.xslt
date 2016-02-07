<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template name="Footer">
    <xsl:param name="UnsubscribeUrl"/>
    <tr>
      <td colspan="3">
        <hr style="width: 504px; height: 5px; color: #CC3333; margin-top: 10px; background-color: #CC3333;
                        border: 0; text-align: left;" />
      </td>
    </tr>
    <tr>
      <td colspan="3">
        <span style="font-size: 10pt; color: #CC3333; font-family: Myriad Pro;">
          FZone.al &#235;sht&#235; pjes&#235; e Zone Sh.p.k. Adr. Rr. e Bogdan&#235;ve/Gjon Muzaka nr.1, Tiran&#235;
          <br />
          P&#235;r m&#235; shum&#235; info: <a href="http://wwww.fzone.al" target="_blank">www.fzone.al</a> ose na kontaktoni n&#235; info@fzone.al ose n&#235; 044 530 424
          <br />
          <xsl:if test="string($UnsubscribeUrl) != ''">
            N&#235;se nuk d&#235;shironi t&#235; merrni email-e nga FZone.al klikoni
            <a>
              <xsl:attribute name="href">
                <xsl:value-of select="$UnsubscribeUrl"/>
              </xsl:attribute>k&#235;tu
            </a> p&#235;r tu c'regjistruar.
          </xsl:if>
        </span>
      </td>
    </tr>
  </xsl:template>
</xsl:stylesheet>
