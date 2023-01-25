---
description: "Import and Export Data with the SQL Server Import and Export Wizard"
title: "Import and Export Data with the SQL Server Import and Export Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "10/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "exporting data"
  - "mapping files [Integration Services]"
  - "SQL Server Import and Export Wizard"
  - "SSIS packages, creating"
  - "packages [Integration Services], copying"
  - "Integration Services packages, creating"
  - "packages [Integration Services], creating"
  - "SQL Server Integration Services packages, creating"
  - "Import and Export Wizard"
  - "copying data [Integration Services]"
  - "importing data, SSIS packages"
  - "sources [Integration Services], copying data"
ms.assetid: c0e4d867-b2a9-4b2a-844b-2fe45be88f81
author: chugugrace
ms.author: chugu
---
# Import and Export Data with the SQL Server Import and Export Wizard

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]



 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard is a simple way to copy data from a source to a destination. This overview describes the data sources that the wizard can use as sources and destinations, as well as the permissions you need to run the wizard.

## Get the wizard
If you want to run the wizard, but you don't have [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on your computer, you can install the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard  by installing SQL Server Data Tools (SSDT). For more info, see [Download SQL Server Data Tools (SSDT)](../../ssdt/download-sql-server-data-tools-ssdt.md).

## What happens when I run the wizard?
-    **See the list of steps.** For a description of the steps in the wizard, see [Steps in the SQL Server Import and Export Wizard](../../integration-services/import-export-data/steps-in-the-sql-server-import-and-export-wizard.md). There's also a separate page of documentation for each page of the wizard.  
    \- or \-
-   **See an example.** For a quick look at the several screens you see in a typical session, take a look at this simple example on a single page - [Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md).  

##  <a name="wizardSources"></a> What sources and destinations can I use?  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard can copy data to and from the data sources listed in the following table. To connect to some of these data sources, you may have to download and install additional files.
 
| Data source | Do I have to download additional files? |
|-------------|-----------------------------------------|
|**Enterprise databases**<br/>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, DB2, and others.|SQL Server or SQL Server Data Tools (SSDT) installs the files that you need to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. But SSDT doesn't install all the files that you need to connect to other enterprise databases such as Oracle or IBM DB2.<br/><br/>To connect to an enterprise database, you typically have to have two things:<br/><br/>1. **Client software**. If you already have the client software installed for your enterprise database system, then you typically have what you need to make a connection. If you don't have the client software installed, ask the database administrator how to install a licensed copy.<br/><br/>2. **Drivers or providers**. Microsoft installs drivers and providers to connect to Oracle. To connect to IBM DB2, get the MicrosoftÂ® OLEDB Provider for DB2 v5.0 for Microsoft SQL Server from the [Microsoft SQL Server 2016 Feature Pack](https://www.microsoft.com/download/details.aspx?id=56833).<br/><br/>For more info, see [Connect to a SQL Server Data Source](connect-to-a-sql-server-data-source-sql-server-import-and-export-wizard.md) or [Connect to an Oracle Data Source](connect-to-an-oracle-data-source-sql-server-import-and-export-wizard.md).|
|**Text files** (flat files)|No additional files required.<br/><br/>For more info, see [Connect to a Flat File Data Source](connect-to-a-flat-file-data-source-sql-server-import-and-export-wizard.md).|
|**Microsoft Excel and Microsoft Access files**|Microsoft Office doesn't install all the files that you need to connect to Excel and Access files as data sources. Get the following download - [Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920).<br/><br/>For more info, see [Connect to an Excel Data Source](../../integration-services/import-export-data/connect-to-an-excel-data-source-sql-server-import-and-export-wizard.md) or [Connect to an Access Data Source](../../integration-services/import-export-data/connect-to-an-access-data-source-sql-server-import-and-export-wizard.md).|
|**Azure data sources**<br/>Currently only Azure Blob Storage.|SQL Server Data Tools don't install the files that you need to connect to Azure Blob Storage as a data source. Get the following download - [Microsoft SQL Server 2016 Integration Services Feature Pack for Azure](https://www.microsoft.com/download/details.aspx?id=49492).<br/><br/>For more info, see [Connect to Azure Blob Storage](../../integration-services/import-export-data/connect-to-azure-blob-storage-sql-server-import-and-export-wizard.md).|
|**Open source databases**<br/>PostgreSQL, MySql, and others.|To connect to these data sources, you have to download additional files.<br/><br/>- For **PostgreSQL**, see [Connect to a PostgreSQL Data Source](../../integration-services/import-export-data/connect-to-a-postgresql-data-source-sql-server-import-and-export-wizard.md).<br/>- For **MySql**, see [Connect to a MySQL Data Source](../../integration-services/import-export-data/connect-to-a-mysql-data-source-sql-server-import-and-export-wizard.md).|
|**Any other data source for which a driver or provider is available**|You typically have to download additional files to connect to the following types of data sources.<br/><br/>- Any source for which an **ODBC driver** is available. For more info, see [Connect to an ODBC Data Source](../../integration-services/import-export-data/connect-to-an-odbc-data-source-sql-server-import-and-export-wizard.md).<br/>- Any source for which a **.Net Framework Data Provider** is available.<br/>- Any source for which an **OLE DB Provider** is available.<br/><br/>Third-party components that provide source and destination capabilities for other data sources are sometimes marketed as add-on products for SQL Server Integration Services (SSIS).|

## How do I connect to my data?
For info about how to connect to a commonly used data source, see one of the following pages:
-   [Connect to SQL Server](../../integration-services/import-export-data/connect-to-a-sql-server-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to Oracle](../../integration-services/import-export-data/connect-to-an-oracle-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to flat files (text files)](../../integration-services/import-export-data/connect-to-a-flat-file-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to Excel](../../integration-services/import-export-data/connect-to-an-excel-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to Access](../../integration-services/import-export-data/connect-to-an-access-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to Azure Blob Storage](../../integration-services/import-export-data/connect-to-azure-blob-storage-sql-server-import-and-export-wizard.md)
-   [Connect with ODBC](../../integration-services/import-export-data/connect-to-an-odbc-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to PostgreSQL](../../integration-services/import-export-data/connect-to-a-postgresql-data-source-sql-server-import-and-export-wizard.md)
-   [Connect to MySQL](../../integration-services/import-export-data/connect-to-a-mysql-data-source-sql-server-import-and-export-wizard.md)


For info about how to connect to a data source that's not listed here, see [The Connection Strings Reference](https://www.connectionstrings.com/). This third-party site contains sample connection strings and more info about data providers and the connection info they require.

## What permissions do I need?  
 To run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard successfully, you have to have at least the following permissions. If you already work with your data source and destination, you probably already have the permissions that you need.
  
|You need permissions to do these things|If you're connecting to SQL Server, you need these specific permissions |  
|-------------------------|----------------------------------------------------|  
|Connect to the source and destination databases or file shares.|Server and database login rights.|  
|Export or read data from the source database or file.|SELECT permissions on the source tables and views.|  
|Import or write data to the destination database or file.|INSERT permissions on the destination tables.|  
|Create the destination database or file, if applicable.|CREATE DATABASE or CREATE TABLE permissions.|  
|Save the SSIS package created by the wizard, if applicable.|If you want to save the package to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], permissions sufficient to save the package to the **msdb** database.|  
  
## Get help while the wizard is running
> [!TIP]
> Tap the F1 key from any page or dialog box of the wizard to see documentation for the current page.   
  
##  <a name="wizardSSIS"></a> The wizard uses SQL Server Integration Services (SSIS)  
 The wizard uses SQL Server Integration Services (SSIS) to copy data. SSIS is a tool for extracting, transforming, and loading data (ETL). The pages of the wizard use some of the language of SSIS.
  
 In SSIS, the basic unit is the **package**. The wizard creates an SSIS package in memory as you move through the pages of the wizard and specify options.    
  
At the end of the wizard, if you have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard Edition or higher installed, you can optionally save the SSIS package. Later you can reuse the package and extend it by using [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer to add tasks, transformations, and event-driven logic. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard is the simplest way to create a basic [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that copies data from a source to a destination.

For more info about SSIS, see [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md).

## What's next?  
 Start the wizard. For more info, see [Start the SQL Server Import and Export Wizard](../../integration-services/import-export-data/start-the-sql-server-import-and-export-wizard.md).  

## See also
[Get started with this simple example of the Import and Export Wizard](../../integration-services/import-export-data/get-started-with-this-simple-example-of-the-import-and-export-wizard.md)  
[Data Type Mapping in the SQL Server Import and Export Wizard](../../integration-services/import-export-data/data-type-mapping-in-the-sql-server-import-and-export-wizard.md)