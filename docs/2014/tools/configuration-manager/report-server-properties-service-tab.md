---
title: "Report Server Properties (Service Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 2a2e1dbf-02b9-4893-aaf0-c0e4a2c9b4f9
author: stevestein
ms.author: sstein
manager: craigg
---
# Report Server Properties (Service Tab)
  This service is the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Report Server service. The property values in light gray cannot be changed by using this application.  
  
## Options  
 **Binary Path**  
 Displays the location of the program files used by this service.  
  
 **Error Control**  
 1 indicates "SERVICE_ERROR_NORMAL". If the service fails to start during computer start up, the startup program logs the error and displays a pop-up message box but continues the startup operation. This value cannot be changed.  
  
 **Exit Code**  
 When an error occurs, the error number appears in this box. Use this number to troubleshoot failures by searching for the number in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base or provide the number to your technical support staff.  
  
 **Host Name**  
 Displays the name of the computer or cluster running the full text search.  
  
 **Name**  
 Indicates the display name of the service.  
  
 **Process ID**  
 Displays the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows process ID.  
  
 **SQL Service Type**  
 Type of service provided to calling processes. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installs several services.  
  
 **Start Mode**  
 Set this service to the following choices:  
  
-   Manual: This service does not automatically start when the computer starts. You must start the service using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, or some other tool.  
  
-   Automatic: This service attempts to start when this computer starts.  
  
-   Disabled: This service cannot be started.  
  
 **State**  
 Indicates whether this service is running, stopped, or disabled.  
  
## See Also  
 [SQL Server Services](../../../2014/tools/configuration-manager/sql-server-services.md)  
  
  
