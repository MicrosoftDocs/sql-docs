---
title: "SQL Server Agent Properties (Service Tab)"
description: Learn about the options on the Service tab in the SQL Server Agent Properties dialog box, such as the binary path, the process ID, and the start mode.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 452857fb-be1b-4e1e-851c-dd2216640f35
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# SQL Server Agent Properties (Service Tab)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  This service is the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service. The property values in light gray cannot be changed using this application.  
  
## Options  
 **Binary Path**  
 Displays the location of the program files used by this service.  
  
 **Error Control**  
 1 indicates `SERVICE_ERROR_NORMAL`. If the service fails to start during computer start up, the startup program logs the error and displays a pop-up message box but continues the startup operation. This value cannot be changed.  
  
 **Exit Code**  
 When an error occurs, the error number appears in this box. Use this number to troubleshoot failures by searching for the number in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base or provide the number to your technical support staff.  
  
 **Host Name**  
 Displays the name of the computer or cluster running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
 **Name**  
 Indicates the display name of the service.  
  
 **Process ID**  
 Displays the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows process ID.  
  
 **SQL Service Type**  
 Displays the type of service provided to calling processes. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installs several services.  
  
 **Start Mode**  
 Set this service to the following choices:  
  
-   Manual: This service does not automatically start when the computer starts. You must start the service using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, or some other tool.  
  
-   Automatic: This service attempts to start when this computer starts.  
  
-   Disabled: This service cannot be started.  
  
 **State**  
 Indicates whether this service is running, stopped, or disabled. "**...**" indicates a state change is pending.  
  
  
