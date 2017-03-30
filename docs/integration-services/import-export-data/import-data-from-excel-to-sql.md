---
title: "Import data from Excel to SQL Server or SQL Database | Microsoft Docs"
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
Import data directly from Excel files to SQL by using Integration Services (SSIS) or the SQL Server Import and Export Wizard. Or, save your Excel files as text files, and then  use the BULK INSERT statement, BCP, or Azure Data Factory.

> [!NOTE]
> A complete description of each tool or service, such as SSIS or Azure Data Factory, is beyond the scope of this article. To learn more about the solution that interests you, follow the links provided for tutorials and more info.

## SQL Server Integration Services (SSIS)

Create an SSIS package that uses the Excel Source and the SQL Server Destination in the data flow to import directly from Excel files. Or, if you don't want to build an SSIS package manually, use the SQL Server Import and Export Wizard to build and run the package.

![](media/excel-to-sql-data-flow.png)

For more info about using these SSIS components, see the following topics.
-   [Excel Source](../data-flow/excel-source.md)
-   [SQL Server Destination](../data-flow/sql-server-destination.md)
-   [How to Create an ETL Package](../ssis-how-to-create-an-etl-package.md) (tutorial).

## SQL Server Import and Export Wizard

Import data directly from Excel files by stepping through the pages of a wizard. Optionally, save the SSIS package that the wizard creates to customize it and reuse it later.

![](media/excel-connection.png)

For an example of using the wizard to import from Excel to SQL Server, see [Get started with this simple example of the Import and Export Wizard](get-started-with-this-simple-example-of-the-import-and-export-wizard.md).

## Save Excel data as text
To use the 'BULK INSERT' statement, the BCP tool, or Azure Data Factory, first export your Excel data to a text file. In Excel, select **File | Save As** and then select **Text (Tab delimited) (\*.txt)** or **CSV (Comma delimited) (\*.csv)** as the file type.

> [!TIP]
> For best results with data importing tools, save sheets that contain only the column headers and the rows of data. If the saved data contains page titles, blank lines, notes, and so forth, you may see unexpected results.

## BULK INSERT command

`BULK INSERT` is a SQL Server command that you can run from SQL Server Management Studio. The following example loads the data from the `Data.csv` CSV file into an existing table in SQL Server.

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

## Azure Data Factory
In Azure Data Factory, create a pipeline with a Copy activity that copies from the text file in a file storage location to SQL Server or to Azure SQL Database. Or, use the Data Factory Copy Wizard to copy the data.

For more info about using these Data Factory sources and sinks, see the following topics.
-   [File system](https://docs.microsoft.com/azure/data-factory/data-factory-onprem-file-system-connector)
-   [SQL Server](https://docs.microsoft.com/azure/data-factory/data-factory-sqlserver-connector)
-   [Azure SQL Database](https://docs.microsoft.com/azure/data-factory/data-factory-azure-sql-connector)
-   [Move data by using Copy Activity](https://docs.microsoft.com/azure/data-factory/data-factory-data-movement-activities)-   [Tutorial: Create a pipeline with Copy Activity using Azure portal](https://docs.microsoft.com/azure/data-factory/data-factory-copy-data-from-azure-blob-storage-to-sql-database)

For more info about the Copy Wizard, see the following topics.
-   [Data Factory Copy Wizard](https://docs.microsoft.com/azure/data-factory/data-factory-azure-copy-wizard)
-   [Tutorial: Create a pipeline with Copy Activity using Data Factory Copy Wizard](https://docs.microsoft.com/azure/data-factory/data-factory-copy-data-wizard-tutorial).
