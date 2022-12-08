---
title: "Create rowset with IOpenRowset (OLE DB driver)"
description: "Learn how OLE DB Driver for SQL Server supports the IOpenRowset::OpenRowset method to return a rowset and restrictions on its use."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "IOpenRowset interface"
  - "rowsets [OLE DB], creating"
  - "OLE DB Driver for SQL Server, rowsets"
  - "OLE DB rowsets, creating"
---
# Creating a Rowset with IOpenRowset
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server supports the **IOpenRowset::OpenRowset** method with the following restrictions:  
  
-   A base table or view must be specified in a database ID (DBID) structure that the *pTableID* parameter points to.  
  
-   The DBID *eKind* member must indicate DBKIND_NAME.  
  
-   The DBID *uName* member must specify the name of an existing base table or a view as a Unicode character string.  
  
-   The *pIndexID* parameter of **OpenRowset** must be NULL.  
  
 The result set of **IOpenRowset::OpenRowset** contains a single rowset. Result sets that contain a single rowset can be supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cursors. Cursor support allows the developer to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] concurrency mechanisms.  
  
## See Also  
 [Rowsets](../../oledb/ole-db-rowsets/rowsets.md)  
  
  
