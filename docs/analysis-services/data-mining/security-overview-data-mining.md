---
title: "Security Overview (Data Mining) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Security Overview (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The process of securing [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] occurs at multiple levels. You must secure each instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and its data sources to make sure that only authorized users have read or read/write permissions to selected dimensions, mining models, and data sources. You must also secure underlying data sources to prevent unauthorized users from maliciously compromising sensitive business information. The process of securing an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is described in the following topics.  
  
##  <a name="bkmk_Architecture"></a> Security Architecture  
 See the following resources to learn about the basic security architecture of an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], including how [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication to authenticate user access.  
  
-   [Security Roles  &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/olap-logical/security-roles-analysis-services-multidimensional-data.md)  
  
-   [Security Properties](../../analysis-services/server-properties/security-properties.md)  
  
-   [Configure Service Accounts &#40;Analysis Services&#41;](../../analysis-services/instances/configure-service-accounts-analysis-services.md)  
  
-   [Authorizing access to objects and operations &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/authorizing-access-to-objects-and-operations-analysis-services.md)  
  
##  <a name="bkmk_Logon"></a> Configuring the Logon Account for Analysis Services  
 You must select an appropriate logon account for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and specify the permissions for this account. You must make sure that the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] logon account has only those permissions that are necessary to perform necessary tasks, including appropriate permissions to the underlying data sources.  
  
 For data mining, you need a different set of permissions to build and process models than you need to view or query the models. Making predictions against a model is a kind of query and does not require administrative permissions.  
  
##  <a name="bkmk_Instance"></a> Securing an Analysis Services Instance  
 Next you must secure the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] computer, the Windows operating system on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] computer, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] itself, and the data sources that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses.  
  
##  <a name="bkmk_Access"></a> Configuring Access to Analysis Services  
 When you set up and define authorized users for an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you need to determine which users should also have permission to administer specific database objects, which users can view the definition of objects or browse the models, and which users are able to access data sources directly.  
  
##  <a name="bkmk_DMspecial"></a> Special Considerations for Data Mining  
 To enable an analyst or developer to create and test data mining models, you must give that analyst or developer administrative permissions on the database where the mining models are stored. As a consequence, the data mining analyst or developer can potentially create or delete other objects that are not related to data mining, including data mining objects that were created and are being used by other analysts or developers, or OLAP objects that are not included in the data mining solution.  
  
 Accordingly, when you create a solution for data mining, you must balance the needs of the analyst or developer to develop, test and tune models, against the needs of other users, and take measures to protect existing database objects. One possible approach is to create a separate database dedicated to data mining, or to create separate databases for each analyst.  
  
 Although the creation of models requires the highest level of permissions, you can control the user's access to data mining models for other operations, such as processing, browsing, or querying, by using role-based security. When you create a role, you set permissions that are specific to data mining objects. Any user who is a member of a role automatically has all permissions associated with that role.  
  
 Additionally, data mining models often reference data sources that contain sensitive information. If the mining structure and mining model has been configured to allow users to drill through from the model to the data in the structure, you must take precautions to mask sensitive information, or to limit the users who have access to the underlying data.  
  
 If you use Integration Services packages to clean data, to update mining models, or to make predictions, you must ensure that the Integration Services service has the appropriate permissions on the database where the model is stored, and appropriate permissions on the source data.  
  
## See Also  
 [Roles and Permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/roles-and-permissions-analysis-services.md)  
  
  
