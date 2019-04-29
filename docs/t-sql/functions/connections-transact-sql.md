---
title: "@@CONNECTIONS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@CONNECTIONS"
  - "@@CONNECTIONS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@CONNECTIONS function"
  - "connections [SQL Server], number of"
  - "connections [SQL Server], attempted"
  - "number of connection attempts"
  - "attempted connections"
ms.assetid: c59836a8-443c-4b9a-8b96-8863ada97ac7
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;CONNECTIONS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

This function returns the number of attempted connections - both successful and unsuccessful - since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
@@CONNECTIONS  
```  
  
## Return types
**integer**
  
## Remarks  
Connections are different from users. An application, for example, can open multiple connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without user observation of those connections.
  
Run **sp_monitor** for a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including count of connection attempts.
  
@@MAX_CONNECTIONS is the maximum allowed number of simultaneous connections to the server. @@CONNECTIONS increments with each login attempt; therefore, @@CONNECTIONS can exceed @@MAX_CONNECTIONS.
  
## Examples  
This example returns the count of login attempts as of the current date and time.
  
```sql
SELECT GETDATE() AS 'Today''s Date and Time',   
@@CONNECTIONS AS 'Login Attempts';  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
  
Today's Date and Time  Login Attempts  
---------------------- --------------  
12/5/2006 10:32:45 AM  211023         
```  
  
## See also
[System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
[sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)
  
  
