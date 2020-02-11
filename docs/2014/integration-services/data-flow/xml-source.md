---
title: "XML Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.xmlsource.f1"
helpviewer_keywords: 
  - "sources [Integration Services], XML"
  - "XML source [Integration Services]"
  - "XML Source Editor"
ms.assetid: 68c27ea5-e93d-4e26-bfb2-d967ca0a5282
author: janinezhang
ms.author: janinez
manager: craigg
---
# XML Source
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
  
 When the data is extracted from the XML data file, it is converted to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type. However, the XML source cannot convert the XML data to the DT_TIME2 or DT_DBTIMESTAMP2 data types because the source does not support these data types. For more information, see [Integration Services Data Types](integration-services-data-types.md).  
  
 The XSD or inline schema may specify the data type for elements, but if it does not, the **XML Source Editor** dialog box assigns the Unicode string data type (DT_WSTR) to the column in the output that contains the element, and sets the column length to 255 characters.  
  
 If the schema specifies the maximum length of an element, the length of output column is set to this value. If the maximum length is greater than the length supported by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type to which the element is converted, then the data is truncated to the maximum length of the data type. For example, if a string has a length of 5000, it is truncated to 4000 characters because the maximum length of the DT_WSTR data type is 4000 characters; likewise, byte data is truncated to 8000 characters, the maximum length of the DT_BYTES data type. If the schema specifies no maximum length, the default length of columns with either data type is set to 255. Data truncation in the XML source is handled the same way as truncation in other data flow components. For more information, see [Error Handling in Data](error-handling-in-data.md).  
  
 You can modify the data type and the column length. For more information, see [Integration Services Data Types](integration-services-data-types.md).  
  
## Configuration of the XML Source  
 The XML source supports three different data access modes. You can specify the file location of the XML data file, the variable that contains the file location, or the variable that contains the XML data.  
  
 The XML source includes the `XMLData` and `XMLSchemaDefinition` custom properties that can be updated by property expressions when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../expressions/use-property-expressions-in-packages.md), and [XML Source Custom Properties](xml-source-custom-properties.md).  
  
 The XML source supports multiple regular outputs and multiple error outputs.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the **XML Source Edito**r dialog box for configuring the XML source. This dialog box is available in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **XML Source Editor** dialog box, click one of the following topics:  
  
-   [XML Source Editor &#40;Connection Manager Page&#41;](../xml-source-editor-connection-manager-page.md)  
  
-   [XML Source Editor &#40;Columns Page&#41;](../xml-source-editor-columns-page.md)  
  
-   [XML Source Editor &#40;Error Output Page&#41;](../xml-source-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [XML Source Custom Properties](xml-source-custom-properties.md)  
  
 For more information about how to set the properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md)  
  
## Related Tasks  
 [Extract Data by Using the XML Source](xml-source.md)  
  
## Related Content  
 Technical article, [Using an XML file to configure an SSIS package](https://www.sqlshack.com/using-xml-file-configure-ssis-package/).  
  
  
