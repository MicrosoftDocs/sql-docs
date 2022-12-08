---
description: "Large CLR User-Defined Types in SQL Server Native Client"
title: "Large CLR User-Defined Types | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.reviewer: ""
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "large CLR user-defined types"
ms.assetid: b65eb61d-ccf6-49c0-98e7-9a4ef4b2f790
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Large CLR User-Defined Types in SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  In SQL Server 2005, user-defined types (UDTs) in the common language runtime (CLR) were restricted to 8,000 bytes in size. This restriction has been lifted in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] and later versions. CLR UDTs are now treated in a similar way to large object (LOB) types. That is, UDTs less than or equal to 8,000 bytes behave the same way as in SQL Server 2005, but larger UDTs are supported and report their size as "unlimited".  
  
 For more information, see [Large CLR User-Defined Types &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/large-clr-user-defined-types-ole-db.md) and [Large CLR User-Defined Types &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## Use Cases  
 For ODBC, support for large UDTs includes the ability to send UDT values in pieces as data-at-execution parameters. This is done by using SQLPutData.  
  
 For OLE DB, support for large UDTs includes the ability to stream UDT values to and from the server by using ISequentialStream binding.  
  
 UDTs less than or equal to 8,000 bytes will behave as they did in SQL Server 2005. For OLE DB, you can still stream small UDTs by using ISequentialStream binding.  
  
 Sometimes native code will have to understand the contents of CLR UDTs, but will not have to instantiate managed objects. If this is the case, you can use custom serialization to convert UDT values on the server into a well known format for clients.  
  
 For applications that have existing data access code, you can exploit CLR UDT behavior on the client by retrieving UDTs through native APIs and instantiating them by using C++ CLI interop in mixed mode applications.  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  
