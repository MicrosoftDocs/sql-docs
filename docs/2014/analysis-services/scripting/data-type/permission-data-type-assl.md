---
title: "Permission Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Derived data types|[CubePermission](../objects/cubepermission-element-assl.md), [DatabasePermission](../objects/databasepermission-element-assl.md), [DimensionPermission](permission-data-type-assl.md), [MiningModelPermission](../objects/miningmodelpermission-element-assl.md), [MiningStructurePermission](../objects/miningstructurepermission-element-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [Name](../properties/name-element-assl.md), [Process](../properties/process-element-assl.md), [Read](../properties/read-element-assl.md), [ReadDefinition](../properties/readdefinition-element-assl.md), [RoleID](../properties/roleid-element-assl.md), [Write](../properties/write-element-assl.md)|  
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
 [Role Element &#40;ASSL&#41;](../objects/role-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
