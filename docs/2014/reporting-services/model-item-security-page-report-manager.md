---
title: "Model Item Security Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.modelproperties.modelitemsecurity.f1"
ms.assetid: 8c5b29ae-1f17-41f2-ab59-97899b8fb4fc
author: markingmyname
ms.author: maghan
manager: kfile
---
# Model Item Security Page (Report Manager)
  Use this page to secure parts of a model by granting or revoking read-only permissions on particular items. Model item security affects ad hoc data exploration at run time and the ability to use parts of a published model when creating reports in Report Builder. To use this feature, you must have Content Manager permissions.  
  
 Model item security is applied to a model that is processed on the report server and does not affect .smdl files that you edit in Model Designer or use in Report Designer. Furthermore, it has no effect on users who have permission to modify a model definition. Any user who has Content Manager or Publisher permissions on a model can see all parts of it, regardless of whether you apply model item security.  
  
> [!NOTE]  
>  Model items can be further secured through the use of security filters.  
  
 You can define model item security on entities, folders, and individual fields within a model. Because a model presents such a large surface of securable items, permission inheritance is built into the model so that you can secure a large number of items through a relatively small number of role assignments. Permission inheritance is based on the following:  
  
-   Model  
  
-   Root node  
  
-   Folders or entities  
  
-   Fields  
  
 Initially, permission to access to model items is inherited through role assignments that are set on the model itself. A user who has permission to view a model in a folder in Report Manager can view all of the items in the model.  
  
 If you apply model item security, you must create at least one role assignment on the root node. This initial role assignment on the root node becomes the new source of inherited permissions. The role assignment on the root node is automatically inherited by all items in the model hierarchy.  
  
 To further customize permissions on data exploration, you can vary permissions on folders and entities. Finally, you can set permissions on individual fields.  
  
 To make role assignments easier to maintain, set permissions only on folders or entities rather than individual fields. You cannot search for role assignments that you create. If you set security on specific fields and you want to update the security settings later, you must click through the model namespace to find the fields you secured.  
  
 To get started, create a role assignment on the root node and then create additional role assignments on entities and folders. To clear model item security, clear the check box for **Secure individual model items independently for this model**. Clearing the check box reverts back to the initial permissions that are inherited from the model.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the General properties page for a report  
  
1.  Open Report Manager, and locate the model for which you want to configure security for model items.  
  
2.  Hover over the model, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the model.  
  
4.  Select the **Model Item Security** tab.  
  
## Options  
 **Secure individual model items independently for this model**  
 Click this check box to enable model item security.  
  
 **Specify security for individual model items in the mode**  
 Shows all of the items in a model. You can navigate the model namespace to select the item you want to secure. You can only select one item at a time. Be sure to create the first role assignment on the root node before proceeding to other entities and folders.  
  
 **Inherit permissions from the parent item**  
 Click this option to inherit the security settings of the parent item.  
  
 **Assign read permission to the following users and groups (semi-colon separated)**  
 Click this option to specify the user or group account for which you are defining access. If you are using default security, the user and group accounts are Windows domain accounts. Specify the accounts in this format: *\<domain>\\<account\>*.  
  
## See Also  
 [Report Server in Management Studio F1 Help](tools/report-server-in-management-studio-f1-help.md)  
  
  
