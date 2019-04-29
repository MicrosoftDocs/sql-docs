---
title: "New Database Dialog Box (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.newdatabase.f1"
ms.assetid: ddc7804b-acb0-4ae4-a88f-e8cdf704c341
author: minewiskan
ms.author: owend
manager: craigg
---
# New Database Dialog Box (Analysis Services)
  Use the **New Database** dialog box in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to create a new, empty [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database. You can display the **New Database** dialog box by right-clicking the **Databases** folder of an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance in **Object Explorer** and selecting **New Database**.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Database name**|Type the name of the new [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.|  
|**Use a specific user name and password**|Select to have the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database use the security credentials of a specified user account. The specified credentials will be used for processing, ROLAP queries, out-of-line bindings, local cubes, mining models, remote partitions, linked objects, and synchronization from target to source. For DMX OPENQUERY statements however, the credentials of the current user will be used.|  
|**User name**|Type the domain and name of the user account to be used by the selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database. Use the following format:<br /><br /> *\<Domain name>* **\\** *\<User account name>*<br /><br /> Note: This option is enabled only if **Use a specific user name and password** is selected.|  
|**Password**|Type the password for the user account specified in **User name**.|  
|**Use the service account**|Select to have the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database use the security credentials that are associated with the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] service that manages the database. The service account credentials will be used for processing, ROLAP queries, remote partitions, linked objects, and synchronization from target to source. For DMX OPENQUERY statements, local cubes, and mining models the credentials of the current user will be used. This option is not supported for out-of-line bindings.|  
|**Use the credentials of the current user**|Select to have the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database use the security credentials of the current user for out-of-line bindings, DMX OPENQUERY statements, local cubes, and mining models. This option is not supported for processing, ROLAP queries, remote partitions, linked objects, and synchronization from target to source.|  
|**Default**|Select to use the credentials of the default user account for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. This option uses the default setting for the database for processing objects, synchronizing servers, and executing **Open Query** data mining statements. For more information about specifying the default settings at the database level, see [Set Multidimensional Database Properties &#40;Analysis Services&#41;](multidimensional-models/set-multidimensional-database-properties-analysis-services.md).<br /><br /> By default the `DataSourceImpersonationInfo` database property is set to **Use the service account**. Regardless of the `DataSourceImpersonationInfo` property value, the credentials of the current user will be used for out-of-line bindings, ROLAP queries, local cubes, and data mining models.|  
|**Description**|Type the description for the new [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Multidimensional Model Databases &#40;SSAS&#41;](multidimensional-models/multidimensional-model-databases-ssas.md)  
  
  
