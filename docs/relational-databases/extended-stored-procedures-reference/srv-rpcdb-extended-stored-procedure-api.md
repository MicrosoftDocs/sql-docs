---
title: "srv_rpcdb (Extended Stored Procedure API)"
description: Learn how srv_rpcdb in the Extended Stored Procedure API returns the database name component for the current remote stored procedure.
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_rpcdb"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_rpcdb
apitype: "DLLExport"
ms.assetid: d52bfd22-7a7c-4ab0-af65-df96ff359e6f
---
# srv_rpcdb (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns the database name component for the current remote stored procedure.  
  
## Syntax  
  
```  
  
DBCHAR * srv_rpcdb (  
SRV_PROC * srvproc,int *len );  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *len*  
 Is a pointer to an **int** variable that receives the length of the database name. If *len* is NULL, the length of the database name is not returned.  
  
## Returns  
 A DBCHAR pointer to the null-terminated string for the database name part of the current remote stored procedure. If there is no current remote stored procedure, NULL is returned and the *len* parameter is set to - 1.  
  
## Remarks  
 This function returns only the database component of the remote stored procedure object name. It does not include the optional specifiers for owner, remote stored procedure name, and remote stored procedure number.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
