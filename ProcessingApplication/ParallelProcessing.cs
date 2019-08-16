using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using TXTextControl.Parallel.DataContainer;

namespace TXTextControl.Parallel
{
    public class ParallelProcessing
    {
        public static ReturningObject CallProcessingApp(DataContainer.PassingObject data)
        {
            string sProcessingAppLocation = Assembly.GetExecutingAssembly().Location;

            var lsResult = new List<string>();

            // Create separate process
            var newProcess = new Process
            {
                StartInfo =
                {
                    FileName = sProcessingAppLocation,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                }
            };

            // Create 2 anonymous pipes (read and write) for duplex communications
            // (each pipe is one-way)
            using (var pipeRead = new AnonymousPipeServerStream(PipeDirection.In,
                HandleInheritability.Inheritable))
            using (var pipeWrite = new AnonymousPipeServerStream(PipeDirection.Out,
                HandleInheritability.Inheritable))
            {
                // Pass to the other process handles to the 2 pipes
                newProcess.StartInfo.Arguments = pipeRead.GetClientHandleAsString() + " " +
                    pipeWrite.GetClientHandleAsString();

                try
                {
                    newProcess.Start();
                }
                catch { }

                pipeRead.DisposeLocalCopyOfClientHandle();
                pipeWrite.DisposeLocalCopyOfClientHandle();

                try
                {
                    using (var sw = new StreamWriter(pipeWrite))
                    {
                        // Send a 'sync message' and wait for the other process to receive it
                        sw.WriteLine("SYNC");
                        pipeWrite.WaitForPipeDrain();

                        sw.WriteLine(JsonConvert.SerializeObject(data));
                        sw.WriteLine("END");
                    }

                    // Get message from the other process
                    using (var sr = new StreamReader(pipeRead))
                    {
                        string sTempResult;

                        // Wait for 'sync message' from the other process
                        do
                        {
                            sTempResult = sr.ReadLine();
                        } while (sTempResult == null || !sTempResult.StartsWith("SYNC"));

                        // Read until 'end message' from the other process
                        while ((sTempResult = sr.ReadLine()) != null && !sTempResult.StartsWith("END"))
                        {
                            lsResult.Add(sTempResult);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    newProcess.WaitForExit();
                    newProcess.Close();
                }
            }

            ReturningObject dataReturnObject;

            if (lsResult.Count == 0)
            {
                dataReturnObject = new ReturningObject() { Error = "Error" };
            }
            else if (lsResult[0] != null)
                dataReturnObject = JsonConvert.DeserializeObject<ReturningObject>(lsResult[0]);
            else
                dataReturnObject = new ReturningObject() { Error = "Error" };

            return dataReturnObject;
        }
    }
}
