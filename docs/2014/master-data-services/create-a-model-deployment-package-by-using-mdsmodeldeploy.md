---
title: "Create a Model Deployment Package by Using MDSModelDeploy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: c2687e39-dc20-494f-a707-2aa29f4c329e
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Model Deployment Package by Using MDSModelDeploy
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], use the MDSModelDeploy tool to create a package. Depending on the commands you specify, the package can contain either:  
  
-   Model objects only.  
  
-   Model objects and data.  
  
 If you want to deploy a package that contains model objects only, you can use the model deployment wizard in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application instead. For more information, see [Create a Model Deployment Package by Using the Wizard](../../2014/master-data-services/create-a-model-deployment-package-by-using-the-wizard.md).  
> [!NOTE]  
> This version of the MDSModelDeploy tool cannot use more than gigabytes (GB) of memory. When you create or deploy large models by using **Model objects and data** option, you may experience "Out of Memory" or "Stream was too long" errors. To resolve this issue, use MDS staging to deploy the data; or upgrade to MDS 2016 or a later version, which includes the updated version of the MDSModelDeploy tool.
## Prerequisites  
 To perform this procedure:  
  
1.  The basic permissions required to run the MDSModelDeploy tool are the following:  
  
    -   The same Windows permission as the MDS Configuration Manager (administrator of Windows)  
  
    -   DBA permission on the MDS database.  
  
2.  The permissions required to create the package using the MDSModelDeploy tool are the following:  
  
    -   MDS model administrator permission on the data model.  
  
    -   MDS Integration Management function permission.  
  
3.  The permissions required to deploy a model using the MDSModelDeploy tool are the following:  
  
    -   MDS Explorer function permission  
  
    -   MDS Integration Management function permission  
  
    -   MDS System Administration function permission.  
  
4.  The permissions required to list models using the MDSModelDeploy tool are the following:  
  
    -   MDS Explorer function permission  
  
    -   MDS model administrator permission on the data model on order to see the model in the list.  
  
 A model must exist for you to create a package of. For more information, see [Create a Model &#40;Master Data Services&#41;](create-a-model-master-data-services.md).  
  
 For more information, see [Administrators &#40;Master Data Services&#41;](../../2014/master-data-services/administrators-master-data-services.md).  
  
### To create a model deployment package by using MDSModelDeploy  
  
1.  Open a command prompt.  
  
2.  Navigate to the location of MDSModelDeploy.exe.  
  
    -   If MDS was installed in the default location, the file is in *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services\Configuration.  
  
    -   If MDS was not installed in the default location, search the local computer for MDSModelDeploy.exe.  
  
3.  Optional. View options and help.  
  
    -   To display all available options, type `MDSModelDeploy` and press Enter.  
  
    -   To display help for an option, type the following, where *OptionName* is the name of the option: `MDSModelDeploy help OptionName`.  
  
4.  Optional. If you have multiple web applications, determine the name of the service you will deploy to by typing this command and pressing Enter:  
  
    ```  
    MDSModelDeploy listservices  
    ```  
  
     A list of values is returned, for example `MDS1, Default Web Site, MDS`. The first value in this list (in this case, `MDS1`) is needed to deploy the model.  
  
5.  To create a package that contains model objects and data, type the following, where *ModelName*, *VersionName*, *ServiceName*,  and *PackageName* are the names of the model, version, service, and of the .pkg output file respectively:  
  
    ```  
    MDSModelDeploy createpackage -model ModelName -version VersionName -service ServiceName -package PackageName -includedata  
    ```  
  
     If you do not want to include data, do not use the `-version` and `-includedata` switches.  
  
6.  Press Enter. When the package is successfully created, a message stating "MDSModelDeploy operation completed successfully" is displayed.  
  
## Next Steps  
  
-   [Deploy a Model Deployment Package by Using MDSModelDeploy](../../2014/master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md)  
  
## See Also  
 [Model Deployment Options &#40;Master Data Services&#41;](../../2014/master-data-services/model-deployment-options-master-data-services.md)   
 [Deploying Models &#40;Master Data Services&#41;](../../2014/master-data-services/deploying-models-master-data-services.md)  
  
  
