---
title: "SynchronizeSecurity Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# SynchronizeSecurity Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Specifies how to synchronize security definitions, such as roles and permissions, during a [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Synchronize>  
   ...  
   <SynchronizeSecurity>...</SynchronizeSecurity>  
   ...  
</Synchronize>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*SkipMembership*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Security** element determines whether the security definitions, such as roles and permissions, defined on an Analysis Services database are synchronized during a **Synchronize** command. This element also determines if the Windows user accounts and groups defined as members of the security definitions are included as part of the **Synchronize** command.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SkipMembership*|Include security definitions, but exclude membership information, during a **Synchronize** command.|  
|*CopyAll*|Include security definitions and membership information during a **Synchronize** command.|  
|*IgnoreSecurity*|Exclude security definitions during a **Synchronize** command.|  
  
## See also
 [Security Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/security-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
