---
title: "DISCOVER_KEYWORDS Rowset (OLE DB for OLAP) | Microsoft Docs"
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
  - "DISCOVER_KEYWORDS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_KEYWORDS rowset"
ms.assetid: 70cc680d-9530-469b-8a61-4e6779aec17a
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_KEYWORDS Rowset (OLE DB for OLAP)
  Enumerates a list of words reserved by the provider.  
  
## Rowset Columns  
 The DISCOVER_KEYWORDS rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**Keyword**|**DBTYPE_WSTR**||A reserved keyword.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The DISCOVER_KEYWORDS rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**Keyword**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  