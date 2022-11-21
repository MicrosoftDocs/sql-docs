---
description: "Row bookmarks (Native Client OLE DB provider)"
title: "Row bookmarks (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "bookmarks [OLE DB]"
  - "SQL Server Native Client OLE DB provider, bookmarks"
  - "rowsets [OLE DB], bookmarks"
  - "OLE DB rowsets, bookmarks"
ms.assetid: 7d9076f2-bf9c-452e-b816-70371a0c1644
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Bookmarks in SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Bookmarks let consumers quickly return to a row. With bookmarks, consumers can access rows randomly based on the bookmark value. The bookmark column is column 0 in the rowset. The consumer sets the dwFlag field value of the binding structure to DBCOLUMNSINFO_ISBOOKMARK to indicate that the column is used as a bookmark. The consumer also sets the rowset property DBPROP_BOOKMARKS to VARIANT_TRUE. This lets column 0 be present in the rowset. The **IRowsetLocate::GetRowsAt** method is then used to fetch rows, starting with the row specified as an offset from a bookmark.  
  
## See Also  
 [Rowsets](../../relational-databases/native-client-ole-db-rowsets/rowsets.md)  
  
  
