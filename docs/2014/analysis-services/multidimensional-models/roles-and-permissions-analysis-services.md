---
title: "Roles and Permissions (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "security [Analysis Services], about security"
  - "security [Analysis Services - multidimensional data], about security"
ms.assetid: bb885447-868b-4686-853c-8241f63d4370
author: minewiskan
ms.author: owend
manager: craigg
---
# Roles and Permissions (Analysis Services)
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides a role-based authorization model that grants access to operations, objects, and data. All users who access an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance or database must do so within the context of a role.  
  
 As an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] system administrator, you are in charge of granting membership to the **server administrator role** that conveys unrestricted access to operations on the server. This role has fixed permissions and cannot be customized. By default, members of the local Administrators group are automatically Analysis Services system administrators.  
  
 Non-administrative users who query or process data are granted access through **database roles**. Both system administrators and database administrators can create the roles that describe different levels of access within a given database, and then assign membership to every user who requires access. Each role has a customized set of permissions for accessing objects and operations within a particular database. You can assign permissions at these levels: database, interior objects such as cubes and dimensions (but not perspectives), and rows.  
  
 It is common practice to create roles and assign membership as separate operations. Often, the model designer adds roles during the design phase. This way, all role definitions are reflected in the project files that define the model. Role membership is typically rolled out later as the database moves into production, usually by database administrators who create scripts that can be developed, tested, and run as an independent operation.  
  
 All authorization is predicated on a valid Windows user identity. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses Windows authentication exclusively to authenticate user identities. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides no proprietary authentication method.See [Authentication methodologies supported by Analysis Services](../instances/authentication-methodologies-supported-by-analysis-services.md).  
  
> [!IMPORTANT]  
>  Permissions are additive for each Windows user or group, across all roles in the database. If one role denies a user or group permission to perform certain tasks or view certain data, but another role grants this permission to that user or group, the user or group will have permission to perform the task or view the data.  
  
## In this section  
  
-   [Authorizing access to objects and operations &#40;Analysis Services&#41;](authorizing-access-to-objects-and-operations-analysis-services.md)  
  
-   [Grant database permissions &#40;Analysis Services&#41;](grant-database-permissions-analysis-services.md)  
  
-   [Grant cube or model permissions &#40;Analysis Services&#41;](grant-cube-or-model-permissions-analysis-services.md)  
  
-   [Grant process permissions &#40;Analysis Services&#41;](grant-process-permissions-analysis-services.md)  
  
-   [Grant read definition permissions on object metadata &#40;Analysis Services&#41;](grant-read-definition-permissions-on-object-metadata-analysis-services.md)  
  
-   [Grant permissions on a data source object &#40;Analysis Services&#41;](grant-permissions-on-a-data-source-object-analysis-services.md)  
  
-   [Grant permissions on data mining structures and models &#40;Analysis Services&#41;](grant-permissions-on-data-mining-structures-and-models-analysis-services.md)  
  
-   [Grant permissions on a dimension &#40;Analysis Services&#41;](grant-permissions-on-a-dimension-analysis-services.md)  
  
-   [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md)  
  
-   [Grant custom access to cell data &#40;Analysis Services&#41;](grant-custom-access-to-cell-data-analysis-services.md)  
  
## See Also  
 [Create and Manage Roles &#40;SSAS Tabular&#41;](../tabular-models/roles-ssas-tabular.md)  
  
  
