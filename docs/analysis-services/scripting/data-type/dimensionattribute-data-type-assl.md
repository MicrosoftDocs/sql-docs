---
title: "DimensionAttribute Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DimensionAttribute Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DimensionAttribute"
helpviewer_keywords: 
  - "DimensionAttribute data type"
ms.assetid: 94349a87-b284-49d1-ac72-888f0375ceb8
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DimensionAttribute Data Type (ASSL)
  Defines a primitive data type that represents an attribute in a dimension.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   <Name>...</Name>  
   <ID>...</ID>  
   <Description>...</Description>  
   <Type>...</Type>  
   <Usage>...</Usage>  
   <Source>...</Source>  
   <EstimatedCount>...</EstimatedCount>  
   <KeyColumns>...</KeyColumns>  
   <NameColumn>...</NameColumn>  
   <ValueColumn>...</ValueColumn>  
   <Translations>...</Translations>  
   <AttributeRelationships>...</AttributeRelationships>  
   <DiscretizationMethod>...</DiscretizationMethod>  
   <DiscretizationBucketCount>...</DiscretizationBucketCount>  
   <RootMemberIf>...</RootMemberIf>  
   <OrderBy>...</OrderBy>  
   <DefaultMember>...</DefaultMember>  
   <OrderByAttributeID>...</OrderByAttributeID>  
   <SkippedLevelsColumn>...</SkippedLevelsColumn>  
   <NamingTemplate>...</NamingTemplate>  
   <MembersWithData>...</MembersWithData>  
   <MembersWithDataCaption>...</MembersWithDataCaption>  
   <NamingTemplateTranslations>...</NamingTemplateTranslations>  
   <CustomRollupColumn>...</CustomRollupColumn>  
   <CustomRollupPropertiesColumn>...</CustomRollupPropertiesColumn>  
   <UnaryOperatorColumn>...</UnaryOperatorColumn>  
   <AttributeHierarchyOrdered>...</AttributeHierarchyOrdered>  
   <MemberNamesUnique>...</MemberNamesUnique>  
   <IsAggregatable>...</IsAggregatable>  
   <AttributeHierarchyEnabled>...</AttributeHierarchyEnabled>  
   <AttributeHierarchyOptimizedState>...</AttributeHierarchyOptimizedState>  
   <AttributeHierarchyVisible>...</AttributeHierarchyVisible>  
   <AttributeHierarchyDisplayFolder>...</AttributeHierarchyDisplayFolder>  
   <KeyUniquenessGuarantee>...<KeyUniquenessGuarantee>  
   <InstanceSelection>...</InstanceSelection>  
   <Annotations>...</Annotations>  
</DimensionAttribute>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [AttributeHierarchyDisplayFolder](../../../analysis-services/scripting/properties/attributehierarchydisplayfolder-element-assl.md), [AttributeHierarchyEnabled](../../../analysis-services/scripting/properties/attributehierarchyenabled-element-assl.md), [AttributeHierarchyOptimizedState](../../../analysis-services/scripting/properties/attributehierarchyoptimizedstate-element-assl.md), [AttributeHierarchyOrdered](../../../analysis-services/scripting/properties/attributehierarchyordered-element-assl.md), [AttributeHierarchyVisible](../../../analysis-services/scripting/properties/attributehierarchyvisible-element-assl.md), [AttributeRelationships](../../../analysis-services/scripting/collections/attributerelationships-element-assl.md), [CustomRollupColumn](../../../analysis-services/scripting/objects/customrollupcolumn-element-assl.md), [CustomRollupPropertiesColumn](../../../analysis-services/scripting/objects/customrolluppropertiescolumn-element-assl.md), [DefaultMember](../../../analysis-services/scripting/properties/defaultmember-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [DiscretizationBucketCount](../../../analysis-services/scripting/properties/discretizationbucketcount-element-assl.md), [DiscretizationMethod](../../../analysis-services/scripting/properties/discretizationmethod-element-assl.md), [EstimatedCount](../../../analysis-services/scripting/properties/estimatedcount-element-assl.md), [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [InstanceSelection](../../../analysis-services/scripting/properties/instanceselection-element-assl.md), [IsAggregatable](../../../analysis-services/scripting/properties/isaggregatable-element-assl.md), [KeyColumns](../../../analysis-services/scripting/collections/keycolumns-element-assl.md), [KeyUniquenessGuarantee](../../../analysis-services/scripting/properties/keyuniquenessguarantee-element-assl.md), [MemberNamesUnique](../../../analysis-services/scripting/properties/membernamesunique-element-assl.md), [MembersWithData](../../../analysis-services/scripting/properties/memberswithdata-element-assl.md), [MembersWithDataCaption](../../../analysis-services/scripting/properties/memberswithdatacaption-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [NameColumn](../../../analysis-services/scripting/objects/namecolumn-element-assl.md), [NamingTemplate](../../../analysis-services/scripting/properties/namingtemplate-element-assl.md), [NamingTemplateTranslations](../../../analysis-services/scripting/collections/namingtemplatetranslations-element-assl.md), [OrderBy](../../../analysis-services/scripting/properties/orderby-element-assl.md), [OrderByAttributeID](../../../analysis-services/scripting/properties/orderbyattributeid-element-assl.md), [RootMemberIf](../../../analysis-services/scripting/properties/rootmemberif-element-assl.md), [SkippedLevelsColumn](../../../analysis-services/scripting/objects/skippedlevelscolumn-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md), [Type](../../../analysis-services/scripting/properties/type-element-dimensionattribute-assl.md), [UnaryOperatorColumn](../../../analysis-services/scripting/objects/unaryoperatorcolumn-element-assl.md), [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md), [ValueColumn](../../../analysis-services/scripting/objects/valuecolumn-element-assl.md)|  
|Derived elements|[Attribute](../../../analysis-services/scripting/objects/attribute-element-assl.md) ([Attributes](../../../analysis-services/scripting/collections/attributes-element-assl.md) collection of [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md))|  
  
## Remarks  
 The following restrictions apply when running the service in DeploymentMode configuration property values of 1 and 2 (SharePoint and Tabular modes, used to run [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] and tabular model databases):  
  
-   Usage element only accepts KEY or REGULAR values.  
  
-   IsAggregatable element cannot be false.  
  
-   OrderBy element only accepts KEY or PROPERTYKEY values.  
  
-   A calculated column cannot be a primary key in the table.  
  
-   A calculated column cannot contain DataSize in the binding.  
  
-   For each calculated column a syntax validation is performed before saving the attribute definition.  
  
-   For AttributeRelationships, RelationshipType must be set to the value, Flexible.  
  
-   The attribute ‘RowNumber’, identified by ‘RowNumber’, must have the type integer.  
  
-   Only the attribute ‘RowNumber’ can have KeyBinding of type RowNumberBinding.  
  
-   All attributes other than ‘RowNumber’ must have a Cardinality of one in relation to the key, unless the attribute itself is the key.  
  
-   When the column specified by OrderBy is also the PropertyKey, the OrderByAttributeId cannot point to the row number column.  
  
-   Attributes used as keys should be related to all other attributes; other types of relationships are not supported.  
  
-   The NullProcessing element cannot be set to ‘UnknownMember’.  
  
-   Bindings cannot be set to ‘Value’.  
  
 The following elements are unsupported when running the service in DeploymentMode configuration property values of 1 and 2 (SharePoint and Tabular modes, used to run [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] and tabular model databases):  
  
-   AttributeHierarchyOptimizedState  
  
-   CustomRollupColumn  
  
-   CustomRollupPropertiesColumn  
  
-   DefaultMember  
  
-   DiscretizationBucketCount  
  
-   DiscretizationMethod  
  
-   SkippedLevelsColumn  
  
-   Source  
  
-   UnaryOperatorColumn  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  