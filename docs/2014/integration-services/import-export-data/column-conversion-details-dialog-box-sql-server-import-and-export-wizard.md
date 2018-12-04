---
title: "Column Conversion Details Dialog Box (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.issuedetails.f1"
ms.assetid: e2d00a39-dfbd-4821-a4d8-a5bd1164ed4d
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Column Conversion Details Dialog Box (SQL Server Import and Export Wizard)
  Use the **Column Conversion Details** dialog box to review detailed conversion information about an individual column. This conversion information contains the column's data type at the source and the destination, and the conversion that the wizard will perform. This page also lists the data type mapping files that the wizard uses to determine the data type conversions that are required. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs these data type mapping files during setup.  
  
 **To open the Column Conversion Details dialog box**  
  
1.  On the **Review Data Type Issues** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard, in the **Table** list, select a table.  
  
2.  In the **Data type mapping** list, double-click the row that contains the column for which you want to view conversion details.  
  
 To learn more about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, and about the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
  
