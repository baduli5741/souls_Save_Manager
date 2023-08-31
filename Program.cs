using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace souls_Save_Manager
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string exeFilePath = Application.ExecutablePath;
            using (RSACryptoServiceProvider privateKey = LoadPrivateKey()) // Load your private key
            {
                ApplyDigitalSignature(exeFilePath, privateKey);
            }
            Application.Run(new Form1());
        }
        private static RSACryptoServiceProvider LoadPrivateKey()
        {
            // Load and return your private key
            // Implement the logic to load the private key here
            string pfxFilePath = @"C:\Windows\System32\certificate.pfx"; // 경로 수정
            string password = ""; // Password to access the private key in the PFX file

            try
            {
                X509Certificate2 certificate = new X509Certificate2(pfxFilePath, password, X509KeyStorageFlags.Exportable);
                if (certificate.HasPrivateKey)
                {
                    RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)certificate.PrivateKey;
                    return privateKey;
                }
                else
                {
                    throw new InvalidOperationException("Private key not found in the certificate.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading private key: " + ex.Message);
                return null;
            }
        }

        private static void ApplyDigitalSignature(string filePath, RSACryptoServiceProvider privateKey)
        {
            try
            {
                if (privateKey == null)
                {
                    throw new ArgumentNullException("privateKey", "Private key is null.");
                }

                byte[] data = File.ReadAllBytes(filePath); // 파일 내용 읽기

                // 디지털 서명 생성
                byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                // 서명 파일 생성 (원본 파일명 끝에 ".signature" 추가)
                string signatureFilePath = Path.ChangeExtension(filePath, ".signature");
                File.WriteAllBytes(signatureFilePath, signature);
                Console.WriteLine("Digital signature applied successfully.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Error applying digital signature: " + ex.Message);
                // privateKey가 null인 경우의 예외 처리 코드
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error applying digital signature: " + ex.Message);
                // 그 외 예외 처리 코드
            }
        }

    }
}
