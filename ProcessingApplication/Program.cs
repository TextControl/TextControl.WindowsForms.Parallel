using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using TXTextControl.DocumentServer;
using TXTextControl.Parallel.DataContainer;

namespace ProcessingApplication
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 2) return;

            // get read and write pipe handles
            // note: Roles are now reversed from how the other process is passing the handles in
            string pipeWriteHandle = args[0];
            string pipeReadHandle = args[1];

            // create 2 anonymous pipes (read and write) for duplex communications
            // (each pipe is one-way)
            using (var pipeRead = new AnonymousPipeClientStream(PipeDirection.In, pipeReadHandle))
            using (var pipeWrite = new AnonymousPipeClientStream(PipeDirection.Out, pipeWriteHandle))
            {
                try
                {
                    var lsValues = new List<string>();

                    // get message from other process
                    using (var sr = new StreamReader(pipeRead))
                    {
                        string sTempMessage;

                        // wait for "sync message" from the other process
                        do
                        {
                            sTempMessage = sr.ReadLine();
                        } while (sTempMessage == null || !sTempMessage.StartsWith("SYNC"));

                        // read until "end message" from the server
                        while ((sTempMessage = sr.ReadLine()) != null && !sTempMessage.StartsWith("END"))
                        {
                            lsValues.Add(sTempMessage);
                        }
                    }

                    // send value to calling process
                    using (var sw = new StreamWriter(pipeWrite))
                    {
                        sw.AutoFlush = true;
                        // send a "sync message" and wait for the calling process to receive it
                        sw.WriteLine("SYNC");
                        pipeWrite.WaitForPipeDrain(); // wait here

                        PassingObject dataObject = JsonConvert.DeserializeObject<PassingObject>(lsValues[0]);
                        ReturningObject returnObject = new ReturningObject();

                        try
                        {
                            // create a new ServerTextControl for the document processing
                            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
                            {
                                tx.Create();
                                tx.Load(dataObject.Document, TXTextControl.BinaryStreamType.InternalUnicodeFormat);

                                using (MailMerge mailMerge = new MailMerge())
                                {
                                    mailMerge.TextComponent = tx;
                                    mailMerge.MergeJsonData(dataObject.Data.ToString());
                                }

                                byte[] data;
                                tx.Save(out data, TXTextControl.BinaryStreamType.AdobePDF);

                                returnObject.Document = data;
                            }

                            sw.WriteLine(JsonConvert.SerializeObject(returnObject));
                            sw.WriteLine("END");
                        }
                        catch (Exception exc)
                        {
                            returnObject.Error = exc.Message;

                            sw.WriteLine(JsonConvert.SerializeObject(returnObject));
                            sw.WriteLine("END");
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
