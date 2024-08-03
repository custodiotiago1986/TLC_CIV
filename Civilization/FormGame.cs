using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Civilization.Models;

namespace CivilizationGame
{
    public partial class FormGame : Form
    {
        private int landPercentage;
        private string mapSize;
        private int gridSize;
        private int buttonSize = 50;
        private Random random = new Random();
        private List<Unidade> unidades = new List<Unidade>();
        private Button selectedButton;
        private int year = 4000;
        private List<Cidade> cidades = new List<Cidade>();
        private Technology selectedTechnology;
        private Technology currentTechnology;
        private int remainingTurns;
        private List<string> researchedTechnologies = new List<string>();

        private Dictionary<string, bool> techResearchStatus = new Dictionary<string, bool>(); public FormGame(int landPercentage, string mapSize)
        
        {
            InitializeComponent();

            this.landPercentage = landPercentage;
            this.mapSize = mapSize;

            gridSize = mapSize == "36x36" ? 36 : 72;

            InitializeMap();
            InitializeUnits();
            this.KeyPreview = true; // Necessário para capturar eventos de teclado no Form
            this.KeyDown += new KeyEventHandler(FormGame_KeyDown);
            UpdateYearLabel();
        }

        private void InitializeMap()
        {
            List<Button> landButtons = new List<Button>();
            List<Button> waterButtons = new List<Button>();

            int totalCells = gridSize * gridSize;
            int landCount = totalCells * landPercentage / 100;
            int waterCount = totalCells - landCount;

            // Cria botões de terra
            for (int i = 0; i < landCount; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(buttonSize, buttonSize);
                btn.BackColor = Color.Green;
                btn.FlatStyle = FlatStyle.Flat; // Define FlatStyle como Flat
                btn.FlatAppearance.BorderSize = 0; // Define inicialmente sem borda
                btn.Click += Btn_Click; // Associa o evento Click
                btn.MouseDown += Btn_MouseDown; // Associa o evento MouseDown
                landButtons.Add(btn);
            }

            // Cria botões de água
            for (int i = 0; i < waterCount; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(buttonSize, buttonSize);
                btn.BackColor = Color.Blue;
                btn.FlatStyle = FlatStyle.Flat; // Define FlatStyle como Flat
                btn.FlatAppearance.BorderSize = 0; // Define inicialmente sem borda
                btn.MouseDown += Btn_MouseDown; // Associa o evento MouseDown
                waterButtons.Add(btn);
            }

            // Distribui aleatoriamente os botões no mapa
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button btn;
                    if (landButtons.Count > 0 && (waterButtons.Count == 0 || random.Next(0, 100) < landPercentage))
                    {
                        int index = random.Next(0, landButtons.Count);
                        btn = landButtons[index];
                        landButtons.RemoveAt(index);
                    }
                    else
                    {
                        int index = random.Next(0, waterButtons.Count);
                        btn = waterButtons[index];
                        waterButtons.RemoveAt(index);
                    }

                    btn.Location = new Point(col * buttonSize, row * buttonSize);
                    panelMap.Controls.Add(btn);
                }
            }
        }

        private void InitializeUnits()
        {
            foreach (Button btn in panelMap.Controls)
            {
                if (btn.BackColor == Color.Green)
                {
                    Unidade unit = new Unidade(1, 1, 1, 1, btn);
                    unit.Botao.Text = "S";
                    unit.Botao.ForeColor = Color.White;
                    unit.Botao.BackColor = Color.Red; // Define o fundo como vermelho
                    unidades.Add(unit);
                    break; // Só cria uma unidade por botão verde encontrado
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (Unidade unit in unidades)
            {
                if (unit.Botao == btn)
                {
                    if (selectedButton != null)
                    {
                        selectedButton.FlatAppearance.BorderSize = 0;
                    }

                    selectedButton = btn;
                    selectedButton.FlatAppearance.BorderSize = 5;
                    selectedButton.FlatAppearance.BorderColor = Color.White;
                }
            }
        }

        private void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (e.Button == MouseButtons.Right && btn.BackColor == Color.Red)
            {
                foreach (Cidade cidade in cidades)
                {
                    if (cidade.Botao == btn)
                    {
                        AbrirDialogoCidade(cidade);
                        break;
                    }
                }
            }
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedButton != null)
            {
                foreach (Unidade unit in unidades)
                {
                    if (unit.Botao == selectedButton && unit.Movimento > 0)
                    {
                        int index = panelMap.Controls.IndexOf(selectedButton);
                        int col = index % gridSize;
                        int row = index / gridSize;

                        Button newButton = null;
                        switch (e.KeyCode)
                        {
                            case Keys.W:
                                if (row > 0) newButton = (Button)panelMap.Controls[index - gridSize];
                                break;
                            case Keys.S:
                                if (row < gridSize - 1) newButton = (Button)panelMap.Controls[index + gridSize];
                                break;
                            case Keys.A:
                                if (col > 0) newButton = (Button)panelMap.Controls[index - 1];
                                break;
                            case Keys.D:
                                if (col < gridSize - 1) newButton = (Button)panelMap.Controls[index + 1];
                                break;
                            case Keys.B:
                                // Fundar cidade
                                AbrirDialogoFundarCidade(unit);
                                return;
                        }

                        if (newButton != null && newButton.BackColor == Color.Green)
                        {
                            string unitSymbol = selectedButton.Text;
                            selectedButton.Text = "";
                            selectedButton.FlatAppearance.BorderSize = 0;
                            selectedButton.BackColor = Color.Green; // Retorna à cor original
                            newButton.Text = unitSymbol;
                            newButton.ForeColor = Color.White;
                            newButton.BackColor = Color.Red; // Define o fundo como vermelho
                            unit.Botao = newButton;
                            selectedButton = newButton;
                            selectedButton.FlatAppearance.BorderSize = 5;
                            selectedButton.FlatAppearance.BorderColor = Color.White;
                            unit.Movimento = 0;
                            selectedButton = null;
                        }
                    }
                }
            }
        }

        private void AbrirDialogoFundarCidade(Unidade settler)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "New City";
                dialog.Size = new Size(350, 200);
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;

                var label = new Label() { Left = 10, Top = 20, Text = "Enter the name of the new city:", AutoSize = true };
                var textBox = new TextBox() { Left = 10, Top = 50, Width = 310 };
                var okButton = new Button() { Text = "OK", Left = 10, Width = 80, Top = 80, DialogResult = DialogResult.OK };

                okButton.Click += (sender, e) => { dialog.Close(); };

                dialog.Controls.Add(label);
                dialog.Controls.Add(textBox);
                dialog.Controls.Add(okButton);
                dialog.AcceptButton = okButton;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FundarCidade(settler, textBox.Text);
                }
            }
        }

        private void FundarCidade(Unidade settler, string nomeCidade)
        {
            // Remove a unidade Settler da lista
            unidades.Remove(settler);

            // Atualiza o botão para representar uma cidade
            settler.Botao.Text = nomeCidade;
            settler.Botao.Font = new Font(settler.Botao.Font.FontFamily, settler.Botao.Font.Size, FontStyle.Regular);
            settler.Botao.TextAlign = ContentAlignment.MiddleCenter;
            settler.Botao.BackColor = Color.Red;
            settler.Botao.FlatAppearance.BorderSize = 0;
            selectedButton = null;

            // Cria uma nova cidade (pode ser armazenada se necessário)
            Cidade novaCidade = new Cidade(nomeCidade);
            novaCidade.Botao = settler.Botao;
            cidades.Add(novaCidade);
        }

        private void AbrirDialogoCidade(Cidade cidade)
        {
            using (var dialog = new Form())
            {
                dialog.Text = $"City View - {cidade.Nome}";
                dialog.Size = new Size(400, 600);
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;

                var label = new Label()
                {
                    Left = 10,
                    Top = 20,
                    Text = $"Gold: +{cidade.Ouro}\nProduction: +{cidade.Producao}\nFaith: +{cidade.Fe}\nFood: +{cidade.Comida}\nLoyalty: +{cidade.Lealdade}",
                    AutoSize = true
                };

                var unitsList = new ListBox()
                {
                    Left = 10,
                    Top = 150,
                    Width = 150,
                    Height = 100
                };
                unitsList.Items.Add("Settler");
                unitsList.Items.Add("Worker");
                unitsList.Items.Add("Warrior");

                var buildingsList = new ListBox()
                {
                    Left = 200,
                    Top = 150,
                    Width = 150,
                    Height = 100
                };
                buildingsList.Items.Add("Granary");

                var queueList = new ListBox()
                {
                    Left = 10,
                    Top = 270,
                    Width = 340,
                    Height = 200
                };

                foreach (var item in cidade.FilaProducao)
                {
                    queueList.Items.Add($"{item.Nome} - {item.TurnosRestantes} turns");
                }

                var buildButton = new Button()
                {
                    Text = "Build",
                    Left = 10,
                    Top = 480,
                    Width = 80
                };

                buildButton.Click += (sender, e) =>
                {
                    if (unitsList.SelectedItem != null)
                    {
                        string unitName = unitsList.SelectedItem.ToString();
                        int productionCost = GetProductionCost(unitName);
                        int turnsRequired = (int)Math.Ceiling((double)productionCost / cidade.Producao);
                        cidade.FilaProducao.Add(new ItemProducao(unitName, turnsRequired, productionCost, ItemType.Unit));
                    }
                    else if (buildingsList.SelectedItem != null)
                    {
                        string buildingName = buildingsList.SelectedItem.ToString();
                        int productionCost = GetProductionCost(buildingName);
                        int turnsRequired = (int)Math.Ceiling((double)productionCost / cidade.Producao);
                        cidade.FilaProducao.Add(new ItemProducao(buildingName, turnsRequired, productionCost, ItemType.Building));
                    }

                    queueList.Items.Clear();
                    foreach (var item in cidade.FilaProducao)
                    {
                        queueList.Items.Add($"{item.Nome} - {item.TurnosRestantes} turns");
                    }
                };

                var okButton = new Button()
                {
                    Text = "OK",
                    Left = 10,
                    Top = 520,
                    Width = 80,
                    DialogResult = DialogResult.OK
                };

                dialog.Controls.Add(label);
                dialog.Controls.Add(unitsList);
                dialog.Controls.Add(buildingsList);
                dialog.Controls.Add(queueList);
                dialog.Controls.Add(buildButton);
                dialog.Controls.Add(okButton);

                dialog.AcceptButton = okButton;
                dialog.ShowDialog();
            }
        }

        private void AbrirDialogoTecnologia()
        {
            var technologies = new List<Technology>
    {
        new Technology("Agriculture", 5),
        new Technology("Pottery", 5, new List<string> { "Agriculture" }),
        new Technology("Animal Husbandry", 5, new List<string> { "Agriculture" }),
        new Technology("Mining", 5, new List<string> { "Agriculture" }),
        new Technology("Archery", 5, new List<string> { "Agriculture" }),

        new Technology("Writing", 5, new List<string> { "Pottery" }),
        new Technology("Masonry", 5, new List<string> { "Animal Husbandry" }),
        new Technology("Wheel", 5, new List<string> { "Pottery", "Mining" }),
        new Technology("Bronze Working", 5, new List<string> { "Mining" }),
        new Technology("Sailing", 5, new List<string> { "Animal Husbandry" }),

        new Technology("Mathematics", 5, new List<string> { "Writing" }),
        new Technology("Construction", 5, new List<string> { "Masonry" }),
        new Technology("Horseback Riding", 5, new List<string> { "Wheel" }),
        new Technology("Iron Working", 5, new List<string> { "Bronze Working" }),
        new Technology("Calendar", 5, new List<string> { "Sailing" }),

        new Technology("Currency", 5, new List<string> { "Mathematics" }),
        new Technology("Engineering", 5, new List<string> { "Construction" }),
        new Technology("Philosophy", 5, new List<string> { "Mathematics" }),
        new Technology("Trapping", 5, new List<string> { "Horseback Riding" }),
        new Technology("Metal Casting", 5, new List<string> { "Iron Working" }),

        new Technology("Drama and Poetry", 5, new List<string> { "Philosophy" }),
        new Technology("Theology", 5, new List<string> { "Philosophy" }),
        new Technology("Civil Service", 5, new List<string> { "Drama and Poetry" }),
        new Technology("Compass", 5, new List<string> { "Calendar" }),
        new Technology("Education", 5, new List<string> { "Theology" })
    };

            using (var dialog = new Form())
            {
                dialog.Text = "Tech Tree";
                dialog.Size = new Size(1300, 500); // Ajuste do tamanho horizontal
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;

                var labels = new Dictionary<Label, Technology>();
                int xOffset = 10;
                int yOffset = 20;
                int columnWidth = 240; // Ajustado para acomodar mais texto
                int rowHeight = 50;    // Ajustado para padding
                int padding = 5;       // Padding entre os labels

                // Number of columns and rows per column
                int columns = 5;
                int rowsPerColumn = 5;

                for (int col = 0; col < columns; col++)
                {
                    for (int row = 0; row < rowsPerColumn; row++)
                    {
                        int index = col * rowsPerColumn + row;
                        if (index >= technologies.Count)
                        {
                            break;
                        }

                        var tech = technologies[index];
                        var prerequisitesText = tech.Prerequisites.Count > 0 ? string.Join(", ", tech.Prerequisites) : "None";
                        var label = new Label
                        {
                            Text = $"{tech.Name}\nPrerequisites: {prerequisitesText}",
                            AutoSize = true,
                            Location = new Point(xOffset + col * columnWidth, yOffset + row * rowHeight),
                            BorderStyle = BorderStyle.None,
                            Padding = new Padding(padding)
                        };
                        label.Click += (s, e) =>
                        {
                            // Verificar se todos os pré-requisitos foram pesquisados
                            var prerequisitesMet = tech.Prerequisites.All(pr => researchedTechnologies.Contains(pr));
                            if (prerequisitesMet)
                            {
                                foreach (var lbl in labels.Keys)
                                {
                                    lbl.BorderStyle = BorderStyle.None;
                                }
                                label.BorderStyle = BorderStyle.FixedSingle;
                            }
                            else
                            {
                                MessageBox.Show("Prerequisites not researched", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };
                        labels.Add(label, tech);
                        dialog.Controls.Add(label);
                    }
                }

                var okButton = new Button
                {
                    Text = "OK",
                    Left = 10,
                    Top = 430, // Ajuste para melhor ajuste vertical
                    Width = 80,
                    DialogResult = DialogResult.OK
                };

                dialog.Controls.Add(okButton);
                dialog.AcceptButton = okButton;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var selectedLabel = labels.Keys.FirstOrDefault(l => l.BorderStyle == BorderStyle.FixedSingle);
                    if (selectedLabel != null)
                    {
                        var selectedTech = labels[selectedLabel];
                        // Verificar se todos os pré-requisitos foram pesquisados
                        var prerequisitesMet = selectedTech.Prerequisites.All(p => researchedTechnologies.Contains(p));
                        if (prerequisitesMet)
                        {
                            currentTechnology = new Technology(selectedTech.Name, selectedTech.ResearchTime, selectedTech.Prerequisites);
                            remainingTurns = currentTechnology.ResearchTime;
                            lblTechChoiced.Text = $"{currentTechnology.Name} - {remainingTurns} turns";
                        }
                        else
                        {
                            MessageBox.Show("Prerequisites not researched", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        private int GetProductionCost(string itemName)
        {
            switch (itemName)
            {
                case "Settler":
                    return 30;
                case "Worker":
                    return 20;
                case "Warrior":
                    return 20;
                case "Granary":
                    return 50;
                default:
                    return 0;
            }
        }

        private void BtnNextTurn_Click(object sender, EventArgs e)
        {
            year -= 25;
            UpdateYearLabel();
            EndTurn();

            // Resetar movimento de todas as unidades
            foreach (Unidade unit in unidades)
            {
                unit.Movimento = 1;
            }

            // Atualizar fila de produção de todas as cidades
            foreach (Cidade cidade in cidades)
            {
                if (cidade.FilaProducao.Count > 0)
                {
                    var item = cidade.FilaProducao[0];
                    item.TurnosRestantes--;

                    if (item.TurnosRestantes == 0)
                    {
                        cidade.FilaProducao.RemoveAt(0);
                        if (item.Tipo == ItemType.Unit)
                        {
                            AdicionarUnidade(cidade, item.Nome);
                        }
                        else if (item.Tipo == ItemType.Building)
                        {
                            cidade.Construcoes.Add(item.Nome);
                        }
                    }
                }
            }
        }

        private void AdicionarUnidade(Cidade cidade, string unitName)
        {
            List<Button> adjacentTiles = GetAdjacentTiles(cidade.Botao);
            if (adjacentTiles.Count > 0)
            {
                int index = random.Next(adjacentTiles.Count);
                Button selectedTile = adjacentTiles[index];

                Unidade newUnit = new Unidade(1, 1, 1, 1, selectedTile);
                newUnit.Botao.Text = GetUnitSymbol(unitName);
                newUnit.Botao.ForeColor = Color.White;
                newUnit.Botao.BackColor = Color.Red; // Define o fundo como vermelho
                unidades.Add(newUnit);
            }
        }

        private string GetUnitSymbol(string unitName)
        {
            switch (unitName)
            {
                case "Settler":
                    return "S";
                case "Worker":
                    return "Wo";
                case "Warrior":
                    return "Wa";
                default:
                    return "";
            }
        }

        private List<Button> GetAdjacentTiles(Button cityButton)
        {
            List<Button> adjacentTiles = new List<Button>();
            int index = panelMap.Controls.IndexOf(cityButton);
            int col = index % gridSize;
            int row = index / gridSize;

            if (row > 0) adjacentTiles.Add((Button)panelMap.Controls[index - gridSize]);
            if (row < gridSize - 1) adjacentTiles.Add((Button)panelMap.Controls[index + gridSize]);
            if (col > 0) adjacentTiles.Add((Button)panelMap.Controls[index - 1]);
            if (col < gridSize - 1) adjacentTiles.Add((Button)panelMap.Controls[index + 1]);

            return adjacentTiles.Where(tile => tile.BackColor == Color.Green).ToList();
        }

        private void EndTurn()
        {
            // Atualizar a pesquisa da tecnologia
            if (currentTechnology != null && remainingTurns > 0)
            {
                remainingTurns--;
                lblTechChoiced.Text = $"{currentTechnology.Name} - {remainingTurns} turns";

                if (remainingTurns == 0)
                {
                    MessageBox.Show($"{currentTechnology.Name} researched!", "Research Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Adicionar a tecnologia pesquisada à lista
                    if (currentTechnology != null)
                    {
                        researchedTechnologies.Add(currentTechnology.Name);
                    }

                    currentTechnology = null; // Pesquisa concluída
                    lblTechChoiced.Text = "No technology in research";
                }
            }
        }

        private void UpdateYearLabel()
        {
            lblYear.Text = $"Year: {year} a.C.";
        }

        private void btnTechTree_Click(object sender, EventArgs e)
        {
            AbrirDialogoTecnologia();
        }
    }
}
