---
title: "SynchronizeSecurity Element (XMLA) | Microsoft Docs"
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
  - "SynchronizeSecurity Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.synchronizesecurity"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#SynchronizeSecurity"
  - "urn:schemas-microsoft-com:xml-analysis#SynchronizeSecurity"
helpviewer_keywords: 
  - "SynchronizeSecurity element"
ms.assetid: d37dbb95-f4a4-44ac-8eb9-f661d5bb5018
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# SynchronizeSecurity Element (XMLA)
  Specifies how to synchronize security definitions, such as roles and permissions, during a [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Synchronize>  
   ...  
   <SynchronizeSecurity>...</SynchronizeSecurity>  
   ...  
</Synchronize>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*SkipMembership*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Security** element determines whether the security definitions, such as roles and permissions, defined on an [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database are synchronized during a **Synchronize** command. This element also determines if the Windows user accounts and groups defined as members of the security definitions are included as part of the **Synchronize** command.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SkipMembership*|Include security definitions, but exclude membership information, during a **Synchronize** command.|  
|*CopyAll*|Include security definitions and membership information during a **Synchronize** command.|  
|*IgnoreSecurity*|Exclude security definitions during a **Synchronize** command.|  
  
## See Also  
 [Security Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/security-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  