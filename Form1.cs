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

namespace ListasPersonas
{
    public partial class Form1 : Form
    {
        //nombre persona es el nombre de mi lista
        List<persona> personas = new List<persona>();
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            // int a= 0;
           //persona es mi clase persona1 persona a guardar 
            persona persona1 = new persona(); //Instanciar la clase 

            //Guarda los datos de una persona 
            persona1.Dpi = TxtDpi.Text;
            persona1.Nombre = TxtNom.Text;
            persona1.Apellido = TxtApe.Text;
            persona1.FechaNacimiento = monthCalendar1.SelectionStart;

            //Manda a guardar a la persona a la lista de personas 

            personas.Add(persona1);

            TxtDpi.Text = "";
            TxtNom.Text = "";
            TxtApe.Text = "";
            TxtDpi.Select();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Mostrar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = personas;
            dataGridView1.Refresh();
        }

        private void BtnMostrar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void BtnOrdenar_Click(object sender, EventArgs e)
        {
            personas = personas.OrderBy(p => p.Apellido).ToList();
            Mostrar();
        }


        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            string dpi = TxtDpi.Text;

            personas.RemoveAll(p => p.Dpi == dpi);
            Mostrar();
        }

        private void BtnDecendente_Click(object sender, EventArgs e)
        {
            personas.OrderByDescending(p => p.Apellido);
            Mostrar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            FileStream stream =new  FileStream("personas.txt", FileMode.OpenOrCreate, FileAccess.Write );
            StreamWriter writer = new StreamWriter(stream);
            for(int i = 0; i<personas.Count; i++)
            {
                writer.WriteLine(personas[i].Dpi);
                writer.WriteLine(personas[i].Nombre);
                writer.WriteLine(personas[i].Apellido);
                writer.WriteLine(personas[i].FechaNacimiento.ToShortDateString());
            }
            writer.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "personas.txt";

            FileStream stream = new FileStream(fileName , FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while(reader.Peek() > -1)
            {
                persona persona = new persona();

                persona.Dpi = reader.ReadLine();
                persona.Nombre = reader.ReadLine();
                persona.Apellido = reader.ReadLine();
                persona.FechaNacimiento = Convert.ToDateTime(reader.ReadLine()) ;

                personas.Add(persona);
               
            }
           // writer.Close();
            Mostrar();
        }
    }
}
