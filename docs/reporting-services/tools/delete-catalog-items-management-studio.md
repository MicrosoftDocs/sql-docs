---
title: "Delete Catalog Items (Management Studio)"
description: Learn about the options on the Delete Catalog Items page of Management Studio that allow you to delete shared schedules and role definitions.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.deleteitems.f1"
---
# Delete Catalog Items (Management Studio)
  Use this page to delete shared schedules and role definitions.  
  
 If you delete a shared schedule that is used by multiple reports and subscriptions, the report server creates individual schedules for each report and subscription that previously used the shared schedule. Each new individual schedule contains the date, time, and recurrence pattern that was specified in the shared schedule. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't provide central management of individual schedules. If you delete a shared schedule, you now need to maintain the schedule information for each individual item. Before deleting a shared schedule, use the [Reports page](../../reporting-services/tools/schedule-properties-reports-page.md) to determine the reports that currently use the shared schedule.  
  
 For role definitions, you can only delete those definitions that aren't actively used in a role assignment. If you try to delete a role that is currently in use, the report server doesn't delete the role and you see an error message to that effect. If this page contains a single role definition that isn't currently in use, you delete it when you select **OK**. If this page contains multiple roles, you can't select which roles to keep or remove. All unused role definitions are deleted when you select **OK**.  
  
 You can't undo a delete operation. To recover an item that you deleted, you must either recreate it or restore a backup copy of the report server database.  
  
## Options  
 **Name**  
 Specifies the name of the item you're deleting.  
  
 **Type**  
 Shows the type of item you're deleting.  
  
 **Owner**  
 Shows the name of the owner. In most cases, this name is System.  
  
 **Status**  
 Shows progress information for a delete operation.  
  
 **Error**  
 Displays an error code if an error occurs while deleting an item.  
  
## Related content 
 [Delete an item &#40;Management Studio&#41;](../../reporting-services/tools/delete-an-item-management-studio.md)   
 [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Create, modify, and delete schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md)  
  
  
