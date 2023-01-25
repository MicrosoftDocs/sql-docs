---
title: "Progress Page (Always On Availability Group Wizards)"
description: "A description of the options found on the 'Progress Page' of the Always On Availability Group Wizard' within SQL Server Management Studio."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.failoverwizard.progress.f1"
  - "sql13.swb.adddatabasewizard.progress.f1"
  - "sql13.swb.addreplicawizard.progress.f1"
  - "sql13.swb.newagwizard.progress.f1"
---
# Progress Page (Always On Availability Group Wizards)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Use this dialog box to view the progress of a [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] wizard that you are running in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)]. The progress bar indicates the relative progress of the steps that the wizard is performing.  
  
## UI element list  
 **More details**  
 Click the down arrow to display a progress grid that lists any completed steps, in order, followed by the current in-progress operation. The grid contains the following columns:  
  
 **Name**  
 Displays a phrase that describes a specific step.  
  
 **Status**  
 Indicates the outcome of completed steps and the percentage of completion of the current step, as follows:  
  
|Result|Description|  
|------------|-----------------|  
|**Error**|Indicates the operation for this step experienced an error. Click the link to display a message dialog box that describes the error.|  
|**In Progress (** *percentage-completed* **)**|Indicates that the operation is occurring now and estimates the percentage of this step that has completed.|  
|**Success**|Indicates the operation for this step completed successfully.|  
  
 **Fewer Details**  
 Click to hide the progress grid.  
  
 **Cancel**  
 Click to skip any remaining operations and then exit the wizard.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md)  
  
-   [Use the Fail Over Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-fail-over-availability-group-wizard-sql-server-management-studio.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
