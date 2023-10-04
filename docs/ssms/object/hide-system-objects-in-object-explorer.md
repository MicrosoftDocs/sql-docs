---
title: "Hide System Objects in Object Explorer"
description: "Hide System Objects in Object Explorer"
author: "markingmyname"
ms.author: "maghan"
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "hiding system objects"
  - "system objects [SQL Server]"
  - "objects [SQL Server], hiding"
  - "Object Explorer, hiding objects"
---
# Hide System Objects in Object Explorer
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
This topic describes how to hide system objects in Object Explorer in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. The **Databases** node of Object Explorer contains system objects such as the system databases. Use the **Tools**/**Options** pages to hide the system objects. Some system objects, such as system functions and system data types, are not affected by this setting.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To hide system objects in Object Explorer  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  On the **Environment/Startup** page, select **Hide system objects in Object Explorer**, and then click **OK**.  
  
3.  In the **SQL Server Management Studio** dialog box, click **OK** to acknowledge that SQL Server Management Studio must be restarted for this change to take effect.  
  
4.  Close and reopen SQL Server Management Studio.  
  
