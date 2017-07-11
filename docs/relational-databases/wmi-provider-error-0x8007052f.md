---
title: "WMI Error 0x8007052f | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "0x8007052f (WMI error)"
ms.assetid: a33f12bd-15c4-42a8-b343-de44c3e87729
caps.latest.revision: 10
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# WMI Provider Error 0x8007052f
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|0x8007052f|  
|Event Source|WMI Provider Error|  
|Component|SQL Server Configuration Manager|  
|Symbolic Name|NA|  
|Message Text|Logon failure: user account restriction. Possible reasons are blank passwords not allowed, login hour restrictions, or a policy restriction has been enforced.|  
  
## Explanation  
 This error can occur when using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager to switch to the built-in accounts (Network Service, Local Service, or Local System) when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on a Windows Server Cluster or Windows Server Domain Controller. Do not run services under built-in accounts on a Windows Server Cluster or Windows Server Domain Controller. For recommendations on service accounts, see the topic Setting Up Windows Service Accounts in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online.  
  
## User Action  
 Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to use a domain account.  
  
  