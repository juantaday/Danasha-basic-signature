using ec.gob.sri.comprobantes.Enum;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Processes;
using FirmaXadesNet.Signature;
using FirmaXadesNet.Signature.Parameters;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

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
                throw new Exception("No se encontró el contenido del archivo XML ni se proporcionó texto XML para firmar en el método: SignatureDocument.");

            if (tokens is null)
                throw new Exception("El parámetro 'tokens' no puede ser nulo. Proporcione un objeto TokensValidos para continuar.");

            // Inicializa la clase que firma
            SignDocument sig = new SignDocument();
            // Parámetros para firmar
            sig.parametros = new SignatureParameters();
            sig.parametros.SigningDate = DateTime.Now;

            var sc = new SignatureCommitment(SignatureCommitmentType.ProofOfOrigin);
            sig.parametros.SignatureCommitments.Add(sc);

            X509Certificate2 cert = ObtenerCertificadoValido(tokens.THUMBPRINT);

            // Asigno el certificado para firmar
            sig.parametros.Signer = new Signer(cert);

            // Ejecución del proceso de firmado
            SignatureDocument doc = sig.Execute(xmldocument, SignaturePackaging.ENVELOPED);
            sig.parametros.Signer?.Dispose();

            // Si se solicita guardar el documento firmado
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

        private static X509Certificate2 ObtenerCertificadoValido(string thumbprintBuscado)
        {
            X509Certificate2 cert = null;

            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                try
                {
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                    // No filtrar por validez de fecha; trabajar con todos los certificados
                    foreach (var certificado in store.Certificates)
                    {
                        Console.WriteLine($"Subject: {certificado.Subject}");
                        Console.WriteLine($"Issuer: {certificado.Issuer}");
                        Console.WriteLine($"Thumbprint: {certificado.Thumbprint}");
                        Console.WriteLine($"Serial Number: {certificado.SerialNumber}");
                        Console.WriteLine($"NotBefore: {certificado.NotBefore}");
                        Console.WriteLine($"NotAfter: {certificado.NotAfter}");
                        Console.WriteLine("----------------------------");

                        // Verificar si el certificado coincide con el thumbprint buscado
                        if (certificado.Thumbprint.Equals(thumbprintBuscado, StringComparison.OrdinalIgnoreCase))
                        {
                            // Validar fechas manualmente
                            if (DateTime.Now < certificado.NotBefore)
                            {
                                throw new Exception($"El certificado con thumbprint '{thumbprintBuscado}' no es válido aún. Válido a partir de {certificado.NotBefore}.");
                            }

                            if (DateTime.Now > certificado.NotAfter)
                            {
                                throw new Exception($"El certificado con thumbprint '{thumbprintBuscado}' está caducado. Válido hasta {certificado.NotAfter}.");
                            }

                            cert = certificado;
                            break;
                        }
                    }
                }
                catch (Exception ex) when (ex.Message.Contains("no es válido aún") || ex.Message.Contains("está caducado"))
                {
                    throw new Exception("Certificdo invalido, fuera de la fecha de validez");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al acceder al almacén de certificados: {ex.Message}");
                    throw new Exception("Ocurrió un error al intentar acceder al almacén de certificados. Ver detalles en la excepción interna.", ex);
                }
            }

            if (cert == null)
            {
                throw new Exception($"No se encontró un certificado con el thumbprint especificado: '{thumbprintBuscado}'.");
            }

            return cert;
        }
    }
}
