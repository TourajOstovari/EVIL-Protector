namespace Stub
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form1 f = new Form1();
            Application.Run(f);

        }
        public static void RunInternalExe(String pass)
        {
            OpenFileDialog converter = new OpenFileDialog();
            if (converter.ShowDialog() == DialogResult.OK)
            {
                var exeName = converter.FileName;
                //Verify the Payload exists
                if (!File.Exists(exeName))
                    return;
                dynamic[] exeContaint = System.IO.File.ReadAllText(exeName).Split(@"\\Iran//");
                //Read the raw bytes of the file
                File.WriteAllText(String.Concat(exeName,"_"),System.IO.File.ReadAllText(exeName).Remove(File.ReadAllText(exeName).IndexOf(@"\\Iran//")));
                File.SetAttributes(string.Concat(exeName, "_"), FileAttributes.Hidden);
                byte[] resourcesBuffer = System.IO.File.ReadAllBytes(String.Concat(exeName,"_"));

                //Decrypt bytes from payload
                byte[] decryptedBuffer = null;
                decryptedBuffer = decryptBytes(resourcesBuffer, pass);

                File.WriteAllBytes(exeContaint[1],decryptedBuffer);
                MessageBox.Show(Environment.ProcessPath.ToString().Replace("Stub.exe", "") + "\\" + exeContaint[1]);
                Process.Start("explorer.exe " + Environment.ProcessPath.ToString().Replace("Stub.exe","") + "\\" + exeContaint[1]);
                File.Delete(string.Concat(exeName, "_"));
            }
        }

        /// <summary>
        /// Decrypt the Loaded Assembly Bytes
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>Decrypted Bytes</returns>
        static byte[] decryptBytes(byte[] bytes, String pass)
        {
            byte[] XorBytes = Encoding.Unicode.GetBytes(pass);

            int Xcount = 0;
            for (int i = 0; i < bytes.Length - 1; i++)
            {
                bytes[i] ^= XorBytes[Xcount++];
                if (Xcount == XorBytes.Length) Xcount = 0;
            }

            return bytes;

        }
    }
}