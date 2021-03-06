﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Promig.View.Components;

namespace Promig.View {
    
    public partial class MainWindow : Window {

        #region Header

        // Importando ddll de conexão com internet
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        // Definindo flags
        public static string currentUsername;
        public static string currentPermission;
        public static int currentId;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            initializeUserControl();
        }

        #endregion Header

        #region Events

        /// <summary>
        /// Método para definir valores padrão ao iniciar componente
        /// </summary>
        private void initializeUserControl(){
            UserControl usc = null;
            if (IsConnected()) {
                // Carregando tela de configurações 
                usc = new UserControlMain();
                GridMain.Children.Clear();
                GridMain.Children.Add(usc);
            } else {
                // Retornando mensagem de aviso
                MessageBox.Show(
                    "Você precisa ter conexão com internet para acessar esse módulo",
                    "Alerta",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

        /// <summary>
        /// Evento ao clicar sobre o botão de preferencias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSobre_Click(object sender, RoutedEventArgs e) {
            UserControl usc = null;
            usc = new UserControlAbout();
            GridMain.Children.Clear();
            GridMain.Children.Add(usc);
        }

        /// <summary>
        /// Evento ao clicar no botão de logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, RoutedEventArgs e) {
            Login janela = new Login();
            janela.Show();
            Close();
        }

        /// <summary>
        /// Evento ao clicar no botão de abrir drawer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirMenu_Click(object sender, RoutedEventArgs e) {
            btnAbrirMenu.Visibility = Visibility.Collapsed;
            btnFecharMenu.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Evento ao clicar no botão de fechar drawer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFecharMenu_Click(object sender, RoutedEventArgs e) {
            btnAbrirMenu.Visibility = Visibility.Visible;
            btnFecharMenu.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Evento ao selecionar alguma opcão do menu priincipal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            //Limpando a view atual
            UserControl usc = null;
            GridMain.Children.Clear();

            //Verificando qual tela será carregada
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name) {
                case "Main":
                    usc = new UserControlMain();
                    GridMain.Children.Add(usc);
                    break;
                case "Client":
                    usc = new UserControlClient();
                    GridMain.Children.Add(usc);
                    break;
                case "Employes":
                    usc = new UserControlEmployes();
                    GridMain.Children.Add(usc);
                    break;
                case "Services":
                    usc = new UserControlServices();
                    GridMain.Children.Add(usc);
                    break;
                case "Supplier":
                    usc = new UserControlSupplier();
                    GridMain.Children.Add(usc);
                    break;
                case "BillsToPay":
                    usc = new UserControlBillsToPay();
                    GridMain.Children.Add(usc);
                    break;
                case "BillsToReceive":
                    usc = new UserControlBillsToReceive();
                    GridMain.Children.Add(usc);
                    break;
                case "Estimate":
                    usc = new UserControlEstimate();
                    GridMain.Children.Add(usc);
                    break;
                case "Order":
                    usc = new UserControlOrder();
                    GridMain.Children.Add(usc);
                    break;
                case "Sair":
                    System.Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Evento ao clicar no botão de logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuditoria_Click(object sender, RoutedEventArgs e) {
            UserControl usc = null;
            GridMain.Children.Clear();
            usc = new UserControlLogs();
            GridMain.Children.Add(usc);
        }

        /// <summary>
        /// Evento ao carregar tela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //Definindo nome do usuário ao abrir
            lblTitulo.Text = $"Bem vindo {currentUsername}!";

            //Verificando permissões
            if(currentPermission.Equals("Admin")){
                return;
            }else{
                esconderBotoes();
            }
        }

        #endregion

        #region Utils

        /// <summary>
        /// Método para verificar conexão com internet
        /// </summary>
        /// <returns></returns>
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }

        /// <summary>
        /// Método para desabilitar botões
        /// </summary>
        private void esconderBotoes(){
            Employes.IsEnabled = false;
            BillsToPay.IsEnabled = false;
            BillsToReceive.IsEnabled = false;
            btnAuditoria.IsEnabled = false;

            Employes.Opacity = .50;
            BillsToPay.Opacity = .50;
            BillsToReceive.Opacity = .50;
            btnAuditoria.Opacity = .50;
        }

        #endregion
    }
}
