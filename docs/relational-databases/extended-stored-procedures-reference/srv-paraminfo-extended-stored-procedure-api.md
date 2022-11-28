---
title: "srv_paraminfo (Extended Stored Procedure API)"
description: Learn how srv_paraminfo in the Extended Stored Procedure API returns information about a parameter.
author: VanMSFT
ms.author: vanto
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_paraminfo"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_paraminfo
apitype: "DLLExport"
ms.assetid: ee2afd4e-0d91-462b-9403-98d481546330
---
# srv_paraminfo (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns information about a parameter. This function supersedes the following functions: [srv_paramtype](../../relational-databases/extended-stored-procedures-reference/srv-paramtype-extended-stored-procedure-api.md), [srv_paramlen](../../relational-databases/extended-stored-procedures-reference/srv-paramlen-extended-stored-procedure-api.md), [srv_parammaxlen](../../relational-databases/extended-stored-procedures-reference/srv-parammaxlen-extended-stored-procedure-api.md), and [srv_paramdata](../../relational-databases/extended-stored-procedures-reference/srv-paramdata-extended-stored-procedure-api.md). **srv_paraminfo** supports the data types in [Data Types](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md) and zero-length data.  
  
## Syntax  
  
```  
  
int srv_paraminfo (  
SRV_PROC *  
srvproc  
,  
int  
n  
,  
BYTE *  
pbType  
,  
ULONG *  
pcbMaxLen  
,  
ULONG *  
pcbActualLen  
,  
BYTE *  
pbData  
,  
BOOL *  
pfNull  
);  
```  
  
## Arguments  
 *srvproc*  
 A handle for a client connection.  
  
 *n*  
 The ordinal number of the parameter to be set. The first parameter is 1.  
  
 *pbType*  
 The data type of the parameter.  
  
 *pcbMaxLen*  
 Pointer to the maximum length of the parameter.  
  
 *pcbActualLen*  
 Pointer to the actual length of the parameter. A value of 0 (\**pcbActualLen* == 0) signifies zero-length data if **pfNull* is set to FALSE.  
  
 *pbData*  
 Pointer to the buffer for parameter data. If *pbData* is not NULL, the Extended Store Procedure API writes \**pcbActualLen* bytes of data to \**pbData*. If *pbData* is NULL, no data is written to \**pbData* but the function returns \**pbType*, \**pcbMaxLen*, \**pcbActualLen*, and **pfNull*. The memory for this buffer must be managed by the application.  
  
 *pfNull*  
 Pointer to a null flag. **pfNull* is set to TRUE if the value of the parameter is NULL.  
  
## Returns  
 If the parameter information was successfully obtained, SUCCEED is returned; otherwise, FAIL. FAIL is returned when there is no current remote stored procedure and when there is no *n*th remote stored procedure parameter.  
  
## Remarks  
 **Security Note** You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [Extended Stored Procedures Programmer's Reference](../../relational-databases/extended-stored-procedures-reference/database-engine-extended-stored-procedures-reference.md)  
  
  
