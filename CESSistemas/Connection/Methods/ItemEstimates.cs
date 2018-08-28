﻿using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using Promig.Connection;
using Promig.Exceptions;
using Promig.Model;
using Promig.Model.CbModel;

namespace Promig.Connection.Methods {

    /*______________________________________________________________________
     |
     |                      CLASSE DE ACESSO A DADOS
     |
     |      Classe para inserir editar, excluir, inativar e recuperar todos
     |      os dados relacionados a items de orçamento do sistema do 
     |      banco de dados.
     |
     */
    class ItemEstimates {

        #region Header

        private MySqlConnection conn;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ItemEstimates() { conn = ConnectionFactory.GetConnection(); }

        #endregion

        #region Data-Access

        public void AddItem(ItemEstimate item) {
            try {

                // Definindo comando de inserção
                string command = $"INSERT INTO orcamento_servicos" +
                                 $"(id_orcamento, id_servico, quantidade) " +
                                 $"VALUES(last_insert_id(), @id_service, @amount);";

                // Definição do comando instanciado
                MySqlCommand cmd = new MySqlCommand(command, conn) {
                    CommandType = CommandType.Text
                };

                // Definição dos valores dos parametros
                cmd.Parameters.Add(new MySqlParameter("@id_service", item.Service.Id));
                cmd.Parameters.Add(new MySqlParameter("@amount", item.Amount));

                // Preparando comando com os parametros
                cmd.Prepare();

                // Executando inserção
                cmd.ExecuteNonQuery();

            } catch (MySqlException) {

            }
        }

        #endregion
    }
}
