---
title: "DimensionAttribute Data Type (ASSL) | Microsoft Docs"
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
  - "DimensionAttribute Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DimensionAttribute"
helpviewer_keywords: 
  - "DimensionAttribute data type"
ms.assetid: 94349a87-b284-49d1-ac72-888f0375ceb8
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AttributeHierarchyDisplayFolder](../../../2014/analysis-services/dev-guide/attributehierarchydisplayfolder-element-assl.md), [AttributeHierarchyEnabled](../../../2014/analysis-services/dev-guide/attributehierarchyenabled-element-assl.md), [AttributeHierarchyOptimizedState](../../../2014/analysis-services/dev-guide/attributehierarchyoptimizedstate-element-assl.md), [AttributeHierarchyOrdered](../../../2014/analysis-services/dev-guide/attributehierarchyordered-element-assl.md), [AttributeHierarchyVisible](../../../2014/analysis-services/dev-guide/attributehierarchyvisible-element-assl.md), [AttributeRelationships](../../../2014/analysis-services/dev-guide/attributerelationships-element-assl.md), [CustomRollupColumn](../../../2014/analysis-services/dev-guide/customrollupcolumn-element-assl.md), [CustomRollupPropertiesColumn](../../../2014/analysis-services/dev-guide/customrolluppropertiescolumn-element-assl.md), [DefaultMember](../../../2014/analysis-services/dev-guide/defaultmember-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DiscretizationBucketCount](../../../2014/analysis-services/dev-guide/discretizationbucketcount-element-assl.md), [DiscretizationMethod](../../../2014/analysis-services/dev-guide/discretizationmethod-element-assl.md), [EstimatedCount](../../../2014/analysis-services/dev-guide/estimatedcount-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [InstanceSelection](../../../2014/analysis-services/dev-guide/instanceselection-element-assl.md), [IsAggregatable](../../../2014/analysis-services/dev-guide/isaggregatable-element-assl.md), [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md), [KeyUniquenessGuarantee](../../../2014/analysis-services/dev-guide/keyuniquenessguarantee-element-assl.md), [MemberNamesUnique](../../../2014/analysis-services/dev-guide/membernamesunique-element-assl.md), [MembersWithData](../../../2014/analysis-services/dev-guide/memberswithdata-element-assl.md), [MembersWithDataCaption](../../../2014/analysis-services/dev-guide/memberswithdatacaption-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [NameColumn](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md), [NamingTemplate](../../../2014/analysis-services/dev-guide/namingtemplate-element-assl.md), [NamingTemplateTranslations](../../../2014/analysis-services/dev-guide/namingtemplatetranslations-element-assl.md), [OrderBy](../../../2014/analysis-services/dev-guide/orderby-element-assl.md), [OrderByAttributeID](../../../2014/analysis-services/dev-guide/orderbyattributeid-element-assl.md), [RootMemberIf](../../../2014/analysis-services/dev-guide/rootmemberif-element-assl.md), [SkippedLevelsColumn](../../../2014/analysis-services/dev-guide/skippedlevelscolumn-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-dimensionattribute-assl.md), [UnaryOperatorColumn](../../../2014/analysis-services/dev-guide/unaryoperatorcolumn-element-assl.md), [Usage](../../../2014/analysis-services/dev-guide/usage-element-dimensionattribute-assl.md), [ValueColumn](../../../2014/analysis-services/dev-guide/valuecolumn-element-assl.md)|  
|Derived elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md) ([Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md) collection of [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md))|  
  
## Remarks  
 The following restrictions apply when running the service in DeploymentMode configuration property values of 1 and 2 (SharePoint and Tabular modes, used to run PowerPivot and tabular model databases):  
  
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
  
 The following elements are unsupported when running the service in DeploymentMode configuration property values of 1 and 2 (SharePoint and Tabular modes, used to run PowerPivot and tabular model databases):  
  
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
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  