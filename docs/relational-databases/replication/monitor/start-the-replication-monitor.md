---
title: "Start the Replication Monitor"
description: "Start the Replication Monitor"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Replication Monitor, starting"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Start the Replication Monitor
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Replication Monitor can be started from [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] on any instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], or from the command prompt. After starting Replication Monitor, add one or more Publishers to monitor. For more information, see [Add and Remove Publishers from Replication Monitor](../../../relational-databases/replication/monitor/add-and-remove-publishers-from-replication-monitor.md).  
  
### To start Replication Monitor from SQL Server Management Studio  
  
1.  Connect to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder or any of its subfolders, and then click **Launch Replication Monitor**.  

### To start Replication Monitor from the command prompt  
  
1.  From the command prompt, navigate to the tools installation directory. The default path is [!INCLUDE[ssInstallPath](../../../includes/ssinstallpath-md.md)]Tools\Binn\ .  
  
2.  Run sqlmonitor.exe.  
  
## Related content

- [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)
