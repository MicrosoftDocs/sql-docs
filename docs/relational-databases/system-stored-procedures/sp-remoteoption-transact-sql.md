---
description: "sp_remoteoption (Transact-SQL)"
title: "sp_remoteoption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_remoteoption_TSQL"
  - "sp_remoteoption"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_remoteoption"
ms.assetid: c9a7309b-eab7-4192-a414-e282581af4e5
author: markingmyname
ms.author: maghan
---
# sp_remoteoption (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Displays or changes options for a remote login defined on the local server running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]sp_remoteoption does not change any options and returns an error message. It is supported for backward compatibility only.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_remoteoption [ [ @remoteserver = ] 'remoteserver' ]   
     [ , [ @loginame = ] 'loginame' ]   
     [ , [ @remotename = ] 'remotename' ]   
     [ , [ @optname = ] 'optname' ]   
     [ , [ @optvalue = ] 'optvalue' ]  
```  
  
## Remarks  
 This stored procedure returns the following error message:  
  
 `The trusted option in remote login mapping is no longer supported.`  
  
## See Also  
 [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md)  
  
  
