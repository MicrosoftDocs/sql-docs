---
title: "Options (SQL Server AlwaysOn, Dashboard Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Alwayson.Dashboard"
ms.assetid: 4369b588-e982-4b57-80a1-beb2e879ce0b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Options (SQL Server AlwaysOn, Dashboard Page)
  Use the **SQL Server AlwaysOn Dashboard** page of the [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]**Options** dialog box to configure the AlwaysOn Dashboard.  
  
 **To access this page:**  
  
 On the **Tools** menu, click **Options**, expand the **SQL Server AlwaysOn** folder, and then click **Dashboard**.  
  
## On This Page  
 **Turn on automatic refresh.**  
 Click to enable automatic refresh. The options are:  
  
-   The **Refresh interval (in seconds)** field displays the number of seconds at which the dashboard will refresh. The default value is 30. When automatic refresh is enabled, you can edit this field to change the refresh interval.  
  
-   The **Number of connection retries** displays the number of times that the dashboard will attempt to connect to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts an availability replica for an availability group that the Dashboard is monitoring. The default value is 65535. When automatic refresh is enabled, you can edit this field to change the number of connection retries.  
  
 **Enable your user-defined AlwaysOn policy.**  
 If you have defined your own AlwaysOn policy, click this option to enable your policy.  
  
## See Also  
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
