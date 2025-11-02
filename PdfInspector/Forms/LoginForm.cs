using PdfInspector.App.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfInspector.Forms
{
    public partial class LoginForm : Form
    {
        private readonly LoginCasoUso _loginCasoUso;
        private readonly RegistroForm _registroForm;
        private bool _passwordVisible = false;

        public LoginForm(LoginCasoUso loginCasoUso, RegistroForm registroForm)
        {
            InitializeComponent();
            _loginCasoUso = loginCasoUso;
            _registroForm = registroForm;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim();

            if (!EsPasswordValido(password))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula, una minúscula, un número y un carácter especial.", "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnRegistro.Enabled = false;
            progressBarSpinner.Visible = true;
            Cursor = Cursors.WaitCursor;

            try
            {
                var token = await _loginCasoUso.Ejecutar(username, password);

                if (token != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales inválidas. Intente nuevamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de autenticación: {ex.Message}");
            }
            finally
            {
                progressBarSpinner.Visible = false;
                btnLogin.Enabled = true;
                btnRegistro.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            this.Hide();
            _registroForm.ShowDialog();
            this.Show();
        }

        private bool EsPasswordValido(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$");
            return regex.IsMatch(password);
        }

        private void btnMostrarPassword_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.UseSystemPasswordChar = !_passwordVisible;
            btnMostrarPassword.Text = _passwordVisible ? "🚫👁️" : "👁️";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnMostrarPassword.Text = "👁️";
            btnMostrarPassword.Font = new Font("Segoe UI Emoji", 8);
            btnMostrarPassword.Cursor = Cursors.Hand;
            btnMostrarPassword.FlatStyle = FlatStyle.Flat;
            btnMostrarPassword.FlatAppearance.BorderSize = 0;
            btnMostrarPassword.BackColor = txtPassword.BackColor;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
