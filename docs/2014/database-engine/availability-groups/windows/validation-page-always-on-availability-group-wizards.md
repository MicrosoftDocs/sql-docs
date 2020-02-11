---
title: "Validation Page (AlwaysOn Availability Group Wizards) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.newagwizard.validation.f1"
  - "sql12.swb.addreplicawizard.validation.f1"
  - "sql12.swb.adddatabasewizard.validation.f1"
helpviewer_keywords: 
  - ", listeners"
ms.assetid: c8971556-240c-491a-bc86-9cc72f71a3dd
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Validation Page (AlwaysOn Availability Group Wizards)
  This help topic describes the options of the **Validation** page. This topic applies to the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)], [!INCLUDE[ssAoAddRepWiz](../../../includes/ssaoaddrepwiz-md.md)], and [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. Use this page to validate that your environment supports all the configuration choices you made on previous pages of the wizard.  
  
##  <a name="PageOptions"></a> Validation Page Options  
 **Results of availability group validation.**  
 This grid displays the results of each completed validation step. The grid columns are as follows:  
  
 **Name**  
 Displays a phrase that describes a specific step.  
  
 **Result**  
 Displays one of the following hyperlink texts. For more information about the result of given validation step, click the hyperlink.  
  
|Result|Description|  
|------------|-----------------|  
|**Error**|Indicates that the validation step failed. Click the link to view the error message.|  
|**Skipped**|Indicates that the validation step was skipped because it is not required by your selections. Click the link to view the reason that a step was skipped.|  
|**Success**|Indicates that the validation step completed successfully|  
|**Warning**|Indicates a potential issue with the availability group configuration.  Click the link to view the warning message.|  
  
 **Re-run Validation**  
 Click to repeat the validation steps if you make a change outside of the wizard in response to a validation error.  
  

  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](availability-group-add-database-to-group-wizard.md)  
  
 
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)  
  
  
