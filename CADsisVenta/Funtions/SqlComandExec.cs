using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainSQLite.Setting;

namespace CADsisVenta.Funtions
{
	public class SqlComandExec : IDisposable
	{
		private SqlConnection cnn;
		private SqlParameter[] _parameterCollection;
		private object elemtOut;
		private string nameElemet;
		private SqlTransaction  transaction;
		private CommandType commandType;

		public SqlComandExec( SqlConnection conec  = null )
		{
			if (conec == null)
			{
				cnn = new SqlConnection(Configuration.ConectionString);
			}
			else {
				cnn = conec;
			}
			
			_parameterCollection = new SqlParameter[] { };
			this.CommandType =CommandType.Text;

		}

        public async Task<bool> ExecuteComandAsync(string sqlStri)
		{
			try
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
				 await	cnn.OpenAsync();
				}
				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn, transaction==null? null : transaction))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0)
					{
						foreach (var item in this.ParameterCollection)
						{
							cmd.Parameters.Add(item);
							if (item.Direction == ParameterDirection.Output ||
								item.Direction == ParameterDirection.InputOutput ||
								item.Direction == ParameterDirection.ReturnValue)
							{
								nameElemet = item.ParameterName;
							}
						}

					}

					if (await cmd.ExecuteNonQueryAsync() > 0)
					{
						this.elemtOut = !string.IsNullOrEmpty(nameElemet) ? cmd.Parameters[nameElemet].Value : null;
						cmd.Parameters.Clear();
						return true;
					}
					else
					{
						cmd.Parameters.Clear();
						return false;
					}
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
			}
		}

		public bool ExecuteComand(string sqlStri)
		{
			try
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
					cnn.Open();
				}
				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn, transaction == null ? null : transaction))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0)
					{
						foreach (var item in this.ParameterCollection)
						{
							cmd.Parameters.Add(item);
							if (item.Direction == ParameterDirection.Output ||
								item.Direction == ParameterDirection.InputOutput ||
								item.Direction == ParameterDirection.ReturnValue)
							{
								nameElemet = item.ParameterName;
							}
						}

					}

					if (cmd.ExecuteNonQuery() > 0)
					{
						this.elemtOut = !string.IsNullOrEmpty(nameElemet) ? cmd.Parameters[nameElemet].Value : null;
						cmd.Parameters.Clear();
						return true;
					}
					else
					{
						cmd.Parameters.Clear();
						return false;
					}
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
			}
		}


		public void BeginTransaction() {
			if (cnn != null)
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
					cnn.Open();
				}
				transaction = cnn.BeginTransaction();
			}
		}


		public void Commit() {
			if (transaction !=null ) {
				transaction.Commit();
			}

		}

		public SqlTransaction GetTransaction() {
			return transaction;
		}

		public async Task<DataTable> RetornaTablaAsync(string sqlStri)
		{
			try
			{
				cnn.FireInfoMessageEventOnUserErrors = true;
				cnn.InfoMessage += Cnn_InfoMessage;

				if (cnn.State != System.Data.ConnectionState.Open)
				{
					await cnn.OpenAsync();
				}
				

				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0 )
					{
						cmd.Parameters.AddRange(this.ParameterCollection);
					}

					SqlDataAdapter dat = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					dat.Fill(dt);
					cmd.Parameters.Clear();
					return dt;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);

			}
		}


		public  DataTable RetornaTabla(string sqlStri)
		{
			try
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
					 cnn.Open();
				}
				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0)
					{
						cmd.Parameters.AddRange(this.ParameterCollection);
					}

					SqlDataAdapter dat = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					dat.Fill(dt);
					cmd.Parameters.Clear();
					return dt;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);

			}
		}


	
		public async Task<T> ReturTableTypeAsync<T>(string sqlStri) where T : DataTable, new()
		{
			try
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
					await cnn.OpenAsync();
				}
				cnn.FireInfoMessageEventOnUserErrors = true;
				cnn.InfoMessage += Cnn_InfoMessage;
				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0)
					{
						cmd.Parameters.AddRange(this.ParameterCollection);
					}

					SqlDataAdapter dat = new SqlDataAdapter(cmd);
					T dt = new T();
					dat.Fill(dt);
					return dt;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
			}
		}


		public async Task<T> ReturUnitDataAsync<T>(string sqlStri)
		{
			try
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
					await cnn.OpenAsync();
				}
				cnn.FireInfoMessageEventOnUserErrors = true;
				cnn.InfoMessage += Cnn_InfoMessage;
				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0)
					{
						cmd.Parameters.AddRange(this.ParameterCollection);
					}

					SqlDataAdapter dat = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					dat.Fill(dt);
					if (dt != null && dt.Rows.Count == 1)
						return (T)dt.Rows[0][0];
					else
						return default(T);

				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
			}

		}

		public Task<T> ReturUnitData<T>(string sqlStri) 
		{
			try
			{
				if (cnn.State != System.Data.ConnectionState.Open)
				{
					cnn.Open();
				}
				cnn.FireInfoMessageEventOnUserErrors = true;
				cnn.InfoMessage += Cnn_InfoMessage;
				using (SqlCommand cmd = new SqlCommand(sqlStri, cnn))
				{
					cmd.CommandType = this.CommandType;
					if (this.ParameterCollection.Count() > 0)
					{
						cmd.Parameters.AddRange(this.ParameterCollection);
					}

					SqlDataAdapter dat = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					dat.Fill(dt);
					if (dt != null && dt.Rows.Count == 1) 
					{
							return Task.FromResult((T)dt.Rows[0][0]);
					}
					else
						return Task.FromResult(default(T));

				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
			}

		}
		private void Cnn_InfoMessage(object sender, SqlInfoMessageEventArgs args)
		{
			var Err_code = args.Errors[0].Number;

			foreach (SqlError err in args.Errors)
			{
				Interaction.MsgBox(err.Message, MsgBoxStyle.Exclamation, "Error");
			}
	
		}

		public SqlParameter[] ParameterCollection
		{
			get
			{
				return _parameterCollection;
			}
			set
			{
				_parameterCollection = value;
			}
		}

		public   void  SetValueParamater(string nameParameter, object value) {
			foreach (var item in this.ParameterCollection)
			{
				if (item.ParameterName == nameParameter) {
					item.Value = value;
				}
			}
		}

		public CommandType CommandType { get=>this.commandType; set=> this.commandType = value; }

		public object GetElementOut()
		{
			return elemtOut;

		}

		public SqlConnection GetConnection()
		{
			return cnn;
		}

		public void Dispose()
		{
			if (this.cnn.State == ConnectionState.Open)
			{
				this.cnn.Close();
			}
			nameElemet = "";
			elemtOut = null;
			transaction = null;
		}


	}

}
