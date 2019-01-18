---
title: "Edit a Model Deployment Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 6b0fdb7d-83dd-4392-9011-4ae642c471f1
author: leolimsft
ms.author: lle
manager: craigg
---
# Edit a Model Deployment Package
  This topic describes how to deploy selected parts of a model in MDS, rather than an entire model. To do so, you edit an MDS model package using the Model Package Editor.  
  
 The Model Package Editor wizard enables you to select the specific entities, derived hierarchies, subscription views, and business rules in a model that you want to include in an MDS package, and then later deploy. You can leave out those parts of the model that you do not want to deploy. When you select an entity, all of the dependent objects in that entity are also automatically selected.  
  
 You use the Model Package Editor to select parts of a model in a package file that was created by either the MDSModelDeploy tool (which creates a package file that includes objects and data) or the Model Deployment wizard (which creates a file that includes only the model structure). After editing the model in the package, you use the MDSModelDeploy tool to deploy objects and data, or the Model Deployment wizard to deploy just the model structure.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   A model package must exist for you to edit. For more information, see [Deploying Models &#40;Master Data Services&#41;](../../2014/master-data-services/deploying-models-master-data-services.md) and [Create a Model Deployment Package by Using the Wizard](../../2014/master-data-services/create-a-model-deployment-package-by-using-the-wizard.md) or [Create a Model Deployment Package by Using MDSModelDeploy](../../2014/master-data-services/create-a-model-deployment-package-by-using-mdsmodeldeploy.md).  
  
### To edit a model deployment package  
  
1.  In Windows Explorer on the MDS server, move to *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services\Configuration.  
  
2.  Execute ModelPackageEditor.exe.  
  
3.  In the Model Package Editor wizard, click **Browse**, move to the folder containing your packages, select a package, and then click **Open**. Click **Next**.  
  
4.  Select those entities, derived hierarchies, subscriptions views, or business rules that you want to deploy. Deselect those that you do not want to deploy. Click **Next**.  
  
5.  Verify the list of selections to deploy. To change, click **Back** and repeat step 4.  
  
6.  Click **Browse**, move to the folder that you want to save the partial package in, and then enter the file name of the partial package (with a .pkg extension). Click **Save**.  
  
7.  Click **Finish**.  
  
## Next Steps  
  
-   [Deploy a Model Deployment Package by Using the Wizard](../../2014/master-data-services/deploy-a-model-deployment-package-by-using-the-wizard.md)  
  
-   [Deploy a Model Deployment Package by Using MDSModelDeploy](../../2014/master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md)  
  
  
