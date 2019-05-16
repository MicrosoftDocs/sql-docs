---
title: "Set the Properties of a Data Flow Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "components [Integration Services], properties"
ms.assetid: 73000ef6-52a2-4dec-8320-0e79acf0c2c5
author: janinezhang
ms.author: janinez
manager: craigg
---
# Set the Properties of a Data Flow Component

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  To set the properties of data flow components, which include sources, destinations, and transformations, use one of the following features:  
  
-   The component editors that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides. These editors include only the custom properties of each data flow component.  
  
-   The **Properties** window lists the component-level custom properties of each element, as well as the properties common to all data flow elements.  
  
-   The **Advanced Editor** dialog box provides access to custom properties for each component. The **Advanced Editor** dialog box also provides access to the properties common to all data flow components-the properties of inputs, outputs, error outputs, columns, and external columns.  
  
## Set the properties of a data flow component with a component editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the data flow with the component whose properties you want to view and modify.  
  
4.  Double-click the data flow component.  
  
5.  In the component editor, view or modify the property values, and then close the editor.  
  
6.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## Set the properties of a data flow component in the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the component whose properties you want to view and modify.  
  
4.  Right-click the data flow component, and then click **Properties**.  
  
5.  View or modify the property values, and then close the **Properties** window.  
  
    > [!NOTE]  
    >  Many properties are read-only, and cannot be modified.  
  
6.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## Set the properties of a data flow component with the Advanced Editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the component you want to view or modify.  
  
4.  In the data flow designer, right-click the data flow component, and then click **Show Advanced Editor**.  
  
    > [!NOTE]  
    >  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], data flow components that support multiple inputs cannot use the **Advanced Editor**.  
  
5.  In the **Advanced Editor** dialog box, do any of the following steps:  
  
    -   To view and specify the connection that the component uses, click the **Connection Managers** tab.  
  
        > [!NOTE]  
        >  The **Connection Managers** tab is available only to data flow components that use connection managers to connect to data sources such as files and databases  
  
    -   To view and modify component-level properties, click the **Component Properties** tab.  
  
    -   To view and modify mappings between external columns and the available output, click the **Column Mappings** tab.  
  
        > [!NOTE]  
        >  The **Column Mappings** tab is available only when viewing or editing sources or destinations.  
  
    -   To view a list of the available input columns and to update the names of output columns, click the **Input Columns** tab.  
  
        > [!NOTE]  
        >  The Input Columns tab is available only when working with transformations or destinations. For more information, see [Integration Services Transformations](../../integration-services/data-flow/transformations/integration-services-transformations.md).  
  
    -   To view and modify the properties of inputs, outputs, and error outputs, and the properties of the columns they contain, click the **Input and Output Properties** tab.  
  
        > [!NOTE]  
        >  Sources have no inputs. Destinations have no outputs, except for an optional error output.  
  
6.  View or modify the property values.  
  
7.  Click **OK**.  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  

## Common properties of data flow components
The data flow objects in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model have common properties and custom properties at the component, input and output, and input column and output column levels. Many properties have read-only values that are assigned at run time by the data flow engine.  
  
 This topic lists and describes the common properties of data flow objects.  
  
-   [Components](#components)  
  
-   [Inputs](#inputs)  
  
-   [Input columns](#inputcolumns)  
  
-   [Outputs](#outputs)  
  
-   [Output columns](#outputcolumns)  
  
 
###  <a name="components"></a> Component properties  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface.  
  
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
|ValidateExternalMetadata|Boolean|Indicates whether the metadata of external columns is validated. The default value of this property is **True**.|  
|Version|Integer|The version of a component.|  
  
###  <a name="inputs"></a> Input properties  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, transformations and destinations have inputs. An input of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100> interface.  
  
 The following table describes the properties of the inputs of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|Description|String|The description of the input.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are **Fail component**, **Ignore failure**, and **Redirect row**.|  
|HasSideEffects|Boolean|Indicates whether a component can be removed from the execution plan of the data flow when it is not attached to a downstream component and when **RunInOptimizedMode** is **true**.|  
|ID|Integer|A value that uniquely identifies the input.|  
|IdentificationString|String|A string that identifies the input.|  
|IsSorted|Boolean|Indicates whether the data in the input is sorted.|  
|Name|String|The name of the input.|  
|SourceLocale|Integer|The locale ID (LCID) of the input data.|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. . The values are **Fail component**, **Ignore failure**, and **Redirect row**.|  
  
 Destinations and some transformations do not support error outputs, and the ErrorRowDisposition and TruncationRowDisposition properties of these components are read-only.  
  
###  <a name="inputcolumns"></a> Input column properties  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, an input contains a collection of input columns. An input column of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100> interface.  
  
 The following table describes the properties of the input columns of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ComparisonFlags|Integer|A set of flags that specify the comparison of columns that have a character data type. For more information, see [Comparing String Data](../../integration-services/data-flow/comparing-string-data.md).|  
|Description|String|Describes the input column.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are **Fail component**, **Ignore failure**, and **Redirect row**.|  
|ExternalMetadataColumnID|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100>|The ID of the external metadata column assigned to an input column.|  
|ID|Integer|A value that uniquely identifies the input column.|  
|IdentificationString|String|A string that identifies the input column.|  
|LineageID|Integer|The ID of the upstream column.|  
|LineageIdentificationString|String|The identification string which includes the name of the upstream column.|  
|Name|String|The name of the input column.|  
|SortKeyPosition|Integer|A value that indicates whether a column is sorted, its sort order, and the sequence in which multiple columns are sorted. The value **0** indicates the column is not sorted.  For more information, see [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. The values are **Fail component**, **Ignore failure**, and **Redirect row**.|  
|UpstreamComponentName|String|The name of the upstream component.|  
|UsageType|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSUsageType>|A value that determines how an input column is used by the component.|  
  
 Input columns also have the data type properties described under "Data Type Properties."  
  
###  <a name="outputs"></a> Output properties  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, sources and transformations have outputs. An output of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100> interface.  
  
 The following table describes the properties of the outputs of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|DeleteOutputOnPathDetached|Boolean|A value that determines whether the data flow engine deletes the output when it is detached from a path.|  
|Description|String|Describes the output.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are **Fail component**, **Ignore failure**, and **Redirect row**.|  
|ExclusionGroup|Integer|A value that identifies a group of mutually exclusive outputs.|  
|HasSideEffects|Boolean|A value that indicates whether a component can be removed from the execution plan of the data flow when it is not attached to an upstream component and when **RunInOptimizedMode** is **true**.|  
|ID|Integer|A value that uniquely identifies the output.|  
|IdentificationString|String|A string that identifies the output.|  
|IsErrorOut|Boolean|Indicates whether the output is an error output.|  
|IsSorted|Boolean|Indicates whether the output is sorted. The default value is **False**.<br /><br /> **\*\* Important \*\*** Setting the value of the **IsSorted** property to **True** does not sort the data. This property only provides a hint to downstream components that the data has been previously sorted. For more information, see [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).|  
|Name|String|The name of the output.|  
|SynchronousInputID|Integer|The ID of an input that is synchronous to the output.|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. The values are **Fail component**, **Ignore failure**, and **Redirect row**.|  
  
###  <a name="outputcolumns"></a> Output column properties  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, an output contains a collection of output columns. An output column of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100> interface.  
  
 The following table describes the properties of the output columns of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ComparisonFlags|Integer|A set of flags that specify the comparison of columns that have a character data type. For more information, see [Comparing String Data](../../integration-services/data-flow/comparing-string-data.md).|  
|Description|String|Describes the output column.|  
|ErrorOrTruncationOperation|String|An optional string that specifies the types of errors or truncations that can occur when processing a row.|  
|ErrorRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that specifies the handling of errors. The values are **Fail component**, **Ignore failure**, and **Redirect row**. The default value is **Fail component**.|  
|ExternalMetadataColumnID|Integer|The ID of the external metadata column assigned to an input column.|  
|ID|Integer|A value that uniquely identifies the output column.|  
|IdentificationString|String|A string that identifies the output column.|  
|LineageID|Integer|The ID of the output column. Downstream components refer to the column by using this value.|  
|LineageIdentificationString|String|The identification string which includes the name of the column.|  
|Name|String|The name of the output column.|  
|SortKeyPosition|Integer|A value that indicates whether a column is sorted, its sort order, and the sequence in which multiple columns are sorted. The value **0** indicates the column is not sorted. For more information, see [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).|  
|SpecialFlags|Integer|A value that contains the special flags of the output column.|  
|TruncationRowDisposition|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition>|A value that determines how the component handles truncations that occur when processing rows. The values are **Fail component**, **Ignore failure**, and **Redirect row**. The default value is **Fail component**.|  
  
 Output columns also include a set of data type properties.  
  
### External metadata column properties  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, inputs and outputs can contain a collection of external metadata columns. An external metadata column of a component in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100> interface.  
  
 The following table describes the properties of the external metadata columns of components in a data flow. Some properties have read-only values that are assigned at run time by the data flow engine.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|Description|String|Describes the external column.|  
|ID|Integer|A value that uniquely identifies the column.|  
|IdentificationString|String|A string that identifies the column.|  
|Name|String|The name of the external column.|  
  
 External metadata columns also include a set of data type properties.  
  
### Data type properties  
 Output columns and external metadata columns include a set of data type properties. Depending on the data type of the column, properties can be read/write or read-only.  
  
 The following table describes the data type properties of output columns and external metadata columns.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|CodePage|Integer|Specifies the code page for string data that is not Unicode.|  
|DataType|Integer (enumeration)|The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type of the column. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
|Length|Integer|The length, measured in characters, of a column.|  
|Precision|Integer|The precision of a numeric column.|  
|Scale|Integer|The scale of a numeric column.|  

## Custom properties of data flow components
For information about custom properties, see the following topics  
  
-   [ADO NET Custom Properties](../../integration-services/data-flow/ado-net-custom-properties.md)  
  
-   [CDC Control Task Custom Properties](../../integration-services/control-flow/cdc-control-task-custom-properties.md)  
  
-   [CDC Source Custom Properties](../../integration-services/data-flow/cdc-source-custom-properties.md)  
  
-   [Data Mining Model Training Destination Custom Properties](../../integration-services/data-flow/data-mining-model-training-destination-custom-properties.md)  
  
-   [DataReader Destination Custom Properties](../../integration-services/data-flow/datareader-destination-custom-properties.md)  
  
-   [Dimension Processing Destination Custom Properies](../../integration-services/data-flow/dimension-processing-destination-custom-properies.md)  
  
-   [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md)  
  
-   [Flat File Custom Properties](../../integration-services/data-flow/flat-file-custom-properties.md)  
  
-   [ODBC Destination Custom Properties](../../integration-services/data-flow/odbc-destination-custom-properties.md)  
  
-   [ODBC Source Custom Properties](../../integration-services/data-flow/odbc-source-custom-properties.md)  
  
-   [OLE DB Custom Properties](../../integration-services/data-flow/ole-db-custom-properties.md)OLE DB Custom Properties  
  
-   [Partition Processing Destination Custom Properties](../../integration-services/data-flow/partition-processing-destination-custom-properties.md)  
  
-   [Raw File Custom Properties](../../integration-services/data-flow/raw-file-custom-properties.md)  
  
-   [Recordset Destination Custom Properties](../../integration-services/data-flow/recordset-destination-custom-properties.md)  
  
-   [SQL Server Compact Edition Destination Custom Properties](../../integration-services/data-flow/sql-server-compact-edition-destination-custom-properties.md)  
  
-   [SQL Server Destination Custom Properties](../../integration-services/data-flow/sql-server-destination-custom-properties.md)  
  
-   [Transformation Custom Properties](../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
-   [XML Source Custom Properties](../../integration-services/data-flow/xml-source-custom-properties.md)  
  
## Use an expression in a data flow component
This procedure describes how to add an expression to the Conditional Split transformation or to the Derived Column transformation. The Conditional Split transformation uses expressions to define the conditions that direct data rows to a transformation output, and the Derived Column transformation uses expressions to define values assigned to columns.  
  
 To implement an expression in a transformation, the package must already include at least one Data Flow task and a source. 
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Control Flow** tab, and then click the Data Flow task that contains the data flow in which you want to implement an expression.  
  
4.  Click the **Data Flow** tab, and drag either a Conditional Split or Derived Column transformation from the **Toolbox** to the design surface.  
  
5.  Drag the green connector from the source or a transformation to the Conditional Split or Derived Column transformation.  
  
6.  Double-click the transformation to open its dialog box.  
  
7.  In the left pane, expand **Variables** to display system and user-defined variables, and expand **Columns** to display the transformation input columns.  
  
8.  In the right pane, expand **Mathematical Functions**, **String Functions**, **Date/Time Functions**, **NULL Functions**, **Type Casts**, and **Operators** to access the functions, the casts, and the operators that the expression grammar provides.  
  
9. Depending on the transformation, do one of the following to build an expression:  
  
    -   In the **Conditional Split Transformation Editor** dialog box, drag variables, columns, functions, operators, and casts to the **Condition** column. Alternatively, you can type an expression directly in the **Condition** column.  
  
    -   In the **Derived Column Transformation Editor** dialog box, drag variables, columns, functions, operators, and casts to the **Expression** column. Alternatively, you can type an expression directly in the **Expression** column.  
  
        > [!NOTE]  
        >  When you remove the focus from the **Condition** column or the **Expression** column, the expression text might be highlighted to indicate that the expression syntax is incorrect.  
  
10. Click **OK** to exit the dialog box.  
  
    > [!NOTE]  
    >  If the expression is not valid, an alert appears describing the syntax errors in the expression.  

## Data flow properties that you can set with an expression
The values of certain properties of data flow objects can be specified by using property expressions available on the Data Flow task container.  
  
 For information about using property expressions, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).  
  
 You can use property expressions to customize configurations for each deployed instance of a package. You can also use property expressions to specify run-time constraints for a package by using the **/set** option with the **dtexec** command prompt utility. For example, you can constrain the **MaximumThreads** used by the Sort transformation, or the **MaxMemoryUsage** of the Fuzzy Grouping and Fuzzy Lookup transformations. If unconstrained, these transformations may cache large amounts of data in memory.  
  
 To specify a property expression for one of the properties of data flow objects listed in this topic, display the **Properties** window for the Data Flow task by selecting the Data Flow task on the **Control Flow** surface of the designer, or by selecting the **Data Flow** tab of the designer without selecting any individual component or path. Select the **Expressions** property and click the ellipsis (...) to display the **Property Expressions Editor** dialog box. Drop down the **Property** list to select a property, then type an expression in the **Expression** text box, or click the ellipsis (...) to display the **Expression Builder** dialog box.  
  
 The **Property** list displays available properties for only those data flow objects that you have already placed on the **Data Flow** surface of the designer. Therefore, you cannot use the **Property** list to view all the possible properties of data flow objects that support property expressions. For example, if you have placed an ADO NET source on the designer surface, the **Property** list contains an entry for the **[ADO NET Source].[SqlCommand]** property. The list also displays many properties of the Data Flow task itself.  
 
 The values of the properties in the following list can be specified by using property expressions.  
  
### Data flow sources  
  
|Data Flow object|Property|  
|----------------------|--------------|  
|ADO NET source|TableOrViewName property<br /><br /> SqlCommand property|  
|XML source|XMLData property<br /><br /> XMLSchemaDefinition property|  
  
### Data flow transformations  
 For more information about these custom properties, see [Transformation Custom Properties](../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
|Data Flow object|Property|  
|----------------------|--------------|  
|Conditional Split transformation|FriendlyExpression property|  
|Derived Column transformation|FriendlyExpression property|  
|Fuzzy Grouping transformation|MaxMemoryUsage property|  
|Fuzzy Lookup transformation|MaxMemoryUsage property|  
|Lookup transformation|SqlCommand property<br /><br /> SqlCommandParam property|  
|OLE DB Command transformation|SqlCommand property|  
|Percentage Sampling transformation|SamplingValue property|  
|Pivot transformation|PivotKeyValue property|  
|Row Sampling transformation|SamplingValue property|  
|Sort transformation|MaximumThreads property|  
|Unpivot transformation|PivotKeyValue property|  
  
### Data flow destinations  
  
|Data Flow object|Property|  
|----------------------|--------------|  
|ADO NET Destination|TableOrViewName property<br /><br /> BatchSize property<br /><br /> CommandTimeout property|  
|Flat File destination|Header property|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact destination|TableName property|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination|BulkInsertTableName property<br /><br /> BulkInsertFirstRow property<br /><br /> BulkInsertLastRow property<br /><br /> BulkInsertOrder property<br /><br /> Timeout property|  

