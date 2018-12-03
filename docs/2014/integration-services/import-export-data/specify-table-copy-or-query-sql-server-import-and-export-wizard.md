---
title: "Specify Table Copy or Query (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.specifytablecopyorquery.f1"
ms.assetid: 08aa7158-40e6-4ef3-84d3-1265a8ba194c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specify Table Copy or Query (SQL Server Import and Export Wizard)
  Use the **Specify Table Copy or Query** page to specify how to copy data. You can use a graphical interface to select the existing database objects you want to copy, or you can use Transact-SQL to create a more complex query.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, as well as the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **Copy data from one or more tables or views**  
 Copy fields from selected source tables and views to the specified destination or destinations by using the **Select Source Tables and Views** dialog box. Use this option if you want to copy all data in the source without filtering or ordering records.  
  
 When you use a [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider to connect to your data source, the **Copy data from one or more tables or views** option might not be available. This option is available only for those providers that have a ProviderDescription section in the ProviderDescriptors.xml file. Each ProviderDescription section contains the information that is required to retrieve metadata from the corresponding provider. By default, the ProviderDescriptors.xml file contains a ProviderDescription section for only the following providers:  
  
-   System.Data.SqlClient  
  
-   System.Data.OracleClient  
  
-   System.Data.OleDb  
  
-   System.Data.Odbc  
  
 To make the **Copy data from one or more tables or views** option available for additional providers, third parties can add their own ProviderDescriptor sections to the ProviderDescriptors.xml file. By default, this file is in \<*drive*>:\Program Files\Microsoft SQL Server\100\DTS\ProviderDescriptors. To review the requirements for the ProviderDescriptor section, see the ProviderDescriptors.xsd schema file that is, by default, in the same folder as the ProviderDescriptors.xml file.  
  
 **Write a query to specify the data to transfer**  
 Build SQL statements to retrieve rows by using the **Provide a Source Query** dialog box. Use this option if you want to modify or restrict the source data during the copy operation. Only the rows matching the selection criteria are available for copying.  
  
  
