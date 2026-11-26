using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registrodealumnosequipojuanjose
{
    public partial class Form1 : Form
    {
        List<Alumnos> alumnos = new List<Alumnos>();
        int indice = -1;
        public Form1()
        {
            InitializeComponent();
        }
        void actualizaVista()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alumnos;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int aprobados = alumnos.Count(a => a.Promedio >= 6);
            int reprobados = alumnos.Count(a => a.Promedio < 6);
            lblAprobados.Text = "Aprobados: " + aprobados;
            lblReprobados.Text = "Reprobados: " + reprobados;
        }
        void limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            indice = -1;
            textBox1.Focus();
        }
        void SoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("El campo Nombre no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("El campo Grupo no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("El campo Calf1 no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("El campo Calf2 no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("El campo Calf3 no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox5.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    MessageBox.Show("El campo Calf4 no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox6.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("El campo Calf5 no puede estar vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox7.Focus();
                    return;
                }

                double c1, c2, c3, c4, c5;
                if (!double.TryParse(textBox3.Text, out c1))
                {
                    MessageBox.Show("Calf1 debe ser un número de 0 a 10.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus();
                    return;
                }
                if (!double.TryParse(textBox4.Text, out c2))
                {
                    MessageBox.Show("Calf2 debe ser un número de 0 a 10.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus();
                    return;
                }
                if (!double.TryParse(textBox5.Text, out c3))
                {
                    MessageBox.Show("Calf3 debe ser un número de 0 a 10.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox5.Focus();
                    return;
                }
                if (!double.TryParse(textBox6.Text, out c4))
                {
                    MessageBox.Show("Calf4 debe ser un número de 0 a 10.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox6.Focus();
                    return;
                }
                if (!double.TryParse(textBox7.Text, out c5))
                {
                    MessageBox.Show("Calf5 debe ser un número de 0 a 10.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox7.Focus();
                    return;
                }

                if (c1 < 0 || c1 > 10 || c2 < 0 || c2 > 10 || c3 < 0 || c3 > 10 || c4 < 0 || c4 > 10 || c5 < 0 || c5 > 10)
                {
                    MessageBox.Show("Las calificaciones solo pueden ser de 0 a 10.", "Rango no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombreNuevo = textBox1.Text.Trim();
                string grupoNuevo = textBox2.Text.Trim();

                bool yaExiste = alumnos.Any(j =>
                    j.Nombre.Trim().Equals(nombreNuevo, StringComparison.OrdinalIgnoreCase) &&
                    j.Grupo.Trim().Equals(grupoNuevo, StringComparison.OrdinalIgnoreCase));

                if (indice != -1)
                {
                    yaExiste = alumnos.Where((j, index) => index != indice).Any(j =>
                        j.Nombre.Trim().Equals(nombreNuevo, StringComparison.OrdinalIgnoreCase) &&
                        j.Grupo.Trim().Equals(grupoNuevo, StringComparison.OrdinalIgnoreCase));
                }

                if (yaExiste)
                {
                    MessageBox.Show("El alumno '" + nombreNuevo + "' ya está registrado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Alumnos a = new Alumnos();
                a.Nombre = nombreNuevo;
                a.Grupo = grupoNuevo;
                a.Cal1 = c1; a.Cal2 = c2; a.Cal3 = c3; a.Cal4 = c4; a.Cal5 = c5;

                if (indice == -1) alumnos.Add(a);
                else alumnos[indice] = a;

                string mensajeEstado = "";
                if (a.Promedio < 6) mensajeEstado = "Reprobado";
                else if (a.Promedio < 8) mensajeEstado = "Bien pero puedes mejorar";
                else mensajeEstado = "Muy bien!";

                MessageBox.Show(a.Nombre + " - Promedio: " + a.Promedio.ToString("0.0") + " - " + mensajeEstado);
                actualizaVista();
                limpiar();
            }
            catch (FormatException)
            {
                MessageBox.Show("Revisa que las calificaciones sean números de 0 a 10.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                indice = e.RowIndex;
                var a = alumnos[indice];
                textBox1.Text = a.Nombre;
                textBox2.Text = a.Grupo;
                textBox3.Text = a.Cal1.ToString();
                textBox4.Text = a.Cal2.ToString();
                textBox5.Text = a.Cal3.ToString();
                textBox6.Text = a.Cal4.ToString();
                textBox7.Text = a.Cal5.ToString();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filtro))
            {
                actualizaVista();
                return;
            }
            var filtrados = alumnos.Where(a => a.Nombre.ToLower().Contains(filtro) || a.Grupo.ToLower().Contains(filtro)).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = filtrados;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (indice == -1)
            {
                MessageBox.Show("Selecciona un alumno de la tabla", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show("¿Seguro que quieres eliminar a " + alumnos[indice].Nombre + "?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                alumnos.RemoveAt(indice);
                actualizaVista();
                limpiar();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            try
            {
                if (!File.Exists("Alumnos.txt")) return;

                StreamReader r = new StreamReader("Alumnos.txt");
                string linea;
                while ((linea = r.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] p = linea.Split('|');
                    if (p.Length < 7) continue;

                    Alumnos a = new Alumnos();
                    a.Nombre = p[0];
                    a.Grupo = p[1];

                    double v1, v2, v3, v4, v5;
                    if (!double.TryParse(p[2], out v1)) continue;
                    if (!double.TryParse(p[3], out v2)) continue;
                    if (!double.TryParse(p[4], out v3)) continue;
                    if (!double.TryParse(p[5], out v4)) continue;
                    if (!double.TryParse(p[6], out v5)) continue;

                    a.Cal1 = v1;
                    a.Cal2 = v2;
                    a.Cal3 = v3;
                    a.Cal4 = v4;
                    a.Cal5 = v5;

                    alumnos.Add(a);
                }
                r.Close();
                actualizaVista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el archivo de alumnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e) => SoloNumeros(e);
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e) => SoloNumeros(e);
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e) => SoloNumeros(e);
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e) => SoloNumeros(e);
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e) => SoloNumeros(e);

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter w = new StreamWriter("Alumnos.txt");
                foreach (var p in alumnos)
                {
                    w.WriteLine(p.Nombre + "|" + p.Grupo + "|" + p.Cal1 + "|" + p.Cal2 + "|" + p.Cal3 + "|" + p.Cal4 + "|" + p.Cal5 + "|" + p.Promedio);
                }
                w.Close();
                MessageBox.Show("Guardado con promedio!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog guarda = new SaveFileDialog();
                guarda.Filter = "Archivo CSV|*.csv";
                guarda.Title = "¿Qué nombre quieres para el archivo?";
                guarda.FileName = "Reprobados_Morelos";

                if (guarda.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(guarda.FileName, false, System.Text.Encoding.UTF8))
                    {
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            sw.Write(dataGridView1.Columns[i].HeaderText);
                            if (i < dataGridView1.Columns.Count - 1) sw.Write(",");
                        }
                        sw.WriteLine();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].IsNewRow) continue;

                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    string valor = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                    if (valor.Contains(","))
                                        valor = "\"" + valor + "\"";

                                    sw.Write(valor);
                                }
                                if (j < dataGridView1.Columns.Count - 1) sw.Write(",");
                            }
                            sw.WriteLine();
                        }
                    }
                    MessageBox.Show("Archivo CSV guardado en:\n" + guarda.FileName, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

