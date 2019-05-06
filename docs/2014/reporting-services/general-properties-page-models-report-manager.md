---
title: "General Properties Page, Models (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.modelproperties.general.f1"
ms.assetid: 7ad59850-8135-4c4d-95e9-6d705b6d77a8
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# General Properties Page, Models (Report Manager)
  Use the General properties page for report models to rename, delete, move, or replace the model definition (.smdl) file. Details about who created or modified the model and when the changes took place are indicated at the top of the page.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the General properties page for a model  
  
1.  Open Report Manager, and locate the model for which you want to view or configure properties.  
  
2.  Hover over the model, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the model.  
  
## Options  
 **Name**  
 Specifies the name of the model. A name must contain at least one alphanumeric character. It can also include spaces and some symbols. Do not use the following characters when specifying a name:  
  
 ; ? : \@ & = + , $ / * \< > | " /  
  
 **Description**  
 Type a description of the model. This description appears in the Contents page to users who have permission to access the modelt.  
  
 **Hidden in list view**  
 Select this check box to hide the item when the folder is set in list view. List view is a mode for viewing folder contents that is supported in Report Manager. You can set this option in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] to define how this item is viewed in Report Manager. For more information about view modes in Report Manager, see [Contents Page &#40;Report Manager&#41;](../../2014/reporting-services/contents-page-report-manager.md).  
  
 **Apply**  
 Click to save your changes.  
  
 **Delete**  
 Click to remove the model from the report server database. Deleting a model does not delete the dependent shared data source that provides connection information, nor does it delete reports that use the model as a data source. However, after the model is deleted, reports that use the model will no longer run.  
  
 **Move**  
 Click to relocate a model within the report server folder hierarchy. Clicking this button opens the Move Items page, on which you can browse through folders for a new folder location. For more information, see [Move Items Page &#40;Report Manager&#41;](../../2014/reporting-services/move-items-page-report-manager.md).  
  
 **Save**  
 Click to save a read-only copy of the model definition. Depending on the file associations defined on your computer, the file will open in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] or a different application. In most cases, the model opens as an XML file.  
  
 The copy that you open is identical to the original model definition that was initially published to the report server. Any properties that were set on the model after it was published (such as data source properties) are not reflected in the file that you open.  
  
 You can modify the model definition and save it as a new file in a shared folder, and upload the model definition to the report server as a new item. Modifications that you make to the model definition while it is open in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] (or another application) are not saved directly to the report server. You must upload the file to publish the modified model to the report server.  
  
 Note that if you want to open the report model in Model Designer, you should save the model as .smdl file, and then add the .smdl file to a project in Model Designer.  
  
 **Replace**  
 Click to replace the model definition with a different one from an .smdl file located on the file system. If you update a model definition, you must reset the shared data source settings after the update is complete.  
  
 **Regenerate Model**  
 Click to regenerate a default model that replaces the current version. This option appears after the model is generated. The generated model is based on the shared data source. It cannot be customized before it is generated. However, after you generate it, you can click **Edit** to open the model definition, save it to the file system, and then add it to a project in Model Designer. After you refine the model, you can upload it to the report server as a new item, or click **Update** on this page to replace the generated model with the version you revised in Model Designer.  
  
## See Also  
 [Bind a Report or Model to a Shared Data Source &#40;SSRS&#41;](report-data/bind-a-report-or-model-to-a-shared-data-source-ssrs.md)   
 [Report Server in Management Studio F1 Help](tools/report-server-in-management-studio-f1-help.md)  
  
  
