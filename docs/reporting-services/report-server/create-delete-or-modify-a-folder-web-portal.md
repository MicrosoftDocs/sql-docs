---
title: "Create, delete, or modify a folder - Reporting Services"
description: Learn how to create, modify, and delete folders so that you can organize and manage the items that you publish to a Reporting Services report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Create, delete, or modify a folder - Reporting Services
  You can create folders to organize and manage the items you publish to a report server. Creating folders can help users find reports of interest to them. For content managers, folders provide a framework for applying permissions. You can create role assignments on specific folders to restrict access to reports that are in development or that shouldn't be widely distributed.  

::: moniker range="=sql-server-2016"

## Create a folder  
  
1.  Start [Report Manager &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md).  
  
1.  In Report Manager, select the Home folder and choose **New Folder**. Or, to create a folder under an existing folder, navigate to that folder in the **Contents** page and select the folder to open it. Then, choose **New Folder**.  
  
     The **New Folder** page opens.  
  
1.  Enter a folder name. A folder name can include spaces, but can't include reserved characters that are used for URL encoding: \; \? \: \@ \& \= \+ \, \$ \/ \* \< \> \|. You can't type a series of folder names to create several folders at once.  
  
1.  Optionally, enter a description.  
  
1.  Select **Hide in list view** if you don't want to display the folder in the default view of the **Contents** page. The folder is visible to users only when they select **Show Details** on the **Contents** page.  
  
1.  Select **OK**.  
  
## Delete a folder  
  
1.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to modify.  
  
1.  Hover over the item, and select the dropdown arrow.  
  
1.  In the menu, choose **Delete**.  
  
1.  Select **OK**.
  
## Modify or delete a folder  
  
1.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to modify.  
  
1.  Hover over the item, and select the dropdown arrow.  
  
1.  In the menu, choose **Manage**. The **General Properties** page opens.  
  
1.  To change the folder location, select **Move**. Enter the location of the destination folder, or choose the destination folder from the tree, and then select **OK**.  
  
1.  Or, modify folder properties in the following ways:  
  
    -   To modify display text about the folder, enter a name or description.  
  
    -   To display the folder in the default view on the **Contents** page, clear **Hide in list view**.  
  
1.  Or, to remove the folder and its contents, select **Delete**.  
  
1.  Select **Apply** to save changes.  

::: moniker-end

::: moniker range=">=sql-server-2017"
 
## Create a folder  
  
1. Open [the web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
1. Navigate to folder or subfolder where you want to locate the new folder. Select the **Home** folder by selecting the **Browse** button on the toolbar at the top left of the page to create it at the top of the folder hierarchy.  
  
1. Select the **New** button on the top right of the report server toolbar, and then select **Folder** from the drop-down menu.  
  
1. In the **Create a new folder in (current folder name)** dialog box, enter the name of the new folder to be created. A folder name can include spaces, but can't include reserved characters that are used for URL encoding: \; \? \: \@ \& \= \+ \, \$ \/ \* \< \> \|. You also can't type a series of folder names to create several folders at once.  
  
1. Select **Create** to complete the action.  
  
## Delete a folder  
  
1. In the web portal, navigate the folder hierarchy and locate the folder that you want to delete.  
  
1. Right-click the folder, and select **Delete** from the menu.  
  
1. Select the **Delete** button in the **Delete \<foldername\>** box to confirm the deletion.  
  
## Modify a folder's properties  
  
1. In the web portal, navigate the folder hierarchy and locate the folder that you want to delete.  
  
1. Right-click the folder, and select **Delete** from the menu.  
  
1. Select the **Properties** tab. The **Properties** page displays by default.  
  
1. You can change the name of the folder in the **Name** text box.  
  
1. You can add or change the description of the folder in the **Description** text box.  
  
1. You can hide the folder by selecting the **Hide this item** checkbox.  
  
1. Select **Apply** to save the properties changes.  
  
1. Optionally, you can move or delete the folder by selecting **Move** or **Delete** at the top of the **Properties** page. For more information, see the [Move or delete an item (web portal)](../../reporting-services/report-server/move-or-delete-an-item-report-manager.md).  
  
## Related content 
-  [Create, delete, or modify a folder (web portal)](../../reporting-services/report-server/create-delete-or-modify-a-folder-web-portal.md)   
-  [Report server content management (SSRS native mode)](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
-  [Find, view, and manage reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)    
  
::: moniker-end
