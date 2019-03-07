---
title: "Impersonation Information | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 42319d60-ccd0-46b8-af0b-f0968c390d8a
author: minewiskan
ms.author: owend
manager: craigg
---
# Impersonation Information
  Use the **Impersonation Information** page to specify the credentials that Analysis Services will use to connect to the data source.  
  
## Options  
 **Use a specific Windows user name and password**  
 Select this option to have the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object use the security credentials of a specified Windows user account. The specified credentials will be used for processing, ROLAP queries, out-of-line bindings, local cubes, mining models, remote partitions, linked objects, and synchronization from target to source. However, for Data Mining Extensions (DMX) OPENQUERY statements, this option is ignored and the credentials of the current user will be used.  
  
 **User name**  
 Type the domain and name of the user account to be used by the selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object. Use the following format:  
  
 *\<Domain name>* **\\** *\<User account name>*  
  
 This option is enabled only if **Use a specific name and password** is selected.  
  
 **Password**  
 Type the password of the user account to be used by the selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object.  
  
 This option is enabled only if **Use a specific name and password** is selected.  
  
 **Use the service account**  
 Select this option to have the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object use the security credentials associated with the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] service that manages the object. The service account credentials will be used for processing, ROLAP queries, remote partitions, linked objects, and synchronization from target to source. However, for Data Mining Extensions (DMX) OPENQUERY statements, local cubes, and mining models, the credentials of the current user will be used. This option is not supported for out-of-line bindings.  
  
 **Use the credentials of the current user**  
 Select this option to have the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object use the security credentials of the current user for out-of-line bindings, DMX OPENQUERY, local cubes, and mining models. This option is not supported for processing, ROLAP queries, remote partitions, linked objects, and synchronization from target to source.  
  
 **Inherit**  
 Select this option to use the impersonation behavior, defined at the database level, which has been set by the server administrator using the `DataSourceImpersonation` database property.  
  
## See Also  
 [Data Sources in Multidimensional Models](multidimensional-models/data-sources-in-multidimensional-models.md)   
 [Data Sources Supported &#40;SSAS Multidimensional&#41;](multidimensional-models/supported-data-sources-ssas-multidimensional.md)  
  
  
