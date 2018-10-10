---
title: "ConnectionValidSharedMemory function in dbmslpcn.dll Shared Memory | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 6ae35826-7d75-4542-b686-5f79316b6157
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ConnectionValidSharedMemory function in dbmslpcn.dll Shared Memory
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

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
  
  
