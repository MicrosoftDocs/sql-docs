---
title: "Transaction Isolation Levels | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "locking [SQL Server], hints"
  - "isolation levels [SQL Server], metadata access"
  - "hints [SQL Server], locking"
ms.assetid: 02bb71fa-1e92-4782-a9cf-6e256cc1f3ea
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Transaction Isolation Levels
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not guarantee that lock hints will be honored in queries that access metadata through catalog views, compatibility views, information schema views, metadata-emitting built-in functions.  
  
 Internally, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] only honors the READ COMMITTED isolation level for metadata access. If a transaction has an isolation level that is, for example, SERIALIZABLE and within the transaction, an attempt is made to access metadata by using catalog views or metadata-emitting built-in functions, those queries will run until they are completed as READ COMMITTED. However, under snapshot isolation, access to metadata might fail because of concurrent DDL operations. This is because metadata is not versioned. Therefore, accessing the following under snapshot isolation might fail:  
  
-   Catalog views  
  
-   Compatibility views  
  
-   Information Schema Views  
  
-   Metadata-emitting built-in functions  
  
-   **sp_help** group of stored procedures  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client catalog procedures  
  
-   Dynamic management views and functions  
  
 For more information about isolation levels, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).  
  
 The following table provides a summary of metadata access under various isolation levels.  
  
|Isolation level|Supported|Honored|  
|---------------------|---------------|-------------|  
|READ UNCOMMITTED|No|Not guaranteed|  
|READ COMMITTED|Yes|Yes|  
|REPEATABLE READ|No|No|  
|SNAPSHOT ISOLATION|No|No|  
|SERIALIZABLE|No|No|  
  
  
