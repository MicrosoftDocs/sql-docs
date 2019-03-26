---
title: "Error Handling in Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.configureerroroutput.f1"
helpviewer_keywords: 
  - "truncating data"
  - "data conversion errors [Integration Services]"
  - "errors [Integration Services], data flow components"
  - "lookups [Integration Services]"
  - "errors [Integration Services]"
  - "errors [Integration Services], data flow outputs"
  - "error outputs [Integration Services]"
  - "data flow [Integration Services], errors"
  - "expressions [Integration Services], errors"
ms.assetid: c61667b4-25cb-4d45-a52f-a733e32863f4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Error Handling in Data
  When a data flow component applies a transformation to column data, extracts data from sources, or loads data into destinations, errors can occur. Errors frequently occur because of unexpected data values. For example, a data conversion fails because a column contains a string instead of a number, an insertion into a database column fails because the data is a date and the column has a numeric data type, or an expression fails to evaluate because a column value is zero, resulting in a mathematical operation that is not valid.  
  
 Errors typically fall into one the following categories:  
  
-   Data conversion errors, which occur if a conversion results in loss of significant digits, the loss of insignificant digits, and the truncation of strings. Data conversion errors also occur if the requested conversion is not supported.  
  
-   Expression evaluation errors, which occur if expressions that are evaluated at run time perform invalid operations or become syntactically incorrect because of missing or incorrect data values.  
  
-   Lookup errors, which occur if a lookup operation fails to locate a match in the lookup table.  
  
 For a list of Integration Services errors, warnings, and other messages, see [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md).  
  
## Use error outputs to capture row-level errors  
 Many data flow components support error outputs, which let you control how the component handles row-level errors in both incoming and outgoing data. You specify how the component behaves when truncation or an error occurs by setting options on individual columns in the input or output. For example, you can specify that the component should fail if customer name data is truncated, but ignore errors on another column that contains less important data.  
  
 The error output can be connected to the input of another transformation or loaded into a different destination than the non-error output. For example, the error output can be a connected to a Derived Column transformation that provides a string for a column that is blank.  
  
 The following diagram shows a simple data flow including an error output.  
  
 ![Data flow with error output](../../integration-services/data-flow/media/mw-dts-11.gif "Data flow with error output")  
  
 For more information, see [Data Flow](../../integration-services/data-flow/data-flow.md) and [Integration Services Paths](../../integration-services/data-flow/integration-services-paths.md).  

## Configure Error Output dialog box
Use the **Configure Error Output** dialog box to configure error handling options for data flow transformations that support an error output.  
  
 To learn more about working with error outputs, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).  
  
### Options  
 **Input or Output**  
 View the name of the output.  
  
 **Column**  
 View the output columns that you selected in the transformation editor dialog box.  
  
 **Error**  
 If applicable, specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 If applicable, specify what should happen when a truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Description**  
 View the description of the operation.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## Errors are either failures or truncations  
 Errors fall into one of two categories: errors or truncations.  
  
 **Errors**. An error indicates an unequivocal failure, and generates a NULL result. Such errors can include data conversion errors or expression evaluation errors. For example, an attempt to convert a string that contains alphabetical characters to a number causes an error. Data conversions, expression evaluations, and assignments of expression results to variables, properties, and data columns may fail because of illegal casts and incompatible data types. For more information see, [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md), [Integration Services Data Types in Expressions](../../integration-services/expressions/integration-services-data-types-in-expressions.md), and [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 **Truncations**. A truncation is less serious than an error. A truncation generates results that might be usable or even desirable. You can elect to treat truncations as errors or as acceptable conditions. For example, if you are inserting a 15-character string into a column that is only one character wide, you can elect to truncate the string.  
  
## Select an error handling option  
 You can configure how sources, transformations, and destinations handle errors and truncations. The following table describes the options.  
  
|Option|Description|  
|------------|-----------------|  
|Fail Component|The Data Flow task fails when an error or a truncation occurs. Failure is the default option for an error and a truncation.|  
|Ignore Failure|The error or the truncation is ignored and the data row is directed to the output of the transformation or source.|  
|Redirect Row|The error or the truncation data row is directed to the error output of the source, transformation, or destination.|  
  
## Get more info about the error  
 In addition to the data columns, the error output includes the **ErrorCode** and **ErrorColumn** columns. The **ErrorCode** column identifies the error and the **ErrorColumn** contains the lineage identifier of the error column.  
  
 Under some circumstances, the value of the **ErrorColumn** column is set to zero. This occurs when the error condition affects the entire row instead of a single column. An example is when a lookup fails in the Lookup transformation.  
  
 These two numeric values may be of limited use without the corresponding error description and column name. Here are some ways to get the error description and column name.  
  
-   You can see both error descriptions and column names by attaching a Data Viewer to the error output. In SSIS Designer, right-click on the red arrow leading to an error output and select **Enable Data Viewer**.  
  
-   You can find column names by enabling logging and selecting the **DiagnosticEx** event. This event writes a data flow column map to the log. You can then look up the column name from its identifier in this column map. Note that the **DiagnosticEx** event does not preserve whitespace in its XML output to reduce the size of the log. To improve readability, copy the log into an XML editor - in Visual Studio, for example - that supports XML formatting and syntax highlighting. For more info about logging, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
     Here is an example of a data flow column map.  
  
    ```xml  
  
    \<DTS:PipelineColumnMap xmlns:DTS="www.microsoft.com/SqlServer/Dts">  
        \<DTS:Pipeline DTS:Path="\Package\Data Flow Task">  
            \<DTS:Column DTS:ID="11" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Output].Columns[Customer]"/>  
            \<DTS:Column DTS:ID="12" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Output].Columns[Product]"/>  
            \<DTS:Column DTS:ID="13" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Output].Columns[Price]"/>  
            \<DTS:Column DTS:ID="14" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Output].Columns[Timestamp]"/>  
            \<DTS:Column DTS:ID="20" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Error Output].Columns[Customer]"/>  
            \<DTS:Column DTS:ID="21" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Error Output].Columns[Product]"/>  
            \<DTS:Column DTS:ID="22" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Error Output].Columns[Price]"/>  
            \<DTS:Column DTS:ID="23" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Error Output].Columns[Timestamp]"/>  
            \<DTS:Column DTS:ID="24" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Error Output].Columns[ErrorCode]"/>  
            \<DTS:Column DTS:ID="25" DTS:IdentificationString="ADO NET Source.Outputs[ADO NET Source Error Output].Columns[ErrorColumn]"/>  
            \<DTS:Column DTS:ID="31" DTS:IdentificationString="Flat File Destination.Inputs[Flat File Destination Input].Columns[Customer]"/>  
            \<DTS:Column DTS:ID="32" DTS:IdentificationString="Flat File Destination.Inputs[Flat File Destination Input].Columns[Product]"/>  
            \<DTS:Column DTS:ID="33" DTS:IdentificationString="Flat File Destination.Inputs[Flat File Destination Input].Columns[Price]"/>  
            \<DTS:Column DTS:ID="34" DTS:IdentificationString="Flat File Destination.Inputs[Flat File Destination Input].Columns[Timestamp]"/>  
        \</DTS:Pipeline>  
    \</DTS:PipelineColumnMap>  
  
    ```  
  
-   You can also use the Script component to include the error description and the column name in additional columns of the error output. For an example, see [Enhancing an Error Output with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-examples/enhancing-an-error-output-with-the-script-component.md).  
  
    -   Include the error description in an additional column by using a single line of script to call the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.GetErrorDescription%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface.  
  
    -   Include the column name in an additional column by using a single line of script to call the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.GetIdentificationStringByID%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface.  
  
     You can add the Script component to the error segment of the data flow anywhere downstream from the data flow components whose errors you want to capture. Typically you place the Script component immediately before the error rows are written to a destination. This way, the script looks up descriptions only for error rows that are written. The error segment of the data flow may correct some errors and not write those rows to an error destination.  

## See Also  
 [Data Flow](../../integration-services/data-flow/data-flow.md)   
 [Transform Data with Transformations](../../integration-services/data-flow/transformations/transform-data-with-transformations.md)   
 [Connect Components with Paths](https://msdn.microsoft.com/library/05633e4c-1370-4b05-802b-f36b07dd71c8)   
 [Data Flow Task](../../integration-services/control-flow/data-flow-task.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
