---
title: "Connection Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade Advisor [SQL Server], connections"
  - "authentication [Upgrade Advisor]"
  - "SQL Server Upgrade Advisor, connections"
  - "connections [Upgrade Advisor]"
  - "server instances [Upgrade Advisor]"
  - "default server instances"
  - "analyzing system [Upgrade Advisor], connections"
ms.assetid: f754d038-637a-4d8e-85b0-b242e6499d26
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Connection Parameters
  To analyze certain server types, such as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], you must select a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is automatically selected. You can change this selection, but you can select only one instance at a time for analysis by Upgrade Advisor. If you have included a server type that requires authentication, you must enter the authentication mode and credentials.  
  
## Options  
 **Server name**  
 Pre-populated with the computer name that you entered in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Components pane.  
  
 **Instance name**  
 Select from the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are available on the computer. If you do not see a list of instances, use MSSQLSERVER to scan the default instance. This is especially relevant for remote computers. You can also use the word "default" to scan the default instance.  
  
 **Authentication**  
 Select from the list of available authentication modes on this computer. By default, the authentication mode is Windows Authentication.  
  
 **User name**  
 If using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, enter a user name in the box. We recommend that the user name have administrative credentials on the computer.  
  
> [!NOTE]  
>  If you select Windows Authentication, the user name of the currently logged-on user is populated in the **User name** text box.  
  
 **Password**  
 Enter the password for the specified user.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)   
 [Upgrade Advisor User Interface Reference](../../../2014/sql-server/install/upgrade-advisor-user-interface-reference.md)  
  
  
