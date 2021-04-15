using chz.WindowsServices.DataBase;
using chz.WindowsServices.DirectoryLoader.DataBase;
using chz.WindowsServices.Setting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace chz.DataAccess.DirectoryLoader
{
    public class DataBase : DataBase<Request>, IDataBase
    {
        private const string GetNewRequestSQLRequest = @"Select [id],[url],[request_json],[method],[answer_json],[start_after],[service_request_status],[error_type],[error_message],
        [http_status_code],[rec_created_login],[rec_created_time],[last_change_login],[last_change_time],[error_response],[guid],[complete_date]                   
        From [dbo].chz_request
        Where service_request_status = 'New' And error_type is NUll";

        private const string GetErrorRequestSQLRequest = @"Select [id],[url],[request_json],[method],[answer_json],[start_after],[service_request_status],[error_type],[error_message],
        [http_status_code],[rec_created_login],[rec_created_time],[last_change_login],[last_change_time],[error_response],[guid],[complete_date]                   
        From [dbo].chz_request
        Where error_type = 'HttpRequestError' Or error_type = 'CryptographyError' Or (error_type='MDLPError' And http_status_code='Unauthorized')
        Or (error_type='MDLPError' And http_status_code='Forbidden')";

        private const string UpdateRequestSQLRequest = @"Update [dbo].chz_request
        Set url=@url, request_json=@request_json, method=@method, answer_json=@answer_json, start_after=@start_after, 
        service_request_status=@service_request_status, error_type=@error_type, error_message=@error_message, http_status_code=@http_status_code,
        error_response=@error_response,guid=@guid,complete_date=@complete_date
        Where id=@id";

        public DataBase(ISettingProvider setting) : base(setting)
        {
        }

        public override IEnumerable<Request> GetError()
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetErrorRequestSQLRequest;
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

        public override void Update(Request item)
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();
                var command = new SqlCommand(UpdateRequestSQLRequest, con);
                var parameters = command.Parameters;

                parameters.AddWithValue("@id", item.Id);
                parameters.AddWithValue("@url", Convert(item.Url));
                parameters.AddWithValue("@request_json", Convert(item.RequestJson));
                parameters.AddWithValue("@method", Convert(item.Method.ToString()));
                parameters.AddWithValue("@answer_json", Convert(item.ResponseJson));
                parameters.AddWithValue("@start_after", Convert(item.StartAfte));
                parameters.AddWithValue("@service_request_status", Convert(item.ServiceStatus.ToString()));
                parameters.AddWithValue("@guid", Convert(item.Guid));
                parameters.AddWithValue("@complete_date", Convert(item.CompleteDateTime));

                if (item.Error != null)
                {
                    parameters.AddWithValue("@error_message", item.Error.Message);

                    if (item.Error is MDLPError)
                    {
                        var error = (MDLPError)item.Error;

                        parameters.AddWithValue("@error_type", "MDLPError");
                        parameters.AddWithValue("@http_status_code", error.HttpStatusCode.ToString());
                        parameters.AddWithValue("@error_response", error.Response);
                    }
                    else
                    {
                        var error = (ServiceError)item.Error;

                        parameters.AddWithValue("@error_type", error.ServiceErrorType.ToString());
                        parameters.AddWithValue("@http_status_code", DBNull.Value);
                        parameters.AddWithValue("@error_response", DBNull.Value);
                    }
                }
                else
                {
                    parameters.AddWithValue("@error_message", DBNull.Value);
                    parameters.AddWithValue("@error_type", DBNull.Value);
                    parameters.AddWithValue("@http_status_code", DBNull.Value);
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

        public IEnumerable<Request> GetNewRequest()
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText = GetNewRequestSQLRequest;
                sqlCommand.Connection = con;

                var reader = sqlCommand.ExecuteReader();

                var result = Read(reader);
                result.ToList().ForEach((x) => x.Guid = Guid.NewGuid().ToString());
                return result;
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

        private IEnumerable<Request> Read(SqlDataReader reader)
        {
            var result = new List<Request>();

            while (reader.Read())
            {
                var request = new Request();

                request.Id = MapValue<int>(reader, "id");
                request.Url = MapValue<string>(reader, "url");
                request.RequestJson = MapValue<string>(reader, "request_json");
                request.Method = new HttpMethod(MapValue<string>(reader, "method"));
                request.ResponseJson = MapValue<string>(reader, "answer_json");
                request.StartAfte = MapValue<DateTime?>(reader, "start_after");
                request.ServiceStatus = (ServiceStatus)Enum.Parse(typeof(ServiceStatus), MapValue<string>(reader, "service_request_status"));
                request.Guid = MapValue<string>(reader, "guid");

                var errorType = MapValue<string>(reader, "error_type");

                if (errorType != null)
                {
                    if (errorType == "MDLPError")
                    {
                        request.SetError(MapValue<string>(reader, "error_message"), MapValue<string>(reader, "error_response")
                            , (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), (string)reader["http_status_code"]));
                    }
                    else
                    {
                        request.SetError(MapValue<string>(reader, "error_message"),
                            (ServiceErrorType)Enum.Parse(typeof(ServiceErrorType), errorType));
                    }
                }

                result.Add(request);
            }
            return result;
        }
    }
}
