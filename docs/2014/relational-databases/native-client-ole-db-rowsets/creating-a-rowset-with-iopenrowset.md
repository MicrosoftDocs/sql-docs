---
title: "Creating a Rowset with IOpenRowset | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "IOpenRowset interface"
  - "rowsets [OLE DB], creating"
  - "SQL Server Native Client OLE DB provider, rowsets"
  - "OLE DB rowsets, creating"
ms.assetid: e8bc3de7-4b97-4de9-8df8-e11947d24045
author: MightyPen
ms.author: genemi
manager: craigg
---
# Creating a Rowset with IOpenRowset
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports the **IOpenRowset::OpenRowset** method with the following restrictions:  
  
-   A base table or view must be specified in a database ID (DBID) structure that the *pTableID* parameter points to.  
  
-   The DBID *eKind* member must indicate DBKIND_NAME.  
  
-   The DBID *uName* member must specify the name of an existing base table or a view as a Unicode character string.  
  
-   The *pIndexID* parameter of **OpenRowset** must be NULL.  
  
 The result set of **IOpenRowset::OpenRowset** contains a single rowset. Result sets that contain a single rowset can be supported by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursors. Cursor support allows the developer to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] concurrency mechanisms.  
  
## See Also  
 [Rowsets](rowsets.md)  
  
  
