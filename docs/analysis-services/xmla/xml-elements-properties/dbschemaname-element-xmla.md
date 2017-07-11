---
title: "DbSchemaName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DbSchemaName Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#DbSchemaName"
  - "microsoft.xml.analysis.dbschemaname"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DbSchemaName"
helpviewer_keywords: 
  - "DbSchemaName element"
ms.assetid: 40ca10c9-7597-48fe-a9d9-ee2c7b84d4d1
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# DbSchemaName Element (XMLA)
  Contains the name of the schema used by the parent [TableNotification](../../../analysis-services/xmla/xml-elements-properties/tablenotification-element-xmla.md) element in the table identified by the [DbTableName](../../../analysis-services/xmla/xml-elements-properties/dbtablename-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<TableNotification>  
   ...  
   <DbSchemaName>...</DbSchemaName>  
   ...  
</TableNotification>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[TableNotification](../../../analysis-services/xmla/xml-elements-properties/tablenotification-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  