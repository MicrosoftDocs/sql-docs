---
title: "Using Always Encrypted with the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "12/30/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 271c0438-8af1-45e5-b96a-4b1cabe32707
caps.latest.revision: 64
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Using Always Encrypted with the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article provides information on how to develop Java applications using [Always Encrypted](https://msdn.microsoft.com/library/mt163865.aspx#Anchor_7) and the Microsoft JDBC Driver 6.0 (or higher) for SQL Server.

Always Encrypted allows clients to encrypt sensitive data and never reveal the data or the encryption keys to SQL Server or Azure SQL Database. An Always Encrypted enabled driver, such as the Microsoft JDBC Driver 6.0 (or higher) for SQL Server, achieves this by transparently encrypting and decrypting sensitive data in the client application. The driver automatically determines which query parameters correspond to sensitive database columns (protected using Always Encrypted), and  encrypts the values of those parameters before passing the values to SQL Server or Azure SQL Database. Similarly, the driver transparently decrypts data retrieved from encrypted database columns in query results. For more information,visit [Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx#Anchor_7) and [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md).  


## Prerequisites

- Configure Always Encrypted in your database. This involves provisioning Always Encrypted keys and setting up encryption for selected database columns. If you do not already have a database with Always Encrypted configured, follow the directions in [Getting Started with Always Encrypted](https://msdn.microsoft.com/library/mt163865.aspx#Anchor_5).
- Make sure Microsoft JDBC Driver 6.0 (or higher) for SQL Server is installed on your development machine. 
-   Download and install the Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files.  Be sure to read the Readme included in the zip file for installation instructions and pertinent details on possible export/import issues.  
  
    -   If using the sqljdbc41.jar, the policy files can be downloaded from [Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files 7 Download](http://www.oracle.com/technetwork/java/javase/downloads/jce-7-download-432124.html)  
  
    -   If using the sqljdbc42.jar, the policy files can be downloaded from  [Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files 8 Download](http://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html)   
    
## Enabling Always Encrypted for Application Queries  
The easiest way to enable the encryption of parameters, and the decryption of query results targeting the encrypted columns, is by setting the value of the **columnEncryptionSetting** connection string keyword to **Enabled**.

The following is an example of a connection string that enables Always Encrypted in the JDBC driver:
  
```  
String connectionString = "jdbc:sqlserver://localhost;user=<user>;password=<password>;databaseName=<database>;columnEncryptionSetting=Enabled;"; 
SQLServerConnection connection = (SQLServerConnection) DriverManager.getConnection(connectionString);
     
```  
  
And, the following is an equivalent example using the SQLServerDataSource object.  
  
```  
SQLServerDataSource ds = new SQLServerDataSource();  
ds.setServerName("localhost");
ds.setUser("<user>");
ds.setPassword("<password>");
ds.setDatabaseName("<database>");
ds.setColumnEncryptionSetting("Enabled");  
SQLServerConnection con = (SQLServerConnection) ds.getConnection(); 
```    

Always Encrypted can also be enabled for individual queries. See the **Controlling Performance Impact of Always Encrypted** section below. Note that, enabling Always Encrypted is not sufficient for encryption or decryption to succeed. You also need to make sure:
- The application has the *VIEW ANY COLUMN MASTER KEY DEFINITION* and *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION* database permissions, required to access the metadata about Always Encrypted keys in the database. For details, see [Permissions section in Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx#Anchor_7).
- The application can access the column master key that protects the column encryption keys, encrypting the queried database columns. Note that, to use the Java Key Store provider you need to provide additional credentials in the connection string. See **Using Java Key Store provider** section for more details.

### Configuring how java.sql.Time Values are Sent to the Server

The **sendTimeAsDatetime** connection property is used to configure how the java.sql.Time value is sent to the server. When sendTimeAsDatetime=false, the time value is sent as SQL Server time type and when sendTimeAsDatetime=true, the time value is sent as a datetime type. Note that, when a time column is encrypted, the sendTimeAsDatetime property must be false as encrypted columns do not support the conversion from time to datetime. Also note that this property is by default true, so when using encrypted time columns, you will have to set it to false. Otherwise the driver will throw as exception. The SQLServerConnection class has two methods, starting with version 6.0 of the driver to configure the value of this property programmatically:
 
* public void setSendTimeAsDatetime(boolean sendTimeAsDateTimeValue)
* public boolean getSendTimeAsDatetime()

For more information on this property visit [Configuring How java.sql.Time Values are Sent to the Server](https://msdn.microsoft.com/library/ff427224(v=sql.110).aspx). 

### Configuring how String Values are Sent to the Server

The **sendStringParametersAsUnicode** connection property is used to configure how String values are sent to SQL Server. If set to "true", String parameters are sent to the server in Unicode format. If set to "false", String parameters are sent in non-Unicode format such as ASCII/MBCS instead of Unicode. The default value for this property is "true". When Always Encrypted is enabled, and a char/varchar/varchar(max) column is encrypted, the value of **sendStringParametersAsUnicode** must be set to true (or be left as the default). The Microsoft JDBC Driver for SQL Server will throw an exception when inserting data to an encrypted char/varchar/varchar(max) column if this property is set to false. For more information on this property, visit [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md). 
  
## Retrieving and Modifying Data in Encrypted Columns

Once you enable Always Encrypted for application queries, you can use standard JDBC APIs to retrieve or modify data in encrypted database columns. Assuming your application has the required database permissions and can access the column master key, the Microsoft JDBC Driver for SQL Server will encrypt any query parameters that target encrypted columns, and will decrypt data retrieved from encrypted columns returning plaintext values of JDBC types, corresponding to the SQL Server data types set for the columns in the database schema.
If Always Encrypted is not enabled, queries with parameters that target encrypted columns will fail. Queries can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns. However, the Microsoft JDBC Driver for SQL Server will not attempt to decrypt any values retrieved from encrypted columns and the application will receive binary encrypted data (as byte arrays).

The below table summarizes the behavior of queries, depending on whether Always Encrypted is enabled or not:

|Query characteristic | Always Encrypted is enabled and application can access the keys and key metadata|Always Encrypted is enabled and application cannot access the keys or key metadata | Always Encrypted is disabled|
|:---|:---|:---|:---|
| Queries with parameters targeting encrypted columns. | Parameter values are transparently encrypted. | Error | Error|
| Queries retrieving data from encrypted columns, without parameters targeting encrypted columns.| Results from encrypted columns are transparently decrypted. The application receives plaintext values of the JDBC datatypes corresponding to the SQL Server types configured for the encrypted columns. | Error | Results from encrypted columns are not decrypted. The application receives encrypted values as byte arrays (byte[]).
      
 
### Inserting and Retrieving Encrypted Data Examples 
The following examples illustrate retrieving and modifying data in encrypted columns. The examples assume the target table with the below schema. Note that the SSN and BirthDate columns are encrypted.

```
CREATE TABLE [dbo].[Patients]([PatientId] [int] IDENTITY(1,1), 
 [SSN] [char](11) COLLATE Latin1_General_BIN2 
 ENCRYPTED WITH (ENCRYPTION_TYPE = DETERMINISTIC, 
 ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', 
 COLUMN_ENCRYPTION_KEY = MyCEK) NOT NULL,
 [FirstName] [nvarchar](50) NULL,
 [LastName] [nvarchar](50) NULL, 
 [BirthDate] [date] 
 ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, 
 ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', 
 COLUMN_ENCRYPTION_KEY = MyCEK) NOT NULL
 PRIMARY KEY CLUSTERED ([PatientId] ASC) ON [PRIMARY])
 GO
```
 
### Inserting Data Example

This example inserts a row into the Patients table. Note the following:
- There is nothing specific to encryption in the sample code. The Microsoft JDBC Driver for SQL Server automatically detects and encrypts the parameters that target encrypted columns. This makes encryption transparent to the application. 
- The values inserted into database columns, including the encrypted columns, are passed as parameters using SQLServerPreparedStatement. While using parameters is optional when sending values to non-encrypted columns (although, it is highly recommended because it helps prevent SQL injection), it is required for values targeting encrypted columns. If the values inserted in the encrypted columns were passed as literals embedded in the query statement, the query would fail because the Microsoft JDBC Driver for SQL Server would not be able to determine the values in the target encrypted columns, so it would not encrypt the values. As a result, the server would reject 
them as incompatible with the encrypted columns.
- All values printed by the program will be in plaintext, as the Microsoft JDBC Driver for SQL Server will transparently decrypt the data retrieved from the encrypted columns.
- If you are doing a lookup using WHERE clause, the value used in the WHERE clause needs to be passed as a parameter, so that the Microsoft JDBC Driver for SQL Server can transparently encrypt it before sending it to the database. In the example below, note that SSN is passed as a parameter, but the LastName is passed as a literal as LastName is not encrypted.
- The setter method used for the parameter targeting the SSN column is setString(), which maps to the char/varchar SQL Server data type. If, for this parameter, the setter method used was  setNString(), which maps to nchar/nvarchar, the query would fail, as Always Encrypted does not support conversions from encrypted nchar/nvarchar values to encrypted char/varchar values.  

```
String connectionString = "jdbc:sqlserver://localhost:1433;databaseName=Clinic;user=sa;password=******;columnEncryptionSetting=Enabled;";
try  
{           
     Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");  
     try (Connection sourceConnection = DriverManager.getConnection(connectionString))  
     {                  
        String insertRecord="INSERT INTO [dbo].[Patients] VALUES (?, ?, ?, ?)";        
        try (PreparedStatement insertStatement = sourceConnection.prepareStatement(insertRecord))  
        {
        	insertStatement.setString(1, "795-73-9838");  
            insertStatement.setString(2, "Catherine");   
            insertStatement.setString(3, "Abel");                   
            insertStatement.setDate(4, Date.valueOf("1996-09-10"));  
            insertStatement.executeUpdate();  
            System.out.println("1 record inserted.\n");  
        }         
     }  
}  
catch (Exception e)  
{  
    e.printStackTrace();  
}
```

### Retrieving Plaintext Data Example

The following example demonstrates filtering data based on encrypted values, and retrieving plaintext data from encrypted columns. Note the following:
- The value used in the WHERE clause to filter on the SSN column needs to be passed as a parameter, so that the Microsoft JDBC Driver for SQL Server can transparently encrypt it before sending it to the database.
- All values printed by the program will be in plaintext, as the Microsoft JDBC Driver for SQL Server will transparently decrypt the data retrieved from the SSN and BirthDate columns.

> [!NOTE]  
>  Queries can perform equality comparisons on columns if they are encrypted using deterministic encryption. For more information, see the **Selecting Deterministic or Randomized encryption** section of the [Always Encrypted (Database Engine)](/sql-docs/docs/relational-databases/security/encryption/always-encrypted-database-engine) topic.  

```
String connectionString =  "jdbc:sqlserver://localhost:1433;databaseName=Clinic;user=sa;password=******;columnEncryptionSetting=Enabled;" ;
String filterRecord="SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN = ?;";  

try (PreparedStatement selectStatement = sourceConnection.prepareStatement(filterRecord))  
{  
    selectStatement.setString(1, "795-73-9838");  
    ResultSet rs = selectStatement.executeQuery();   
    while(rs.next()) 
    {  
    System.out.println("SSN: " +rs.getString("SSN") + 
    ", FirstName: " + rs.getString("FirstName") + 
    ", LastName:"+ rs.getString("LastName")+
     ", Date of Birth: " + rs.getString("BirthDate"));  
          }  
}
catch (Exception e)  
{  
    e.printStackTrace();  
}
```
  
### Retrieving Encrypted Data Example

If Always Encrypted is not enabled, a query can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns.

The following examples illustrates retrieving binary encrypted data from encrypted columns. Note the following:

- As Always Encrypted is not enabled in the connection string, the query will return encrypted values of SSN and BirthDate as byte arrays (the program converts the values to strings).
- A query retrieving data from encrypted columns with Always Encrypted disabled can have parameters, as long as none of the parameters target an encrypted column. The above query filters by LastName, which is not encrypted in the database. If the query filtered by SSN or BirthDate, the query would fail.

```
String connectionString  = "jdbc:sqlserver://localhost:1433;" + "databaseName=Clinic;user=sa;password=******";

String filterRecord="SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE LastName = ?;"; 
 
try (PreparedStatement selectStatement = sourceConnection.prepareStatement(filterRecord))  
{  
    selectStatement.setString(1, "Abel");  
    ResultSet rs = selectStatement.executeQuery();   
    while(rs.next())
    {  
        System.out.println("SSN: " + rs.getString("SSN") +
         ", FirstName: " + rs.getString("FirstName") + 
        ", LastName:"+ rs.getString("LastName")+ 
        ", Date of Birth: " + rs.getString("BirthDate"));  
    } 
}  
catch (Exception e)  
{  
    e.printStackTrace();  
}
```

### Avoiding Common Problems when Querying Encrypted Columns

This section describes common categories of errors when querying encrypted columns from Java applications and a few guidelines on how to avoid them.

### Unsupported Data Type Conversion Errors

Always Encrypted supports few conversions for encrypted data types. See [Always Encrypted (Database Engine)](/sql-docs/docs/relational-databases/security/encryption/always-encrypted-database-engine) for the detailed list of supported type conversions. Here is what you can do to avoid data type conversion errors, make sure that:

- you use the proper setter methods when passing values for the parameters targeting encrypted columns, so that the SQL Server data type of the parameter is either exactly the same as the type of the target column, or a conversion of the SQL Server data type of the parameter to the target type of the column is supported. Note that new API methods have been added to SQLServerPreparedStatement, SQLServerCallableStatement and SQLServerResultSet classes to pass parameters corresponding to specific SQL Server data types. For example, if a column is unencrypted you can use setTimestamp() method to pass a parameter to a datetime2 or to a datetime column. But when a column is encrypted you will have to use the exact method representing the type of the column in the database. For example, use setTimestamp() to pass values to an encrypted datetime2 column and use setDateTime() to pass values to an encrypted datetime column. See [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md) for a complete list of new APIs. 
- the precision and scale of parameters targeting columns of the decimal and numeric SQL Server data types is the same as the precision and scale configured for the target column. Note that new API methods have been added to SQLServerPreparedStatement, SQLServerCallableStatement and SQLServerResultSet classes to accept precision and scale along with data values for parameters/columns representing decimal and numeric data types. See [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md) for a complete list of new/overloaded APIs.  
- the fractional seconds precision/scale of parameters targeting columns of datetime2, datetimeoffset, or time SQL Server data types is not greater than that for the target column, in queries that modify values of the target column. Note that new API methods have been added to SQLServerPreparedStatement, SQLServerCallableStatement and SQLServerResultSet classes to accept fractional seconds precision/scale along with data values for parameters representing these data types. See [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md) for a complete list of new/overloaded APIs.   

### Errors due to Incorrect Connection Properties
This section describes how to configure connection settings properly to use Always Encrypted data. Since encrypted data types support limited conversions, the 'sendTimeAsDatetime' and 'sendStringParametersAsUnicode' connection settings need proper configuration when using encrypted columns. Make sure that: 
- [sendTimeAsDatetime](https://msdn.microsoft.com/library/ff427224(v=sql.110).aspx) connection setting is set to false when inserting data into encrypted time columns. For more information, see 'Configuring how java.sql.Time Values are Sent to the Server' section.
- [sendStringParametersAsUnicode](../../connect/jdbc/setting-the-connection-properties.md) connection setting is set to true (or is left as the default) when inserting data into encrypted char/varchar/varchar(max) columns. For more information, see 'Configuring how String Values are Sent to the Server' section.

### Errors due to Passing Plaintext instead of Encrypted Values

Any value that targets an encrypted column needs to be encrypted inside the application. An attempt to insert/modify or to filter by a plaintext value on an encrypted column will result in an error similar to this:


```
com.microsoft.sqlserver.jdbc.SQLServerException: Operand type clash: varchar is incompatible with varchar(8000) encrypted with (encryption_type = 'DETERMINISTIC', encryption_algorithm_name = 'AEAD_AES_256_CBC_HMAC_SHA_256', column_encryption_key_name = 'MyCEK', column_encryption_key_database_name = 'ae') collation_name = 'SQL_Latin1_General_CP1_CI_AS'
```

To prevent such errors, make sure:
- always Encrypted is enabled for application queries targeting encrypted columns (for the connection string or for a specific query).
- you use prepared statements and parameters to send data targeting encrypted columns. The below example shows a query that incorrectly filters by a literal/constant on an encrypted column (SSN), instead of passing the literal inside as a parameter. This query will fail.

```
ResultSet rs = connection.createStatement().executeQuery("SELECT * FROM Customers WHERE SSN='795-73-9838'");  
```

## Working with Column Master Key Stores
To encrypt a parameter value or to decrypt data in query results, the Microsoft JDBC Driver for SQL Server needs to obtain a column encryption key that is configured for the target column. Column encryption keys are stored in encrypted form in the database metadata. Each column encryption key has a corresponding column master key that was used to encrypt the column encryption key. The database metadata does not store the column master keys – it only contains the information about a key store containing a particular column master key and the location of the key in the key store.

To obtain a plaintext value of a column encryption key, the Microsoft JDBC Driver for SQL Server first obtains the metadata about both the column encryption key and its corresponding column master key, and then it uses the information in the metadata to contact the key store, containing the column master key, and to decrypt the encrypted column encryption key. The Microsoft JDBC Driver for SQL Server communicates with a key store using a column master key store provider – which is an instance of a class derived from **SQLServerColumnEncryptionKeyStoreProvider** Class.


### Using Built-in Column Master Key Store Providers
  
The Microsoft JDBC Driver for SQL Server comes with the following built-in column master key store providers. Note that, some of these providers are pre-registered with the specific provider names (used to look up the provider), and some require either additional credentials or explicit registration.

| Class | Description | Provider (lookup) name |Is pre-registered?|
|:---|:---|:---|:---|
|**SQLServerColumnEncryptionAzureKeyVaultProvider**| A provider for a key store for the Azure Key Vault.| AZURE_KEY_VAULT|No|
|**SQLServerColumnEncryptionCertificateStoreProvider**| A provider for the Windows Certificate Store.|MSSQL_CERTIFICATE_STORE|Yes
|**SQLServerColumnEncryptionJavaKeyStoreProvider**| A provider for the Java keystore|MSSQL_JAVA_KEYSTORE|Yes|

For the pre-registered key store providers you do not need to make any application code changes to use these providers but note the following:

- You (or your DBA) need to make sure the provider name, configured in the column master key metadata, is correct and the column master key path complies with the key path format that is valid for a given provider. It is recommended that you configure the keys using tools such as SQL Server Management Studio, which automatically generates the valid provider names and key paths when issuing the CREATE COLUMN MASTER KEY (Transact-SQL) statement.
- You need to ensure your application can access the key in the key store. This may involve granting your application access to the key and/or the key store, depending on the key store, or performing other key store-specific configuration steps. For example, for using the SQLServerColumnEncryptionJavaKeyStoreProvider you need to provide the location and the password of the keystore in the connection properties. 

All of these key store providers are described in more detail below.
  
### Using Azure Key Vault Provider
Azure Key Vault is a convenient option to store and manage column master keys for Always Encrypted (especially if your applications are hosted in Azure). The Microsoft JDBC Driver for SQL Server includes a built in provider, SQLServerColumnEncryptionAzureKeyVaultProvider, for applications that have keys stored in Azure Key Vault. The name of this provider is AZURE_KEY_VAULT. In order to use the Azure Key Vault store provider, an application developer needs to create the vault and the keys in Azure and configure the application to access the keys. For more information on how to setup the key vault and create column master key refer to [Azure Key Vault – Step by Step for more information on setting up the key vault](https://blogs.technet.microsoft.com/kv/2015/06/02/azure-key-vault-step-by-step/) and [Creating Column Master Keys in Azure Key Vault](https://msdn.microsoft.com/library/mt723359.aspx#Anchor_2).  
  
To use the Azure Key Vault, client applications need to instantiate the SQLServerColumnEncryptionAzureKeyVaultProvider and register it with the driver. The JDBC driver delegates authentication to the application through an interface called SQLServerKeyVaultAuthenticationCallback which has a method for retrieving an access token from the key vault. To instantiate the Azure Key Vault store provider, the application developer needs to provide an implementation for the only method called **getAccessToken** that retrieves the access token for the key stored in Azure Key Vault.  
  
Here is an example of initializing SQLServerKeyVaultAuthenticationCallback and SQLServerColumnEncryptionAzureKeyVaultProvider:  
  
```  
// String variables clientID and clientSecret hold the client id and client secret values respectively.  
  
ExecutorService service = Executors.newFixedThreadPool(10);  
SQLServerKeyVaultAuthenticationCallback authenticationCallback = new SQLServerKeyVaultAuthenticationCallback() {  
       @Override  
    public String getAccessToken(String authority, String resource, String scope) {  
        AuthenticationResult result = null;  
        try{  
                AuthenticationContext context = new AuthenticationContext(authority, false, service);  
            ClientCredential cred = new ClientCredential(clientID, clientSecret);  
  
            Future<AuthenticationResult> future = context.acquireToken(resource, cred, null);  
            result = future.get();  
        }  
        catch(Exception e){  
            e.printStackTrace();  
        }  
        return result.getAccessToken();  
    }  
};  
  
SQLServerColumnEncryptionAzureKeyVaultProvider akvProvider = new SQLServerColumnEncryptionAzureKeyVaultProvider(authenticationCallback, service);  
  
```

After the application creates an instance of SQLServerColumnEncryptionAzureKeyVaultProvider, the application needs to register the instance within Microsoft JDBC Driver for SQL Server using the SQLServerConnection.registerColumnEncryptionKeyStoreProviders() method. It is highly recommended, the instance is registered using the default lookup name, AZURE_KEY_VAULT, which can be obtained by calling the SQLServerColumnEncryptionAzureKeyVaultProvider.getName() API. Using the default name, will allow you to use tools, such as SQL Server Management Studio or PowerShell, to provision and manage Always Encrypted keys (the tools use the default name to generate the metadata object to column master key). The below example shows registering the Azure Key Vault provider. For more details on the SQLServerConnection.registerColumnEncryptionKeyStoreProviders() method , see [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md). 

```
Map<String, SQLServerColumnEncryptionKeyStoreProvider> keyStoreMap = new HashMap<String, SQLServerColumnEncryptionKeyStoreProvider>();   
keyStoreMap.put(akvProvider.getName(), akvProvider);   
SQLServerConnection.registerColumnEncryptionKeyStoreProviders(keyStoreMap);   
```
  
> [!IMPORTANT]  
>  The Azure Key Vault implementation of the JDBC driver has  dependencies  on these libraries (from GitHub):  
>   
>  [azure-sdk-for-java](https://github.com/Azure/azure-sdk-for-java)  
>   
>  [azure-activedirectory-library-for-java libraries](https://github.com/AzureAD/azure-activedirectory-library-for-java)  
  
### Using Windows Certificate Store Provider
The SQLServerColumnEncryptionCertificateStoreProvider can be used to store column master keys in the Windows Certificate Store. Use the SQL Server Management Studio (SSMS) Always Encrypted wizard or other supported tools to create the column master key and column encryption key definitions in the database. The same wizard can be used to generate a self signed certificate in the Windows Certificate Store that be used as a column master key for the always encrypted data. For more information on column master key and column encryption key T-SQL syntax visit [CREATE COLUMN MASTER KEY](../../t-sql/statements/create-column-master-key-transact-sql.md) and [CREATE COLUMN ENCRPTION KEY](../../t-sql/statements/create-column-encryption-key-transact-sql.md) respectively.

The name of the SQLServerColumnEncryptionCertificateStoreProvider is "MSSQL_CERTIFICATE_STORE" and can be queried by the getName() API of the provider object. It is automatically registered by the driver and can be used seamlessly without any application change.

> [!IMPORTANT]  
>  The SQLServerColumnEncryptionCertificateStoreProvider implementation of the JDBC driver is available with Windows operating systems only and has a dependency on the sqljdbc_auth.dll that is available in the driver package.  To use this provider, copy the sqljdbc_auth.dll file to a directory on the Windows system path on the computer where the JDBC driver is installed. Alternatively you can set the java.libary.path system property to specify the directory of the sqljdbc_auth.dll. If you are running a 32-bit Java Virtual Machine (JVM), use the sqljdbc_auth.dll file in the x86 folder, even if the operating system is the x64 version. If you are running a 64-bit JVM on a x64 processor, use the sqljdbc_auth.dll file in the x64 folder. For example, if you are using the 32-bit JVM and the JDBC driver is installed in the default directory, you can specify the location of the DLL by using the following virtual machine (VM) argument when the Java application is started:  
`-Djava.library.path=C:\Microsoft JDBC Driver <version> for SQL Server\sqljdbc_<version>\enu\auth\x86`
        
### Using Java Key Store Provider  
The JDBC driver comes with a built in key store provider implementation for the Java Key Store. The driver automatically instantiates and registers the provider for Java Key Store, if the **keyStoreAuthentication** connection string property is present in the connection string and is set to "JavaKeyStorePassword" (please, see more details below). The name of the Java key store provider is MSSQL_JAVA_KEYSTORE. This name can also be queried by the SQLServerColumnEncryptionJavaKeyStoreProvider.getName() API. 

Three new connection string keywords are introduced to allow a client aplication to declaratively specify the credentials the driver needs to authenticate to the Java Key Store. The driver would initialize the provider, based on the values of the following three properties of the connection string, for the specific connections. 
  
 **keyStoreAuthentication:** Identifies the Key Store to use. With Microsoft JDBC Driver 6.0 for SQL Server, you can authenticate to the Java Key Store only through this property. For the Java Key Store, the value for this property must be "JavaKeyStorePassword".   
  
 **keyStoreLocation:** The path to the Java keystore file that stores the column master key. Note that the path includes the keystore filename.  
  
 **keyStoreSecret:** The secret/password to use for the keystore as well as for the key. Note that for using the Java Key Store the keystore and the key password must be the same.  

Here is an example on providing these credentials in connection string:

    String connectionString = "jdbc:sqlserver://localhost;user=<user>;password=<password>;columnEncryptionSetting=Enabled;keyStoreAuthentication=JavaKeyStorePassword;keyStoreLocation=<path_to_the_keystore_file>;keyStoreSecret=<keystore_key_password>";
    
These settings can also be set/get using the SQLServerDataSource object. See [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md) for more information.     
  
The JDBC driver automatically instantiates the SQLServerColumnEncryptionJavaKeyStoreProvider when these credentials are present in connection properties. 
  
### Creating a Column Master Key for the Java Key Store
The SQLServerColumnEncryptionJavaKeyStoreProvider can be used with JKS or PKCS12 keystore types. To create or import a key to use with this provider use the Java [keytool](http://docs.oracle.com/javase/7/docs/technotes/tools/windows/keytool.html) utility. Please note that the key must have the same password as the keystore itself. Here is an example of how to create a public key and its associated private key using the keytool utility.

    keytool -genkeypair -keyalg RSA -alias AlwaysEncryptedKey -keystore keystore.jks -storepass mypassword -validity 360 -keysize 2048 -storetype jks    

This command creates a public key and wraps it in a X.509 self signed certificate which is then stored in the keystore 'keystore.jks' along with it's associated private key. This entry in the keystore is identified by the alias 'AlwaysEncryptedKey'. 

Here is an example of the same using a PKCS12 storetype. 

    keytool -genkeypair -keyalg RSA -alias AlwaysEncryptedKey -keystore keystore.pfx -storepass mypassword -validity 360 -keysize 2048 -storetype pkcs12 -keypass mypassword   

Note that if the keystore is of type PKCS12 then the keytool utility does not prompt for key password and the key password needs to be provided with -keypass option as the SQLServerColumnEncryptionJavaKeyStoreProvider requires that the keystore and the key have the same password.

You can also export a certificate from the Windows Certificate store in .pfx format and use that with the SQLServerColumnEncryptionJavaKeyStoreProvider. The exported certificate can also be imported to the Java Key Store as a JKS keystore type. 

After creating the keytool entry you will have to create the column master key meta data in the database which needs the key store provider name and the key path. For more information on how to create column master key meta data visit [CREATE COLUMN MASTER KEY](../../t-sql/statements/create-column-master-key-transact-sql.md). For SQLServerColumnEncryptionJavaKeyStoreProvider, the key path is just the alias of the key. And the name of the SQLServerColumnEncryptionJavaKeyStoreProvider is 'MSSQL_JAVA_KEYSTORE'. You can also query this name using the getName() public API of the SQLServerColumnEncryptionJavaKeyStoreProvider class. 

The T-SQL syntax for creating the column master key is:

```  
CREATE COLUMN MASTER KEY [<CMK_name>]  
WITH  
(  
    KEY_STORE_PROVIDER_NAME = N'MSSQL_JAVA_KEYSTORE',  
    KEY_PATH = N'<key_alias>'  
)  
```  

For the 'AlwaysEncryptedKey' created above, the column master key definition would be:

```  
CREATE COLUMN MASTER KEY [MyCMK]
WITH
(
    KEY_STORE_PROVIDER_NAME = N'MSSQL_JAVA_KEYSTORE',
    KEY_PATH = N'AlwaysEncryptedKey'
)
```  
    
> [!NOTE]  
>  The built in SQL Server management Studio functionality cannot create column master key definitions for the Java Key Store for which you must use T-SQL command.  
  
### Creating a Column Encryption Key for the Java Key Store
Note that the SQL Server management Studio or any other tool can not be used to create column encryption keys using column master keys in the Java Key Store. The client application must create the column encryption key programmatically using the SQLServerColumnEncryptionJavaKeyStoreProvider class. For more details visit section 'Using Column Master Key Store Providers for Programmatic Key Provisioning'. 

  
### Implementing a custom column master key store provider
If you want to store column master keys in a key store that is not supported by an existing provider, you can implement a custom provider by extending the SQLServerColumnEncryptionKeyStoreProvider Class and registering the provider using the SQLServerConnection.registerColumnEncryptionKeyStoreProviders() method.
  
```  
public class MyCustomKeyStore extends SQLServerColumnEncryptionKeyStoreProvider{  
    private String name = "MY_CUSTOM_KEYSTORE";
    
    public void setName(String name)
    {
        this.name = name;
    }
    
    public String getName()
    {
        return name;
    }

    public byte[] encryptColumnEncryptionKey(String masterKeyPath, String encryptionAlgorithm, byte[] plainTextColumnEncryptionKey)  
    {  
        // Logic for encrypting the column encryption key  
    }  
    
    public byte[] decryptColumnEncryptionKey(String masterKeyPath, String encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)  
    {  
        // Logic for decrypting the column encryption key  
    }  
}  
  
```  
  
 Register the provider:  
  
```  
SQLServerColumnEncryptionKeyStoreProvider storeProvider = new MyCustomKeyStore();  
Map<String, SQLServerColumnEncryptionKeyStoreProvider> keyStoreMap = new HashMap<String, SQLServerColumnEncryptionKeyStoreProvider>();  
keyStoreMap.put(storeProvider.getName(), storeProvider);  
SQLServerConnection.registerColumnEncryptionKeyStoreProviders(keyStoreMap);  
  
```  
  
## Using Column Master Key Store Providers for Programmatic Key Provisioning

When accessing encrypted columns, the Microsoft JDBC Driver for SQL Server transparently finds and calls the right column master key store provider to decrypt column encryption keys. Typically, your normal application code does not directly call column master key store providers. You may, however, instantiate and call a provider explicitly to programmatically provision and manage Always Encrypted keys: to generate an encrypted column encryption key and decrypt a column encryption key (e.g. as part column master key rotation). For more information, see [Overview of Key Management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md).
Note that implementing your own key management tools may be required only if you use a custom key store provider. When using keys stored in Windows Certificate Store or in  Azure Key Vault, you can use existing tools, such as SQL Server Management Studio or PowerShell, to manage and provision keys. When using keys stored in the Java Key Store, you need to provision keys programatically. 
The below example, illustrates using SQLServerColumnEncryptionJavaKeyStoreProvider Class to encrypt the key with a key stored in the Java Key Store.

```  
import java.sql.*;
import javax.xml.bind.DatatypeConverter;
import com.microsoft.sqlserver.jdbc.SQLServerColumnEncryptionJavaKeyStoreProvider;
import com.microsoft.sqlserver.jdbc.SQLServerColumnEncryptionKeyStoreProvider;
import com.microsoft.sqlserver.jdbc.SQLServerException;

/**
 * This program demonstrates how to create a column encryption key programatically for the Java Key Store.
 */
public class AlwaysEncrypted
{
    // Alias of the key stored in the keystore.
    private static String keyAlias = "<proide key alias>";

    // Name by which the column master key will be known in the database.
    private static String columnMasterKeyName = "MyCMK";

    // Name by which the column encryption key will be known in the database.
    private static String columnEncryptionKey = "MyCEK";

    // The location of the keystore. 
    private static String keyStoreLocation = "C:\\Dev\\Always Encrypted\\keystore.jks";

    // The password of the keystore and the key.
    private static char[] keyStoreSecret = "********".toCharArray();

    /**
     * Name of the encryption algorithm used to encrypt the value of
     * the column encryption key. The algorithm for the system providers must be RSA_OAEP.
     */
    private static String algorithm = "RSA_OAEP";

    public static void main(String[] args)
    {
        String connectionString = GetConnectionString();
        try
        {
            // Note: if you are not using try-with-resources statements (as here),
            // you must remember to call close() on any Connection, Statement,
            // ResultSet objects that you create.

            // Open a connection to the database.
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
            try (Connection sourceConnection = DriverManager.getConnection(connectionString))
            {
                // Instantiate the Java Key Store provider.
                SQLServerColumnEncryptionKeyStoreProvider storeProvider = 
                        new SQLServerColumnEncryptionJavaKeyStoreProvider(
                                keyStoreLocation,
                                keyStoreSecret);

                byte [] encryptedCEK=getEncryptedCEK(storeProvider);

                /**
                 * Create column encryption key 
                 * For more details on the syntax refer: 
                 * https://msdn.microsoft.com/library/mt146372.aspx
                 * Encrypted column encryption key first needs to be converted into varbinary_literal from bytes, 
                 * for which DatatypeConverter.printHexBinary is used
                 */
                String createCEKSQL = "CREATE COLUMN ENCRYPTION KEY " 
                        + columnEncryptionKey
                        + " WITH VALUES ( "
                        + " COLUMN_MASTER_KEY = " 
                        + columnMasterKeyName
                        + " , ALGORITHM =  '" 
                        + algorithm
                        + "' , ENCRYPTED_VALUE =  0x" 
                        + DatatypeConverter.printHexBinary(encryptedCEK)
                        + " ) ";

                try (Statement cekStatement = sourceConnection.createStatement())
                {
                    cekStatement.executeUpdate(createCEKSQL);
                    System.out.println("Column encryption key created with name : " + columnEncryptionKey);
                }
            }
        }
        catch (Exception e)
        {
            // Handle any errors that may have occurred.
            e.printStackTrace();
        }
    }

    // To avoid storing the sourceConnection String in your code,
    // you can retrieve it from a configuration file.
    private static String GetConnectionString()
    {

        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://localhost:1433;" +
                "databaseName=ae2;user=sa;password=********;";

        return connectionUrl;
    }

    private static byte[] getEncryptedCEK(SQLServerColumnEncryptionKeyStoreProvider storeProvider) throws SQLServerException
    {
        /**
         * Following arguments needed by  SQLServerColumnEncryptionJavaKeyStoreProvider
         * 1) keyStoreLocation : 
         *      Path where keystore is located, including the keystore file name. 
         * 2) keyStoreSecret : 
         *      Password of the keystore and the key.  
         */
        String plainTextKey = "You need to give your plain text";

        // plainTextKey has to be 32 bytes with current algorithm supported
        byte[] plainCEK = plainTextKey.getBytes();

        // This will give us encrypted column encryption key in bytes
        byte[] encryptedCEK = storeProvider.encryptColumnEncryptionKey(
                keyAlias,
                algorithm,
                plainCEK);

        return encryptedCEK;
    }
}

```  
  
## Force Encryption on Input Parameters
  
The Force Encryption feature enforces encryption of a parameter when using Always Encrypted. If force encryption is used and SQL Server informs the driver that the parameter does not need to be encrypted, the query using the parameter will fail. This property provides additional protection against security attacks that involve a compromised SQL Server providing incorrect encryption metadata to the client, which may lead to data disclosure. The set* methods in SQLServerPreparedStatement and SQLServerCallableStatement classes and the update\* methods in the SQLServerResultSet class are overloaded to accept a boolean argument to specify force encryption setting. If the value of this argument is false, the driver will not force encryption on parameters. If force encryption is set to true, the query parameter will only be sent if the destination column is encrypted and Always Encrypted is enabled on the connection or on the statement. This gives an extra layer of security, ensuring that no data is mistakenly sent to SQL Server as plaintext when it is expected to be encrypted.  
  
 For more information on the SQLServerPreparedStatement and SQLServerCallableStatement methods that are overloaded with the force encryption setting, see [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md)  

## Controlling Performance Impact of Always Encrypted

Because Always Encrypted is a client-side encryption technology, most of performance overheads are observed on the client side, not in the database. Apart from the cost of encryption and decryption operations, the other sources of performance overheads on the client side are:
- Additional round trips to the database to retrieve metadata for query parameters.
- Calls to a column master key store to access a column master key.

This section describes the built-in performance optimizations in Microsoft JDBC Driver for SQL Server and how you can control the impact of the above two factors on performance.

### Controlling round trips to retrieve metadata for query parameters

If Always Encrypted is enabled for a connection, by default, the Microsoft JDBC Driver for SQL Server will call [sys.sp_describe_parameter_encryption](https://msdn.microsoft.com/library/mt631693.aspx?f=255&MSPPError=-2147217396) for each parameterized query, passing the query statement (without any parameter values) to SQL Server. [sys.sp_describe_parameter_encryption](https://msdn.microsoft.com/library/mt631693.aspx?f=255&MSPPError=-2147217396) analyzes the query statement to find out if any parameters need to be encrypted, and if so, for each such, it returns the encryption-related information that will allow the Microsoft JDBC Driver for SQL Server to encrypt parameter values. The above behavior ensures a high-level of transparency to the client application. The application (and the application developer) does not need to be aware of which queries access encrypted columns, as long as the values targeting encrypted columns are passed to the Microsoft JDBC Driver for SQL Server as parameters.


#### Setting Always Encrypted at the query level

To control performance impact of retrieving encryption metadata for parameterized queries, you can enable Always Encrypted for individual queries, instead of setting it up for the connection. This way, you can ensure that sys.sp_describe_parameter_encryption is invoked only for queries that you know have parameters targeting encrypted columns. Note, however, that by doing so, you reduce transparency of encryption: if you change encryption properties of your database columns, you may need to change the code of your application to align it with the schema changes.


To control the Always Encrypted behavior of individual queries, you need to configure individual statement objects by passing an Enum, SQLServerStatementColumnEncryptionSetting, which specifies how data will be sent and received when reading and writing encrypted columns for that specific statement. Here are some useful guidelines:
- If most queries a client application sends over a database connection access encrypted columns:
    - Set the columnEncryptionSetting connection string keyword to Enabled.
    - Set SQLServerStatementColumnEncryptionSetting.Disabled for individual queries that do not access any encrypted columns. This will disable both calling sys.sp_describe_parameter_encryption as well as an attempt to decrypt any values in the result set.
    - Set SQLServerStatementColumnEncryptionSetting.ResultSet for individual queries that do not have any parameters requiring encryption, but retrieve data from encrypted columns. This will disable calling sys.sp_describe_parameter_encryption and parameter encryption. The query will be able to decrypt the results from encryption columns.
- If most queries a client application sends over a database connection do not access encrypted columns:
    - Set the columnEncryptionSetting connection string keyword to Disabled.
    - Set SQLServerStatementColumnEncryptionSetting.Enabled for individual queries that have any parameters that need to be encrypted. This will enable both calling sys.sp_describe_parameter_encryption as well as the decryption of any query results retrieved from encrypted columns.
    - Set SQLServerStatementColumnEncryptionSetting.ResultSet for queries that do not have any parameters requiring encryption, but retrieve data from encrypted columns. This will disable calling sys.sp_describe_parameter_encryption and parameter encryption. The query will be able to decrypt the results from encryption columns.

Note that the SQLServerStatementColumnEncryptionSetting settings cannot be used to bypass encryption and gain access to plaintext data. For more details on how to configure column encryption on a statement, see [Always Encrypted API Reference for the JDBC Driver](../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md).  

In the below example, Always Encrypted is disabled for the database connection. The query, the application issues, has a parameter that targets the LastName column that is not encrypted. The query retrieves data from the SSN and BirthDate columns that are both encrypted. In such a case, calling sys.sp_describe_parameter_encryption to retrieve encryption metadata is not required. However, the decryption of the query results need to be enabled, so that the application can receive plaintext values from the two encrypted columns. SQLServerStatementColumnEncryptionSetting.ResultSet setting is used to ensure that.

```
// Assumes the same table definition as in Section "Retrieving and modifying data in encrypted columns"
// where only SSN and BirthDate columns are encrypted in the database.
String connectionUrl = "jdbc:sqlserver://localhost;databaseName=ae;user=sa;password=******;"
		+ "keyStoreAuthentication=JavaKeyStorePassword;"
		+ "keyStoreLocation=" + keyStoreLocation + ";"
		+ "keyStoreSecret=******;";
SQLServerConnection connection = (SQLServerConnection) DriverManager.getConnection(connectionString);

String filterRecord="SELECT FirstName, LastName, SSN, BirthDate FROM " + tblName + " WHERE LastName = ?";  
PreparedStatement selectStatement = connection.prepareStatement(
		filterRecord, 
		ResultSet.TYPE_FORWARD_ONLY, 
		ResultSet.CONCUR_READ_ONLY, 
		connection.getHoldability(), 
		SQLServerStatementColumnEncryptionSetting.ResultSetOnly);
selectStatement.setString(1, "Abel");  
ResultSet rs = selectStatement.executeQuery();  
while(rs.next()) {  
	System.out.println("First name: " + rs.getString("FirstName"));  
	System.out.println("Last name: " + rs.getString("LastName"));  
	System.out.println("SSN: " + rs.getString("SSN"));  
	System.out.println("Date of Birth: " + rs.getDate("BirthDate"));  
}  
rs.close();
selectStatement.close();
connection.close();
```


### Column encryption key caching

To reduce the number of calls to a column master key store to decrypt column encryption keys, the Microsoft JDBC Driver for SQL Server caches the plaintext column encryption keys in memory. After receiving the encrypted column encryption key value from database metadata, the driver first tries to find the plaintext column encryption key, corresponding to the encrypted key value. The driver calls the key store containing the column master key, only if it cannot find the encrypted column encryption key value in the cache.

You can configure a time-to-live value for the column encryption key entries in the cache using the API, setColumnEncryptionKeyCacheTtl(), in the SQLServerConnection class. The default time-to-live value for the column encryption key entries in the cache is 2 hours. To turn off caching use a value of 0. To set any time-to-live value use the following API:
    
    SQLServerConnection.setColumnEncryptionKeyCacheTtl (int columnEncryptionKeyCacheTTL, TimeUnit unit)
   
For example, to set a time-to-live value of 10 minutes, use
    
    SQLServerConnection.setColumnEncryptionKeyCacheTtl (10, TimeUnit.MINUTES)

Note that, only DAYS, HOURS, MINUTES or SECONDS are supported as the time unit.  


## Copying Encrypted Data using SQLServerBulkCopy

With SQLServerBulkCopy, you can copy data, which is already encrypted and stored in one table, to another table, without decrypting the data. To do that:

- Make sure the encryption configuration of the target table is identical to the configuration of the source table. In particular, both tables must have the same columns encrypted, and the columns must be encrypted using the same encryption types and the same encryption keys. Note: if any of the target columns is encrypted differently than its corresponding source column, you will not be able to decrypt the data in the target table after the copy operation. The data will be corrupted.
- Configure both database connections, to the source table and to the target table, without Always Encrypted enabled. 
- Set the allowEncryptedValueModifications option. See [Using Bulk Copy with the JDBC Driver](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md) for more information.
 
Note: Use caution when specifying AllowEncryptedValueModifications as this may lead to corrupting the database because the Microsoft JDBC Driver for SQL Server does not check if the data is indeed encrypted, or if it is correctly encrypted using the same encryption type, algorithm and key as the target column.

## See Also  
 [Always Encrypted (Database Engine)](/sql-docs/docs/relational-databases/security/encryption/always-encrypted-database-engine)  
  
  