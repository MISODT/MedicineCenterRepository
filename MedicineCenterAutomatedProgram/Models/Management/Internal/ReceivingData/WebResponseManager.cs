using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using System;
using System.IO;
using System.Net;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData
{
    public class WebResponseManager
    {
        public static string GetJsonResponseFromRequestQuery(string requestQueryString)
        {
            try
            {
                string jsonValueString = "";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://88.87.92.66/api.php/?dataQuery={requestQueryString}");

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    jsonValueString = reader.ReadToEnd();
                }

                return jsonValueString;
            }

            catch(Exception _)
            {
                InteriorControlsInitializationManager.InitializeErrorAlertWindow($"Произошла ошибка соединения");

                return "";
            }
        }

        public static void ResponseFromRequestQuery(string requestQueryString)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://88.87.92.66/api.php/?dataSimpleQuery={requestQueryString}");

                var _ = (HttpWebResponse)httpWebRequest.GetResponse();
            }

            catch (Exception exception)
            {
                InteriorControlsInitializationManager.InitializeErrorAlertWindow($"Ошибка: {exception.Message}");
            }
        }
    }
}