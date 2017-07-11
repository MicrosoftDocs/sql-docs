---
title: "ApplyCompression Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ApplyCompression Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ApplyCompression"
  - "urn:schemas-microsoft-com:xml-analysis#ApplyCompression"
  - "microsoft.xml.analysis.applycompression"
helpviewer_keywords: 
  - "ApplyCompression element"
ms.assetid: 93e222e5-9371-4fb5-aae0-f50b964cc264
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# ApplyCompression Element (XMLA)
  Determines whether the parent [Backup](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md) command compresses the backup file.  
  
## Syntax  
  
```xml  
  
<Backup>  
   ...  
   <ApplyCompression>...</ApplyCompression>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|True|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  