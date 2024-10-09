using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3_OOAP_
{
    public partial class Form1 : Form
    {
        private IPhoneFactory _phoneFactory;
        private ComboBox comboBoxCountry;
        private ComboBox comboBoxModel;
        private Label labelInfo;

        public Form1()
        {
            InitializeComponent();

            // Ініціалізація ComboBox для вибору країни
            comboBoxCountry = new ComboBox { Location = new Point(10, 10), Size = new Size(200, 30) };
            comboBoxCountry.Items.Add("USA");
            comboBoxCountry.Items.Add("China");
            comboBoxCountry.SelectedIndex = 0; // За замовчуванням вибрано USA
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
            Controls.Add(comboBoxCountry);

            // Ініціалізація ComboBox для вибору моделі
            comboBoxModel = new ComboBox { Location = new Point(10, 50), Size = new Size(200, 30) };
            comboBoxModel.SelectedIndexChanged += ComboBoxModel_SelectedIndexChanged;
            Controls.Add(comboBoxModel);

            // Ініціалізація Label для відображення інформації про покупку
            labelInfo = new Label { Location = new Point(220, 10), Size = new Size(310, 100), BorderStyle = BorderStyle.FixedSingle, TextAlign = ContentAlignment.MiddleLeft };
            Controls.Add(labelInfo);

            // Створення кнопки для оформлення замовлення
            Button buttonOrder = new Button { Text = "Order", Location = new Point(540, 10), Size = new Size(100, 30) };
            buttonOrder.Click += ButtonOrder_Click;
            Controls.Add(buttonOrder);

            // Оновлюємо моделі для вибору
            UpdateModelOptions();
            UpdateInfo();
        }

        private void ComboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Оновлюємо моделі після вибору країни
            UpdateModelOptions();
            UpdateInfo();
        }

        private void ComboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Оновлюємо інформацію про вибрану модель
            UpdateInfo();
        }

        private void UpdateModelOptions()
        {
            comboBoxModel.Items.Clear();

            // Оновлюємо список моделей відповідно до вибраної країни
            if (comboBoxCountry.SelectedItem.ToString() == "USA")
            {
                comboBoxModel.Items.AddRange(new[] { "Model A", "Model B", "Model C", "Model D", "Model E" });
                _phoneFactory = new USPhoneFactory();
            }
            else if (comboBoxCountry.SelectedItem.ToString() == "China")
            {
                comboBoxModel.Items.AddRange(new[] { "Model A", "Model B", "Model C", "Model D", "Model E" });
                _phoneFactory = new ChinaPhoneFactory();
            }
            comboBoxModel.SelectedIndex = 0; // Вибір за замовчуванням
        }

        private void UpdateInfo()
        {
            if (_phoneFactory == null || comboBoxModel.SelectedItem == null)
            {
                labelInfo.Text = "Select a country and model to see the details.";
                return;
            }

            try
            {
                // Створення телефону на основі вибраної моделі
                IPhone phone = _phoneFactory.CreatePhone(comboBoxModel.SelectedItem.ToString());
                string country = comboBoxCountry.SelectedItem.ToString();
                string info = $"Country: {country}\nModel: {phone.GetModel()}\nPrice: {phone.GetPrice()} USD\nDelivery Time: {phone.GetDeliveryTime().Days} days";
                labelInfo.Text = info; // Відображення інформації про покупку у Label
            }
            catch (Exception ex)
            {
                labelInfo.Text = $"Error: {ex.Message}";
            }
        }

        private void ButtonOrder_Click(object sender, EventArgs e)
        {
            UpdateInfo();
        }
    }

    // Інтерфейс телефону
    public interface IPhone
    {
        string GetModel();
        decimal GetPrice();
        TimeSpan GetDeliveryTime();
    }

    // Американські моделі телефонів
    public class USPhoneA : IPhone
    {
        public string GetModel() => "Model A";
        public decimal GetPrice() => 1000m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(7);
    }

    public class USPhoneB : IPhone
    {
        public string GetModel() => "Model B";
        public decimal GetPrice() => 1100m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(7);
    }

    public class USPhoneC : IPhone
    {
        public string GetModel() => "Model C";
        public decimal GetPrice() => 1200m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(7);
    }

    public class USPhoneD : IPhone
    {
        public string GetModel() => "Model D";
        public decimal GetPrice() => 1300m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(7);
    }

    public class USPhoneE : IPhone
    {
        public string GetModel() => "Model E";
        public decimal GetPrice() => 1400m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(7);
    }

    // Китайські моделі телефонів
    public class ChinaPhoneA : IPhone
    {
        public string GetModel() => "Model A";
        public decimal GetPrice() => 700m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(10);
    }

    public class ChinaPhoneB : IPhone
    {
        public string GetModel() => "Model B";
        public decimal GetPrice() => 750m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(10);
    }

    public class ChinaPhoneC : IPhone
    {
        public string GetModel() => "Model C";
        public decimal GetPrice() => 800m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(10);
    }

    public class ChinaPhoneD : IPhone
    {
        public string GetModel() => "Model D";
        public decimal GetPrice() => 850m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(10);
    }

    public class ChinaPhoneE : IPhone
    {
        public string GetModel() => "Model E";
        public decimal GetPrice() => 900m;
        public TimeSpan GetDeliveryTime() => TimeSpan.FromDays(10);
    }

    // Інтерфейс фабрики телефонів
    public interface IPhoneFactory
    {
        IPhone CreatePhone(string model);
    }

    // Фабрика для американських телефонів
    public class USPhoneFactory : IPhoneFactory
    {
        public IPhone CreatePhone(string model)
        {
            switch (model)
            {
                case "Model A": return new USPhoneA();
                case "Model B": return new USPhoneB();
                case "Model C": return new USPhoneC();
                case "Model D": return new USPhoneD();
                case "Model E": return new USPhoneE();
                default: throw new ArgumentException("Unknown model");
            }
        }
    }

    // Фабрика для китайських телефонів
    public class ChinaPhoneFactory : IPhoneFactory
    {
        public IPhone CreatePhone(string model)
        {
            switch (model)
            {
                case "Model A": return new ChinaPhoneA();
                case "Model B": return new ChinaPhoneB();
                case "Model C": return new ChinaPhoneC();
                case "Model D": return new ChinaPhoneD();
                case "Model E": return new ChinaPhoneE();
                default: throw new ArgumentException("Unknown model");
            }
        }
    }
}