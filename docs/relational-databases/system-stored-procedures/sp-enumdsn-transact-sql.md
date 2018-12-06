---
title: "sp_enumdsn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_enumdsn"
  - "sp_enumdsn_TSQL"
helpviewer_keywords: 
  - "sp_enumdsn"
ms.assetid: 171cbc7d-7406-4cb0-8602-9405243bfd1d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_enumdsn (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a list of all defined ODBC and OLE DB data source names for a server running under a specific [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user account. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_enumdsn  
```  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Data Source Name**|**sysname**|Name of the data source.|  
|**Description**|**varchar(255)**|Description of the data source.|  
|**Type**|**int**|Type of data source:<br /><br /> **1** = ODBC DSN<br /><br /> **3** = OLE DB data source|  
|**Provider Name**|**varchar(255)**|Name of the OLE DB provider. Value is NULL for ODBC DSN.|  
  
## Remarks  
 Every [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has a user context. A user context is a set of Registry entries that includes the definitions of the ODBC data sources for the user. The user context is provided by the username under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
 For example, if the server is running under the system account user context, the data source names (DSNs) that are returned will all be system DSNs that are associated with the system account. If the server is running under a private user account, only the DSNs defined for that private account of that user is returned.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_enumdsn**.  
  
## See Also  
 [sp_dsninfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dsninfo-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
