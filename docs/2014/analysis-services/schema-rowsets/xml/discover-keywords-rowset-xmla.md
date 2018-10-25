---
title: "DISCOVER_KEYWORDS Rowset (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DISCOVER_KEYWORDS"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DISCOVER_KEYWORDS rowset"
ms.assetid: 99945e53-3a1b-4d7b-9aff-712977db8b2d
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_KEYWORDS Rowset (XMLA)
  Returns information about keywords reserved by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider.  
  
 If you call the [Discover](../../xmla/xml-elements-methods-discover.md) method with the `DISCOVER_KEYWORDS` enumeration value in the [RequestType](../../xmla/xml-elements-properties/type-element-xmla.md) element, the `Discover` method returns the `DISCOVER_KEYWORDS` rowset.  
  
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
 [XML for Analysis Schema Rowsets](xml-for-analysis-schema-rowsets.md)   
 [DISCOVER_LITERALS Rowset](discover-literals-rowset.md)  
  
  
