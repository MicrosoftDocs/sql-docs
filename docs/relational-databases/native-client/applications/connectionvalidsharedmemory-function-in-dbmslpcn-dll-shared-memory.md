---
title: "ConnectionValidSharedMemory dbmslpcn.dll"
description: "ConnectionValidSharedMemory function in dbmslpcn.dll Shared Memory"
author: markingmyname
ms.author: maghan
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
---
# ConnectionValidSharedMemory function in dbmslpcn.dll Shared Memory
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  The function determines whether SQL Server Shared Memory is installed and active.  
  
## Syntax  
  
```cpp  
BOOL ConnectionValidSharedMemory(char * szServerName);  
```  
  
## Parameters  
 *szServerName*  
  
-   Type: **char\***  
  
-   The name of the SQL Server.  
  
## Return value  
 Type: **BOOL**  
  
 Returns 0  if not valid; else returns nonzero.  
  
  
