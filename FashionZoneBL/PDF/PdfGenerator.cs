using System;
using System.Globalization;
using System.Web;
using FashionZone.DataLayer.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;
using FashionZone.BL.Util;

namespace FashionZone.BL.PDF
{
    public class PdfGenerator
    {
        /// <summary>
        /// Docs the PDF.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public static MemoryStream DocPDF(ORDERS order)
        {
            #region Stili e Margini del documento PDF
            // bordi pagina (A4 = 595 x 842 pt.)
            int _topBorder = 40;
            int _leftBorder = 10;
            int _rightBorder = 10;
            int _bottomBorder = 10;

            // Stili caratteri
            //int _normalTextSize = 12;
            int _paragraphTitleSize = 14;
            int _paragraphTextSize = 9;
            float _myLeading = 30f;
            float totlaWidth;

            Font _titlesFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, _paragraphTitleSize, Font.BOLD);
            Font _textFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, _paragraphTextSize, Font.NORMAL);
            Font _miniTitlesFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, _paragraphTextSize, Font.BOLD, BaseColor.WHITE);
            Font _miniTitlesFontStyleIns = FontFactory.GetFont(FontFactory.TIMES_ROMAN, _paragraphTextSize, Font.NORMAL);
            Font _miniTextFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL);
            #endregion

            string pathFile = @"/PdfDoc/" + Path.GetRandomFileName() + ".pdf";
            pathFile = Path.Combine(HttpContext.Current.Server.MapPath(pathFile));
            using (var doc = new Document(PageSize.A4, _leftBorder, _rightBorder, _topBorder, _bottomBorder))
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pathFile, FileMode.Create));
                doc.Open();

                totlaWidth = CreateHeader(_rightBorder, _leftBorder, _miniTextFontStyle, doc);

                Paragraph spacer = new Paragraph(_myLeading, " ");
                doc.Add(spacer);

                CreateUsrInfoTab(order, doc, _miniTitlesFontStyle, _miniTitlesFontStyleIns);

                doc.Add(spacer = new Paragraph(_myLeading, " "));

                CreateBillingTab(order, doc, totlaWidth);

                //the end
                var endTab = new PdfPTable(1) { TotalWidth = totlaWidth };
                var endCell = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = 2,
                    VerticalAlignment = 2
                };
                var msgP = new Paragraph(ConfigurationManager.AppSettings["MsgServicePDF"], _miniTextFontStyle);
                endCell.AddElement(msgP);
                endTab.AddCell(endCell);

                endCell = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = 2,
                    VerticalAlignment = 2
                };
                Font ff = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL, BaseColor.RED);
                var msgP2 = new Phrase(20f, ConfigurationManager.AppSettings["EndMesPDF"], ff);
                endCell.AddElement(msgP2);
                endCell.Border = Rectangle.NO_BORDER;
                endCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                endCell.VerticalAlignment = Element.ALIGN_JUSTIFIED;
                endTab.AddCell(endCell);
                doc.Add(endTab);

                doc.Add(spacer);
                doc.Add(spacer);

                var endTab2 = new PdfPTable(2) { TotalWidth = totlaWidth };
                var endCell2 = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = 2,
                    VerticalAlignment = 2
                };
                var msgPost = new Paragraph("Data:", _textFontStyle);
                endCell2.AddElement(msgPost);
                endTab2.AddCell(endCell2);

                var endCell3 = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = 2,
                    VerticalAlignment = 2
                };
                var msgPost2 = new Paragraph("Firma klientit: ", _textFontStyle);
                endCell3.AddElement(msgPost2);
                endTab2.AddCell(endCell3);

                doc.Add(endTab2);
                if (order.PAYMENT.Type == (int)PaymentType.CA)
                {
                    doc.Add(spacer);
                    doc.Add(spacer);

                    var cashTab = new PdfPTable(2) { TotalWidth = totlaWidth };
                    var cashCell = new PdfPCell
                    {
                        //Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = 2,
                        VerticalAlignment = 2
                    };

                    cashCell.Border = Rectangle.TOP_BORDER;
                    var msgCash = new Paragraph(_myLeading, "Paguar: " + order.PAYMENT.CASH_PAYMENT.Receiver, _textFontStyle);
                    cashCell.AddElement(msgCash);
                    cashTab.AddCell(cashCell);

                    var cashCell2 = new PdfPCell
                    {
                        HorizontalAlignment = 2,
                        VerticalAlignment = 2
                    };
                    cashCell2.Border = Rectangle.TOP_BORDER;
                    var msgCash2 = new Paragraph(_myLeading, string.Format("Firma:( {0} ): ", order.PAYMENT.CASH_PAYMENT.Receiver), _textFontStyle);//\n\r
                    cashCell2.AddElement(msgCash2);
                    cashTab.AddCell(cashCell2);

                    doc.Add(cashTab);
                }
            }
            return RequestFileStream(pathFile);
        }

        /// <summary>
        /// Creates the header.
        /// </summary>
        /// <param name="_rightBorder">The _right border.</param>
        /// <param name="_leftBorder">The _left border.</param>
        /// <param name="_miniTextFontStyle">The _mini text font style.</param>
        /// <param name="doc">The doc.</param>
        /// <returns></returns>
        private static float CreateHeader(int _rightBorder, int _leftBorder, Font _miniTextFontStyle, Document doc)
        {
            float totlaWidth;
            //create document header; shows GMT time when PDF created.
            //HeaderFooter class removed in iText 5.0.0, so we instead write 
            //content to an **absolute** position on the document
            Rectangle page = doc.PageSize;

            totlaWidth = page.Width - (_rightBorder + _leftBorder);


            PdfPTable head = new PdfPTable(2);
            head.TotalWidth = totlaWidth;
            //add image to document
            string pathImgHeader = Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["HeaderLogoPDF"]));
            Image gifHeader = Image.GetInstance(pathImgHeader);
            gifHeader.Alignment = Image.ALIGN_LEFT;
            // downsize the image by specified percentage        
            gifHeader.ScalePercent(28f);

            PdfPCell c = new PdfPCell(gifHeader);
            c.Border = Rectangle.NO_BORDER;
            c.VerticalAlignment = Element.ALIGN_JUSTIFIED;
            c.HorizontalAlignment = Element.ALIGN_LEFT;
            head.AddCell(c);
            c = new PdfPCell(new Paragraph(ConfigurationManager.AppSettings["HeaderPDF"], _miniTextFontStyle));
            c.Border = Rectangle.NO_BORDER;
            c.VerticalAlignment = Element.ALIGN_TOP;
            c.HorizontalAlignment = Element.ALIGN_RIGHT;
            head.AddCell(c);
            doc.Add(head);
            return totlaWidth;
        }

        private static void CreateBillingTab(ORDERS order, Document doc, float totalWidth)
        {
            var nrFatFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.PINK);
            var datFatFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, Font.NORMAL, BaseColor.BLACK);
            var headStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.WHITE);
            var itemsStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, BaseColor.BLACK);
            var emptyStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 1, Font.NORMAL, BaseColor.BLACK);

            #region Nr & data
            var tblFatturaAllContent = new PdfPTable(1);
            var cF1 = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = GrayColor.WHITE,
                BorderWidthTop = 1f,
                BorderColorTop = BaseColor.GRAY,
                BorderWidthRight = 1f,
                BorderColorRight = BaseColor.GRAY,
                BorderWidthLeft = 1f,
                BorderColorLeft = BaseColor.GRAY
            };
            var fatPh = new Phrase { Leading = 20f };
            var fatCh =
                new Chunk(ConfigurationManager.AppSettings["TitFatPDF"] + String.Format(Configuration.OrderNrFormatting, order.ID),
                          nrFatFontStyle);
            fatPh.Add(fatCh);
            fatCh = new Chunk("          ");
            fatPh.Add(fatCh);
            fatCh = new Chunk("data: " + order.DateCreated.ToString("d"), datFatFontStyle);
            fatPh.Add(fatCh);
            cF1.AddElement(fatPh);
            cF1.AddElement(new Paragraph(10, " "));
            tblFatturaAllContent.AddCell(cF1);
            doc.Add(tblFatturaAllContent);
            #endregion

            var tblFattura = new PdfPTable(10) { TotalWidth = totalWidth };
            var widths = new float[] { 5f, 25f, 60f, 220f, 70f, 45f, 50f, 40f, 55f, 5f }; //total575
            tblFattura.SetWidths(widths);
            #region Header LEFT
            //cell spacer Left
            var cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthLeft = 1f,
                BorderColorLeft = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.DARK_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase("Nr.", headStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitArtPDF"], headStyle));
            tblFattura.AddCell(cellFat);


            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.DARK_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitDeskPDF"], headStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitSum-VatPDF"], headStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.DARK_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitVATPDF"], headStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitSum+VatPDF"], headStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.DARK_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitQuantPDF"], headStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                //BorderWidthBottom = 1f,
                //BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitTotPDF"], headStyle));
            tblFattura.AddCell(cellFat);

            //CELL spacer
            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthRight = 1f,
                BorderColorRight = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            };
            cellFat.AddElement(new Phrase(" "));
            tblFattura.AddCell(cellFat);
            #endregion

            #region BODY
            int count = 1;
            int num = 1;
            foreach (var singleDett in order.ORDER_DETAIL)
            {
                while (count < 11)
                {
                    switch (count)
                    {
                        case 1:
                            FirstColumnInvoice(tblFattura);
                            break;
                        case 10:
                            LastColumnInvoice(tblFattura);
                            break;
                        default:
                            WriteBodyDettailOrder(singleDett, tblFattura, count, itemsStyle, ref num);
                            break;
                    }
                    count += 1;
                }
                count = 1;
            }
            #endregion

            #region Shippemnt

            decimal ShppingCost = order.SHIPPING.ShippingCost;
            FirstColumnInvoice(tblFattura);
            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
                Colspan = 3
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["ShippingMsgPDF"], itemsStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(CalcTvsh(ShppingCost) + " €", itemsStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TVSHPDF"] + "%", itemsStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(ShppingCost.ToString() + " €", itemsStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(" "));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(ShppingCost.ToString() + "€", itemsStyle));
            tblFattura.AddCell(cellFat);

            LastColumnInvoice(tblFattura);

            #endregion

            #region Summ row
            FirstColumnInvoice(tblFattura);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
                Colspan = 5
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellFat.AddElement(new Phrase(" "));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
                Colspan = 2
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            if (order.BonusUsed.HasValue && order.BonusUsed.Value != 0)
            {
                string totPlusBonus = string.Format(
@"{0} 
{1}
{2}
{3}", ConfigurationManager.AppSettings["TitTotPDF"],
    ConfigurationManager.AppSettings["BonusPDF"], 
    ConfigurationManager.AppSettings["TitTotPagPDF"], 
    " ");
                cellFat.AddElement(new Phrase(totPlusBonus, itemsStyle));
            }
            else
            cellFat.AddElement(new Phrase(ConfigurationManager.AppSettings["TitTotPDF"], itemsStyle));
            tblFattura.AddCell(cellFat);

            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY
            };
            cellFat.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            if (order.BonusUsed.HasValue && order.BonusUsed.Value != 0)
            {
                string totPlusBonus = string.Format(
@"{0} 
-{1}€ 
{2}€
{3}", order.TotalAmount, 
     order.BonusUsed.Value, 
     order.AmountPaid, 
     " ");
                cellFat.AddElement(new Phrase(totPlusBonus, itemsStyle));
            }
            else
                cellFat.AddElement(new Phrase(order.TotalAmount + " €", itemsStyle));
            tblFattura.AddCell(cellFat);

            LastColumnInvoice(tblFattura);
            #endregion

            #region footer
            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                BorderWidthRight = 1f,
                BorderColorRight = BaseColor.GRAY,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
                BorderWidthLeft = 1f,
                BorderColorLeft = BaseColor.GRAY,
                Colspan = 10
            };
            cellFat.AddElement(new Phrase(" "));
            tblFattura.AddCell(cellFat);
            #endregion

            doc.Add(tblFattura);
        }

        private static void WriteBodyDettailOrder(ORDER_DETAIL orderDett, PdfPTable tblFattura, int count, Font itemsStyle, ref int nrItem)
        {
            PdfPCell cellFat;
            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                BorderWidthBottom = 1f,
                BorderColorBottom = BaseColor.GRAY,
                HorizontalAlignment = 2
            };
            string info = string.Empty;
            switch (count)
            {
                case 2:
                    info = nrItem.ToString();
                    nrItem += 1;
                    break;
                case 3:
                    info = String.Format(FashionZone.BL.Configuration.OrderNrFormatting, orderDett.ProdAttrID);
                    break;
                case 4:
                    info = orderDett.ProductNameWithSizeAndCode;
                    break;
                case 5:
                    info = CalcTvsh(orderDett.UnitPrice.Value);
                    break;
                case 6:
                    info = ConfigurationManager.AppSettings["TVSHPDF"];
                    break;
                case 7:
                    info = orderDett.UnitPrice.Value.ToString();
                    break;
                case 8:
                    info = orderDett.Quantity.ToString();
                    break;
                case 9:
                    info = (orderDett.UnitPrice * orderDett.Quantity).ToString();
                    break;
            }
            cellFat.AddElement(new Phrase(info, itemsStyle));
            tblFattura.AddCell(cellFat);
        }

        private static void LastColumnInvoice(PdfPTable tblFattura)
        {
            PdfPCell cellFat;
            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                //BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthRight = 1f,
                BorderColorRight = BaseColor.GRAY
            };
            cellFat.AddElement(new Phrase(" "));
            tblFattura.AddCell(cellFat);
        }

        /// <summary>
        /// Firsts the column invoice.
        /// </summary>
        /// <param name="tblFattura">The TBL fattura.</param>
        private static void FirstColumnInvoice(PdfPTable tblFattura)
        {
            PdfPCell cellFat;
            //spacer
            cellFat = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                //BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderWidthLeft = 1f,
                BorderColorLeft = BaseColor.GRAY
            };
            cellFat.AddElement(new Phrase(" "));
            tblFattura.AddCell(cellFat);
        }

        /// <summary>
        /// Creates the usr info tab.
        /// </summary>
        /// <param name="OrderDett">The order dett.</param>
        /// <param name="doc">The doc.</param>
        /// <param name="_miniTitlesFontStyle">The _mini titles font style.</param>
        /// <param name="_miniTitlesFontStyleIns">The _mini titles font style ins.</param>
        private static void CreateUsrInfoTab(ORDERS OrderDett, Document doc, Font _miniTitlesFontStyle, Font _miniTitlesFontStyleIns)
        {
            PdfPTable tblUsrInfo = new PdfPTable(3);
            #region HeaderTable
            PdfPCell cU1 = new PdfPCell(new Phrase(ConfigurationManager.AppSettings["TabH1PDF"], _miniTitlesFontStyle));
            cU1.Border = Rectangle.NO_BORDER;
            cU1.FixedHeight = 20f;
            cU1.VerticalAlignment = Element.ALIGN_CENTER;
            cU1.HorizontalAlignment = Element.ALIGN_CENTER;
            cU1.BackgroundColor = BaseColor.GRAY;
            //cU1.Border = Rectangle.RIGHT_BORDER;
            //cU1.BorderColor = GrayColor.WHITE;
            //cU1.BorderWidth = 3f;
            tblUsrInfo.AddCell(cU1);

            cU1 = new PdfPCell(new Phrase(ConfigurationManager.AppSettings["TabH2PDF"], _miniTitlesFontStyle));
            cU1.Border = Rectangle.RIGHT_BORDER;
            cU1.BorderWidthRight = 3f;
            cU1.BorderColorRight = BaseColor.WHITE;
            cU1.Border = Rectangle.LEFT_BORDER;
            cU1.BorderWidthLeft = 3f;
            cU1.BorderColorLeft = BaseColor.WHITE;
            cU1.FixedHeight = 20f;
            cU1.VerticalAlignment = Element.ALIGN_CENTER;
            cU1.HorizontalAlignment = Element.ALIGN_CENTER;
            cU1.BackgroundColor = BaseColor.GRAY;
            tblUsrInfo.AddCell(cU1);

            cU1 = new PdfPCell(new Phrase(ConfigurationManager.AppSettings["TabH3PDF"], _miniTitlesFontStyle));
            cU1.Border = Rectangle.LEFT_BORDER;
            cU1.BorderWidthLeft = 3f;
            cU1.FixedHeight = 20f;
            cU1.VerticalAlignment = Element.ALIGN_CENTER;
            cU1.HorizontalAlignment = Element.ALIGN_CENTER;
            cU1.BackgroundColor = BaseColor.GRAY;
            cU1.BorderColor = BaseColor.WHITE;
            tblUsrInfo.AddCell(cU1);

            doc.Add(tblUsrInfo);
            #endregion

            doc.Add(new Paragraph(3, " "));

            #region BodyTable
            tblUsrInfo = new PdfPTable(3);
            PdfPCell cU2 = new PdfPCell();

            var phOrd = new Phrase();
            WriteInfoOrder(OrderDett, ref phOrd, _miniTitlesFontStyleIns);
            cU2.AddElement(phOrd);
            cU2.Border = Rectangle.NO_BORDER;
            cU2.FixedHeight = 110f;
            cU2.PaddingLeft = 10f;
            cU2.BorderWidthLeft = 6f;
            cU1.BorderColorLeft = BaseColor.GRAY;
            cU2.VerticalAlignment = Element.ALIGN_TOP;
            cU2.HorizontalAlignment = Element.ALIGN_LEFT;
            tblUsrInfo.AddCell(cU2);

            cU2 = new PdfPCell();
            WriteAddress(OrderDett.ShippingAddress, ref phOrd, _miniTitlesFontStyleIns);
            cU2.AddElement(phOrd);
            cU2.Border = Rectangle.NO_BORDER;
            cU2.BorderWidthLeft = 3f;
            cU1.BorderColorLeft = BaseColor.GRAY;
            cU2.PaddingLeft = 10f;
            cU2.VerticalAlignment = Element.ALIGN_TOP;
            cU2.HorizontalAlignment = Element.ALIGN_LEFT;
            tblUsrInfo.AddCell(cU2);

            cU2 = new PdfPCell();
            WriteAddress(OrderDett.BillingAddress, ref phOrd, _miniTitlesFontStyleIns);
            cU2.AddElement(phOrd);
            cU2.Border = Rectangle.NO_BORDER;
            cU2.PaddingLeft = 10f;
            cU2.BorderWidthLeft = 3f;
            cU1.BorderColorLeft = BaseColor.GRAY;
            cU2.BorderWidthRight = 6f;
            cU1.BorderColorRight = BaseColor.GRAY;
            cU2.VerticalAlignment = Element.ALIGN_TOP;
            cU2.HorizontalAlignment = Element.ALIGN_LEFT;
            tblUsrInfo.AddCell(cU2);

            doc.Add(tblUsrInfo);
            #endregion
        }

        /// <summary>
        /// Writes the address.
        /// </summary>
        /// <param name="orderDettId">The order dett id.</param>
        /// <param name="phOrd">The ph ord.</param>
        /// <param name="miniTitlesFontStyleIns">The mini titles font style ins.</param>
        private static void WriteAddress(int? orderDettId, ref Phrase phOrd, Font miniTitlesFontStyleIns)
        {
            if (orderDettId.HasValue)
            {
                var addressShipp = new ADDRESS(ApplicationContext.Current.Orders.GetAddress(orderDettId.Value));
                {
                    string addSh = string.Format(
@"{0}
{1}
{2}
{3}
{4}
{5}", addressShipp.Name,
    addressShipp.Address1,
    addressShipp.Location,
    addressShipp.PostCode,
    addressShipp.StateName,
    addressShipp.Telephone);
                    phOrd = new Phrase(addSh, miniTitlesFontStyleIns);
                }
            }
            else
            {
                phOrd = new Phrase(" ", miniTitlesFontStyleIns);
            }
        }

        /// <summary>
        /// Writes the info order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="phOrd">The ph ord.</param>
        /// <param name="miniTitlesFontStyleIns">The mini titles font style ins.</param>
        private static void WriteInfoOrder(ORDERS order, ref Phrase phOrd, Font miniTitlesFontStyleIns)
        {
            string paymentOrder = String.Empty;
            decimal paid = 0;
            switch (order.PAYMENT.Type)
            {
                case (int)PaymentType.CA :
                    paymentOrder = "Cash";
                    paid = order.PAYMENT.CASH_PAYMENT.Amount.Value;
                    break;
                case (int)PaymentType.PP:
                    paymentOrder = "PayPal";
                    paid = order.PAYMENT.PAYPAL_PAYMENT.Amount.Value;
                    break;
                case (int)PaymentType.EP:
                    paymentOrder = "EasyPay";
                    paid = order.PAYMENT.EASYPAY_PAYMENT.Amount;
                    break;
            }
            if (order != null)
            {
                string addSh = string.Format(
@"Fatura nr: {0}
Data faturës: {1}
Tipi i pagimit: {2}
Paguar: {3}
Blerësi:{4}
{5}
Kodi: {6}",
                    String.Format(FashionZone.BL.Configuration.OrderNrFormatting, order.ID),
                    order.DateCreated.ToString("dd/MM/yyyy hh:mm"),
                    paymentOrder, paid.ToString("N2") + (order.PAYMENT.Type == (int)PaymentType.EP ? " Lek" : " €"), order.CustomerName, order.CUSTOMER.Email,
                    order.VerificationNumber);
                phOrd = new Phrase(addSh, miniTitlesFontStyleIns);
            }
        }

        /// <summary>
        /// Calcs the TVSH.
        /// </summary>
        /// <param name="Cost">The cost.</param>
        /// <returns></returns>
        private static string CalcTvsh(Decimal Cost)
        {
            var costNoTvsh = new double();
            var tvshByConfig = ConfigurationManager.AppSettings["TVSHPDF"];
            double tvsh;
            if (double.TryParse(tvshByConfig, out tvsh))
            {
                var cost = double.Parse(Cost.ToString());
                costNoTvsh = cost * (100 / (100 + tvsh));
            }
            return costNoTvsh.ToString(ConfigurationManager.AppSettings["TVSHFormatPDF"], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Requests the file stream.
        /// </summary>
        /// <param name="requestFilePath">The request file path.</param>
        /// <returns></returns>
        private static MemoryStream RequestFileStream(String requestFilePath)
        {
            using (var m = new MemoryStream())
            {
                PdfReader reader = new PdfReader(requestFilePath);
                PdfStamper stamper = new PdfStamper(reader, m);
                stamper.ViewerPreferences = PdfWriter.PageLayoutSinglePage | PdfWriter.PageModeUseThumbs;
                stamper.Close();
                deleteFile(requestFilePath);
                return m;
            }
        }

        private static void deleteFile(string path)
        {
            try
            {
                //string[] txtList = Directory.GetFiles(path, "*.pdf");
                //foreach (var file in txtList)
                //{
                File.Delete(path);
                //}
            }
            catch {}
        }

    }
}
