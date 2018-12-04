---
title: "Select Databases Page (New Availability Group Wizard-Add Database Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.adddatabasewizard.selectdatabases.f1"
  - "sql12.swb.newagwizard.selectdatabases.f1"
ms.assetid: 929c5e15-d087-438d-b1f2-aa97c5f8bff8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Select Databases Page (New Availability Group Wizard-Add Database Wizard)
  This help topic describes the options of the **Specify Databases** page. This topic applies to the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)] and [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
##  <a name="PageOptions"></a> Select Databases Options  
 The **User databases on this instance of SQL Server** grid lists every local user database. The columns are as follows:  
  
 **Name**  
 Displays the name of a local user database.  
  
 **Size**  
 Displays the database size, if the size is available to the wizard.  
  
 **Status**  
 Displays a hyperlink whose text that indicates whether a given database meets the prerequisites for being added to an availability group. If the status is "**Meets prerequisites**" you can add the database to the availability group. If a database does not meet all of the prerequisites, the **Status** hyperlink provides a brief explanation of why the database is ineligible. For more information, click the hyperlink.  
  
 You can leave the wizard on the **Select Database** page while you take action on a database to meet a prerequisite. When you return to the **Select Databases** page, click **Refresh** to update the grid.  
  
 **Refresh**  
 Click to refresh the grid. This is useful after you take action on a database to meet a prerequisite.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](availability-group-add-database-to-group-wizard.md)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
