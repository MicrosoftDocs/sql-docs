---
title: "Resynchronizing Rows | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "synchronization [OLE DB]"
  - "IRowsetResynch interface"
  - "resynchronizing rows"
  - "data updates [SQL Server], OLE DB"
ms.assetid: d2d30505-a878-4aa9-b821-53d8118a45a5
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Updating Data in Rowsets - Resynchronizing Rows
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports **IRowsetResynch** on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursor-supported rowsets only. **IRowsetResynch** is not available on demand. The consumer must request the interface before opening the rowset.  
  
## See Also  
 [Updating Data in Rowsets](../../relational-databases/native-client-ole-db-rowsets/updating-data-in-rowsets.md)  
  
  
