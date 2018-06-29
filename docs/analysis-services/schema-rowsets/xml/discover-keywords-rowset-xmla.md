---
title: "DISCOVER_KEYWORDS Rowset (XMLA) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DISCOVER_KEYWORDS Rowset (XMLA)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Returns information about keywords reserved by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_KEYWORDS** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_KEYWORDS** rowset.  
  
## Rowset Columns  
 The **DISCOVER_KEYWORDS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**Keyword**|**DBTYPE_WSTR**||A list of all the keywords reserved by a provider.<br /><br /> Example: **AND**|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_KEYWORDS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**Keyword**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)   
 [DISCOVER_LITERALS Rowset](../../../analysis-services/schema-rowsets/xml/discover-literals-rowset.md)  
  
  
