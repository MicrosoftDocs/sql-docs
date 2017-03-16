---
title: "XML Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.xmlsourceadapter.connectionmanager.f1"
helpviewer_keywords: 
  - "XML Source Editor"
ms.assetid: e6507403-a3ce-4b6f-91fc-a7de9f7b6283
caps.latest.revision: 20
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# XML Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **XML Source Editor** to specify an XML file and the XSD that transforms the XML data.  
  
 For more information about the XML source, see [XML Source](../../integration-services/data-flow/xml-source.md).  
  
## Static Options  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Value|Description|  
|-----------|-----------------|  
|XML file location|Retrieve data from an XML file.|  
|XML file from variable|Specify the XML file name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](http://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
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
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [XML Source Editor &#40;Columns Page&#41;](../../integration-services/data-flow/xml-source-editor-columns-page.md)   
 [XML Source Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/xml-source-editor-error-output-page.md)   
 [Extract Data by Using the XML Source](../../integration-services/data-flow/extract-data-by-using-the-xml-source.md)  
  
  