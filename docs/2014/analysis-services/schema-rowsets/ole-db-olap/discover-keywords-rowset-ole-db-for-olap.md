---
title: "DISCOVER_KEYWORDS Rowset (OLE DB for OLAP) | Microsoft Docs"
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
ms.assetid: 70cc680d-9530-469b-8a61-4e6779aec17a
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_KEYWORDS Rowset (OLE DB for OLAP)
  Enumerates a list of words reserved by the provider.  
  
## Rowset Columns  
 The DISCOVER_KEYWORDS rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`Keyword`|`DBTYPE_WSTR`||A reserved keyword.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The DISCOVER_KEYWORDS rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|`Keyword`|`DBTYPE_WSTR`|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](ole-db-for-olap-schema-rowsets.md)  
  
  
