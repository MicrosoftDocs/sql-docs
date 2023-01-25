---
title: "srv_rpcowner (Extended Stored Procedure API)"
description: Learn how srv_rpcowner in the Extended Stored Procedure API returns the owner component for the current remote stored procedure.
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_rpcowner"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_rpcowner
apitype: "DLLExport"
ms.assetid: e81a60e6-14ea-47bc-a11c-3d7635344447
---
# srv_rpcowner (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns the owner component for the current remote stored procedure.  
  
## Syntax  
  
```  
  
DBCHAR * srv_rpcowner (  
SRV_PROC *  
srvproc  
,  
int *  
len   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *len*  
 Is a pointer to an integer variable that receives the length of the owner name. The parameter *len* can be NULL, in which case the length of the owner component is not returned.  
  
## Returns  
 A DBCHAR pointer to the null-terminated owner component for the current remote stored procedure. If there is no current remote stored procedure, NULL is returned and *len* is set to - 1.  
  
## Remarks  
 This function returns only the owner component of the remote stored procedure. It does not include the optional specifiers for name, remote stored procedure name, and remote stored procedure number.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
