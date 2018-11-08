---
title: "Deploy From SQL Server Data Tools (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.deploystatus.f1"
ms.assetid: 67dde3fe-ba43-41f3-b56c-c656029ee93f
author: minewiskan
ms.author: owend
manager: craigg
---
# Deploy From SQL Server Data Tools (SSAS Tabular)
  Use the tasks in this topic to deploy a tabular model solution by using the Deploy command in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 Sections in this topic:  
  
-   [Configure Deployment Options and Deployment Server Properties](#bkmk_deploy)  
  
-   [Deploy a Tabular Model Solution](#bkmk_deploy_proc)  
  
-   [Deploy Status](#bkmk_deploy_status)  
  
##  <a name="bkmk_deploy"></a> Configure Deployment Options and Deployment Server Properties  
 Before you deploy your tabular model solution, you must first specify the Deployment Options and Deployment Server properties. For more information about deployment properties and settings, see [Tabular Model Solution Deployment &#40;SSAS Tabular&#41;](tabular-model-solution-deployment-ssas-tabular.md).  
  
#### To configure Deployment Options and Deployment Server properties  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], in **Solution Explorer**, right-click the project name, and then click **Properties**.  
  
2.  In the **\<project name> Properties** dialog, in **Deployment Options**, specify property settings if different from the default settings.  
  
    > [!NOTE]  
    >  For models in cached mode, **Query Mode** is always **In-Memory**.  
  
    > [!NOTE]  
    >  You cannot specify **Impersonation Settings** for models in DirectQuery mode.  
  
3.  In **Deployment Server**, specify the **Server** (name), **Edition**, **Database** (name), and **Cube Name** property settings, if different from the default settings, and then click **OK**.  
  
> [!NOTE]  
>  You can also specify the Default Deployment Server property setting so any new projects you create will automatically be deployed to the specified server. For more information, see [Configure Default Data Modeling and Deployment Properties &#40;SSAS Tabular&#41;](properties-ssas-tabular.md).  
  
##  <a name="bkmk_deploy_proc"></a> Deploy a Tabular Model Solution  
  
#### To deploy a tabular model solution  
  
-   In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], on the **Build** menu, click **Deploy \<project name>**.  
  
     The **Deploy** dialog box will appear and indicate the status of the metadata deployment and the processing (unless Processing Option property is set to Do Not Process) of each table included in the model. After the deployment process is complete, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to connect to the Analysis Services instance and verify the new model database object has been created or use a client reporting application to connect to the deployed model.  
  
##  <a name="bkmk_deploy_status"></a> Deploy Status  
 The **Deploy** dialog box enables you to monitor the progress of a Deploy operation. A deploy operation can also be stopped.  
  
 **Status**  
 Indicates whether the Deploy operation was successful or not.  
  
 **Details**  
 Lists the metadata items that were deployed, the status for each metadata item, and provides a message of any issues.  
  
 **Stop Deploy**  
 Click to halt the Deploy operation. This option is useful if the Deploy operation is taking too long, or if there are too many errors.  
  
## See Also  
 [Tabular Model Solution Deployment &#40;SSAS Tabular&#41;](tabular-model-solution-deployment-ssas-tabular.md)   
 [Configure Default Data Modeling and Deployment Properties &#40;SSAS Tabular&#41;](properties-ssas-tabular.md)  
  
  
