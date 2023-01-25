---
title: "Import data from Excel to SQL"
description: This article describes methods to import data from Excel to SQL Server or Azure SQL Database. Some use a single step, others require an intermediate text file.
author: rwestMSFT
ms.author: randolphwest
ms.date: "12/12/2021"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom:
  - sqlfreshmay19
  - FY22Q2Fresh
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Import data from Excel to SQL Server or Azure SQL Database

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

There are several ways to import data from Excel files to SQL Server or to Azure SQL Database. Some methods let you import data in a single step directly from Excel files; other methods require you to export your Excel data as text (CSV file) before you can import it. 

This article summarizes the frequently used methods and provides links for more detailed information. A complete description of complex tools and services like SSIS or Azure Data Factory is beyond the scope of this article. To learn more about the solution that interests you, follow the provided links.

## List of methods

There are a number of methods to import data from Excel. You may need to install [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) to use some of these tools.

You can use the following tools to import data from Excel:

| Export to text first (SQL Server and SQL Database) | Directly from Excel (SQL Server on-premises only) |
| :------------------------------------------------- |:------------------------------------------------- |
| [Import Flat File Wizard](#import-wiz)             |[SQL Server Import and Export Wizard](#wiz)        |
| [BULK INSERT](#bulk-insert) statement              |[SQL Server Integration Services (SSIS)](#ssis)    |
| [BCP](#bcp)                                        |[OPENROWSET](#openrowset) function <br>            |
| [Copy Wizard (Azure Data Factory)](#adf-wiz)       |                                                   |
| [Azure Data Factory](#adf)                         |                                                   |

If you want to import multiple worksheets from an Excel workbook, you typically have to run any of these tools once for each sheet.

> [!IMPORTANT]
> To learn more, see [limitations and known issues for loading data](../../integration-services/load-data-to-from-excel-with-ssis.md#issues-types) to or from Excel files. 



## <a name="wiz"></a> Import and Export Wizard

Import data directly from Excel files by using the SQL Server Import and Export Wizard. You also have the option to save the settings as a SQL Server Integration Services (SSIS) package that you can customize and reuse later.

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)].

2. Expand **Databases**.
3. Right-click a database.
4. Point to **Tasks**.
5. Choose to **Import Data** or **Export Data**: 

    :::image type="content" source="../../integration-services/import-export-data/media/start-wizard-ssms.jpg" alt-text="Start wizard SSMS":::

This launches the wizard: 

:::image type="content" source="media/excel-connection.png" alt-text="Connect to an Excel data source":::

To learn more, review: 

- [Start the SQL Server Import and Export Wizard](../../integration-services/import-export-data/start-the-sql-server-import-and-export-wizard.md)
- [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md)


## <a name="ssis"></a> Integration Services (SSIS)

If you're familiar with SQL Server Integration Services (SSIS) and don't want to run the SQL Server Import and Export Wizard, create an SSIS package that uses the Excel Source and the SQL Server Destination in the data flow.

To learn more, review: 
- [Excel Source](../../integration-services/data-flow/excel-source.md)
- [SQL Server Destination](../../integration-services/data-flow/sql-server-destination.md)

To start learning how to build SSIS packages, see the tutorial [How to Create an ETL Package](../../integration-services/ssis-how-to-create-an-etl-package.md).

:::image type="content" source="media/excel-to-sql-data-flow.png" alt-text="Components in the data flow":::

## <a name="openrowset"></a> OPENROWSET and linked servers

> [!IMPORTANT]
> In Azure SQL Database, you cannot import directly from Excel. You must first [export the data to a text (CSV) file](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

> [!NOTE]
> The ACE provider (formerly the Jet provider) that connects to Excel data sources is intended for interactive client-side use. If you use the ACE provider on SQL Server, especially in automated processes or processes running in parallel, you may see unexpected results.

### Distributed queries

Import data directly into SQL Server from Excel files by using the Transact-SQL `OPENROWSET` or `OPENDATASOURCE` function. This usage is called a *distributed query*.

> [!IMPORTANT]
> In Azure SQL Database, you cannot import directly from Excel. You must first [export the data to a text (CSV) file](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

Before you can run a distributed query, you have to enable the `ad hoc distributed queries` server configuration option, as shown in the following example. For more info, see [ad hoc distributed queries Server Configuration Option](../../database-engine/configure-windows/ad-hoc-distributed-queries-server-configuration-option.md).

```sql
sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO
```

The following code sample uses `OPENROWSET` to import the data from the Excel `Sheet1` worksheet into a new database table.

```sql
USE ImportFromExcel;
GO
SELECT * INTO Data_dq
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',
    'Excel 12.0; Database=C:\Temp\Data.xlsx', [Sheet1$]);
GO
```

Here's the same example with `OPENDATASOURCE`.

```sql
USE ImportFromExcel;
GO
SELECT * INTO Data_dq
FROM OPENDATASOURCE('Microsoft.ACE.OLEDB.12.0',
    'Data Source=C:\Temp\Data.xlsx;Extended Properties=Excel 12.0')...[Sheet1$];
GO
```

To *append* the imported data to an *existing* table instead of creating a new table, use the `INSERT INTO ... SELECT ... FROM ...` syntax instead of the `SELECT ... INTO ... FROM ...` syntax used in the preceding examples.

To query the Excel data without importing it, just use the standard `SELECT ... FROM ...` syntax.

For more info about distributed queries, see the following topics:

- [Distributed Queries](/previous-versions/sql/sql-server-2008-r2/ms188721(v=sql.105)) (Distributed queries are still supported in SQL Server 2019, but the documentation for this feature has not been updated.)
- [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md)
- [OPENDATASOURCE](../../t-sql/functions/opendatasource-transact-sql.md)

### Linked servers

You can also configure a persistent connection from SQL Server to the Excel file as a *linked server*. The following example imports the data from the `Data` worksheet on the existing Excel linked server `EXCELLINK` into a new SQL Server database table named `Data_ls`.

```sql
USE ImportFromExcel;
GO
SELECT * INTO Data_ls FROM EXCELLINK...[Data$];
GO
```

You can create a linked server from SQL Server Management Studio, or by running the system stored procedure `sp_addlinkedserver`, as shown in the following example.

```sql
DECLARE @RC int

DECLARE @server     nvarchar(128)
DECLARE @srvproduct nvarchar(128)
DECLARE @provider   nvarchar(128)
DECLARE @datasrc    nvarchar(4000)
DECLARE @location   nvarchar(4000)
DECLARE @provstr    nvarchar(4000)
DECLARE @catalog    nvarchar(128)

-- Set parameter values
SET @server =     'EXCELLINK'
SET @srvproduct = 'Excel'
SET @provider =   'Microsoft.ACE.OLEDB.12.0'
SET @datasrc =    'C:\Temp\Data.xlsx'
SET @provstr =    'Excel 12.0'

EXEC @RC = [master].[dbo].[sp_addlinkedserver] @server, @srvproduct, @provider,
@datasrc, @location, @provstr, @catalog
```

For more info about linked servers, see the following topics:

- [Create Linked Servers](../../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md)
- [OPENQUERY](../../t-sql/functions/openquery-transact-sql.md)

For more examples and info about both linked servers and distributed queries, see the following topic:

- [How to use Excel with SQL Server linked servers and distributed queries](https://support.microsoft.com/help/306397/how-to-use-excel-with-sql-server-linked-servers-and-distributed-queries)

## <a name="prereq"></a> Prerequisite - Save Excel data as text

To use the rest of the methods described on this page - the BULK INSERT statement, the BCP tool, or Azure Data Factory - first you have to export your Excel data to a text file.

In Excel, select **File | Save As** and then select **Text (Tab-delimited) (\*.txt)** or **CSV (Comma-delimited) (\*.csv)** as the destination file type.

If you want to export multiple worksheets from the workbook, select each sheet and then repeat this procedure. The **Save as** command exports only the active sheet.

> [!TIP]
> For best results with data importing tools, save sheets that contain only the column headers and the rows of data. If the saved data contains page titles, blank lines, notes, and so forth, you may see unexpected results later when you import the data.

## <a name="import-wiz"></a> The Import Flat File Wizard

Import data saved as text files by stepping through the pages of the Import Flat File Wizard.

As described previously in the [Prerequisite](#prereq) section, you have to export your Excel data as text before you can use the Import Flat File Wizard to import it.

For more info about the Import Flat File Wizard, see [Import Flat File to SQL Wizard](import-flat-file-wizard.md).

## <a name="bulk-insert"></a> BULK INSERT command

`BULK INSERT` is a Transact-SQL command that you can run from SQL Server Management Studio. The following example loads the data from the `Data.csv` comma-delimited file into an existing database table.

As described previously in the [Prerequisite](#prereq) section, you have to export your Excel data as text before you can use BULK INSERT to import it. BULK INSERT can't read Excel files directly. With the BULK INSERT command, you can import a CSV file that is stored locally or in Azure Blob storage.

```sql
USE ImportFromExcel;
GO
BULK INSERT Data_bi FROM 'C:\Temp\data.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n'
);
GO
```

For more info and examples for SQL Server and SQL Database, see the following topics:

- [Import Bulk Data by Using BULK INSERT or OPENROWSET(BULK...)](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)
- [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md)

## <a name="bcp"></a> BCP tool

BCP is a program that you run from the command prompt. The following example loads the data from the `Data.csv` comma-delimited file into the existing `Data_bcp` database table.

As described previously in the [Prerequisite](#prereq) section, you have to export your Excel data as text before you can use BCP to import it. BCP can't read Excel files directly. Use to import into SQL Server or SQL Database from a test (CSV) file saved to local storage.

> [!IMPORTANT]
> For a text (CSV) file stored in Azure Blob storage, use BULK INSERT or OPENROWSET. For an examples, see [Example](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

```console
bcp.exe ImportFromExcel..Data_bcp in "C:\Temp\data.csv" -T -c -t ,
```

For more info about BCP, see the following topics:

- [Import and Export Bulk Data by Using the bcp Utility](../../relational-databases/import-export/import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)
- [bcp Utility](../../tools/bcp-utility.md)
- [Prepare Data for Bulk Export or Import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)

## <a name="adf-wiz"></a> Copy Wizard (ADF)

Import data saved as text files by stepping through the pages of the Azure Data Factory (ADF) Copy Wizard.

As described previously in the [Prerequisite](#prereq) section, you have to export your Excel data as text before you can use Azure Data Factory to import it. Data Factory can't read Excel files directly.

For more info about the Copy Wizard, see the following topics:

- [Data Factory Copy Wizard](/azure/data-factory/data-factory-azure-copy-wizard)
- [Tutorial: Create a pipeline with Copy Activity using Data Factory Copy Wizard](/azure/data-factory/data-factory-copy-data-wizard-tutorial).

## <a name="adf"></a> Azure Data Factory

If you're familiar with Azure Data Factory and don't want to run the Copy Wizard, create a pipeline with a Copy activity that copies from the text file to SQL Server or to Azure SQL Database.

As described previously in the [Prerequisite](#prereq) section, you have to export your Excel data as text before you can use Azure Data Factory to import it. Data Factory can't read Excel files directly.

For more info about using these Data Factory sources and sinks, see the following topics:

- [File system](/azure/data-factory/data-factory-onprem-file-system-connector)
- [SQL Server](/azure/data-factory/data-factory-sqlserver-connector)
- [Azure SQL Database](/azure/data-factory/data-factory-azure-sql-connector)

To start learning how to copy data with Azure data factory, see the following topics:

- [Move data by using Copy Activity](/azure/data-factory/data-factory-data-movement-activities)
- [Tutorial: Create a pipeline with Copy Activity using Azure portal](/azure/data-factory/data-factory-copy-data-from-azure-blob-storage-to-sql-database)

## Common errors

### Microsoft.ACE.OLEDB.12.0" has not been registered

This error occurs because the OLEDB provider is not installed. Install it from [Microsoft Access Database Engine 2010 Redistributable](https://www.microsoft.com/download/details.aspx?id=13255). Be sure to install the 64-bit version if Windows and SQL Server are both 64-bit.

The full error is:

```
Msg 7403, Level 16, State 1, Line 3
The OLE DB provider "Microsoft.ACE.OLEDB.12.0" has not been registered.
```

### Cannot create an instance of OLE DB provider "Microsoft.ACE.OLEDB.12.0" for linked server "(null)"

This indicates that the Microsoft OLEDB has not been configured properly. Run the following Transact-SQL code to resolve this:

```sql
EXEC sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'AllowInProcess', 1
EXEC sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'DynamicParameters', 1
```

The full error is:

```
Msg 7302, Level 16, State 1, Line 3
Cannot create an instance of OLE DB provider "Microsoft.ACE.OLEDB.12.0" for linked server "(null)".
```

### The 32-bit OLE DB provider "Microsoft.ACE.OLEDB.12.0" cannot be loaded in-process on a 64-bit SQL Server

This occurs when a 32-bit version of the OLD DB provider is installed with a 64-bit SQL Server. To resolve this issue, uninstall the 32-bit version and install the 64-bit version of the OLE DB provider instead.

The full error is:

```
Msg 7438, Level 16, State 1, Line 3
The 32-bit OLE DB provider "Microsoft.ACE.OLEDB.12.0" cannot be loaded in-process on a 64-bit SQL Server.
```

### The OLE DB provider "Microsoft.ACE.OLEDB.12.0" for linked server "(null)" reported an error. 

### Cannot initialize the data source object of OLE DB provider "Microsoft.ACE.OLEDB.12.0" for linked server "(null)"

Both of these errors typically indicate a permissions issue between the SQL Server process and the file. Ensure that the account that is running the SQL Server service has full access permission to the file. We recommend against trying to import files from the desktop.

The full errors are:

```
Msg 7399, Level 16, State 1, Line 3
The OLE DB provider "Microsoft.ACE.OLEDB.12.0" for linked server "(null)" reported an error. The provider did not give any information about the error.
```

```
Msg 7303, Level 16, State 1, Line 3
Cannot initialize the data source object of OLE DB provider "Microsoft.ACE.OLEDB.12.0" for linked server "(null)".
```

## Next steps

- [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md)
- [Import data from Excel or export data to Excel with SQL Server Integration Services (SSIS)](../../integration-services/load-data-to-from-excel-with-ssis.md)
- [bcp Utility](../../tools/bcp-utility.md)
- [Move data by using Copy Activity](/azure/data-factory/data-factory-data-movement-activities)
