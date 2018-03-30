---
title: "DISCOVER_KEYWORDS Rowset (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DISCOVER_KEYWORDS"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DISCOVER_KEYWORDS rowset"
ms.assetid: 99945e53-3a1b-4d7b-9aff-712977db8b2d
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DISCOVER_KEYWORDS Rowset (XMLA)
  Returns information about keywords reserved by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider.  
  
 If you call the [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) method with the `DISCOVER_KEYWORDS` enumeration value in the [RequestType](../../../2014/analysis-services/dev-guide/requesttype-element-xmla.md) element, the `Discover` method returns the `DISCOVER_KEYWORDS` rowset.  
  
## Rowset Columns  
 The `DISCOVER_KEYWORDS` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`Keyword`|`DBTYPE_WSTR`||A list of all the keywords reserved by a provider.<br /><br /> Example: `AND`|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The `DISCOVER_KEYWORDS` rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|`Keyword`|`DBTYPE_WSTR`|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../2014/analysis-services/dev-guide/xml-for-analysis-schema-rowsets.md)   
 [DISCOVER_LITERALS Rowset](../../../2014/analysis-services/dev-guide/discover-literals-rowset.md)  
  
  