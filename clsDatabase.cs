using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            @"Data Source=VideoGameStore.db;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        public static void InitializeDatabase()
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    PRAGMA foreign_keys = ON;

                    CREATE TABLE IF NOT EXISTS Categories (
                        CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CategoryName TEXT NOT NULL,
                        CategoryDescription TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Position (
                        PositionID INTEGER PRIMARY KEY AUTOINCREMENT,
                        PositionTitle TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS SecurityQuestions (
                        QuestionID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SetID INTEGER NOT NULL,
                        QuestionPrompt TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Inventory (
                        InventoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemName TEXT NOT NULL,
                        ItemDescription TEXT NOT NULL,
                        CategoryID INTEGER NOT NULL,
                        RetailPrice NUMERIC NOT NULL,
                        Cost NUMERIC NOT NULL,
                        Quantity INTEGER NOT NULL,
                        RestockThreshold INTEGER NOT NULL,
                        Discontinued INTEGER,
                        ItemImage BLOB,
                        FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
                    );

                    CREATE TABLE IF NOT EXISTS Discounts (
                        DiscountID INTEGER PRIMARY KEY AUTOINCREMENT,
                        DiscountCode TEXT NOT NULL,
                        Description TEXT NOT NULL,
                        DiscountLevel INTEGER NOT NULL,
                        InventoryID INTEGER,
                        DiscountType INTEGER NOT NULL,
                        DiscountPercentage NUMERIC,
                        DiscountDollarAmount NUMERIC,
                        StartDate TEXT,
                        ExpirationDate TEXT NOT NULL,
                        FOREIGN KEY (InventoryID) REFERENCES Inventory(InventoryID)
                    );

                    CREATE TABLE IF NOT EXISTS Person (
                        PersonID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT,
                        NameFirst TEXT NOT NULL,
                        NameMiddle TEXT,
                        NameLast TEXT NOT NULL,
                        Suffix TEXT,
                        Address1 TEXT NOT NULL,
                        Address2 TEXT,
                        Address3 TEXT,
                        City TEXT NOT NULL,
                        Zipcode TEXT NOT NULL,
                        State TEXT NOT NULL,
                        Email TEXT,
                        PhonePrimary TEXT,
                        PhoneSecondary TEXT,
                        Image BLOB,
                        PositionID INTEGER NOT NULL,
                        PersonDeleted INTEGER,
                        FOREIGN KEY (PositionID) REFERENCES Position(PositionID)
                    );

                    CREATE TABLE IF NOT EXISTS Logon (
                        LogonID INTEGER PRIMARY KEY AUTOINCREMENT,
                        PersonID INTEGER NOT NULL,
                        LogonName TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL,
                        FirstChallengeQuestion INTEGER NOT NULL,
                        FirstChallengeAnswer TEXT NOT NULL,
                        SecondChallengeQuestion INTEGER NOT NULL,
                        SecondChallengeAnswer TEXT NOT NULL,
                        ThirdChallengeQuestion INTEGER NOT NULL,
                        ThirdChallengeAnswer TEXT NOT NULL,
                        PositionTitle TEXT NOT NULL,
                        AccountDisabled INTEGER,
                        AccountDeleted INTEGER,
                        FOREIGN KEY (PersonID) REFERENCES Person(PersonID),
                        FOREIGN KEY (FirstChallengeQuestion)
                            REFERENCES SecurityQuestions(QuestionID),
                        FOREIGN KEY (SecondChallengeQuestion)
                            REFERENCES SecurityQuestions(QuestionID),
                        FOREIGN KEY (ThirdChallengeQuestion)
                            REFERENCES SecurityQuestions(QuestionID)
                    );

                    CREATE TABLE IF NOT EXISTS Orders (
                        OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
                        DiscountID INTEGER,
                        PersonID INTEGER NOT NULL,
                        EmployeeID INTEGER,
                        OrderDate TEXT NOT NULL,
                        CC_Number TEXT,
                        ExpDate TEXT,
                        CCV TEXT,
                        FOREIGN KEY (DiscountID) REFERENCES Discounts(DiscountID),
                        FOREIGN KEY (PersonID) REFERENCES Person(PersonID),
                        FOREIGN KEY (EmployeeID) REFERENCES Person(PersonID)
                    );

                    CREATE TABLE IF NOT EXISTS OrderDetails (
                        OrderDetailsID INTEGER PRIMARY KEY AUTOINCREMENT,
                        OrderID INTEGER NOT NULL,
                        InventoryID INTEGER NOT NULL,
                        DiscountID INTEGER,
                        Quantity INTEGER NOT NULL,
                        FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
                        FOREIGN KEY (InventoryID) REFERENCES Inventory(InventoryID),
                        FOREIGN KEY (DiscountID) REFERENCES Discounts(DiscountID)
                    );
                    ";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void SeedDatabase()
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                // Don't insert duplicates if data already exists.
                using (SQLiteCommand check = new SQLiteCommand(
                    "SELECT COUNT(*) FROM Categories;", conn))
                {
                    if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                        return;
                }

                                    string sql = @"
                    INSERT INTO Categories
                    (CategoryID, CategoryName, CategoryDescription)
                    VALUES
                    (100, 'Nintendo Switch', 'Games for the Nintendo Switch'),
                    (101, 'Nintendo Switch 2', 'Games for the Nintendo Switch 2'),
                    (102, 'Nintendo 3DS', 'Games for the Nintendo 3DS');

                    INSERT INTO Position
                    (PositionID, PositionTitle)
                    VALUES
                    (1000, 'Customer'),
                    (1001, 'Employee'),
                    (1002, 'Manager');

                    INSERT INTO SecurityQuestions
                    (QuestionID, SetID, QuestionPrompt)
                    VALUES
                    (105, 1, 'What is your favorite Color?'),
                    (106, 1, 'Your favorite Toy''s name?'),
                    (107, 1, 'Your Pet''s name?'),
                    (108, 2, 'Your Home Town name?'),
                    (109, 2, 'Your mother''s first name?'),
                    (110, 2, 'Your favorite Football Team?'),
                    (111, 3, 'What is your favorite food?'),
                    (112, 3, 'Favorite place to vacation?'),
                    (113, 3, 'Name of your favorite book?');
";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
//        public static void UpdateSecurityQuestions()
//        {
//            using (SQLiteConnection conn = GetConnection())
//            {
//                conn.Open();

//                string sql = @"
//UPDATE SecurityQuestions SET QuestionPrompt = 'What is your favorite Color?' WHERE QuestionID = 105;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Your favorite Toy''s name?' WHERE QuestionID = 106;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Your Pet''s name?' WHERE QuestionID = 107;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Your Home Town name?' WHERE QuestionID = 108;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Your mother''s first name?' WHERE QuestionID = 109;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Your favorite Football Team?' WHERE QuestionID = 110;
//UPDATE SecurityQuestions SET QuestionPrompt = 'What is your favorite food?' WHERE QuestionID = 111;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Favorite place to vacation?' WHERE QuestionID = 112;
//UPDATE SecurityQuestions SET QuestionPrompt = 'Name of your favorite book?' WHERE QuestionID = 113;";

//                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
//                {
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }
        public static void ImportInventory()
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                using (SQLiteCommand check =
     new SQLiteCommand("SELECT COUNT(*) FROM Inventory;", conn))
                {
                    if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                        return;
                }

                string filePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Inventory.csv");

                foreach (string line in File.ReadAllLines(filePath))
                {
                    string[] values = Regex.Split(
                        line,
                        ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    if (values.Length < 9)
                        continue;

                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();

                        if (values[i].StartsWith("\"") &&
                            values[i].EndsWith("\""))
                        {
                            values[i] = values[i].Substring(
                                1, values[i].Length - 2);

                            values[i] = values[i].Replace("\"\"", "\"");
                        }
                    }

                    string sql = @"
                            INSERT INTO Inventory
                            (
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
                            )
                            VALUES
                            (
                                @InventoryID,
                                @ItemName,
                                @ItemDescription,
                                @CategoryID,
                                @RetailPrice,
                                @Cost,
                                @Quantity,
                                @RestockThreshold,
                                @Discontinued,
                                NULL
                            );";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@InventoryID", Convert.ToInt32(values[0]));

                        cmd.Parameters.AddWithValue(
                            "@ItemName", values[1]);

                        cmd.Parameters.AddWithValue(
                            "@ItemDescription", values[2]);

                        cmd.Parameters.AddWithValue(
                            "@CategoryID", Convert.ToInt32(values[3]));

                        cmd.Parameters.AddWithValue(
                            "@RetailPrice", Convert.ToDecimal(values[4]));

                        cmd.Parameters.AddWithValue(
                            "@Cost", Convert.ToDecimal(values[5]));

                        cmd.Parameters.AddWithValue(
                            "@Quantity", Convert.ToInt32(values[6]));

                        cmd.Parameters.AddWithValue(
                            "@RestockThreshold", Convert.ToInt32(values[7]));

                        cmd.Parameters.AddWithValue(
                            "@Discontinued", Convert.ToInt32(values[8]));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void ImportRemainingData()
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string discountsPath = Path.Combine(
      AppDomain.CurrentDomain.BaseDirectory,
      "Discount.csv");

                foreach (string line in File.ReadAllLines(discountsPath))
                {
                    string[] v = Regex.Split(
                        line,
                        ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    for (int i = 0; i < v.Length; i++)
                    {
                        v[i] = v[i].Trim().Trim('"');
                    }

                    string sql = @"
                    INSERT OR IGNORE INTO Discounts
                    (DiscountID, DiscountCode, Description, DiscountLevel,
                     InventoryID, DiscountType, DiscountPercentage,
                     DiscountDollarAmount, StartDate, ExpirationDate)
                    VALUES
                    (@ID, @Code, @Description, @Level,
                     @InventoryID, @Type, @Percentage,
                     @DollarAmount, @StartDate, @ExpirationDate);";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(v[0]));
                        cmd.Parameters.AddWithValue("@Code", v[1]);
                        cmd.Parameters.AddWithValue("@Description", v[2]);
                        cmd.Parameters.AddWithValue("@Level", Convert.ToInt32(v[3]));

                        cmd.Parameters.AddWithValue("@InventoryID",
                            v[4] == "NULL" ? (object)DBNull.Value : Convert.ToInt32(v[4]));

                        cmd.Parameters.AddWithValue("@Type", Convert.ToInt32(v[5]));

                        cmd.Parameters.AddWithValue("@Percentage",
                            v[6] == "NULL" ? (object)DBNull.Value : Convert.ToDecimal(v[6]));

                        cmd.Parameters.AddWithValue("@DollarAmount",
                            v[7] == "NULL" ? (object)DBNull.Value : Convert.ToDecimal(v[7]));

                        cmd.Parameters.AddWithValue("@StartDate",
                            v[8] == "NULL" ? (object)DBNull.Value : v[8]);

                        cmd.Parameters.AddWithValue("@ExpirationDate", v[9]);

                        cmd.ExecuteNonQuery();
                    }
                }
                string personPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "Person.csv");

                foreach (string line in File.ReadAllLines(personPath))
                {
                    string[] v = Regex.Split(
                        line,
                        ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    for (int i = 0; i < v.Length; i++)
                        v[i] = v[i].Trim().Trim('"');

                    string sql = @"
                    INSERT OR IGNORE INTO Person
                    (PersonID, Title, NameFirst, NameMiddle, NameLast, Suffix,
                     Address1, Address2, Address3, City, Zipcode, State, Email,
                     PhonePrimary, PhoneSecondary, Image, PositionID, PersonDeleted)
                    VALUES
                    (@PersonID, @Title, @First, @Middle, @Last, @Suffix,
                     @Address1, @Address2, @Address3, @City, @Zip, @State, @Email,
                     @Phone1, @Phone2, NULL, @PositionID, @Deleted);";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@PersonID", Convert.ToInt32(v[0]));
                        cmd.Parameters.AddWithValue("@Title", v[1]);
                        cmd.Parameters.AddWithValue("@First", v[2]);
                        cmd.Parameters.AddWithValue("@Middle",
                            v[3] == "NULL" ? (object)DBNull.Value : v[3]);
                        cmd.Parameters.AddWithValue("@Last", v[4]);
                        cmd.Parameters.AddWithValue("@Suffix", v[5]);
                        cmd.Parameters.AddWithValue("@Address1", v[6]);
                        cmd.Parameters.AddWithValue("@Address2", v[7]);
                        cmd.Parameters.AddWithValue("@Address3", v[8]);
                        cmd.Parameters.AddWithValue("@City", v[9]);
                        cmd.Parameters.AddWithValue("@Zip", v[10]);
                        cmd.Parameters.AddWithValue("@State", v[11]);
                        cmd.Parameters.AddWithValue("@Email", v[12]);
                        cmd.Parameters.AddWithValue("@Phone1", v[13]);
                        cmd.Parameters.AddWithValue("@Phone2", v[14]);
                        cmd.Parameters.AddWithValue("@PositionID", Convert.ToInt32(v[16]));
                        cmd.Parameters.AddWithValue("@Deleted",
                            v[17] == "NULL" ? (object)DBNull.Value : Convert.ToInt32(v[17]));

                        cmd.ExecuteNonQuery();
                    }
                }
                string logonPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "Logon.csv");

                foreach (string line in File.ReadAllLines(logonPath))
                {
                    string[] v = Regex.Split(
                        line,
                        ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    for (int i = 0; i < v.Length; i++)
                        v[i] = v[i].Trim().Trim('"');

                    string sql = @"
                INSERT OR IGNORE INTO Logon
                (LogonID, PersonID, LogonName, Password,
                 FirstChallengeQuestion, FirstChallengeAnswer,
                 SecondChallengeQuestion, SecondChallengeAnswer,
                 ThirdChallengeQuestion, ThirdChallengeAnswer,
                 PositionTitle, AccountDisabled, AccountDeleted)
                VALUES
                (@LogonID, @PersonID, @LogonName, @Password,
                 @Question1, @Answer1,
                 @Question2, @Answer2,
                 @Question3, @Answer3,
                 @PositionTitle, @Disabled, @Deleted);";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@LogonID", Convert.ToInt32(v[0]));
                        cmd.Parameters.AddWithValue("@PersonID", Convert.ToInt32(v[1]));
                        cmd.Parameters.AddWithValue("@LogonName", v[2]);
                        cmd.Parameters.AddWithValue("@Password", v[3]);

                        cmd.Parameters.AddWithValue("@Question1", Convert.ToInt32(v[4]));
                        cmd.Parameters.AddWithValue("@Answer1", v[5]);

                        cmd.Parameters.AddWithValue("@Question2", Convert.ToInt32(v[6]));
                        cmd.Parameters.AddWithValue("@Answer2", v[7]);

                        cmd.Parameters.AddWithValue("@Question3", Convert.ToInt32(v[8]));
                        cmd.Parameters.AddWithValue("@Answer3", v[9]);

                        cmd.Parameters.AddWithValue("@PositionTitle", v[10]);
                        cmd.Parameters.AddWithValue("@Disabled", Convert.ToInt32(v[11]));
                        cmd.Parameters.AddWithValue("@Deleted", Convert.ToInt32(v[12]));

                        cmd.ExecuteNonQuery();
                    }
                }
                string ordersPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "OrderID.csv");

                foreach (string line in File.ReadAllLines(ordersPath))
                {
                    string[] v = Regex.Split(
                        line,
                        ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    for (int i = 0; i < v.Length; i++)
                        v[i] = v[i].Trim().Trim('"');

                    string sql = @"
                    INSERT OR IGNORE INTO Orders
                    (OrderID, DiscountID, PersonID, EmployeeID,
                     OrderDate, CC_Number, ExpDate, CCV)
                    VALUES
                    (@OrderID, @DiscountID, @PersonID, @EmployeeID,
                     @OrderDate, @CCNumber, @ExpDate, @CCV);";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID",
                            Convert.ToInt32(v[0]));

                        cmd.Parameters.AddWithValue("@DiscountID",
                            v[1] == "NULL" ? (object)DBNull.Value : Convert.ToInt32(v[1]));

                        cmd.Parameters.AddWithValue("@PersonID",
                            Convert.ToInt32(v[2]));

                        cmd.Parameters.AddWithValue("@EmployeeID",
                            v[3] == "NULL" ? (object)DBNull.Value : Convert.ToInt32(v[3]));

                        cmd.Parameters.AddWithValue("@OrderDate", v[4]);
                        cmd.Parameters.AddWithValue("@CCNumber", v[5]);
                        cmd.Parameters.AddWithValue("@ExpDate", v[6]);
                        cmd.Parameters.AddWithValue("@CCV", v[7]);

                        cmd.ExecuteNonQuery();
                    }
                }
                string detailsPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "Details.csv");

                foreach (string line in File.ReadAllLines(detailsPath))
                {
                    string[] v = Regex.Split(
                        line,
                        ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    for (int i = 0; i < v.Length; i++)
                        v[i] = v[i].Trim().Trim('"');

                    string sql = @"
                    INSERT OR IGNORE INTO OrderDetails
                    (OrderDetailsID, OrderID, InventoryID, DiscountID, Quantity)
                    VALUES
                    (@DetailsID, @OrderID, @InventoryID, @DiscountID, @Quantity);";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@DetailsID",
                            Convert.ToInt32(v[0]));

                        cmd.Parameters.AddWithValue("@OrderID",
                            Convert.ToInt32(v[1]));

                        cmd.Parameters.AddWithValue("@InventoryID",
                            Convert.ToInt32(v[2]));

                        cmd.Parameters.AddWithValue("@DiscountID",
                            v[3] == "NULL"
                                ? (object)DBNull.Value
                                : Convert.ToInt32(v[3]));

                        cmd.Parameters.AddWithValue("@Quantity",
                            Convert.ToInt32(v[4]));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void ImportInventoryImages()
        {
            string imageFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "GameImages");

            if (!Directory.Exists(imageFolder))
                return;

            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();
                using (SQLiteCommand check = new SQLiteCommand(
    "SELECT COUNT(*) FROM Inventory WHERE ItemImage IS NOT NULL;", conn))
                {
                    if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                        return;
                }
                foreach (string file in Directory.GetFiles(imageFolder))
                {
                    string extension = Path.GetExtension(file).ToLower();

                    if (extension != ".png" && extension != ".jpg" &&
                        extension != ".jpeg")
                        continue;

                    int inventoryID;

                    if (!int.TryParse(
                        Path.GetFileNameWithoutExtension(file),
                        out inventoryID))
                        continue;

                    byte[] imageBytes = File.ReadAllBytes(file);

                    string sql = @"
                UPDATE Inventory
                SET ItemImage = @Image
                WHERE InventoryID = @InventoryID;";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@Image", DbType.Binary).Value = imageBytes;
                        cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Microsoft.Data.SQLite does not include SqlDataAdapter, so this helper
        // loads query results into the DataTable objects used by the existing forms.
        private static void FillDataTable(SQLiteCommand command, DataTable table)
        {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                table.Load(reader);
            }
        }


        public static string UserLogin(string username, string password)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT PersonID, PositionTitle
            FROM Logon
            WHERE LogonName = @User
            AND Password = @Pass
            AND (AccountDisabled = 0 OR AccountDisabled IS NULL)
            AND (AccountDeleted = 0 OR AccountDeleted IS NULL);";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                cmd.Parameters.AddWithValue("@User", username);
                cmd.Parameters.AddWithValue("@Pass", password);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        clsCurrentUser.PersonID =
                            Convert.ToInt32(reader["PersonID"]);

                        clsCurrentUser.UserName = username;
                        clsCurrentUser.IsLoggedIn = true;

                        return reader["PositionTitle"].ToString();
                    }
                }

                return "";
            }
        }
        public static List<string> GetSecurityQuestions(int setID)
        {
            List<string> questions = new List<string>();

            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT QuestionPrompt
            FROM SecurityQuestions
            WHERE SetID = @SetID
            ORDER BY QuestionID";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SetID", setID);

                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    questions.Add(dr["QuestionPrompt"].ToString());
                }
            }

            return questions;
        }
        public static clsDiscount GetDiscount(string promoCode)
        {
            using (SQLiteConnection conn = GetConnection())
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
                        FROM Discounts
                        WHERE DiscountCode = @Code
                        AND (StartDate IS NULL OR StartDate <= date('now'))
                        AND ExpirationDate >= date('now')";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Code", promoCode);

                using (SQLiteDataReader reader = cmd.ExecuteReader())

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

                
            }
            return null;
        }
        public static int GetQuestionID(string question)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT QuestionID
                    FROM SecurityQuestions
                    WHERE QuestionPrompt = @Question";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                SQLiteTransaction transaction = conn.BeginTransaction();

                try
                {
                    int positionID = 1000;
                    int personID;

                    string sqlPerson = @"
                        INSERT INTO Person
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

                        SELECT last_insert_rowid();";

                    SQLiteCommand cmdPerson = new SQLiteCommand(sqlPerson, conn, transaction);

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

                    personID = Convert.ToInt32(cmdPerson.ExecuteScalar());

                    string sqlLogon = @"
                            INSERT INTO Logon
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

                    SQLiteCommand cmdLogon = new SQLiteCommand(sqlLogon, conn, transaction);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                SQLiteTransaction transaction = conn.BeginTransaction();

                try
                {
                    int positionID = 1002;
                    int personID;

                    string sqlPerson = @"
                INSERT INTO Person
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

                SELECT last_insert_rowid();";

                    SQLiteCommand cmdPerson =
                        new SQLiteCommand(
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
                        Convert.ToInt32(cmdPerson.ExecuteScalar());

                    string sqlLogon = @"
                INSERT INTO Logon
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

                    SQLiteCommand cmdLogon =
                        new SQLiteCommand(
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
            using (SQLiteConnection conn = GetConnection())
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
            FROM Person p
            INNER JOIN Logon l
                ON p.PersonID = l.PersonID
            WHERE p.PersonID = @PersonID
            AND l.PositionTitle = 'Manager';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                SQLiteTransaction transaction =
                    conn.BeginTransaction();

                try
                {
                    string personSql = @"
                UPDATE Person
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

                    using (SQLiteCommand cmd =
                        new SQLiteCommand(
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
                UPDATE Logon
                SET LogonName = @LogonName
                WHERE PersonID = @PersonID
                AND PositionTitle = 'Manager';";

                    using (SQLiteCommand cmd =
                        new SQLiteCommand(
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE Logon
            SET AccountDisabled = @AccountDisabled
            WHERE PersonID = @PersonID
            AND PositionTitle = 'Manager';";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT PersonID
            FROM Logon
            WHERE LogonName = @Username;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
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
            FROM Discounts d
            ORDER BY d.DiscountCode;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchDiscounts(
    string searchText)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Discounts d
            WHERE
                d.DiscountCode LIKE @Search
                OR d.Description LIKE @Search
            ORDER BY d.DiscountCode;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            INSERT INTO Discounts
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE Discounts
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            DELETE FROM Discounts
            WHERE DiscountID = @DiscountID;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
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
            FROM Discounts
            WHERE DiscountID = @DiscountID;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountID",
                        discountID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT QuestionPrompt
          FROM SecurityQuestions
          WHERE QuestionID = @QuestionID";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                    return result.ToString();

                return "";
            }
        }
        public static int[] GetChallengeQuestions(string username)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT FirstChallengeQuestion,
                 SecondChallengeQuestion,
                 ThirdChallengeQuestion
          FROM Logon
          WHERE LogonName = @User";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@User", username);

                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new int[]
                        {
            Convert.ToInt32(dr["FirstChallengeQuestion"]),
            Convert.ToInt32(dr["SecondChallengeQuestion"]),
            Convert.ToInt32(dr["ThirdChallengeQuestion"])
                        };
                    }
                }

                

                return null;
            }
        }
        public static bool ResetPassword( string username,string answer1,string answer2,string answer3,string newPassword)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT FirstChallengeAnswer,
                 SecondChallengeAnswer,
                 ThirdChallengeAnswer
          FROM Logon
          WHERE LogonName = @User";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@User", username);

                

                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                        return false;

                    bool answersCorrect =
                        answer1.Equals(dr["FirstChallengeAnswer"].ToString(), StringComparison.OrdinalIgnoreCase) &&
                        answer2.Equals(dr["SecondChallengeAnswer"].ToString(), StringComparison.OrdinalIgnoreCase) &&
                        answer3.Equals(dr["ThirdChallengeAnswer"].ToString(), StringComparison.OrdinalIgnoreCase);

                    if (!answersCorrect)
                        return false;
                }

                string updateSql =
                @"UPDATE Logon
          SET Password = @Password
          WHERE LogonName = @User";

                SQLiteCommand updateCmd = new SQLiteCommand(updateSql, conn);

                updateCmd.Parameters.AddWithValue("@Password", newPassword);
                updateCmd.Parameters.AddWithValue("@User", username);

                updateCmd.ExecuteNonQuery();

                return true;
            }
        }
        public static DataTable GetInventory()
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Inventory
            WHERE Discontinued = 0
            ORDER BY ItemName;";
                // SQLite uses FillDataTable instead of SqlDataAdapter.

                DataTable dt = new DataTable();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    FillDataTable(cmd, dt);
                }

                return dt;
            }
        }
        public static DataTable GetManagerInventory()
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Inventory
            ORDER BY ItemName;";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.
                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchManagerInventory(string search,int categoryID)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Inventory
            WHERE
                (@CategoryID = 0 OR CategoryID = @CategoryID)
            AND
                ItemName LIKE @Search
            ORDER BY ItemName;";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@CategoryID",
                        categoryID);

                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + search + "%");

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.
                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchInventory(string search, int categoryID)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Inventory
            WHERE
                Discontinued = 0
            AND
                (@CategoryID = 0 OR CategoryID = @CategoryID)
            AND
                ItemName LIKE @Search
            ORDER BY ItemName;";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@Search", "%" + search + "%");

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.
                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static bool AdjustInventoryQuantity(int inventoryID, int adjustment)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE Inventory
            SET Quantity = Quantity + @Adjustment
            WHERE InventoryID = @InventoryID
            AND Quantity + @Adjustment >= 0;";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT CategoryID, CategoryName
            FROM Categories
            ORDER BY CategoryName;";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.
                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static bool AddInventoryItem(string itemName, string itemDescription,int categoryID,decimal retailPrice,decimal cost,int quantity,int restockThreshold,byte[] itemImage)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            INSERT INTO Inventory
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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

                    SQLiteParameter imageParameter = cmd.Parameters.Add("@ItemImage", DbType.Binary);

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
            using (SQLiteConnection conn = GetConnection())
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
            FROM Inventory
            WHERE InventoryID = @InventoryID;";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@InventoryID",
                        inventoryID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
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
                FROM Inventory
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
                FROM Inventory
                WHERE Discontinued = 0
                AND CategoryID = @CategoryID
                ORDER BY ItemName";
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    if (categoryID != 0)
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.
                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE Inventory
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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

                    SQLiteParameter imageParameter =
                        cmd.Parameters.Add(
                            "@ItemImage",
                            DbType.Binary);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE Inventory
            SET Discontinued = @Discontinued
            WHERE InventoryID = @InventoryID;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                SQLiteTransaction trans = conn.BeginTransaction();

                try
                {
                    string orderSql = @"
                            INSERT INTO Orders
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
                                CURRENT_TIMESTAMP,
                                @CC,
                                @Exp,
                                @CCV
                            );

                            SELECT last_insert_rowid();";

                    SQLiteCommand orderCmd =
                        new SQLiteCommand(orderSql, conn, trans);

                    orderCmd.Parameters.AddWithValue("@PersonID", personID);
                    orderCmd.Parameters.AddWithValue("@CC", ccNumber);
                    orderCmd.Parameters.AddWithValue("@Exp", expDate);
                    orderCmd.Parameters.AddWithValue("@CCV", ccv);

                    int orderID = Convert.ToInt32(orderCmd.ExecuteScalar());

                    foreach (clsCartItem item in cart.Items)
                    {
                        string detailSql = @"
                            INSERT INTO OrderDetails
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

                        SQLiteCommand detailCmd =
                            new SQLiteCommand(detailSql, conn, trans);

                        detailCmd.Parameters.AddWithValue("@OrderID", orderID);
                        detailCmd.Parameters.AddWithValue("@InventoryID", item.InventoryID);
                        detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                        detailCmd.ExecuteNonQuery();

                        string inventorySql = @"
                            UPDATE Inventory
                            SET Quantity = Quantity - @Quantity
                            WHERE InventoryID = @InventoryID";

                        SQLiteCommand inventoryCmd =
                            new SQLiteCommand(inventorySql, conn, trans);

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
            using (SQLiteConnection conn = GetConnection())
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
            FROM Person p
            INNER JOIN Logon l
                ON p.PersonID = l.PersonID
            WHERE l.PositionTitle = 'Customer'
            ORDER BY p.NameLast, p.NameFirst;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchCustomers(string searchText)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Person p
            INNER JOIN Logon l
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataRow GetCustomerByID(int personID)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Person p
            INNER JOIN Logon l
                ON p.PersonID = l.PersonID
            WHERE p.PersonID = @PersonID
            AND l.PositionTitle = 'Customer';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                SQLiteTransaction transaction =
                    conn.BeginTransaction();

                try
                {
                    string personSql = @"
                UPDATE Person
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

                    using (SQLiteCommand cmd =
                        new SQLiteCommand(
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
                UPDATE Logon
                SET LogonName = @LogonName
                WHERE PersonID = @PersonID;";

                    using (SQLiteCommand cmd =
                        new SQLiteCommand(
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
            using (SQLiteConnection conn = GetConnection())
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
            FROM Person p
            INNER JOIN Logon l
                ON p.PersonID = l.PersonID
            WHERE l.PositionTitle = 'Manager'
            ORDER BY p.NameLast, p.NameFirst;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchManagers(string searchText)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Person p
            INNER JOIN Logon l
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static bool SetCustomerDisabled(
    int personID,
    bool disabled)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE Logon
            SET AccountDisabled = @AccountDisabled
            WHERE PersonID = @PersonID;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();


    //            using (SQLiteCommand pragma = new SQLiteCommand(
    //"PRAGMA busy_timeout = 5000;", conn))
    //            {
    //                pragma.ExecuteNonQuery();
    //            }

                SQLiteTransaction transaction = conn.BeginTransaction();

                try
                {
                    object orderDiscountID = DBNull.Value;

                    if (cart.AppliedDiscount != null &&
                        cart.AppliedDiscount.DiscountLevel == 0)
                    {
                        orderDiscountID = cart.AppliedDiscount.DiscountID;
                    }

                    string sqlOrder = @"
            INSERT INTO Orders
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

            SELECT last_insert_rowid();";

                    SQLiteCommand cmdOrder =
                        new SQLiteCommand(sqlOrder, conn, transaction);

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

                    int orderID = Convert.ToInt32(cmdOrder.ExecuteScalar());

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
                INSERT INTO OrderDetails
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

                        SQLiteCommand cmdDetail =
                            new SQLiteCommand(
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
                UPDATE Inventory
                SET Quantity = Quantity - @Quantity
                WHERE InventoryID = @InventoryID
                AND Quantity >= @Quantity;";

                        SQLiteCommand cmdInventory =
                            new SQLiteCommand(
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
                catch (Exception ex)
                {
                    transaction.Rollback();

                    MessageBox.Show(
                        "Order database error:\n\n" + ex.Message);

                    return -1;
                }
            }
        }
        public static DataTable GetSalesByDateRange(
    DateTime startDate,
    DateTime endDate)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Orders o
            INNER JOIN OrderDetails od
                ON o.OrderID = od.OrderID
            INNER JOIN Inventory i
                ON od.InventoryID = i.InventoryID
            WHERE o.OrderDate >= @StartDate
            AND o.OrderDate <= @EndDate
            ORDER BY o.OrderID, od.OrderDetailsID;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@StartDate",
                        startDate.Date);

                    cmd.Parameters.AddWithValue(
                        "@EndDate",
                        endDate.Date);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataRow GetDiscountByIDForReport(
    int discountID)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Discounts
            WHERE DiscountID = @DiscountID;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@DiscountID",
                        discountID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    if (dt.Rows.Count == 0)
                        return null;

                    return dt.Rows[0];
                }
            }
        }
        public static DataTable GetInventoryReport(int reportType)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Inventory
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@ReportType",
                        reportType);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable SearchPOSCustomers(string searchText)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT DISTINCT
                p.PersonID,
                p.NameFirst,
                p.NameLast,
                p.Email,
                p.PhonePrimary
            FROM Person p
            INNER JOIN Logon l
                ON p.PersonID = l.PersonID
            LEFT JOIN Orders o
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

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + searchText + "%");

                    DataTable dt = new DataTable();

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable GetCustomerTransactions(
    int personID)
        {
            using (SQLiteConnection conn = GetConnection())
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
            FROM Orders o
            INNER JOIN OrderDetails od
                ON o.OrderID = od.OrderID
            INNER JOIN Inventory i
                ON od.InventoryID = i.InventoryID
            WHERE o.PersonID = @PersonID
            ORDER BY o.OrderDate DESC, o.OrderID DESC;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@PersonID",
                        personID);

                    DataTable dt = new DataTable();

                    FillDataTable(cmd, dt);

                    return dt;
                }
            }
        }
        public static DataTable GetAvailableDiscounts()
        {
            using (SQLiteConnection conn = GetConnection())
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
                    ) || '%'
                ELSE
                    '$' || CAST(
                        CAST(DiscountDollarAmount AS DECIMAL(10,2))
                        AS VARCHAR(20)
                    )
            END AS Amount,

                ExpirationDate

            FROM Discounts

            WHERE
                (StartDate IS NULL
                    OR StartDate <= date('now'))

                AND ExpirationDate >=
                    date('now')

            ORDER BY DiscountCode;";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(sql, conn))
                {
                    DataTable dt =
                        new DataTable();

                    FillDataTable(cmd, dt);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                SQLiteTransaction transaction =
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
                INSERT INTO Orders
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

                SELECT last_insert_rowid();";

                    SQLiteCommand cmdOrder =
                        new SQLiteCommand(
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
                        Convert.ToInt32(cmdOrder.ExecuteScalar());

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
                    INSERT INTO OrderDetails
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

                        SQLiteCommand cmdDetail =
                            new SQLiteCommand(
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
                    UPDATE Inventory
                    SET Quantity = Quantity - @Quantity
                    WHERE InventoryID = @InventoryID
                    AND Quantity >= @Quantity;";

                        SQLiteCommand cmdInventory =
                            new SQLiteCommand(
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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        NameFirst,
                        NameLast,
                        PhonePrimary
                    FROM Person
                    WHERE PersonID = @PersonID";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    DataTable dt = new DataTable();
                    // SQLite uses FillDataTable instead of SqlDataAdapter.
                    FillDataTable(cmd, dt);

                    if (dt.Rows.Count > 0)
                        return dt.Rows[0];

                    return null;
                }
            }
        }
        public static int CustomerLogin(string username, string password)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT PersonID
                    FROM Logon
                    WHERE LogonName = @User
                    AND Password = @Pass";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

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
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT Quantity
            FROM Inventory
            WHERE InventoryID = @InventoryID";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);

                return 0;
            }
        }
        
    }
    
}
