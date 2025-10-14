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
    public partial class RegistroForm : Form
    {
        private readonly IAuthService _authService;
        private readonly IPdfServiceFactory _pdfServiceFactory;
        private bool _passwordVisible = false;

        public RegistroForm(IAuthService authService, IPdfServiceFactory pdfServiceFactory)
        {
            InitializeComponent();
            _authService = authService;
            _pdfServiceFactory = pdfServiceFactory;
        }

        private async void btnRegistrarse_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim();

            if (!EsPasswordValido(password))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula, una minúscula, un número y un carácter especial.", "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnRegistro.Enabled = false;
            btnLogin.Enabled = false;
            progressBarSpinner.Visible = true;
            Cursor = Cursors.WaitCursor;

            try
            {
                var registrado = await _authService.RegistroAsync(email, password);
                if (registrado)
                {
                    MessageBox.Show("Usuario registrado exitosamente.");
                    var loginForm = new LoginForm(_authService, _pdfServiceFactory);
                    this.Hide();
                    loginForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrió un problema. Intente más tarde.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}");
            }
            finally
            {
                progressBarSpinner.Visible = false;
                btnRegistro.Enabled = true;
                btnLogin.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnVolverLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm(_authService, _pdfServiceFactory);
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void RegistroForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnMostrarPassword.Text = "👁️";
            btnMostrarPassword.Font = new Font("Segoe UI Emoji", 8);
            btnMostrarPassword.Cursor = Cursors.Hand;
            btnMostrarPassword.FlatStyle = FlatStyle.Flat;
            btnMostrarPassword.FlatAppearance.BorderSize = 0;
            btnMostrarPassword.BackColor = txtPassword.BackColor;
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
    }
}
