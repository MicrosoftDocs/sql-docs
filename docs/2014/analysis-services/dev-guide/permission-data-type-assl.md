---
title: "Permission Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Permission Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Permission"
helpviewer_keywords: 
  - "Permission data type"
ms.assetid: 5f309544-59f8-4432-b1eb-b7c1a049f8df
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Permission Data Type (ASSL)
  Defines an abstract primitive data type that represents information about an individual permission.  
  
## Syntax  
  
```xml  
  
<Permission>  
   <Name>...</Name>  
   <ID>...</ID>  
   <CreatedTimestamp>...</CreateTimestamp>  
   <LastSchemaUpdate>...</LastSchemaUpdate>  
   <RoleID>...</RoleID>  
   <Description>...</Description>  
   <Process>...</Process>  
   <ReadDefinition>...</ReadDefinition>  
   <Read>...</Read>  
   <Write>...</Write>  
   <Annotations>...</Annotations>  
</Permission>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[CubePermission](../../../2014/analysis-services/dev-guide/cubepermission-element-assl.md), [DatabasePermission](../../../2014/analysis-services/dev-guide/databasepermission-element-assl.md), [DimensionPermission](../../../2014/analysis-services/dev-guide/dimensionpermission-data-type-assl.md), [MiningModelPermission](../../../2014/analysis-services/dev-guide/miningmodelpermission-element-assl.md), [MiningStructurePermission](../../../2014/analysis-services/dev-guide/miningstructurepermission-element-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Process](../../../2014/analysis-services/dev-guide/process-element-assl.md), [Read](../../../2014/analysis-services/dev-guide/read-element-assl.md), [ReadDefinition](../../../2014/analysis-services/dev-guide/readdefinition-element-assl.md), [RoleID](../../../2014/analysis-services/dev-guide/roleid-element-assl.md), [Write](../../../2014/analysis-services/dev-guide/write-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 `Permission` serves as the abstract base type for a number of derived permission types used in an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 This data type has the following validations under DeploymentMode value 2 (tabular server mode).  
  
-   *Process* attribute default value is set to `False`, except when the user has the **Refresh** permission. For users with the **Refresh** permission the *Process* attribute value is set to `True`.  
  
-   *ReadDefinition* attribute value is set to `None`; any other value generates an error.  
  
-   *Read* attribute value is set to `Allowed` for users with the **User** permission and to `None` when the users are assigned to the **Refresh** permission; if a user has both **User** and **Refresh** permissions, then the attribute is set to `Allowed`. For users with administrative privileges the attribute value is set to `Allowed`.  
  
-   *Write* attribute value is set to `None`; any other value generates an error.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Permission>.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/role-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  