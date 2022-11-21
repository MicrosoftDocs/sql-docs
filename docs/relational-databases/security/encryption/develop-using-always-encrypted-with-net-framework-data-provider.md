---
title: "Always Encrypted with .NET Framework Data Provider"
description: Learn how to develop .NET applications using the Always Encrypted feature for SQL Server. 
ms.custom: seo-lt-2019
ms.date: "11/15/2022"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
ms.assetid: 827e509e-3c4f-4820-aa37-cebf0f7bbf80
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Always Encrypted with the .NET Framework Data Provider for SQL Server
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides information on how to develop .NET applications using [Always Encrypted](always-encrypted-database-engine.md) or [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) and the [.NET Framework Data Provider for SQL Server](/dotnet/framework/data/adonet/sql/).

Always Encrypted allows client applications to encrypt sensitive data and never reveal the data or the encryption keys to SQL Server or Azure SQL Database. An Always Encrypted enabled driver, such as the .NET Framework Data Provider for SQL Server, achieves this by transparently encrypting and decrypting sensitive data in the client application. The driver automatically determines which query parameters correspond to sensitive database columns (protected using Always Encrypted), and encrypts the values of those parameters before passing the data to SQL Server or Azure SQL Database. Similarly, the driver transparently decrypts data retrieved from encrypted database columns in query results. For more information, see [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md).

## Prerequisites

- Configure Always Encrypted in your database. This involves provisioning Always Encrypted keys and setting up encryption for selected database columns. If you don't already have a database with Always Encrypted configured, follow the directions in [Tutorial: Getting started with Always Encrypted](always-encrypted-tutorial-getting-started.md).
- If you are using Always Encrypted with secure enclaves, see [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md) for additional prerequisites.
- Ensure .NET Framework version 4.6.1 or higher is installed on your development machine. For details, see [.NET Framework 4.6](/dotnet/framework/). You also need to ensure .NET Framework version 4.6 or higher is configured as the target .NET Framework version in your development environment. If you're using Visual Studio, please refer to [How to: Target a Version of the .NET Framework](/visualstudio/ide/how-to-target-a-version-of-the-dotnet-framework). 

> [!NOTE]
> The level of support for Always Encrypted in particular versions of .NET Framework varies. Please, see the Always Encrypted API reference section below for details.

## Enabling Always Encrypted for Application Queries
The easiest way to enable the encryption of parameters, and the decryption of query results targeting the encrypted columns, is by setting the value of the Column Encryption Setting connection string keyword to **enabled**.

The following is an example of a connection string that enables Always Encrypted:

```cs
string connectionString = "Data Source=server63; Initial Catalog=Clinic; Integrated Security=true; Column Encryption Setting=enabled";
SqlConnection connection = new SqlConnection(connectionString);
```

The following is an equivalent example using the SqlConnectionStringBuilder.ColumnEncryptionSetting Property.

```cs
SqlConnectionStringBuilder strbldr = new SqlConnectionStringBuilder();
strbldr.DataSource = "server63";
strbldr.InitialCatalog = "Clinic";
strbldr.IntegratedSecurity = true;
strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
SqlConnection connection = new SqlConnection(strbldr.ConnectionString);
```

Always Encrypted can also be enabled for individual queries. See the **Controlling performance impact of Always Encrypted** section below.
Enabling Always Encrypted isn't sufficient for encryption or decryption to succeed. You also need to make sure:
- The application has the *VIEW ANY COLUMN MASTER KEY DEFINITION* and *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION* database permissions, required to access the metadata about Always Encrypted keys in the database. For details, see [Permissions section in Always Encrypted (Database Engine)](./always-encrypted-database-engine.md#database-permissions).
- The application can access the column master key that protects the column encryption keys, encrypting the queried database columns.

## Enabling Always Encrypted with Secure Enclaves

Beginning with .NET Framework version 4.7.2, the driver supports [Always Encrypted with secure enclaves](always-encrypted-enclaves.md). 

For general information on the client driver role in enclave computations and enclave attestation, see [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md). 

To configure your application:

1. Enable Always Encrypted  for your application queries, as explained in the previous section.
2. Integrate the [Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders](https://www.nuget.org/packages/Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders) NuGet package with your application. The NuGet is a library of enclave providers, implementing the client-side logic for attestation protocols and for establishing a secure channel with a secure enclave.  
3. Update your application configuration (for example in web.config or app.config) to define the mapping between an enclave type, configured for your database, and an enclave provider. 
    1. If you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), you need to map the VBS enclave type to the Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders.HostGuardianServiceEnclaveProvider class from the NuGet package.
    2. If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] and Microsoft Azure Attestation, you need to map the SGX enclave type to the Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders.AzureAttestationEnclaveProvider class from the NuGet package.

    For detailed instructions for how to edit your application configuration, see [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](../tutorial-always-encrypted-enclaves-develop-net-framework-apps.md).

4. Set the `Enclave Attestation URL` keyword in your database connection string to an attestation URL (an attestation service endpoint). You need to obtain an attestation URL for your environment from your attestation service administrator.
   1. If you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
   2. If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] and Microsoft Azure Attestation, see [Determine the attestation URL for your attestation policy](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation).

For a step-by-step tutorial, see [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](../tutorial-always-encrypted-enclaves-develop-net-framework-apps.md)

## Retrieving and Modifying Data in Encrypted Columns

Once you enable Always Encrypted for application queries, you can use standard ADO.NET APIs (see [Retrieving and Modifying Data in ADO.NET](/dotnet/framework/data/adonet/retrieving-and-modifying-data)) or the [.NET Framework Data Provider for SQL Server](/dotnet/framework/data/adonet/sql/) APIs, defined in the [System.Data.SqlClient Namespace](/dotnet/api/system.data.sqlclient), to retrieve or modify data in encrypted database columns. Assuming your application has the required database permissions and can access the column master key, the .NET Framework Data Provider for SQL Server will encrypt any query parameters that target encrypted columns, and will decrypt data retrieved from encrypted columns returning plaintext values of .NET types, corresponding to the SQL Server data types set for the columns in the database schema.
If Always Encrypted isn't enabled, queries with parameters that target encrypted columns will fail. Queries can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns. However, the .NET Framework Data Provider for SQL Server won't attempt to decrypt any values retrieved from encrypted columns and the application will receive binary encrypted data (as byte arrays).

The below table summarizes the behavior of queries, depending on whether Always Encrypted is enabled or not:

|Query characteristic | Always Encrypted is enabled and application can access the keys and key metadata|Always Encrypted is enabled and application can't access the keys or key metadata | Always Encrypted is disabled|
|:---|:---|:---|:---|
| Queries with parameters targeting encrypted columns. | Parameter values are transparently encrypted. | Error | Error|
| Queries retrieving data from encrypted columns, without parameters targeting encrypted columns.| Results from encrypted columns are transparently decrypted. The application receives plaintext values of the .NET datatypes corresponding to the SQL Server types configured for the encrypted columns. | Error | Results from encrypted columns aren't decrypted. The application receives encrypted values as byte arrays (byte[]). 

The following examples illustrate retrieving and modifying data in encrypted columns. The examples assume the target table with the below schema. The SSN and BirthDate columns are encrypted.


```sql
CREATE TABLE [dbo].[Patients]([PatientId] [int] IDENTITY(1,1), 
 [SSN] [char](11) COLLATE Latin1_General_BIN2 
 ENCRYPTED WITH (ENCRYPTION_TYPE = DETERMINISTIC, 
 ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', 
 COLUMN_ENCRYPTION_KEY = CEK1) NOT NULL,
 [FirstName] [nvarchar](50) NULL,
 [LastName] [nvarchar](50) NULL, 
 [BirthDate] [date] 
 ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, 
 ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', 
 COLUMN_ENCRYPTION_KEY = CEK1) NOT NULL
 PRIMARY KEY CLUSTERED ([PatientId] ASC) ON [PRIMARY])
 GO
```

### Inserting Data Example

This example inserts a row into the Patients table. Note the following:
- There is nothing specific to encryption in the sample code. The .NET Framework Data Provider for SQL Server automatically detects and encrypts the *paramSSN* and *paramBirthdate* parameters that target encrypted columns. This makes encryption transparent to the application. 
- The values inserted into database columns, including the encrypted columns, are passed as [SqlParameter](/dotnet/api/system.data.sqlclient.sqlparameter) objects. While using **SqlParameter** is optional when sending values to non-encrypted columns (although, it's highly recommended because it helps prevent SQL injection), it's required for values targeting encrypted columns. If the values inserted in the SSN or BirthDate columns were passed as literals embedded in the query statement, the query would fail because the .NET Framework Data Provider for SQL Server would not be able to determine the values in the target encrypted columns, so it would not encrypt the values. As a result, the server would reject them as incompatible with the encrypted columns.
- The data type of the parameter targeting the SSN column is set to an ANSI (non-Unicode) string, which maps to the char/varchar SQL Server data type. If the type of the parameter was set to a Unicode string (String), which maps to nchar/nvarchar, the query would fail, as Always Encrypted doesn't support conversions from encrypted nchar/nvarchar values to encrypted char/varchar values. See [SQL Server Data Type Mappings](/dotnet/framework/data/adonet/sql-server-data-type-mappings) for information about the data type mappings.
- The data type of the parameter inserted into the BirthDate column is explicitly set to the target SQL Server data type using [SqlParameter.SqlDbType Property](/dotnet/api/system.data.sqlclient.sqlparameter.sqldbtype), instead of relying on the implicit mapping of .NET types to SQL Server data types applied when using [SqlParameter.DbType Property](/dotnet/api/system.data.sqlclient.sqlparameter.dbtype). By default, [DateTime Structure](/dotnet/api/system.datetime) maps to the datetime SQL Server data type. As the data type of the BirthDate column is date and Always Encrypted does not support a conversion of encrypted datetime values to encrypted date values, using the default mapping would result in an error. 

```cs
string connectionString = "Data Source=server63; Initial Catalog=Clinic; Integrated Security=true; Column Encryption Setting=enabled";
using (SqlConnection connection = new SqlConnection(strbldr.ConnectionString))
{
   using (SqlCommand cmd = connection.CreateCommand())
   {
      cmd.CommandText = @"INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (@SSN, @FirstName, @LastName, @BirthDate);";

      SqlParameter paramSSN = cmd.CreateParameter();
      paramSSN.ParameterName = @"@SSN";
      paramSSN.DbType = DbType.AnsiStringFixedLength;
      paramSSN.Direction = ParameterDirection.Input;
      paramSSN.Value = "795-73-9838";
      paramSSN.Size = 11;
      cmd.Parameters.Add(paramSSN);

      SqlParameter paramFirstName = cmd.CreateParameter();
      paramFirstName.ParameterName = @"@FirstName";
      paramFirstName.DbType = DbType.String;
      paramFirstName.Direction = ParameterDirection.Input;
      paramFirstName.Value = "Catherine";
      paramFirstName.Size = 50;
      cmd.Parameters.Add(paramFirstName);

      SqlParameter paramLastName = cmd.CreateParameter();
      paramLastName.ParameterName = @"@LastName";
      paramLastName.DbType = DbType.String;
      paramLastName.Direction = ParameterDirection.Input;
      paramLastName.Value = "Abel";
      paramLastName.Size = 50;
      cmd.Parameters.Add(paramLastName);

      SqlParameter paramBirthdate = cmd.CreateParameter();
      paramBirthdate.ParameterName = @"@BirthDate";
      paramBirthdate.SqlDbType = SqlDbType.Date;
      paramBirthdate.Direction = ParameterDirection.Input;
      paramBirthdate.Value = new DateTime(1996, 09, 10);
      cmd.Parameters.Add(paramBirthdate);

      cmd.ExecuteNonQuery();
   } 
}
```

### Retrieving Plaintext Data Example

The following example demonstrates filtering data based on encrypted values, and retrieving plaintext data from encrypted columns. Note the following:
- The value used in the WHERE clause to filter on the SSN column needs to be passed using SqlParameter, so that the .NET Framework Data Provider for SQL Server can transparently encrypt it before sending it to the database.
- All values printed by the program will be in plaintext, as the .NET Framework Data Provider for SQL Server will transparently decrypt the data retrieved from the SSN and BirthDate columns.


> [!NOTE]
> Queries can perform equality comparisons on columns if they are encrypted using deterministic encryption. For more information, see [Selecting Deterministic or Randomized Encryption](always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption).

```cs
string connectionString = "Data Source=server63; Initial Catalog=Clinic; Integrated Security=true; Column Encryption Setting=enabled";
    
using (SqlConnection connection = new SqlConnection(strbldr.ConnectionString))
 {
    using (SqlCommand cmd = connection.CreateCommand())
 {

 cmd.CommandText = @"SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN=@SSN";
 SqlParameter paramSSN = cmd.CreateParameter();
 paramSSN.ParameterName = @"@SSN";
 paramSSN.DbType = DbType.AnsiStringFixedLength;
 paramSSN.Direction = ParameterDirection.Input;
 paramSSN.Value = "795-73-9838";
 paramSSN.Size = 11;
 cmd.Parameters.Add(paramSSN);
 using (SqlDataReader reader = cmd.ExecuteReader())
 {
   if (reader.HasRows)
 {
 while (reader.Read())
 {
    Console.WriteLine(@"{0}, {1}, {2}, {3}", reader[0], reader[1], reader[2], ((DateTime)reader[3]).ToShortDateString());
 }
```

### Retrieving Encrypted Data Example

If Always Encrypted is not enabled, a query can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns.

The following example demonstrates how to retrieve binary encrypted data from encrypted columns. Note the following:

- As Always Encrypted is not enabled in the connection string, the query will return encrypted values of SSN and BirthDate as byte arrays (the program converts the values to strings).
- A query retrieving data from encrypted columns with Always Encrypted disabled can have parameters, as long as none of the parameters target an encrypted column. The above query filters by LastName, which isn't encrypted in the database. If the query filtered by SSN or BirthDate, the query would fail.


```cs
string connectionString = "Data Source=server63; Initial Catalog=Clinic; Integrated Security=true";
                
using (SqlConnection connection = new SqlConnection(connectionString))
{
   connection.Open();
   using (SqlCommand cmd = connection.CreateCommand())
   {
      cmd.CommandText = @"SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [LastName]=@LastName";
      SqlParameter paramLastName = cmd.CreateParameter();
      paramLastName.ParameterName = @"@LastName";
      paramLastName.DbType = DbType.String;
      paramLastName.Direction = ParameterDirection.Input;
      paramLastName.Value = "Abel";
      paramLastName.Size = 50;
      cmd.Parameters.Add(paramLastName);
      using (SqlDataReader reader = cmd.ExecuteReader())
      {
         if (reader.HasRows)
         {
            while (reader.Read())
         {
         Console.WriteLine(@"{0}, {1}, {2}, {3}", BitConverter.ToString((byte[])reader[0]), reader[1], reader[2], BitConverter.ToString((byte[])reader[3]));
      }
   }
}
```

### Avoiding common problems when querying encrypted columns

This section describes common categories of errors when querying encrypted columns from .NET applications and a few guidelines on how to avoid them.

### Unsupported data type conversion errors

Always Encrypted supports few conversions for encrypted data types. See [Always Encrypted](always-encrypted-database-engine.md) for a detailed list of supported type conversions. Do the following to avoid data type conversion errors:

- Set the types of parameters targeting encrypted columns, so the SQL Server data type of the parameter is either exactly the same as the type of the target column, or a conversion of the SQL Server data type of the parameter to the target type of the column is supported. You can enforce the desired mapping of .NET data types to specific SQL Server data types by using SqlParameter.SqlDbType Property.
- Verify the precision and scale of parameters targeting columns of the decimal and numeric SQL Server data types is the same as the precision and scale configured for the target column.  
- Verify the precision of parameters targeting columns of datetime2, datetimeoffset, or time SQL Server data types is not greater than the precision for the target column (in queries that modify values in the target column).  

### Errors due to passing plaintext instead of encrypted values

Any value that targets an encrypted column needs to be encrypted inside the application. An attempt to insert/modify or to filter by a plaintext value on an encrypted column will result in an error similar to this:


```cs
System.Data.SqlClient.SqlException (0x80131904): Operand type clash: varchar is incompatible with varchar(8000) encrypted with (encryption_type = 'DETERMINISTIC', encryption_algorithm_name = 'AEAD_AES_256_CBC_HMAC_SHA_256', column_encryption_key_name = 'CEK_Auto1', column_encryption_key_database_name = 'Clinic') collation_name = 'SQL_Latin1_General_CP1_CI_AS'
```

To prevent such errors, make sure:
- Always Encrypted is enabled for application queries targeting encrypted columns (for the connection string or in the [SqlCommand](/dotnet/api/system.data.sqlclient.sqlcommand) object for a specific query).
- You use SqlParameter to send data targeting encrypted columns. The following example shows a query that incorrectly filters by a literal/constant on an encrypted column (SSN)(instead of passing the literal inside a SqlParameter object). 


```cs
using (SqlCommand cmd = connection.CreateCommand())
{
   cmd.CommandText = @"SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN='795-73-9838'";
cmd.ExecuteNonQuery();
}
```

## Working with Column Master Key Stores

To encrypt a parameter value or to decrypt data in query results, the .NET Framework Data Provider for SQL Server needs to obtain a column encryption key that is configured for the target column. Column encryption keys are stored in the encrypted form in the database metadata. Each column encryption key has a corresponding column master key that was used to encrypt the column encryption key. The database metadata does not store the column master keys - it only contains the information about a key store containing a particular column master key and the location of the key in the key store.

To obtain a plaintext value of a column encryption key, the .NET Framework Data Provider for SQL Server first obtains the metadata about both the column encryption key and its corresponding column master key, and then it uses the information in the metadata to contact the key store, containing the column master key, and to decrypt the encrypted column encryption key. The .NET Framework Data Provider for SQL Server communicates with a key store using a column master key store provider - which is an instance of a class derived from [SqlColumnEncryptionKeyStoreProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptionkeystoreprovider).


The process to obtain a column encryption key:

1.	If Always Encrypted is enabled for a query, the .NET Framework Data Provider for SQL Server transparently calls **sys.sp_describe_parameter_encryption** to retrieve encryption metadata for parameters targeting encrypted columns, if the query has parameters. For encrypted data contained in the results of a query, SQL Server automatically attaches encryption metadata. The information about the column master key includes:
    - The name of a key store provider that encapsulates a key store containing the column master key. 
    - The key path that specifies the location of the column master key in the key store.
    
    The information about the column encryption key includes:

    - The encrypted value of a column encryption key.
    - The name of the algorithm that was used to encrypt the column encryption key.
2.	The .NET Framework Data Provider for SQL Server uses the name of the column master key store provider to look up the provider object (an instance of a class derived from SqlColumnEncryptionKeyStoreProvider Class) in an internal data structure.
3.	To decrypt the column encryption key, the .NET Framework Data Provider for SQL Server calls SqlColumnEncryptionKeyStoreProvider.DecryptColumnEncryptionKey Method, passing the column master key path, the encrypted value of the column encryption key and the name of the encryption algorithm, used to produce the encrypted column encryption key.




### Using built-in column master key store providers

The .NET Framework Data Provider for SQL Server comes with the following built-in column master key store providers, which are pre-registered with the specific provider names (used to look up the provider).


| Class | Description | Provider (lookup) name |
|:---|:---|:---|
|SqlColumnEncryptionCertificateStoreProvider Class| A provider for Windows Certificate Store. | MSSQL_CERTIFICATE_STORE |
|[SqlColumnEncryptionCngProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncngprovider) <br><br>**Note:** this provider is available in .NET Framework 4.6.1 and later versions. |A provider for a key store that supports [Microsoft Cryptography API: Next Generation (CNG) API](/windows/win32/seccng/cng-portal). Typically, a store of this type is a hardware security module - a physical device that safeguards and manages digital keys and provides crypto-processing.  | MSSQL_CNG_STORE|
| [SqlColumnEncryptionCspProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncspprovider)<br><br>**Note:** this provider is available in .NET Framework 4.6.1 or later versions.| A provider for a key store that supports [Microsoft Cryptography API (CAPI)](/previous-versions/visualstudio/aa266944(v=vs.60)). Typically, a store of this type is a hardware security module - a physical device that safeguards and manages digital keys and provides crypto-processing.| MSSQL_CSP_PROVIDER |
  
You do not need to make any application code changes to use these providers but note the following:

- You (or your DBA) need to make sure the provider name, configured in the column master key metadata, is correct and the column master key path complies with the key path format that is valid for a given provider. It is recommended that you configure the keys using tools such as SQL Server Management Studio, which automatically generates the valid provider names and key paths when issuing the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement. For more information, see [Configuring Always Encrypted using SQL Server Management Studio](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md) and [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md).
- Ensure your application can access the key in the key store. This may involve granting your application access to the key and/or the key store, depending on the key store, or performing other key store-specific configuration steps. For example, to access a key store implementing CNG or CAPI (e.g. a hardware security module), you need to make sure a library implementing CNG or CAPI for your store is installed on your application machine. For details, see [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md).

### Using Azure Key Vault provider

Azure Key Vault is a convenient option to store and manage column master keys for Always Encrypted (especially if your applications are hosted in Azure). The .NET Framework Data Provider for SQL Server does not include a built-in column master key store provider for Azure Key Vault, but it is available as a NuGet package, that you can easily integrate with your application. For details, see:

- [Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider](https://www.nuget.org/packages/Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider/)
- [Always Encrypted - Protect sensitive data in SQL Database with data encryption and store your encryption keys in the Azure Key Vault](/azure/azure-sql/database/always-encrypted-azure-key-vault-configure)

### Implementing a custom column master key store provider

If you want to store column master keys in a key store that is not supported by an existing provider, you can implement a custom provider by extending the [SqlColumnEncryptionCngProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncngprovider) and registering the provider using the [SqlConnection.RegisterColumnEncryptionKeyStoreProviders](/dotnet/api/system.data.sqlclient.sqlconnection.registercolumnencryptionkeystoreproviders) method.


```cs
public class MyCustomKeyStoreProvider : SqlColumnEncryptionKeyStoreProvider
    {
        public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
        {
            // Logic for encrypting a column encrypted key.
        }
        public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] EncryptedColumnEncryptionKey)
        {
            // Logic for decrypting a column encrypted key.
        }
    }  
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary\<string, SqlColumnEncryptionKeyStoreProvider> providers =
               new Dictionary\<string, SqlColumnEncryptionKeyStoreProvider>();
            providers.Add("MY_CUSTOM_STORE", customProvider);
            SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
            providers.Add(SqlColumnEncryptionCertificateStoreProvider.ProviderName, customProvider);
            SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers); 
	   // ...
        }

    }
```
 
### Using column master key store providers for programmatic key provisioning

When accessing encrypted columns, the .NET Framework Data Provider for SQL Server transparently finds and calls the right column master key store provider to decrypt column encryption keys. Typically, your normal application code does not directly call column master key store providers. You may, however, instantiate and call a provider explicitly to programmatically provision and manage Always Encrypted keys: to generate an encrypted column encryption key and decrypt a column encryption key (e.g. as part column master key rotation). For more information, see [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md).
Implementing your own key management tools may be required only if you use a custom key store provider. When using keys stored in keys stores, for which built-in providers exist, and or in  Azure Key Vault, you can use existing tools, such as SQL Server Management Studio or PowerShell, to manage and provision keys.
The below example, illustrates generating a column encryption key and using [SqlColumnEncryptionCertificateStoreProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncertificatestoreprovider) to encrypt the key with a certificate.


```cs
using System.Security.Cryptography;
static void Main(string[] args)
{
    byte[] EncryptedColumnEncryptionKey = GetEncryptedColumnEncryptonKey(); 
    Console.WriteLine("0x" + BitConverter.ToString(EncryptedColumnEncryptionKey).Replace("-", "")); 
    Console.ReadKey();
}

static byte[]  GetEncryptedColumnEncryptonKey()
{
    int cekLength = 32;
    String certificateStoreLocation = "CurrentUser";
    String certificateThumbprint = "698C7F8E21B2158E9AED4978ADB147CF66574180";
    // Generate the plaintext column encryption key.
    byte[] columnEncryptionKey = new byte[cekLength];
    RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    rngCsp.GetBytes(columnEncryptionKey);

    // Encrypt the column encryption key with a certificate.
    string keyPath = String.Format(@"{0}/My/{1}", certificateStoreLocation, certificateThumbprint);
    SqlColumnEncryptionCertificateStoreProvider provider = new SqlColumnEncryptionCertificateStoreProvider();
    return provider.EncryptColumnEncryptionKey(keyPath, @"RSA_OAEP", columnEncryptionKey); 
}
```


## Controlling Performance Impact of Always Encrypted

Because Always Encrypted is a client-side encryption technology, most of performance overheads are observed on the client side, not in the database. Apart from the cost of encryption and decryption operations, the other sources of performance overheads on the client side are:
- Additional round trips to the database to retrieve metadata for query parameters.
- Calls to a column master key store to access a column master key.

This section describes the built-in performance optimizations in .NET Framework Provider for SQL Server and how you can control the impact of the above two factors on performance.

### Controlling round trips to retrieve metadata for query parameters

If Always Encrypted is enabled for a connection, by default, the .NET Framework Data Provider for SQL Server will call [sys.sp_describe_parameter_encryption](../../system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) for each parameterized query, passing the query statement (without any parameter values) to SQL Server. **sys.sp_describe_parameter_encryption** analyzes the query statement to find out if any parameters need to be encrypted, and if so, for each such, it returns the encryption-related information that will allow the .NET Framework Data Provider for SQL Server to encrypt parameter values. The above behavior ensures a high level of transparency to the client application. The application (and the application developer) doesn't need to be aware of which queries access encrypted columns, as long as the values targeting encrypted columns are passed to the .NET Framework Data Provider for SQL Server in SqlParameter objects.


### Query metadata caching

In .NET Framework 4.6.2 and later, the .NET Framework Data Provider for SQL Server caches the results of **sys.sp_describe_parameter_encryption** for each query statement. Consequently, if the same query statement is executed multiple times, the driver calls **sys.sp_describe_parameter_encryption** only once. Encryption metadata caching for query statements substantially reduces the performance cost of fetching metadata from the database. Caching is enabled by default. You can disable parameter metadata caching by setting the  [SqlConnection.ColumnEncryptionQueryMetadataCacheEnabled Property](/dotnet/api/system.data.sqlclient.sqlconnection.columnencryptionquerymetadatacacheenabled) to false, but doing so isn't recommended except in rare cases like the one described below:

Consider a database that has two different schemas: s1 and s2. Each schema contains a table with the same name: t. The definitions of the s1.t and s2.t tables are identical, except encryption-related properties: A column, named c, in s1.t is not encrypted, and it is encrypted in s2.t. The database has two users: u1 and u2. The default schema for the u1 users it s1. The default schema for u2 is s2. A .NET application opens two connections to the database, impersonating the u1 user on one connection, and the u2 user on another connection. The application sends a query with a parameter targeting the c column over the connection for user u1 (the query does not specify the schema, so the default user scheme is assumed). Next, the application sends the same query over the connection for the u2 user. If query metadata caching is enabled, after the first query, the cache will be populated with metadata indicating the c column, the query parameter targets, is not encrypted. As the second query has the identical query statement, the information stored in the cache will be used. As a result, the driver will send the query without encrypting the parameter (which is incorrect, as the target column, s2.t.c, is encrypted), leaking the plaintext value of the parameter to the server. The server will detect that incompatibility and it will force the driver to refresh the cache, so the application will transparently resend the query with the correctly encrypted parameter value. In such a case, caching should be disabled to prevent leaking sensitive values to the server. 




### Setting Always Encrypted at the query level

To control performance impact of retrieving encryption metadata for parameterized queries, you can enable Always Encrypted for individual queries, instead of setting it up for the connection. This way, you can ensure that **sys.sp_describe_parameter_encryption** is invoked only for queries that you know have parameters targeting encrypted columns. Note, however, that by doing so, you reduce transparency of encryption: if you change encryption properties of your database columns, you may need to change the code of your application to align it with the schema changes.

> [!NOTE]
> Setting Always Encrypted at the query level has limited performance benefits in .NET 4.6.2 and later versions, which implement parameter encryption metadata caching.

To control the Always Encrypted behavior of individual queries, you need to use this constructor of
 [SqlCommand](/dotnet/api/system.data.sqlclient.sqlcommand) and [SqlCommandColumnEncryptionSetting](/dotnet/api/system.data.sqlclient.sqlcommandcolumnencryptionsetting). Here are some useful guidelines:
- If most queries a client application sends over a database connection access encrypted columns:
    - Set the **Column Encryption Setting** connection string keyword to *Enabled*.
    - Set **SqlCommandColumnEncryptionSetting.Disabled** for individual queries that do not access any encrypted columns. This will disable both calling sys.sp_describe_parameter_encryption as well as an attempt to decrypt any values in the result set.
    - Set **SqlCommandColumnEncryptionSetting.ResultSet** for individual queries that do not have any parameters requiring encryption, but retrieve data from encrypted columns. This will disable calling sys.sp_describe_parameter_encryption and parameter encryption. The query will be able to decrypt the results from encryption columns.
- If most queries a client application sends over a database connection do not access encrypted columns:
    - Set the **Column Encryption Setting** connection string keyword to **Disabled**.
    - Set **SqlCommandColumnEncryptionSetting.Enabled** for individual queries that have any parameters that need to be encrypted. This will enable both calling sys.sp_describe_parameter_encryption as well as the decryption of any query results retrieved from encrypted columns.
    - Set **SqlCommandColumnEncryptionSetting.ResultSet** for queries that do not have any parameters requiring encryption, but retrieve data from encrypted columns. This will disable calling sys.sp_describe_parameter_encryption and parameter encryption. The query will be able to decrypt the results from encryption columns.

In the below example, Always Encrypted is disabled for the database connection. The query, the application issues, has a parameter that targets the LastName column that is not encrypted. The query retrieves data from the SSN and BirthDate columns that are both encrypted. In such a case, calling sys.sp_describe_parameter_encryption to retrieve encryption metadata is not required. However, the decryption of the query results needs to be enabled, so that the application can receive plaintext values from the two encrypted columns. SqlCommandColumnEncryptionSetting.ResultSet setting is used to ensure that.



```cs
string connectionString = "Data Source=server63; Initial Catalog=Clinic; Integrated Security=true";
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    using (SqlCommand cmd = new SqlCommand(@"SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [LastName]=@LastName",
connection, null, SqlCommandColumnEncryptionSetting.ResultSetOnly))
    {
        SqlParameter paramLastName = cmd.CreateParameter();
        paramLastName.ParameterName = @"@LastName";
        paramLastName.DbType = DbType.String;
        paramLastName.Direction = ParameterDirection.Input;
        paramLastName.Value = "Abel";
        paramLastName.Size = 50;
        cmd.Parameters.Add(paramLastName);
        using (SqlDataReader reader = cmd.ExecuteReader())
            {
               if (reader.HasRows)
               {
                  while (reader.Read())
                  {
                     Console.WriteLine(@"{0}, {1}, {2}, {3}", reader[0], reader[1], reader[2], ((DateTime)reader[3]).ToShortDateString());
                  }
               }
            }
  } 
}
```


### Column encryption key caching

To reduce the number of calls to a column master key store to decrypt column encryption keys, the .NET Framework Data Provider for SQL Server caches the plaintext column encryption keys in memory. After receiving the encrypted column encryption key value from database metadata, the driver first tries to find the plaintext column encryption key, corresponding to the encrypted key value. The driver calls the key store containing the column master key, only if it cannot find the encrypted column encryption key value in the cache.

> [!NOTE]
> In .NET Framework 4.6 and 4.6.1, the column encryption key entries in the cache are never evicted. This means that for a given encrypted column encryption key, the driver contacts the key store only once during the life time of the application.

In .NET Framework 4.6.2 and later, the cache entries are evicted after a configurable time-to-live interval for security reasons. The default time-to-live value is 2 hours. If you have stricter security requirements about how long column encryption keys can be cached in plaintext in the application, you can change it using the [SqlConnection.ColumnEncryptionKeyCacheTtl Property](/dotnet/api/system.data.sqlclient.sqlconnection.columnencryptionkeycachettl). 


## Enabling Additional Protection for a Compromised SQL Server

By default, the *.NET Framework Data Provider for SQL Server* relies on the database system (SQL Server or Azure SQL Database) to provide metadata about which columns in the database are encrypted and how. The encryption metadata enables the .NET Framework Data Provider for SQL Server to encrypt query parameters and decrypt query results without any input from the application, which greatly reduces the number of changes required in the application. However, if the SQL Server process gets compromised and an attacker tampers with the metadata SQL Server sends to the .NET Framework Data Provider for SQL Server, the attacker might be able to steal sensitive information. This section describes APIs that help provide an additional level of protection against this type of attack, at the price of reduced transparency. 

### Forcing Parameter Encryption 

Before the .NET Framework Data Provider for SQL Server sends a parameterized query to SQL Server, it asks SQL Server (by calling [sys.sp_describe_parameter_encryption](../../../relational-databases/system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md)) to analyze the query statement and provide information about which parameters in the query should be encrypted. A compromised SQL Server instance could mislead the .NET Framework Data Provider for SQL Server by sending the metadata indicating the parameter does not target an encrypted column, despite the fact the column is encrypted in the database. As a result, the .NET Framework Data Provider for SQL Server would not encrypt the parameter value and it would send it as plaintext to the compromised SQL Server instance.

To prevent such an attack, an application can set the [SqlParameter.ForceColumnEncryption Property](/dotnet/api/system.data.sqlclient.sqlparameter.forcecolumnencryption) for the parameter to true. This will cause the .NET Framework Data Provider for SQL Server to throw an exception, if the metadata, it has received from the server, indicates the parameter does not need to be encrypted.

Although using the **SqlParameter.ForceColumnEncryption Property** helps improve security, it also reduces the transparency of encryption to the client application. If you update the database schema to change the set of encrypted columns, you might need to make application changes as well.

The following code sample illustrates using the **SqlParameter.ForceColumnEncryption Property** to prevent social security numbers to be sent in plaintext to the database. 



```cs
SqlCommand cmd = _sqlconn.CreateCommand(); 

// Use parameterized queries to access Always Encrypted data. 
 
cmd.CommandText = @"SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [SSN] = @SSN;"; 

SqlParameter paramSSN = cmd.CreateParameter(); 
paramSSN.ParameterName = @"@SSN"; 
paramSSN.DbType = DbType.AnsiStringFixedLength; 
paramSSN.Direction = ParameterDirection.Input; 
paramSSN.Value = ssn; 
paramSSN.Size = 11; 
paramSSN.ForceColumnEncryption = true; 
cmd.Parameters.Add(paramSSN); 

SqlDataReader reader = cmd.ExecuteReader();
```
 

### Configuring Trusted Column Master Key Paths 

The encryption metadata, SQL Server returns for query parameters targeting encrypted columns and for the results retrieved from encryption columns, includes the key path of the column master key that identifies the key store and the location of the key in the key store. If the SQL Server instance is compromised, it could send the key path directing the .NET Framework Data Provider for SQL Server to the location controlled by an attacker. This may lead to leaking key store credentials, in the case of the key store that requires the application to authenticate. 

To prevent such attacks, the application can specify the list of trusted key paths for a given server using the [SqlConnection.ColumnEncryptionTrustedMasterKeyPaths Property](/dotnet/api/system.data.sqlclient.sqlconnection.columnencryptiontrustedmasterkeypaths). If the.NET Framework Data Provider for SQL Server receives a key path outside of the trusted key path list, it will throw an exception. 

Although setting trusted key paths improves security of your application, you will need to change the code or/and the configuration of the application, whenever you rotate your column master key (whenever the column master key path changes). 

The following example shows how to configure trusted column master key paths:


```cs
// Configure trusted key paths to protect against fake key paths sent by a compromised SQL Server instance 
// First, create a list of trusted key paths for your server 
List<string> trustedKeyPathList = new List<string>(); 
trustedKeyPathList.Add("CurrentUser/my/425CFBB9DDDD081BB0061534CE6AB06CB5283F5Ea"); 

// Register the trusted key path list for your server 

SqlConnection.ColumnEncryptionTrustedMasterKeyPaths.Add(serverName, trustedKeyPathList);
```
 



## Copying Encrypted Data using SqlBulkCopy

With SqlBulkCopy, you can copy data, which is already encrypted and stored in one table, to another table, without decrypting the data. To do that:

- Make sure the encryption configuration of the target table is identical to the configuration of the source table. In particular, both tables must have the same columns encrypted, and the columns must be encrypted using the same encryption types and the same encryption keys. Note: if any of the target columns is encrypted differently than its corresponding source column, you will not be able to decrypt the data in the target table after the copy operation. The data will be corrupted.
- Configure both database connections, to the source table and to the target table, without Always Encrypted enabled. 
- Set the AllowEncryptedValueModifications option (see [SqlBulkCopyOptions](/dotnet/api/system.data.sqlclient.sqlbulkcopyoptions)). 
Note: Use caution when specifying AllowEncryptedValueModifications as this may lead to corrupting the database because the .NET Framework Data Provider for SQL Server does not check if the data is indeed encrypted, or if it is correctly encrypted using the same encryption type, algorithm and key as the target column.

The AllowEncryptedValueModifications option is available in .NET Framework 4.6.1 and later versions.

Here is an example that copies data from one table to another. The SSN and BirthDate columns are assumed to be encrypted.
        

```cs
static public void CopyTablesUsingBulk(string sourceTable, string targetTable)
{
   string sourceConnectionString = "Data Source=server63; Initial Catalog=Clinic; Integrated Security=true";
   string targetConnectionString = "Data Source= server64; Initial Catalog=Clinic; Integrated Security=true";
   using (SqlConnection connSource = new SqlConnection(sourceConnectionString))
   {
      connSource.Open();
      using (SqlCommand cmd = new SqlCommand(string.Format("SELECT [PatientID], [SSN], [FirstName], [LastName], [BirthDate] FROM {0}", sourceTable), connSource))
      {
         using (SqlDataReader reader = cmd.ExecuteReader())
         {
            SqlBulkCopy copy = new SqlBulkCopy(targetConnectionString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.AllowEncryptedValueModifications);
            copy.EnableStreaming = true;
            copy.DestinationTableName = targetTable;
            copy.WriteToServer(reader);
         }
      }
}
```


## Always Encrypted API Reference

**Namespace:** [System.Data.SqlClient](/dotnet/api/system.data.sqlclient)

**Assembly:** System.Data (in System.Data.dll)




|Name|Description|Introduced in .NET version
|:---|:---|:---
|[SqlColumnEncryptionCertificateStoreProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncertificatestoreprovider)|A key store provider for Windows Certificate Store.|  4.6
|[SqlColumnEncryptionCngProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncngprovider)|A key store provider for Microsoft Cryptography API: Next Generation (CNG).|  4.6.1
|[SqlColumnEncryptionCspProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncspprovider)|A key store provider for Microsoft CAPI based Cryptographic Service Providers (CSP).|4.6.1  
|[SqlColumnEncryptionKeyStoreProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptionkeystoreprovider)|Base class of the key store providers.|  4.6
|[SqlCommandColumnEncryptionSetting Enumeration](/dotnet/api/system.data.sqlclient.sqlcommandcolumnencryptionsetting)|Settings to enable encryption and decryption for a database connection.|4.6  
|[SqlConnectionColumnEncryptionSetting Enumeration](/dotnet/api/system.data.sqlclient.sqlconnectioncolumnencryptionsetting)|Settings to control the behavior of Always Encrypted for individual queries.|4.6  
| [SqlConnectionStringBuilder.ColumnEncryptionSetting Property](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.columnencryptionsetting)|Gets and sets Always Encrypted in the connection string.|4.6|
| [SqlConnection.ColumnEncryptionQueryMetadataCacheEnabled Property](/dotnet/api/system.data.sqlclient.sqlconnection.columnencryptionquerymetadatacacheenabled) | Enables and disables encryption query metadata caching. | 4.6.2
| [SqlConnection.ColumnEncryptionKeyCacheTtl Property](/dotnet/api/system.data.sqlclient.sqlconnection.columnencryptionkeycachettl) | Gets and sets time-to-live for entries in the column encryption key cache. | 4.6.2
|[SqlConnection.ColumnEncryptionTrustedMasterKeyPaths Property](/dotnet/api/system.data.sqlclient.sqlconnection.columnencryptiontrustedmasterkeypaths)|Allows you to set a list of trusted key paths for a database server. If while processing an application query the driver receives a key path that is not on the list, the query will fail. This property provides additional protection against security attacks that involve a compromised SQL Server providing fake key paths, which may lead to leaking key store credentials.|  4.6
|[SqlConnection.RegisterColumnEncryptionKeyStoreProviders Method](/dotnet/api/system.data.sqlclient.sqlconnection.registercolumnencryptionkeystoreproviders)|Allows you to register custom key store providers. It is a dictionary that maps key store provider names to key store provider implementations.|  4.6
|[SqlCommand Constructor (String, SqlConnection, SqlTransaction, SqlCommandColumnEncryptionSetting)](https://msdn.microsoft.com/library/dn956511\(v=vs.110\).aspx)|Enables you to control the behavior of Always Encrypted for individual queries.|  4.6
|[SqlParameter.ForceColumnEncryption Property](/dotnet/api/system.data.sqlclient.sqlparameter.forcecolumnencryption)|Enforces encryption of a parameter. If SQL Server informs the driver that the parameter does not need to be encrypted, the query using the parameter will fail. This property provides additional protection against security attacks that involve a compromised SQL Server providing incorrect encryption metadata to the client, which may lead to data disclosure.|4.6  
|New [connection string](/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring) keyword: `Column Encryption Setting=enabled`|Enables or disables Always Encrypted functionality for the connection.| 4.6 
  

## See Also

- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [SQL Database tutorial: Protect sensitive data with Always Encrypted](/azure/azure-sql/database/always-encrypted-certificate-store-configure)
