using chz.Common;
using chz.WindowsServices.DataBase;
using chz.WindowsServices.LoadDocument.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
namespace chz.DataAccess.LoadDocument
{
    public class DataBase : DataBase<IncomeDocument>, IDataBase
    {
        private const string GetErrorIncomeDocumentsSQLRequest = @"SELECT [id],[request_id],[document_id],[date],[processed_date],[sender],[receiver],[sys_id],[doc_type],[mdlp_ducument_status],[file_uploadtype]       
        ,[version],[sender_sys_id],[content],[link],[service_document_status],[error_type],[error_message],[http_status],[error_response],[guid]
        From [dbo].chz_income_documents
        Where error_type = 'HttpRequestError' Or error_type = 'CryptographyError' Or (error_type='MDLPError' And http_status='Unauthorized')
        Or (error_type='MDLPError' And http_status='Forbidden')";

        private const string GetLastIncomeDocumentsSQLRequest = @"Select [id],[request_id],[document_id],[date],[processed_date],[sender],[receiver],[sys_id],[doc_type],[mdlp_ducument_status],[file_uploadtype]       
        ,[version],[sender_sys_id],[content],[link],[service_document_status],[error_type],[error_message],[http_status],[error_response],[guid]        
        From [dbo].chz_income_documents
        Where [dbo].chz_income_documents.processed_date =
        (
         Select MAX([dbo].chz_income_documents.processed_date)
         From [dbo].chz_income_documents
        )";

        private const string GetLoadLinkSQLRequest = @"SELECT [id],[request_id],[document_id],[date],[processed_date],[sender],[receiver],[sys_id],[doc_type],[mdlp_ducument_status],[file_uploadtype]       
       ,[version],[sender_sys_id],[content],[link],[service_document_status],[error_type],[error_message],[http_status],[error_response],[guid]  
        From [dbo].chz_income_documents
        Where service_document_status = 'LinkLoad' And error_type = NUll";

        private const string GetNewIncomeDocumentsSQLRequest = @"SELECT [id],[request_id],[document_id],[date],[processed_date],[sender],[receiver],[sys_id],[doc_type],[mdlp_ducument_status],[file_uploadtype]       
        ,[version],[sender_sys_id],[content],[link],[service_document_status],[error_type],[error_message],[http_status],[error_response],[guid]  
        From [dbo].chz_income_documents
        Where service_document_status = 'New' And error_type = NUll";

        private const string AddIncomeDocumentSQLRequest = @"Insert Into [dbo].chz_income_documents (request_id,document_id,date,processed_date,sender,receiver,sys_id,doc_type,mdlp_ducument_status,
	    file_uploadtype,version,sender_sys_id, content, link, service_document_status, error_type, error_message, http_status, error_response,guid)
	    Values(@request_id, @document_id, @date, @processed_date, @sender, @receiver, @sys_id, @doc_type, @mdlp_ducument_status, @file_uploadtype, 
	    @version,
	    @sender_sys_id, @content, @link, @service_document_status, @error_type, @error_message, @http_status, @error_response, @guid)
        SET @id= SCOPE_IDENTITY()";


        private const string UpdateIncomeDocumentSQLRequest = @"Update [dbo].chz_income_documents 
        Set request_id = @request_id, document_id=@document_id, date=@date, processed_date=@processed_date, sender=@sender, receiver=@receiver,
        sys_id=@sys_id, doc_type=@doc_type, mdlp_ducument_status=@mdlp_ducument_status, file_uploadtype=@file_uploadtype, version =@version,
        sender_sys_id=@sender_sys_id,content=@content, link=@link, service_document_status=@service_document_status, error_type=@error_type, 
        error_message=@error_message,complete_date=@complete_date, http_status=@http_status, error_response=@error_response,guid=@guid
        Where id=@id";

        public DataBase(WindowsServices.Setting.ISettingProvider setting) : base(setting)
        {
        }

        public int Add(IncomeDocument incomeDocument)
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var command = new SqlCommand(AddIncomeDocumentSQLRequest, con);
                incomeDocument.Guid = Guid.NewGuid().ToString();
                var parameters = command.Parameters;

                parameters.AddWithValue("@request_id", Convert(incomeDocument.RequestId));
                parameters.AddWithValue("@document_id", Convert(incomeDocument.DocumentId));
                parameters.AddWithValue("@date", Convert(incomeDocument.Date));
                parameters.AddWithValue("@processed_date", Convert(incomeDocument.ProcessedDate));
                parameters.AddWithValue("@sender", Convert(incomeDocument.Sender));
                parameters.AddWithValue("@receiver", Convert(incomeDocument.Receiver));
                parameters.AddWithValue("@sys_id", Convert(incomeDocument.SysId));
                parameters.AddWithValue("@doc_type", Convert(incomeDocument.DocType));
                parameters.AddWithValue("@mdlp_ducument_status", incomeDocument.Status.ToString());
                parameters.AddWithValue("@file_uploadtype", Convert(incomeDocument.FileUploadtype));
                parameters.AddWithValue("@version", Convert(incomeDocument.Version));
                parameters.AddWithValue("@sender_sys_id", Convert(incomeDocument.SenderSysId));
                parameters.AddWithValue("@content", Convert(incomeDocument.Content));
                parameters.AddWithValue("@link", Convert(incomeDocument.Link));
                parameters.AddWithValue("@service_document_status", incomeDocument.IncomeDocumentStatus.ToString());
                parameters.AddWithValue("@guid", Convert(incomeDocument.Guid));

                if (incomeDocument.Error != null)
                {
                    parameters.AddWithValue("@error_message", incomeDocument.Error.Message);

                    if (incomeDocument.Error is MDLPError)
                    {
                        var error = (MDLPError)incomeDocument.Error;

                        parameters.AddWithValue("@error_type", "MDLPError");
                        parameters.AddWithValue("@http_status", error.HttpStatusCode.ToString());
                        parameters.AddWithValue("@error_response", error.Response);
                    }
                    else
                    {
                        var error = (ServiceError)incomeDocument.Error;

                        parameters.AddWithValue("@error_type", error.ServiceErrorType.ToString());
                    }
                }
                else
                {
                    parameters.AddWithValue("@error_message", DBNull.Value);
                    parameters.AddWithValue("@error_type", DBNull.Value);
                    parameters.AddWithValue("@http_status", DBNull.Value);
                    parameters.AddWithValue("@error_response", DBNull.Value);
                }

                var param = new SqlParameter();
                param.ParameterName = "@id";
                param.SqlDbType = SqlDbType.Int;
                param.Size = 100;
                param.Direction = ParameterDirection.Output;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();

                return (int)command.Parameters["@id"].Value;
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
            }
        }

        public override IEnumerable<IncomeDocument> GetError()
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetErrorIncomeDocumentsSQLRequest;
                sqlCommand.Connection = con;

                var reader = sqlCommand.ExecuteReader();

                return Read(reader);
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
            }
            finally
            {
                con.Close();
            }
        }

        public IEnumerable<IncomeDocument> GetLastDocument()
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetLastIncomeDocumentsSQLRequest;
                sqlCommand.Connection = con;

                var reader = sqlCommand.ExecuteReader();

                return Read(reader);
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
            }
            finally
            {
                con.Close();
            }
        }

        public IEnumerable<IncomeDocument> GetLoadLink()
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetLoadLinkSQLRequest;
                sqlCommand.Connection = con;

                var reader = sqlCommand.ExecuteReader();

                return Read(reader);
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
            }
            finally
            {
                con.Close();
            }
        }

        public IEnumerable<IncomeDocument> GetNewIncomeDocument()
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetNewIncomeDocumentsSQLRequest;
                sqlCommand.Connection = con;

                var reader = sqlCommand.ExecuteReader();

                return Read(reader);
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
            }
            finally
            {
                con.Close();
            }
        }

        public override void Update(IncomeDocument incomeDocument)
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var command = new SqlCommand(UpdateIncomeDocumentSQLRequest, con);
                var parameters = command.Parameters;

                parameters.AddWithValue("@id", Convert(incomeDocument.Id));
                parameters.AddWithValue("@request_id", Convert(incomeDocument.RequestId));
                parameters.AddWithValue("@document_id", Convert(incomeDocument.DocumentId));
                parameters.AddWithValue("@date", Convert(incomeDocument.Date));
                parameters.AddWithValue("@processed_date", Convert(incomeDocument.ProcessedDate));
                parameters.AddWithValue("@sender", Convert(incomeDocument.Sender));
                parameters.AddWithValue("@receiver", Convert(incomeDocument.Receiver));
                parameters.AddWithValue("@sys_id", Convert(incomeDocument.SysId));
                parameters.AddWithValue("@doc_type", Convert(incomeDocument.DocType));
                parameters.AddWithValue("@mdlp_ducument_status", Convert(incomeDocument.Status.ToString()));
                parameters.AddWithValue("@file_uploadtype", Convert(incomeDocument.FileUploadtype));
                parameters.AddWithValue("@version", Convert(incomeDocument.Version));
                parameters.AddWithValue("@sender_sys_id", Convert(incomeDocument.SenderSysId));
                parameters.AddWithValue("@content", Convert(incomeDocument.Content));
                parameters.AddWithValue("@link", Convert(incomeDocument.Link));
                parameters.AddWithValue("@service_document_status", incomeDocument.IncomeDocumentStatus.ToString());
                parameters.AddWithValue("@guid", Convert(incomeDocument.Guid));
                parameters.AddWithValue("@complete_date", Convert(incomeDocument.CompleteDateTime));

                if (incomeDocument.Error != null)
                {
                    parameters.AddWithValue("@error_message", incomeDocument.Error.Message);

                    if (incomeDocument.Error is MDLPError)
                    {
                        var error = (MDLPError)incomeDocument.Error;

                        parameters.AddWithValue("@error_type", "MDLPError");
                        parameters.AddWithValue("@http_status", error.HttpStatusCode.ToString());
                        parameters.AddWithValue("@error_response", error.Response);
                    }
                    else
                    {
                        var error = (ServiceError)incomeDocument.Error;

                        parameters.AddWithValue("@error_type", error.ServiceErrorType.ToString());
                        parameters.AddWithValue("@http_status", DBNull.Value);
                        parameters.AddWithValue("@error_response", DBNull.Value);
                    }
                }
                else
                {
                    parameters.AddWithValue("@error_message", DBNull.Value);
                    parameters.AddWithValue("@error_type", DBNull.Value);
                    parameters.AddWithValue("@http_status", DBNull.Value);
                    parameters.AddWithValue("@error_response", DBNull.Value);
                }

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
                
            }
        }

        private IEnumerable<IncomeDocument> Read(SqlDataReader reader)
        {
            var result = new List<IncomeDocument>();

            while (reader.Read())
            {
                var incomeDocument = new IncomeDocument();

                incomeDocument.Id = (int)reader["id"];
                incomeDocument.DocumentId = MapValue<string>(reader, "document_id");
                incomeDocument.RequestId = MapValue<string>(reader, "request_id");
                incomeDocument.Date = MapValue<DateTime?>(reader, "date");
                incomeDocument.ProcessedDate = MapValue<DateTime?>(reader, "processed_date");
                incomeDocument.Sender = MapValue<string>(reader, "sender");
                incomeDocument.Receiver = MapValue<string>(reader, "receiver");
                incomeDocument.SysId = MapValue<string>(reader, "sys_id");
                incomeDocument.DocType = MapValue<int>(reader, "doc_type");
                incomeDocument.Status = (MDLPDocumentStatus)Enum.Parse(typeof(MDLPDocumentStatus), MapValue<string>(reader, "mdlp_ducument_status"));
                incomeDocument.FileUploadtype = MapValue<int>(reader, "file_uploadtype");
                incomeDocument.Version = MapValue<string>(reader, "version");
                incomeDocument.SenderSysId = MapValue<string>(reader, "sender_sys_id");
                incomeDocument.Content = MapValue<string>(reader, "content");
                incomeDocument.Link = MapValue<string>(reader, "link");
                incomeDocument.Guid = MapValue<string>(reader, "guid");
                incomeDocument.IncomeDocumentStatus = (ServiceIncomeDocumentStatus)Enum.Parse(typeof(ServiceIncomeDocumentStatus), MapValue<string>(reader, "service_document_status"));

                var errorType = MapValue<string>(reader, "error_type");

                if (errorType != null)
                {
                    if (errorType == "MDLPError")
                    {
                        incomeDocument.SetError(MapValue<string>(reader, "error_message"), MapValue<string>(reader, "error_response")
                            , (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), (string)reader["http_status"]));
                    }
                    else
                    {
                        incomeDocument.SetError(MapValue<string>(reader, "error_message"),
                            (ServiceErrorType)Enum.Parse(typeof(ServiceErrorType), errorType));
                    }
                }
                result.Add(incomeDocument);
            }
            return result;
        }
    }
}
