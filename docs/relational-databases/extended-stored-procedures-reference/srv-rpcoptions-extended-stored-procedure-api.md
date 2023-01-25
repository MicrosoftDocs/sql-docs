---
title: "srv_rpcoptions (Extended Stored Procedure API)"
description: Learn how srv_rpcoptions in the Extended Stored Procedure API returns run-time options for the current remote stored procedure.
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_rpcoptions"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_rpcoptions
apitype: "DLLExport"
ms.assetid: dbcce5d1-d5a1-4379-9597-04e43af5923d
---
# srv_rpcoptions (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns run-time options for the current remote stored procedure.  
  
## Syntax  
  
```  
  
DBUSMALLINT srv_rpcoptions ( SRV_PROC *  
srvproc   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure). The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
## Returns  
 A bitmap that contains the run-time flags joined in a logical OR for the current remote stored procedure. If there is not a current remote stored procedure, 0 is returned and a message is generated.  
  
## Remarks  
 The following table describes each run-time flag.  
  
|Run-time flag|Description|  
|--------------------|-----------------|  
|SRV_NOMETADATA|The client has requested results without metadata information. This flag is only used when the client is communicating with an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. An Extended Stored Procedure API application cannot omit metadata information.|  
|SRV_RECOMPILE|The client has requested to recompile the remote stored procedure before executing it. This flag may not apply to an Extended Stored Procedure API application.|  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
