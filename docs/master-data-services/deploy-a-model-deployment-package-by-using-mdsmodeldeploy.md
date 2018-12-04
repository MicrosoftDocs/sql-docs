---
title: "Deploy a Model Deployment Package by Using MDSModelDeploy | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: fb2a4df4-5e0d-4b34-818f-383dbde1b15c
author: leolimsft
ms.author: lle
manager: craigg
---
# Deploy a Model Deployment Package by Using MDSModelDeploy

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], use the MDSModelDeploy tool to deploy a package that contains either:  
  
-   Model objects only.  
  
-   Model objects and data.  
  
 If you want to deploy a package that contains model objects only, you can use the model deployment wizard in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application instead. For more information, see [Deploy a Model Deployment Package by Using the Wizard](../master-data-services/deploy-a-model-deployment-package-by-using-the-wizard.md).  
  
> [!IMPORTANT]  
>  Packages can be deployed to the edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] they were created in only. This means that packages created in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] cannot be deployed to [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] or higher.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area in the target [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] environment.  
  
-   A model deployment package must exist. For more information, see  [Create a Model Deployment Package by Using MDSModelDeploy](../master-data-services/create-a-model-deployment-package-by-using-mdsmodeldeploy.md).  
  
-   You must be an administrator in the environment where you are deploying the model. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   If you are updating a model with data, the version you're deploying to cannot be **Locked** or **Committed**.  
  
### To deploy a model deployment package  
  
1.  Determine whether you are deploying a new model, a clone of a model, or updating a previously-cloned model. For more information, see [Model Deployment Options &#40;Master Data Services&#41;](../master-data-services/model-deployment-options-master-data-services.md).  
  
2.  Open an Administrator: Command Prompt and navigate to MDSModelDeploy.exe.  
  
    -   If MDS is installed at the default location, the tool is available at *drive*:\Program Files\Microsoft SQL Server\130\Master Data Services\Configuration  
  
    -   If MDS is not installed at the default location, search the local computer for MDSModelDeploy.exe.  
  
3.  Optional. View options and help.  
  
    -   To display all available options, type `MDSModelDeploy` and press Enter.  
  
    -   To display help for an option, type the following, where *OptionName* is the name of the option: `MDSModelDeploy help OptionName`.  
  
4.  Optional. If you have multiple web applications, determine the name of the service you will deploy to by typing this command and pressing Enter:  
  
    ```  
    MDSModelDeploy listservices  
    ```  
  
     A list of values is returned, for example `MDS1, Default Web Site, MDS`. The first value in this list (in this case, `MDS1`) is needed to deploy the model.  
  
5.  Depending on whether you are creating a model, cloning a model, or updating a model, at the command prompt, type the following and press Enter.  
  
    -   To create a new model:  
  
        ```  
        MDSModelDeploy deploynew -package PackageName -model ModelName -service ServiceName  
        ```  
  
    -   To create a clone of a model:  
  
        ```  
        MDSModelDeploy deployclone -package PackageName  
        ```  
  
    -   To update an existing model and its data:  
  
        ```  
        MDSModelDeploy deployupdate -package PackageName -version VersionName  
        ```  
  
    > [!IMPORTANT]  
    >  If you use the MDSModelDeploy tool to update an existing model and its data, and the package does not contain an entity, attribute, or member that exists in the destination model, MDSModelDeploy will not delete that entity, attribute, or member from the model.  
  
     Where *PackageName* is the name of the package (.pkg) file, *ModelName* is the name of the new model, *VersionName* is the name of the version, and *ServiceName* is the name of the service that you returned in the previous step. Ensure that the model and version names match the exact case-sensitive names.  
  
6.  When the package is successfully deployed, a message stating "MDSModelDeploy operation completed successfully" is displayed.  
  
 **Notes:**  
  
-   If a subscription view in the package has the same name as a subscription view in an existing model, this warning is displayed: **Deployer subscription view renamed** and the view is created as *modelname.subscriptionviewname*. If this name is already in use, the subscription view is not created.  
  
-   The deployment process has four steps:  
  
    1.  The model objects are created.  
  
    2.  Business rules are created.  
  
    3.  Subscription views are created.  
  
    4.  Master data is populated.  
  
-   When creating a new or cloned model, if the process fails during any step, the model is deleted.  
  
     When updating a model, if the process fails during the first three steps, it does not proceed; however, changes that are already made are not rolled back. If the process fails in step 4, members that can be updated are updated.  
  
## Next Steps  
 File attributes, and user and group permissions are not included in model deployment packages. After you deploy a model, you must update these manually. For more information, see:  
  
-   [Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)  
  
## See Also  
 [Deploying Models &#40;Master Data Services&#41;](../master-data-services/deploying-models-master-data-services.md)  
  
  
