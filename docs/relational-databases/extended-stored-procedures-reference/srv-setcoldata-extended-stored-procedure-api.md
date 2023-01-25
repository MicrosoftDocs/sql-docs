---
title: "srv_setcoldata (Extended Stored Procedure API)"
description: Learn srv_setcoldata in the Extended Stored Procedure API specifies the current address for a column's data.
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_setcoldata"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_setcoldata
apitype: "DLLExport"
ms.assetid: 2e19205a-25ca-4d4a-916b-d591cf2c892b
---
# srv_setcoldata (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Specifies the current address for a column's data.  
  
## Syntax  
  
```  
  
int srv_setcoldata (  
SRV_PROC *  
srvproc  
,  
int   
column  
,  
void *  
data   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *column*  
 Indicates the number of the column the address is being specified for. Columns are numbered beginning with 1.  
  
 *data*  
 Is a pointer for a column's data. Memory allocated for *data* should not be freed until the column data is replaced by another call to **srv_setcoldata**, or until **srv_senddone** is called.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 Each column of the row must be defined first with **srv_describe**. Column data addresses are initially set with **srv_describe**. If the address of the column data changes, **srv_setcoldata** must be called to specify the new address of the data and **srv_setcoldata** must be called separately for each changed column.  
  
 Null data is represented by setting the column's length to 0 with **srv_setcollen**. The data address is then ignored.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_describe &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-describe-extended-stored-procedure-api.md)  
  
  
