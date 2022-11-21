---
title: "'Connect to Existing Secondary Replicas' Page for availability groups"
description: "A description of the various options found on the 'Connect to Existing Secondary Replicas' page within the 'Availability Group wizard' in SQL Server Management Studio."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: end-user-help
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.adddatabasewizard.connecttoreplicas.f1"
  - "sql13.swb.addreplicawizard.connecttoreplicas.f1"
---
# Connect to Existing Secondary Replicas Page - Always On availability groups
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This help topic describes the options of the **Connect to Existing Secondary Replicas** page. This topic is used by the [!INCLUDE[ssAoAddRepWiz](../../../includes/ssaoaddrepwiz-md.md)] and [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] of [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
 **Grid columns:**  
 **Server Instance**  
 Displays the name of the server instance that will host the availability replica.  
  
 **Connected As**  
 Displays the account that is connected to the server instance, once the connection has been established. If this column displays "**Not Connected**" for a given server instance, you will need to click either the **Connect** or **Connect All** button.  
  
 **Connect**  
 Click if this server instance is running under a different account than other server instances to which you need to connect.  
  
 **Connect All**  
 Click only if every instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which you need to connect is running as a service in the same user account.  
  
 **Cancel**  
 Click to cancel the wizard. On the **Connect to Existing Secondary Replica** page, cancelling the wizard cause it to exit without performing any actions.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
