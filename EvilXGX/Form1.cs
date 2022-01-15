namespace EvilXGX
{
    using System.Text;
    public partial class Form1 : Form
    {
        public static byte[] encodeBytes(byte[] bytes, String pass)
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TargetFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Reading Data ....", "READING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                byte[] plainB = File.ReadAllBytes(TargetFile.FileName);
                MessageBox.Show("Encoding Data ....", "Encode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                byte[] encodedB = encodeBytes(plainB, textBox1.Text);
                MessageBox.Show("Saving Data ....", "Saving process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string temp = Encoding.UTF8.GetString(encodedB);
                MessageBox.Show(System.IO.Path.GetFileName(TargetFile.FileName));
                temp = string.Concat(temp,@"\\Iran//",System.IO.Path.GetFileName(TargetFile.FileName));
                File.WriteAllText(System.IO.Path.GetRandomFileName() + System.IO.Path.GetExtension(TargetFile.FileName),temp);
            }
        }
    }
}