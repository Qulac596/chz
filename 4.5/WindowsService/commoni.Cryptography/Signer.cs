using chz.WindowsServices.Cryptography;
using System;
using System.Configuration;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace chz.Cryptography
{
    /*
     * Выполняет цифровую попись данных
     */
    public class Signer : ISigner
    {
        private static string CertificateNotFoudErrorMessage = "Сертификат не найден.";

        private readonly chz.WindowsServices.Setting.ISettingProvider setting;

        public Signer(chz.WindowsServices.Setting.ISettingProvider setting)
        {
            this.setting = setting;
        }
        /*
         * Подписывает строку
         */
        public string Sign(string code)
        {
            try
            {
                //создаем хранилище сертификатов
                X509Store storeCurrentUser = new X509Store(StoreName.My, StoreLocation.CurrentUser);

                //открываем хранилище
                storeCurrentUser.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                //получаем колекцию сертификатов
                var colCurrentUser = storeCurrentUser.Certificates;

                //получаем отпечаток
                var thumbprin = setting.GetValue("UserId");

                //ищем сертификат по отпечатку
                var certificate2CollectionCurrentUser = colCurrentUser.Find(X509FindType.FindByThumbprint, thumbprin, true);

                X509Certificate2 cer;

                if (certificate2CollectionCurrentUser.Count > 0)
                {
                    cer = certificate2CollectionCurrentUser[0];
                }
                else
                {
                    //создаем хранилище сертификатов
                    X509Store storeLocalMachine = new X509Store(StoreName.My, StoreLocation.LocalMachine);

                    //открываем хранилище
                    storeLocalMachine.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                    //получаем колекцию сертификатов 
                    var colLocalMachine = storeLocalMachine.Certificates;

                    //ищем сертификат по отпечатку
                    var certificate2CollectionLocalMachine = colLocalMachine.Find(X509FindType.FindByThumbprint, thumbprin, true);

                    cer = certificate2CollectionLocalMachine[0];
                }

                //преобразуем токен в массиф байт
                var codeBytes = Encoding.UTF8.GetBytes(code);


                //  Создаем объект ContentInfo по сообщению.
                //  Это необходимо для создания объекта SignedCms.
                ContentInfo contentInfo = new ContentInfo(codeBytes);

                // Создаем объект SignedCms 
                SignedCms signedCms = new SignedCms(contentInfo, true);

                // Определяем подписывающего, объектом CmsSigner.
                CmsSigner cmsSigner = new CmsSigner(cer);

                // Подписываем CMS/PKCS #7 сообение.
                signedCms.ComputeSignature(cmsSigner);

                // Кодируем CMS/PKCS #7 сообщение.
                var sign = signedCms.Encode();

                //конвертируем в Base64
                var str = Convert.ToBase64String(sign);

                return str;

            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new CryptographyException(CertificateNotFoudErrorMessage);
            }
            catch (Exception e)
            {
                throw new CryptographyException(e.Message, e);
            }
        }
    }
}
