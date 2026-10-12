using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurlesonJLogonView
{
    public class clsDiscount
    {
        public int DiscountID { get; set; }

        public string DiscountCode { get; set; }

        public int DiscountLevel { get; set; }

        public int? InventoryID { get; set; }

        public int DiscountType { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? DiscountDollarAmount { get; set; }

    }

    internal static class clsDatabase
    {
        private static string connectionString =
            @"Server=3.130.26.194;
              Database=inew233xsu26;
              User Id=BurlesonJ26Su233x;
              Password=Whisp#0412;
              TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static string UserLogin(string username, string password)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT PersonID, PositionTitle
            FROM BurlesonJ26Su233x.Logon
            WHERE LogonName = @User
            AND Password = @Pass
            AND (AccountDisabled = 0 OR AccountDisabled IS NULL)
            AND (AccountDeleted = 0 OR AccountDeleted IS NULL);";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@User", username);
                cmd.Parameters.AddWithValue("@Pass", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    clsCurrentUser.PersonID =
                        Convert.ToInt32(reader["PersonID"]);

                    clsCurrentUser.UserName =
                        username;

                    clsCurrentUser.IsLoggedIn =
                        true;

                    return reader["PositionTitle"].ToString();
                }

                return "";
            }
        }
        public static List<string> GetSecurityQuestions(int setID)
        {
            List<string> questions = new List<string>();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT QuestionPrompt
            FROM BurlesonJ26Su233x.SecurityQuestions
            WHERE SetID = @SetID
            ORDER BY QuestionID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SetID", setID);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    questions.Add(dr["QuestionPrompt"].ToString());
                }
            }

            return questions;
        }
        public static clsDiscount GetDiscount(string promoCode)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                        SELECT
                            DiscountID,
                            DiscountCode,
                            DiscountLevel,
                            InventoryID,
                            DiscountType,
                            DiscountPercentage,
                            DiscountDollarAmount
                        FROM BurlesonJ26Su233x.Discounts
                        WHERE DiscountCode = @Code
                        AND (StartDate IS NULL OR StartDate <= CAST(GETDATE() AS date))
                        AND ExpirationDate >= CAST(GETDATE() AS date)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Code", promoCode);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    clsDiscount discount = new clsDiscount();

                    discount.DiscountID = Convert.ToInt32(reader["DiscountID"]);
                    discount.DiscountCode = reader["DiscountCode"].ToString();
                    discount.DiscountLevel = Convert.ToInt32(reader["DiscountLevel"]);
                    discount.DiscountType = Convert.ToInt32(reader["DiscountType"]);

                    if (reader["InventoryID"] != DBNull.Value)
                        discount.InventoryID = Convert.ToInt32(reader["InventoryID"]);

                    if (reader["DiscountPercentage"] != DBNull.Value)
                        discount.DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]);

                    if (reader["DiscountDollarAmount"] != DBNull.Value)
                        discount.DiscountDollarAmount = Convert.ToDecimal(reader["DiscountDollarAmount"]);

                    return discount;
                }

                return null;
            }
        }
        public static int GetQuestionID(string question)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT QuestionID
                    FROM BurlesonJ26Su233x.SecurityQuestions
                    WHERE QuestionPrompt = @Question";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Question", question);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public static bool CreateCustomerAccount(
            string title,
            string firstName,
            string lastName,
            string suffix,
            string address1,
            string address2,
            string address3,
            string city,
            string zip,
            string state,
            string email,
            string phonePrimary,
            string phoneSecondary,
            string username,
            string password,
            int q1,
            string a1,
            int q2,
            string a2,
            int q3,
            string a3)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int positionID = 1000;
                    int personID;

                    string sqlPerson = @"
                        INSERT INTO BurlesonJ26Su233x.Person
                        (
                            Title,
                            NameFirst,
                            NameLast,
                            Suffix,
                            Address1,
                            Address2,
                            Address3,
                            City,
                            Zipcode,
                            State,
                            Email,
                            PhonePrimary,
                            PhoneSecondary,
                            PositionID
                        )
                        VALUES
                        (
                            @Title,
                            @FirstName,
                            @LastName,
                            @Suffix,
                            @Address1,
                            @Address2,
                            @Address3,
                            @City,
                            @Zip,
                            @State,
                            @Email,
                            @Phone,
                            @PhoneSecondary,
                            @PositionID
                        );

                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmdPerson = new SqlCommand(sqlPerson, conn, transaction);

                    cmdPerson.Parameters.AddWithValue("@Title", title);
                    cmdPerson.Parameters.AddWithValue("@FirstName", firstName);
                    cmdPerson.Parameters.AddWithValue("@LastName", lastName);
                    cmdPerson.Parameters.AddWithValue("@Suffix", suffix);
                    cmdPerson.Parameters.AddWithValue("@Address1", address1);
                    cmdPerson.Parameters.AddWithValue("@Address2", address2);
                    cmdPerson.Parameters.AddWithValue("@Address3", address3);
                    cmdPerson.Parameters.AddWithValue("@City", city);
                    cmdPerson.Parameters.AddWithValue("@Zip", zip);
                    cmdPerson.Parameters.AddWithValue("@State", state);
                    cmdPerson.Parameters.AddWithValue("@Email", email);
                    cmdPerson.Parameters.AddWithValue("@Phone", phonePrimary);
                    cmdPerson.Parameters.AddWithValue("@PhoneSecondary", phoneSecondary);
                    cmdPerson.Parameters.AddWithValue("@PositionID", positionID);

                    personID = (int)cmdPerson.ExecuteScalar();

                    string sqlLogon = @"
                            INSERT INTO BurlesonJ26Su233x.Logon
                            (
                                PersonID,
                                LogonName,
                                Password,
                                FirstChallengeQuestion,
                                FirstChallengeAnswer,
                                SecondChallengeQuestion,
                                SecondChallengeAnswer,
                                ThirdChallengeQuestion,
                                ThirdChallengeAnswer,
                                PositionTitle,
                                AccountDisabled,
                                AccountDeleted
                            )
                            VALUES
                            (
                                @PersonID,
                                @LogonName,
                                @Password,
                                @Q1,
                                @A1,
                                @Q2,
                                @A2,
                                @Q3,
                                @A3,
                                @PositionTitle,
                                0,
                                0
                            )";

                    SqlCommand cmdLogon = new SqlCommand(sqlLogon, conn, transaction);

                    cmdLogon.Parameters.AddWithValue("@PersonID", personID);
                    cmdLogon.Parameters.AddWithValue("@LogonName", username);
                    cmdLogon.Parameters.AddWithValue("@Password", password);
                    cmdLogon.Parameters.AddWithValue("@Q1", q1);
                    cmdLogon.Parameters.AddWithValue("@A1", a1);
                    cmdLogon.Parameters.AddWithValue("@Q2", q2);
                    cmdLogon.Parameters.AddWithValue("@A2", a2);
                    cmdLogon.Parameters.AddWithValue("@Q3", q3);
                    cmdLogon.Parameters.AddWithValue("@A3", a3);
                    cmdLogon.Parameters.AddWithValue("@PositionTitle", "Customer");

                    cmdLogon.ExecuteNonQuery();

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public static bool CreateManagerAccount(
    string title,
    string firstName,
    string lastName,
    string suffix,
    string address1,
    string address2,
    string address3,
    string city,
    string zip,
    string state,
    string email,
    string phonePrimary,
    string phoneSecondary,
    string username,
    string password,
    int q1,
    string a1,
    int q2,
    string a2,
    int q3,
    string a3)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int positionID = 1002;
                    int personID;

                    string sqlPerson = @"
                INSERT INTO BurlesonJ26Su233x.Person
                (
                    Title,
                    NameFirst,
                    NameLast,
                    Suffix,
                    Address1,
                    Address2,
                    Address3,
                    City,
                    Zipcode,
                    State,
                    Email,
                    PhonePrimary,
                    PhoneSecondary,
                    PositionID
                )
                VALUES
                (
                    @Title,
                    @FirstName,
                    @LastName,
                    @Suffix,
                    @Address1,
                    @Address2,
                    @Address3,
                    @City,
                    @Zip,
                    @State,
                    @Email,
                    @Phone,
                    @PhoneSecondary,
                    @PositionID
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmdPerson =
                        new SqlCommand(
                            sqlPerson,
                            conn,
                            transaction);

                    cmdPerson.Parameters.AddWithValue(
                        "@Title", title);

                    cmdPerson.Parameters.AddWithValue(
                        "@FirstName", firstName);

                    cmdPerson.Parameters.AddWithValue(
                        "@LastName", lastName);

                    cmdPerson.Parameters.AddWithValue(
                        "@Suffix", suffix);

                    cmdPerson.Parameters.AddWithValue(
                        "@Address1", address1);

                    cmdPerson.Parameters.AddWithValue(
                        "@Address2", address2);

                    cmdPerson.Parameters.AddWithValue(
                        "@Address3", address3);

                    cmdPerson.Parameters.AddWithValue(
                        "@City", city);

                    cmdPerson.Parameters.AddWithValue(
                        "@Zip", zip);

                    cmdPerson.Parameters.AddWithValue(
                        "@State", state);

                    cmdPerson.Parameters.AddWithValue(
                        "@Email", email);

                    cmdPerson.Parameters.AddWithValue(
                        "@Phone", phonePrimary);

                    cmdPerson.Parameters.AddWithValue(
                        "@PhoneSecondary", phoneSecondary);

                    cmdPerson.Parameters.AddWithValue(
                        "@PositionID", positionID);

                    personID =
                        (int)cmdPerson.ExecuteScalar();

                    string sqlLogon = @"
                INSERT INTO BurlesonJ26Su233x.Logon
                (
                    PersonID,
                    LogonName,
                    Password,
                    FirstChallengeQuestion,
                    FirstChallengeAnswer,
                    SecondChallengeQuestion,
                    SecondChallengeAnswer,
                    ThirdChallengeQuestion,
                    ThirdChallengeAnswer,
                    PositionTitle,
                    AccountDisabled,
                    AccountDeleted
                )
                VALUES
                (
                    @PersonID,
                    @LogonName,
                    @Password,
                    @Q1,
                    @A1,
                    @Q2,
                    @A2,
                    @Q3,
                    @A3,
                    @PositionTitle,
                    0,
                    0
                )";

                    SqlCommand cmdLogon =
                        new SqlCommand(
                            sqlLogon,
                            conn,
                            transaction);

                    cmdLogon.Parameters.AddWithValue(
                        "@PersonID", personID);

                    cmdLogon.Parameters.AddWithValue(
                        "@LogonName", username);

                    cmdLogon.Parameters.AddWithValue(
                        "@Password", password);

                    cmdLogon.Parameters.AddWithValue(
                        "@Q1", q1);

                    cmdLogon.Parameters.AddWithValue(
                        "@A1", a1);

                    cmdLogon.Parameters.AddWithValue(
                        "@Q2", q2);

                    cmdLogon.Parameters.AddWithValue(
                        "@A2", a2);

                    cmdLogon.Parameters.AddWithValue(
                        "@Q3", q3);

                    cmdLogon.Parameters.AddWithValue(
                        "@A3", a3);

                    cmdLogon.Parameters.AddWithValue(
                        "@PositionTitle", "Manager");

                    cmdLogon.ExecuteNonQuery();

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public static DataRow GetManagerByID(int personID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                p.PersonID,
                p.Title,
                p.NameFirst,
                p.NameLast,
                p.Suffix,
                p.Address1,
                p.Address2,
                p.Address3,
                p.City,
                p.Zipcode,
                p.State,
                p.Email,
                p.PhonePrimary,
                p.PhoneSecondary,
                l.LogonName
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            WHERE p.PersonID = @PersonID
            AND l.PositionTitle = 'Manager';";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }

                    return dt.Rows[0];
                }
            }
        }
        public static bool UpdateManager(
    int personID,
    string title,
    string firstName,
    string lastName,
    string suffix,
    string address1,
    string address2,
    string address3,
    string city,
    string zipcode,
    string state,
    string email,
    string phonePrimary,
    string phoneSecondary,
    string username)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction =
                    conn.BeginTransaction();

                try
                {
                    string personSql = @"
                UPDATE BurlesonJ26Su233x.Person
                SET
                    Title = @Title,
                    NameFirst = @NameFirst,
                    NameLast = @NameLast,
                    Suffix = @Suffix,
                    Address1 = @Address1,
                    Address2 = @Address2,
                    Address3 = @Address3,
                    City = @City,
                    Zipcode = @Zipcode,
                    State = @State,
                    Email = @Email,
                    PhonePrimary = @PhonePrimary,
                    PhoneSecondary = @PhoneSecondary
                WHERE PersonID = @PersonID
                AND PositionID = 1002;";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            personSql,
                            conn,
                            transaction))
                    {
                        cmd.Parameters.AddWithValue(
                            "@PersonID", personID);

                        cmd.Parameters.AddWithValue(
                            "@Title", title);

                        cmd.Parameters.AddWithValue(
                            "@NameFirst", firstName);

                        cmd.Parameters.AddWithValue(
                            "@NameLast", lastName);

                        cmd.Parameters.AddWithValue(
                            "@Suffix", suffix);

                        cmd.Parameters.AddWithValue(
                            "@Address1", address1);

                        cmd.Parameters.AddWithValue(
                            "@Address2", address2);

                        cmd.Parameters.AddWithValue(
                            "@Address3", address3);

                        cmd.Parameters.AddWithValue(
                            "@City", city);

                        cmd.Parameters.AddWithValue(
                            "@Zipcode", zipcode);

                        cmd.Parameters.AddWithValue(
                            "@State", state);

                        cmd.Parameters.AddWithValue(
                            "@Email", email);

                        cmd.Parameters.AddWithValue(
                            "@PhonePrimary", phonePrimary);

                        cmd.Parameters.AddWithValue(
                            "@PhoneSecondary", phoneSecondary);

                        cmd.ExecuteNonQuery();
                    }

                    string logonSql = @"
                UPDATE BurlesonJ26Su233x.Logon
                SET LogonName = @LogonName
                WHERE PersonID = @PersonID
                AND PositionTitle = 'Manager';";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            logonSql,
                            conn,
                            transaction))
                    {
                        cmd.Parameters.AddWithValue(
                            "@PersonID",
                            personID);

                        cmd.Parameters.AddWithValue(
                            "@LogonName",
                            username);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public static bool SetManagerDisabled(
    int personID,
    bool disabled)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE BurlesonJ26Su233x.Logon
            SET AccountDisabled = @AccountDisabled
            WHERE PersonID = @PersonID
            AND PositionTitle = 'Manager';";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    cmd.Parameters.AddWithValue(
                        "@AccountDisabled",
                        disabled);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static int GetPersonIDByUsername(string username)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT PersonID
            FROM BurlesonJ26Su233x.Logon
            WHERE LogonName = @Username;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Username",
                        username);

                    object result =
                        cmd.ExecuteScalar();

                    if (result == null)
                    {
                        return -1;
                    }

                    return Convert.ToInt32(result);
                }
            }
        }
        public static DataTable GetDiscounts()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                d.DiscountID,
                d.DiscountCode,
                d.Description,
                CASE
                    WHEN d.DiscountLevel = 0 THEN 'Entire Order'
                    ELSE 'Individual Item'
                END AS AppliesTo,
                CASE
                    WHEN d.DiscountType = 0 THEN 'Percentage'
                    ELSE 'Dollar Amount'
                END AS DiscountTypeName,
                d.DiscountPercentage,
                d.DiscountDollarAmount,
                d.StartDate,
                d.ExpirationDate,
                d.InventoryID
            FROM BurlesonJ26Su233x.Discounts d
            ORDER BY d.DiscountCode;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchDiscounts(
    string searchText)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                d.DiscountID,
                d.DiscountCode,
                d.Description,
                CASE
                    WHEN d.DiscountLevel = 0 THEN 'Entire Order'
                    ELSE 'Individual Item'
                END AS AppliesTo,
                CASE
                    WHEN d.DiscountType = 0 THEN 'Percentage'
                    ELSE 'Dollar Amount'
                END AS DiscountTypeName,
                d.DiscountPercentage,
                d.DiscountDollarAmount,
                d.StartDate,
                d.ExpirationDate,
                d.InventoryID
            FROM BurlesonJ26Su233x.Discounts d
            WHERE
                d.DiscountCode LIKE @Search
                OR d.Description LIKE @Search
            ORDER BY d.DiscountCode;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static bool AddDiscount(
    string discountCode,
    string description,
    int discountLevel,
    int? inventoryID,
    int discountType,
    decimal? discountPercentage,
    decimal? discountDollarAmount,
    DateTime startDate,
    DateTime expirationDate)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            INSERT INTO BurlesonJ26Su233x.Discounts
            (
                DiscountCode,
                Description,
                DiscountLevel,
                InventoryID,
                DiscountType,
                DiscountPercentage,
                DiscountDollarAmount,
                StartDate,
                ExpirationDate
            )
            VALUES
            (
                @DiscountCode,
                @Description,
                @DiscountLevel,
                @InventoryID,
                @DiscountType,
                @DiscountPercentage,
                @DiscountDollarAmount,
                @StartDate,
                @ExpirationDate
            );";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountCode",
                        discountCode);

                    cmd.Parameters.AddWithValue(
                        "@Description",
                        description);

                    cmd.Parameters.AddWithValue(
                        "@DiscountLevel",
                        discountLevel);

                    cmd.Parameters.AddWithValue(
                        "@InventoryID",
                        inventoryID.HasValue
                            ? (object)inventoryID.Value
                            : DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@DiscountType",
                        discountType);

                    cmd.Parameters.AddWithValue(
                        "@DiscountPercentage",
                        discountPercentage.HasValue
                            ? (object)discountPercentage.Value
                            : DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@DiscountDollarAmount",
                        discountDollarAmount.HasValue
                            ? (object)discountDollarAmount.Value
                            : DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@StartDate",
                        startDate.Date);

                    cmd.Parameters.AddWithValue(
                        "@ExpirationDate",
                        expirationDate.Date);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool UpdateDiscount(
    int discountID,
    string code,
    string description,
    int discountLevel,
    int? inventoryID,
    int discountType,
    decimal? percentage,
    decimal? dollarAmount,
    DateTime startDate,
    DateTime expirationDate)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE BurlesonJ26Su233x.Discounts
            SET
                DiscountCode = @DiscountCode,
                Description = @Description,
                DiscountLevel = @DiscountLevel,
                InventoryID = @InventoryID,
                DiscountType = @DiscountType,
                DiscountPercentage = @DiscountPercentage,
                DiscountDollarAmount = @DiscountDollarAmount,
                StartDate = @StartDate,
                ExpirationDate = @ExpirationDate
            WHERE DiscountID = @DiscountID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountID",
                        discountID);

                    cmd.Parameters.AddWithValue(
                        "@DiscountCode",
                        code);

                    cmd.Parameters.AddWithValue(
                        "@Description",
                        description);

                    cmd.Parameters.AddWithValue(
                        "@DiscountLevel",
                        discountLevel);

                    cmd.Parameters.AddWithValue(
                        "@InventoryID",
                        (object)inventoryID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@DiscountType",
                        discountType);

                    cmd.Parameters.AddWithValue(
                        "@DiscountPercentage",
                        (object)percentage ?? DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@DiscountDollarAmount",
                        (object)dollarAmount ?? DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@StartDate",
                        startDate.Date);

                    cmd.Parameters.AddWithValue(
                        "@ExpirationDate",
                        expirationDate.Date);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool DeleteDiscount(int discountID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            DELETE FROM BurlesonJ26Su233x.Discounts
            WHERE DiscountID = @DiscountID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountID",
                        discountID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static DataRow GetDiscountByID(int discountID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                DiscountID,
                DiscountCode,
                Description,
                DiscountLevel,
                InventoryID,
                DiscountType,
                DiscountPercentage,
                DiscountDollarAmount,
                StartDate,
                ExpirationDate
            FROM BurlesonJ26Su233x.Discounts
            WHERE DiscountID = @DiscountID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountID",
                        discountID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }

                    return dt.Rows[0];
                }
            }
        }
        public static string GetQuestionText(int questionID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT QuestionPrompt
          FROM BurlesonJ26Su233x.SecurityQuestions
          WHERE QuestionID = @QuestionID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                    return result.ToString();

                return "";
            }
        }
        public static int[] GetChallengeQuestions(string username)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT FirstChallengeQuestion,
                 SecondChallengeQuestion,
                 ThirdChallengeQuestion
          FROM BurlesonJ26Su233x.Logon
          WHERE LogonName = @User";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@User", username);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new int[]
                    {
                Convert.ToInt32(dr["FirstChallengeQuestion"]),
                Convert.ToInt32(dr["SecondChallengeQuestion"]),
                Convert.ToInt32(dr["ThirdChallengeQuestion"])
                    };
                }

                return null;
            }
        }
        public static bool ResetPassword( string username,string answer1,string answer2,string answer3,string newPassword)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT FirstChallengeAnswer,
                 SecondChallengeAnswer,
                 ThirdChallengeAnswer
          FROM BurlesonJ26Su233x.Logon
          WHERE LogonName = @User";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@User", username);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                    return false;

                bool answersCorrect =
                    answer1.Equals(dr["FirstChallengeAnswer"].ToString(), StringComparison.OrdinalIgnoreCase) &&
                    answer2.Equals(dr["SecondChallengeAnswer"].ToString(), StringComparison.OrdinalIgnoreCase) &&
                    answer3.Equals(dr["ThirdChallengeAnswer"].ToString(), StringComparison.OrdinalIgnoreCase);

                dr.Close();

                if (!answersCorrect)
                    return false;

                string updateSql =
                @"UPDATE BurlesonJ26Su233x.Logon
          SET Password = @Password
          WHERE LogonName = @User";

                SqlCommand updateCmd = new SqlCommand(updateSql, conn);

                updateCmd.Parameters.AddWithValue("@Password", newPassword);
                updateCmd.Parameters.AddWithValue("@User", username);

                updateCmd.ExecuteNonQuery();

                return true;
            }
        }
        public static DataTable GetInventory()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
        SELECT
            InventoryID,
            ItemName,
            ItemDescription,
            RetailPrice,
            Quantity,
            ItemImage,
            CategoryID
            FROM BurlesonJ26Su233x.Inventory
            WHERE Discontinued = 0
            ORDER BY ItemName;";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
        public static DataTable GetManagerInventory()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                InventoryID,
                ItemName,
                Cost,
                RetailPrice,
                Quantity,
                RestockThreshold,
                Discontinued
            FROM BurlesonJ26Su233x.Inventory
            ORDER BY ItemName;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchManagerInventory(string search,int categoryID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                InventoryID,
                ItemName,
                Cost,
                RetailPrice,
                Quantity,
                RestockThreshold,
                Discontinued
            FROM BurlesonJ26Su233x.Inventory
            WHERE
                (@CategoryID = 0 OR CategoryID = @CategoryID)
            AND
                ItemName LIKE @Search
            ORDER BY ItemName;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@CategoryID",
                        categoryID);

                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + search + "%");

                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchInventory(string search, int categoryID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                InventoryID,
                ItemName,
                ItemDescription,
                RetailPrice,
                Quantity,
                ItemImage,
                CategoryID
            FROM BurlesonJ26Su233x.Inventory
            WHERE
                Discontinued = 0
            AND
                (@CategoryID = 0 OR CategoryID = @CategoryID)
            AND
                ItemName LIKE @Search
            ORDER BY ItemName;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@Search", "%" + search + "%");

                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static bool AdjustInventoryQuantity(int inventoryID, int adjustment)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE BurlesonJ26Su233x.Inventory
            SET Quantity = Quantity + @Adjustment
            WHERE InventoryID = @InventoryID
            AND Quantity + @Adjustment >= 0;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Adjustment",
                        adjustment);

                    cmd.Parameters.AddWithValue(
                        "@InventoryID",
                        inventoryID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static DataTable GetCategories()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT CategoryID, CategoryName
            FROM BurlesonJ26Su233x.Categories
            ORDER BY CategoryName;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static bool AddInventoryItem(string itemName, string itemDescription,int categoryID,decimal retailPrice,decimal cost,int quantity,int restockThreshold,byte[] itemImage)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            INSERT INTO BurlesonJ26Su233x.Inventory
            (
                ItemName,
                ItemDescription,
                CategoryID,
                RetailPrice,
                Cost,
                Quantity,
                RestockThreshold,
                Discontinued,
                ItemImage
            )
            VALUES
            (
                @ItemName,
                @ItemDescription,
                @CategoryID,
                @RetailPrice,
                @Cost,
                @Quantity,
                @RestockThreshold,
                0,
                @ItemImage
            );";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@ItemName", itemName);

                    cmd.Parameters.AddWithValue(
                        "@ItemDescription", itemDescription);

                    cmd.Parameters.AddWithValue(
                        "@CategoryID", categoryID);

                    cmd.Parameters.AddWithValue(
                        "@RetailPrice", retailPrice);

                    cmd.Parameters.AddWithValue(
                        "@Cost", cost);

                    cmd.Parameters.AddWithValue(
                        "@Quantity", quantity);

                    cmd.Parameters.AddWithValue(
                        "@RestockThreshold", restockThreshold);

                    SqlParameter imageParameter =cmd.Parameters.Add("@ItemImage",SqlDbType.VarBinary);

                    if (itemImage == null)
                    {
                        imageParameter.Value = DBNull.Value;
                    }
                    else
                    {
                        imageParameter.Value = itemImage;
                    }

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static DataRow GetInventoryItemByID(int inventoryID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                InventoryID,
                ItemName,
                ItemDescription,
                CategoryID,
                RetailPrice,
                Cost,
                Quantity,
                RestockThreshold,
                Discontinued,
                ItemImage
            FROM BurlesonJ26Su233x.Inventory
            WHERE InventoryID = @InventoryID;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@InventoryID",
                        inventoryID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }

                    return dt.Rows[0];
                }
            }
        }
        public static DataTable GetInventoryByCategory(int categoryID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql;

                if (categoryID == 0)
                {
                    sql = @"
                SELECT
                    InventoryID,
                    ItemName,
                    ItemDescription,
                    RetailPrice,
                    Quantity,
                    ItemImage,
                    CategoryID
                FROM BurlesonJ26Su233x.Inventory
                WHERE Discontinued = 0
                ORDER BY ItemName";
                }
                else
                {
                    sql = @"
                SELECT
                    InventoryID,
                    ItemName,
                    ItemDescription,
                    RetailPrice,
                    Quantity,
                    ItemImage,
                    CategoryID
                FROM BurlesonJ26Su233x.Inventory
                WHERE Discontinued = 0
                AND CategoryID = @CategoryID
                ORDER BY ItemName";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (categoryID != 0)
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static bool UpdateInventoryItem(
    int inventoryID,
    string itemName,
    string itemDescription,
    int categoryID,
    decimal retailPrice,
    decimal cost,
    int quantity,
    int restockThreshold,
    byte[] itemImage)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE BurlesonJ26Su233x.Inventory
            SET
                ItemName = @ItemName,
                ItemDescription = @ItemDescription,
                CategoryID = @CategoryID,
                RetailPrice = @RetailPrice,
                Cost = @Cost,
                Quantity = @Quantity,
                RestockThreshold = @RestockThreshold,
                ItemImage = @ItemImage
            WHERE InventoryID = @InventoryID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@InventoryID", inventoryID);

                    cmd.Parameters.AddWithValue(
                        "@ItemName", itemName);

                    cmd.Parameters.AddWithValue(
                        "@ItemDescription", itemDescription);

                    cmd.Parameters.AddWithValue(
                        "@CategoryID", categoryID);

                    cmd.Parameters.AddWithValue(
                        "@RetailPrice", retailPrice);

                    cmd.Parameters.AddWithValue(
                        "@Cost", cost);

                    cmd.Parameters.AddWithValue(
                        "@Quantity", quantity);

                    cmd.Parameters.AddWithValue(
                        "@RestockThreshold",
                        restockThreshold);

                    SqlParameter imageParameter =
                        cmd.Parameters.Add(
                            "@ItemImage",
                            SqlDbType.VarBinary);

                    if (itemImage == null)
                    {
                        imageParameter.Value =
                            DBNull.Value;
                    }
                    else
                    {
                        imageParameter.Value =
                            itemImage;
                    }

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool SetInventoryDiscontinued(
    int inventoryID,
    bool discontinued)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE BurlesonJ26Su233x.Inventory
            SET Discontinued = @Discontinued
            WHERE InventoryID = @InventoryID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Discontinued",
                        discontinued);

                    cmd.Parameters.AddWithValue(
                        "@InventoryID",
                        inventoryID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool Checkout(
                    clsCart cart,
                    int personID,
                    string ccNumber,
                    string expDate,
                    string ccv)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string orderSql = @"
                            INSERT INTO BurlesonJ26Su233x.Orders
                            (
                                DiscountID,
                                PersonID,
                                EmployeeID,
                                OrderDate,
                                CC_Number,
                                ExpDate,
                                CCV
                            )
                            VALUES
                            (
                                NULL,
                                @PersonID,
                                NULL,
                                GETDATE(),
                                @CC,
                                @Exp,
                                @CCV
                            );

                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand orderCmd =
                        new SqlCommand(orderSql, conn, trans);

                    orderCmd.Parameters.AddWithValue("@PersonID", personID);
                    orderCmd.Parameters.AddWithValue("@CC", ccNumber);
                    orderCmd.Parameters.AddWithValue("@Exp", expDate);
                    orderCmd.Parameters.AddWithValue("@CCV", ccv);

                    int orderID = (int)orderCmd.ExecuteScalar();

                    foreach (clsCartItem item in cart.Items)
                    {
                        string detailSql = @"
                            INSERT INTO BurlesonJ26Su233x.OrderDetails
                            (
                                OrderID,
                                InventoryID,
                                DiscountID,
                                Quantity
                            )
                            VALUES
                            (
                                @OrderID,
                                @InventoryID,
                                NULL,
                                @Quantity
                            );";

                        SqlCommand detailCmd =
                            new SqlCommand(detailSql, conn, trans);

                        detailCmd.Parameters.AddWithValue("@OrderID", orderID);
                        detailCmd.Parameters.AddWithValue("@InventoryID", item.InventoryID);
                        detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                        detailCmd.ExecuteNonQuery();

                        string inventorySql = @"
                            UPDATE BurlesonJ26Su233x.Inventory
                            SET Quantity = Quantity - @Quantity
                            WHERE InventoryID = @InventoryID";

                        SqlCommand inventoryCmd =
                            new SqlCommand(inventorySql, conn, trans);

                        inventoryCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        inventoryCmd.Parameters.AddWithValue("@InventoryID", item.InventoryID);

                        inventoryCmd.ExecuteNonQuery();
                    }

                    trans.Commit();

                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public static DataTable GetCustomers()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                p.PersonID,
                p.NameFirst,
                p.NameLast,
                p.Email,
                p.PhonePrimary,
                l.LogonName,
                l.AccountDisabled,
                l.AccountDeleted
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            WHERE l.PositionTitle = 'Customer'
            ORDER BY p.NameLast, p.NameFirst;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchCustomers(string searchText)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                p.PersonID,
                p.NameFirst,
                p.NameLast,
                p.Email,
                p.PhonePrimary,
                l.LogonName,
                l.AccountDisabled,
                l.AccountDeleted
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            WHERE l.PositionTitle = 'Customer'
            AND
            (
                p.NameFirst LIKE @Search
                OR p.NameLast LIKE @Search
                OR p.Email LIKE @Search
                OR p.PhonePrimary LIKE @Search
                OR l.LogonName LIKE @Search
                OR CAST(p.PersonID AS VARCHAR) LIKE @Search
            )
            ORDER BY p.NameLast, p.NameFirst;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataRow GetCustomerByID(int personID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                p.PersonID,
                p.Title,
                p.NameFirst,
                p.NameLast,
                p.Suffix,
                p.Address1,
                p.Address2,
                p.Address3,
                p.City,
                p.Zipcode,
                p.State,
                p.Email,
                p.PhonePrimary,
                p.PhoneSecondary,
                l.LogonName
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            WHERE p.PersonID = @PersonID
            AND l.PositionTitle = 'Customer';";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }

                    return dt.Rows[0];
                }
            }
        }
        public static bool UpdateCustomer(
    int personID,
    string title,
    string firstName,
    string lastName,
    string suffix,
    string address1,
    string address2,
    string address3,
    string city,
    string zipcode,
    string state,
    string email,
    string phonePrimary,
    string phoneSecondary,
    string username)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction =
                    conn.BeginTransaction();

                try
                {
                    string personSql = @"
                UPDATE BurlesonJ26Su233x.Person
                SET
                    Title = @Title,
                    NameFirst = @NameFirst,
                    NameLast = @NameLast,
                    Suffix = @Suffix,
                    Address1 = @Address1,
                    Address2 = @Address2,
                    Address3 = @Address3,
                    City = @City,
                    Zipcode = @Zipcode,
                    State = @State,
                    Email = @Email,
                    PhonePrimary = @PhonePrimary,
                    PhoneSecondary = @PhoneSecondary
                WHERE PersonID = @PersonID;";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            personSql,
                            conn,
                            transaction))
                    {
                        cmd.Parameters.AddWithValue(
                            "@PersonID", personID);

                        cmd.Parameters.AddWithValue(
                            "@Title",
                            string.IsNullOrWhiteSpace(title)
                                ? (object)DBNull.Value
                                : title);

                        cmd.Parameters.AddWithValue(
                            "@NameFirst", firstName);

                        cmd.Parameters.AddWithValue(
                            "@NameLast", lastName);

                        cmd.Parameters.AddWithValue(
                            "@Suffix",
                            string.IsNullOrWhiteSpace(suffix)
                                ? (object)DBNull.Value
                                : suffix);

                        cmd.Parameters.AddWithValue(
                            "@Address1", address1);

                        cmd.Parameters.AddWithValue(
                            "@Address2",
                            string.IsNullOrWhiteSpace(address2)
                                ? (object)DBNull.Value
                                : address2);

                        cmd.Parameters.AddWithValue(
                            "@Address3",
                            string.IsNullOrWhiteSpace(address3)
                                ? (object)DBNull.Value
                                : address3);

                        cmd.Parameters.AddWithValue(
                            "@City", city);

                        cmd.Parameters.AddWithValue(
                            "@Zipcode", zipcode);

                        cmd.Parameters.AddWithValue(
                            "@State", state);

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            string.IsNullOrWhiteSpace(email)
                                ? (object)DBNull.Value
                                : email);

                        cmd.Parameters.AddWithValue(
                            "@PhonePrimary",
                            string.IsNullOrWhiteSpace(phonePrimary)
                                ? (object)DBNull.Value
                                : phonePrimary);

                        cmd.Parameters.AddWithValue(
                            "@PhoneSecondary",
                            string.IsNullOrWhiteSpace(phoneSecondary)
                                ? (object)DBNull.Value
                                : phoneSecondary);

                        cmd.ExecuteNonQuery();
                    }

                    string logonSql = @"
                UPDATE BurlesonJ26Su233x.Logon
                SET LogonName = @LogonName
                WHERE PersonID = @PersonID;";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            logonSql,
                            conn,
                            transaction))
                    {
                        cmd.Parameters.AddWithValue(
                            "@PersonID", personID);

                        cmd.Parameters.AddWithValue(
                            "@LogonName", username);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public static DataTable GetManagers()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                p.PersonID,
                p.NameFirst,
                p.NameLast,
                p.Email,
                p.PhonePrimary,
                l.LogonName,
                l.AccountDisabled,
                l.AccountDeleted
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            WHERE l.PositionTitle = 'Manager'
            ORDER BY p.NameLast, p.NameFirst;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchManagers(string searchText)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                p.PersonID,
                p.NameFirst,
                p.NameLast,
                p.Email,
                p.PhonePrimary,
                l.LogonName,
                l.AccountDisabled,
                l.AccountDeleted
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            WHERE l.PositionTitle = 'Manager'
            AND
            (
                p.NameFirst LIKE @Search
                OR p.NameLast LIKE @Search
                OR p.Email LIKE @Search
                OR p.PhonePrimary LIKE @Search
                OR l.LogonName LIKE @Search
            )
            ORDER BY p.NameLast, p.NameFirst;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static bool SetCustomerDisabled(
    int personID,
    bool disabled)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE BurlesonJ26Su233x.Logon
            SET AccountDisabled = @AccountDisabled
            WHERE PersonID = @PersonID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID", personID);

                    cmd.Parameters.AddWithValue(
                        "@AccountDisabled", disabled);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static int PlaceOrder(
                 int personID,
                 clsCart cart,
                 string cardNumber,
                 string expiration,
                 string ccv)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    object orderDiscountID = DBNull.Value;

                    if (cart.AppliedDiscount != null &&
                        cart.AppliedDiscount.DiscountLevel == 0)
                    {
                        orderDiscountID = cart.AppliedDiscount.DiscountID;
                    }

                    string sqlOrder = @"
            INSERT INTO BurlesonJ26Su233x.Orders
            (
                DiscountID,
                PersonID,
                OrderDate,
                CC_Number,
                ExpDate,
                CCV
            )
            VALUES
            (
                @DiscountID,
                @PersonID,
                @OrderDate,
                @CardNumber,
                @Expiration,
                @CCV
            );

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmdOrder =
                        new SqlCommand(sqlOrder, conn, transaction);

                    cmdOrder.Parameters.AddWithValue(
                        "@DiscountID",
                        orderDiscountID);

                    cmdOrder.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    cmdOrder.Parameters.AddWithValue(
                        "@OrderDate",
                        DateTime.Today);

                    cmdOrder.Parameters.AddWithValue(
                        "@CardNumber",
                        cardNumber);

                    cmdOrder.Parameters.AddWithValue(
                        "@Expiration",
                        expiration);

                    cmdOrder.Parameters.AddWithValue(
                        "@CCV",
                        ccv);

                    int orderID = (int)cmdOrder.ExecuteScalar();

                    foreach (clsCartItem item in cart.Items)
                    {
                        object detailDiscountID = DBNull.Value;

                        if (cart.AppliedDiscount != null &&
                            cart.AppliedDiscount.DiscountLevel == 1 &&
                            cart.AppliedDiscount.InventoryID.HasValue &&
                            cart.AppliedDiscount.InventoryID.Value == item.InventoryID)
                        {
                            detailDiscountID =
                                cart.AppliedDiscount.DiscountID;
                        }

                        string sqlDetail = @"
                INSERT INTO BurlesonJ26Su233x.OrderDetails
                (
                    OrderID,
                    InventoryID,
                    DiscountID,
                    Quantity
                )
                VALUES
                (
                    @OrderID,
                    @InventoryID,
                    @DiscountID,
                    @Quantity
                );";

                        SqlCommand cmdDetail =
                            new SqlCommand(
                                sqlDetail,
                                conn,
                                transaction);

                        cmdDetail.Parameters.AddWithValue(
                            "@OrderID",
                            orderID);

                        cmdDetail.Parameters.AddWithValue(
                            "@InventoryID",
                            item.InventoryID);

                        cmdDetail.Parameters.AddWithValue(
                            "@DiscountID",
                            detailDiscountID);

                        cmdDetail.Parameters.AddWithValue(
                            "@Quantity",
                            item.Quantity);

                        cmdDetail.ExecuteNonQuery();

                        string sqlInventory = @"
                UPDATE BurlesonJ26Su233x.Inventory
                SET Quantity = Quantity - @Quantity
                WHERE InventoryID = @InventoryID
                AND Quantity >= @Quantity;";

                        SqlCommand cmdInventory =
                            new SqlCommand(
                                sqlInventory,
                                conn,
                                transaction);

                        cmdInventory.Parameters.AddWithValue(
                            "@Quantity",
                            item.Quantity);

                        cmdInventory.Parameters.AddWithValue(
                            "@InventoryID",
                            item.InventoryID);

                        int rowsUpdated =
                            cmdInventory.ExecuteNonQuery();

                        if (rowsUpdated == 0)
                        {
                            throw new InvalidOperationException(
                                "An item no longer has enough inventory.");
                        }
                    }

                    transaction.Commit();
                    return orderID;
                }
                catch
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }
        public static DataTable GetSalesByDateRange(
    DateTime startDate,
    DateTime endDate)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                o.OrderID,
                o.OrderDate,
                o.DiscountID AS OrderDiscountID,
                od.OrderDetailsID,
                od.InventoryID,
                od.DiscountID AS ItemDiscountID,
                od.Quantity,
                i.ItemName,
                i.RetailPrice
            FROM BurlesonJ26Su233x.Orders o
            INNER JOIN BurlesonJ26Su233x.OrderDetails od
                ON o.OrderID = od.OrderID
            INNER JOIN BurlesonJ26Su233x.Inventory i
                ON od.InventoryID = i.InventoryID
            WHERE o.OrderDate >= @StartDate
            AND o.OrderDate <= @EndDate
            ORDER BY o.OrderID, od.OrderDetailsID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@StartDate",
                        startDate.Date);

                    cmd.Parameters.AddWithValue(
                        "@EndDate",
                        endDate.Date);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataRow GetDiscountByIDForReport(
    int discountID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                DiscountID,
                DiscountLevel,
                InventoryID,
                DiscountType,
                DiscountPercentage,
                DiscountDollarAmount
            FROM BurlesonJ26Su233x.Discounts
            WHERE DiscountID = @DiscountID;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountID",
                        discountID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                        return null;

                    return dt.Rows[0];
                }
            }
        }
        public static DataTable GetInventoryReport(int reportType)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                InventoryID,
                ItemName,
                Cost,
                RetailPrice,
                Quantity,
                RestockThreshold,
                CASE
                    WHEN Discontinued = 1 THEN 'No'
                    ELSE 'Yes'
                END AS Available
            FROM BurlesonJ26Su233x.Inventory
            WHERE
                (@ReportType = 2)

                OR

                (@ReportType = 0
                 AND (Discontinued = 0 OR Discontinued IS NULL))

                OR

                (@ReportType = 1
                 AND (Discontinued = 0 OR Discontinued IS NULL)
                 AND Quantity <= RestockThreshold)

            ORDER BY ItemName;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@ReportType",
                        reportType);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchPOSCustomers(string searchText)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT DISTINCT
                p.PersonID,
                p.NameFirst,
                p.NameLast,
                p.Email,
                p.PhonePrimary
            FROM BurlesonJ26Su233x.Person p
            INNER JOIN BurlesonJ26Su233x.Logon l
                ON p.PersonID = l.PersonID
            LEFT JOIN BurlesonJ26Su233x.Orders o
                ON p.PersonID = o.PersonID
            WHERE
                p.PositionID = 1000
                AND (l.AccountDeleted = 0
                     OR l.AccountDeleted IS NULL)
                AND
                (
                    CAST(p.PersonID AS VARCHAR(20))
                        LIKE @Search
                    OR p.NameFirst LIKE @Search
                    OR p.NameLast LIKE @Search
                    OR p.Email LIKE @Search
                    OR p.PhonePrimary LIKE @Search
                    OR CAST(o.OrderID AS VARCHAR(20))
                        LIKE @Search
                )
            ORDER BY
                p.NameLast,
                p.NameFirst;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
        }
        public static DataTable GetCustomerTransactions(
    int personID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                o.OrderID,
                o.OrderDate,
                i.ItemName,
                od.Quantity,
                i.RetailPrice,
                CASE
                    WHEN od.DiscountID IS NOT NULL
                        THEN od.DiscountID
                    WHEN o.DiscountID IS NOT NULL
                        THEN o.DiscountID
                    ELSE NULL
                END AS DiscountID
            FROM BurlesonJ26Su233x.Orders o
            INNER JOIN BurlesonJ26Su233x.OrderDetails od
                ON o.OrderID = od.OrderID
            INNER JOIN BurlesonJ26Su233x.Inventory i
                ON od.InventoryID = i.InventoryID
            WHERE o.PersonID = @PersonID
            ORDER BY o.OrderDate DESC, o.OrderID DESC;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    DataTable dt = new DataTable();

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
        }
        public static DataTable GetAvailableDiscounts()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                DiscountCode,
                Description,

                CASE
                    WHEN DiscountLevel = 0
                        THEN 'Entire Order'
                    ELSE 'Individual Item'
                END AS AppliesTo,

                CASE
                    WHEN DiscountType = 0
                        THEN 'Percentage'
                    ELSE 'Dollar Amount'
                END AS DiscountTypeName,

                CASE
             WHEN DiscountType = 0 THEN
                 CAST(
                CAST(DiscountPercentage * 100 AS DECIMAL(10,0))
                AS VARCHAR(20)
                    ) + '%'
                ELSE
                    '$' + CAST(
                        CAST(DiscountDollarAmount AS DECIMAL(10,2))
                        AS VARCHAR(20)
                    )
            END AS Amount,

                ExpirationDate

            FROM BurlesonJ26Su233x.Discounts

            WHERE
                (StartDate IS NULL
                    OR StartDate <= CAST(GETDATE() AS DATE))

                AND ExpirationDate >=
                    CAST(GETDATE() AS DATE)

            ORDER BY DiscountCode;";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    DataTable dt =
                        new DataTable();

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
        }
        public static int PlacePOSOrder(
    int customerPersonID,
    int managerPersonID,
    clsCart cart,
    string cardNumber,
    string expiration,
    string ccv)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction =
                    conn.BeginTransaction();

                try
                {
                    object orderDiscountID = DBNull.Value;

                    // Entire-order discount goes on Orders
                    if (cart.AppliedDiscount != null &&
                        cart.AppliedDiscount.DiscountLevel == 0)
                    {
                        orderDiscountID =
                            cart.AppliedDiscount.DiscountID;
                    }

                    string sqlOrder = @"
                INSERT INTO BurlesonJ26Su233x.Orders
                (
                    DiscountID,
                    PersonID,
                    EmployeeID,
                    OrderDate,
                    CC_Number,
                    ExpDate,
                    CCV
                )
                VALUES
                (
                    @DiscountID,
                    @PersonID,
                    @EmployeeID,
                    @OrderDate,
                    @CardNumber,
                    @Expiration,
                    @CCV
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmdOrder =
                        new SqlCommand(
                            sqlOrder,
                            conn,
                            transaction);

                    cmdOrder.Parameters.AddWithValue(
                        "@DiscountID",
                        orderDiscountID);

                    cmdOrder.Parameters.AddWithValue(
                        "@PersonID",
                        customerPersonID);

                    cmdOrder.Parameters.AddWithValue(
                        "@EmployeeID",
                        managerPersonID);

                    cmdOrder.Parameters.AddWithValue(
                        "@OrderDate",
                        DateTime.Today);

                    cmdOrder.Parameters.AddWithValue(
                        "@CardNumber",
                        cardNumber);

                    cmdOrder.Parameters.AddWithValue(
                        "@Expiration",
                        expiration);

                    cmdOrder.Parameters.AddWithValue(
                        "@CCV",
                        ccv);

                    int orderID =
                        (int)cmdOrder.ExecuteScalar();

                    foreach (clsCartItem item in cart.Items)
                    {
                        object detailDiscountID = DBNull.Value;

                        // Item-specific discount goes on OrderDetails
                        if (cart.AppliedDiscount != null &&
                            cart.AppliedDiscount.DiscountLevel == 1 &&
                            cart.AppliedDiscount.InventoryID.HasValue &&
                            cart.AppliedDiscount.InventoryID.Value ==
                                item.InventoryID)
                        {
                            detailDiscountID =
                                cart.AppliedDiscount.DiscountID;
                        }

                        string sqlDetail = @"
                    INSERT INTO BurlesonJ26Su233x.OrderDetails
                    (
                        OrderID,
                        InventoryID,
                        DiscountID,
                        Quantity
                    )
                    VALUES
                    (
                        @OrderID,
                        @InventoryID,
                        @DiscountID,
                        @Quantity
                    );";

                        SqlCommand cmdDetail =
                            new SqlCommand(
                                sqlDetail,
                                conn,
                                transaction);

                        cmdDetail.Parameters.AddWithValue(
                            "@OrderID",
                            orderID);

                        cmdDetail.Parameters.AddWithValue(
                            "@InventoryID",
                            item.InventoryID);

                        cmdDetail.Parameters.AddWithValue(
                            "@DiscountID",
                            detailDiscountID);

                        cmdDetail.Parameters.AddWithValue(
                            "@Quantity",
                            item.Quantity);

                        cmdDetail.ExecuteNonQuery();

                        string sqlInventory = @"
                    UPDATE BurlesonJ26Su233x.Inventory
                    SET Quantity = Quantity - @Quantity
                    WHERE InventoryID = @InventoryID
                    AND Quantity >= @Quantity;";

                        SqlCommand cmdInventory =
                            new SqlCommand(
                                sqlInventory,
                                conn,
                                transaction);

                        cmdInventory.Parameters.AddWithValue(
                            "@Quantity",
                            item.Quantity);

                        cmdInventory.Parameters.AddWithValue(
                            "@InventoryID",
                            item.InventoryID);

                        int rowsUpdated =
                            cmdInventory.ExecuteNonQuery();

                        if (rowsUpdated == 0)
                        {
                            throw new InvalidOperationException(
                                "An item no longer has enough inventory.");
                        }
                    }

                    transaction.Commit();

                    return orderID;
                }
                catch
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }
        public static DataRow GetCustomerInfo(int personID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        NameFirst,
                        NameLast,
                        PhonePrimary
                    FROM BurlesonJ26Su233x.Person
                    WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                        return dt.Rows[0];

                    return null;
                }
            }
        }
        public static int CustomerLogin(string username, string password)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT PersonID
                    FROM BurlesonJ26Su233x.Logon
                    WHERE LogonName = @User
                    AND Password = @Pass";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@User", username);
                cmd.Parameters.AddWithValue("@Pass", password);

                object result = cmd.ExecuteScalar();

                if (result == null)
                    return -1;

                return Convert.ToInt32(result);
            }
        }
        public static int GetInventoryQuantity(int inventoryID)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT Quantity
            FROM BurlesonJ26Su233x.Inventory
            WHERE InventoryID = @InventoryID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);

                return 0;
            }
        }
        
    }
    
}
