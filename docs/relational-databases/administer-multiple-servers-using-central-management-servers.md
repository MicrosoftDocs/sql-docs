---
title: "Administer Multiple Servers Using Central Management Servers | Microsoft Docs"
ms.date: "08/12/2016"
ms.prod: sql
ms.prod_service: "database-engine"
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
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Administer Multiple Servers Using Central Management Servers
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can administer multiple servers by designating Central Management Servers and creating server groups.  
  
## What is a Central Management Server and server groups?  
 An instance of SQL Server designated as a Central Management Server maintains server groups that contain the connection information for one or more instances. You can execute [!INCLUDE[tsql](../includes/tsql-md.md)] statements and Policy-Based Management policies at the same time against server groups. You can also view the log files on instances managed through a Central Management Server. 
 
 Basically, a Central Management Server is a central repository containing a list of your managed servers. Versions earlier than [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] cannot be designated as a Central Management Server.  
  
 [!INCLUDE[tsql](../includes/tsql-md.md)] statements can also be executed against local server groups in Registered Servers.  
  
## Create Central Management Server and server groups 
 To create a Central Management Server and server groups, use the **Registered Servers** window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. Note that the Central Management Server cannot be a member of a group that it maintains. 
 
 For how to create Central Management Servers and server groups, see [Create a Central Management Server and Server Group &#40;SQL Server Management Studio&#41;](../tools/sql-server-management-studio/create-a-central-management-server-and-server-group.md).  
  
## See also  
 [Administer Servers by Using Policy-Based Management](../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
