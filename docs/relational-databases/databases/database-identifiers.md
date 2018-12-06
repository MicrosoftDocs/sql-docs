---
title: "Database Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "regular identifiers [SQL Server]"
  - "identifiers [SQL Server]"
  - "names [SQL Server], identifiers"
  - "identifiers [SQL Server], about identifiers"
  - "SQL Server identifiers"
  - "Transact-SQL identifiers"
  - "database objects [SQL Server], names"
ms.assetid: 171291bb-f57f-4ad1-8cea-0b092d5d150c
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database Identifiers
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The database object name is referred to as its identifier. Everything in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can have an identifier. Servers, databases, and database objects, such as tables, views, columns, indexes, triggers, procedures, constraints, and rules, can have identifiers. Identifiers are required for most objects, but are optional for some objects such as constraints.  
  
 An object identifier is created when the object is defined. The identifier is then used to reference the object. For example, the following statement creates a table with the identifier `TableX`, and two columns with the identifiers `KeyCol` and `Description`:  
  
```  
CREATE TABLE TableX  
(KeyCol INT PRIMARY KEY, Description nvarchar(80))  
```  
  
 This table also has an unnamed constraint. The `PRIMARY KEY` constraint has no identifier.  
  
 The collation of an identifier depends on the level at which it is defined. Identifiers of instance-level objects, such as logins and database names, are assigned the default collation of the instance. Identifiers of objects in a database, such as tables, views, and column names, are assigned the default collation of the database. For example, two tables with names that differ only in case can be created in a database that has case-sensitive collation, but cannot be created in a database that has case-insensitive collation.  
  
> [!NOTE]  
>  The names of variables, or the parameters of functions and stored procedures must comply with the rules for [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers.  
  
## Classes of Identifiers  
 There are two classes of identifiers:  
  
 Regular identifiers  
 Comply with the rules for the format of identifiers. Regular identifiers are not delimited when they are used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
```  
SELECT *  
FROM TableX  
WHERE KeyCol = 124  
```  
  
 Delimited identifiers  
 Are enclosed in double quotation marks (") or brackets ([ ]). Identifiers that comply with the rules for the format of identifiers might not be delimited. For example:  
  
```  
SELECT *  
FROM [TableX]         --Delimiter is optional.  
WHERE [KeyCol] = 124  --Delimiter is optional.  
```  
  
 Identifiers that do not comply with all the rules for identifiers must be delimited in a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For example:  
  
```  
SELECT *  
FROM [My Table]      --Identifier contains a space and uses a reserved keyword.  
WHERE [order] = 10   --Identifier is a reserved keyword.  
```  
  
 Both regular and delimited identifiers must contain from 1 through 128 characters. For local temporary tables, the identifier can have a maximum of 116 characters.  
  
## Rules for Regular Identifiers  
 The names of variables, functions, and stored procedures must comply with the following rules for [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers.  
  
1.  The first character must be one of the following:  
  
    -   A letter as defined by the Unicode Standard 3.2. The Unicode definition of letters includes Latin characters from a through z, from A through Z, and also letter characters from other languages.  
  
    -   The underscore (_), at sign (@), or number sign (#).  
  
         Certain symbols at the beginning of an identifier have special meaning in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A regular identifier that starts with the at sign always denotes a local variable or parameter and cannot be used as the name of any other type of object. An identifier that starts with a number sign denotes a temporary table or procedure. An identifier that starts with double number signs (##) denotes a global temporary object. Although the number sign or double number sign characters can be used to begin the names of other types of objects, we do not recommend this practice.  
  
         Some [!INCLUDE[tsql](../../includes/tsql-md.md)] functions have names that start with double at signs (@@). To avoid confusion with these functions, you should not use names that start with @@.  
  
2.  Subsequent characters can include the following:  
  
    -   Letters as defined in the Unicode Standard 3.2.  
  
    -   Decimal numbers from either Basic Latin or other national scripts.  
  
    -   The at sign, dollar sign ($), number sign, or underscore.  
  
3.  The identifier must not be a [!INCLUDE[tsql](../../includes/tsql-md.md)] reserved word. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reserves both the uppercase and lowercase versions of reserved words. When identifiers are used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, the identifiers that do not comply with these rules must be delimited by double quotation marks or brackets. The words that are reserved depend on the database compatibility level. This level can be set by using the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) statement.  
  
4.  Embedded spaces or special characters are not allowed.  
  
5.  Supplementary characters are not allowed.  
  
 When identifiers are used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, the identifiers that do not comply with these rules must be delimited by double quotation marks or brackets.  
  
> [!NOTE]  
>  Some rules for the format of regular identifiers depend on the database compatibility level. This level can be set by using [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)   
 [CREATE DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/create-default-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [CREATE RULE &#40;Transact-SQL&#41;](../../t-sql/statements/create-rule-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [Reserved Keywords &#40;Transact-SQL&#41;](../../t-sql/language-elements/reserved-keywords-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)  
  
  
