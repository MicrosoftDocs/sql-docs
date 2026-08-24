---
title: "Row bookmarks (OLE DB driver)"
description: Learn how bookmarks let consumers quickly return to a row by accessing rows randomly based on the bookmark value in OLE DB Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "bookmarks [OLE DB]"
  - "OLE DB Driver for SQL Server, bookmarks"
  - "rowsets [OLE DB], bookmarks"
  - "OLE DB rowsets, bookmarks"
---
# Bookmarks
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Bookmarks let consumers quickly return to a row. With bookmarks, consumers can access rows randomly based on the bookmark value. The bookmark column is column 0 in the rowset. The consumer sets the dwFlag field value of the binding structure to DBCOLUMNSINFO_ISBOOKMARK to indicate that the column is used as a bookmark. The consumer also sets the rowset property DBPROP_BOOKMARKS to VARIANT_TRUE. This lets column 0 be present in the rowset. The **IRowsetLocate::GetRowsAt** method is then used to fetch rows, starting with the row specified as an offset from a bookmark.  
  
## Related content

- [Rowsets](rowsets.md)
