---
description: "ConnectionValidSharedMemory function in dbmslpcn.dll Shared Memory"
title: "ConnectionValidSharedMemory dbmslpcn.dll"
ms.custom: ""
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 6ae35826-7d75-4542-b686-5f79316b6157
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
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
  
-   The name of the SQL server.  
  
## Return value  
 Type: **BOOL**  
  
 Returns 0  if not valid; else returns nonzero.  
  
  
