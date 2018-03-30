---
title: "Objects (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "ASSL, objects"
  - "objects [Analysis Services Scripting Language]"
  - "Analysis Services Scripting Language, objects"
ms.assetid: 0f672b93-c317-47e5-b44d-ecea9b587c98
caps.latest.revision: 21
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Objects (ASSL)
  This reference section contains syntax and usage information for each element that acts as an object in the Analysis Services Scripting Language (ASSL) schema.  
  
 Although the ASSL schema contains only XML elements, from a developer's point of view, the elements described in this section correspond to objects, such as `Database`, `Cube`, and `Dimension` objects, in the hierarchy of objects contained by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 Objects are never leaf-level elements in the ASSL schema, but have child elements and elements that correspond to object properties.  
  
 In a few cases, a leaf-level element in the schema that may appear to be a property is classified as an object because the element's type is an object type. For example, the `Source` of a `Dimension` object is of type `DimensionBinding`.  
  
## In This Section  
  
|Element|Description|  
|-------------|-----------------|  
|[Account Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/account-element-assl.md)|Contains details about an account type within a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.|  
|[Action Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/action-element-assl.md)|Contains information about an action available in a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element or a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.|  
|[Aggregation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregation-element-assl.md)|Defines a single aggregation for a [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md) element.|  
|[AggregationDesign Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md)|Defines a set of aggregation definitions that can be shared across multiple partitions in a database.|  
|[AggregationInstance Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregationinstance-element-assl.md)|Defines an aggregation instance for a partition.|  
|[AlgorithmParameter Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/algorithmparameter-element-assl.md)|Defines a parameter for the algorithm used by a [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md) element.|  
|[AllMemberTranslation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/allmembertranslation-element-assl.md)|Contains a translation for the caption of the All member of a [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) element.|  
|[Annotation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/annotation-element-assl.md)|Contains elements that are used to extend the ASSL schema.|  
|[Assembly Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/assembly-element-assl.md)|Represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly or a COM dynamic link library (DLL) associated with a [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md) element or a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.|  
|[Attribute Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/attribute-element-assl.md)|Contains the description of an attribute.|  
|[AttributeAllMemberTranslation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/attributeallmembertranslation-element-assl.md)|Contains a translation for the caption of the All member of a [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md) element.|  
|[AttributePermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/attributepermission-element-assl.md)|Defines the permissions that members of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element have on the attributes of an individual dimension in a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.|  
|[AttributeRelationship Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/attributerelationship-element-assl.md)|Provides details about the relationship between two attributes.|  
|[Block Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/block-element-assl.md)|Contains all or a portion of the binary contents of a [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md) element.|  
|[Calculation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculation-element-assl.md)|Asssociates a calculation with a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.|  
|[CalculationProperty Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md)|Contains a collection of user interface properties for a calculation used in an [MdxScript](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md) element.|  
|[CaptionColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/captioncolumn-element-assl.md)|Defines the column that provides the caption for the attribute.|  
|[CellPermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/cellpermission-element-assl.md)|Describes the permissions that members of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element have on individual cells within a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.|  
|[Column Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/column-element-assl.md)|Describes a column in the collection of columns associated with the parent element.|  
|[Command Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/command-element-assl.md)|Defines a command that is available for use within the context of the parent element of the Commands collection.|  
|[Cube Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|Defines a regular, virtual, or linked cube in an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.|  
|[CubePermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/cubepermission-element-assl.md)|Defines the permissions of the members of a particular [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element in a specific [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.|  
|[CustomRollupColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/customrollupcolumn-element-assl.md)|Defines the details of the column that provide a custom rollup formula.|  
|[CustomRollupPropertiesColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/customrolluppropertiescolumn-element-assl.md)|Defines the details of a column that provide the properties of a custom rollup formula.|  
|[Data Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/data-element-assl.md)|Contains (in the collection of child `Block` elements) the binary contents of a [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md) element.|  
|[Database Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/database-element-assl.md)|Defines an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database.|  
|[DatabasePermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/databasepermission-element-assl.md)|Defines the default permissions in a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element for a specific [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element.|  
|[DataSource Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/datasource-element-assl.md)|Defines a data source in a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.|  
|[DataSourcePermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/datasourcepermission-element-assl.md)|Defines the default permissions in a [DataSource](../../../2014/analysis-services/dev-guide/datasource-data-type-assl.md) data type for a specific [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element.|  
|[DataSourceView Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/datasourceview-element-assl.md)|Defines a data source view used by a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.|  
|[Dimension Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|Defines a dimension.|  
|[DimensionPermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dimensionpermission-element-assl.md)|Defines the permissions that belong to a particular [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element for a specific database dimension or cube dimension.|  
|[ErrorConfiguration Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md)|Specifies settings for handling errors that can occur when the parent element is processed.|  
|[Event Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/event-element-assl.md)|Defines an event to be captured as part of a [Trace](../../../2014/analysis-services/dev-guide/trace-element-assl.md) element.|  
|[File Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/file-element-assl.md)|Defines one of the files that compose a [ClrAssembly](../../../2014/analysis-services/dev-guide/clrassembly-data-type-assl.md) element.|  
|[ForeignKeyColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/foreignkeycolumn-element-assl.md)|Identifies the join to a parent table for a relational data source.|  
|[Group Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/group-element-assl.md)|Defines a group of members bound to an attribute.|  
|[Hierarchy Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md)|Defines a hierarchy in a dimension.|  
|[IncrementalProcessingNotification Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/incrementalprocessingnotification-element-assl.md)|Contains information for the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about a query to execute to determine the progress of incremental processing.|  
|[KeyColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md)|Contains the definition of a column that is, or is part of, the key for an attribute.|  
|[Kpi Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/kpi-element-assl.md)|Defines a key performance indicator (KPI) within a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element or a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.|  
|[Level Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/level-element-assl.md)|Defines a level in a [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) element.|  
|[MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)|Contains information about a Multidimensional Expressions (MDX) script that is associated with a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.|  
|[Measure Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|Defines a measure.|  
|[MeasureGroup Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|Defines a set of measures at the same level of granularity.|  
|[Member Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/member-element-assl.md)|Contains the name of a member of a [Group](../../../2014/analysis-services/dev-guide/group-element-assl.md) element or of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element.|  
|[MiningModel Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md)|Defines a single data mining model.|  
|[MiningModelPermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningmodelpermission-element-assl.md)|Defines the permissions members of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element have on an individual [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md) element.|  
|[MiningStructure Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md)|Defines the structure for a set of mining models.|  
|[MiningStructurePermission Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningstructurepermission-element-assl.md)|Defines the permissions that members of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element have on an individual [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md) element.|  
|[ModelingFlag Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/modelingflag-element-assl.md)|Contains a modeling flag for a column in a mining structure or a mining model.|  
|[NameColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md)|Identifies the column that provides the name of the parent element.|  
|[NamingTemplateTranslation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/namingtemplatetranslation-element-assl.md)|Provides a localized translation of the `NamingTemplate` element for a parent [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md) data type.|  
|[Partition Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|Defines a partition of a [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) element or a partition binding in an out-of-line [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)element.|  
|[Perspective Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|Defines details for a perspective of a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.|  
|[ProactiveCaching Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md)|Defines proactive caching settings for the parent element.|  
|[QueryNotification Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/querynotification-element-assl.md)|Contains information for the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about a query to execute to determine whether a data source has been modified.|  
|[ReportFormatParameter Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/reportformatparameter-element-assl.md)|Contains the name and value of a parameter that specifies how a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report is formatted at run time.|  
|[ReportParameter Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/reportparameter-element-assl.md)|Contains the name and value of a parameter that is passed to a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report at run time.|  
|[Role Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/role-element-assl.md)|Contains information about a security role.|  
|[Server Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/server-element-assl.md)|Describes an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[ServerProperty Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md)|Defines a server property associated with a [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md) element.|  
|[SkippedLevelsColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/skippedlevelscolumn-element-assl.md)|Provides the details of a column that stores the number of skipped (empty) levels between each member and its parent.|  
|[SourceMeasureGroup Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/sourcemeasuregroup-element-assl.md)|Identifies the measure group that serves as the data source for a mining structure column.|  
|[TableNotification Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/tablenotification-element-assl.md)|Contains information for the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about a table or view in a data source that has been modified.|  
|[Trace Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/trace-element-assl.md)|Defines a trace that can be queried.|  
|[Translation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|Provides a localized translation for the parent of the [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md) collection.|  
|[UnaryOperatorColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/unaryoperatorcolumn-element-assl.md)|Defines the details of a column providing a unary operator.|  
|[UnknownMemberTranslation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/unknownmembertranslation-element-assl.md)|Contains a translation for the caption of the [UnknownMember](../../../2014/analysis-services/dev-guide/unknownmember-element-assl.md) element for a [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md) element.|  
|[ValueColumn Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/valuecolumn-element-assl.md)|Identifies the column that provides the value of the parent element.|  
  
## See Also  
 [Analysis Services Scripting Language XML Element Hierarchy &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-element-hierarchy-assl.md)  
  
  