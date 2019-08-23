---
title: "Prevent Automatic Startup of an Instance of SQL Server (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "automatic SQL Server startup"
  - "SQL Server, stopping"
  - "SQL Server, automatic startup"
  - "canceling automatic startup"
  - "stopping SQL Server"
  - "preventing automatic startups [SQL Server]"
ms.assetid: 782663cf-f3d7-4cc6-b621-21e4550f0322
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Prevent Automatic Startup of an Instance of SQL Server (SQL Server Configuration Manager)
  This topic describes how prevent an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from starting automatically in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using SQL Server Configuration Manager. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is normally configured to start automatically. You can change that by setting the start mode for the instance to manual.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To prevent automatic startup of an instance of SQL Server  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In SQL Server Configuration Manager, expand **Services**, and then click **SQL Server**.  
  
3.  In the details pane, right-click **MSSQLServer**, and then click **Properties.**  
  
4.  In the **SQL Server \<**_instancename_**> Properties** dialog box, in the **Properties** box, set the value of **Start Mode** to **Manual**.  
  
5.  Click **OK** to close the **SQL Server \<**_instancename_**> Properties** dialog box, and then close [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
## See Also  
 [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](start-stop-pause-resume-restart-sql-server-services.md)  
  
  
