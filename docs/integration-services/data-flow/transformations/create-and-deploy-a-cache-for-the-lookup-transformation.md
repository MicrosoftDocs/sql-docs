---
description: "Create and Deploy a Cache for the Lookup Transformation"
title: "Create and Deploy a Cache for the Lookup Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "creating cache files for Lookup transformation"
  - "deploying cache files for Lookup transformation"
  - "Lookup transformation cache files"
ms.assetid: cedf5cad-2fac-42d0-ad91-9461e117d330
author: chugugrace
ms.author: chugu
---
# Create and Deploy a Cache for the Lookup Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  You can create and deploy a cache file (.caw) for the Lookup transformation. The reference dataset is stored in the cache file.  
  
 The Lookup transformation performs lookups by joining data in input columns from a connected data source with columns in the reference dataset.  
  
 You create a cache file by using a Cache connection manager and a Cache Transform transformation. For more information, see [Cache Connection Manager](../../connection-manager/cache-connection-manager.md) and [Cache Transform](../../../integration-services/data-flow/transformations/cache-transform.md).  
  
 To learn more about the Lookup transformation and cache files, see [Lookup Transformation](../../../integration-services/data-flow/transformations/lookup-transformation.md).  
  
### To create a cache file  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want, and then open the package.  
  
2.  On the **Control Flow** tab, add a Data Flow task.  
  
3.  On the **Data Flow** tab, add a Cache Transform transformation to the data flow, and then connect the transformation to a data source.  
  
     Configure the data source as needed.  
  
4.  Double-click the Cache Transform, and then in the **Cache Transformation Editor**, on the **Connection Manager** page, click **New** to create a new Cache connection manager.  
  
5.  In the **Cache Connection Manager Editor**, on the **General** tab, configure the Cache connection manager to save the cache by selecting the following options:  
  
    1.  Select **Use file cache**.  
  
    2.  For **File name**, type the file path.  
  
     The system creates the file when you run the package.  
  
    > [!NOTE]  
    >  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../../../integration-services/security/security-overview-integration-services.md#files).  
  
6.  Click the **Columns** tab, and then specify which columns are the index columns by using the **Index Position** option.  
  
     For non-index columns, the index position is 0. For index columns, the index position is a sequential, positive number.  
  
    > [!NOTE]  
    >  When the Lookup transformation is configured to use a Cache connection manager, only index columns in the reference dataset can be mapped to input columns. Also all index columns must be mapped.  
  
     For more information, see [Cache Connection Manager Editor](../../connection-manager/cache-connection-manager.md).  
  
7.  Configure the Cache Transform as needed.  
  
     For more information, see [Cache Transformation Editor &#40;Connection Manager Page&#41;](./cache-transform.md) and [Cache Transformation Editor &#40;Mappings Page&#41;](./cache-transform.md).  
  
8.  Run the package.  
  
### To deploy a cache file  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want, and then open the package.  
  
2.  Optionally, create a package configuration. For more information, see [Create Package Configurations](../../packages/legacy-package-deployment-ssis.md).  
  
3.  Add the cache file to the project by doing the following:  
  
    1.  In Solution Explorer, select the project you opened in step 1.  
  
    2.  On the **Project** menu, click **AddExisting Item**.  
  
    3.  Select the cache file, and then click **Add**.  
  
     The file appears in the **Miscellaneous** folder in Solution Explorer.  
  
4.  Configure the project to create a deployment utility, and then build the project. For more information, see [Create a Deployment Utility](../../packages/legacy-package-deployment-ssis.md).  
  
     A manifest file, \<*project name*>.SSISDeploymentManifest.xml, is created that lists the miscellaneous files in the project, the packages, and the package configurations.  
  
5.  Deploy the package to the file system. For more information, see [Deploy Packages by Using the Deployment Utility](../../packages/legacy-package-deployment-ssis.md).  
  
## See Also  
 [Create a Deployment Utility](../../packages/legacy-package-deployment-ssis.md)  
  
