---
title: "Common Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "external metadata column properties [Integration Services]"
  - "output properties [Integration Services]"
  - "data types [Integration Services], properties"
  - "input properties [Integration Services]"
  - "component properties [Integration Services]"
ms.assetid: 51973502-5cc6-4125-9fce-e60fa1b7b796
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Common Properties
  The data flow objects in the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model have common properties and custom properties at the component, input and output, and input column and output column levels. Many properties have read-only values that are assigned at run time by the data flow engine.  
  
 This topic lists and describes the common properties of data flow objects.  
  
-   [Components](#components)  
  
-   [Inputs](#inputs)  
  
-   [Input columns](#inputcolumns)  
  
-   [Outputs](#outputs)  
  
-   [Output columns](#outputcolumns)  
  
 For information about customer properties, see the following topics  
  
-   [ADO NET Custom Properties](data-flow/ado-net-custom-properties.md)  
  
-   [CDC Control Task Custom Properties](control-flow/cdc-control-task-custom-properties.md)  
  
-   [CDC Source Custom Properties](data-flow/cdc-source-custom-properties.md)  
  
-   [Data Mining Model Training Destination Custom Properties](data-flow/data-mining-model-training-destination-custom-properties.md)  
  
-   [DataReader Destination Custom Properties](data-flow/datareader-destination-custom-properties.md)  
  
-   [Dimension Processing Destination Custom Properies](data-flow/dimension-processing-destination-custom-properies.md)  
  
-   [Excel Custom Properties](data-flow/excel-custom-properties.md)  
  
-   [Flat File Custom Properties](data-flow/flat-file-custom-properties.md)  
  
-   [ODBC Destination Custom Properties](data-flow/odbc-destination-custom-properties.md)  
  
-   [ODBC Source Custom Properties](data-flow/odbc-source-custom-properties.md)  
  
-   [OLE DB Custom Properties](data-flow/ole-db-custom-properties.md)OLE DB Custom Properties  
  
-   [Partition Processing Destination Custom Properties](data-flow/partition-processing-destination-custom-properties.md)  
  
-   [Raw File Custom Properties](data-flow/raw-file-custom-properties.md)  
  
-   [Recordset Destination Custom Properties](data-flow/recordset-destination-custom-properties.md)  
  
-   [SQL Server Compact Edition Destination Custom Properties](data-flow/sql-server-compact-edition-destination-custom-properties.md)  
  
-   [SQL Server Destination Custom Properties](data-flow/sql-server-destination-custom-properties.md)  
  
-   [Transformation Custom Properties](data-flow/transformations/transformation-custom-properties.md)  
  
-   [XML Source Custom Properties](data-flow/xml-source-custom-properties.md)  
  
##  <a name="components"></a> Component Properties  
 In the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model, a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface.  
  
 The following table describes the properties of the components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ComponentClassID|String|The CLSID of the component.|  
|ContactInfo|String|Contact information for the developer of a component.|  
|Description|String|The description of the data flow component. The default value of this property is the name of the data flow component.|  
|ID|Integer|A value that uniquely identifies this instance of the component.|  
|IdentificationString|String|Identifies the component.|  
|IsDefaultLocale|Boolean|Indicates whether the component uses the locale of the Data Flow task to which it belongs.|  
|LocaleID|Integer|The locale that the data flow component uses when the package runs. All Windows locales are available for use in data flow components.|  
|Name|String|The name of the data flow component.|  
|PipelineVersion|Integer|The version of the data flow task within which a component is designed to execute.|  
|UsesDispositions|Boolean|Indicates whether a component has an error output.|  
|ValidateExternalMetadata|Boolean|Indicates whether the metadata of external columns is validated. The default value of this property is `True`.|  
|Version|Integer|The version of a component.|  
  
##  <a name="inputs"></a> Input Properties  
 In the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model, transformations and destinations have inputs. An input of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100> interface.  
  
 The following table describes the properties of the inputs of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|Description|String|The description of the input.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are `Fail component`, `Ignore failure`, and `Redirect row`.|  
|HasSideEffects|Boolean|Indicates whether a component can be removed from the execution plan of the data flow when it is not attached to a downstream component and when `RunInOptimizedMode` is `true`.|  
|ID|Integer|A value that uniquely identifies the input.|  
|IdentificationString|String|A string that identifies the input.|  
|IsSorted|Boolean|Indicates whether the data in the input is sorted.|  
|Name|String|The name of the input.|  
|SourceLocale|Integer|The locale ID (LCID) of the input data.|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. . The values are `Fail component`, `Ignore failure`, and `Redirect row`.|  
  
 Destinations and some transformations do not support error outputs, and the ErrorRowDisposition and TruncationRowDisposition properties of these components are read-only.  
  
###  <a name="inputcolumns"></a> Input Column Properties  
 In the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model, an input contains a collection of input columns. An input column of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100> interface.  
  
 The following table describes the properties of the input columns of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ComparisonFlags|Integer|A set of flags that specify the comparison of columns that have a character data type. For more information, see [Comparing String Data](data-flow/comparing-string-data.md).|  
|Description|String|Describes the input column.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are `Fail component`, `Ignore failure`, and `Redirect row`.|  
|ExternalMetadataColumnID|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100>|The ID of the external metadata column assigned to an input column.|  
|ID|Integer|A value that uniquely identifies the input column.|  
|IdentificationString|String|A string that identifies the input column.|  
|LineageID|Integer|The ID of the upstream column.|  
|Name|String|The name of the input column.|  
|SortKeyPosition|Integer|A value that indicates whether a column is sorted, its sort order, and the sequence in which multiple columns are sorted. The value **0** indicates the column is not sorted.  For more information, see [Sort Data for the Merge and Merge Join Transformations](data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. The values are `Fail component`, `Ignore failure`, and `Redirect row`.|  
|UpstreamComponentName|String|The name of the upstream component.|  
|UsageType|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSUsageType>|A value that determines how an input column is used by the component.|  
  
 Input columns also have the data type properties described under "Data Type Properties."  
  
##  <a name="outputs"></a> Output Properties  
 In the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model, sources and transformations have outputs. An output of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100> interface.  
  
 The following table describes the properties of the outputs of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|DeleteOutputOnPathDetached|Boolean|A value that determines whether the data flow engine deletes the output when it is detached from a path.|  
|Description|String|Describes the output.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are `Fail component`, `Ignore failure`, and `Redirect row`.|  
|ExclusionGroup|Integer|A value that identifies a group of mutually exclusive outputs.|  
|HasSideEffects|Boolean|A value that indicates whether a component can be removed from the execution plan of the data flow when it is not attached to an upstream component and when `RunInOptimizedMode` is `true`.|  
|ID|Integer|A value that uniquely identifies the output.|  
|IdentificationString|String|A string that identifies the output.|  
|IsErrorOut|Boolean|Indicates whether the output is an error output.|  
|IsSorted|Boolean|Indicates whether the output is sorted. The default value is `False`.<br /><br /> **\*\* Important \*\*** Setting the value of the `IsSorted` property to `True` does not sort the data. This property only provides a hint to downstream components that the data has been previously sorted. For more information, see [Sort Data for the Merge and Merge Join Transformations](data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).|  
|Name|String|The name of the output.|  
|SynchronousInputID|Integer|The ID of an input that is synchronous to the output.|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. The values are `Fail component`, `Ignore failure`, and `Redirect row`.|  
  
###  <a name="outputcolumns"></a> Output Column Properties  
 In the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model, an output contains a collection of output columns. An output column of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100> interface.  
  
 The following table describes the properties of the output columns of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ComparisonFlags|Integer|A set of flags that specify the comparison of columns that have a character data type. For more information, see [Comparing String Data](data-flow/comparing-string-data.md).|  
|Description|String|Describes the output column.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are `Fail component`, `Ignore failure`, and `Redirect row`. The default value is `Fail component`.|  
|ExternalMetadataColumnID|Integer|The ID of the external metadata column assigned to an input column.|  
|ID|Integer|A value that uniquely identifies the output column.|  
|IdentificationString|String|A string that identifies the output column.|  
|LineageID|Integer|The ID of the output column. Downstream components refer to the column by using this value.|  
|Name|String|The name of the output column.|  
|SortKeyPosition|Integer|A value that indicates whether a column is sorted, its sort order, and the sequence in which multiple columns are sorted. The value **0** indicates the column is not sorted. For more information, see [Sort Data for the Merge and Merge Join Transformations](data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).|  
|SpecialFlags|Integer|A value that contains the special flags of the output column.|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. The values are `Fail component`, `Ignore failure`, and `Redirect row`. The default value is `Fail component`.|  
  
 Output columns also include a set of data type properties.  
  
## External Metadata Column Properties  
 In the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model, inputs and outputs can contain a collection of external metadata columns. An external metadata column of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100> interface.  
  
 The following table describes the properties of the external metadata columns of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|Description|String|Describes the external column.|  
|ID|Integer|A value that uniquely identifies the column.|  
|IdentificationString|String|A string that identifies the column.|  
|Name|String|The name of the external column.|  
  
 External metadata columns also include a set of data type properties.  
  
## Data Type Properties  
 Output columns and external metadata columns include a set of data type properties. Depending on the data type of the column, properties can be read/write or read-only.  
  
 The following table describes the data type properties of output columns and external metadata columns.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|CodePage|Integer|Specifies the code page for string data that is not Unicode.|  
|DataType|Integer (enumeration)|The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] data type of the column. For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).|  
|Length|Integer|The length, measured in characters, of a column.|  
|Precision|Integer|The precision of a numeric column.|  
|Scale|Integer|The scale of a numeric column.|  
  
## See Also  
 [Data Flow](data-flow/data-flow.md)   
 [Transformation Custom Properties](data-flow/transformations/transformation-custom-properties.md)   
 [Path Properties](../../2014/integration-services/path-properties.md)   
 [Data Flow Properties that Can Be Set by Using Expressions](../../2014/integration-services/data-flow-properties-that-can-be-set-by-using-expressions.md)  
  
  
