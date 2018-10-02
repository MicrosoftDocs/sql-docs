---
title: "Data Flow Path Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.patheditor.general.f1"
helpviewer_keywords: 
  - "Data Flow Path Editor dialog box"
ms.assetid: 72a9ff1d-3748-41d1-a9b2-63f4a77bba24
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Data Flow Path Editor (General Page)
  Use the **Data Flow Path Editor** dialog box to set path properties, view column metadata, and manage the data viewers attached to the path.  
  
 Use the **General** node of the **Data Flow Path Editor** dialog box to name and describe the path, and to specify the options for path annotation.  
  
## Options  
 **Name**  
 Provide a unique name for the path.  
  
 **ID**  
 The lineage identifier of the path. This property is read-only.  
  
 **IdentificationString**  
 The string that identifies the path. Automatically generated from the name entered above.  
  
 **Description**  
 Describe the path.  
  
 **PathAnnotation**  
 Specify the type of annotation to use. Choose **Never** to disable annotations, **AsNeeded** to enable annotation on demand, **SourceName** to automatically annotate using the value of the **SourceName** option, and **PathName** to automatically annotate using the value of the **Name** property.  
  
 **DestinationName**  
 Displays the input that is the end of the path.  
  
 **SourceName**  
 Displays the output that is the start of the path.  
  
## See Also  
 [Data Flow Path Editor &#40;Metadata Page&#41;](../../2014/integration-services/data-flow-path-editor-metadata-page.md)   
 [Data Flow Path Editor &#40;Data Viewers Page&#41;](../../2014/integration-services/data-flow-path-editor-data-viewers-page.md)   
 [Data Flow](data-flow/data-flow.md)   
 [Use Annotations in Packages](use-annotations-in-packages.md)  
  
  
