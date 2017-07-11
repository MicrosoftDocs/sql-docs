---
title: "Migrating Access Data into SQL Server - Azure SQL DB (AccessToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
helpviewer_keywords: 
  - "bulk loading data"
  - "data, loading into SQL Azure"
  - "data, loading into SQL Server"
  - "migrating databases, loading data"
  - "migrating databases, options"
  - "options, migrating data"
  - "SQL Azure, migrating data into"
  - "SQL Server, migrating data into"
ms.assetid: f3b18af7-1af0-499d-a00d-a0af94895625
caps.latest.revision: 17
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Migrating Access Data into SQL Server - Azure SQL DB (AccessToSQL)
After you have successfully created the database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)], you can migrate data from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.  
  
## Setting Migration Options  
Before you migrate data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, review the project migration options in the **Project Settings** dialog box. In this dialog box, you can set the migration batch size, table locking, constraint checking, insertion trigger firing, identity and null value handling, and how to handle dates that are out of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] range. For more information, see [Project Settings (Migration)](http://msdn.microsoft.com/en-us/4caebc9c-8680-4b99-a8fa-89c43161c95d).  
  
## Migrating Data  
Migrating data is a bulk-load operation that moves rows of data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure in transactions. The number of rows to be loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure in each transaction is configured in the project settings.  
  
To view migration messages, make sure the Output pane is visible. If it is not, on the **View** menu, select **Output**.  
  
**To migrate data**  
  
1.  Make sure you have loaded the Access database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.  
  
2.  In Access Metadata Explorer, select the objects that contain the data that you want to migrate:  
  
    -   To migrate data for an entire database, select the check box next to the database name.  
  
    -   To migrate data from individual tables, expand the database, expand **Tables**, and then select the check box next to the table. To omit data from individual tables, clear the check box.  
  
3.  Right-click **Databases** and then select **Migrate Data**.  
  
You also can migrate data outside of SSMA by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] **bcp** command-line utility or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion_md.md)]. For more information about these tools, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Books Online.  
  
## Next Step  
If you have Access database applications that you want to continue to use after migration, link the Access database tables to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure tables. For more information, see [Linking Access Applications to SQL Server](http://msdn.microsoft.com/en-us/82374ad2-7737-4164-a489-13261ba393d4).  
  
## See Also  
[Migrating Access Databases to SQL Server](http://msdn.microsoft.com/en-us/76a3abcf-2998-4712-9490-fe8d872c89ca)  
[Setting Conversion and Migration Options](http://msdn.microsoft.com/en-us/0a7304df-2f35-4453-96ef-7ac83dea1167)  
  
