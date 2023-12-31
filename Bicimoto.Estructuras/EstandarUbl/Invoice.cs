﻿using Bicimoto.Comun;
using Bicimoto.Comun.Constantes;
using Bicimoto.Estructuras.CommonAggregateComponents;
using Bicimoto.Estructuras.CommonExtensionComponents;
using Bicimoto.Estructuras.SunatAggregateComponents;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Bicimoto.Estructuras.EstandarUbl
{
    [Serializable]
    public class Invoice : IXmlSerializable, IEstructuraXml
    {
        public DateTime IssueDate { get; set; }

        public UblExtensions UblExtensions { get; set; }

        public SignatureCac Signature { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public string InvoiceTypeCode { get; set; }

        public string Id { get; set; }

        public AccountingSupplierParty AccountingCustomerParty { get; set; }

        public PaymentTerms PaymentTerms { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; }

        public List<InvoiceDocumentReference> DespatchDocumentReferences { get; set; }

        public List<InvoiceDocumentReference> AdditionalDocumentReferences { get; set; }

        public string DocumentCurrencyCode { get; set; }

        public List<TaxTotal> TaxTotals { get; set; }

        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }

        public BillingPayment PrepaidPayment { get; set; }

        public string UblVersionId { get; set; }

        public string CustomizationId { get; set; }

        public InvoiceTypeCode InvoiceTypeCode2 { get; set; }
        public DocumentCurrencyCode DocumentCurrencyCode2 { get; set; }
        public PartyIdentification2 PartyIdentification2 { get; set; }
        public IFormatProvider Formato { get; set; }
        public string LineCountNumeric { get; set; }

        public Invoice()
        {
            AccountingSupplierParty = new AccountingSupplierParty();
            AccountingCustomerParty = new AccountingSupplierParty();
            DespatchDocumentReferences = new List<InvoiceDocumentReference>();
            AdditionalDocumentReferences = new List<InvoiceDocumentReference>();
            UblExtensions = new UblExtensions();
            Signature = new SignatureCac();
            InvoiceLines = new List<InvoiceLine>();
            TaxTotals = new List<TaxTotal>();
            LegalMonetaryTotal = new LegalMonetaryTotal();
            UblVersionId = "2.0";
            CustomizationId = "1.0";
            Formato = new System.Globalization.CultureInfo(Formatos.Cultura);
            InvoiceTypeCode2 = new Comun.Constantes.InvoiceTypeCode();
            DocumentCurrencyCode2 = new Comun.Constantes.DocumentCurrencyCode();
            PartyIdentification2 = new PartyIdentification2();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("xmlns", EspacioNombres.xmlnsInvoice);
            writer.WriteAttributeString("xmlns:cac", EspacioNombres.cac);
            writer.WriteAttributeString("xmlns:cbc", EspacioNombres.cbc);
            writer.WriteAttributeString("xmlns:ccts", EspacioNombres.ccts);
            writer.WriteAttributeString("xmlns:ds", EspacioNombres.ds);
            writer.WriteAttributeString("xmlns:ext", EspacioNombres.ext);
            writer.WriteAttributeString("xmlns:qdt", EspacioNombres.qdt);
            writer.WriteAttributeString("xmlns:sac", EspacioNombres.sac);
            writer.WriteAttributeString("xmlns:udt", EspacioNombres.udt);
            writer.WriteAttributeString("xmlns:xsi", EspacioNombres.xsi);

            #region UBLExtensions

            writer.WriteStartElement("ext:UBLExtensions");

            #region UBLExtension

            var ext2 = UblExtensions.Extension2.ExtensionContent.AdditionalInformation;

            #endregion UBLExtension

            #region UBLExtension

            writer.WriteStartElement("ext:UBLExtension");

            #region ExtensionContent

            writer.WriteStartElement("ext:ExtensionContent");

            // En esta zona va el certificado digital.

            writer.WriteEndElement();

            #endregion ExtensionContent

            writer.WriteEndElement();

            #endregion UBLExtension

            writer.WriteEndElement();

            #endregion UBLExtensions

            writer.WriteElementString("cbc:UBLVersionID", UblVersionId);
            writer.WriteElementString("cbc:CustomizationID", CustomizationId);

            writer.WriteElementString("cbc:ID", Id);
            writer.WriteElementString("cbc:IssueDate", IssueDate.ToString("yyyy-MM-dd"));
            writer.WriteElementString("cbc:IssueTime", String.Format("{0:HH:mm:ss}", DateTime.Now));
            writer.WriteElementString("cbc:DueDate", IssueDate.ToString("yyyy-MM-dd")); //Fecha de vencimiento de Documento F-B

            writer.WriteStartElement("cbc:InvoiceTypeCode");
            writer.WriteAttributeString("listID", ext2.SunatTransaction.Id); //Tipo de Venta Interna
            writer.WriteAttributeString("listAgencyName", InvoiceTypeCode2.listAgencyName);
            writer.WriteAttributeString("listName", InvoiceTypeCode2.listName); //ERROR CDR 4252 PROBAR
            writer.WriteAttributeString("listURI", InvoiceTypeCode2.listURI);
            writer.WriteValue(InvoiceTypeCode); //Tipo de Documento 03 Boleta - 01 Factura
            writer.WriteEndElement();

            writer.WriteStartElement("cbc:Note");
            writer.WriteAttributeString("languageLocaleID", "1000"); //1000 Monto en letras
            writer.WriteValue(ext2.AdditionalProperties[0].Value);
            writer.WriteEndElement();

            writer.WriteStartElement("cbc:DocumentCurrencyCode");
            writer.WriteAttributeString("listID", DocumentCurrencyCode2.listID);
            writer.WriteAttributeString("listName", DocumentCurrencyCode2.listName);
            writer.WriteAttributeString("listAgencyName", DocumentCurrencyCode2.listAgencyName);
            writer.WriteValue(DocumentCurrencyCode);
            writer.WriteEndElement();

            writer.WriteElementString("cbc:LineCountNumeric", LineCountNumeric); //Cantidad de Items de la factura

            #region DespatchDocumentReferences

            foreach (var reference in DespatchDocumentReferences)
            {
                writer.WriteStartElement("cac:DespatchDocumentReference");
                {
                    writer.WriteElementString("cbc:ID", reference.Id);
                    writer.WriteElementString("cbc:DocumentTypeCode", reference.DocumentTypeCode);
                }
                writer.WriteEndElement();
            }

            #endregion DespatchDocumentReferences

            #region Signature

            writer.WriteStartElement("cac:Signature");
            writer.WriteElementString("cbc:ID", Signature.Id);

            #region SignatoryParty

            writer.WriteStartElement("cac:SignatoryParty");

            writer.WriteStartElement("cac:PartyIdentification");
            writer.WriteElementString("cbc:ID", Signature.SignatoryParty.PartyIdentification.Id.Value);
            writer.WriteEndElement();

            #region PartyName

            writer.WriteStartElement("cac:PartyName");

            writer.WriteElementString("cbc:Name", Signature.SignatoryParty.PartyName.Name);

            writer.WriteEndElement();

            #endregion PartyName

            writer.WriteEndElement();

            #endregion SignatoryParty

            #region DigitalSignatureAttachment

            writer.WriteStartElement("cac:DigitalSignatureAttachment");

            writer.WriteStartElement("cac:ExternalReference");
            writer.WriteElementString("cbc:URI", Signature.DigitalSignatureAttachment.ExternalReference.Uri.Trim());
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion DigitalSignatureAttachment

            writer.WriteEndElement();

            #endregion Signature

            #region AccountingSupplierParty

            writer.WriteStartElement("cac:AccountingSupplierParty");

            #region Party

            writer.WriteStartElement("cac:Party");

            #region PartyIdentification

            writer.WriteStartElement("cac:PartyIdentification");
            writer.WriteStartElement("cbc:ID");
            writer.WriteAttributeString("schemeID", AccountingSupplierParty.AdditionalAccountId); //Codigo de identificacion de documento de contribuyente
            writer.WriteAttributeString("schemeName", PartyIdentification2.schemeName); //CDR ERROR 4255 ARREGLADO
            writer.WriteAttributeString("schemeAgencyName", InvoiceTypeCode2.listAgencyName);
            writer.WriteAttributeString("schemeURI", PartyIdentification2.schemeURI);
            writer.WriteValue(AccountingSupplierParty.CustomerAssignedAccountId);
            writer.WriteEndElement();
            writer.WriteEndElement();

            #endregion PartyIdentification

            #region PartyName

            writer.WriteStartElement("cac:PartyName");

            writer.WriteStartElement("cbc:Name");
            writer.WriteCData(AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName);
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion PartyName

            #region PostalAddress

            /*writer.WriteStartElement("cac:PostalAddress");
            writer.WriteElementString("cbc:ID", AccountingSupplierParty.Party.PostalAddress.ID);
            writer.WriteElementString("cbc:StreetName", AccountingSupplierParty.Party.PostalAddress.StreetName);
            if (!string.IsNullOrEmpty(AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName))
                writer.WriteElementString("cbc:CitySubdivisionName", AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName);
            writer.WriteElementString("cbc:CityName", AccountingSupplierParty.Party.PostalAddress.CityName);
            writer.WriteElementString("cbc:CountrySubentity", AccountingSupplierParty.Party.PostalAddress.CountrySubentity);
            writer.WriteElementString("cbc:District", AccountingSupplierParty.Party.PostalAddress.District);

            #region Country

            writer.WriteStartElement("cac:Country");
            writer.WriteElementString("cbc:IdentificationCode",
                AccountingSupplierParty.Party.PostalAddress.Country.IdentificationCode);
            writer.WriteEndElement();

            #endregion Country

            writer.WriteEndElement();*/

            #endregion PostalAddress

            #region PartyLegalEntity

            writer.WriteStartElement("cac:PartyLegalEntity");
            writer.WriteStartElement("cbc:RegistrationName");
            writer.WriteCData(AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName);
            writer.WriteEndElement();

            writer.WriteStartElement("cac:RegistrationAddress");
            writer.WriteElementString("cbc:AddressTypeCode", AccountingSupplierParty.CodDomicilioFiscal); //Código del domicilio fiscal sunat
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion PartyLegalEntity

            writer.WriteEndElement();

            #endregion Party

            writer.WriteEndElement();

            #endregion AccountingSupplierParty

            #region AccountingCustomerParty

            writer.WriteStartElement("cac:AccountingCustomerParty");

            #region Party

            writer.WriteStartElement("cac:Party");

            #region PartyIdentification

            writer.WriteStartElement("cac:PartyIdentification");
            writer.WriteStartElement("cbc:ID");
            writer.WriteAttributeString("schemeID", AccountingCustomerParty.AdditionalAccountId); //Codigo de identificacion de documento de cliente
            writer.WriteAttributeString("schemeName", PartyIdentification2.schemeName);
            writer.WriteAttributeString("schemeAgencyName", InvoiceTypeCode2.listAgencyName);
            writer.WriteAttributeString("schemeURI", PartyIdentification2.schemeURI);
            writer.WriteValue(AccountingCustomerParty.CustomerAssignedAccountId);
            writer.WriteEndElement();
            writer.WriteEndElement();

            #endregion PartyIdentification

            #region cbc:PartyLegalEntity

            writer.WriteStartElement("cac:PartyLegalEntity");

            writer.WriteStartElement("cbc:RegistrationName");
            writer.WriteCData(AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName);
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion cbc:PartyLegalEntity

            writer.WriteEndElement();

            #endregion Party

            writer.WriteEndElement();

            #endregion AccountingCustomerParty

          

            #region PaymentTerms

            writer.WriteStartElement("cac:PaymentTerms");

            writer.WriteStartElement("cbc:ID");
            writer.WriteString(PaymentTerms.ID);
            writer.WriteEndElement();
            writer.WriteStartElement("cbc:PaymentMeansID");
            writer.WriteString(PaymentTerms.PaymentMeansID);
            writer.WriteEndElement();
            writer.WriteEndElement();

            #endregion PaymentTerms

            #region PrepaidPayment

            if (PrepaidPayment != null)
            {
                writer.WriteStartElement("cac:PrepaidPayment");
                {
                    writer.WriteStartElement("cbc:ID");
                    {
                        writer.WriteAttributeString("schemeID", PrepaidPayment.Id.SchemeId);
                        writer.WriteValue(PrepaidPayment.Id.Value);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("cbc:PaidAmount");
                    {
                        writer.WriteAttributeString("currencyID", PrepaidPayment.PaidAmount.CurrencyId);
                        writer.WriteValue(PrepaidPayment.PaidAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("cbc:InstructionID");
                    {
                        writer.WriteAttributeString("schemeID", "6");
                        writer.WriteValue(PrepaidPayment.InstructionId);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            #endregion PrepaidPayment

            #region AllowanceCharge Descuentos

            foreach (var taxTotal in TaxTotals)
            {
                if (LegalMonetaryTotal.AllowanceTotalAmount.Value > 0) //Evalua si hay descuento
                {
                    // < !--En esta sección se ingresara el monto de descuento-->
                    writer.WriteStartElement("cac:AllowanceCharge");
                    writer.WriteElementString("cbc:ChargeIndicator", "false");
                    writer.WriteElementString("cbc:AllowanceChargeReasonCode", "0.00");
                    writer.WriteElementString("cbc:MultiplierFactorNumeric", "0.00");
                    // < !--Monto del descuento-->
                    writer.WriteStartElement("cbc:Amount");
                    writer.WriteAttributeString("currencyID", DocumentCurrencyCode);
                    writer.WriteValue(LegalMonetaryTotal.AllowanceTotalAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();
                    //Monto del cual se hará el descuento
                    writer.WriteStartElement("cbc:BaseAmount");
                    writer.WriteAttributeString("currencyID", DocumentCurrencyCode);
                    writer.WriteValue(taxTotal.TaxableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }

            #endregion AllowanceCharge Descuentos

            #region TaxTotal

            foreach (var taxTotal in TaxTotals)
            {
                writer.WriteStartElement("cac:TaxTotal");

                writer.WriteStartElement("cbc:TaxAmount");
                writer.WriteAttributeString("currencyID", taxTotal.TaxAmount.CurrencyId);
                writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                #region TaxSubtotal

                {
                    writer.WriteStartElement("cac:TaxSubtotal");

                    writer.WriteStartElement("cbc:TaxableAmount");
                    writer.WriteAttributeString("currencyID", taxTotal.TaxableAmount.CurrencyId);
                    writer.WriteString(taxTotal.TaxableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    writer.WriteStartElement("cbc:TaxAmount");
                    writer.WriteAttributeString("currencyID", taxTotal.TaxSubtotal.TaxAmount.CurrencyId);
                    writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    #region TaxCategory

                    {
                        writer.WriteStartElement("cac:TaxCategory");

                        #region ID

                        writer.WriteStartElement("cbc:ID");
                        writer.WriteAttributeString("schemeID", "UN/ECE 5305");
                        writer.WriteAttributeString("schemeName", "Tax Category Identifier");
                        writer.WriteAttributeString("schemeAgencyName", "United Nations Economic Commission for Europe");

                        writer.WriteValue(taxTotal.TaxSubtotal.TaxCategory.Identifier); //VALOR OBTENIDO DE LA TABLA 5
                        writer.WriteEndElement();

                        #endregion ID

                        #region TaxScheme

                        {
                            writer.WriteStartElement("cac:TaxScheme");

                            //writer.WriteElementString("cbc:ID", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID);

                            writer.WriteStartElement("cbc:ID");
                            writer.WriteAttributeString("schemeID", "UN/ECE 5305");
                            writer.WriteAttributeString("schemeAgencyID", "6");
                            writer.WriteValue(taxTotal.TaxSubtotal.TaxCategory.TaxScheme.Id);
                            writer.WriteEndElement();

                            writer.WriteElementString("cbc:Name", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.Name);
                            writer.WriteElementString("cbc:TaxTypeCode", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode);

                            writer.WriteEndElement();
                        }

                        #endregion TaxScheme

                        writer.WriteEndElement();
                    }

                    #endregion TaxCategory

                    writer.WriteEndElement();
                }

                #endregion TaxSubtotal

                writer.WriteEndElement();
            }

            #endregion TaxTotal

            #region LegalMonetaryTotal

            writer.WriteStartElement("cac:LegalMonetaryTotal");
            {
                //Descuento
                if (LegalMonetaryTotal.AllowanceTotalAmount.Value > 0)
                {
                    writer.WriteStartElement("cbc:AllowanceTotalAmount");
                    {
                        writer.WriteAttributeString("currencyID", LegalMonetaryTotal.AllowanceTotalAmount.CurrencyId);
                        writer.WriteValue(LegalMonetaryTotal.AllowanceTotalAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();
                }
                if (LegalMonetaryTotal.PrepaidAmount.Value > 0)
                {
                    writer.WriteStartElement("cbc:PrepaidAmount");
                    {
                        writer.WriteAttributeString("currencyID", LegalMonetaryTotal.PrepaidAmount.CurrencyId);
                        writer.WriteValue(LegalMonetaryTotal.PrepaidAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();
                }

                //SE AGREGA FALTANTES CDR LINE EXTENSION AMOUNT  Y TAXINCLUSIVEAMOUNT

                writer.WriteStartElement("cbc:LineExtensionAmount");
                {
                    writer.WriteAttributeString("currencyID", LegalMonetaryTotal.PayableAmount.CurrencyId);
                    writer.WriteValue(LegalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();

                writer.WriteStartElement("cbc:TaxInclusiveAmount");
                {
                    writer.WriteAttributeString("currencyID", LegalMonetaryTotal.PayableAmount.CurrencyId);
                    writer.WriteValue(LegalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();


                writer.WriteStartElement("cbc:PayableAmount");
                {
                    writer.WriteAttributeString("currencyID", LegalMonetaryTotal.PayableAmount.CurrencyId);
                    writer.WriteValue(LegalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            #endregion LegalMonetaryTotal

            #region InvoiceLines

            foreach (var invoiceLine in InvoiceLines)
            {
                writer.WriteStartElement("cac:InvoiceLine");

                writer.WriteElementString("cbc:ID", invoiceLine.Id.ToString());

                #region InvoicedQuantity

                writer.WriteStartElement("cbc:InvoicedQuantity");
                writer.WriteAttributeString("unitCode", invoiceLine.InvoicedQuantity.UnitCode);
                writer.WriteAttributeString("unitCodeListID", "UN/ECE rec 20");
                writer.WriteAttributeString("unitCodeListAgencyName", "United Nations Economic Commission for Europe");
                writer.WriteValue(invoiceLine.InvoicedQuantity.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                #endregion InvoicedQuantity

                #region LineExtensionAmount

                writer.WriteStartElement("cbc:LineExtensionAmount");
                writer.WriteAttributeString("currencyID", invoiceLine.LineExtensionAmount.CurrencyId);
                writer.WriteValue(invoiceLine.LineExtensionAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                #endregion LineExtensionAmount

                #region PricingReference

                writer.WriteStartElement("cac:PricingReference");

                #region AlternativeConditionPrice

                foreach (var item in invoiceLine.PricingReference.AlternativeConditionPrices)
                {
                    writer.WriteStartElement("cac:AlternativeConditionPrice");

                    #region PriceAmount

                    writer.WriteStartElement("cbc:PriceAmount");
                    writer.WriteAttributeString("currencyID", item.PriceAmount.CurrencyId);
                    writer.WriteValue(item.PriceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    #endregion PriceAmount

                    // writer.WriteElementString("cbc:PriceTypeCode", item.PriceTypeCode);
                    writer.WriteStartElement("cbc:PriceTypeCode");
                    //writer.WriteAttributeString("listName", "SUNAT:Indicador de Tipo de Precio");
                    writer.WriteAttributeString("listName" ,"Tipo de Precio"); // CDR ERROR 4252
                    writer.WriteAttributeString("listAgencyName", "PE:SUNAT");
                    writer.WriteAttributeString("listURI", "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16");
                    writer.WriteValue(item.PriceTypeCode);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }

                #endregion AlternativeConditionPrice

                writer.WriteEndElement();

                #endregion PricingReference

                #region AllowanceCharge

                if (invoiceLine.AllowanceCharge.ChargeIndicator)
                {
                    writer.WriteStartElement("cac:AllowanceCharge");

                    writer.WriteElementString("cbc:ChargeIndicator", invoiceLine.AllowanceCharge.ChargeIndicator.ToString());

                    #region Amount

                    writer.WriteStartElement("cbc:Amount");
                    writer.WriteAttributeString("currencyID", invoiceLine.AllowanceCharge.Amount.CurrencyId);
                    writer.WriteValue(invoiceLine.AllowanceCharge.Amount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    #endregion Amount

                    writer.WriteEndElement();
                }

                #endregion AllowanceCharge

                #region TaxTotal

                {
                    foreach (var taxTotal in invoiceLine.TaxTotals)
                    {
                        writer.WriteStartElement("cac:TaxTotal");

                        writer.WriteStartElement("cbc:TaxAmount");
                        writer.WriteAttributeString("currencyID", taxTotal.TaxAmount.CurrencyId);
                        writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteEndElement();

                        #region TaxSubtotal

                        writer.WriteStartElement("cac:TaxSubtotal");

                        #region TaxableAmount

                        if (!string.IsNullOrEmpty(taxTotal.TaxableAmount.CurrencyId))
                        {
                            writer.WriteStartElement("cbc:TaxableAmount");
                            writer.WriteAttributeString("currencyID", taxTotal.TaxableAmount.CurrencyId);
                            writer.WriteString(taxTotal.TaxableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            writer.WriteEndElement();
                        }

                        #endregion TaxableAmount

                        writer.WriteStartElement("cbc:TaxAmount");
                        writer.WriteAttributeString("currencyID", taxTotal.TaxSubtotal.TaxAmount.CurrencyId);
                        writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteEndElement();
                        if (taxTotal.TaxSubtotal.Percent > 0)
                            writer.WriteElementString("cbc:Percent", taxTotal.TaxSubtotal.Percent.ToString(Formatos.FormatoNumerico, Formato));

                        #region TaxCategory

                        writer.WriteStartElement("cac:TaxCategory");
                        //writer.WriteElementString("cbc:ID", invoiceLine.TaxTotal.TaxSubtotal.TaxCategory.ID);

                        #region ID

                        writer.WriteStartElement("cbc:ID");
                        writer.WriteAttributeString("schemeID", "UN/ECE 5305");
                        writer.WriteAttributeString("schemeName", "Codigo de tributos");
                        writer.WriteAttributeString("schemeAgencyName", "PE:SUNAT");

                        writer.WriteValue(taxTotal.TaxSubtotal.TaxCategory.Identifier); //VALOR OBTENIDO DE LA TABLA 5
                        writer.WriteEndElement();

                        #endregion ID

                        writer.WriteElementString("cbc:Percent", ext2.AdditionalMonetaryTotals[4].Percent.ToString(Formatos.FormatoNumerico, Formato));
                        //writer.WriteElementString("cbc:TaxExemptionReasonCode", taxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode);
                        writer.WriteStartElement("cbc:TaxExemptionReasonCode");
                        writer.WriteAttributeString("listAgencyName", "PE:SUNAT");
                        //writer.WriteAttributeString("listName", "SUNAT:Codigo de Tipo de Afectación del IGV");
                        writer.WriteAttributeString("listName", "Afectacion del IGV"); //CDR CORREGIDO
                        writer.WriteAttributeString("listURI", "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07");
                        writer.WriteValue(taxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode);
                        writer.WriteEndElement();

                        if (!string.IsNullOrEmpty(taxTotal.TaxSubtotal.TaxCategory.TierRange))
                            writer.WriteElementString("cbc:TierRange", taxTotal.TaxSubtotal.TaxCategory.TierRange);

                        #region TaxScheme

                        {
                            writer.WriteStartElement("cac:TaxScheme");

                            // writer.WriteElementString("cbc:ID", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.Id);

                            #region ID

                            writer.WriteStartElement("cbc:ID");
                            writer.WriteAttributeString("schemeID", "UN/ECE 5153");
                            //writer.WriteAttributeString("schemeName", "Tax Scheme Identifier"); Codigo de tributos
                            writer.WriteAttributeString("schemeName", "Codigo de tributos");
                            //writer.WriteAttributeString("schemeAgencyName", "United Nations Economic Commission for Europe"); PE:SUNAT
                            writer.WriteAttributeString("schemeAgencyName", "PE:SUNAT");

                            writer.WriteValue(taxTotal.TaxSubtotal.TaxCategory.TaxScheme.Id); //VALOR OBTENIDO DE LA TABLA 5
                            writer.WriteEndElement();

                            #endregion ID

                            writer.WriteElementString("cbc:Name", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.Name);
                            writer.WriteElementString("cbc:TaxTypeCode", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode);

                            writer.WriteEndElement();
                        }

                        #endregion TaxScheme

                        writer.WriteEndElement();

                        #endregion TaxCategory

                        writer.WriteEndElement();

                        #endregion TaxSubtotal

                        writer.WriteEndElement();
                    }
                }

                #endregion TaxTotal

                #region Item

                writer.WriteStartElement("cac:Item");

                #region Description

                writer.WriteElementString("cbc:Description", invoiceLine.Item.Description);
                //writer.WriteStartElement("cbc:Description");
                //writer.WriteCData(invoiceLine.Item.Description);
                //writer.WriteEndElement();

                #endregion Description

                #region SellersItemIdentification

                writer.WriteStartElement("cac:SellersItemIdentification");
                writer.WriteElementString("cbc:ID", invoiceLine.Item.SellersItemIdentification.Id);
                writer.WriteEndElement();

                #endregion SellersItemIdentification

                #region CommodityClassification

                if (invoiceLine.ItemClassificationCode != null)
                {
                    writer.WriteStartElement("cac:CommodityClassification");
                    writer.WriteStartElement("cbc:ItemClassificationCode");
                    writer.WriteAttributeString("listID", "UNSPSC");
                    writer.WriteAttributeString("listAgencyName", "GS1 US");
                    writer.WriteAttributeString("listName", "Item Classification");
                    writer.WriteValue(invoiceLine.ItemClassificationCode);//82141601-SERVICIOS FOTOGRAFICOS, MONTAJE Y ENMARCADO	82141602 - MONTAJE DE EXPOSICION DE ARTICULOS
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                #endregion CommodityClassification

                writer.WriteEndElement();

                #endregion Item

                #region Price

                writer.WriteStartElement("cac:Price");

                writer.WriteStartElement("cbc:PriceAmount");
                writer.WriteAttributeString("currencyID", invoiceLine.Price.PriceAmount.CurrencyId);
                writer.WriteString(invoiceLine.Price.PriceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                writer.WriteEndElement();

                #endregion Price

                writer.WriteEndElement();
            }

            #endregion InvoiceLines
        }
    }
}