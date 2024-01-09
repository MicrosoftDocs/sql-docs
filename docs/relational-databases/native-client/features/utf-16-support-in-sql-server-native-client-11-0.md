---
title: "UTF-16 Support"
description: Learn about support for UTF-16 in a fixed-length buffer in SQL Server Native Client, beginning with SQL Server 2012.
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
---
# UTF-16 Support in SQL Server Native Client 11.0
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  Beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], if you supply a fixed-length buffer when binding a column result or output parameter and if the **wchar** character written into the buffer before the terminating character is a high surrogate code point of a surrogate pair, and if the next **wchar** character is a low surrogate code point, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will not add the high surrogate code point to the buffer.  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  
