using PdfInspector.App.CasosUso.Auth;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PdfInspector.Forms
{
    public partial class RegistroForm : Form
    {
        private readonly RegistroCasoUso _registroCasoUso;
        private bool _passwordVisible = false;

        public RegistroForm(RegistroCasoUso registroCasoUso)
        {
            InitializeComponent();
            _registroCasoUso = registroCasoUso;
        }

        private async void btnRegistrarse_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || !EsPasswordValido(password))
            {
                MessageBox.Show("Por favor, ingrese un correo válido y una contraseña que cumpla con los requisitos.", "Datos Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ToggleControls(false);

            try
            {
                var registrado = await _registroCasoUso.ExecuteAsync(new App.DTOs.Auth.RegistroRequest() { Email = email, Password  = password});
                if ((bool)registrado)
                {
                    MessageBox.Show("Usuario registrado exitosamente. Ahora puede iniciar sesión.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo completar el registro. El correo electrónico ya podría estar en uso.", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleControls(true);
            }
        }

        private void btnVolverLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistroForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnShowPass.Font = new Font("Segoe UI Emoji", 8);
        }

        private bool EsPasswordValido(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$");
            return !string.IsNullOrEmpty(password) && regex.IsMatch(password);
        }

        private void btnMostrarPassword_Click(object sender, EventArgs e)
        {
            
        }

        private void ToggleControls(bool isEnabled)
        {
            btnLogin.Enabled = isEnabled;
            btnRegistro.Enabled = isEnabled;
            progressBarSpinner.Visible = !isEnabled;
            Cursor = isEnabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.UseSystemPasswordChar = !_passwordVisible;
            btnShowPass.Text = _passwordVisible ? "🚫" : "👁️";
        }
    }
}
