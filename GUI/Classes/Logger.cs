/*
 * Created by SharpDevelop.
 * User: Raffy
 * Date: 8/12/2016
 * Time: 8:24 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using FAD3.Database.Classes;
using FAD3.Database.Classes.merge;
namespace FAD3
{
    /// <summary>
    /// Description of ErrorLogger.
    /// </summary>
    public class Logger
    {
        private static string _mergeLogPath;
        private static string _mergeErrorLogPath;
        static Logger()
        {
            _mergeLogPath = Application.StartupPath + "\\merge.log";
            _mergeErrorLogPath = Application.StartupPath + "\\merge_error.log";
        }
        public static bool DeleteMergeLog()
        {
            bool success = false;
            if(File.Exists(_mergeLogPath))
            {
                try
                {
                    File.Delete(_mergeLogPath);
                }
                catch(Exception ex)
                {
                    Log(ex);
                }
                success = File.Exists(_mergeLogPath);
            }
            else
            {
                success = true;
            }

            return success;
        }
        public static bool DeleteMergeErrorLog()
        {
            bool success = false;
            if (File.Exists(_mergeErrorLogPath))
            {
                try
                {
                    File.Delete(_mergeErrorLogPath);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
                success = File.Exists(_mergeErrorLogPath);
            }
            else
            {
                success = true;
            }

            return success;
        }
        public static void LogMerge(FADEntities source, FADEntities destination)
        {

            using (StreamWriter writer = new StreamWriter(_mergeLogPath, true))
            {
                writer.WriteLine($"source: {source.MDBPath} destination:{destination.MDBPath} - {DateTime.Now.ToString()}");
            }
        }
        public static void LogMerge(string s, bool isError=false, object obj = null)
        {

            if (isError)
            {
                if (obj != null)
                {
                    string objectDetail = "";
                    string typeName = obj.GetType().Name;
                    switch (typeName)
                    {
                        case "Sampling":
                            objectDetail = $"{((Database.Classes.merge.Sampling)obj).ReferenceNumber.ToString()}";
                            break;
                        case "CatchComposition":
                            var cc = (Database.Classes.merge.CatchComposition)obj;
                            objectDetail = $"{cc.Sampling.ReferenceNumber} - {cc.CatchNameString}";
                            break;
                        case "CatchDetail":
                            var cd = (CatchDetail)obj;
                            objectDetail = $"{cd.CatchComposition.Sampling.ReferenceNumber} - {cd.CatchComposition.CatchNameString}";
                            break;
                        case "LenFreq":
                            var lf = (LenFreq)obj;
                            objectDetail = $"{lf.CatchComposition.Sampling.ReferenceNumber} - {lf.CatchComposition.CatchNameString}";
                            break;
                        case "Species":
                            var sp = (Species)obj;
                            objectDetail = $"ID: {sp.SpeciesID}  Name: {sp.Generic} {sp.Specific} Taxa:{sp.Taxa}";
                            break;
                    }
                    using (StreamWriter writer = new StreamWriter(_mergeErrorLogPath, true))
                    {
                        writer.WriteLine($"Merge error Type:{typeName} Details:{objectDetail} Error:{s} - {DateTime.Now.ToString()}");
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(_mergeLogPath, true))
                    {
                        writer.WriteLine($"Merge error: {s} - {DateTime.Now.ToString()}");
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(_mergeLogPath, true))
                {
                    writer.WriteLine($"{s} - {DateTime.Now.ToString()}");
                }
            }
        }
        public static void Log(Exception ex, Boolean ShowMessage = false)
        {
            string filepath = Application.StartupPath + "\\fad.log";

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine("Message: " + ex.Message + "<br/>" + Environment.NewLine +
                             "StackTrace: " + ex.StackTrace +
                              "" + Environment.NewLine + "Date: " + DateTime.Now.ToString() +
                              System.Environment.NewLine);
            }

            if (Debugger.IsAttached)
            {
                if (global.ShowErrorMessage)
                {
                    StackTrace st = new StackTrace(ex, true);
                    StackFrame frame = st.GetFrame(st.FrameCount - 1);
                    string fileName = frame.GetFileName(); //returns null

                    //Get the method name
                    string methodName = frame.GetMethod().Name; //returns PermissionDemand

                    //Get the line number from the stack frame
                    int line = frame.GetFileLineNumber(); //returns 0

                    //Get the column number
                    int col = frame.GetFileColumnNumber(); //returns 0
                    string errorMessage = "Filename: " + fileName + Environment.NewLine +
                                          "Method: " + methodName + Environment.NewLine +
                                          "Line: " + line + Environment.NewLine;
                    errorMessage = ex.Message + Environment.NewLine + errorMessage;
                    MessageBox.Show(errorMessage, "Error");
                }
                else if (ShowMessage)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static async Task LogAsync(Exception ex)
        {
            string filepath = Application.StartupPath + "\\fad.log";

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                await writer.WriteLineAsync(ex.Message);
            }
            
        }
        public static void LogError(Exception ex)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine($"Error: {ex.Message}\r\n{ex.StackTrace}\r\n Date :{DateTime.Now.ToString()}");
            }
        }

        public static void LogError(string exceptionMessage, string stackTrace)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine($"Error: {exceptionMessage}\r\n{stackTrace}\r\n Date :{DateTime.Now.ToString()}");
            }
        }

        public static void Log(string s)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine($"Message: {s} Date :{DateTime.Now.ToString()}");
            }
        }

        public static void Log(string errorDescription, string className, string methodName)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine($"Error: {errorDescription} at Class: {className} at  Method: {methodName} at Date : { DateTime.Now.ToString()}");
            }
        }

        public static void Log(string errorDescription, string className, string methodName, string methodPart)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine($"Error: {errorDescription} at Class:{className} at  Method:{methodName}  at part:{methodPart} at Date : { DateTime.Now.ToString()}");
            }
        }

        public static void LogSimple(string s)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(s);
            }
        }

        public static void LogSimpleEx(string s)
        {
            string filepath = Application.StartupPath + "\\fad.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.Write(s);
            }
        }
    }
}