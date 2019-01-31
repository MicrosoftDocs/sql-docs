---
title: "ORIGINAL_DB_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ORIGINAL_DB_NAME"
  - "ORIGINAL_DB_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ORIGINAL_DB_NAME function"
ms.assetid: 7dadc40a-1287-4f31-8487-434ee477144d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ORIGINAL_DB_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the database name specified by the user in the database connection string. This database is specified by using the **sqlcmd-d** option (USE *database*). It can also be specified with the Open Database Connectivity (ODBC) data source expression (initial catalog =*databasename*).  
  
 This database is different from the default user database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ORIGINAL_DB_NAME ()  
```  
  
## Remarks  
 If the initial database isn't specified, the function returns an empty string.  
  
## See Also  
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [osql Utility](../../tools/osql-utility.md)   
 [SQL Server Native Client (ODBC)](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)  
  
  
