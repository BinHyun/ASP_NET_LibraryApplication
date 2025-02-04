using System;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace LibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            string connectionString = "User Id=system;Password=1234;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=Local)))";
            string resultMessage;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // DB 연결 성공 메시지
                    using (OracleCommand cmd = new OracleCommand("SELECT SYSDATE FROM dual", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        resultMessage = $"✅ Oracle DB 연결 성공! 현재 DB 시간: {result}";
                    }
                }
                catch (Exception ex)
                {
                    resultMessage = $"❌ Oracle DB 연결 실패: {ex.Message}";
                }
            }

            return resultMessage;
        }
    }
}
