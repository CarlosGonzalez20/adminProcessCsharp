using System;
using System.Diagnostics;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace Proyecto
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            UpdateProcessList();
            //System.Timers.Timer temporizador = new System.Timers.Timer(2000);
            timer1.Enabled = true;
            //temporizador.Elapsed += (s, e) => UpdateProcessList();
        }

        private void UpdateProcessList()
        {
            dgvAdministrador.Rows.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                int n = dgvAdministrador.Rows.Add();
                dgvAdministrador.Rows[n].Cells[0].Value = p.ProcessName;
                dgvAdministrador.Rows[n].Cells[1].Value = p.Id;
                dgvAdministrador.Rows[n].Cells[2].Value = p.WorkingSet64;
                dgvAdministrador.Rows[n].Cells[3].Value = p.VirtualMemorySize64;
                dgvAdministrador.Rows[n].Cells[4].Value = p.SessionId;
            }
            dgvAdministrador.Sort(dgvAdministrador.Columns[0], ListSortDirection.Ascending);
            txtContador.Text = "Procesos activos: "+dgvAdministrador.Rows.Count.ToString();
            Console.WriteLine("Lista actualizada");
        }

        private void dgvAdministrador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombreProceso.Text = dgvAdministrador.CurrentRow.Cells[0].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            UpdateProcessList();
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            if (dgvAdministrador.SelectedRows.Count>0)
            {
                //
            }
            try
            {
                foreach (Process p in Process.GetProcesses())
                {


                    if (p.ProcessName == txtNombreProceso.Text)
                    {
                        p.Kill(); // elimina el proceso
                    }

                }
            }
            catch (Exception x)
            {
                MessageBox.Show("No seleccionó ningún proceso" + x, "Error al eliminar", MessageBoxButtons.OK);
            }


        }
        

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateProcessList();
        }
    }
}
