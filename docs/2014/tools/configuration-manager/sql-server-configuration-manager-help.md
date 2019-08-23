---
title: "SQL Server Configuration Manager Help | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Configuration Manager, help"
ms.assetid: 6e909911-39a6-469b-b22a-3afdfd08a30b
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Configuration Manager Help
  Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services and configure network connectivity. To create or manage database objects, configure security, and write [!INCLUDE[tsql](../../includes/tsql-md.md)] queries, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information about [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 This section contains the F1 Help topics for the dialogs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager cannot configure versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
## Services  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager manages services that are related to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Although many of these tasks can be accomplished using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Services dialog, is important to note that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager performs additional operations on the services it manages, such as applying the correct permissions when the service account is changed. Using the normal Windows Services dialog to configure any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services might cause the service to malfunction.  
  
 Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for the following tasks for services:  
  
-   Start, stop, and pause services  
  
-   Configure services to start automatically or manually, disable the services, or change other service settings  
  
-   Change the passwords for the accounts used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services  
  
-   Start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using trace flags (command line parameters)  
  
-   View the properties of services  
  
## SQL Server Network Configuration  
 Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for the following tasks related to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services on this computer:  
  
-   Enable or disable a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] network protocol  
  
-   Configure a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] network protocol  
  
> [!NOTE]  
>  For a short tutorial about how to configure protocols and connect to the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], see [Tutorial: Getting Started with the Database Engine](../../relational-databases/tutorial-getting-started-with-the-database-engine.md).  
  
## SQL Server Native Client Configuration  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client network library. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for the following tasks related to client applications on this computer:  
  
-   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client applications on this computer, specify the protocol order, when connecting to instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Configure client connection protocols.  
  
-   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client applications, create aliases for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], so that clients can connect using a custom connection string.  
  
 For more information about each of these tasks, see F1 help for each task.  
  
#### To open SQL Server Configuration Manager  
  
-   On the **Start** menu, point to **All Programs**, point to **Microsoft SQL Server** (version), point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
## See Also  
 [SQL Server Services](../../../2014/tools/configuration-manager/sql-server-services.md)   
 [SQL Server Network Configuration](sql-server-network-configuration.md)   
 [SQL Native Client 11.0 Configuration](../../../2014/tools/configuration-manager/sql-native-client-11-0-configuration.md)   
 [Choosing a Network Protocol](../../../2014/tools/configuration-manager/choosing-a-network-protocol.md)  
  
  
