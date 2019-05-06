---
title: "XML Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.xmlsourceadapter.connectionmanager.f1"
helpviewer_keywords: 
  - "XML Source Editor"
ms.assetid: e6507403-a3ce-4b6f-91fc-a7de9f7b6283
author: janinezhang
ms.author: janinez
manager: craigg
---
# XML Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **XML Source Editor** to specify an XML file and the XSD that transforms the XML data.  
  
 For more information about the XML source, see [XML Source](data-flow/xml-source.md).  
  
## Static Options  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Value|Description|  
|-----------|-----------------|  
|XML file location|Retrieve data from an XML file.|  
|XML file from variable|Specify the XML file name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)|  
|XML data from variable|Retrieve XML data from a variable.|  
  
 **Use inline schema**  
 Specify whether the XML source data itself contains the XSD schema that defines and validates its structure and data.  
  
 **XSD location**  
 Type the path and file name of the XSD schema file, or locate the file by clicking **Browse**.  
  
 **Browse**  
 Use the **Open** dialog box to locate the XSD schema file.  
  
 **Generate XSD**  
 Use the **Save As** dialog box to select a location for the auto-generated XSD schema file. The editor infers the schema from the structure of the XML data.  
  
## Data Access Mode Dynamic Options  
  
### Data access mode = XML file location  
 **XML location**  
 Type the path and file name of the XML data file, or locate the file by clicking **Browse**.  
  
 **Browse**  
 Use the **Open** dialog box to locate the XML data file.  
  
### Data access mode = XML file from variable  
 **Variable name**  
 Select the variable that contains the path and file name of the XML file.  
  
### Data access mode = XML data from variable  
 **Variable name**  
 Select the variable that contains the XML data.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [XML Source Editor &#40;Columns Page&#41;](../../2014/integration-services/xml-source-editor-columns-page.md)   
 [XML Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/xml-source-editor-error-output-page.md)   
 [Extract Data by Using the XML Source](data-flow/extract-data-by-using-the-xml-source.md)  
  
  
