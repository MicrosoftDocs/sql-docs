---
title: "Import and export data from SQL Server and Azure SQL Database | Microsoft Docs"
ms.custom: ""
ms.date: "09/12/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Import and export data from SQL Server and Azure SQL Database
You can use a variety of methods to import data to, and export data from, SQL Server and Azure SQL Database. These methods include Transact-SQL statements, command-line tools, and wizards.

You can also import and export data in a variety of data formats. These formats include flat files, Excel, major relational databases, and various cloud services.

## Methods for importing and exporting data

### Use Transact-SQL statements
You can import data with the `BULK INSERT` or the `OPENROWSET(BULK...)` commands. Typically you run these commands in SQL Server Management Studio (SSMS). For more info, see [Import Bulk Data by Using BULK INSERT or OPENROWSET(BULK...)](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

### Use BCP from the command prompt
You can import and export data with the BCP command-line utility. For more info, see [Import and Export Bulk Data by Using the bcp Utility](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

### Use the SQL Server Import and Export Wizard
You can import data to, or export data from, a variety of sources and destinations with the SQL Server Import and Export Wizard. To use the wizard, you must have SQL Server Integration Services (SSIS) or SQL Server Data Tools (SSDT) installed. For more info, see [Import and Export Data with the SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md).

### Design your own import or export
If you want to design a custom data import, you can use one of the following features or services:
-   SQL Server Integration Services. For more info, see [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md).
-   Azure Data Factory. For more info, see [Introduction to Azure Data Factory](https://docs.microsoft.com/azure/data-factory/data-factory-introduction).

## Data formats for import and export

### Supported formats

You can import data from, and export data to, flat files or a variety of other file formats, relational databases, and cloud services. To learn more about these options for specific tools, see the following topics
-   For the SQL Server Import and Export Wizard, see [Connect to Data Sources with the SQL Server Import and Export Wizard](../../integration-services/import-export-data/connect-to-data-sources-with-the-sql-server-import-and-export-wizard.md).
-   For SQL Server Integration Services, see [Integration Services (SSIS) Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md).
-   For Azure Data Factory, see [Azure Data Factory Connectors](https://docs.microsoft.com/en-us/azure/data-factory/data-factory-amazon-redshift-connector).

### Commonly used data formats

There are special considerations and examples available for some commonly-used data formats. To learn more about these data formats, see the following topics:
-   For Excel, see [Import from Excel](import-data-from-excel-to-sql.md).
-   For JSON, see [Import JSON Documents](../json/import-json-documents-into-sql-server.md).
-   For XML, see [Import and Export XML Documents](examples-of-bulk-import-and-export-of-xml-documents-sql-server.md).
-   For Azure Blob Storage, see [Import and Export from Azure Blob Storage](examples-of-bulk-access-to-data-in-azure-blob-storage.md).

## Next steps
If you're not sure where to begin with your import or export task, consider the SQL Server Import and Export Wizard. For a quick introduction, see [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md).
