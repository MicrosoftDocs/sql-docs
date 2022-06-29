---
title: "PolyBase Transact-SQL reference"
description: "Use PolyBase to query your external data in Hadoop, Azure blob storage, Azure Data Lake Store, SQL Server, Oracle, Teradata, MongoDB, or CSV files."
ms.date: 06/29/2022
ms.prod: sql
ms.technology: polybase
ms.topic: reference
helpviewer_keywords: 
  - "PolyBase, fundamentals"
  - "PolyBase, SQL statements"
  - "PolyBase, SQL objects"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-2016"
---
# PolyBase Transact-SQL reference

[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article reviews options for using [PolyBase](polybase.md) to query external data in-place, referred to as data virtualization, for a variety of external data sources. 

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

## Prerequisites  

To get started, install PolyBase on your SQL Server in [Windows](polybase-installation.md) or [Linux](polybase-linux-setup.md), then see [PolyBase configuration](polybase-configuration.md).
 
## Create external tables for Hadoop

Applies to: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)]

#### 1. Create database scoped credential

This step is required only for Kerberos-secured Hadoop clusters. 

Create a master key on the database. This is required to encrypt the credential secret.  

```sql
-- Create a master key on the database.  
-- Required to encrypt the credential secret.  
  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';  
```

Then, create a database scoped credential for Kerberos-secured Hadoop clusters. `IDENTITY` is the Kerberos user name and `SECRET` is the Kerberos password.

```sql  
-- Create a database scoped credential  for Kerberos-secured Hadoop clusters.  
-- IDENTITY: the Kerberos user name.  
-- SECRET: the Kerberos password  
  
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1   
WITH IDENTITY = '<hadoop_user_name>', Secret = '<hadoop_password>';  
```

#### 2. Create external data source

Create an external data source. 

- `LOCATION` is required and is the Hadoop name node IP address and port. 
- `RESOURCE MANAGER LOCATION` is optional. This is the Hadoop Resource Manager location to enable [pushdown computation](polybase-pushdown-computation.md). 
- `CREDENTIAL` is the database scoped credential, created in the previous step.
  
Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

```sql  
-- Create an external data source.  
-- LOCATION (Required) : Hadoop Name Node IP address and port.  
-- RESOURCE MANAGER LOCATION (Optional): Hadoop Resource Manager location to enable pushdown computation.  
-- CREDENTIAL (Optional):  the database scoped credential, created above.  
  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster WITH (  
        TYPE = HADOOP,   
        LOCATION ='hdfs://10.xxx.xx.xxx:xxxx',   
        RESOURCE_MANAGER_LOCATION = '10.xxx.xx.xxx:xxxx',   
        CREDENTIAL = HadoopUser1      
);  
```

#### 3. Create external file format

Create an external file format, where `FORMAT_TYPE` ius the format in Hadoop, such as `DELIMITEDTEXT`, `RCFILE`, `ORC`, or `PARQUET`. 

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

## Create external tables for Azure blob storage  

Applies to: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)]

#### 1. Create database scoped credential

```sql  
-- Create a master key on the database.  
-- Required to encrypt the credential secret.  
  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';  
  
-- Create a database scoped credential  for Azure blob storage.  
-- IDENTITY: any string (this is not used for authentication to Azure storage).  
-- SECRET: your Azure storage account key.  
  
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential   
WITH IDENTITY = 'user', Secret = '<azure_storage_account_key>';  
```  

#### 2. Create external data source

- `LOCATION` is required and is the Azure account storage account name and blob container name. 
- `CREDENTIAL` is the database scoped credential, created in the previous step.

Be aware that options for `TYPE` and `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

```sql  
-- Create an external data source.  
-- LOCATION:  Azure account storage account name and blob container name.  
-- CREDENTIAL: The database scoped credential created above.  
  
CREATE EXTERNAL DATA SOURCE AzureStorage with (  
        TYPE = BLOB_STORAGE,   
        LOCATION ='wasbs://<blob_container_name>@<azure_storage_account_name>.blob.core.windows.net',  
        CREDENTIAL = AzureStorageCredential  
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
CREATE STATISTICS StatsForSensors on CarSensor_Data(CustomerKey, Speed)  
  
```  

## Create external tables for Azure Data Lake Store
Applies to: [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]

For more information, see [Load with Azure Data Lake Store](/azure/sql-data-warehouse/sql-data-warehouse-load-from-azure-data-lake-store)

#### 1. Create database scoped credential

```sql
-- Create a Database Master Key.
-- Only necessary if one does not already exist.
-- Required to encrypt the credential secret in the next step.

CREATE MASTER KEY;

-- Create a database scoped credential
-- IDENTITY: Pass the client id and OAuth 2.0 Token Endpoint taken from your Azure Active Directory Application
-- SECRET: Provide your AAD Application Service Principal key.
-- For more information on Create database scoped credential: https://msdn.microsoft.com/library/mt270260.aspx

CREATE DATABASE SCOPED CREDENTIAL ADL_User
WITH
    IDENTITY = '<client_id>@<OAuth_2.0_Token_EndPoint>'
    ,SECRET = '<key>'
;
```  

#### 2. Create external data source to reference Azure Data Lake Store (ADLS) Gen 1 or 2

- `LOCATION` is required and is the Azure account storage account name and blob container name. 
- `CREDENTIAL` is the database scoped credential, created in the previous step.

Be aware that options for `TYPE` and `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

For ADLS Gen1 (not recommended):

```sql  
-- For ADLS Gen1 - Create an external data source
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH 
  ( LOCATION = 'adl://<AzureDataLake account_name>.azuredatalakestore.net',
    CREDENTIAL = AzureStorageCredential,
    TYPE = HADOOP
  );
```

For ADLS Gen2 prior to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use `abfss`:

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

Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use a new prefix `adls` for ADLS Gen2:

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

#### 1. Create database scoped credential

```sql
     -- Create a Master Key
      CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';  
 
     /*  specify credentials to external data source
     *  IDENTITY: user name for external source.  
     *  SECRET: password for external source.
     */
     CREATE DATABASE SCOPED CREDENTIAL SqlServerCredentials   
     WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Create the external data source to the other SQL Server. 

Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

```sql
    /*  LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
    *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default. Applies to: SQL Server 2019 (15.x) and later.
    *  CREDENTIAL: the database scoped credential, created above.
    */  
    CREATE EXTERNAL DATA SOURCE SQLServerInstance
    WITH ( 
    LOCATION = 'sqlserver://SqlServer',
    -- PUSHDOWN = ON | OFF,
      CREDENTIAL = SQLServerCredentials
    );
```

#### 3. Create schema

```sql
     CREATE SCHEMA sqlserver;
     GO
```

#### 4. Create external table
 
```sql
     /*  LOCATION: sql server table/view in 'database_name.schema_name.object_name' format
     *  DATA_SOURCE: the external data source, created above.
     */
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

#### 1. Create database scoped credential

 ```sql
  -- Create a Master Key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  

   /*  
   * Specify credentials to external data source
   * IDENTITY: user name for external source.  
   * SECRET: password for external source.
   */
   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

  ```sql
   /* 
   * LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
   * PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
   * CONNECTION_OPTIONS: Specify driver location
   * CREDENTIAL: the database scoped credential, created above.
   */  
   CREATE EXTERNAL DATA SOURCE external_data_source_name
   WITH ( 
     LOCATION = 'oracle://<server address>[:<port>]',
     -- PUSHDOWN = ON | OFF,
     CREDENTIAL = credential_name)
   ```

#### 3. Create external table
 
```sql
   /*
   * LOCATION: Oracle table/view in '<database_name>.<schema_name>.<object_name>' format
   * DATA_SOURCE: the external data source, created above.
   */
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
    LOCATION='customer',
    DATA_SOURCE=  external_data_source_name
   );
   ```

#### 4. Create statistics

Finally, manually create a statistics object on the new external table.

  ```sql
   CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
   ```

## Create external tables for Teradata

#### 1. Create database scoped credential

 ```sql
  -- Create a Master Key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  

   /*  
   * Specify credentials to external data source
   * IDENTITY: user name for external source.  
   * SECRET: password for external source.
   */
   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

```sql
    /*  LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
    *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    * CONNECTION_OPTIONS: Specify driver location
    *  CREDENTIAL: the database scoped credential, created above.
    */  
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH ( 
    LOCATION = teradata://<server address>[:<port>],
   -- PUSHDOWN = ON | OFF,
    CREDENTIAL =credential_name
    );

```

#### 3. Create external table
 
```sql
     /*  LOCATION: Teradata table/view in '<database_name>.<object_name>' format
      *  DATA_SOURCE: the external data source, created above.
      */
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

#### 1. Create database scoped credential

 ```sql
  -- Create a Master Key
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  

   /*  
   * Specify credentials to external data source
   * IDENTITY: user name for external source.  
   * SECRET: password for external source.
   */
   CREATE DATABASE SCOPED CREDENTIAL credential_name
   WITH IDENTITY = 'username', Secret = 'password';
```

#### 2. Create external data source

Be aware that options for `LOCATION` prefixes vary in different versions of SQL Server and platforms in Azure SQL, always refer to [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

```sql
     /*  LOCATION: Location string should be of format '<type>://<server>[:<port>]'.
    *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    *CONNECTION_OPTIONS: Specify driver location
    *  CREDENTIAL: the database scoped credential, created above.
    */  
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH (
    LOCATION = mongodb://<server>[:<port>],
    -- PUSHDOWN = ON | OFF,
      CREDENTIAL = credential_name
    );
```

#### 3. Create external table
 
```sql
     /*  LOCATION: MongoDB table/view in '<database_name>.<schema_name>.<object_name>' format
     *  DATA_SOURCE: the external data source, created above.
     */
     CREATE EXTERNAL TABLE customers(
     [O_ORDERKEY] DECIMAL(38) NOT NULL,
     [O_CUSTKEY] DECIMAL(38) NOT NULL,
     [O_ORDERSTATUS] CHAR COLLATE Latin1_General_BIN NOT NULL,
     [O_TOTALPRICE] DECIMAL(15,2) NOT NULL,
     [O_ORDERDATE] DATETIME2(0) NOT NULL,
     [O_COMMENT] VARCHAR(79) COLLATE Latin1_General_BIN NOT NULL
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

## Next steps  

For examples of queries, see [PolyBase Queries](../../relational-databases/polybase/polybase-queries.md).  

## See also  

 - [Get started with PolyBase](./polybase-guide.md)   
 - [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)
 - [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
 - [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)