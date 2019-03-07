---
title: "MSSQLSERVER_15404 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "15404 (Database Engine error)"
ms.assetid: 69677f02-bc81-4e4a-99b8-5c1bd1de36df
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_15404
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|15404|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_NTGRP_ERROR|  
|Message Text|Could not obtain information about Windows NT group/user '*user*', error code *code*.|  
  
## Explanation  
 15404 is used in authentication when an invalid principal is specified. Or, impersonation of a Windows account fails because there is no full trust relationship between the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and the domain of the Windows account.  
  
## User Action  
 Check that the Windows principal exists and is not misspelled.  
  
 If this error is the result of a lack of a full trust relationship between the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and the domain of the Windows account, one of the following actions can resolve the error:  
  
-   Use an account from the same domain as the Windows user for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
-   If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is using a machine account such as Network Service or Local System, the machine must be trusted by the domain containing the Windows User.  
  
-   Use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
  
