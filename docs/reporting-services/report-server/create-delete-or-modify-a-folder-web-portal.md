---
title: "Create, Delete, or Modify a Folder - Reporting Services | Microsoft Docs"
description: Learn how to create, modify, and delete folders so that you can organize and manage the items that you publish to a Reporting Services report server.
ms.date: 06/26/2019
ms.service: reporting-services
ms.subservice: report-server

ms.topic: conceptual
ms.assetid: 70a38879-856c-414b-8479-5f9dead38f15
author: maggiesMSFT
ms.author: maggies
---
# Create, Delete, or Modify a Folder - Reporting Services
  You can create folders to organize and manage the items you publish to a report server. Creating folders can help users find reports of interest to them. For content managers, folders provide a framework for applying permissions. You can create role assignments on specific folders to restrict access to reports that are in development or that should not be widely distributed.  

::: moniker range="=sql-server-2016"

## To create a folder  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../web-portal-ssrs-native-mode.md).  
  
2.  In Report Manager, select the Home folder and click **New Folder**. Or, to create a folder under an existing folder, navigate to that folder in the **Contents** page and click the folder to open it. Then click **New Folder**.  
  
     The **New Folder** page opens.  
  
3.  Type a folder name. A folder name can include spaces, but cannot include reserved characters that are used for URL encoding: \; \? \: \@ \& \= \+ \, \$ \/ \* \< \> \|. You cannot type a series of folder names to create several folders at once.  
  
4.  Optionally type a description.  
  
5.  Select **Hide in list view** if you do not want to display the folder in the default view of the **Contents** page. The folder will be visible to users only when they click **Show Details** on the **Contents** page.  
  
6.  Click **OK**.  
  
## To delete a folder  
  
1.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to modify.  
  
2.  Hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Delete**.  
  
4.  Select **OK**.
  
## To modify or delete a folder  
  
1.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to modify.  
  
2.  Hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. The General Properties page opens.  
  
4.  To change the folder location, click **Move**. Type the location of the destination folder, or choose the destination folder from the tree, and then click **OK**.  
  
5.  Or, modify folder properties in the following ways:  
  
    -   To modify display text about the folder, type a name or description.  
  
    -   To display the folder in the default view on the **Contents** page, clear **Hide in list view**.  
  
6.  Or, to remove the folder and its contents, click **Delete**.  
  
7.  Click **Apply** to save changes.  

::: moniker-end

::: moniker range=">=sql-server-2017"
 
## To create a folder  
  
1. Open [the web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
2. Navigate to folder or sub-folder where you want to locate the new folder. Select the **Home** folder by selecting the **Browse** button on the toolbar at the top left of the page to create it at the top of the folder hierarchy.  
  
3. Select the **New** button on the top right of the report server toolbar, and then select **Folder** from the drop-down menu.  
  
4. In the **Create a new folder in (current folder name)** dialog box, enter the name of the new folder to be created. A folder name can include spaces, but can't include reserved characters that are used for URL encoding: \; \? \: \@ \& \= \+ \, \$ \/ \* \< \> \|. You also can't type a series of folder names to create several folders at once.  
  
5. Select **Create** to complete the action.  
  
## To delete a folder  
  
1. In the web portal, navigate the folder hierarchy and locate the folder that you want to delete.  
  
2. Right-click-the folder, and select **Delete** from the drop-down menu.  
  
3. Select the **Delete** button in the **Delete \<foldername\>** dialog box to confirm the deletion.  
  
## To modify a folder's properties  
  
1. In the web portal, navigate the folder hierarchy and locate the folder that you want to delete.  
  
2. Right-click-the folder, and select **Delete** from the drop-down menu.  
  
3. Select the **Properties** tab. The **Properties** page is displayed by default.  
  
4. You can change the name of the folder in the *Name** textbox.  
  
5. You can add  or change the description of the folder in the *Description** textbox.  
  
6. You can hide or un-hide the folder by checking or un-checking the **Hide this item** checkbox respectively.  
  
7. Select **Apply** to save the properties changes.  
  
8. Optionally, you can move or delete the folder by selecting the **Move** or **Delete** buttons at the top of the **Properties** page. See the [Move or Delete an Item (web portal)](../../reporting-services/report-server/move-or-delete-an-item-report-manager.md) article for more information.  
  
## See also  
 [Create, Delete, or Modify a Folder (web portal)](../../reporting-services/report-server/create-delete-or-modify-a-folder-web-portal.md)   
 [Report Server Content Management (SSRS Native Mode)](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)    
  
::: moniker-end