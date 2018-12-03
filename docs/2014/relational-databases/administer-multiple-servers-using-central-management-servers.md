---
title: "Administer Multiple Servers Using Central Management Servers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "multiserver queries"
  - "central management server"
  - "multiserver administration [SQL Server]"
  - "master servers [SQL Server], central management servers"
  - "target configuration [SQL Server]"
  - "server configuration [SQL Server]"
ms.assetid: 427911a7-57d4-4542-8846-47c3267a5d9c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Administer Multiple Servers Using Central Management Servers
  You can administer multiple servers by designating Central Management Servers and creating server groups.  
  
## Benefits of Central Management Servers and Server Groups  
 An instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that is designated as a Central Management Server maintains server groups that contain the connection information for one or more instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. [!INCLUDE[tsql](../includes/tsql-md.md)] statements and Policy-Based Management policies can be executed at the same time against server groups. You can also view the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] log files on instances that are managed through a Central Management Server. Versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that are earlier than [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] cannot be designated as a Central Management Server.  
  
 [!INCLUDE[tsql](../includes/tsql-md.md)] statements can also be executed against local server groups in Registered Servers.  
  
### Related Tasks  
 To create a Central Management Server and server groups, use the **Registered Servers** window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. Note that the Central Management Server cannot be a member of a group that it maintains. For more information about how to create Central Management Servers and server groups, see [Create a Central Management Server and Server Group &#40;SQL Server Management Studio&#41;](../ssms/register-servers/create-a-central-management-server-and-server-group.md).  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
