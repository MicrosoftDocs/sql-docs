---
title: "Import and Export Data from SQL Server and Azure SQL Database"
description: You can use Transact-SQL, command-line tools, and wizards to import and export data in SQL Server and Azure SQL Database in various data formats.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/02/2026
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Import and export data from SQL Server and Azure SQL Database

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can use various methods to import data to, and export data from, SQL Server and Azure SQL Database. These methods include Transact-SQL statements, command-line tools, and wizards.

You can also import and export data in various data formats. These formats include flat files, Excel, major relational databases, and various cloud services.

## Methods for importing and exporting data

### Use Transact-SQL statements

You can import data with the `BULK INSERT` or the `OPENROWSET(BULK...)` commands. Typically you run these commands in SQL Server Management Studio (SSMS). For more information, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

### Use BCP from the command prompt

You can import and export data with the BCP command-line utility. For more information, see [Import and export bulk data using bcp (SQL Server)](import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md).

### Azure portal import and export

The Azure portal provides **Import** and **Export** actions for Azure SQL Database. You can use these actions to import or export a database as a BACPAC file through the **Azure SQL Import/Export service**.

- **Export**: From an Azure SQL Database page, select **Export** to create a BACPAC file in Azure Blob Storage.
- **Import**: From a [logical server for Azure SQL Database](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) page, select **Import database** to create a new database from a BACPAC file stored in Azure Blob Storage.

Portal-based import and export operations:

- Use the same import or export service and APIs as PowerShell, Azure CLI, and REST.
- Support BACPAC files stored in Azure Blob Storage.
- Surface operation status and history through **Import/Export history** on the logical server.

Authentication options available in the Azure portal align with those supported by the import and export service. Support for **Managed Identity authentication** is in **Preview**.

For a detailed tutorial, see [Use managed identity with import and export (preview)](/azure/azure-sql/database/database-import-export-managed-identity).

In this model:

- A **user‑assigned managed identity (UAMI)** is assigned to the logical server for Azure SQL Database.
- The managed identity is configured as a **Microsoft Entra administrator** on the server.
- The same or a different managed identity is granted **Azure RBAC data-plane access** to the target Azure Storage account.

> [!NOTE]
> - Import and export with managed identity authentication is currently in [**preview**](/azure/azure-sql/database/doc-changes-updates-release-notes-whats-new#preview) and only available for Azure SQL Database.
> - Only **server-level user-assigned managed identities** are supported in the current preview.

For more information, see [Import a BACPAC file to a database in Azure SQL Database](/azure/azure-sql/database/database-import#import-with-managed-identity-authentication-preview) and [Export a database to a BACPAC file](/azure/azure-sql/database/database-export##export-with-managed-identity-authentication-preview).


### Use the Import Flat File Wizard

If you don't need all the configuration options available in the Import and Export Wizard and other tools, you can import a text file into SQL Server by using the **Import Flat File Wizard** in SQL Server Management Studio (SSMS). For more information, see the following articles:

- [Import Flat File to SQL Wizard](import-flat-file-wizard.md)
- [What's new in SQL Server Management Studio 17.3](https://blogs.technet.microsoft.com/dataplatforminsider/2017/10/10/whats-new-in-sql-server-management-studio-17-3/)

### Use the SQL Server Import and Export Wizard

You can import data to, or export data from, various sources and destinations with the SQL Server Import and Export Wizard. To use the wizard, you must have SQL Server Integration Services (SSIS) or SQL Server Data Tools (SSDT) installed. For more information, see [Import and Export Data with the SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md).

### Design your own import or export

If you want to design a custom data import, you can use one of the following features or services:

- [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)
- [Introduction to Azure Data Factory](/azure/data-factory/data-factory-introduction)

## Data formats for import and export

### Supported formats

You can import data from, and export data to, flat files or various other file formats, relational databases, and cloud services. To learn more about these options for specific tools, see the following articles:

- [Connect to Data Sources with the SQL Server Import and Export Wizard](../../integration-services/import-export-data/connect-to-data-sources-with-the-sql-server-import-and-export-wizard.md)
- [Integration Services (SSIS) Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)
- [Azure Data Factory Connectors](/azure/data-factory/data-factory-amazon-redshift-connector)

### Commonly used data formats

There are special considerations and examples available for some commonly used data formats. To learn more about these data formats, see the following articles:

- [Import data from Excel to SQL Server or Azure SQL Database](import-data-from-excel-to-sql.md)
- [Import JSON documents into SQL Server](../json/import-json-documents-into-sql-server.md)
- [Examples of bulk import and export of XML documents (SQL Server)](examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Examples of bulk access to data in Azure Blob Storage](examples-of-bulk-access-to-data-in-azure-blob-storage.md).

## Related content

- [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md)
