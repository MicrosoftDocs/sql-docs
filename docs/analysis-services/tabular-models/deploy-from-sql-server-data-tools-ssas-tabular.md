---
title: "Deploy Analysis Services tabular models from SQL Server Data Tools | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Deploy From SQL Server Data Tools
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Use the tasks in this topic to deploy a tabular model solution by using the Deploy command in SSDT.  
  
##  <a name="bkmk_deploy"></a> Configure deployment options and deployment server properties  
 Before you deploy your tabular model solution, you must first specify the Deployment Options and Deployment Server properties. For more information about deployment properties and settings, see [Tabular model solution deployment](../../analysis-services/tabular-models/tabular-model-solution-deployment-ssas-tabular.md).  
  
#### To configure options and properties  
  
1.  In SSDT, in **Solution Explorer**, right-click the project name, and then click **Properties**.  
  
2.  In the **\<project name> Properties** dialog, in **Deployment Options**, specify property settings if different from the default settings.  
  
    > [!NOTE]  
    >  For models in cached mode, **Query Mode** is always **In-Memory**.  
  
    > [!NOTE]  
    >  You cannot specify **Impersonation Settings** for models in DirectQuery mode.  
  
3.  In **Deployment Server**, specify the **Server** (name), **Edition**, **Database** (name), and **Cube Name** property settings, if different from the default settings, and then click **OK**.  
  
> [!NOTE]  
>  You can also specify the Default Deployment Server property setting so any new projects you create will automatically be deployed to the specified server. For more information, see [Configure default data modeling and deployment properties](../../analysis-services/tabular-models/configure-default-data-modeling-and-deployment-properties-ssas-tabular.md).  
  
##  <a name="bkmk_deploy_proc"></a> Deploy a tabular model  
  
#### To deploy a tabular model
  
-   In SSDT, on the **Build** menu, click **Deploy \<project name>**.  
  
     The **Deploy** dialog box will appear and indicate the status of the metadata deployment and the processing (unless Processing Option property is set to Do Not Process) of each table included in the model. After the deployment process is complete, use SSMS to connect to the Analysis Services instance and verify the new model database object has been created or use a client reporting application to connect to the deployed model.  
  
##  <a name="bkmk_deploy_status"></a> Deploy Status  
 The **Deploy** dialog box enables you to monitor the progress of a Deploy operation. A deploy operation can also be stopped.  
  
 **Status**  
 Indicates whether the Deploy operation was successful or not.  
  
 **Details**  
 Lists the metadata items that were deployed, the status for each metadata item, and provides a message of any issues.  
  
 **Stop Deploy**  
 Click to halt the Deploy operation. This option is useful if the Deploy operation is taking too long, or if there are too many errors.  
  
## See also  
 [Tabular model solution deployment](../../analysis-services/tabular-models/tabular-model-solution-deployment-ssas-tabular.md)   
 [Configure default data modeling and deployment properties](../../analysis-services/tabular-models/configure-default-data-modeling-and-deployment-properties-ssas-tabular.md)  
  
  
