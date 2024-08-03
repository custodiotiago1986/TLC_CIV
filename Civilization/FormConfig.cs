using System;
using System.Windows.Forms;

namespace CivilizationGame
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            int landPercentage = (int)numericUpDownLandWater.Value;
            string mapSize = comboBoxMapSize.SelectedItem.ToString();

            // Passar para a próxima tela de jogo com as configurações escolhidas
            FormGame formGame = new FormGame(landPercentage, mapSize);
            formGame.Show();
            this.Hide();
        }
    }
}
