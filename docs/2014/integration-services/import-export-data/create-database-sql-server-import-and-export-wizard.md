---
title: "Create Database (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.createdatabase.f1"
ms.assetid: 56a8a79f-086c-4bdc-8888-0045bb4b0cbf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Create Database (SQL Server Import and Export Wizard)
  Use the **Create Database** page to define a new database for a destination file.  
  
 This page offers a subset of the available options for creating a new database. To view all the options, use [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, and about the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **Name**  
 Provide a unique name for the destination SQL Server database. Make sure that you follow SQL Server conventions when you name this database.  
  
 **Data file name**  
 View the name of the data file. This is derived from the database name you provided earlier.  
  
 **Log file name**  
 View the name of the log file. This is derived from the database name you provided earlier.  
  
 **Initial size (data file)**  
 Specify the number of megabytes for the initial size of the data file.  
  
 **No growth allowed (data file)**  
 Indicate whether the data file can grow beyond the specified initial size.  
  
 **Grow by percentage (data file)**  
 Specify a percentage by which the data file can grow.  
  
 **Grow by size (data file)**  
 Specify a number of megabytes by which the data file can grow.  
  
 **Initial size (log file)**  
 Specify the number of megabytes for the initial size of the log file.  
  
 **No growth allowed (log file)**  
 Indicate whether the log file can grow beyond the specified initial size.  
  
 **Grow by percentage (log file)**  
 Specify a percentage by which the log file can grow.  
  
 **Grow by size (log file)**  
 Specify a number of megabytes by which the log file can grow.  
  
  
