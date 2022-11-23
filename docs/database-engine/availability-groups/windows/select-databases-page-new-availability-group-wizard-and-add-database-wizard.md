---
title: "Select Databases Page (New Availability Group & Add Database Wizard)"
description: Describes the Select Databases page for both the New Availability Group and Add Database Wizards found in SQL Server Management Studio GUI.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.adddatabasewizard.selectdatabases.f1"
  - "sql13.swb.newagwizard.selectdatabases.f1"
---
# Select Databases Page (New Availability Group Wizard and Add Database Wizard)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This help topic describes the options of the **Specify Databases** page. This topic applies to the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)] and [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] of [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
##  <a name="PageOptions"></a> Select Databases Options  
 The **User databases on this instance of SQL Server** grid lists every local user database. The columns are as follows:  
  
 **Name**  
 Displays the name of a local user database.  

 **Size**  
 Displays the database size, if the size is available to the wizard.  
  
 **Status**  
 Displays a hyperlink whose text that indicates whether a given database meets the prerequisites for being added to an availability group. If the status is "**Meets prerequisites**" you can add the database to the availability group. If a database does not meet all of the prerequisites, the **Status** hyperlink provides a brief explanation of why the database is ineligible. For more information, click the hyperlink.  
  
 You can leave the wizard on the **Select Database** page while you take action on a database to meet a prerequisite. When you return to the **Select Databases** page, click **Refresh** to update the grid.  
  
 **Password**  
 If the database contains a database master key, enter the password for the database master key.  
  
 **Refresh**  
 Click to refresh the grid. This is useful after you take action on a database to meet a prerequisite.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
