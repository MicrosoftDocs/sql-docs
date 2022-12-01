---
title: "Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server"
description: Find out how to use Transact-SQL statements to bulk import data from a file to a SQL Server or Azure SQL Database table, including security considerations.
author: markingmyname
ms.author: maghan
ms.date: "09/25/2019"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019, FY22Q2Fresh
helpviewer_keywords:
  - "BULK INSERT statement, importing data from a remote data file"
  - "bulk importing [SQL Server], methods"
  - "bulk exporting [SQL Server], methods"
  - "OPENROWSET function, BULK INSERT"
  - "bulk importing [SQL Server], security"
  - "bulk rowset providers [SQL Server]"
  - "bulk exporting [SQL Server], BULK INSERT statement"
  - "remote data access [SQL Server], bulk importing"
  - "bulk importing [SQL Server], BULK INSERT statement"
  - "Transact-SQL bulk export/import operations"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server

[!INCLUDE [SQL Server Azure SQL Database Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides an overview of how to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] BULK INSERT statement and the INSERT...SELECT * FROM OPENROWSET(BULK...) statement to bulk import data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database table. This article also describes security considerations for using BULK INSERT and OPENROWSET(BULK...), and using these methods to bulk import from a remote data source.

> [!NOTE]
> When you use BULK INSERT or OPENROWSET(BULK...), it is important to understand how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version handles impersonation. For more information, see "Security Considerations," later in this topic.

## BULK INSERT statement

BULK INSERT loads data from a data file into a table. This functionality is similar to that provided by the **in** option of the **bcp** command; however, the data file is read by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. For a description of the BULK INSERT syntax, see [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md).

## BULK INSERT examples

- [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)
- [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)
- [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)
- [Specify Field and Row Terminators &#40;SQL Server&#41;](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)
- [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
- [Use Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)
- [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)
- [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)
- [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

## OPENROWSET(BULK...) Function

The OPENROWSET bulk rowset provider is accessed by calling the OPENROWSET function and specifying the BULK option. The OPENROWSET(BULK...) function allows you to access remote data by connecting to a remote data source, such as a data file, through an OLE DB provider.

To bulk import data, call OPENROWSET(BULK...) from a SELECT...FROM clause within an INSERT statement. The basic syntax for bulk importing data is:

INSERT ... SELECT * FROM OPENROWSET(BULK...)

When used in an INSERT statement, OPENROWSET(BULK...) supports table hints. In addition to the regular table hints, such as TABLOCK, the BULK clause can accept the following specialized table hints: IGNORE_CONSTRAINTS (ignores only the CHECK constraints), IGNORE_TRIGGERS, KEEPDEFAULTS, and KEEPIDENTITY. For more information, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).

For information about additional uses of the BULK option, see [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md).

## INSERT...SELECT * FROM OPENROWSET(BULK...) statements - examples:

- [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)
- [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)
- [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
- [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)
- [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

## Security considerations

If a user uses a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, the security profile of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process account is used. A login using SQL Server authentication cannot be authenticated outside of the Database Engine. Therefore, when a BULK INSERT command is initiated by a login using SQL Server authentication, the connection to the data is made using the security context of the SQL Server process account (the account used by the SQL Server Database Engine service). 

To successfully read the source data you must grant the account used by the SQL Server Database Engine, access to the source data. In contrast, if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user logs on by using Windows Authentication, the user can read only those files that can be accessed by the user account, regardless of the security profile of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process.

For example, consider a user who logged in to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using Windows Authentication. For the user to be able to use BULK INSERT or OPENROWSET to import data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, the user account requires read access to the data file. With access to the data file, the user can import data from the file into a table even if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process does not have permission to access the file. The user does not have to grant file-access permission to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows can be configured to enable an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to connect to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by forwarding the credentials of an authenticated Windows user. This arrangement is known as *impersonation* or *delegation*. Understanding how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version handle security for user impersonation is important when you use BULK INSERT or OPENROWSET. User impersonation allows the data file to reside on a different computer than either the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process or the user. For example, if a user on **Computer_A** has access to a data file on **Computer_B**, and the delegation of credentials has been set appropriately, the user can connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running on **Computer_C**, access the data file on **Computer_B**, and bulk import data from that file into a table on **Computer_C**.

## Bulk importing to SQL Server from a remote data file

To use BULK INSERT or INSERT...SELECT \* FROM OPENROWSET(BULK...) to bulk import data from another computer, the data file must be shared between the two computers. To specify a shared data file, use its universal naming convention (UNC) name, which takes the general form, **\\\\**_Servername_**\\**_Sharename_**\\**_Path_**\\**_Filename_. Additionally, the account used to access the data file must have the permissions that are required for reading the file on the remote disk.

For example, the following `BULK INSERT` statement bulk imports data into the `SalesOrderDetail` table of the `AdventureWorks` database from a data file that is named `newdata.txt`. This data file resides in a shared folder named `\dailyorders` on a network share directory named `salesforce` on a system named `computer2`.

```sql
BULK INSERT AdventureWorks2012.Sales.SalesOrderDetail
   FROM '\\computer2\salesforce\dailyorders\neworders.txt';
```

> [!NOTE]
> This restriction does not apply to the **bcp** utility because the client reads the file independently of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Bulk importing from Azure Blob storage

When importing from Azure Blob storage and the data is not public (anonymous access), create a [DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md) based on a SAS key which is encrypted with a [MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md), and then create an [external database source](../../t-sql/statements/create-external-data-source-transact-sql.md) for use in your BULK INSERT command.

> [!NOTE]
> Do not use explicit transaction, or you receive a 4861 error.

### Using BULK INSERT

The following example shows how to use the BULK INSERT command to load data from a csv file in an Azure Blob storage location on which you have created a SAS key. The Azure Blob storage location is configured as an external data source. This requires a database scoped credential using a shared access signature that is encrypted using a master key in the user database.

```sql
--> Optional - a MASTER KEY is not required if a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'YourStrongPassword1';
GO
--> Optional - a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
 WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
 SECRET = '******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************';

 -- NOTE: Make sure that you don't have a leading ? in SAS token, and
 -- that you have at least read permission on the object that should be loaded srt=o&sp=r, and
 -- that expiration period is valid (all dates are in UTC time)

CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH ( TYPE = BLOB_STORAGE,
          LOCATION = 'https://****************.blob.core.windows.net/invoices'
          , CREDENTIAL= MyAzureBlobStorageCredential --> CREDENTIAL is not required if a blob is configured for public (anonymous) access!
);

BULK INSERT Sales.Invoices
FROM 'inv-2017-12-08.csv'
WITH (DATA_SOURCE = 'MyAzureBlobStorage');
```

> [!IMPORTANT]
> Azure SQL Database does not support reading from Windows files.

### Using OPENROWSET

The following example shows how to use the OPENROWSET command to load data from a csv file in an Azure Blob storage location on which you have created a SAS key. The Azure Blob storage location is configured as an external data source. This requires a database scoped credential using a shared access signature that is encrypted using a master key in the user database.

```sql
--> Optional - a MASTER KEY is not required if a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'YourStrongPassword1';
GO
--> Optional - a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
 WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
 SECRET = '******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************';

 -- NOTE: Make sure that you don't have a leading ? in SAS token, and
 -- that you have at least read permission on the object that should be loaded srt=o&sp=r, and
 -- that expiration period is valid (all dates are in UTC time)

CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH ( TYPE = BLOB_STORAGE,
          LOCATION = 'https://****************.blob.core.windows.net/invoices'
          , CREDENTIAL= MyAzureBlobStorageCredential --> CREDENTIAL is not required if a blob is configured for public (anonymous) access!
);

INSERT INTO achievements with (TABLOCK) (id, description)
SELECT * FROM OPENROWSET(
   BULK  'csv/achievements.csv',
   DATA_SOURCE = 'MyAzureBlobStorage',
   FORMAT ='CSV',
   FORMATFILE='csv/achievements-c.xml',
   FORMATFILE_DATA_SOURCE = 'MyAzureBlobStorage'
    ) AS DataFile;
```

> [!IMPORTANT]
> Azure SQL Database does not support reading from Windows files.

## See also

- [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)
- [SELECT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-clause-transact-sql.md)
- [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)
- [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)
- [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)
- [bcp Utility](../../tools/bcp-utility.md)
- [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)  
