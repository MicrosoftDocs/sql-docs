---
title: "Objects (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "ASSL, objects"
  - "objects [Analysis Services Scripting Language]"
  - "Analysis Services Scripting Language, objects"
ms.assetid: 0f672b93-c317-47e5-b44d-ecea9b587c98
caps.latest.revision: 21
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Objects (ASSL)
  This reference section contains syntax and usage information for each element that acts as an object in the Analysis Services Scripting Language (ASSL) schema.  
  
 Although the ASSL schema contains only XML elements, from a developer's point of view, the elements described in this section correspond to objects, such as **Database**, **Cube**, and **Dimension** objects, in the hierarchy of objects contained by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 Objects are never leaf-level elements in the ASSL schema, but have child elements and elements that correspond to object properties.  
  
 In a few cases, a leaf-level element in the schema that may appear to be a property is classified as an object because the element's type is an object type. For example, the **Source** of a **Dimension** object is of type **DimensionBinding**.  
  
## In This Section  
  
|Element|Description|  
|-------------|-----------------|  
|[Account Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/account-element-assl.md)|Contains details about an account type within a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.|  
|[Action Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/action-element-assl.md)|Contains information about an action available in a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element or a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.|  
|[Aggregation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/aggregation-element-assl.md)|Defines a single aggregation for a [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md) element.|  
|[AggregationDesign Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md)|Defines a set of aggregation definitions that can be shared across multiple partitions in a database.|  
|[AggregationInstance Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/aggregationinstance-element-assl.md)|Defines an aggregation instance for a partition.|  
|[AlgorithmParameter Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md)|Defines a parameter for the algorithm used by a [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.|  
|[AllMemberTranslation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/allmembertranslation-element-assl.md)|Contains a translation for the caption of the All member of a [Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md) element.|  
|[Annotation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/annotation-element-assl.md)|Contains elements that are used to extend the ASSL schema.|  
|[Assembly Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/assembly-element-assl.md)|Represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly or a COM dynamic link library (DLL) associated with a [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element or a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.|  
|[Attribute Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/attribute-element-assl.md)|Contains the description of an attribute.|  
|[AttributeAllMemberTranslation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/attributeallmembertranslation-element-assl.md)|Contains a translation for the caption of the All member of a [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md) element.|  
|[AttributePermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/attributepermission-element-assl.md)|Defines the permissions that members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have on the attributes of an individual dimension in a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.|  
|[AttributeRelationship Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/attributerelationship-element-assl.md)|Provides details about the relationship between two attributes.|  
|[Block Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/block-element-assl.md)|Contains all or a portion of the binary contents of a [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) element.|  
|[Calculation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/calculation-element-assl.md)|Asssociates a calculation with a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.|  
|[CalculationProperty Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/calculationproperty-element-assl.md)|Contains a collection of user interface properties for a calculation used in an [MdxScript](../../../analysis-services/scripting/objects/mdxscript-element-assl.md) element.|  
|[CaptionColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/captioncolumn-element-assl.md)|Defines the column that provides the caption for the attribute.|  
|[CellPermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/cellpermission-element-assl.md)|Describes the permissions that members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have on individual cells within a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.|  
|[Column Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/column-element-assl.md)|Describes a column in the collection of columns associated with the parent element.|  
|[Command Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/command-element-assl.md)|Defines a command that is available for use within the context of the parent element of the Commands collection.|  
|[Cube Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/cube-element-assl.md)|Defines a regular, virtual, or linked cube in an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.|  
|[CubePermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/cubepermission-element-assl.md)|Defines the permissions of the members of a particular [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element in a specific [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.|  
|[CustomRollupColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/customrollupcolumn-element-assl.md)|Defines the details of the column that provide a custom rollup formula.|  
|[CustomRollupPropertiesColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/customrolluppropertiescolumn-element-assl.md)|Defines the details of a column that provide the properties of a custom rollup formula.|  
|[Data Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/data-element-assl.md)|Contains (in the collection of child **Block** elements) the binary contents of a [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) element.|  
|[Database Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/database-element-assl.md)|Defines an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database.|  
|[DatabasePermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/databasepermission-element-assl.md)|Defines the default permissions in a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element for a specific [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element.|  
|[DataSource Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/datasource-element-assl.md)|Defines a data source in a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.|  
|[DataSourcePermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/datasourcepermission-element-assl.md)|Defines the default permissions in a [DataSource](../../../analysis-services/scripting/data-type/datasource-data-type-assl.md) data type for a specific [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element.|  
|[DataSourceView Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/datasourceview-element-assl.md)|Defines a data source view used by a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.|  
|[Dimension Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/dimension-element-assl.md)|Defines a dimension.|  
|[DimensionPermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/dimensionpermission-element-assl.md)|Defines the permissions that belong to a particular [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element for a specific database dimension or cube dimension.|  
|[ErrorConfiguration Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md)|Specifies settings for handling errors that can occur when the parent element is processed.|  
|[Event Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/event-element-assl.md)|Defines an event to be captured as part of a [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md) element.|  
|[File Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/file-element-assl.md)|Defines one of the files that compose a [ClrAssembly](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md) element.|  
|[ForeignKeyColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/foreignkeycolumn-element-assl.md)|Identifies the join to a parent table for a relational data source.|  
|[Group Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/group-element-assl.md)|Defines a group of members bound to an attribute.|  
|[Hierarchy Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/hierarchy-element-assl.md)|Defines a hierarchy in a dimension.|  
|[IncrementalProcessingNotification Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/incrementalprocessingnotification-element-assl.md)|Contains information for the [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md) element about a query to execute to determine the progress of incremental processing.|  
|[KeyColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/keycolumn-element-assl.md)|Contains the definition of a column that is, or is part of, the key for an attribute.|  
|[Kpi Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/kpi-element-assl.md)|Defines a key performance indicator (KPI) within a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element or a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.|  
|[Level Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/level-element-assl.md)|Defines a level in a [Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md) element.|  
|[MdxScript Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/mdxscript-element-assl.md)|Contains information about a Multidimensional Expressions (MDX) script that is associated with a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.|  
|[Measure Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/measure-element-assl.md)|Defines a measure.|  
|[MeasureGroup Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/measuregroup-element-assl.md)|Defines a set of measures at the same level of granularity.|  
|[Member Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/member-element-assl.md)|Contains the name of a member of a [Group](../../../analysis-services/scripting/objects/group-element-assl.md) element or of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element.|  
|[MiningModel Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningmodel-element-assl.md)|Defines a single data mining model.|  
|[MiningModelPermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningmodelpermission-element-assl.md)|Defines the permissions members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have on an individual [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.|  
|[MiningStructure Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningstructure-element-assl.md)|Defines the structure for a set of mining models.|  
|[MiningStructurePermission Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningstructurepermission-element-assl.md)|Defines the permissions that members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have on an individual [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element.|  
|[ModelingFlag Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/modelingflag-element-assl.md)|Contains a modeling flag for a column in a mining structure or a mining model.|  
|[NameColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/namecolumn-element-assl.md)|Identifies the column that provides the name of the parent element.|  
|[NamingTemplateTranslation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/namingtemplatetranslation-element-assl.md)|Provides a localized translation of the **NamingTemplate** element for a parent [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md) data type.|  
|[Partition Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/partition-element-assl.md)|Defines a partition of a [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md) element or a partition binding in an out-of-line [MeasureGroupBinding](../../../analysis-services/scripting/data-type/measuregroupbinding-data-type-out-of-line-assl.md)element.|  
|[Perspective Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/perspective-element-assl.md)|Defines details for a perspective of a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.|  
|[ProactiveCaching Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md)|Defines proactive caching settings for the parent element.|  
|[QueryNotification Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/querynotification-element-assl.md)|Contains information for the [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md) element about a query to execute to determine whether a data source has been modified.|  
|[ReportFormatParameter Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md)|Contains the name and value of a parameter that specifies how a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report is formatted at run time.|  
|[ReportParameter Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/reportparameter-element-assl.md)|Contains the name and value of a parameter that is passed to a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report at run time.|  
|[Role Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/role-element-assl.md)|Contains information about a security role.|  
|[Server Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/server-element-assl.md)|Describes an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[ServerProperty Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/serverproperty-element-assl.md)|Defines a server property associated with a [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element.|  
|[SkippedLevelsColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/skippedlevelscolumn-element-assl.md)|Provides the details of a column that stores the number of skipped (empty) levels between each member and its parent.|  
|[SourceMeasureGroup Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/sourcemeasuregroup-element-assl.md)|Identifies the measure group that serves as the data source for a mining structure column.|  
|[TableNotification Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/tablenotification-element-assl.md)|Contains information for the [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md) element about a table or view in a data source that has been modified.|  
|[Trace Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/trace-element-assl.md)|Defines a trace that can be queried.|  
|[Translation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/translation-element-assl.md)|Provides a localized translation for the parent of the [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md) collection.|  
|[UnaryOperatorColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/unaryoperatorcolumn-element-assl.md)|Defines the details of a column providing a unary operator.|  
|[UnknownMemberTranslation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/unknownmembertranslation-element-assl.md)|Contains a translation for the caption of the [UnknownMember](../../../analysis-services/scripting/properties/unknownmember-element-assl.md) element for a [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md) element.|  
|[ValueColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/valuecolumn-element-assl.md)|Identifies the column that provides the value of the parent element.|  
  
## See Also  
 [Analysis Services Scripting Language XML Element Hierarchy &#40;ASSL&#41;](../../../analysis-services/scripting/analysis-services-scripting-language-xml-element-hierarchy-assl.md)  
  
  