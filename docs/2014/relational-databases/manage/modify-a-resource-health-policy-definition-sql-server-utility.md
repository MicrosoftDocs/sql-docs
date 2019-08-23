---
title: "Modify a Resource Health Policy Definition (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "SQL12.SWB.UE.UTILITY.ADMINISTRATION.F1"
ms.assetid: 27bec0b6-92e9-448e-8c70-fe36802cf128
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Modify a Resource Health Policy Definition (SQL Server Utility)
  This topic describes how to modify a resource health policy definition in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Before you modify a resource utilization policy in your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, you must create a utility control point (UCP). For more information, see [SQL Server Utility Features and Tasks](sql-server-utility-features-and-tasks.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility resource utilization policies can be configured for data-tier applications and managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Resource utilization policies can be defined globally for all data-tier applications and managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, or they can be defined individually for each data-tier application and for each managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. You can also implement global policies and have individual data-tier applications or managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configured with their own policy definitions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### Modify global resource utilization policies in a SQL Server Utility.  
  
1.  Connect to the UCP in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  In the Utility Explorer navigation pane, click **Utility Administration** to view or modify global monitoring policies, then click the **Policy** tab in the Utility Explorer content pane.  
  
3.  In the Utility Explorer content pane, select **Set global data-tier monitoring policies** or **Set global managed instance monitoring policies** by clicking on the arrow or the policy description.  
  
4.  Use the controls on the right side of the policy descriptions to set underutilization or overutilization policy thresholds.  
  
5.  Use the **Apply**, **Discard**, or **Restore Default** buttons as needed. The policy change can take up to 15 minutes to propagate back to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility dashboard and list view details.  
  
6.  To refresh data, right-click on the **Utility Administration** node in the Utility Explorer navigation pane, and select **Refresh**.  
  
#### Modify resource health policy definitions for an individual data-tier application or an individual managed instance of SQL Server in a SQL Server Utility  
  
1.  Connect to the UCP in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  In the Utility Explorer navigation pane, click **Deployed Date-tier Applications**, or click **Managed Instances**, to view or modify monitoring policies for an individual data-tier application or managed instance.  
  
3.  In the Utility Explorer content pane list view, click the data-tier application or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name whose policies you would like to modify, then click on the **Policy Details** tab.  
  
4.  Select the policy to view or modify by clicking on the arrow or the policy description. Global policies are selected by default.  
  
5.  Select the radio button to **Override the global policy** to override global policies and implement an individual policy definition for the specified data-tier application.  
  
6.  Use the controls on the right side of the policy description to set underutilization or overutilization policy thresholds.  
  
7.  Use the **Apply**, **Discard**, or **Restore Default** buttons as needed. The policy change can take up to 15 minutes to propagate back to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility dashboard and list view details.  
  
8.  To refresh data, right-click on the **Deployed Data-tier Applications** node in the Utility Explorer navigation pane, and select **Refresh**.  
  
## See Also  
 [SQL Server Utility Features and Tasks](sql-server-utility-features-and-tasks.md)   
 [View Resource Health Policy Results &#40;SQL Server Utility&#41;](view-resource-health-policy-results-sql-server-utility.md)  
  
  
