---
title: "MSSQLSERVER_33027 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "33027 (Database Engine error)"
ms.assetid: bfdc626e-7958-4511-987d-3b687824e8af
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_33027
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|33027|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_CRYPTOPROV_CANTLOADDLL|  
|Message Text|Failed to load cryptographic provider '%.*ls' due to an invalid Authenticode signature or invalid file path. Check previous messages for other failures.|  
  
## Explanation  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was unable to use the cryptographic provider listed in the error message, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could not load the DLL. Either the name is invalid or the Authenticode signature is invalid.  
  
## User Action  
 Check that the file is present and that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has permission to access that location. Check the error log for additional related messages. Otherwise, contact the cryptographic provider for more information.  
  
  
