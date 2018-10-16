---
title: "MSSQLSERVER_33129 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "33129 (Database Engine error)"
ms.assetid: 83b5f368-f1a1-4a40-9bb6-c77e2dec690f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_33129
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|33129|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_CANNOT_DISABLE_WIN_GROUP|  
|Message Text|Cannot use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group.|  
  
## Explanation  
This message occurs when attempting to disable the login of a Windows Group.  
  
## User Action  
The login of a Windows Group cannot be disabled. To temporarily remove access permission granted to a Windows Group, REVOKE the CONNECT permission of the login for the Windows Group. Windows users might still have access through their individual login or through another Windows Group. The following example revokes the connect permission to the Accounting Group of the WESTCOAST domain.  
  
```Transact-SQL  
REVOKE CONNECT TO [WESTCOAST\Accounting];  
```  
  
