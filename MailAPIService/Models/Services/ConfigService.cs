using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace MailerAPIService.Models.Services
{

    public static class ConfigService
    {
        /// <summary>
        /// Получение информации об авторизационной информации почтового сервера из файла
        /// </summary>
        /// <param name="configPath">Путь до файла конфига </param>
        /// <returns>Структурированный класс, содержащий авторизационную информацию</returns>
        public static MailServerInfo GetServerAuthFromConfig(string configPath)
        {
            MailServerInfo serverAuth;
            string[] lines = File.ReadAllLines(configPath);
            serverAuth = new()
            {
                ServerAddress = lines[0].Trim(),
                AuthPassword = lines[1].Trim(),
                SMTPHost = lines[2].Trim(),
                SMTPPort = Convert.ToInt32(lines[3].Trim()),
                DisplayName = lines[4].Trim(), 
            };
            return serverAuth;            
        }
    }
}
