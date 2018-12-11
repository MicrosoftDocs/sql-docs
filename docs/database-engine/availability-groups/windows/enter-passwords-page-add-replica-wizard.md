---
title: "Enter Passwords Page (Add Replica Wizard) for availability groups"
description: "A description of the properties found on the 'Enter Passwords Page' of the 'Add Replica' wizard in SQL Server Management Studio."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.addreplicawizard.enterpasswords.f1"
ms.assetid: e69207a0-c5c4-44e4-ae9a-4afbb67251d1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Enter Passwords Page (Add Replica Wizard) for Always On availability groups
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This help topic describes the options of the **Enter Passwords** page. This topic applies to the [!INCLUDE[ssAoAddRepWiz](../../../includes/ssaoaddrepwiz-md.md)] of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 If the replicas that you selected on the **Specify Replicas** page contain databases that have a database master key, the Enter Passwords page appears.  
  
## Enter Passwords Options  
 The **User databases on this instance of SQL Server** grid lists every local user database. The columns are as follows:  
  
 **Name**  
 Displays the name of a local user database.  
  
 **Size**  
 Displays the database size, if the size is available to the wizard.  
  
 **Status**  
 Indicates **Password required** for databases that have a database master key. After you enter the passwords for the database master keys in the **Passwords** column, click **Refresh**. If you entered the passwords correctly, the **Status** column indicates **Password entered**.  
  
 If the database doesn't have a database master key, the **Status** column indicates **Password not required**.  
  
 **Password**  
 If the **Status** column indicates **Password required**, enter the password for the database master key.  
  
 **Refresh**  
 Click to refresh the grid. This is useful after you enter required passwords.  
  
## Related Tasks  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
## See Also  
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
