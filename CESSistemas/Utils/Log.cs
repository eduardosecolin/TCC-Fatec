﻿using System;
using System.Text;
using System.IO;

namespace Promig.Utils {
    class Log {

        public static StringBuilder log = new StringBuilder();

        public static void logException(Exception e){

            //Configurando diretorio atual para emissão de logs         
            string oldPath = Directory.GetCurrentDirectory();
            string rawPath = $"C:\\ProERP\\ErrorLogs\\";
            if (!Directory.Exists(rawPath)) Directory.CreateDirectory(rawPath);
            Directory.SetCurrentDirectory(rawPath);

            //Emitindo Log
            log.Append(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            log.AppendLine();
            log.Append(e.Message);
            log.AppendLine();
            log.Append(e.StackTrace);
            log.AppendLine();
            if(e.InnerException != null){
                log.AppendLine("InnerException: " + e.InnerException.Message);
            }
            File.AppendAllText("LogError_Promig.txt", log.ToString());
            log.Clear();

            //Restaurando diretorio anterior
            Directory.SetCurrentDirectory(oldPath);
        }

        public static void logMessage(string message){

            //Configurando diretorio atual para emissão de mensagem  
            string oldPath = Directory.GetCurrentDirectory();
            string rawPath = $"C:\\ProERP\\ErrorLogs\\";
            if (!Directory.Exists(rawPath)) Directory.CreateDirectory(rawPath);
            Directory.SetCurrentDirectory(rawPath);

            //Emitindo mensagem
            log.Append(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            log.Append(" " + message);
            log.AppendLine();
            File.AppendAllText("LogMessage_Promig.txt", log.ToString());
            log.Clear();

            //Restaurando diretorio anterior
            Directory.SetCurrentDirectory(oldPath);
        }
    }
}
