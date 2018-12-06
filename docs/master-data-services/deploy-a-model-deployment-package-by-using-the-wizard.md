---
title: "Deploy a Model Deployment Package by Using the Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "deployment packages [Master Data Services], deploying"
  - "models [Master Data Services], deploying a package"
ms.assetid: 4f65dc60-0ff8-46e6-9988-5bc5b9603ad0
author: leolimsft
ms.author: lle
manager: craigg
---
# Deploy a Model Deployment Package by Using the Wizard

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Use the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] model deployment wizard to deploy packages that contain model objects only. If you need to deploy a package with data, see [Deploy a Model Deployment Package by Using MDSModelDeploy](../master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md).  
  
> [!IMPORTANT]  
>  Packages can be deployed to the edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] they were created in only. This means that packages created in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] cannot be deployed to [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area in the target [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] environment.  
  
-   A model deployment package must exist. For more information, see [Create a Model Deployment Package by Using the Wizard](../master-data-services/create-a-model-deployment-package-by-using-the-wizard.md).  
  
-   You must be an administrator in the environment where you are deploying the model. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To deploy a model deployment package of model objects only  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **System** and click **Deployment**.  
  
3.  On the **Model Deployment Wizard**, click **Deploy**.  
  
4.  Click **Browse**.  
  
5.  Find your deployment package (.pkg file) and click **Open**.  
  
6.  Click **Next**.  
  
7.  After the package is loaded, click **Next**.  
  
8.  If the model already exists, you can update it by selecting **Update the existing model**. To create a new model, select **Create a new model** and after you click **Next** you can type a name for the new model.  
  
9. Click **Finish** to exit the wizard.  
  
 **Notes:**  
  
-   If a subscription view in the package has the same name as a subscription view in an existing model, this warning is displayed: **Deployer subscription view renamed**. In addition, the view is created as *modelname.subscriptionviewname*. If this name is already in use, the subscription view is not created.  
  
-   The deployment process has four steps:  
  
    1.  The model objects are created.  
  
    2.  Subscription views are created.  
  
    3.  Business rules are created.  
  
-   When creating a new or cloned model, if the process fails during any step, the model is deleted.  
  
     When updating a model, if the process fails during any of the first three steps, it does not proceed beyond that step; however, changes that are already made are not rolled back.  
  
## Next Steps  
 File attributes, and user and group permissions are not included in model deployment packages. After you deploy a model, you must update these manually. For more information, see:  
  
-   [Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)  
  
## See Also  
 [Deploying Models &#40;Master Data Services&#41;](../master-data-services/deploying-models-master-data-services.md)  
  
  
