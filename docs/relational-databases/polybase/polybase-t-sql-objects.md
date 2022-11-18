---
title: "PolyBase Transact-SQL reference"
description: "Use PolyBase to query your external data in Hadoop, Azure Blob Storage, Azure Data Lake Store, SQL Server, Oracle, Teradata, MongoDB, or CSV files."
ms.date: 07/25/2022
ms.service: sql
ms.subservice: polybase
ms.topic: tutorial
helpviewer_keywords: 
  - "PolyBase, fundamentals"
  - "PolyBase, SQL statements"
  - "PolyBase, SQL objects"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-2016 || >=aps-pdw-2016 || =azure-sqldw-latest"
---
# PolyBase Transact-SQL reference

[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article reviews options for using [PolyBase](polybase-guide.md) to query external data in-place, referred to as data virtualization, for a variety of external data sources. 

To use PolyBase, you must create external tables to reference your external data. Refer to:
  
 - [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md) 
 - [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)  
 - [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)  
 - [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)  
 - [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md)  

> [!NOTE]
> In order to use PolyBase you must have sysadmin or CONTROL SERVER level permissions on the database.

For more information and tutorials on creating various external data sources, see:

- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [S3-compatible object storage](polybase-configure-s3-compatible.md)
- [ODBC generic types](polybase-configure-odbc-generic.md)
- [CSV](virtualize-csv.md)
- [Delta table](virtualize-delta.md)

## Prerequisites  

To get started, install PolyBase on your SQL Server in [Windows](polybase-installation.md) or [Linux](polybase-linux-setup.md), then see [PolyBase configuration](polybase-configuration.md).
 
## Create external tables for Hadoop

Applies to: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)]

#### 1. Create database scoped credential

This step is required only for Kerberos-secured Hadoop clusters. 

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

```sql
-- Create a database master key on the database if one does not already exist.  
-- Required to encrypt the credential secret.  
  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';  
```

Then, create a database scoped credential for Kerberos-secured Hadoop clusters. `IDENTITY` is the Kerberos user name and `SECRET` is the Kerberos password. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql  
-- Create a database scoped credential for Kerberos-secured Hadoop clusters.  
-- IDENTITY: the Kerberos user name.  
-- SECRET: the Kerberos password  
  
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1   
WITH IDENTITY = '<hadoop_user_name>', Secret = '<hadoop_password>';  
```

#### 2. Create external data source

Create an external data source. 

- `LOCATION` is required and is the Hadoop name node IP address and port. Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
- `RESOURCE MANAGER LOCATION` is optional. This is the Hadoop Resource Manager location to enable [pushdown computation](polybase-pushdown-computation.md). 
- `CREDENTIAL` is the database scoped credential, created in the previous step.
  
```sql  
-- Create an external data source.  
-- LOCATION (Required) : Hadoop Name Node IP address and port.  
-- RESOURCE MANAGER LOCATION (Optional): Hadoop Resource Manager location to enable pushdown computation.  
-- CREDENTIAL (Optional):  the database scoped credential, created in the previous step.  
  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster WITH (  
        TYPE = HADOOP,   
        LOCATION ='hdfs://10.xxx.xx.xxx:xxxx',   
        RESOURCE_MANAGER_LOCATION = '10.xxx.xx.xxx:xxxx',   
        CREDENTIAL = HadoopUser1      
);  
```

#### 3. Create external file format

Create an external file format, where `FORMAT_TYPE` is the format in Hadoop, such as `DELIMITEDTEXT`, `RCFILE`, `ORC`, or `PARQUET`. 

```sql  
-- Create an external file format.  
-- FORMAT TYPE: Type of format in Hadoop (DELIMITEDTEXT,  RCFILE, ORC, PARQUET).  
  
CREATE EXTERNAL FILE FORMAT TextFileFormat WITH (  
        FORMAT_TYPE = DELIMITEDTEXT,   
        FORMAT_OPTIONS (FIELD_TERMINATOR ='|',   
                USE_TYPE_DEFAULT = TRUE)  
  
```  

#### 4. Create external table

Create an external table pointing to data stored in Hadoop, where `LOCATION` is the path to the file or directory that contains the data relative to HDFS root.

```sql  
-- Create an external table pointing to data stored in Hadoop.  
-- LOCATION: path to file or directory that contains the data (relative to HDFS root).  
  
CREATE EXTERNAL TABLE [dbo].[CarSensor_Data] (  
        [SensorKey] int NOT NULL,   
        [CustomerKey] int NOT NULL,   
        [GeographyKey] int NULL,   
        [Speed] float NOT NULL,   
        [YearMeasured] int NOT NULL  
)  
WITH (LOCATION='/Demo/',   
        DATA_SOURCE = MyHadoopCluster,  
        FILE_FORMAT = TextFileFormat  
);  
```  

#### 5. Create statistics

Finally, manually create a statistics object on the new external table.

```sql  
-- Create statistics on an external table.   
CREATE STATISTICS StatsForSensors on CarSensor_Data(CustomerKey, Speed)  
```  

## Create external tables for Azure Blob Storage  

Applies to: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.

For more information, see [Configure PolyBase to access external data in Azure Blob Storage](polybase-configure-azure-blob-storage.md).

#### 1. Create database scoped credential

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

```sql  
-- Create a database master key on the database if one does not already exist.  
-- Required to encrypt the credential secret.  
  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';  
```

Then, create a database scoped credential. `IDENTITY` in this case is any string, as this is not used for authentication to Azure storage. `SECRET` is the Azure storage account key. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql  
-- Create a database scoped credential  for Azure Blob Storage.  
-- IDENTITY: any string (this is not used for authentication to Azure storage).  
-- SECRET: your Azure storage account key.  
  
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential   
WITH IDENTITY = 'user', Secret = '<azure_storage_account_key>';  
```  

#### 2. Create external data source

- `LOCATION` is required and is the Azure account storage account name and blob container name. Be aware that options for `TYPE` and `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
- `CREDENTIAL` is the database scoped credential, created in the previous step.

To connect to ADLS Gen2 with PolyBase prior to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use `abfss` or `wasbs`:

```sql  
-- LOCATION:  Azure account storage account name and blob container name.  
-- CREDENTIAL: The database scoped credential created in the previous step.  
  
CREATE EXTERNAL DATA SOURCE AzureStorage with (  
        TYPE = BLOB_STORAGE,   
        LOCATION ='wasbs://<blob_container_name>@<azure_storage_account_name>.blob.core.windows.net',  
        CREDENTIAL = AzureStorageCredential  
);  
  
```  

Starting with PolyBase in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use a new prefix `adls` for ADLS Gen2:

```sql
-- For ADLS Gen 2 in SQL Server 2022 and later - Create an external data source
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'abfss://daily@logs.dfs.core.windows.net/' ,
    CREDENTIAL = AzureStorageCredential ,
    TYPE = HADOOP
  );
```

#### 3. Create external file format

```sql  
-- Create an external file format.  
-- FORMAT TYPE: Type of format in Hadoop (DELIMITEDTEXT,  RCFILE, ORC, PARQUET).  
  
CREATE EXTERNAL FILE FORMAT TextFileFormat WITH (  
        FORMAT_TYPE = DELIMITEDTEXT,   
        FORMAT_OPTIONS (FIELD_TERMINATOR ='|',   
                USE_TYPE_DEFAULT = TRUE)  
  
```  

#### 4. Create external table

```sql  
-- Create an external table pointing to data stored in Azure storage.  
-- LOCATION: path to a file or directory that contains the data (relative to the blob container).  
-- To point to all files under the blob container, use LOCATION='/'   
  
CREATE EXTERNAL TABLE [dbo].[CarSensor_Data] (  
        [SensorKey] int NOT NULL,   
        [CustomerKey] int NOT NULL,   
        [GeographyKey] int NULL,   
        [Speed] float NOT NULL,   
        [YearMeasured] int NOT NULL  
)  
WITH (LOCATION='/Demo/',   
        DATA_SOURCE = AzureStorage,  
        FILE_FORMAT = TextFileFormat  
);  
```  

#### 5. Create statistics

Finally, manually create a statistics object on the new external table.

```sql  
-- Create statistics on an external table.   
CREATE STATISTICS StatsForSensors on CarSensor_Data(CustomerKey, Speed);
```  

## Create external tables for Azure Data Lake Store

Applies to: [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], Analytics Platform System (PDW)

For more information, see [Load with Azure Data Lake Store](/azure/sql-data-warehouse/sql-data-warehouse-load-from-azure-data-lake-store).

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

#### 1. Create database scoped credential

```sql
-- Create a Database Master Key.
-- Only necessary if one does not already exist.
-- Required to encrypt the credential secret in the next step.

CREATE MASTER KEY;
```

Then, create a database scoped credential. `IDENTITY` is both the client ID and OAuth 2.0 Token Endpoint token from your Azure Active Directory Application, separated by a `@`. `SECRET` is the Azure AD Application Service Principal key. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).


```sql
-- Create a database scoped credential
-- IDENTITY: Pass the client id and OAuth 2.0 Token Endpoint token from your Azure Active Directory Application
-- SECRET: Provide your AAD Application Service Principal key.

CREATE DATABASE SCOPED CREDENTIAL ADL_User
WITH
    IDENTITY = '<client_id>@<OAuth_2.0_Token_EndPoint>'
    ,SECRET = '<key>'
;
```  

#### 2. Create external data source to reference Azure Data Lake Store (ADLS) Gen 1 or 2

- `LOCATION` is required and is the Azure account storage account name and blob container name. Be aware that options for `TYPE` and `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
- `CREDENTIAL` is the database scoped credential, created in the previous step.

To connect to ADLS Gen1:

```sql  
-- For ADLS Gen1 - Create an external data source
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH 
  ( LOCATION = 'adl://<AzureDataLake account_name>.azuredatalakestore.net',
    CREDENTIAL = AzureStorageCredential,
    TYPE = HADOOP
  );
```

To connect to ADLS Gen2:

```sql
-- For ADLS Gen 2 - Create an external data source
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH
  -- Please note the abfss endpoint when your account has secure transfer enabled
  ( LOCATION = 'abfss://<file-system-name>@<storage-account-name>.dfs.core.windows.net/' , 
    CREDENTIAL = ADLS_credential ,
    TYPE = HADOOP
  );
```  


#### 3. Create external file format

```sql  
-- FIELD_TERMINATOR: Marks the end of each field (column) in a delimited text file
-- STRING_DELIMITER: Specifies the field terminator for data of type string in the text-delimited file.
-- DATE_FORMAT: Specifies a custom format for all date and time data that might appear in a delimited text file.
-- Use_Type_Default: Store all Missing values as NULL

CREATE EXTERNAL FILE FORMAT TextFileFormat
WITH
(   FORMAT_TYPE = DELIMITEDTEXT
,    FORMAT_OPTIONS    (   FIELD_TERMINATOR = '|'
                    ,    STRING_DELIMITER = ''
                    ,    DATE_FORMAT         = 'yyyy-MM-dd HH:mm:ss.fff'
                    ,    USE_TYPE_DEFAULT = FALSE
                    )
);
```  

#### 4. Create external table

```sql  
-- LOCATION: Folder under the ADLS root folder.
-- DATA_SOURCE: Specifies which Data Source Object to use.
-- FILE_FORMAT: Specifies which File Format Object to use
-- REJECT_TYPE: Specifies how you want to deal with rejected rows. Either Value or percentage of the total
-- REJECT_VALUE: Sets the Reject value based on the reject type.

-- DimProduct
CREATE EXTERNAL TABLE [dbo].[DimProduct_external] (
    [ProductKey] [int] NOT NULL,
    [ProductLabel] [nvarchar](255) NULL,
    [ProductName] [nvarchar](500) NULL
)
WITH
(
    LOCATION='/DimProduct/'
,   DATA_SOURCE = AzureDataLakeStore
,   FILE_FORMAT = TextFileFormat
,   REJECT_TYPE = VALUE
,   REJECT_VALUE = 0
)
;
```  

#### 5. Create statistics

Finally, manually create a statistics object on the new external table.

```sql
CREATE STATISTICS StatsForProduct on DimProduct_external(ProductKey)  
```  

## Create external tables for SQL Server

For more information and examples, see [Configure PolyBase to access external data in SQL Server](polybase-configure-sql-server.md).

#### 1. Create database scoped credential

 Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

```sql
     -- Create a database master key
      CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';  
```

Then, create a database scoped credential. `IDENTITY` is the user name to authenticate to the SQL Server instance, and `SECRET` is the password. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql
     --  IDENTITY: user name for external source.  
     --  SECRET: password for external source.

     CREATE DATABASE SCOPED CREDENTIAL SqlServerCredentials   
     WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Create the external data source to the other SQL Server. 

- LOCATION should be `<vendor>://<server>[:<port>]`, in this case, `sqlserver://servername` or `sqlserver://servername\instance` or `sqlserver://servername:port`. For [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], use the fully qualified domain name (FQDN) such as `sqlserver://servername.database.windows.net`. Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
- PUSHDOWN is ON by default for PolyBase in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later. Specify whether computation should be pushed down to the source.
- CREDENTIAL is the database scoped credential name created in the previous step.

```sql
    -- LOCATION: Location string should be of format `<vendor>://<server>[:<port>]`.
    -- PUSHDOWN: specify whether computation should be pushed down to the source. ON by default. Applies to: SQL Server 2019 (15.x) and later.
    -- CREDENTIAL: the database scoped credential, created previously.
  
    CREATE EXTERNAL DATA SOURCE SQLServerInstance
    WITH ( 
    LOCATION = 'sqlserver://SqlServer',
    -- PUSHDOWN = ON | OFF,
      CREDENTIAL = SQLServerCredentials
    );
```

#### 3. Create schema

To differentiate from other tables in your database, create a schema for the external tables for this external data source.

```sql
     CREATE SCHEMA sqlserver;
     GO
```

#### 4. Create external table
 
```sql
     -- LOCATION: sql server table/view in 'database_name.schema_name.object_name' format
     -- DATA_SOURCE: the external data source, created in the previous step.
     
     CREATE EXTERNAL TABLE sqlserver.customer(
     C_CUSTKEY INT NOT NULL,
     C_NAME VARCHAR(25) NOT NULL,
     C_ADDRESS VARCHAR(40) NOT NULL,
     C_NATIONKEY INT NOT NULL,
     C_PHONE CHAR(15) NOT NULL,
     C_ACCTBAL DECIMAL(15,2) NOT NULL,
     C_MKTSEGMENT CHAR(10) NOT NULL,
     C_COMMENT VARCHAR(117) NOT NULL
      )
      WITH (
      LOCATION='tpch_10.dbo.customer',
      DATA_SOURCE=SqlServerInstance
     );
 ```

#### 5. Create statistics

Finally, manually create a statistics object on the new external table.

```sql
CREATE STATISTICS CustomerCustKeyStatistics ON sqlserver.customer (C_CUSTKEY) WITH FULLSCAN; 
```

## Create external tables for Oracle

For more information and examples, see [Configure PolyBase to access external data in Oracle](polybase-configure-oracle.md).

#### 1. Create database scoped credential

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

 ```sql
  -- Create a database master key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
```

Then, create a database scoped credential. `IDENTITY` is the user name to authenticate to Oracle, and `SECRET` is the password. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql
   -- IDENTITY: user name for external source.  
   -- SECRET: password for external source.

   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Create the external data source to Oracle data source. 

- LOCATION should be `<vendor>://<server>[:<port>]`, in this case, `oracle://servername` or `oracle://servername:port`. Options for `LOCATION` prefixes may differ in different versions of Oracle. Refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
- PUSHDOWN is ON by default for PolyBase in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later. Specify whether computation should be pushed down to the source.
- CONNECTION_OPTIONS specifies additional options when connecting over ODBC to an external data source. To use multiple connection options, separate them by a semi-colon.

- CREDENTIAL is the database scoped credential name created in the previous step.

```sql
   -- LOCATION: Location string should be of format `<vendor>://<server>[:<port>]`.
   -- PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
   -- CREDENTIAL: the database scoped credential, created in the previous step.
     
   CREATE EXTERNAL DATA SOURCE external_data_source_name
   WITH ( 
   LOCATION = 'oracle://<server address>[:<port>]',
   -- PUSHDOWN = ON | OFF,
   -- CONNECTION_OPTIONS = ImpersonateUser 
   CREDENTIAL = credential_name)
```

#### 3. Create external table

Create an external table to query the external data with T-SQL commands.

- LOCATION: Oracle table/view in '<database_name>.<schema_name>.<object_name>' format. Note this may be case sensitive in the Oracle database.
- DATA_SOURCE: the external data source, created in the previous step.

```sql
   -- LOCATION: Oracle table/view in '<database_name>.<schema_name>.<object_name>' format. Note this may be case sensitive in the Oracle database.
   -- DATA_SOURCE: the external data source, created in the previous step.

   CREATE EXTERNAL TABLE customers(
   [O_ORDERKEY] DECIMAL(38) NOT NULL,
   [O_CUSTKEY] DECIMAL(38) NOT NULL,
   [O_ORDERSTATUS] CHAR COLLATE Latin1_General_BIN NOT NULL,
   [O_TOTALPRICE] DECIMAL(15,2) NOT NULL,
   [O_ORDERDATE] DATETIME2(0) NOT NULL,
   [O_ORDERPRIORITY] CHAR(15) COLLATE Latin1_General_BIN NOT NULL,
   [O_CLERK] CHAR(15) COLLATE Latin1_General_BIN NOT NULL,
   [O_SHIPPRIORITY] DECIMAL(38) NOT NULL,
   [O_COMMENT] VARCHAR(79) COLLATE Latin1_General_BIN NOT NULL
   )
   WITH (
   LOCATION='database_name.schema_name.customer',
   DATA_SOURCE=  external_data_source_name
   );
```

#### 4. Create statistics

Finally, manually create a statistics object on the new external table.

```sql
   CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
```

## Create external tables for Teradata

For more information and examples, see [Configure PolyBase to access external data in Teradata](polybase-configure-teradata.md).

#### 1. Create database scoped credential

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

 ```sql
   -- Create a database master key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
```

Then, create a database scoped credential. `IDENTITY` is the user name to authenticate to Teradata, and `SECRET` is the password. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql
   -- IDENTITY: user name for external source.  
   -- SECRET: password for external source.

   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

- LOCATION should be of format `<vendor>://<server>[:<port>]`.
- PUSHDOWN is ON by default for PolyBase in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later. Specify whether computation should be pushed down to the source.
- CONNECTION_OPTIONS should be specified for [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later, as needed. Specifies additional options when connecting over ODBC to an external data source. To use multiple connection options, separate them by a semi-colon.
- CREDENTIAL is the database scoped credential name created in the previous step.

```sql
    -- LOCATION: Location string should be of format `<vendor>://<server>[:<port>]`.
    -- PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    -- CONNECTION_OPTIONS: Specify driver location
    -- CREDENTIAL: the database scoped credential, created in the previous step.
  
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH ( 
    LOCATION = teradata://<server address>[:<port>],
   -- PUSHDOWN = ON | OFF,
    CREDENTIAL =credential_name
    );
```

#### 3. Create external table

- LOCATION: Teradata table/view in '<database_name>.<schema_name>.<object_name>' format.
- DATA_SOURCE: the external data source, created in the previous step.
 
```sql
     -- LOCATION: Teradata table/view in '<database_name>.<object_name>' format
     -- DATA_SOURCE: the external data source, created in the previous step.

     CREATE EXTERNAL TABLE customer(
     L_ORDERKEY INT NOT NULL,
     L_PARTKEY INT NOT NULL,
     L_SUPPKEY INT NOT NULL,
     L_LINENUMBER INT NOT NULL,
     L_QUANTITY DECIMAL(15,2) NOT NULL,
     L_EXTENDEDPRICE DECIMAL(15,2) NOT NULL,
     L_DISCOUNT DECIMAL(15,2) NOT NULL,
     L_TAX DECIMAL(15,2) NOT NULL,
     L_RETURNFLAG CHAR NOT NULL,
     L_LINESTATUS CHAR NOT NULL,
     L_SHIPDATE DATE NOT NULL,
     L_COMMITDATE DATE NOT NULL,
     L_RECEIPTDATE DATE NOT NULL,
     L_SHIPINSTRUCT CHAR(25) NOT NULL,
     L_SHIPMODE CHAR(10) NOT NULL,
     L_COMMENT VARCHAR(44) NOT NULL
     )
     WITH (
     LOCATION='customer',
     DATA_SOURCE= external_data_source_name
     );
```

#### 4. Create statistics

Finally, manually create a statistics object on the new external table.

```sql
      CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
```

## Create external tables for MongoDB

For additional examples for MongoDB, see [Configure PolyBase to access external data in MongoDB](polybase-configure-mongodb.md).

#### 1. Create database scoped credential

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

```sql
  -- Create a database master key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
```

Then, create a database scoped credential. `IDENTITY` is the user name to authenticate to MongoDB, and `SECRET` is the password. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql
   -- IDENTITY: user name for external source.  
   -- SECRET: password for external source.

   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', SECRET = 'password';
```

#### 2. Create external data source

- LOCATION should be `mongodb://<server>[:<port>]`. Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
- PUSHDOWN specifies whether computation should be pushed down to the source. ON by default.
- CONNECTION_OPTIONS should be specified for [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later, as needed. Specifies additional options when connecting over ODBC to an external data source. To use multiple connection options, separate them by a semi-colon.
- CREDENTIAL is the database scoped credential, created in the previous step.

```sql
   -- LOCATION: Location string should be of format '<type>://<server>[:<port>]'.
   -- PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
   -- CONNECTION_OPTIONS: Specify driver location for PolyBase in SQL Server 2019 (15.x) and later.
   -- CREDENTIAL: the database scoped credential, created in the previous step.
     
   CREATE EXTERNAL DATA SOURCE external_data_source_name
   WITH (
   LOCATION = mongodb://<server>[:<port>],
   -- PUSHDOWN = ON | OFF,
     CREDENTIAL = credential_name
   );
```

#### 3. Create external table

If you use the [Data Virtualization extension for Azure Data Studio](../../azure-data-studio/extensions/data-virtualization-extension.md), you can skip this step, as the CREATE EXTERNAL TABLE statement is generated for you and can be further customized from there. To provide the schema manually, consider the following sample script to create an external table. For more information, see [Configure PolyBase to access external data in MongoDB](polybase-configure-mongodb.md).

- LOCATION: MongoDB object in '<database_name>.<table_name>' format. Note three-part names are not allowed.
- DATA_SOURCE: the external data source, created in the previous step.

```sql
     -- LOCATION: MongoDB table/view in '<database_name>.<table_name>' format.
     -- DATA_SOURCE: the external data source, created in the previous step.

     CREATE EXTERNAL TABLE customers(
     [O_ORDERKEY] DECIMAL(38) NOT NULL,
     [O_CUSTKEY] DECIMAL(38) NOT NULL,
     [O_ORDERSTATUS] CHAR COLLATE Latin1_General_BIN NOT NULL,
     [O_TOTALPRICE] DECIMAL(15,2) NOT NULL,
     [O_ORDERDATE] DATETIME2(0) NOT NULL,
     [O_COMMENT] VARCHAR(79) COLLATE Latin1_General_BIN NOT NULL
     )
     WITH (
     LOCATION='MyDb.customer',
     DATA_SOURCE= external_data_source_name
     );
```

#### 4. Create statistics

Finally, manually create a statistics object on the new external table.

```sql
      CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
```

## Create external table for CSV

Applies to: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.

For more information and examples, see [Virtualize CSV file with PolyBase](virtualize-csv.md).

#### 1. Create a master key and database scoped credential

The database master key in the user database is required to encrypt the database scoped credential secret, `blob_storage`.

```sql
USE [CSV_Demo];
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
```

```sql
CREATE DATABASE SCOPED CREDENTIAL blob_storage   
WITH IDENTITY = '<user_name>', Secret = '<password>';  
```

### 2. Create external data source

Database scoped credential will be used for the external data source. In this example, the CSV file resides in Azure Blob Storage, so use prefix `abs` and the `SHARED ACCESS SIGNATURE` identity method. Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use a new prefix `abs` for Azure Storage Account v2. For more information about the connectors and prefixes, including new settings for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], refer to [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#location--prefixpathport-3).

```sql
CREATE EXTERNAL DATA SOURCE Blob_CSV
WITH
(
 LOCATION = 'abs://<container>@<storage_account>.blob.core.windows.net'
,CREDENTIAL = blob_storage 
);
```

#### 3. Create external file format

 The columns must be defined and strongly typed. To define the file's formatting, an external file format is required. External file formats are also recommended due to reusability. In the following example, the data starts on the second row. 

```sql
CREATE EXTERNAL FILE FORMAT csv_ff
WITH
(   FORMAT_TYPE = DELIMITEDTEXT
,   FORMAT_OPTIONS  (    FIELD_TERMINATOR = ','
                    ,    STRING_DELIMITER = '"'
                    ,    FIRST_ROW = 2 )
);
```

#### 4. Create external table

- LOCATION is the file path of the `call_center.csv` file relative to the path of the location in the external data source. In this case, the file lies in a subfolder called `2022`.
- DATA_SOURCE is the external data source. 
- FILE_FORMAT is the path to the `csv_ff` external file format in the SQL Server.

```sql
CREATE EXTERNAL TABLE extCall_Center_csv
(
            cc_call_center_sk         integer             NOT NULL  ,
            cc_call_center_id         char(16)            NOT NULL  ,
            cc_rec_start_date         date                          ,
            cc_rec_end_date           date                          ,
            cc_closed_date_sk         integer                       ,
            cc_open_date_sk           integer                       ,
            cc_name                   varchar(50)                   ,
            cc_class                  varchar(50)                   ,
            cc_employees              integer                       ,
            cc_sq_ft                  integer                       ,
            cc_hours                  char(20)                      ,
            cc_manager                varchar(40)                   ,
            cc_mkt_id                 integer                       ,
            cc_mkt_class              char(50)                      ,
            cc_mkt_desc               varchar(100)                  ,
            cc_market_manager         varchar(MAX)                  ,
            cc_division               varchar(50)                   ,
            cc_division_name          varchar(50)                   ,
            cc_company                varchar(60)                   ,
            cc_company_name           char(50)                      ,
            cc_street_number          char(10)                      ,
            cc_street_name            varchar(60)                   ,
            cc_street_type            char(15)                      ,
            cc_suite_number           char(10)                      ,
            cc_city                   varchar(60)                   ,
            cc_county                 varchar(30)                   ,
            cc_state                  char(20)                      ,
            cc_zip                    char(20)                      ,
            cc_country                varchar(MAX)                  ,
            cc_gmt_offset             decimal(5,2)                  ,
            cc_tax_percentage         decimal(5,2) 
)
WITH
(
    LOCATION = '/2022/call_center.csv',
    DATA_SOURCE = Blob_CSV
    ,FILE_FORMAT = csv_ff
)
GO
```

## Create external table for delta table

Applies to: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.

For more information and examples, see [Virtualize delta table with PolyBase](virtualize-delta.md).

#### 1. Create a master key and database scoped credential

Create a database master key on the database if one does not already exist. This is required to encrypt the credential secret.  

```sql
  -- Create a database master key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
```

Then, create a database scoped credential. For this example, the delta table resides on Azure Data Lake Storage Gen2. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql
   -- IDENTITY: user name for external source.  
   -- SECRET: password for external source.

   CREATE DATABASE SCOPED CREDENTIAL delta_storage_dsc   
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE', 
   SECRET = '<SAS Token>';  
```

#### 2. Create external data source


Database scoped credential will be used for the external data source. In this example, the delta table resides in Azure Data Lake Storage Gen2, so use prefix `adls` and the `SHARED ACCESS SIGNATURE` identity method. For more information about the connectors and prefixes, including new settings for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], refer to [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#location--prefixpathport-3).

```sql
CREATE EXTERNAL DATA SOURCE Delta_ED
WITH
(
 LOCATION = 'adls://<container>@<storage_account>.dfs.core.windows.net'
,CREDENTIAL = delta_storage_dsc 
);
```

For example, if your storage account is named `delta_lake_sample` and the container is named `sink`, the code would be:

```sql
CREATE EXTERNAL DATA SOURCE Delta_ED
WITH
(
 LOCATION = 'abs://sink@delta_lake_sample.dfs.core.windows.net'
,CREDENTIAL = delta_storage_dsc
)
```

#### 3. Create external file format

To define the file's formatting, an external file format is required. External file formats are also recommended due to reusability. For more information, see [CREATE EXTERNAL FILE FORMAT](../../t-sql/statements/create-external-file-format-transact-sql.md).


```sql
CREATE EXTERNAL FILE FORMAT DeltaTableFormat WITH(FORMAT_TYPE = DELTA);
```

#### 4. Create external table

The delta table files are located at `/delta/Delta_yob/` and the external data source for this example is S3-compatible object storage, previously configured under the data source `s3_eds`. PolyBase can use the as LOCATION the delta table folder or the absolute file itself, which would be located at `delta/Delta_yob/_delta_log/00000000000000000000.json`.

```sql
-- Create External Table using delta
CREATE EXTERNAL TABLE extCall_Center_delta 
(      id int, 
       name VARCHAR(200),
       dob date
)WITH 
(     LOCATION = '/delta/Delta_yob/'
    , FILE_FORMAT = DeltaTableFormat
    , DATA_SOURCE = s3_eds
)
GO
```

## Next steps  

For examples of queries, see [PolyBase Queries](../../relational-databases/polybase/polybase-queries.md).  

## See also  

 - [Get started with PolyBase](polybase-guide.md)
 - [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)
 - [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
 - [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
