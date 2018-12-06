---
title: "Deploying Models (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "deployment packages [Master Data Services], about deployment packages"
  - "deployment packages [Master Data Services]"
ms.assetid: 30085c08-034f-4efe-80fe-408f9091ff5c
author: leolimsft
ms.author: lle
manager: craigg
---
# Deploying Models (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a package is an XML file that contains a deployable model structure, and optionally, data from the model. Use model packages to move copies of models from one MDS environment to another, or to create new models in your existing MDS environment.  
  
> [!IMPORTANT]  
>  Packages can be deployed to the edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] they were created in only. This means that packages created in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] cannot be deployed to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] or higher.  
  
## Tools for Deploying Models  
 To work with model packages, you can use one of three tools, depending on your needs.  
  
-   **MDSModelDeploy tool**: To create and deploy model objects and data, use the MDSModelDeploy.exe tool. If you selected the default path when installing MDS, this tool is located on *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services\Configuration.  
  
-   **Model Deployment wizard**: To create and deploy packages of the model structure only, use the wizard in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application. You cannot use this wizard to deploy data.  
  
-   **Model Package Editor**: To edit a model package, use the ModelPackageEditor.exe that launches the Model Package Editor wizard. You use this wizard to edit a package that was created by the MDSModelDeploy tool or the Model Deployment wizard. If you selected the default path when installing MDS, this tool is located on *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services\Configuration.  
  
> [!IMPORTANT]  
>  You can use the MDSDeployModel to create a new model, create a clone of a model, or update an existing model and its data. If you use the MDSModelDeploy tool to update an existing model and its data, and the package does not contain an entity, attribute, or member that exists in the destination model, MDSModelDeploy will not delete that entity, attribute, or member from the model.  
  
## What Packages Contain  
 A model package is an XML file that is saved with the .pkg extension. When you create a deployment package, you can decide whether or not to include data. If you decide to include data, you must select a version of the data to include.  
  
 All model objects are included in a package. These objects are:  
  
-   Entities  
  
-   Attributes  
  
-   Attribute groups  
  
-   Hierarchies  
  
-   Collections  
  
-   Business rules  
  
-   Version flags  
  
-   Subscription views  
  
 User-defined metadata, file attributes, and user and group permissions are not included. After you deploy a model, you must update these manually.  
  
## Sample Packages  
 Sample package files are included when you install [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. These package files are in the Master Data Services\Samples\Packages directory where you installed [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. When you deploy these sample packages by using the MDSModelDeploy tool, sample models are created and populated with data.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a new deployment package of model objects and/or data by using the MDSModelDeploy tool.|[Create a Model Deployment Package by Using MDSModelDeploy](../../2014/master-data-services/create-a-model-deployment-package-by-using-mdsmodeldeploy.md)|  
|Create a new deployment package of model objects only by using the wizard.|[Create a Model Deployment Package by Using the Wizard](../../2014/master-data-services/create-a-model-deployment-package-by-using-the-wizard.md)|  
|Deploy a package of model objects and data by using the MDSModelDeploy tool.|[Deploy a Model Deployment Package by Using MDSModelDeploy](../../2014/master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md)|  
|Deploy a package of model objects only by using the wizard.|[Deploy a Model Deployment Package by Using the Wizard](../../2014/master-data-services/deploy-a-model-deployment-package-by-using-the-wizard.md)|  
|Edit a model deployment package to deploy selected parts of a model, rather than the entire model.|[Edit a Model Deployment Package](../../2014/master-data-services/edit-a-model-deployment-package.md)|  
  
## Related Content  
  
-   [Model Deployment Options &#40;Master Data Services&#41;](model-deployment-options-master-data-services.md)  
  
  
