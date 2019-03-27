---
title: "Select Source Tables and Views (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.selectsourcetablesandviews.f1"
ms.assetid: f60e1a19-2ea6-403c-89ab-3e60ac533ea0
author: janinezhang
ms.author: janinez
manager: craigg
---
# Select Source Tables and Views (SQL Server Import and Export Wizard)
  Use the **Select Source Tables and Views** page to specify the tables and views to be copied from the data source to the destination.  
  
> [!NOTE]  
>  You do not have to copy all the columns in a table when you select the Table Copy option. After selecting a destination table, click Edit Mappings to display the **Column Mappings** dialog box. Select **\<ignore>** in the **Destination** column of the **Column Mappings** dialog box for columns that you want to skip.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, and about the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
  
### Tables and views List  
 **Source**  
 Using the check boxes, select from the list of available tables and views to copy to the destination. If you select a source table or view and perform no other action, the schema and data from the source are copied without changes.  
  
 **Destination**  
 Select a destination table from the list for each source table.  
  
> [!NOTE]  
>  If you pause at this point in the wizard to create a destination table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or another tool, the new table is not immediately visible in the list of available destination tables. To refresh the list of destination tables, step back two pages to the **Choose a Destination** page, re-select the destination database, then step forward again to the **Select Source Tables and Views**.  
  
### Other Options  
 **Edit mappings**  
 Use the **Column Mappings** dialog box to specify destination columns to receive the source data. You can copy only a subset of columns by selecting \<ignore> in the **Destination** column of the **Column Mappings** dialog box for columns that you want to skip.  
  
 **Preview**  
 Preview source data in the **Preview Data** dialog box to verify it before performing the import or export. The **Preview Data** dialog box displays up to 200 rows of data.  
  
 After previewing the data, you might want to change the options that you have selected for the data source and destination. To make these changes, on the **Select Source Tables and Views** page, click **Back** to return to previous pages where you can change your selections.  
  
  
