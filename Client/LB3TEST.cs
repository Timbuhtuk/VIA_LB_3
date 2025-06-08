using Entities;

namespace Client
{
    public partial class LB3TEST : Form
    {
        public DataType currentType = DataType.Json;
        public dynamic currentEvent = new BasicEvent<int>();

        public LB3TEST()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.Add(typeof(int));
            comboBox1.Items.Add(typeof(string));
            comboBox1.Items.Add(typeof(bool));
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Type eventType = currentEvent.GetType();
            Type genericType = eventType.GetGenericArguments()[0];
            try
            {
                object? value = Convert.ChangeType(textBox2.Text, genericType);
                currentEvent.Append((dynamic)value);
                currentEvent.TimeStamp = DateTime.Now;
                switch (currentType)
                {
                    case DataType.Json:
                        textBox1.Text = new JsonEventSerializer().Serialize(currentEvent);
                        break;
                    case DataType.Xml:
                        textBox1.Text = new XmlEventSerializer().Serialize(currentEvent);
                        break;
                    case DataType.Csv:
                        textBox1.Text = new CsvEventSerializer().Serialize(currentEvent);
                        break;
                }
                //radioButton1.Enabled = false;
                //radioButton2.Enabled = false;
                //radioButton3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string result;

            switch (currentType)
            {
                case DataType.Json:
                    result = new JsonEventSerializer().Serialize(currentEvent);
                    break;
                case DataType.Xml:
                    result = new XmlEventSerializer().Serialize(currentEvent);
                    break;
                case DataType.Csv:
                    result = new CsvEventSerializer().Serialize(currentEvent);
                    break;
                default:
                    result = string.Empty;
                    break;
            }

            var server = new UDPAsyncClient();
            server.Boot();

            Type eventType = currentEvent.GetType();
            Type genericType = eventType.GetGenericArguments()[0];

            var request = new Request
            {
                dateTime = DateTime.Now,
                text = result,
                convertingType = (int)currentType,
                eventType = genericType.AssemblyQualifiedName! // ВАЖНО: строка, не typeof(int)
            };

            server.Send(request);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            currentType = DataType.Json;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            currentType = DataType.Xml;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            currentType = DataType.Csv;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var type = (Type)comboBox1.SelectedItem!;

            Type genericEventType = typeof(BasicEvent<>).MakeGenericType(type);
            object? newEvent = Activator.CreateInstance(genericEventType);

            currentEvent = (dynamic)newEvent;
        }
    }
}
