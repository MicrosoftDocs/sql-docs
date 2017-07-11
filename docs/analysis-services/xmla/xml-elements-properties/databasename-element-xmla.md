---
title: "DatabaseName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DatabaseName Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.databasename"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DatabaseName"
  - "urn:schemas-microsoft-com:xml-analysis#DatabaseName"
helpviewer_keywords: 
  - "DatabaseName element"
ms.assetid: 5cfd9a1f-da53-497a-b677-c51be4641bd0
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# DatabaseName Element (XMLA)
  Identifies the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database to be restored by the parent [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Restore>  
   ...  
   <DatabaseName>...</DatabaseName>  
   ...  
</Restore>  
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
|Parent elements|[Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **DatabaseName** element identifies the database into which the **Restore** command restores a backup file. If this element is not specified or contains an empty string, the name of the database contained in the backup file is used.  
  
 If the database already exists on the target instance, an error occurs unless the **AllowOverwrite** element for the parent **Restore** command is set to **True**.  
  
## See Also  
 [AllowOverwrite Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/allowoverwrite-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  