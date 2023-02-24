---
description: "Configure WMI to Show Server Status in SQL Server Tools"
title: "Configure WMI to Show Server Status in SQL Server Tools"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "WMI Provider for Server Events, setting permissions"
  - "WMI permissions [SQL Server]"
ms.assetid: 7e97197b-ed4d-40d1-9a52-9ab1d92401d7
author: "markingmyname"
ms.author: "maghan"
---
# Configure WMI to Show Server Status in SQL Server Tools

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This topic describes how to configure WMI to show the server status in SQL Server tools in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)]. When connecting to servers, both the Registered Servers and Object Explorer components of [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], as well as [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager, use Windows Management Instrumentation (WMI) to obtain the status of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (MSSQLSERVER) and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent (MSSQLSERVER) services. To display the status of the service, the user must have rights to remotely access the WMI object. The server must have WMI installed to configure this permission.  
  
## <a name="SSMSProcedure"></a>To configure WMI permission  
  
1.  On the **Start** menu on the remote server, click **Run**.  
  
2.  In the **Open** box type **wmimgmt.msc**, and then click **OK**.  
  
3.  In the **Windows Management Infrastructure** program, right-click **WMI Control (Local)**, and then click **Properties**.  
  
4.  In the **WMI Control (Local) Properties** dialog box, on the **Security** tab, expand **Root**, and then click **CIMV2**.  
  
5.  Click **Security** to open the **Security for ROOT\CIMV2** dialog box.  
  
6.  Add a group or user to the **Group or user names** box and select it.  
  
7.  In the **Permissions for**_\<group or user\>_ box, select the **Allow** column, for the **Remote Enable** permission, for users whom you wish to remotely detect the service status.  
  
## See Also  
[Start, Stop, or Pause the SQL Server Agent Service](../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)  
  
