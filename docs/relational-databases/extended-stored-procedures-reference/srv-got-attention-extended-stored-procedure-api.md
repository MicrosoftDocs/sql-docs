---
title: "srv_got_attention (Extended Stored Procedure API)"
description: Learn how srv_got_attention checks if the current connection or task needs to be aborted and returns TRUE if the connection is killed or the batch is aborted.
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_got_attention"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_got_attention
apitype: "DLLExport"
ms.assetid: 805e68e1-d17f-41bd-8b9f-a27283bb6fbe
---
# srv_got_attention (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Checks whether the current connection or task needs to be aborted and returns TRUE if the connection is killed or the batch is aborted  
  
## Syntax  
  
```  
  
BOOL srv_got_attention (SRV_PROC *   
srvproc  
);  
```  
  
#### Parameters  
 *srvproc*  
 Pointer identifying a database connection.  
  
## Return Value  
 TRUE if the connection is killed or the batch is aborted. FALSE if the connection or batch is active.  
  
## Remarks  
 A long-running extended stored procedure should check the server attention by calling **srv_got_attention** periodically so that the procedure may terminate itself when the connection is killed or the batch is aborted.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
