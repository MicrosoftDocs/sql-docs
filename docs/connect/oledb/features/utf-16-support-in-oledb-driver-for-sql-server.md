---
title: "UTF-16 Support in OLE DB Driver for SQL Server| Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb|features"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# UTF-16 Support in OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  Beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], if you supply a fixed-length buffer when binding a column result or output parameter and if the **wchar** character written into the buffer before the terminating character is a high surrogate code point of a surrogate pair, and if the next **wchar** character is a low surrogate code point, OLE DB Driver for SQL Server will not add the high surrogate code point to the buffer.  
  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)   
  
  
