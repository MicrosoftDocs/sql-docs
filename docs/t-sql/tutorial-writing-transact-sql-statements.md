---
title: "Tutorial: Writing Transact-SQL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: quickstart
helpviewer_keywords: 
  - "Transact-SQL statements, tutorials"
  - "Transact-SQL tutorials"
  - "tutorials [Transact-SQL]"
ms.assetid: 2addc9be-67d0-423d-a457-192fe9d7d058
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Tutorial: Writing Transact-SQL Statements
[!INCLUDE[tsql-appliesto-ss2008-all-md](../includes/tsql-appliesto-ss2008-all-md.md)]
Welcome to the Writing [!INCLUDE[tsql](../includes/tsql-md.md)] Statements tutorial. This tutorial is intended for users who are new to writing SQL statements. It will help new users get started by reviewing some basic statements for creating tables and inserting data. This tutorial uses [!INCLUDE[tsql](../includes/tsql-md.md)], the [!INCLUDE[msCoName](../includes/msconame-md.md)] implementation of the SQL standard. This tutorial is intended as a brief introduction to the [!INCLUDE[tsql](../includes/tsql-md.md)] language and not as a replacement for a [!INCLUDE[tsql](../includes/tsql-md.md)] class. The statements in this tutorial are intentionally simple, and are not meant to represent the complexity found in a typical production database.  
  
>**NOTE:** If you are a beginner you might find it easier to use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] instead of writing [!INCLUDE[tsql](../includes/tsql-md.md)] statements.  
  
## Finding More Information  
To find more information about any specific statement, either search for the statement by name in SQL Server Books Online, or use the Contents to browse the 1,800 language elements listed alphabetically under [Transact-SQL Reference &#40;Database Engine&#41;](../t-sql/transact-sql-reference-database-engine.md). Another good strategy for finding information is to search for key words that are related to the subject matter you are interested in. For example, if you want to know how to return a part of a date (such as the month), search the index for **dates [SQL Server]**, and then select **dateparts**. This takes you to the topic [DATEPART &#40;Transact-SQL&#41;](../t-sql/functions/datepart-transact-sql.md). As another example, to find out how to work with strings, search for **string functions**. This takes you to the topic [String Functions &#40;Transact-SQL&#41;](../t-sql/functions/string-functions-transact-sql.md).  
  
## What You Will Learn  
This tutorial shows you how to create a database, create a table in the database, insert data into the table, update the data, read the data, delete the data, and then delete the table. You will create views and stored procedures and configure a user to the database and the data.  
  
This tutorial is divided into three lessons:  
  
[Lesson 1: Creating Database Objects](../t-sql/lesson-1-creating-database-objects.md)  
In this lesson, you create a database, create a table in the database, insert data into the table, update the data, and read the data.  
  
[Lesson 2: Configuring Permissions on Database Objects](../t-sql/lesson-2-configuring-permissions-on-database-objects.md)  
In this lesson, you create a login and user. You will also create a view and a stored procedure, and then grant the user permission to the stored procedure.  
  
[Lesson 3: Deleting Database Objects](../t-sql/lesson-3-deleting-database-objects.md)  
In this lesson, you remove access to data, delete data from a table, delete the table, and then delete the database.  
  
## Requirements  
To complete this tutorial, you do not have to know the SQL language, but you should understand basic database concepts such as tables. During this tutorial, you will create a database and create a Windows user. These tasks require a high level of permissions; therefore, you should log in to the computer as an administrator.  
  
Your system must have the following installed:  
  
-   Any edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-  [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md)  
  

 
  
  
  

