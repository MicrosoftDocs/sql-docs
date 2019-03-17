---
title: "Creating a Database (Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "tutorial creating a database"
ms.assetid: e1e2c83f-dfad-4bb8-aa7a-09d3f69517ae
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Creating a Database (Tutorial)
  Like many [!INCLUDE[tsql](../includes/tsql-md.md)] statements, the CREATE DATABASE statement has a required parameter: the name of the database. CREATE DATABASE also has many optional parameters, such as the disk location where you want to put the database files. When you execute CREATE DATABASE without the optional parameters, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses default values for many of these parameters. This tutorial uses very few of the optional syntax parameters.  
  
### To create a database  
  
1.  In a Query Editor window, type but do not execute the following code:  
  
    ```  
    CREATE DATABASE TestData  
    GO  
    ```  
  
2.  Use the pointer to select the words `CREATE DATABASE`, and then press **F1**. The CREATE DATABASE topic in SQL Server Books Online should open. You can use this technique to find the complete syntax for CREATE DATABASE and for the other statements that are used in this tutorial.  
  
3.  In Query Editor, press **F5** to execute the statement and create a database named `TestData`.  
  
 When you create a database, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] makes a copy of the **model** database, and renames the copy to the database name. This operation should only take several seconds, unless you specify a large initial size of the database as an optional parameter.  
  
> [!NOTE]  
>  The keyword GO separates statements when more than one statement is submitted in a single batch. GO is optional when the batch contains only one statement.  
  
## Next Task in Lesson  
 [Creating a Table &#40;Tutorial&#41;](lesson-1-2-creating-a-table.md)  
  
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)  
  
  
