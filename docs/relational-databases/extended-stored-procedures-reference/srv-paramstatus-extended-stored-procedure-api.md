---
title: "srv_paramstatus (Extended Stored Procedure API)"
description: Learn how srv_paramstatus returns the status of a particular remote stored procedure call parameter.
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_paramstatus"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_paramstatus
apitype: "DLLExport"
ms.assetid: 86cecd45-0b09-42e9-8152-32a12a1c2b7a
---
# srv_paramstatus (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns the status of a particular remote stored procedure call parameter.  
  
## Syntax  
  
```  
  
int srv_paramstatus (  
SRV_PROC *  
srvproc  
,  
int  
n   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *n*  
 Indicates the number of the parameter. The first parameter is number 1.  
  
## Returns  
 An **int** that contains status flags for the parameter. Currently, there is only one flag: If bit 0 is set to 1, the parameter is a return parameter. If there is no *n*th parameter or if there is no remote stored procedure, it returns -1.  
  
## Remarks  
 This routine returns the status flags for a remote stored procedure call parameter.  
  
 Parameters contain data passed between clients and the application with remote stored procedures. The client can specify certain parameters as return parameters. These return parameters can contain values that the application passes back to the client.  
  
 Currently, the only status flag is one that indicates whether the parameter is a return parameter.  
  
 When a remote stored procedure call is made with parameters, the parameters can be passed either by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. If an error occurs, the SRV_RPC handler is still called, but it appears as if there were no parameters, and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_rpcparams &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)  
  
  
