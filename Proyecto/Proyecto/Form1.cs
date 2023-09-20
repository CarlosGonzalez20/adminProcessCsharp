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

        
        
        // En esta parte se maneja el evento de hacer clic en una celda del DataGridView
        private void dgvAdministrador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Primero se muestra el nombre del proceso seleccionado en un cuadro de texto
            txtNombreProceso.Text = dgvAdministrador.CurrentRow.Cells[0].Value.ToString();
        }

        // En esta parte encontramos, el evento de carga del formulario (opcional)
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // En esta otra parte se esta trabajando con el boton de Actualizar
        // Por ello, se maneja el evento de hacer clic en el boton de Actualizar
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Llama a la funcion para actualizar manualmente 
            UpdateProcessList();
        }
        // En esta parte se esta trabajando con el boton de Detener 
        // Por ello, se maneja el evento de hacer clic en el boton de Deter 
        private void btnDetener_Click(object sender, EventArgs e)
        {
            // Primero verifica si se selecciono una fila en el DataGridView
            if (dgvAdministrador.SelectedRows.Count>0)
            {

                // Este bloque de codigo se ejecutara solo si se ha seleccionado al menos una fila en el DataGridView.
            }
            try
            {
                //Utiliza un bucle foreach para recorrer todos los procesos en ejecución en el sistema.
                foreach (Process p in Process.GetProcesses())
                {

                    // Compara el nombre del proceso actual con el texto en el cuadro de texto txtNombreProceso.
                    if (p.ProcessName == txtNombreProceso.Text)
                    {
                        p.Kill(); // elimina el proceso
                    }

                }
            }
            catch (Exception x)
            {
                // Muestra un cuadro de diálogo de mensaje en caso de un error
                MessageBox.Show("No seleccionó ningún proceso" + x, "Error al eliminar", MessageBoxButtons.OK);
            }


        }
        // En esta parte se esta trabajando con el boton de Salir
        // Por ello se maneja el evento de hacer clic en el botón "Salir" para cerrar la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Encontramos un evento de clic en un label (puede estar vacío en este caso).
        private void label1_Click(object sender, EventArgs e)
        {

        }
        // Y finalmente se maneja el evento del temporizador (timer1) para actualizar automáticamente la lista de procesos.
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateProcessList();
        }
    }
}
