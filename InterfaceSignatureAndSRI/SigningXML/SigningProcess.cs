using ec.gob.sri.comprobantes.Enum;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Processes;
using FirmaXadesNet.Signature;
using FirmaXadesNet.Signature.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.SigningXML
{
    public class SigningProcess
    {
        public static string GeneratedSignigWithPlaneText(string planeText,
           TokensValidos tokens = null, string outPhatFolder = null, bool SaveFile = false)
        {
            SignatureDocument doc = Signig(xmlTextPlane: planeText,
                        filePath: outPhatFolder, SaveFile: SaveFile, tokens: tokens);

            if (doc != null)
            {
                return doc.Document.InnerXml;
            }
            else
            {
                return string.Empty;
            }

        }

        private static SignatureDocument Signig(FileInfo file = null, string xmlTextPlane = null,
            string filePath = null, bool SaveFile = false, TokensValidos tokens = null)
        {
            string xmldocument = xmlTextPlane;
            if (file != null)
                xmldocument = File.ReadAllText(file.FullName);

            if (string.IsNullOrEmpty(xmldocument))
                throw new Exception("Not fout xmlTextPlane or  file to sining in  InterfaceSignatureAndSRI.SigningXML method:SignatureDocument");



            //inicializa la clase que firma....
            SignDocument sig = new SignDocument();
            //parametros para firmar
            sig.parametros = new SignatureParameters();

            sig.parametros.SigningDate = DateTime.Now;

            var sc = new SignatureCommitment(SignatureCommitmentType.ProofOfOrigin);
            sig.parametros.SignatureCommitments.Add(sc);
            X509Certificate2 cert = null;

            //Selecionamo sel sertificado
            #region Selecion_Certificado_Valido
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            store.Close();
            //seleccion solo los token validos segun  fecha de expiration
            X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);


            if (tokens == null)
            {
                string title = "Listado de firmas Validas...";
                string message = "Seleccione una firma válida..";
                X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(fcollection, title, message, X509SelectionFlag.MultiSelection);
                if (scollection != null && scollection.Count == 1)
                {
                    cert = scollection[0];
                }
            }
            else
            {
            

                foreach (var certificado in fcollection)
                {
                    X500NameGeneral x500emisor = new X500NameGeneral(certificado.Issuer);
                    X500NameGeneral x500sujeto = new X500NameGeneral(certificado.Subject);

                    if ((tokens.Equals(TokensValidos.SD_BIOPASS) || tokens.Equals(TokensValidos.SD_EPASS3000)) &&
                        (x500emisor.CN.Contains(AutoridadesCertificantes.SECURITY_DATA.Cn) ||
                        x500emisor.CN.Contains(AutoridadesCertificantes.SECURITY_DATA_SUB_1.Cn) ||
                        x500emisor.CN.Contains(AutoridadesCertificantes.SECURITY_DATA_SUB_2.Cn)))
                    {
                        if (AutoridadesCertificantes.SECURITY_DATA.O.Equals(x500emisor.O) &&
                            AutoridadesCertificantes.SECURITY_DATA.C.Contains(x500emisor.C) &&
                            AutoridadesCertificantes.SECURITY_DATA.O.Equals(x500sujeto.O) &&
                            AutoridadesCertificantes.SECURITY_DATA.C.Contains(x500sujeto.C))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }

                        if (AutoridadesCertificantes.SECURITY_DATA_SUB_1.O.Equals(x500emisor.O) &&
                            AutoridadesCertificantes.SECURITY_DATA_SUB_1.C.Contains(x500emisor.C) &&
                            AutoridadesCertificantes.SECURITY_DATA_SUB_1.O.Equals(x500sujeto.O) &&
                            AutoridadesCertificantes.SECURITY_DATA_SUB_1.C.Contains(x500sujeto.C))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }

                        if (AutoridadesCertificantes.SECURITY_DATA_SUB_2.O.Equals(x500emisor.O) &&
                            AutoridadesCertificantes.SECURITY_DATA_SUB_2.C.Contains(x500emisor.C) &&
                            AutoridadesCertificantes.SECURITY_DATA_SUB_2.O.Equals(x500sujeto.O) &&
                            AutoridadesCertificantes.SECURITY_DATA_SUB_2.C.Contains(x500sujeto.C))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }

                    }

                    else if ((tokens.Equals(TokensValidos.BCE_ALADDIN)) ||
                        ((tokens.Equals(TokensValidos.BCE_IKEY2032)) &&
                        (x500emisor.CN.Contains(AutoridadesCertificantes.BANCO_CENTRAL.Cn))))
                    {
                        if ((x500emisor.O.Contains(AutoridadesCertificantes.BANCO_CENTRAL.O)) &&
                            (AutoridadesCertificantes.BANCO_CENTRAL.C.Equals(x500emisor.C)) &&
                            (x500sujeto.O.Contains(AutoridadesCertificantes.BANCO_CENTRAL.O)) &&
                            (AutoridadesCertificantes.BANCO_CENTRAL.C.Equals(x500sujeto.C)))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }
                    }
                    else if ((tokens.Equals(TokensValidos.BCE_ALADDIN)) ||
                        ((tokens.Equals(TokensValidos.BCE_IKEY2032)) &&
                        (x500emisor.CN.Contains(AutoridadesCertificantes.BANCO_CENTRAL.Cn))))
                    {
                        if ((x500emisor.O.Contains(AutoridadesCertificantes.BANCO_CENTRAL.O)) &&
                            (AutoridadesCertificantes.BANCO_CENTRAL.C.Contains(x500emisor.C)) &&
                            (x500sujeto.O.Contains(AutoridadesCertificantes.BANCO_CENTRAL.O)) &&
                            (AutoridadesCertificantes.BANCO_CENTRAL.C.Equals(x500sujeto.C)))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }
                    }
                    else if ((tokens.Equals(TokensValidos.ANF1)) &&
                        (x500emisor.CN.Contains(AutoridadesCertificantes.ANF.Cn)))
                    {
                        if ((AutoridadesCertificantes.ANF.O.Equals(x500emisor.O)) &&
                            (AutoridadesCertificantes.ANF.C.Equals(x500emisor.C)) &&
                            (AutoridadesCertificantes.ANF.C.ToLower().Equals(x500sujeto.C)))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }

                    }
                    else if ((tokens.Equals(TokensValidos.ANF1)) &&
                        (x500emisor.CN.Contains(AutoridadesCertificantes.ANF_ECUADOR_CA1.Cn)))
                    {
                        if ((AutoridadesCertificantes.ANF_ECUADOR_CA1.O.Equals(x500emisor.O)) &&
                            (AutoridadesCertificantes.ANF_ECUADOR_CA1.C.Equals(x500emisor.C)) &&
                            (AutoridadesCertificantes.ANF_ECUADOR_CA1.C.Equals(x500sujeto.C)))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }
                        }
                    }

                    else if ((tokens.Equals(TokensValidos.KEY4_CONSEJO_JUDICATURA)) &&
                        (x500emisor.CN.Contains(AutoridadesCertificantes.CONSEJO_JUDICATURA.Cn)))
                    {
                        if ((x500emisor.O.Contains(AutoridadesCertificantes.CONSEJO_JUDICATURA.O)) &&
                            (AutoridadesCertificantes.CONSEJO_JUDICATURA.C.Equals(x500emisor.C)) &&
                            (AutoridadesCertificantes.CONSEJO_JUDICATURA.C.Equals(x500sujeto.C)))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }

                        }
                    }

                    else if ((tokens.Equals(TokensValidos.TOKENME_UANATACA)) &&
                       (x500emisor.CN.Contains(AutoridadesCertificantes.UANATACA.Cn)))
                    {
                        if (x500emisor.O.Contains(AutoridadesCertificantes.UANATACA.O) &&
                            AutoridadesCertificantes.UANATACA.C.Equals(x500emisor.C))
                        {
                            if (certificado.HasPrivateKey)
                            {
                                cert = certificado;
                                break;
                            }

                        }
                    }


                }

            }

            #endregion

            // control de certificadoss
            if (cert == null && tokens != null)
            {
                string sql = "No se encontro un sertificado valido para firmar";
                sql = sql + "\n Certificado buscado: " + tokens.Id;
                throw new Exception(sql, new Exception { Source = "User_Index" });
            }
            else if (cert == null)
            {
                return null;
            }

            //asigno el sertificado para firmar..
            sig.parametros.Signer = new Signer(cert);
            //ejecucion de proceso de firmado...
            SignatureDocument doc = sig.Execute(xmldocument, SignaturePackaging.ENVELOPED);
            sig.parametros.Signer?.Dispose();


            //si pide que guarde el documento firmado?
            if (SaveFile && !string.IsNullOrEmpty(filePath))
            {
                string claveAcceso = doc.Document.GetElementsByTagName("claveAcceso")[0].InnerText;
                if (!string.IsNullOrEmpty(claveAcceso))
                {
                    if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(filePath))
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(filePath);
                    }
                    doc.Save(string.Format("{0}\\{1}.xml", filePath, claveAcceso));
                }
            }

            return doc;
        }

        private static byte[] EncodeExtension(X509Certificate2 certificateAuthority)
        {
              Oid SubjectKeyIdentifierOid = new Oid("2.5.29.14");

        var subjectKeyIdentifier = certificateAuthority.Extensions.Cast<X509Extension>().FirstOrDefault(p => p.Oid?.Value == SubjectKeyIdentifierOid.Value);
            if (subjectKeyIdentifier == null)
                return null;
            var rawData = subjectKeyIdentifier.RawData;
            var segment = new ArraySegment<byte>(rawData, 2, rawData.Length - 2);
            var authorityKeyIdentifier = new byte[segment.Count + 4];
            // KeyID of the AuthorityKeyIdentifier
            authorityKeyIdentifier[0] = 0x30;
            authorityKeyIdentifier[1] = 0x16;
            authorityKeyIdentifier[2] = 0x80;
            authorityKeyIdentifier[3] = 0x14;
            return authorityKeyIdentifier;
        }


    }
}
