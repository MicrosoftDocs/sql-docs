---
title: "SQL Server Browser Properties (Service Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 98ace9b0-72d5-4b72-9b7b-11fbc490981a
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Browser Properties (Service Tab)
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser program runs as a service on the server. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens for incoming requests for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer.  
  
 Use the **Service** tab on the **SQL Server Browser Properties** dialog box to view the following options. All properties except **Start Mode** are read-only.  
  
## Options  
 **Binary Path**  
 Displays the location of the program files used by this service.  
  
 **Error Control**  
 1 indicates `SERVICE_ERROR_NORMAL`. If the service fails to start during computer start up, the startup program logs the error and displays a pop-up message box but continues the startup operation. This value cannot be changed.  
  
 **Exit Code**  
 When an error occurs, the error number appears in this box. Use this number to troubleshoot failures by searching for the number in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base or provide the number to your technical support staff.  
  
 **Host Name**  
 Displays the name of the computer or cluster running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.  
  
 **Name**  
 Indicates the display name of the service.  
  
 **Process ID**  
 Displays the Windows process ID.  
  
 **Service Type**  
 Displays the type of service provided to calling processes. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installs several services.  
  
 **Start Mode**  
 Set this service to the following choices:  
  
-   Manual: This service does not automatically start when the computer starts. You must start the service using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, or some other tool.  
  
-   Automatic: This service attempts to start when this computer starts.  
  
-   Disabled: This service cannot be started.  
  
 **State**  
 Indicates whether this service is running, stopped, or disabled. "**...**" indicates a state change is pending.  
  
## See Also  
 [SQL Server Browser Service](../../../2014/tools/configuration-manager/sql-server-browser-service.md)  
  
  
