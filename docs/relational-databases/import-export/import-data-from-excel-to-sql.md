---
title: "Import data from Excel to SQL | Microsoft Docs"
ms.custom: ""
ms.date: "03/27/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.assetid: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Import data from Excel to SQL Server or Azure SQL Database
There are several ways to import data from Excel files to SQL Server or to Azure SQL Database.
-   You can import data directly from Excel SQL by using the SQL Server Import and Export Wizard, Integration Services (SSIS), or the OPENROWSET function. 
-   You can save your Excel data as text, and then use the BULK INSERT statement, BCP, or Azure Data Factory. We summarize the options here with links to more detailed instructions.

> [!NOTE]
> A complete description of complex tools and services like SSIS or Azure Data Factory is beyond the scope of this overview. To learn more about the solution that interests you, follow the links provided for tutorials and more info.

## SQL Server Import and Export Wizard

Import data directly from Excel files by stepping through the pages of a wizard. Optionally, save the import/export settings as a SQL Server Integration Services (SSIS) package that you can customize and reuse.

![Connect to an Excel data source](media/excel-connection.png)

For an example of using the wizard to import from Excel to SQL Server, see [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md).

## SQL Server Integration Services (SSIS)

If you're familiar with SSIS and don't want to run the SQL Server Import and Export Wizard, create an SSIS package that uses the Excel Source and the SQL Server Destination in the data flow.

![Components in the data flow](media/excel-to-sql-data-flow.png)

For more info about these SSIS components, see the following topics.
-   [Excel Source](../../integration-services/data-flow/excel-source.md)
-   [SQL Server Destination](../../integration-services/data-flow/sql-server-destination.md)

To start learning how to build SSIS packages, see the tutorial [How to Create an ETL Package](../../integration-services/ssis-how-to-create-an-etl-package.md).

## OPENROWSET and linked servers
> [!NOTE]
> The ACE provider (formerly the Jet provider) that connects to Excel files is intended for interactive client-side use. If you use the ACE provider on the server, especially in automated processes or processes running in parallel, you may see unexpected results.

Import data directly from Excel files by using the `OPENROWSET` or `OPENDATASOURCE` function. This usage is called a *distributed query*.

Before you can run a distributed query, you have to enable the `ad hoc distributed queries` server configuration option, as shown in the following example. For more info, see [ad hoc distributed queries Server Configuration Option](../../database-engine/configure-windows/ad-hoc-distributed-queries-server-configuration-option.md).

```sql
sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO
```

The following code sample imports the data from the Excel `Customers` worksheet into a new SQL Server table with `OPENROWSET`.

```sql
USE ImportFromExcel;
GO
SELECT * INTO Data_dq
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',
    'Excel 12.0; Database=D:\Desktop\Data.xlsx', [Data$]);
GO
```

Here's the same example with `OPENDATASOURCE`.

```sql
USE ImportFromExcel;
GO
SELECT * INTO Data_dq
FROM OPENDATASOURCE('Microsoft.ACE.OLEDB.12.0',
    'Data Source=D:\Desktop\Data.xlsx;Extended Properties=Excel 12.0')...[Data$];
GO
```

To *append* the imported data to an *existing* table instead of creating a new table, use the `INSERT INTO ... SELECT ... FROM ...` syntax instead of the `SELECT ... INTO ... FROM ...` syntax used in the preceding examples.

To query the Excel data without importing it, just use the `SELECT ... FROM ...` syntax.

For more info about distributed queries, see the following topics.
-   [Distributed Queries](https://msdn.microsoft.com/library/ms188721(v=sql.105).aspx). (Distributed queries are still supported in SQL Server 2016, but the documentation for this feature has not been updated.)
-   [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md)
-   [OPENDATASOURCE](../../t-sql/functions/openquery-transact-sql.md)

You can also configure a persistent connection to the Excel file as a *linked server*. The following example imports the data from the `Data` worksheet on the existing Excel linked server `EXCELLINK` into a new SQL Server table named `Data_ls`.

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
SET @datasrc =    'D:\Desktop\Data.xlsx'
SET @provstr =    'Excel 12.0'

EXEC @RC = [master].[dbo].[sp_addlinkedserver] @server, @srvproduct, @provider,
@datasrc, @location, @provstr, @catalog
```

For more info about linked servers, see the following topics.
-   [Create Linked Servers](../../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md)
-   [OPENQUERY](../../t-sql/functions/openquery-transact-sql.md)

For more examples and info about both linked servers and distributed queries, see the following topics.
-   [How to use Excel with SQL Server linked servers and distributed queries](https://support.microsoft.com/help/306397/how-to-use-excel-with-sql-server-linked-servers-and-distributed-queries)
-   [How to import data from Excel to SQL Server](https://support.microsoft.com/help/321686/how-to-import-data-from-excel-to-sql-server)

## Prerequisite - Save Excel data as text
To use the rest of the methods described on this page - the BULK INSERT statement, the BCP tool, or Azure Data Factory - first you have to export your Excel data to a text file.

In Excel, select **File | Save As** and then select **Text (Tab delimited) (\*.txt)** or **CSV (Comma delimited) (\*.csv)** as the destination file type.

> [!TIP]
> For best results with data importing tools, save sheets that contain only the column headers and the rows of data. If the saved data contains page titles, blank lines, notes, and so forth, you may see unexpected results later when you import the data.

## BULK INSERT command

`BULK INSERT` is a command that you can run from SQL Server Management Studio. The following example loads the data from the `Data.csv` comma-delimited file into an existing table.

```sql
USE ImportFromExcel;
GO
BULK INSERT Data_bi FROM 'D:\Desktop\data.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n'
);
GO
```

For more info, see the following topics.
-   [Import Bulk Data by Using BULK INSERT or OPENROWSET(BULK...)](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)
-   [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md)

## BCP tool

BCP is a SQL Server that you run from the command prompt. The following example loads the data from the `Data.csv` CSV file into the existing `Data_bcp` table in SQL Server.

```sql
bcp.exe ImportFromExcel..Data_bcp in "D:\Desktop\data.csv" -T -c -t ,
```

For more info, see the following topics.
-   [Import and Export Bulk Data by Using the bcp Utility ](../../relational-databases/import-export/import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)
-   [bcp Utility](../../tools/bcp-utility.md)
-   [Prepare Data for Bulk Export or Import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)

## Copy Wizard (Azure Data Factory)
Import data saved as text files by stepping through the pages of a wizard. For more info about the Copy Wizard, see the following topics.
-   [Data Factory Copy Wizard](https://docs.microsoft.com/azure/data-factory/data-factory-azure-copy-wizard)
-   [Tutorial: Create a pipeline with Copy Activity using Data Factory Copy Wizard](https://docs.microsoft.com/azure/data-factory/data-factory-copy-data-wizard-tutorial).

## Azure Data Factory
If you're familiar with Azure Data Factory and don't want to run the Azure Data Factory Copy Wizard, create a pipeline with a Copy activity that copies from the text file in a file storage location to SQL Server or to Azure SQL Database.

For more info about using these Data Factory sources and sinks, see the following topics.
-   [File system](https://docs.microsoft.com/azure/data-factory/data-factory-onprem-file-system-connector)
-   [SQL Server](https://docs.microsoft.com/azure/data-factory/data-factory-sqlserver-connector)
-   [Azure SQL Database](https://docs.microsoft.com/azure/data-factory/data-factory-azure-sql-connector)

To start learning how to copy data with Azure data factory, see the following topics.
-   [Move data by using Copy Activity](https://docs.microsoft.com/azure/data-factory/data-factory-data-movement-activities)
-   [Tutorial: Create a pipeline with Copy Activity using Azure portal](https://docs.microsoft.com/azure/data-factory/data-factory-copy-data-from-azure-blob-storage-to-sql-database)
