---
title: "@@SPID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@SPID"
  - "@@SPID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@SPID function"
  - "session_id"
  - "server process IDs [SQL Server]"
  - "IDs [SQL Server], user processes"
  - "SPID"
  - "session IDs [SQL Server]"
  - "process ID of current user process"
ms.assetid: df955d32-8194-438e-abee-387eebebcbb7
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# &#x40;&#x40;SPID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the session ID of the current user process.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@SPID  
```  
  
## Return Types  
 **smallint**  
  
## Remarks  
 @@SPID can be used to identify the current user process in the output of **sp_who**.  
  
## Examples  
 This example returns the session ID, login name, and user name for the current user process.  
  
```  
SELECT @@SPID AS 'ID', SYSTEM_USER AS 'Login Name', USER AS 'User Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
ID     Login Name                     User Name                       
------ ------------------------------ ------------------------------  
54     SEATTLE\joanna                 dbo                             
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 This example returns the [!INCLUDE[ssDW](../../includes/ssdw-md.md)] session ID, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Control node session ID, login name, and user name for the current user process.  
  
```  
SELECT SESSION_ID() AS ID, @@SPID AS 'Control ID', SYSTEM_USER AS 'Login Name', USER AS 'User Name';  
```  
  
## See Also  
 [Configuration Functions](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [sp_lock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lock-transact-sql.md)   
 [sp_who](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
  
  

