using chz.WindowsServices.DataBase;
using chz.WindowsServices.UnloadDocument.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

namespace chz.DataAccess.UnloadDocument
{
    public class DataBase : DataBase<OutcomeDocument>, IDataBase
    {
        private const string GetCanselOutcomeDocumentsSQLRequest = @"SELECT id, is_cansel, guid, request_id,document_id,content,base_64_content,
        signatyre,mdlp_ducument_status,attempt_count,ticket_link,ticket,
        service_document_status, error_message,error_type, http_status, error_response
        FROM chz_outcome_documents
        Where is_cansel = 'True' And error_type is NULL";

        private const string GetErrorOutcomeDocumentsSQLRequest = @"SELECT id, is_cansel, guid, request_id,document_id,content,base_64_content,
        signatyre,mdlp_ducument_status,attempt_count,ticket_link,ticket,service_document_status, error_message,error_type, http_status, 
        error_response FROM chz_outcome_documents Where error_type = 'HttpRequestError' Or error_type = 'CryptographyError' Or 
        (error_type='MDLPError' And http_status='Unauthorized') Or (error_type='MDLPError' And http_status='Forbidden')";


        private const string GetLinkLoadOutcomeDocumentsSQLRequest = @"SELECT id, is_cansel, guid, request_id,document_id,content,base_64_content,
        signatyre,mdlp_ducument_status,attempt_count,ticket_link,ticket,service_document_status, error_message,error_type, http_status, 
        error_response FROM chz_outcome_documents Where service_document_status = 'LoadLink' And is_cansel = 'False' And error_type is NULL";


        private const string GetNewDocumentsSQLRequest = @"SELECT id, is_cansel, guid, request_id,document_id,content,base_64_content,signatyre,
        mdlp_ducument_status,attempt_count,ticket_link,ticket,service_document_status, error_message,error_type, http_status, error_response
        FROM chz_outcome_documents Where service_document_status = 'New' And is_cansel = 'False' And error_type is NULL";

        private const string GetProcessedOutcomeDocumentsSQLRequest = @"SELECT id, is_cansel, guid, request_id,document_id,content,base_64_content,signatyre,mdlp_ducument_status,attempt_count,ticket_link,ticket,
        service_document_status, error_message,error_type, http_status, error_response FROM chz_outcome_documents Where service_document_status = 'Processed' 
        And is_cansel = 'False' And error_type is NULL";

        private const string GetUnloadDocumentsSQLRequest = @"SELECT id, is_cansel, guid, request_id,document_id,content,base_64_content,signatyre,mdlp_ducument_status,attempt_count,ticket_link,ticket,
        service_document_status, error_message,error_type, http_status, error_response FROM chz_outcome_documents 
        Where service_document_status = 'UnLoad' And is_cansel = 'False' And error_type is NULL";

        private const string UpdateOutcomeDocumentsSQLRequest = @"Update chz_outcome_documents Set
	    is_cansel=@is_cansel,guid = @guid, document_id = @document_id,request_id=@request_id,content=@content, base_64_content=@base_64_content,
	    signatyre=@signatyre, mdlp_ducument_status=@mdlp_ducument_status,attempt_count=@attempt_count,ticket_link=@ticket_link, ticket=@ticket,
	    service_document_status=@service_document_status, error_message=@error_message, error_type=@error_type, http_status=@http_status, error_response=@error_response,
        complete_date=@complete_date
        where id = @id";

        public DataBase(WindowsServices.Setting.ISettingProvider setting) : base(setting)
        {
        }

        public IEnumerable<OutcomeDocument> GetCanceledDocuments()
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetCanselOutcomeDocumentsSQLRequest;
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

        public override IEnumerable<OutcomeDocument> GetError()
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetErrorOutcomeDocumentsSQLRequest;
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

        public IEnumerable<OutcomeDocument> GetLinkLoadDocument()
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetLinkLoadOutcomeDocumentsSQLRequest;
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

        public IEnumerable<OutcomeDocument> GetNewDocuments()
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetNewDocumentsSQLRequest;
                sqlCommand.Connection = con;

                var reader = sqlCommand.ExecuteReader();

                var docs = Read(reader);
                docs.ToList().ForEach((x) => x.Guid = Guid.NewGuid().ToString());
                return docs;
            }
            catch (DataBaseException e)
            {
                throw new DataBaseException(e.Message, e);

            }
            finally
            {
                con.Close();
            }
        }

        public IEnumerable<OutcomeDocument> GetProccessedDocuments()
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetProcessedOutcomeDocumentsSQLRequest;
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

        public Signal GetSignal()
        {
            return new Signal(GetNewDocuments(), GetCanceledDocuments());
        }

        public IEnumerable<OutcomeDocument> GetUnloadDocuments()
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetUnloadDocumentsSQLRequest;
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

        public override void Update(OutcomeDocument item)
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var command = new SqlCommand(UpdateOutcomeDocumentsSQLRequest, con);

                var parameters = command.Parameters;

                parameters.AddWithValue("@id", item.Id);
                parameters.AddWithValue("@is_cansel", item.IsCansel.ToString());
                parameters.AddWithValue("@guid", item.Guid);
                parameters.AddWithValue("@document_id", Convert(item.DocumentId));
                parameters.AddWithValue("@request_id", Convert(item.RequestId));
                parameters.AddWithValue("@content", item.Content);
                parameters.AddWithValue("@base_64_content", Convert(item.Base64Content));
                parameters.AddWithValue("@signatyre", Convert(item.Signature));
                parameters.AddWithValue("@mdlp_ducument_status", item.MDLPDocumentStatus == null ? DBNull.Value : (object)item.MDLPDocumentStatus.ToString());
                parameters.AddWithValue("@attempt_count", item.AttemptCount);
                parameters.AddWithValue("@ticket_link", Convert(item.TicketLink));
                parameters.AddWithValue("@ticket", Convert(item.Ticket));
                parameters.AddWithValue("@service_document_status", item.DocumentServiceStatus.ToString());
                parameters.AddWithValue("@complete_date", Convert(item.CompleteDateTime));

                if (item.Error != null)
                {

                    parameters.AddWithValue("@error_message", item.Error.Message);

                    if (item.Error is MDLPError)
                    {
                        var error = (MDLPError)item.Error;

                        parameters.AddWithValue("@error_type", "MDLPError");
                        parameters.AddWithValue("@http_status", error.HttpStatusCode.ToString());
                        parameters.AddWithValue("@error_response", error.Response);
                    }
                    else
                    {
                        var error = (ServiceError)item.Error;

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
            finally
            {
                con.Close();
            }
        }

        private IEnumerable<OutcomeDocument> Read(SqlDataReader reader)
        {
            var result = new List<OutcomeDocument>();

            while (reader.Read())
            {
                var outcomeDocument = new OutcomeDocument();

                outcomeDocument.Id = (int)reader["id"];

                outcomeDocument.Guid = MapValue<string>(reader, "guid");
                outcomeDocument.IsCansel = bool.Parse((string)reader["is_cansel"]);

                var value = MapValue<string>(reader, "mdlp_ducument_status");

                if (value != null)
                {
                    outcomeDocument.MDLPDocumentStatus = (Common.MDLPDocumentStatus)Enum.Parse(typeof(Common.MDLPDocumentStatus), value);
                }

                outcomeDocument.AttemptCount = MapValue<int>(reader, "attempt_count");
                outcomeDocument.Base64Content = MapValue<string>(reader, "base_64_content");
                outcomeDocument.Content = MapValue<string>(reader, "content");
                outcomeDocument.DocumentId = MapValue<string>(reader, "document_id");
                outcomeDocument.DocumentServiceStatus = (DocumentServiceStatus)Enum.Parse(typeof(DocumentServiceStatus),
                    (string)reader["service_document_status"]);
                outcomeDocument.RequestId = MapValue<string>(reader, "request_id");
                outcomeDocument.TicketLink = MapValue<string>(reader, "ticket_link");
                outcomeDocument.Ticket = MapValue<string>(reader, "ticket");

                var errorType = MapValue<string>(reader, "error_type");

                if (errorType != null)
                {
                    if (errorType == "MDLPError")
                    {
                        outcomeDocument.SetError(MapValue<string>(reader, "error_message"), MapValue<string>(reader, "error_response")
                            , (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), (string)reader["http_status"]));
                    }
                    else
                    {
                        outcomeDocument.SetError(MapValue<string>(reader, "error_message"),
                            (ServiceErrorType)Enum.Parse(typeof(ServiceErrorType), errorType));
                    }
                }

                result.Add(outcomeDocument);
            }

            return result;
        }
    }
}
