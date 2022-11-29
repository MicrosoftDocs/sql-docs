---
description: "Start the Replication Monitor"
title: "Start the Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Replication Monitor, starting"
ms.assetid: e037bd27-cc87-4ee9-9e5f-83f6d717cfa4
author: "MashaMSFT"
ms.author: "mathoma"
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
  
## See Also  
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
