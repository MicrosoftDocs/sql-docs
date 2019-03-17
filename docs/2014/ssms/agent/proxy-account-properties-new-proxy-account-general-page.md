---
title: "Proxy Account Properties and New Proxy Account (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql12.ag.proxy.general.f1"
ms.assetid: 5cd81265-bf59-413b-8397-150ddc70d0c7
author: stevestein
ms.author: sstein
manager: craigg
---
# Proxy Account Properties and New Proxy Account (General Page)
  Use this page to view or change the properties of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account.  
  
## Options  
 **Proxy name**  
 Type the name of the proxy.  
  
 **Credential name**  
 Type the name of the credential for the proxy.  
  
> [!NOTE]  
>  The credential name provided must be the name of an existing credential. For information on creating credentials, see [Create a Credential](../../relational-databases/security/authentication-access/create-a-credential.md)  
  
 **...**  
 Launches the **Select Credential** dialog.  
  
 **Description**  
 Type the description for the proxy.  
  
 **Active to the following subsystems**  
 Select the subsystems that the proxy account has access to.  
  
 **Reassign job steps to**  
 Select the proxy to reassign job steps to. This list is enabled when you revoke access to a subsystem that the proxy previously had access to.  
  
## See Also  
 [Create a SQL Server Agent Proxy](create-a-sql-server-agent-proxy.md)  
  
  
