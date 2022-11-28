---
title: "srv_paramnumber (Extended Stored Procedure API)"
description: Learn how srv_paramnumber in the Extended Stored Procedure API returns the number of a remote stored procedure call parameter.
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_paramnumber"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_paramnumber
apitype: "DLLExport"
ms.assetid: d7a6dbff-71d9-4297-8a4f-bfd2876fe204
---
# srv_paramnumber (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns the number of a remote stored procedure call parameter.  
  
## Syntax  
  
```  
  
int srv_paramnumber (  
SRV_PROC *  
srvproc  
,  
DBCHAR *  
name  
,   
int  
namelen   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *name*  
 Is a pointer to the parameter *name*.  
  
 *namelen*  
 Is the length of *name*. If *name* is null-terminated, set *namelen* to SRV_NULLTERM.  
  
## Returns  
 The parameter number of the named parameter. The first parameter is 1. If there is no parameter named *name* or no remote stored procedure, 0 is returned and a message is generated.  
  
## Remarks  
 When a remote stored procedure call is made with parameters, the parameters can be passed either by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. The SRV_RPC handler is still called, but it appears as if there were no parameters, and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_rpcparams &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)  
  
  
