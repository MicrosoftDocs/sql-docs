---
description: "SQL Server Examples: Model Deployment Packages (MDS)"
title: Model Deployment Package examples
ms.custom: ""
ms.date: "07/28/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
keywords: 
  - "master data services"
  - "sample"
ms.assetid: 9b31b7b6-319b-4840-b67d-eb383e7762b1
author: CordeliaGrey
ms.author: jiwang6
---
# SQL Server Examples: Model Deployment Packages (MDS)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Sample model packages with data are included when you install [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. The default location for these package files is \<drive>\Program Files\Microsoft SQL Server\130\Master Data Services\Samples\Packages.  
  
 For instructions on how to deploy the sample model packages, see [Deploying Sample Models and Data](../master-data-services/master-data-services-installation-and-configuration.md#deploySample). You deploy the sample model packages by using the [MDSModelDeploy tool](../master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md).  
  
> [!IMPORTANT]
>  **Sample Updates in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)]**  
> 
>  The sample packages support the following capabilities.  
> 
>  -   Show Many-to-Many Relationships.  
> 
>      For more information, see [M2M Relationship in Sample Model](../master-data-services/show-many-to-many-relationships-in-derived-hierarchies-master-data-services.md#M2MSample).  
> 
> -   Constrain Allowed Values for Domain-based Attributes.  
> 
>      For more information, see [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-domain-based-attribute-master-data-services.md).  
> -   Require Approval for Entity Changes.  
> 
>      For more information, see [Approval Required &#40;Master Data Services&#41;](../master-data-services/approval-required-master-data-services.md).  
> -   Use Not and Else Operators in Business Rules  
> 
>      For more information, see [Business Rule Examples](../master-data-services/business-rule-examples-master-data-services.md).  
> -   Implement Custom Index  
> 
>      For more information, see [Custom Index &#40;Master Data Services&#41;](../master-data-services/custom-index-master-data-services.md).  
 

 
 In Master Data Services, a package is an XML file that contains a deployable model structure, and optionally, data from the model. Use model packages to move copies of models from one MDS environment to another, or to create new models in your existing [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] environment.  
  
## See Also  
 [Deploy a Model Deployment Package by Using MDSModelDeploy](../master-data-services/deploy-a-model-deployment-package-by-using-mdsmodeldeploy.md)  
  
  
