---
description: "XML Source"
title: "XML Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.xmlsource.f1"
  - "sql13.dts.designer.xmlsourceadapter.connectionmanager.f1"
  - "sql13.dts.designer.xmlsourceadapter.columns.f1"
  - "sql13.dts.designer.xmlsourceadapter.erroroutput.f1"
helpviewer_keywords: 
  - "sources [Integration Services], XML"
  - "XML source [Integration Services]"
  - "XML Source Editor"
ms.assetid: 68c27ea5-e93d-4e26-bfb2-d967ca0a5282
author: chugugrace
ms.author: chugu
---
# XML Source

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The XML source reads an XML data file and populates the columns in the source output with the data.  
  
 The data in XML files frequently includes hierarchical relationships. For example, an XML data file can represent catalogs and items in catalogs. Before the data can enter the data flow, the relationship of the elements in XML data file must be determined, and an output must be generated for each element in the file.  
  
## Schemas  
 The XML source uses a schema to interpret the XML data. The XML source supports use of a XML Schema Definition (XSD) file or inline schemas to translate the XML data into a tabular format. If you configure the XML source by using the **XML Source Editor** dialog box, the user interface can generate an XSD from the specified XML data file.  
  
> [!NOTE]  
>  DTDs are not supported.  
  
 The schemas can support a single namespace only; they do not support schema collections.  
  
> [!NOTE]  
>  The XML source does not validate the data in the XML file against the XSD.  
  
## XML Source Editor  
 The data in the XML files frequently includes hierarchical relationships. The **XML Source Editor** dialog box uses the specified schema to generate the XML source outputs. You can specify an XSD file, use an inline schema, or generate an XSD from the specified XML data file. The schema must be available at design time.  
  
 The XML source generates tabular structures from the XML data by creating an output for every element that contains other elements in the XML files. For example, if the XML data represents catalogs and items in catalogs, the XML source creates an output for catalogs and an output for each type of item that the catalogs contain. The output of each item will contain output columns for the attributes of that item.  
  
 To provide information about the hierarchical relationship of the data in the outputs, the XML source adds a column in the outputs that identifies the parent element for each child element. Using the example of catalogs with different types of items, each item would have a column value that identifies the catalog to which it belongs.  
  
 The XML source creates an output for every element, but it is not required that you use all the outputs. You can delete any output that you do not want to use, or just not connect it to a downstream component.  
  
 The XML source also generates the output names, to ensure that the names are unambiguous. These names may be long and may not identify the outputs in a way that is useful to you. You can rename the outputs, as long as their names remain unique. You can also modify the data type and the length of output columns.  
  
 For every output, the XML source adds an error output. By default the columns in error outputs have Unicode string data type (DT_WSTR) with a length of 255, but you can configure the columns in the error outputs by modifying their data type and length.  
  
 If the XML data file contains elements that are not in the XSD, these elements are ignored and no output is generated for them. On the other hand, if the XML data file is missing elements that are represented in the XSD, the output will contain columns with null values.  
  
 When the data is extracted from the XML data file, it is converted to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type. However, the XML source cannot convert the XML data to the DT_TIME2 or DT_DBTIMESTAMP2 data types because the source does not support these data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 The XSD or inline schema may specify the data type for elements, but if it does not, the **XML Source Editor** dialog box assigns the Unicode string data type (DT_WSTR) to the column in the output that contains the element, and sets the column length to 255 characters.  
  
 If the schema specifies the maximum length of an element, the length of output column is set to this value. If the maximum length is greater than the length supported by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type to which the element is converted, then the data is truncated to the maximum length of the data type. For example, if a string has a length of 5000, it is truncated to 4000 characters because the maximum length of the DT_WSTR data type is 4000 characters; likewise, byte data is truncated to 8000 characters, the maximum length of the DT_BYTES data type. If the schema specifies no maximum length, the default length of columns with either data type is set to 255. Data truncation in the XML source is handled the same way as truncation in other data flow components. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).  
  
 You can modify the data type and the column length. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Configuration of the XML Source  
 The XML source supports three different data access modes. You can specify the file location of the XML data file, the variable that contains the file location, or the variable that contains the XML data.  
  
 The XML source includes the **XMLData** and **XMLSchemaDefinition** custom properties that can be updated by property expressions when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md), and [XML Source Custom Properties](../../integration-services/data-flow/xml-source-custom-properties.md).  
  
 The XML source supports multiple regular outputs and multiple error outputs.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the **XML Source Edito**r dialog box for configuring the XML source. This dialog box is available in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](./set-the-properties-of-a-data-flow-component.md)  
  
-   [XML Source Custom Properties](../../integration-services/data-flow/xml-source-custom-properties.md)  
  
 For more information about how to set the properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## XML Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **XML Source Editor** to specify an XML file and the XSD that transforms the XML data.  
  
### Static Options  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Value|Description|  
|-----------|-----------------|  
|XML file location|Retrieve data from an XML file.|  
|XML file from variable|Specify the XML file name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](../integration-services-ssis-variables.md)|  
|XML data from variable|Retrieve XML data from a variable.|  
  
 **Use inline schema**  
 Specify whether the XML source data itself contains the XSD schema that defines and validates its structure and data.  
  
 **XSD location**  
 Type the path and file name of the XSD schema file, or locate the file by clicking **Browse**.  
  
 **Browse**  
 Use the **Open** dialog box to locate the XSD schema file.  
  
 **Generate XSD**  
 Use the **Save As** dialog box to select a location for the auto-generated XSD schema file. The editor infers the schema from the structure of the XML data.  
  
### Data Access Mode Dynamic Options  
  
#### Data access mode = XML file location  
 **XML location**  
 Type the path and file name of the XML data file, or locate the file by clicking **Browse**.  
  
 **Browse**  
 Use the **Open** dialog box to locate the XML data file.  
  
#### Data access mode = XML file from variable  
 **Variable name**  
 Select the variable that contains the path and file name of the XML file.  
  
#### Data access mode = XML data from variable  
 **Variable name**  
 Select the variable that contains the XML data.  
  
## XML Source Editor (Columns Page)
  Use the **Columns** node of the **XML Source Editor** dialog box to map an output column to an external (source) column.  
  
### Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which the task will read them. You can change this order by first clearing the selected columns in the table displayed in the editor, and then selecting external columns from the list in a different order.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
## XML Source Editor (Error Output Page)
  Use the **Error Output** page of the **XML Source Editor** dialog box to select error handling options and to set properties on error output columns.  
  
### Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **XML Source Editor**dialog box.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Specify what should happen when a truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 View the description of the error.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## Related Tasks  
 [Extract Data by Using the XML Source](../../integration-services/data-flow/extract-data-by-using-the-xml-source.md)