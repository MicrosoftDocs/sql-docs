---
title: COPY INTO (Transact-SQL) (preview) 
titleSuffix: (Azure Synapse Analytics) - SQL Server
description: Use the COPY statement in Azure Synapse Analytics for loading from external storage accounts.
ms.date: 08/05/2020
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse"
ms.reviewer: jrasnick
ms.technology: t-sql
ms.topic: language-reference
f1_keywords: 
  - "COPY_TSQL"
  - "COPY INTO"
  - "COPY"
  - "LOAD"
dev_langs: 
  - "TSQL"
author: kevinvngo
ms.author: kevin
monikerRange: "=sqlallproducts-allversions||=azure-sqldw-latest"
---
# COPY (Transact-SQL) (preview)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

This article explains how to use the COPY statement in [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] for loading from external storage accounts. The COPY statement provides the most flexibility for high-throughput data ingestion into [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]. Use COPY for the following capabilities:

- Use lower privileged users to load without needing strict CONTROL permissions on the data warehouse
- Execute a single T-SQL statement without having to create any additional database objects
- Properly parse and load CSV files where **delimiters** (string, field, row) **are** **escaped within string delimited columns**
- Specify a finer permission model without exposing storage account keys using Share Access Signatures (SAS)
- Use a different storage account for the ERRORFILE location (REJECTED_ROW_LOCATION)
- Customize default values for each target column and specify source data fields to load into specific target columns
- Specify a custom row terminator for CSV files
- Leverage SQL Server Date formats for CSV files
- Specify wildcards and multiple files in the storage location path

> [!NOTE]  
> The COPY statement is currently in public preview.

Visit the following documentation for comprehensive examples and quickstarts using the COPY statement:

- [Quickstart: Bulk load data using the COPY statement](https://docs.microsoft.com/azure/synapse-analytics/sql-data-warehouse/quickstart-bulk-load-copy-tsql)
- [Quickstart: Examples using the COPY statement and its supported authentication methods](https://docs.microsoft.com/azure/synapse-analytics/sql-data-warehouse/quickstart-bulk-load-copy-tsql-examples)
- [Quickstart: Creating the COPY statement using the rich Synapse Studio UI (Workspace preview)](https://docs.microsoft.com/azure/synapse-analytics/quickstart-load-studio-sql-pool)

## Syntax  

```syntaxsql
COPY INTO [schema.]table_name
[(Column_list)] 
FROM '<external_location>' [,...n]
WITH  
 ( 
 [FILE_TYPE = {'CSV' | 'PARQUET' | 'ORC'} ]
 [,FILE_FORMAT = EXTERNAL FILE FORMAT OBJECT ]	
 [,CREDENTIAL = (AZURE CREDENTIAL) ]
 [,ERRORFILE = '[http(s)://storageaccount/container]/errorfile_directory[/]]' 
 [,ERRORFILE_CREDENTIAL = (AZURE CREDENTIAL) ]
 [,MAXERRORS = max_errors ] 
 [,COMPRESSION = { 'Gzip' | 'DefaultCodec'| 'Snappy'}] 
 [,FIELDQUOTE = 'string_delimiter'] 
 [,FIELDTERMINATOR =  'field_terminator']  
 [,ROWTERMINATOR = 'row_terminator']
 [,FIRSTROW = first_row]
 [,DATEFORMAT = 'date_format'] 
 [,ENCODING = {'UTF8'|'UTF16'}] 
 [,IDENTITY_INSERT = {'ON' | 'OFF'}]
)
```

## Arguments  

*schema_name*  
Is optional if the default schema for the user performing the operation is the schema of the specified table. If *schema* is not specified, and the default schema of the user performing the COPY operation is different from the specified table, COPY will be canceled, and an error message will be returned.  

*table_name*  
Is the name of the table to COPY data into. The target table can be a temporary or permanent table and must already exist in the database. 

*(column_list)*  
Is an optional list of one or more columns used to map source data fields to target table columns for loading data. *column_list* must be enclosed in parentheses and delimited by commas. The column list is of the following format:

[(Column_name [Default_value] [Field_number] [,...n])]

- *Column_name* - the name of the column in the target table.
- *Default_value* - the default value that will replace any NULL value in the input file. Default value applies to all file formats. COPY will attempt to load NULL from the input file when a column is omitted from the column list or when there is an empty input file field.
- *Field_number* - the input file field number that will be mapped to the target column name.
- The field indexing starts at 1.

When a column list is not specified, COPY will map columns based on the source and target ordinality: Input field 1 will go to target column 1, field 2 will go to column 2, etc.

*External locations(s)*</br>
Is where the files containing the data is staged. Currently Azure Data Lake Storage (ADLS) Gen2 and Azure Blob Storage are supported:

- *External location* for Blob Storage: https://<account>.blob.core.windows.net/<container>/<path>
- *External location* for ADLS Gen2: https://<account>. dfs.core.windows.net/<container>/<path>

> [!NOTE]  
> The blob endpoint is available for ADLS Gen2 for backward compatibility. Use the **blob** endpoint for best performance.

- *Account* - The storage account name

- *Container* - The blob container name

- *Path* - the folder or file path for the data. The location starts from the container. If a folder is specified, COPY will retrieve all files from the folder and all its subfolders. COPY ignores hidden folders and doesn't return files that begin with an underline (_) or a period (.) unless explicitly specified in the path. This behavior is the same even when specifying a path with a wildcard.

Wildcards cards can be included in the path where

- Wildcard path name matching is case-sensitive
- Wildcard can be escaped using the backslash character (\\)
- Wildcard expansion is applied recursively. For instance, all CSV files under Customer1 (including subdirectories of Customer1 will be loaded in the following example: ‘Account/Container/Customer1/*.csv’

> [!NOTE]  
> For best performance, avoid specifying wildcards that would expand over a larger number of files. If possible, list multiple file locations instead of specifying wildcards.

Multiple file locations can only be specified from the same storage account and container via a comma-separated list such as:

- ‘https://<account>.blob.core.windows.net/<container>/<path>’, ‘https://<account>.blob.core.windows.net/<container>/<path>’…

*FILE_TYPE = { ‘CSV’ | ‘PARQUET’ | ‘ORC’ }*</br>
*FILE_TYPE* specifies the format of the external data.

- CSV: Specifies a comma-separated values file compliant to the [RFC 4180](https://tools.ietf.org/html/rfc4180) standard.
- PARQUET: Specifies a Parquet format.
- ORC: Specifies an Optimized Row Columnar (ORC) format.

>[!NOTE]  
>The file type 'Delimited Text' in Polybase is replaced by the ‘CSV’ file format where the default comma delimiter can be configured via the FIELDTERMINATOR parameter. 

*FILE_FORMAT = external_file_format_name*</br>
*FILE_FORMAT* applies to Parquet and ORC files only and specifies the name of the external file format object that stores the file type and compression method for the external data. To create an external file format, use [CREATE EXTERNAL FILE FORMAT](create-external-file-format-transact-sql.md?view=azure-sqldw-latest).

*CREDENTIAL (IDENTITY = ‘’, SECRET = ‘’)*</br>
*CREDENTIAL* specifies the authentication mechanism to access the external storage account. Authentication methods are:

|                          |                CSV                |              Parquet               |                ORC                 |
| :----------------------: | :-------------------------------: | :-------------------------------:  | :-------------------------------:  |
|  **Azure blob storage**  | SAS/MSI/SERVICE PRINCIPAL/KEY/AAD |              SAS/KEY               |              SAS/KEY               |
| **Azure Data Lake Gen2** | SAS/MSI/SERVICE PRINCIPAL/KEY/AAD | SAS (blob endpoint)/MSI (dfs endpoint)/SERVICE PRINCIPAL/KEY/AAD | SAS (blob endpoint)/MSI (dfs endpoint)/SERVICE PRINCIPAL/KEY/AAD |


When authenticating using AAD or to a public storage account, CREDENTIAL does not need to be specified. 

- Authenticating with Shared Access Signatures (SAS)
  
  - *IDENTITY: A constant with a value of ‘Shared Access Signature’*
  - *SECRET: The* [*shared access signature*](/azure/storage/common/storage-sas-overview) *provides delegated access to resources in your storage account.*
  -  Minimum permissions required: READ and LIST
  
- Authenticating with [*Service Principals*](/azure/sql-data-warehouse/sql-data-warehouse-load-from-azure-data-lake-store#create-a-credential)

  - *IDENTITY: <ClientID>@<OAuth_2.0_Token_EndPoint>*
  - *SECRET: AAD Application Service Principal key*
  -  Minimum RBAC roles required: Storage blob data contributor, Storage blob data contributor, Storage blob data owner, or Storage blob data reader

- Authenticating with Storage account key
  
  - *IDENTITY: A constant with a value of ‘Storage Account Key’*
  - *SECRET: Storage account key*
  
- Authenticating with [Managed Identity](/azure/sql-data-warehouse/load-data-from-azure-blob-storage-using-polybase#authenticate-using-managed-identities-to-load-optional) (VNet Service Endpoints)
  
  - *IDENTITY: A constant with a value of ‘Managed Identity’*
  - Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the AAD registered SQL Database server
  
- Authenticating with an AAD user
  
  - *CREDENTIAL is not required*
  - Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the AAD user

*ERRORFILE = Directory Location*</br>
*ERRORFILE* only applies to CSV. Specifies the directory within the COPY statement where the rejected rows and the corresponding error file should be written. The full path from the storage account can be specified or the path relative to the container can be specified. If the specified path doesn't exist, one will be created on your behalf. A child directory is created with the name "_rejectedrows". The "_" character ensures that the directory is escaped for other data processing unless explicitly named in the location parameter. 

Within this directory, there's a folder created based on the time of load submission in the format YearMonthDay -HourMinuteSecond (Ex. 20180330-173205). In this folder, two types of files are written, the reason (Error) file and the data (Row) file each pre-appending with the queryID, distributionID, and a file guid. Because the data and the reason are in separate files, corresponding files have a matching prefix.

If ERRORFILE has the full path of the storage account defined, then the ERRORFILE_CREDENTIAL will be used to connect to that storage. Otherwise, the value mentioned for CREDENTIAL will be used.

*ERRORFILE_CREDENTIAL = (IDENTITY= ‘’, SECRET = ‘’)*</br>
*ERRORFILE_CREDENTIAL* only applies to CSV files. Supported data source and authentication methods are:

- Azure Blob Storage  - SAS/SERVICE PRINCIPAL/KEY/AAD
- Azure Data Lake Gen2 -   SAS/MSI/SERVICE PRINCIPAL/KEY/AAD
  
- Authenticating with Shared Access Signatures (SAS)
  - *IDENTITY: A constant with a value of ‘Shared Access Signature’*
  - *SECRET: The* [*shared access signature*](/azure/storage/common/storage-sas-overview) *provides delegated access to resources in your storage account.*
  - Minimum permissions required: READ, LIST, WRITE, CREATE, DELETE
  
- Authenticating with [*Service Principals*](/azure/sql-data-warehouse/sql-data-warehouse-load-from-azure-data-lake-store#create-a-credential)
  - *IDENTITY: <ClientID>@<OAuth_2.0_Token_EndPoint>*
  - *SECRET: AAD Application Service Principal key*
  - Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner
  
> [!NOTE]  
> Use the OAuth 2.0 token endpoint **V1**

- Authenticating with Storage account key
  - *IDENTITY: A constant with a value of ‘Storage Account Key’*
  - *SECRET: Storage account key*
  
- Authenticating with [Managed Identity](/azure/sql-data-warehouse/load-data-from-azure-blob-storage-using-polybase#authenticate-using-managed-identities-to-load-optional) (VNet Service Endpoints)
  - *IDENTITY: A constant with a value of ‘Managed Identity’*
  - Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the AAD registered SQL Database server
  
- Authenticating with an AAD user
  - *CREDENTIAL is not required*
  - Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the AAD user

> [!NOTE]  
> If you are using the same storage account for your ERRORFILE and specifying the ERRORFILE path relative to the root of the container, you do not need to specify the ERROR_CREDENTIAL.

*MAXERRORS = max_errors*</br>
*MAXERRORS* specifies the maximum number of reject rows allowed in the load before the COPY operation is canceled. Each row that cannot be imported by the COPY operation is ignored and counted as one error. If max_errors is not specified, the default is 0.

*COMPRESSION = { 'DefaultCodec '\| ’Snappy’ \| ‘GZIP’ \| ‘NONE’}*</br>
*COMPRESSION* is optional and specifies the data compression method for the external data.

- CSV supports GZIP
- Parquet supports GZIP and Snappy
- ORC supports DefaultCodec and Snappy.
- Zlib is the default compression for ORC

The COPY command will autodetect the compression type based on the file extension when this parameter is not specified:

- .gz  - **GZIP**
- .snappy – **Snappy**
- .deflate - **DefaultCodec**  (Parquet and ORC only)

 *FIELDQUOTE = 'field_quote'*</br>
*FIELDQUOTE* applies to CSV and specifies a single character that will be used as the quote character (string delimiter) in the CSV file. If not specified, the quote character (") will be used as the quote character as defined in the RFC 4180 standard. Extended ASCII and multi-byte characters and are not supported with UTF-8 for FIELDQUOTE.

> [!NOTE]  
> FIELDQUOTE characters are escaped in string columns where there is a presence of a double FIELDQUOTE (delimiter). 

*FIELDTERMINATOR = 'field_terminator’*</br>
*FIELDTERMINATOR* Only applies to CSV. Specifies the field terminator that will be used in the CSV file. The field terminator can be specified using hexadecimal notation. The field terminator can be multi-character. The default field terminator is a (,). Extended ASCII and multi-byte characters and are not supported with UTF-8 for FIELDTERMINATOR.

ROW TERMINATOR = 'row_terminator'</br>
*ROW TERMINATOR* Only applies to CSV. Specifies the row terminator that will be used in the CSV file. The row terminator can be specified using hexadecimal notation. The row terminator can be multi-character. By default, the row terminator is \r\n. 

The COPY command prefixes the \r character when specifying \n (newline) resulting in \r\n. To specify only the \n character, use hexadecimal notation (0x0A). When specifying multi-character row terminators in hexadecimal, do not specify 0x between each character.

Extended ASCII and multi-byte characters and are not supported with UTF-8 for ROW TERMINATOR.

*FIRSTROW  = First_row_int*</br>
*FIRSTROW* applies to CSV and specifies the row number that is read first in all files for the COPY command. Values start from 1, which is the default value. If the value is set to two, the first row in every file (header row) is skipped when the data is loaded. Rows are skipped based on the existence of row terminators.

*DATEFORMAT = { ‘mdy’ \| ‘dmy’ \| ‘ymd’ \| ‘ydm’ \| ‘myd’ \| ‘dym’ }*</br>
DATEFORMAT only applies to CSV and specifies the date format of the date mapping to SQL Server date formats. For an overview of all Transact-SQL date and time data types and functions, see [Date and Time Data Types and Functions (Transact-SQL)](../functions/date-and-time-data-types-and-functions-transact-sql.md?view=sql-server-ver15). DATEFORMAT within the COPY command takes precedence over [DATEFORMAT configured at the session level](set-dateformat-transact-sql.md?view=sql-server-ver15).

*ENCODING = ‘UTF8’ | ‘UTF16’*</br>
*ENCODING* only applies to CSV. Default is UTF8. Specifies the data encoding standard for the files loaded by the COPY command. 

*IDENTITY_INSERT = ‘ON’ | ‘OFF’*</br>
IDENTITY_INSERT specifies whether the identity value or values in the imported data file are to be used for the identity column. If IDENTITY_INSERT is OFF (default), the identity values for this column are verified, but not imported. SQL DW will automatically assign unique values based on the seed and increment values specified during table creation. Note the following behavior with the COPY command:

- If IDENTITY_INSERT is OFF, and table has an identity column
  - A column list must be specified which does not map an input field to the identity column.
- If IDENTITY_INSERT is ON, and table has an identity column
  - If a column list is passed, it must map an input field to the identity column.
- Default value is not supported for the IDENTITY COLUMN in the column list.
- IDENTITY_INSERT can only be set for one table at a time.

### Permissions  

The user executing the Copy Command must have the following permissions: 

- [ADMINISTER DATABASE BULK OPERATIONS](grant-database-permissions-transact-sql.md?view=azure-sqldw-latest#remarks)
- [INSERT ](grant-database-permissions-transact-sql.md?view=azure-sqldw-latest#remarks)

Requires INSERT and ADMINISTER BULK OPERATIONS permissions. In [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)], INSERT, and ADMINISTER DATABASE BULK OPERATIONS permissions are required.

## Examples  

### A. Load from a public storage account

The following example is the simplest form of the COPY command, which loads data from a public storage account. For this example, the COPY statement's defaults match the format of the line item csv file.

```sql
COPY INTO dbo.[lineitem] FROM 'https://unsecureaccount.blob.core.windows.net/customerdatasets/folder1/lineitem.csv'
```

The default values of the COPY command are:

- DATEFORMAT = Session DATEFORMAT 

- MAXERRORS = 0

- COMPRESSION default is uncompressed

- FIELDQUOTE = “” 

- FIELDTERMINATOR = “,” 

- ROWTERMINATOR = ‘\n'

> [!IMPORTANT]
> COPY treats ‘\n’ as ‘\r\n’ internally. For more information, see the ROWTERMINATOR section.

- FIRSTROW = 1

- ENCODING = ‘UTF8’

- FILE_TYPE = ‘CSV’

- IDENTITY_INSERT = ‘OFF’

### B. Load authenticating via Share Access Signature (SAS)

The following example loads files that use the line feed as a row terminator such as a UNIX output. This example also uses a SAS key to authenticate to Azure blob storage.

```sql
COPY INTO test_1
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='<Your_SAS_Token>'),
	--CREDENTIAL should look something like this:
    --CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='?sv=2018-03-28&ss=bfqt&srt=sco&sp=rl&st=2016-10-17T20%3A14%3A55Z&se=2021-10-18T20%3A19%3A00Z&sig=IEoOdmeYnE9%2FKiJDSHFSYsz4AkNa%2F%2BTx61FuQ%2FfKHefqoBE%3D'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=';',
    ROWTERMINATOR='0X0A',
    ENCODING = 'UTF8',
    DATEFORMAT = 'ymd',
	MAXERRORS = 10,
	ERRORFILE = '/errorsfolder',--path starting from the storage container
	IDENTITY_INSERT = 'ON'
)
```

### C. Load with a column list with default values authenticating via Storage Account Key

This example loads files specifying a column list with default values. 

```sql
--Note when specifying the column list, input field numbers start from 1
COPY INTO test_1 (Col_one default 'myStringDefault' 1, Col_two default 1 3)
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Storage Account Key', SECRET='<Your_Account_Key>'),
	--CREDENTIAL should look something like this:
    --CREDENTIAL=(IDENTITY= 'Storage Account Key', SECRET='x6RWv4It5F2msnjelv3H4DA80n0PQW0daPdw43jM0nyetx4c6CpDkdj3986DX5AHFMIf/YN4y6kkCnU8lb+Wx0Pj+6MDw=='),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=',',
    ROWTERMINATOR='0x0A',
    ENCODING = 'UTF8',
    FIRSTROW = 2
)
```

### D. Load Parquet or ORC using existing file format object

 This example uses a wildcard to load all parquet files under a folder. 

```sql
COPY INTO test_parquet
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/*.parquet'
WITH (
    FILE_FORMAT = myFileFormat,
    CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='<Your_SAS_Token>')
)
```

### E. Load specifying wild cards and multiple files

```sql
COPY INTO t1
FROM 
'https://myaccount.blob.core.windows.net/myblobcontainer/folder0/*.txt', 
	'https://myaccount.blob.core.windows.net/myblobcontainer/folder1'
WITH ( 
	FILE_TYPE = 'CSV',
	CREDENTIAL=(IDENTITY= '<client_id>@<OAuth_2.0_Token_EndPoint>',SECRET='<key>'),
	FIELDTERMINATOR = '|'
)
```

### F. Load using MSI credentials

```sql
COPY INTO dbo.myCOPYDemoTable
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder0/*.txt'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL = (IDENTITY = 'Managed Identity'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=','
)
```

## FAQ

### What is the performance of the COPY command compared to PolyBase?
The COPY command will have better performance depending on your workload. For best loading performance during public preview, consider splitting your input into multiple files when loading CSV. Share your performance results with our team during preview! sqldwcopypreview@service.microsoft.com

### What is the file splitting guidance for the COPY command loading CSV files?
Guidance on the number of files is outlined in the table below. Once the recommended number of files are reached, you will have better performance the larger the files. For a simple file splitting experience, refer to the following [documentation](https://techcommunity.microsoft.com/t5/azure-synapse-analytics/how-to-maximize-copy-load-throughput-with-file-splits/ba-p/1314474). 

| **DWU** | **#Files** |
| :-----: | :--------: |
|   100   |     60     |
|   200   |     60     |
|   300   |     60     |
|   400   |     60     |
|   500   |     60     |
|  1,000  |    120     |
|  1,500  |    180     |
|  2,000  |    240     |
|  2,500  |    300     |
|  3,000  |    360     |
|  5,000  |    600     |
|  6,000  |    720     |
|  7,500  |    900     |
| 10,000  |    1200    |
| 15,000  |    1800    |
| 30,000  |    3600    |


### What is the file splitting guidance for the COPY command loading Parquet or ORC files?
There is no need to split Parquet and ORC files because the COPY command will automatically split files. Parquet and ORC files in the Azure storage account should be 256MB or larger for best performance. 

### When will the COPY command be generally available?
The COPY command will be generally available by the end of this calendar year (2020). 

### Are there any limitations on the number or size of files?
There are no limitations on the number or size of files; however, for best performance, we recommend files that are at least 4MB.

### Are there any limitations with COPY using Synapse workspaces (preview)?

Authenticating using Managed Identity (MSI) is not supported with the COPY statement or PolyBase (including when used in pipelines). You may run into a similiar error message:

*com.microsoft.sqlserver.jdbc.SQLServerException: Managed Service Identity has not been enabled on this server. Please enable Managed Service Identity and try again.*

MSI authentication is required when the storage account is associated with a VNet. You must use BCP/Bulk insert to load data instead of COPY or PolyBase if your storage account is attached to a VNet.

This limitation is only applicable to SQL pools belonging to a Synapse workspace (preview). We will enable MSI support in Synapse workspaces in an upcoming release. 

Please send any feedback or issues to the following distribution list: sqldwcopypreview@service.microsoft.com

## See also  

 [Loading overview with [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]](/azure/sql-data-warehouse/design-elt-data-loading)
