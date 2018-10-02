---
title: "Analysis Services Scripting Language XML Data Types (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Analysis Services Scripting Language XML Data Types"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ASSL, data types"
  - "Analysis Services Scripting Language, data types"
  - "data types [Analysis Services Scripting Language]"
ms.assetid: 8e527916-932e-48ec-9010-f22cd4b721e2
author: minewiskan
ms.author: owend
manager: craigg
---
# Analysis Services Scripting Language XML Data Types (ASSL)
  This reference section contains syntax and usage information for each element that acts as a type in the Analysis Services Scripting Language (ASSL) schema.  
  
 Although the ASSL schema contains only XML elements, from a developer's point of view, the elements described in this section correspond to types, such as `Binding` and `Permission`, which are used to define the child elements and properties of other objects.  
  
 Type elements, like objects elements, are never leaf-level elements in the ASSL schema, but have child elements and elements that correspond to object properties.  
  
 However a type element never appears as an element in a script that defines or describes [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects. Instead it appears as the type of other object elements, normally designated with the `type` attribute from the XML Schema Instance schema using `xsi:type` or `xs:type`. For example, `<Assembly xsi:type="ClrAssembly">...</Assembly>`.  
  
 In some cases, a type derives from another type. For example, the `CubeBinding` type derives from the parent `Binding` type.  
  
|Element|Description|  
|-------------|-----------------|  
|[Action Data Type &#40;ASSL&#41;](action-data-type-assl.md)|Defines an abstract primitive data type that represents an action in a [Cube](../objects/cube-element-assl.md) element or a [Perspective](../objects/perspective-element-assl.md) element.|  
|[AggregationAttribute Data Type &#40;ASSL&#41;](aggregationattribute-data-type-assl.md)|Defines a primitive data type that represents the association between an [Aggregation](../objects/aggregation-element-assl.md) element and an attribute.|  
|[AggregationDesignAttribute Data Type &#40;ASSL&#41;](aggregationdesignattribute-data-type-assl.md)|Defines a primitive data type that represents the association between an attribute and an [AggregationDesignDimension](dimension-data-type-assl.md) element.|  
|[AggregationDesignDimension Data Type &#40;ASSL&#41;](dimension-data-type-assl.md)|Defines a primitive data type that represents the relationship between a cube dimension and an [AggregationDesign](../objects/aggregationdesign-element-assl.md) element.|  
|[AggregationDimension Data Type &#40;ASSL&#41;](aggregationdimension-data-type-assl.md)|Defines a primitive data type that represents the relationship between a dimension and an [Aggregation](../objects/aggregation-element-assl.md) element.|  
|[AggregationInstanceAttribute Data Type &#40;ASSL&#41;](aggregationinstanceattribute-data-type-assl.md)|Defines a primitive data type that represents information about an attribute used by an aggregation instance.|  
|[AggregationInstanceCubeDimension Data Type &#40;ASSL&#41;](cubedimension-data-type-assl.md)|Defines a primitive data type that represents information about a cube dimension used by an aggregation instance.|  
|[AggregationInstanceMeasure Data Type &#40;ASSL&#41;](aggregationinstancemeasure-data-type-assl.md)|Defines a primitive data type that represents information about a measure used by an aggregation instance.|  
|[Assembly Data Type &#40;ASSL&#41;](assembly-data-type-assl.md)|Defines an abstract primitive data type that represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly or a COM dynamic link library (DLL) associated with a [Server](../objects/server-element-assl.md) or [Database](../objects/database-element-assl.md) element.|  
|[AttributeBinding Data Type &#40;ASSL&#41;](binding-data-type-assl.md)|Defines a derived data type that represents a binding for an [Attribute](../objects/attribute-element-assl.md) element.|  
|[AttributeTranslation Data Type &#40;ASSL&#41;](translation-data-type-assl.md)|Defines a derived data type that represents a translation associated with an [Attribute](../objects/attribute-element-assl.md) element|  
|[Binding Data Type &#40;ASSL&#41;](binding-data-type-assl.md)|Defines an abstract primitive data type that represents a dependent relationship between two objects in which the data or metadata of one object is dependent on the data or metadata of a bound object.|  
|[ClrAssembly Data Type &#40;ASSL&#41;](clrassembly-data-type-assl.md)|Defines a derived data type that represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly associated with a [Database](../objects/database-element-assl.md) or [Server](../objects/server-element-assl.md) element|  
|[ClrAssemblyFile Data Type &#40;ASSL&#41;](clrassemblyfile-data-type-assl.md)|Defines a primitive data type that represents one of the files that compose a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly ([ClrAssembly](clrassembly-data-type-assl.md) element).|  
|[ColumnBinding Data Type &#40;ASSL&#41;](columnbinding-data-type-assl.md)|Defines a derived data type that represents the binding of a column in a data source view to a [DataItem](dataitem-data-type-assl.md) element.|  
|[ComAssembly Data Type &#40;ASSL&#41;](comassembly-data-type-assl.md)|Defines a derived data type that represents a COM library associated with a [Server](../objects/server-element-assl.md) or [Database](../objects/database-element-assl.md) element.|  
|[CubeAttribute Data Type &#40;ASSL&#41;](cubeattribute-data-type-assl.md)|Defines a primitive data type that represents an attribute associated with a [Cube](../objects/cube-element-assl.md) element.|  
|[CubeAttributeBinding Data Type &#40;ASSL&#41;](cubebinding-data-type-out-of-line-assl.md)|Defines a derived data type that represents the binding of an attribute in a cube dimension to either an action or a mining structure column.|  
|[CubeBinding Data Type &#40;out-of-line&#41; &#40;ASSL&#41;](cubebinding-data-type-out-of-line-assl.md)|Defines a primitive data type that represents the relationship between a [Cube](../objects/cube-element-assl.md) element and a [DataSource](../objects/datasource-element-assl.md) element.|  
|[CubeDimension Data Type &#40;ASSL&#41;](cubedimension-data-type-assl.md)|Defines a primitive data type that represents the relationship between a dimension and a cube.|  
|[CubeDimensionBinding Data Type &#40;ASSL&#41;](dimensionbinding-data-type-assl.md)|Defines a derived data type that represents the binding of a [Dimension](../objects/dimension-element-assl.md), [Measure](../objects/measure-element-assl.md), or [MiningModel](../objects/miningmodel-element-assl.md) element to a cube dimension.|  
|[CubeDimensionPermission Data Type &#40;ASSL&#41;](permission-data-type-assl.md)|Defines a primitive data type that represents the permissions for a single role on a specific dimension in a cube.|  
|[CubeHierarchy Data Type &#40;ASSL&#41;](hierarchy-data-type-assl.md)|Defines a primitive data type that represents information about a [Hierarchy](../objects/hierarchy-element-assl.md) element in a [Cube](../objects/cube-element-assl.md) element.|  
|[DataBlock Data Type &#40;ASSL&#41;](datablock-data-type-assl.md)|Defines a primitive data type that represents a collection of data blocks used to store the binary contents of a [ClrAssemblyFile](clrassemblyfile-data-type-assl.md) element.|  
|[DataItem Data Type &#40;ASSL&#41;](dataitem-data-type-assl.md)|Defines a primitive data type that represents the data-related characteristics of a data item, such as a column or attribute.|  
|[DataMiningMeasureGroupDimension Data Type &#40;ASSL&#41;](measuregroupdimension-data-type-assl.md)|Defines a derived data type that represents the relationship between a measure group and a data mining dimension.|  
|[DataSource Data Type &#40;ASSL&#41;](datasource-data-type-assl.md)|Defines an abstract primitive data type that represents a data source in a [Database](../objects/database-element-assl.md) element.|  
|[DataSourceViewBinding Data Type &#40;ASSL&#41;](datasourceviewbinding-data-type-assl.md)|Defines a derived data type that represents a binding between a data source view and the parent element.|  
|[DegenerateMeasureGroupDimension Data Type &#40;ASSL&#41;](degeneratemeasuregroupdimension-data-type-assl.md)|Defines a derived data type that represents the relationship between a degenerate dimension (that is, a fact dimension) and a measure group.|  
|[Dimension Data Type &#40;ASSL&#41;](dimension-data-type-assl.md)|Defines a primitive data type that represents a database dimension.|  
|[DimensionAttribute Data Type &#40;ASSL&#41;](dimensionattribute-data-type-assl.md)|Defines a primitive data type that represents an attribute in a dimension.|  
|[DimensionBinding Data Type &#40;ASSL&#41;](dimensionbinding-data-type-assl.md)|Defines a derived data type that represents the binding between a data source and a [Dimension](../objects/dimension-element-assl.md) element.|  
|[DimensionPermission Data Type &#40;ASSL&#41;](permission-data-type-assl.md)|Defines a derived data type that represents the permissions assigned to a database dimension.|  
|[DrillThroughAction Data Type &#40;ASSL&#41;](drillthroughaction-data-type-assl.md)|Defines a derived data type that represents a drillthrough action.|  
|[DSVTableBinding Data Type &#40;ASSL&#41;](tablebinding-data-type-assl.md)|Defines a derived data type that represents the binding between a table and a [DataSourceView](../objects/datasourceview-element-assl.md) element.|  
|[EventColumn Data Type &#40;ASSL&#41;](eventcolumn-data-type-assl.md)|Defines a primitive data type that represents a column of information to be captured for an [Event](../objects/event-element-assl.md) element as part of a [Trace](../objects/trace-element-assl.md) element.|  
|[Hierarchy Data Type &#40;ASSL&#41;](hierarchy-data-type-assl.md)|Defines a primitive data type that represents a hierarchy in a dimension.|  
|[ImpersonationInfo Data Type &#40;ASSL&#41;](impersonationinfo-data-type-assl.md)|Defines a primitive data type that represents the information used to impersonate a user.|  
|[IncrementalProcessingNotification Data Type &#40;ASSL&#41;](incrementalprocessingnotification-data-type-assl.md)|Defines a derived data type which represents information for the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about a query to execute to determine the progress of incremental processing.|  
|[InheritedBinding Data Type &#40;ASSL&#41;](inheritedbinding-data-type-assl.md)|Defines a derived data type that indicates that a [MeasureGroupAttribute](measuregroupattribute-data-type-assl.md) element inherits its bindings from the attribute.|  
|[ManyToManyMeasureGroupDimension Data Type &#40;ASSL&#41;](manytomanymeasuregroupdimension-data-type-assl.md)|Defines a derived data type that represents the relationship between a many-to-many dimension and a measure group.|  
|[MeasureBinding Data Type &#40;ASSL&#41;](measurebinding-data-type-assl.md)|Defines a derived data type which represents the binding of a measure to the parent element.|  
|[MeasureGroupAttribute Data Type &#40;ASSL&#41;](measuregroupattribute-data-type-assl.md)|Defines a primitive data type that represents the relationship between an attribute and a measure group.|  
|[MeasureGroupBinding Data Type &#40;ASSL&#41;](measuregroupbinding-data-type-assl.md)|Defines a derived data type that represents a binding to a [MeasureGroup](../objects/group-element-assl.md) element.|  
|[MeasureGroupBinding Data Type &#40;out-of-line&#41; &#40;ASSL&#41;](measuregroupbinding-data-type-out-of-line-assl.md)|Defines a primitive data type that represents a binding to a measure group.|  
|[MeasureGroupDimension Data Type &#40;ASSL&#41;](measuregroupdimension-data-type-assl.md)|Defines an abstract primitive data type that represents the relationship between a dimension and a measure group.|  
|[MeasureGroupDimensionBinding Data Type &#40;ASSL&#41;](measuregroupdimensionbinding-data-type-assl.md)|Defines a derived data type that represents a binding between a dimension and a measure group.|  
|[MeasureGroupHierarchy Data Type &#40;ASSL&#41;](measuregrouphierarchy-data-type-assl.md)|Defines a primitive data type that represents information about a hierarchy in a measure group.|  
|[MiningModelColumn Data Type &#40;ASSL&#41;](miningmodelcolumn-data-type-assl.md)|Defines a primitive data type that represents information about a column in a [MiningModel](../objects/miningmodel-element-assl.md) element.|  
|[MiningModelingFlag Data Type &#40;ASSL&#41;](miningmodelingflag-data-type-assl.md)|Defines a primitive data type that represents the available modeling flags for a [ModelingFlag](../objects/modelingflag-element-assl.md) element.|  
|[MiningStructureColumn Data Type &#40;ASSL&#41;](miningstructurecolumn-data-type-assl.md)|Defines an abstract primitive data type that represents information about a column in a [MiningStructure](../objects/miningstructure-element-assl.md) element.|  
|[OlapDataSource Data Type &#40;ASSL&#41;](olapdatasource-data-type-assl.md)|Defines a derived data type that represents a multidimensional [DataSource](../objects/datasource-element-assl.md) element.|  
|[PartitionBinding Data Type &#40;ASSL&#41;](partitionbinding-data-type-assl.md)|Defines a derived data type that represents a binding to a [Partition](../objects/partition-element-assl.md) element.|  
|[Permission Data Type &#40;ASSL&#41;](permission-data-type-assl.md)|Defines an abstract primitive data type that represents information about an individual permission.|  
|[PerspectiveAction Data Type &#40;ASSL&#41;](perspectiveaction-data-type-assl.md)|Defines a primitive data type that represents information about an action in a [Perspective](../objects/perspective-element-assl.md) element.|  
|[PerspectiveAttribute Data Type &#40;ASSL&#41;](perspectiveattribute-data-type-assl.md)|Defines a primitive data type that represents information about an attribute in a [PerspectiveDimension](perspectivedimension-data-type-assl.md) element.|  
|[PerspectiveCalculation Data Type &#40;ASSL&#41;](perspectivecalculation-data-type-assl.md)|Defines a primitive data type that represents the relationship between a calculation and a [Perspective](../objects/perspective-element-assl.md) element.|  
|[PerspectiveDimension Data Type &#40;ASSL&#41;](perspectivedimension-data-type-assl.md)|Defines a primitive data type that represents information about a dimension in a perspective.|  
|[PerspectiveHierarchy Data Type &#40;ASSL&#41;](perspectivehierarchy-data-type-assl.md)|Defines a primitive data type that represents information about a hierarchy in a [PerspectiveDimension](perspectivedimension-data-type-assl.md) element.|  
|[PerspectiveKpi Data Type &#40;ASSL&#41;](perspectivekpi-data-type-assl.md)|Defines a primitive data type that represents information about a key performance indicator (KPI) in a [Perspective](../objects/perspective-element-assl.md) element.|  
|[PerspectiveMeasure Data Type &#40;ASSL&#41;](perspectivemeasure-data-type-assl.md)|Defines a primitive data type that represents information about a measure in a [PerspectiveMeasureGroup](perspectivemeasuregroup-data-type-assl.md) element.|  
|[PerspectiveMeasureGroup Data Type &#40;ASSL&#41;](perspectivemeasuregroup-data-type-assl.md)|Defines a primitive data type that represents information about a measure group in a [Perspective](../objects/perspective-element-assl.md) element.|  
|[ProactiveCachingBinding Data Type &#40;ASSL&#41;](proactivecachingbinding-data-type-assl.md)|Defines an abstract derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes that require rebuilding the cache, or about the status of the rebuilding process.|  
|[ProactiveCachingIncrementalProcessingBinding Data Type &#40;ASSL&#41;](proactivecachingincrementalprocessingbinding-data-type-assl.md)|Defines a derived data type that represents a binding to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about the status of the process of rebuilding the cache.|  
|[ProactiveCachingInheritedBinding Data Type &#40;ASSL&#41;](proactivecachinginheritedbinding-data-type-assl.md)|Defines a derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes in tables and views identified through existing data bindings that require rebuilding the cache.|  
|[ProactiveCachingObjectNotificationBinding Data Type &#40;ASSL&#41;](proactivecachingobjectnotificationbinding-data-type-assl.md)|Defines an abstract derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes, either in specified tables and views or in tables and views identified through existing data bindings that require rebuilding the cache.|  
|[ProactiveCachingQueryBinding Data Type &#40;ASSL&#41;](querybinding-data-type-assl.md)|Defines a derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes in tables and views, identified through the execution of the specified queries that require rebuilding the cache.|  
|[ProactiveCachingTablesBinding Data Type &#40;ASSL&#41;](proactivecachingtablesbinding-data-type-assl.md)|Defines a derived data type that represents information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about data source changes in specified tables and views that require rebuilding the cache.|  
|[PushedDataSource Data Type &#40;ASSL&#41;](pusheddatasource-data-type-assl.md)|Defines a primitive data type that represents a data source (such as an [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] package) used for "pushing" data into a [Cube](../objects/cube-element-assl.md) element.|  
|[QueryBinding Data Type &#40;ASSL&#41;](querybinding-data-type-assl.md)|Defines a derived data type that represents the association of a [DataSource](../objects/datasource-element-assl.md) element with a [QueryDefinition](../properties/querydefinition-element-assl.md) element.|  
|[ReferenceMeasureGroupDimension Data Type &#40;ASSL&#41;](referencemeasuregroupdimension-data-type-assl.md)|Defines a derived data type that represents a dimension that is indirectly related to the fact table through an intermediate dimension. (For example, a Sales measure group can reference a Geography dimension, which is related through the Customer dimension.)|  
|[RegularMeasureGroupDimension Data Type &#40;ASSL&#41;](regularmeasuregroupdimension-data-type-assl.md)|Defines a derived data type that represents a regular relationship between a dimension and a measure group.|  
|[RelationalDataSource Data Type &#40;ASSL&#41;](relationaldatasource-data-type-assl.md)|Defines a derived data type that represents a [DataSource](../objects/datasource-element-assl.md) element based on a relational data source.|  
|[ReportAction Data Type &#40;ASSL&#41;](reportaction-data-type-assl.md)|Defines a derived data type that represents an action that generates a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report.|  
|[RowBinding Data Type &#40;ASSL&#41;](rowbinding-data-type-assl.md)|Defines a derived data type that represents a binding to the rows of a table in a [DataSourceView](../objects/datasourceview-element-assl.md) element.|  
|[ScalarMiningStructureColumn Data Type &#40;ASSL&#41;](scalarminingstructurecolumn-data-type-assl.md)|Defines a derived data type that represents a [MiningStructureColumn](miningstructurecolumn-data-type-assl.md) element that contains scalar values, as opposed to the nested tables associated with the [TableMiningStructureColumn](tableminingstructurecolumn-data-type-assl.md) element that contains nested tables.|  
|[StandardAction Data Type &#40;ASSL&#41;](standardaction-data-type-assl.md)|Defines a derived data type that represents any [Action](../objects/action-element-assl.md) element other than a [DrillThroughAction](drillthroughaction-data-type-assl.md) element or a [ReportAction](reportaction-data-type-assl.md) element.|  
|[TableBinding Data Type &#40;ASSL&#41;](tablebinding-data-type-assl.md)|Defines a derived data type that represents a binding to a table.|  
|[TableMiningStructureColumn Data Type &#40;ASSL&#41;](tableminingstructurecolumn-data-type-assl.md)|Defines a derived data type that represents a [MiningStructureColumn](miningstructurecolumn-data-type-assl.md) element that contains nested tables, as opposed to the scalar values associated with the [ScalarMiningStructureColumn](scalarminingstructurecolumn-data-type-assl.md) element that contains scalar values.|  
|[TabularBinding Data Type &#40;ASSL&#41;](tabularbinding-data-type-assl.md)|Defines an abstract derived data type that represents a binding to a tabular item such as a table or a cube dimension.|  
|[TimeAttributeBinding Data Type &#40;ASSL&#41;](timebinding-data-type-assl.md)|Defines a derived data type that represents a "placeholder" binding for generated data items in a server time dimension, such as the key columns of an attribute.|  
|[TimeBinding Data Type &#40;ASSL&#41;](timebinding-data-type-assl.md)|Defines a derived data type that represents a binding to time periods.|  
|[Translation Data Type &#40;ASSL&#41;](translation-data-type-assl.md)|Defines a primitive data type that represents a localized translation.|  
|[UserDefinedGroupBinding Data Type &#40;ASSL&#41;](userdefinedgroupbinding-data-type-assl.md)|Defines a derived data type that represents a user-defined grouping for an attribute.|  
  
## See Also  
 [Analysis Services Scripting Language XML Element Hierarchy &#40;ASSL&#41;](../analysis-services-scripting-language-xml-element-hierarchy-assl.md)  
  
  
