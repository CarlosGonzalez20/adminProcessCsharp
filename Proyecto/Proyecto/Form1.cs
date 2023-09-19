using System;
//La siguiente librería es necesaria para la utilización o acceso a los procesos de la computadora
using System.Diagnostics;
//La siguiente librería es utilizada para controlar el tiempo en el que se ejecutará una acción
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
            //Se inicializan los componentes, que en este caso es el Data Grid View
            InitializeComponent();
            //Se imprime la lista en el Data Grid View
            UpdateProcessList();
            //Se activa el temporizador
            timer1.Enabled = true;
        }

        //Función para actualizar la lista con toda la información de los procesos
        private void UpdateProcessList()
        {
            //Primero se limpia cualquier tipo de contenido en el DGV
            dgvAdministrador.Rows.Clear();
            //Por medio del Foreach se inicia el bucle para recopilar información sobre los procesos
            //Lo que está seguido del Foreach es la inicialización de la variable "p" como tipo proceso, para así obtener información sobre los procesos
            foreach (Process p in Process.GetProcesses())
            {
                //Cada vez que el bucle termina de recopilar información, se agrega un nuevo registro a la lista
                int n = dgvAdministrador.Rows.Add();
                //Se obtiene el nombre del proceso y se coloca en la columna 0
                dgvAdministrador.Rows[n].Cells[0].Value = p.ProcessName;
                //Se obtiene el ID del proceso y se coloca en la columna 1
                dgvAdministrador.Rows[n].Cells[1].Value = p.Id;
                //Se obtiene la utilización de memoria del proceso y se coloca en la columna 2
                dgvAdministrador.Rows[n].Cells[2].Value = p.WorkingSet64;
                //Se obtiene la utilización de memoria virtual del proceso y se coloca en la columna 3
                dgvAdministrador.Rows[n].Cells[3].Value = p.VirtualMemorySize64;
                //Se obtiene la utilización de CPU del proceso y se coloca en la columna 4
                dgvAdministrador.Rows[n].Cells[4].Value = p.SessionId;
            }
            //Se organizan los procesos de forma alfabética para más facilidad
            dgvAdministrador.Sort(dgvAdministrador.Columns[0], ListSortDirection.Ascending);
            //Se coloca en una etiqueta situada en el lado izauierdo inferior de la ventana, la cantidad de procesos activos
            txtContador.Text = "Procesos activos: "+dgvAdministrador.Rows.Count.ToString();
            //Se imprime en consola el mensaje "lista actualizada" para verificar que sí se ejecute la función
            Console.WriteLine("Lista actualizada");
            
            //Esta función está siendo ejecutada por medio de un contador o temporizador. Esta función se ejecuta cada 10 segundos
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
