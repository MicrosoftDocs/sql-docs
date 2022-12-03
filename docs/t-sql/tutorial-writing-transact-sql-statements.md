---
title: "T-SQL Tutorial: Write Transact-SQL statements"
description: This tutorial is intended for users who are new to writing SQL statements. It will help new users get started by reviewing some basic statements for creating tables and inserting data.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 12/02/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: quickstart
ms.custom: intro-quickstart
helpviewer_keywords:
  - "Transact-SQL statements, tutorials"
  - "Transact-SQL tutorials"
  - "tutorials [Transact-SQL]"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Tutorial: Write Transact-SQL statements

[!INCLUDE[sql-asdb-asa-pdw](../includes/applies-to-version/sql-asdb-asa-pdw.md)]

Welcome to the Writing [!INCLUDE[tsql](../includes/tsql-md.md)] Statements tutorial. This tutorial is intended for users who are new to writing SQL statements. It will help new users get started by reviewing some basic statements for creating tables and inserting data. This tutorial uses [!INCLUDE[tsql](../includes/tsql-md.md)], the [!INCLUDE[msCoName](../includes/msconame-md.md)] implementation of the SQL standard.

This tutorial is intended as a brief introduction to the [!INCLUDE[tsql](../includes/tsql-md.md)] language and not as a replacement for a [!INCLUDE[tsql](../includes/tsql-md.md)] class. The statements in this tutorial are intentionally simple, and aren't meant to represent the complexity found in a typical production database.

> [!NOTE]  
> If you are a beginner you might find it easier to use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] instead of writing [!INCLUDE[tsql](../includes/tsql-md.md)] statements.

## Find more information

To find more information about any specific statement, either search for the statement by name in SQL Server Books Online, or use the Contents to browse the 1,800 language elements listed alphabetically under [Transact-SQL Reference (Database Engine)](./language-reference.md). Another good strategy for finding information is to search for key words that are related to the subject matter you are interested in. For example, if you want to know how to return a part of a date (such as the month), search the index for **dates [SQL Server]**, and then select **dateparts**. This takes you to the article [DATEPART (Transact-SQL)](../t-sql/functions/datepart-transact-sql.md). As another example, to find out how to work with strings, search for **string functions**. This takes you to the article [String Functions (Transact-SQL)](../t-sql/functions/string-functions-transact-sql.md).

## What you will learn

This tutorial shows you how to create a database, create a table in the database, insert data into the table, update the data, read the data, delete the data, and then delete the table. You will create views and stored procedures and configure a user to the database and the data.

This tutorial is divided into three lessons:

- [Lesson 1: Creating Database Objects](../t-sql/lesson-1-creating-database-objects.md)

  In this lesson, you create a database, create a table in the database, insert data into the table, update the data, and read the data.

- [Lesson 2: Configuring Permissions on Database Objects](../t-sql/lesson-2-configuring-permissions-on-database-objects.md)

  In this lesson, you create a login and user. You will also create a view and a stored procedure, and then grant the user permission to the stored procedure.

- [Lesson 3: Deleting Database Objects](../t-sql/lesson-3-deleting-database-objects.md)

  In this lesson, you remove access to data, delete data from a table, delete the table, and then delete the database.

## Requirements

To complete this tutorial, you don't have to know the SQL language, but you should understand basic database concepts such as tables. During this tutorial, you will create a database and create a Windows user. These tasks require a high level of permissions; therefore, you should log in to the computer as an administrator.

Your system must have the following installed:

- Any edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

- [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md)

## Next steps

The next article teaches you how to create database objects.

Go to the next article to learn more:
> [!div class="nextstepaction"]
> [Lesson 1: Creating Database Objects](../t-sql/lesson-1-creating-database-objects.md)
