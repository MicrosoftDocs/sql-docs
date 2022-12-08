---
description: "Model Deployment Options (Master Data Services)"
title: Model Deployment Options
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: cf1b17b4-47d5-4eba-83f9-fb0555806867
author: CordeliaGrey
ms.author: jiwang6
---
# Model Deployment Options (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], when you deploy a model package file, you must decide whether to deploy a new or cloned model, or to update a model that was previously cloned.  
  
## Workflows  
 When working with model packages, there are two primary workflows.  
  
-   Create a package of a model in a test environment and deploy a clone of the model to a production environment. Over time, deploy updates from the test environment to the production environment.  
  
-   Create a package of a model and deploy it as a new model to the same environment. In this case, you must give the model a new name.  
  
## Options  
 In the MDS database, each model object has a unique identifier (ID). These IDs are included in model deployment packages. When you deploy the package, you must choose what to do with these IDs.  
  
 The following table should help you determine which choice to make when deploying a model by using either the System Administration model deployment wizard or the MDSModelDeploy tool.  
  
|Option|Description|Notes|  
|------------|-----------------|-----------|  
|New|Create a new model with a unique name. New identifiers are created for all model objects.|If you create a new model with new identifiers, you cannot use model deployment tools to apply updates to the model later. When using the wizard in the web application to deploy a model package, you have the option to create a new model only if a model with the same name or ID already exists.|  
|Clone|Create a new model that is an exact clone of the model in the package. This works only if the model does not exist (by name or identifier) in the target environment. Use "clone" when you want to have the same model in multiple environments and update the cloned model over time.|This is the default behavior of the wizard in the web application. If a model with the same name or ID already exists, you are prompted to create a new model instead.|  
|Update|Update an existing model with the model in the package. The identifiers must be the same in both models. This is used to update a model that you previously cloned.|You can only update models that were previously cloned. (The names and IDs must match.)|  
  
## See Also  
 [Deploy a Model Deployment Package by Using MDSModelDeploy](../master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md)   
 [Deploy a Model Deployment Package by Using the Wizard](../master-data-services/deploy-a-model-deployment-package-by-using-the-wizard.md)   
 [Deploying Models &#40;Master Data Services&#41;](../master-data-services/deploying-models-master-data-services.md)  
  
  
