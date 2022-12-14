---
title: Create a Model Deployment Package (MDSModelDeploy)
description: Use MDSModelDeploy to create a deployment package in Master Data Services. A package can contain either model objects only or model objects and data.
ms.custom: "seo-lt-2019"
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: c2687e39-dc20-494f-a707-2aa29f4c329e
author: CordeliaGrey
ms.author: jiwang6
---
# Create a Model Deployment Package by Using MDSModelDeploy

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], use the MDSModelDeploy tool to create a package. Depending on the commands you specify, the package can contain either:  
  
-   Model objects only.  
  
-   Model objects and data.  
  
 If you want to deploy a package that contains model objects only, you can use the model deployment wizard in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application instead. For more information, see [Create a Model Deployment Package by Using the Wizard](../master-data-services/create-a-model-deployment-package-by-using-the-wizard.md).  
  
## Prerequisites  
 To perform this procedure:  
  
1.  The basic permissions required to run the MDSModelDeploy tool are the following:  
  
    -   The same Windows permission as the MDS Configuration Manager (administrator of Windows)  
  
    -   DBA permission on the MDS database.  
  
2.  The permissions required to create the package using the MDSModelDeploy tool are the following:  
  
    -   MDS model administrator permission on the data model.  
  
    -   MDS ImportExport functional area permission.  
  
3.  The permissions required to deploy a model using the MDSModelDeploy tool are the following:  
  
    -   MDS Explorer function permission  
  
    -   MDS System Administration function permission.  
  
4.  The permissions required to list models using the MDSModelDeploy tool are the following:  
  
    -   MDS Explorer function permission  
  
    -   MDS model administrator permission on the data model on order to see the model in the list.  
  
 A model must exist for you to create a package of. For more information, see [Create a Model &#40;Master Data Services&#41;](../master-data-services/create-a-model-master-data-services.md).  
  
 For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To create a model deployment package by using MDSModelDeploy  
  
1.  Open an Administrator: Command Prompt.  
  
2.  Navigate to the location of MDSModelDeploy.exe.  
  
    -   If MDS was installed in the default location, the file is in *drive*:\Program Files\Microsoft SQL Server\130\Master Data Services\Configuration.  
  
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
  
-   [Deploy a Model Deployment Package by Using MDSModelDeploy](../master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md)  
  
## See Also  
 [Model Deployment Options &#40;Master Data Services&#41;](../master-data-services/model-deployment-options-master-data-services.md)   
 [Deploying Models &#40;Master Data Services&#41;](../master-data-services/deploying-models-master-data-services.md)  
  
  
