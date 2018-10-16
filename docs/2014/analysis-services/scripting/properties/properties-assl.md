---
title: "Properties (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "properties [Analysis Services Scripting Language]"
  - "Analysis Services Scripting Language, properties"
  - "ASSL, properties"
ms.assetid: 9a38cdc9-a210-421a-90e9-6391876765fa
author: minewiskan
ms.author: owend
manager: craigg
---
# Properties (ASSL)
  This reference section contains syntax and usage information for each element that acts as an object property in the Analysis Services Scripting Language (ASSL) schema.  
  
 Although the ASSL schema contains only XML elements, from a developer's point of view, the elements described in this section correspond to properties that describe objects.  
  
 Properties are leaf-level elements in the ASSL schema, and do not have child elements or elements that correspond to properties of their own.  
  
 In a few cases, a leaf-level element in the schema that may appear to be a property is classified as an object because the element's type is an object type. For example, the `Source` of a `Dimension` object is of type `DimensionBinding`.  
  
## In This Section  
  
|Element|Description|  
|-------------|-----------------|  
|[Access Element &#40;ASSL&#41;](access-element-assl.md)|Indicates the level of access given to a [CellPermission](../objects/cellpermission-element-assl.md) element.|  
|[Account Element &#40;ImpersonationInfo&#41; &#40;ASSL&#41;](account-element-impersonationinfo-assl.md)|Contains the name of the user account for the [ImpersonationInfo](../data-type/impersonationinfo-data-type-assl.md) data type.|  
|[AccountType Element &#40;ASSL&#41;](accounttype-element-assl.md)|Contains the name of an account type defined in a [Database](../objects/database-element-assl.md) element.|  
|[ActionID Element &#40;ASSL&#41;](id-element-assl.md)|Contains the name of an [Action](../objects/action-element-assl.md) element defined on a [Cube](../objects/cube-element-assl.md) element which is made available in a [Perspective](../objects/perspective-element-assl.md) element as a [PerspectiveAction](../data-type/action-data-type-assl.md) element.|  
|[Administer Element &#40;ASSL&#41;](administer-element-assl.md)|Indicates whether the associated permission includes the right to administer a `Database` element.|  
|[AggregateFunction Element &#40;ASSL&#41;](aggregatefunction-element-assl.md)|Defines the type of aggregate function used by a [Measure](../objects/measure-element-assl.md) element.|  
|[AggregationDesignID Element &#40;ASSL&#41;](aggregationdesignid-element-assl.md)|Identifies the [AggregationDesign](../objects/aggregationdesign-element-assl.md) element associated with the [Partition](../objects/partition-element-assl.md) element.|  
|[AggregationFunction Element &#40;ASSL&#41;](aggregationfunction-element-assl.md)|Contains the aggregation function to use for the account type.|  
|[AggregationID Element &#40;ASSL&#41;](aggregationid-element-assl.md)|Identifies the aggregation definition from the `AggregationDesign` element used to create the aggregation instance.|  
|[AggregationInstanceSource Element &#40;ASSL&#41;](aggregationinstancesource-element-assl.md)|Identifies the source of data for user-defined aggregation instances bound to a `Partition` element.|  
|[AggregationPrefix Element &#40;ASSL&#41;](aggregationprefix-element-assl.md)|Defines the common prefix to be used for aggregation names throughout the associated parent element.|  
|[AggregationStorage Element &#40;ASSL&#41;](aggregationstorage-element-assl.md)|Identifies the storage method for aggregations.|  
|[AggregationType Element &#40;ASSL&#41;](aggregationtype-element-assl.md)|Defines the type of aggregation stored by the `Partition` element.|  
|[AggregationUsage Element &#40;ASSL&#41;](aggregationusage-element-assl.md)|Controls how the Aggregation Designer in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] designs aggregations.|  
|[Algorithm Element &#40;ASSL&#41;](algorithm-element-assl.md)|Defines the algorithm used by a [MiningModel](../objects/miningmodel-element-assl.md) element.|  
|[Alias Element &#40;ASSL&#41;](alias-element-assl.md)|Defines an alias for an [Account](../objects/account-element-assl.md) element.|  
|[AllMemberAggregationUsage Element &#40;ASSL&#41;](allmemberaggregationusage-element-assl.md)|Controls how the Aggregation Designer in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] designs aggregations.|  
|[AllMemberName Element &#40;ASSL&#41;](name-element-assl.md)|Contains the caption in the default language for the All member of a [Hierarchy](../objects/hierarchy-element-assl.md) element.|  
|[AllowBrowsing Element &#40;ASSL&#41;](allowbrowsing-element-assl.md)|Defines whether the members of a [Role](../objects/role-element-assl.md) element have browse permission on a `MiningModel` element.|  
|[AllowDrillThrough Element &#40;ASSL&#41;](allowdrillthrough-element-assl.md)|Determines whether drillthrough is permitted on the parent element.|  
|[AllowDuplicateNames Element &#40;ASSL&#41;](allowduplicatenames-element-assl.md)|Determines whether duplicate names are allowed in a `Hierarchy` element.|  
|[AllowedSet Element &#40;ASSL&#41;](allowedset-element-assl.md)|Contains a set expression that defines the set of allowed permissions for a `Role` element on an attribute.|  
|[Application Element &#40;ASSL&#41;](application-element-assl.md)|Identifies the application associated with an `Action` element.|  
|[AssociatedMeasureGroupID Element &#40;ASSL&#41;](measuregroupid-element-assl.md)|Contains the identifier (ID) of the [MeasureGroup](../objects/group-element-assl.md) element associated with a [CalculationProperty](../objects/calculationproperty-element-assl.md) element or a [Kpi](../objects/kpi-element-assl.md) element.|  
|[AttributeAllMemberName Element &#40;ASSL&#41;](attributeallmembername-element-assl.md)|Contains the caption, in the default language, for the All member of the dimension.|  
|[AttributeHierarchyDisplayFolder Element &#40;ASSL&#41;](displayfolder-element-assl.md)|Identifies the folder in which to display the associated attribute hierarchy.|  
|[AttributeHierarchyEnabled Element &#40;ASSL&#41;](enabled-element-assl.md)|Determines whether an attribute hierarchy is enabled for the attribute.|  
|[AttributeHierarchyOptimizedState Element &#40;ASSL&#41;](state-element-assl.md)|Determines the level of optimization applied to the attribute hierarchy.|  
|[AttributeHierarchyOrdered Element &#40;ASSL&#41;](attributehierarchyordered-element-assl.md)|Determines whether the associated attribute hierarchy is ordered.|  
|[AttributeHierarchyVisible Element &#40;ASSL&#41;](visible-element-assl.md)|Determines whether the attribute hierarchy is visible to client applications.|  
|[AttributeID Element &#40;ASSL&#41;](attributeid-element-assl.md)|Contains the ID of the attribute associated with the parent element.|  
|[Audit Element &#40;ASSL&#41;](audit-element-assl.md)|Specifies that a [Trace](../objects/trace-element-assl.md) element cannot drop any events, even if this results in degraded performance on the server.|  
|[AutoRestart Element &#40;ASSL&#41;](autorestart-element-assl.md)|Determines whether a `Trace` element should automatically restart if the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] service stops and restarts.|  
|[BackColor Element &#40;ASSL&#41;](backcolor-element-assl.md)|Describes color-related display characteristics of the parent element.|  
|[CacheMode Element &#40;ASSL&#41;](cachemode-element-assl.md)|Determines the caching mechanism used for training data retrieved while processing a mining structure.|  
|[CalculationReference Element &#40;ASSL&#41;](calculationreference-element-assl.md)|Contains the name of the named set or calculated cell referenced by the `CalculationProperty` element.|  
|[CalculationType Element &#40;ASSL&#41;](calculationtype-element-assl.md)|Describes the type of calculation defined in the associated `CalculationProperty` element.|  
|[CalendarEndDate Element &#40;ASSL&#41;](calendarenddate-element-assl.md)|Defines the end date of the calendar period for a [TimeBinding](../data-type/binding-data-type-assl.md) element.|  
|[CalendarLanguage Element &#40;ASSL&#41;](language-element-assl.md)|Defines the calendar language used for the `TimeBinding` element.|  
|[CalendarStartDate Element &#40;ASSL&#41;](calendarstartdate-element-assl.md)|Defines the start date of the calendar period for the `TimeBinding` element.|  
|[Caption Element &#40;ASSL&#41;](caption-element-assl.md)|Contains the caption for the associated parent element.|  
|[CaptionIsMdx Element &#40;ASSL&#41;](captionismdx-element-assl.md)|Defines whether the caption for the `Action` element is a Multidimensional Expressions (MDX) expression.|  
|[Cardinality Element &#40;ASSL&#41;](cardinality-element-assl.md)|Indicates the cardinality of the relationship described by an [AttributeRelationship](../objects/attributerelationship-element-assl.md) or [RegularMeasureGroupDimension](../data-type/dimension-data-type-assl.md).|  
|[CaseCubeDimensionID Element &#40;ASSL&#41;](dimensionid-element-assl.md)|Contains the ID of the cube dimension that relates the data mining dimension to the measure group.|  
|[ClassifiedColumnID Element &#40;ASSL&#41;](classifiedcolumnid-element-assl.md)|Contains the ID of a related column that is classified by the [ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) element.|  
|[Collation Element &#40;ASSL&#41;](collation-element-assl.md)|Determines the collation used by the parent element.|  
|[ColumnID Element &#40;ColumnBinding&#41; &#40;ASSL&#41;](columnid-element-columnbinding-assl.md)|Contains the ID of the column within the table to which the data item is bound.|  
|[ColumnID Element &#40;EventColumn&#41; &#40;ASSL&#41;](columnid-element-eventcolumn-assl.md)|Contains the ID of the column of information to be captured for an event as part of a `Trace` element.|  
|[Condition Element &#40;ASSL&#41;](condition-element-assl.md)|Contains an MDX expression that determines whether the `Action` parent element applies to the target.|  
|[ConnectionString Element &#40;ASSL&#41;](connectionstring-element-assl.md)|Contains the encrypted connection string for a [DataSource](../objects/datasource-element-assl.md) element.|  
|[ConnectionStringSecurity Element &#40;ASSL&#41;](connectionstringsecurity-element-assl.md)|Specifies whether the user's password is stripped from the data source connection string for security purposes.|  
|[Content Element &#40;ASSL&#41;](content-element-assl.md)|Describes the content of the column in the [MiningStructure](../objects/miningstructure-element-assl.md) element.|  
|[CreatedTimestamp Element &#40;ASSL&#41;](createdtimestamp-element-assl.md)|Contains the read-only creation timestamp of the parent element.|  
|[CubeDimensionID Element &#40;ASSL&#41;](cubedimensionid-element-assl.md)|Identifies the [CubeDimension](../data-type/cubedimension-data-type-assl.md) element associated with the parent element.|  
|[CubeID Element &#40;ASSL&#41;](cubeid-element-assl.md)|Identifies the `Cube` element associated with a [Binding](../data-type/binding-data-type-assl.md) element.|  
|[CurrentStorageMode Element &#40;ASSL&#41;](storagemode-element-assl.md)|Determines the current storage mode for the parent element.|  
|[CurrentTimeMember Element &#40;ASSL&#41;](../objects/member-element-assl.md)|Defines the current member of a time dimension associated with a `Kpi` element.|  
|[DataAggregation Element &#40;ASSL&#41;](../objects/aggregation-element-assl.md)|Determines whether the instance can aggregate persisted data or cached data for the `MeasureGroup`.|  
|[DatabaseID Element &#40;ASSL&#41;](databaseid-element-assl.md)|Identifies the `Database` element associated with an out-of-line `Binding` element.|  
|[DataSize Element &#40;ASSL&#41;](datasize-element-assl.md)|Contains the size in bytes of a [DataItem](../data-type/dataitem-data-type-assl.md) element.|  
|[DataSourceID Element &#40;ASSL&#41;](datasourceid-element-assl.md)|Identifies the `DataSource` element associated with the parent element.|  
|[DataSourceImpersonationInfo Element &#40;ASSL&#41;](impersonationinfo-element-assl.md)|Contains the information used to determine impersonation behavior when connecting to the data source for a `Database` element.|  
|[DataSourceViewID Element &#40;ASSL&#41;](datasourceviewid-element-assl.md)|Identifies the [DataSourceView](../objects/datasourceview-element-assl.md) element associated with the `Binding` parent element.|  
|[DataType Element &#40;ASSL&#41;](datatype-element-assl.md)|Defines the data type of the associated element.|  
|[DbSchemaName Element &#40;ASSL&#41;](dbschemaname-element-assl.md)|Contains the name of the schema used by the parent element in the table identified by the [DbTableName](dbtablename-element-assl.md) element.|  
|[DbTableName Element &#40;ASSL&#41;](dbtablename-element-assl.md)|Contains the name of the table to which the parent element is bound.|  
|[Default Element &#40;ASSL&#41;](default-element-assl.md)|Determines whether the `DrillThroughAction` is the default drillthrough action.|  
|[DefaultMeasure Element &#40;ASSL&#41;](defaultmeasure-element-assl.md)|Contains an MDX language expression that defines the default measure for a `Cube` or `Perspective` element.|  
|[DefaultMember Element &#40;ASSL&#41;](defaultmember-element-assl.md)|Contains an MDX expression that identifies the default member of the parent element.|  
|[DefaultScript Element &#40;ASSL&#41;](defaultscript-element-assl.md)|Identifies the default [MdxScript](../objects/mdxscript-element-assl.md) element in the [MdxScripts](../collections/mdxscripts-element-assl.md) collection.|  
|[DefaultValue Element &#40;ASSL&#41;](value-element-assl.md)|Contains the read-only default value of the associated [ServerProperty](../objects/serverproperty-element-assl.md) element.|  
|[DeniedSet Element &#40;ASSL&#41;](deniedset-element-assl.md)|Contains a set expression that defines the list of permissions that are denied on the associated attribute.|  
|[DependsOnDimensionID Element &#40;ASSL&#41;](dependsondimensionid-element-assl.md)|Contains the ID of another dimension on which the parent dimension depends.|  
|[Description Element &#40;ASSL&#41;](description-element-assl.md)|Contains the description of the parent element.|  
|[DimensionID Element &#40;ASSL&#41;](dimensionid-element-assl.md)|Contains the ID of the dimension.|  
|[DiscretizationBucketCount Element &#40;ASSL&#41;](discretizationbucketcount-element-assl.md)|Contains the number of buckets into which to discretize.|  
|[DiscretizationMethod Element &#40;ASSL&#41;](discretizationmethod-element-assl.md)|Defines the method to be used for discretization.|  
|[DisplayFlag Element &#40;ASSL&#41;](displayflag-element-assl.md)|Contains a read-only hint that indicates whether user interface components should display the associated `ServerProperty` element.|  
|[DisplayFolder Element &#40;ASSL&#41;](displayfolder-element-assl.md)|Specifies the folder in which to list the parent element. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] applications for developers and administrators may support the use of display folders to categorize multiple elements visually.|  
|[Distribution Element &#40;ASSL&#41;](distribution-element-assl.md)|Contains a provider-specific value that describes how scalar values are distributed within a column of a `MiningStructure` element.|  
|[Edition Element &#40;ASSL&#41;](edition-element-assl.md)|Contains the read-only edition of the instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] represented by the [Server](../objects/server-element-assl.md) element.|  
|[Enabled Element &#40;ASSL&#41;](enabled-element-assl.md)|Indicates whether the parent element is enabled.|  
|[EndOfData Element &#40;ASSL&#41;](../objects/data-element-assl.md)|Indicates the end of data received from a [PushedDataSource](../data-type/datasource-data-type-assl.md) element.|  
|[EstimatedCount Element &#40;ASSL&#41;](estimatedcount-element-assl.md)|Contains the estimated number of members for an attribute.|  
|[EstimatedPerformanceGain Element &#40;ASSL&#41;](estimatedperformancegain-element-assl.md)|Contains the read-only percentage of estimated performance gain for the partition.|  
|[EstimatedRows Element &#40;ASSL&#41;](estimatedrows-element-assl.md)|Contains the estimated number of rows represented by the parent element.|  
|[EstimatedSize Element &#40;ASSL&#41;](estimatedsize-element-assl.md)|Contains the read-only estimated size, in bytes, of the parent element.|  
|[EventID Element &#40;ASSL&#41;](eventid-element-assl.md)|Uniquely identifies an [Event](../objects/event-element-assl.md) element that is to be captured as part of a `Trace` element.|  
|[Expression Element &#40;ASSL&#41;](expression-element-assl.md)|Contains an MDX expression that defines the contents of the parent element.|  
|[Filter Element &#40;Binding&#41; &#40;ASSL&#41;](filter-element-binding-assl.md)|Contains an MDX expression that filters the contents of the parent element.|  
|[Filter Element &#40;Trace&#41; &#40;ASSL&#41;](filter-element-trace-assl.md)|Contains an XML document fragment that describes the `Trace` filter.|  
|[FirstDayOfWeek Element &#40;ASSL&#41;](firstdayofweek-element-assl.md)|Defines the first day of the week for a `TimeBinding` element.|  
|[FiscalFirstDayOfMonth Element &#40;ASSL&#41;](fiscalfirstdayofmonth-element-assl.md)|Defines the first day of the fiscal month for a `TimeBinding` element.|  
|[FiscalFirstMonth Element &#40;ASSL&#41;](fiscalfirstmonth-element-assl.md)|Defines the first month of the fiscal period for a `TimeBinding` element.|  
|[FiscalYearName Element &#40;ASSL&#41;](fiscalyearname-element-assl.md)|Defines the naming convention for the name of the fiscal year for a `TimeBinding` element.|  
|[FontFlags Element &#40;ASSL&#41;](fontflags-element-assl.md)|Describes font-related display characteristics of the `CalculationProperty` or `Measure` parent element.|  
|[FontName Element &#40;ASSL&#41;](fontname-element-assl.md)|Describes font-related display characteristics of the `CalculationProperty` or `Measure` parent element.|  
|[FontSize Element &#40;ASSL&#41;](fontsize-element-assl.md)|Describes font-related display characteristics of the `CalculationProperty` or `Measure` parent element.|  
|[ForceRebuildInterval Element &#40;ASSL&#41;](forcerebuildinterval-element-assl.md)|Determines the amount of time, starting when a fresh multidimensional OLAP (MOLAP) image becomes available, after which MOLAP imaging unconditionally starts.|  
|[ForeColor Element &#40;ASSL&#41;](forecolor-element-assl.md)|Describes color-related display characteristics of the `CalculationProperty` or `Measure` parent element.|  
|[Format Element &#40;ASSL&#41;](format-element-assl.md)|Contains the required format of the `DataItem` element.|  
|[FormatString Element &#40;ASSL&#41;](formatstring-element-assl.md)|Describes the display format for a `CalculationProperty` element or a `Measure` element.|  
|[Goal Element &#40;ASSL&#41;](goal-element-assl.md)|Identifies the desired goal in a `Kpi` element.|  
|[GranularityAttributeID Element &#40;ASSL&#41;](granularityattributeid-element-assl.md)|Contains the ID of the attribute associated with the parent [MeasureGroupAttributeBinding](../data-type/measuregroupattributebinding-data-type-out-of-line-assl.md) data type.|  
|[HideMemberIf Element &#40;ASSL&#41;](hidememberif-element-assl.md)|Indicates whether and when a member in a level should be hidden from client applications.|  
|[HierarchyID Element &#40;ASSL&#41;](hierarchyid-element-assl.md)|Contains the ID for a [CubeHierarchy](../data-type/hierarchy-data-type-assl.md), [MeasureGroupHierarchy](../data-type/measuregrouphierarchy-data-type-assl.md), or [PerspectiveHierarchy](../data-type/perspectivehierarchy-data-type-assl.md) element.|  
|[HierarchyUniqueNameStyle Element &#40;ASSL&#41;](hierarchyuniquenamestyle-element-assl.md)|Determines how unique names are generated for hierarchies that are contained within the `CubeDimension`.|  
|[ID Element &#40;ASSL&#41;](id-element-assl.md)|Contains the unique ID of the parent element.|  
|[IgnoreUnrelatedDimensions Element &#40;ASSL&#41;](../collections/dimensions-element-assl.md)|Determines whether unrelated dimensions are forced to their top level when members of dimensions that are unrelated to the measure group are included in a query.|  
|[ImpersonationInfo Element &#40;ASSL&#41;](impersonationinfo-element-assl.md)|Contains the information that is used to determine impersonation behavior when accessing or executing an assembly.|  
|[ImpersonationInfoSecurity Element &#40;ASSL&#41;](impersonationinfosecurity-element-assl.md)|Contains a read-only value that indicates if any changes were made to the security credentials that are supplied in the `ImpersonationInfo` data type.|  
|[ImpersonationMode Element &#40;ASSL&#41;](impersonationmode-element-assl.md)|Contains a value that indicates the method of impersonation for elements that are derived from the `ImpersonationInfo` data type.|  
|[InstanceSelection Element &#40;ASSL&#41;](instanceselection-element-assl.md)|Provides a hint to client applications to suggest how a list of items should be displayed, based on the expected number of items in the list.|  
|[IntermediateCubeDimensionID Element &#40;ASSL&#41;](intermediatecubedimensionid-element-assl.md)|Contains the ID of the dimension that relates a reference dimension to a measure group.|  
|[IntermediateGranularityAttributeID Element &#40;ASSL&#41;](intermediategranularityattributeid-element-assl.md)|Contains the ID of the granularity attribute in the intermediate cube dimension that is used to relate a reference dimension to an intermediate dimension.|  
|[InvalidXmlCharacters Element &#40;ASSL&#41;](invalidxmlcharacters-element-assl.md)|Specifies the handling method for XML characters in the source data that are not valid.|  
|[Invocation Element &#40;ASSL&#41;](invocation-element-assl.md)|Specifies how an `Action` should be invoked.|  
|[IsAggregatable Element &#40;ASSL&#41;](isaggregatable-element-assl.md)|Specifies whether the values of the [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md) element can be aggregated.|  
|[IsKey Element &#40;ASSL&#41;](iskey-element-assl.md)|Indicates whether the column provides the key for the case in a `MiningStructure` element.|  
|[Isolation Element &#40;ASSL&#41;](isolation-element-assl.md)|Indicates the isolation level for an element that is derived from the [DataSource](../data-type/datasource-data-type-assl.md) data type.|  
|[KeyDuplicate Element &#40;ASSL&#41;](keyduplicate-element-assl.md)|Determines how [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] handles a duplicate key error if it encounters one during processing.|  
|[KeyErrorAction Element &#40;ASSL&#41;](keyerroraction-element-assl.md)|Specifies the action for [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] to take when an error occurs on a key.|  
|[KeyErrorLimit Element &#40;ASSL&#41;](keyerrorlimit-element-assl.md)|Contains the number of acceptable errors during processing.|  
|[KeyErrorLimitAction Element &#40;ASSL&#41;](keyerrorlimitaction-element-assl.md)|Specifies the action that [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] takes when the key error count that is specified in the [KeyErrorLimit](keyerrorlimit-element-assl.md) element is reached.|  
|[KeyErrorLogFile Element &#40;ASSL&#41;](../objects/file-element-assl.md)|Contains the file name for logging processing errors.|  
|[KeyNotFound Element &#40;ASSL&#41;](keynotfound-element-assl.md)|Specifies how [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] responds when it encounters a referential integrity error.|  
|[KeyUniquenessGuarantee Element &#40;ASSL&#41;](keyuniquenessguarantee-element-assl.md)|Indicates whether the relationship between the attribute key and its name, and the relationship to related attributes, is guaranteed to be valid.|  
|[KpiID Element &#40;ASSL&#41;](kpiid-element-assl.md)|Contains an ID that associates a `Kpi` element with a `Perspective` element.|  
|[Language Element &#40;ASSL&#41;](language-element-assl.md)|Contains the language identifier of the parent element.|  
|[LastProcessed Element &#40;ASSL&#41;](lastprocessed-element-assl.md)|Contains the read-only timestamp that indicates when the database that contains the parent element was last processed.|  
|[LastSchemaUpdate Element &#40;ASSL&#41;](lastschemaupdate-element-assl.md)|Contains the read-only metadata update timestamp of the parent element.|  
|[LastUpdate Element &#40;ASSL&#41;](lastupdate-element-assl.md)|Contains a read-only timestamp that indicates the last time that the associated `Database` or any of the major objects that the database contains were altered.|  
|[Latency Element &#40;ASSL&#41;](latency-element-assl.md)|Defines the "grace period" between the earliest notification and the moment when the MOLAP images are destroyed.|  
|[LogFileAppend Element &#40;ASSL&#41;](logfileappend-element-assl.md)|Determines whether the `Trace` element appends its logging output to the existing log file, or overwrites it.|  
|[LogFileName Element &#40;ASSL&#41;](logfilename-element-assl.md)|Contains the file name of the log file for the `Trace` element.|  
|[LogFileRollover Element &#40;ASSL&#41;](logfilerollover-element-assl.md)|Specifies whether logging of `Trace` output should roll over to a new file or should stop when the maximum log file size that is specified in [LogFileSize](logfilesize-element-assl.md) is reached.|  
|[LogFileSize Element &#40;ASSL&#41;](logfilesize-element-assl.md)|Specifies the maximum log file size, in megabytes.|  
|[ManagedProvider Element &#40;ASSL&#41;](managedprovider-element-assl.md)|Contains the name of the managed provider that is used by an element that is derived from the `DataSource` data type.|  
|[ManufacturingExtraMonthQuarter Element &#40;ASSL&#41;](manufacturingextramonthquarter-element-assl.md)|Defines the month of the manufacturing period to which an extra month is assigned for a `TimeBinding` element.|  
|[ManufacturingFirstMonth Element &#40;ASSL&#41;](manufacturingfirstmonth-element-assl.md)|Defines the first manufacturing month for a `TimeBinding` element.|  
|[ManufacturingFirstWeekOfMonth Element &#40;ASSL&#41;](manufacturingfirstweekofmonth-element-assl.md)|Defines the first week of the manufacturing month for a `TimeBinding` element.|  
|[MasterDatasourceID Element &#40;ASSL&#41;](masterdatasourceid-element-assl.md)|Contains the master data source ID for a `Database` element.|  
|[Materialization Element &#40;ASSL&#41;](materialization-element-assl.md)|Indicates the type of relationship between the measure group and the reference dimension.|  
|[MaxActiveConnections Element &#40;ASSL&#41;](maxactiveconnections-element-assl.md)|Contains the maximum number of concurrent connections allowed by an element that is derived from the `DataSource` data type.|  
|[MdxMissingMemberMode Element &#40;ASSL&#41;](mdxmissingmembermode-element-assl.md)|Determines how missing members are handled for MDX statements.|  
|[MeasureExpression Element &#40;ASSL&#41;](measureexpression-element-assl.md)|Contains the MDX expression that defines a measure.|  
|[MeasureGroupID Element &#40;ASSL&#41;](measuregroupid-element-assl.md)|Associates a `MeasureGroup` with the parent element, binding, or out-of-line binding.|  
|[MeasureID Element &#40;ASSL&#41;](measureid-element-assl.md)|Associates a `Measure` element with the parent element.|  
|[MeasureQualificaton Element &#40;ASSL&#41;](measurequalificaton-element-assl.md)|Determines whether a prefix is applied to measures in the `MeasureGroup`.|  
|[MemberNamesUnique Element &#40;ASSL&#41;](membernamesunique-element-assl.md)|Determines whether member names under the parent element must be unique.|  
|[MemberUniqueNameStyle Element &#40;ASSL&#41;](memberuniquenamestyle-element-assl.md)|Determines how unique names are generated for members of hierarchies contained within the `CubeDimension` element.|  
|[MembersWithData Element &#40;ASSL&#41;](memberswithdata-element-assl.md)|Determines whether to display data members for non-leaf members in the parent attribute.|  
|[MembersWithDataCaption Element &#40;ASSL&#41;](memberswithdatacaption-element-assl.md)|Provides a template string that is used to create captions for system-generated data members.|  
|[MimeType Element &#40;ASSL&#41;](mimetype-element-assl.md)|Contains the Multipurpose Internet Mail Extensions (MIME) type, if applicable, of the data represented by the parent `DataItem` element.|  
|[MiningModelID Element &#40;ASSL&#41;](miningmodelid-element-assl.md)|Associates a mining model with a data mining dimension.|  
|[Name Element &#40;ASSL&#41;](name-element-assl.md)|Contains the name of the parent element.|  
|[NamingTemplate Element &#40;ASSL&#41;](namingtemplate-element-assl.md)|Defines how levels are named in a parent-child hierarchy constructed from the `DimensionAttribute` parent element.|  
|[NonEmptyBehavior Element &#40;ASSL&#41;](nonemptybehavior-element-assl.md)|Determines the non-empty behavior associated with the parent of the `CalculationProperty` element.|  
|[NotificationTechnique Element &#40;ASSL&#41;](notificationtechnique-element-assl.md)|Specifies whether [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or an external client application processes the notifications.|  
|[NullKeyConvertedToUnknown Element &#40;ASSL&#41;](nullkeyconvertedtounknown-element-assl.md)|Specifies the action to be taken when a null conversion error is encountered.|  
|[NullKeyNotAllowed Element &#40;ASSL&#41;](nullkeynotallowed-element-assl.md)|Determines how the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] processing engine handles a null key error encountered during processing.|  
|[NullProcessing Element &#40;ASSL&#41;](nullprocessing-element-assl.md)|Defines how null values are processed.|  
|[OnlineMode Element &#40;ASSL&#41;](onlinemode-element-assl.md)|Specifies whether the database is brought back online immediately when the rebuilding of the cache is initiated, or only when the rebuilding of the cache is complete.|  
|[OptimizedState Element &#40;ASSL&#41;](state-element-assl.md)|Determines the level of optimization that is applied to the hierarchy.|  
|[Optionality Element &#40;ASSL&#41;](optionality-element-assl.md)|Indicates the optionality of the members for an `AttributeRelationship` element.|  
|[OrderBy Element &#40;ASSL&#41;](orderby-element-assl.md)|Describes how to order the members contained in the attribute.|  
|[OrderByAttributeID Element &#40;ASSL&#41;](orderbyattributeid-element-assl.md)|Identifies another attribute by which to order the members of the [Dimension](../data-type/dimensionattribute-data-type-assl.md) attribute.|  
|[Ordinal Element &#40;ASSL&#41;](ordinal-element-assl.md)|Indicates the ordinal number to bind to in collections such as keys and translations.|  
|[OverrideBehavior Element &#40;ASSL&#41;](overridebehavior-element-assl.md)|Indicates the override behavior of the relationship described by an `AttributeRelationship` element.|  
|[PartitionID Element &#40;ASSL&#41;](partitionid-element-assl.md)|Associates a `Partition` element with a parent element, binding, or out-of-line binding.|  
|[Password Element &#40;ASSL&#41;](password-element-assl.md)|Contains the password of the user account for the `ImpersonationInfo` element.|  
|[Path Element &#40;ASSL&#41;](path-element-assl.md)|Contains the path, as provided by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], of a report used by the [ReportAction](../data-type/reportaction-data-type-assl.md) element.|  
|[PendingValue Element &#40;ASSL&#41;](pendingvalue-element-assl.md)|Contains the read-only pending value of the associated `ServerProperty` element.|  
|[PermissionSet Element &#40;ASSL&#41;](permissionset-element-assl.md)|Identifies the permission set associated with a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework assembly.|  
|[Persistence Element &#40;ASSL&#41;](persistence-element-assl.md)|Determines which parts of the bound source data are dynamic and are checked for updates using the frequency specified by the [RefreshPolicy](refreshpolicy-element-assl.md) element.|  
|[Process Element &#40;ASSL&#41;](process-element-assl.md)|Determines whether a user can process the owner of the parent element.|  
|[ProcessingMode Element &#40;ASSL&#41;](processingmode-element-assl.md)|Indicates whether the instance should index and aggregate during or after processing.|  
|[ProcessingPriority Element &#40;ASSL&#41;](processingpriority-element-assl.md)|Determines the processing priority of the parent object during background operations, for example lazy aggregation, indexing, or clustering.|  
|[ProcessingQuery Element &#40;ASSL&#41;](query-element-assl.md)|Contains the parameterized text of the query to execute for notification of incremental processing status.|  
|[ProductName Element &#40;ASSL&#41;](productname-element-assl.md)|Contains the read-only product name of the instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that is associated with a `Server` element.|  
|[Query Element &#40;ASSL&#41;](query-element-assl.md)|Contains the text of the query to execute for the notification.|  
|[QueryDefinition Element &#40;ASSL&#41;](querydefinition-element-assl.md)|Contains an opaque expression for a query associated with a `DataSource` element in a [QueryBinding](../data-type/querybinding-data-type-assl.md) element.|  
|[Read Element &#40;ASSL&#41;](read-element-assl.md)|Determines whether data or metadata can be read for a given [CubeDimensionPermission](../data-type/permission-data-type-assl.md) or [Permission](../data-type/permission-data-type-assl.md) element.|  
|[ReadDefinition Element &#40;ASSL&#41;](readdefinition-element-assl.md)|Determines whether members can read the definition of the database or the definition of objects in the database.|  
|[ReadSourceData Element &#40;ASSL&#41;](readsourcedata-element-assl.md)|Determines how unique names are generated for hierarchies that are contained within the `CubePermission`.|  
|[RefreshInterval Element &#40;ASSL&#41;](refreshinterval-element-assl.md)|Specifies the interval at which the bound data associated with the parent element is refreshed.|  
|[RefreshPolicy Element &#40;ASSL&#41;](refreshpolicy-element-assl.md)|Determines how often the dynamic part of the dimension or measure group (as specified by the [Persistence](persistence-element-assl.md) element) is checked for changes.|  
|[RelationshipType Element &#40;ASSL&#41;](relationshiptype-element-assl.md)|Indicates whether member relationships for an `AttributeRelationship` can be changed.|  
|[RemoteDatasourceID Element &#40;ASSL&#41;](remotedatasourceid-element-assl.md)|Specifies the ID of the OLAP data source that points to the instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that stores the remote partition.|  
|[ReportingFirstMonth Element &#40;ASSL&#41;](reportingfirstmonth-element-assl.md)|Defines the first reporting month for the `TimeBinding` element.|  
|[ReportingFirstWeekOfMonth Element &#40;ASSL&#41;](reportingfirstweekofmonth-element-assl.md)|Defines the first week of the reporting month for the `TimeBinding` element.|  
|[ReportingWeekToMonthPattern Element &#40;ASSL&#41;](reportingweektomonthpattern-element-assl.md)|Defines the reporting week-to-month pattern for the `TimeBinding` element.|  
|[ReportServer Element &#40;ASSL&#41;](reportserver-element-assl.md)|Contains the name of the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] instance that is used by the `ReportAction`.|  
|[RequiresRestart Element &#40;ASSL&#41;](requiresrestart-element-assl.md)|Contains a read-only value associated with a `ServerProperty` element that determines whether changing the value of the server property requires that the instance be restarted for the change to take effect.|  
|[RoleID Element &#40;ASSL&#41;](roleid-element-assl.md)|Identifies the role for which permissions are being defined.|  
|[Root Element &#40;ASSL&#41;](root-element-assl.md)|Contains the data (rowset) for a data source.|  
|[RootMemberIf Element &#40;ASSL&#41;](rootmemberif-element-assl.md)|Determines how the root member or members of a parent attribute are identified.|  
|[Schema Element &#40;ASSL&#41;](schema-element-assl.md)|Contains the schema of the data source view.|  
|[ScriptCacheProcessingMode Element &#40;ASSL&#41;](scriptcacheprocessingmode-element-assl.md)|Indicates whether the server should build the script cache during processing or after processing.|  
|[SilenceInterval Element &#40;ASSL&#41;](silenceinterval-element-assl.md)|Defines the minimum amount of time the instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] pauses before starting the MOLAP imaging process.|  
|[SilenceOverrideInterval Element &#40;ASSL&#41;](silenceoverrideinterval-element-assl.md)|Defines the amount of time that must elapse after receiving initial notification before MOLAP imaging begins unconditionally.|  
|[Slice Element &#40;ASSL&#41;](slice-element-assl.md)|Contains an MDX expression defining the slice contained in a partition.|  
|[SolveOrder Element &#40;ASSL&#41;](solveorder-element-assl.md)|Indicates the solve order in which the `CalculationProperty` element is applied to a calculated member or calculated cell definition.|  
|[Source Element &#40;Binding&#41; &#40;ASSL&#41;](source-element-binding-assl.md)|Identifies the source of data to which the parent element is bound.|  
|[Source Element &#40;ComAssembly&#41; &#40;ASSL&#41;](source-element-comassembly-assl.md)|Contains the file name or programmatic identifier (ProgID) for a Component Object Model (COM) component.|  
|[Source Element &#40;Measure&#41; &#40;ASSL&#41;](source-element-measure-assl.md)|Contains the details of the source containing the value of the `Measure` element.|  
|[SourceAttributeID Element &#40;ASSL&#41;](sourceattributeid-element-assl.md)|Contains the ID of the source attribute on which the [Level](../objects/level-element-assl.md) element is based.|  
|[SourceColumnID Element &#40;ASSL&#41;](sourcecolumnid-element-assl.md)|Contains the ID of the source mining structure column in the ancestor `MiningStructure` element.|  
|[State Element &#40;ASSL&#41;](state-element-assl.md)|Contains a read-only value that describes the current processing state of the parent element.|  
|[Status Element &#40;ASSL&#41;](status-element-assl.md)|Contains an MDX expression that returns a status indicator for a `Kpi` element.|  
|[StatusGraphic Element &#40;ASSL&#41;](statusgraphic-element-assl.md)|Contains the recommended graphical representation of the status of the `Kpi` element.|  
|[StopTime Element &#40;ASSL&#41;](stoptime-element-assl.md)|Specifies the date and time at which a `Trace` element should stop.|  
|[StorageLocation Element &#40;ASSL&#41;](storagelocation-element-assl.md)|Contains the file system storage location for the contents of the parent element.|  
|[StorageMode Element &#40;ASSL&#41;](storagemode-element-assl.md)|Determines the storage mode for the parent element.|  
|[TableID Element &#40;ASSL&#41;](tableid-element-assl.md)|Contains the ID of the table (from the `DataSourceView` element) associated with the parent element.|  
|[Target Element &#40;ASSL&#41;](target-element-assl.md)|Identifies the target of the `Action` element.|  
|[TargetType Element &#40;ASSL&#41;](targettype-element-assl.md)|Identifies the item type of the item identified in the [Target](target-element-assl.md) element.|  
|[Text Element &#40;ASSL&#41;](text-element-assl.md)|Contains the text of a [Command](../objects/command-element-assl.md) element.|  
|[Timeout Element &#40;ASSL&#41;](timeout-element-assl.md)|Specifies the time, in seconds, after which an attempt to retrieve data reports a timeout.|  
|[Trend Element &#40;ASSL&#41;](trend-element-assl.md)|Contains an MDX expression that returns a trend indicator for a `Kpi` element.|  
|[TrendGraphic Element &#40;ASSL&#41;](trendgraphic-element-assl.md)|Contains the recommended graphical representation of the trend of the `Kpi` element.|  
|[Trimming Element &#40;ASSL&#41;](trimming-element-assl.md)|Specifies how data from the data source is trimmed.|  
|[Type Element &#40;Action&#41; &#40;ASSL&#41;](type-element-action-assl.md)|Contains the type of the `Action` element.|  
|[Type Element &#40;Binding&#41; &#40;ASSL&#41;](type-element-binding-assl.md)|Contains the type of the attribute binding.|  
|[Type Element &#40;ClrAssemblyFile&#41; &#40;ASSL&#41;](type-element-clrassemblyfile-assl.md)|Specifies the file type of one of the files that belong to a .NET Framework assembly.|  
|[Type Element &#40;Dimension&#41; &#40;ASSL&#41;](type-element-dimension-assl.md)|Provides information about the contents of the dimension.|  
|[Type Element &#40;DimensionAttribute&#41; &#40;ASSL&#41;](type-element-dimensionattribute-assl.md)|Contains the type of the attribute.|  
|[Type Element &#40;MeasureGroup&#41; &#40;ASSL&#41;](type-element-measuregroup-assl.md)|Specifies the type of the `MeasureGroup`.|  
|[Type Element &#40;MeasureGroupAttribute&#41; &#40;ASSL&#41;](type-element-measuregroupattribute-assl.md)|Contains the type of a [MeasureGroupAttribute](../data-type/measuregroupattribute-data-type-assl.md) element.|  
|[Type Element &#40;MiningStructureColumn&#41; &#40;ASSL&#41;](type-element-miningstructurecolumn-assl.md)|Contains the type of the [MiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) element.|  
|[Type Element &#40;Partition&#41; &#40;ASSL&#41;](type-element-partition-assl.md)|Contains the type of the `Partition` element.|  
|[Type Element &#40;PerspectiveCalculation&#41; &#40;ASSL&#41;](type-element-perspectivecalculation-assl.md)|Indicates the type of the [PerspectiveCalculation](../data-type/perspectivecalculation-data-type-assl.md) element.|  
|[UnknownMember Element &#40;ASSL&#41;](unknownmember-element-assl.md)|Indicates whether the unknown member is visible.|  
|[UnknownMemberName Element &#40;ASSL&#41;](unknownmembername-element-assl.md)|Contains the caption, in the default language of the dimension, for the unknown member of the dimension.|  
|[Usage Element &#40;DimensionAttribute&#41; &#40;ASSL&#41;](usage-element-dimensionattribute-assl.md)|Describes how an attribute is used.|  
|[Usage Element &#40;MiningModelColumn&#41; &#40;ASSL&#41;](usage-element-miningmodelcolumn-assl.md)|Describes how the associated column in the parent `MiningStructure` is used.|  
|[Value Element &#40;ASSL&#41;](value-element-assl.md)|Contains the value of the parent element.|  
|[Version Element &#40;ASSL&#41;](version-element-assl.md)|Contains the read-only version number of the instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] represented by the `Server` element.|  
|[Visibility Element &#40;ASSL&#41;](visibility-element-assl.md)|Defines the visibility of an [Annotation](../objects/annotation-element-assl.md) element.|  
|[Visible Element &#40;ASSL&#41;](visible-element-assl.md)|Determines the visibility of the parent element.|  
|[VisualTotals Element &#40;ASSL&#41;](visualtotals-element-assl.md)|Contains an MDX expression that determines whether visual totals are displayed for members of this attribute.|  
|[Write Element &#40;ASSL&#41;](write-element-assl.md)|Determines whether data or metadata can be written for a given `CubeDimensionPermission` or `Permission` element.|  
|[WriteEnabled Element &#40;ASSL&#41;](writeenabled-element-assl.md)|Indicates whether dimension writebacks are available (subject to security permissions).|  
  
## See Also  
 [Analysis Services Scripting Language XML Element Hierarchy &#40;ASSL&#41;](../analysis-services-scripting-language-xml-element-hierarchy-assl.md)  
  
  
