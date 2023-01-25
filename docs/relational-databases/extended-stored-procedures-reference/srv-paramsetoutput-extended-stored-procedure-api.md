---
title: "srv_paramsetoutput (Extended Stored Procedure API)"
description: Learn how srv_paramsetoutput in the Extended Stored Procedure API sets the value of a return parameter.
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "srv_paramsetoutput"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_paramsetoutput
apitype: "DLLExport"
ms.assetid: f2810e19-e513-458b-8925-5756b6ee1313
---
# srv_paramsetoutput (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Sets the value of a return parameter. This function supersedes the **srv_paramset** function.  
  
## Syntax  
  
```  
  
int srv_paramsetoutput (  
SRV_PROC *  
srvproc  
,  
int  
n  
,  
BYTE *  
pbData  
,  
ULONG   
cbLen  
,  
BOOL  
fNull   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a handle for a client connection.  
  
 *n*  
 Is the ordinal number of the parameter to be set. The first parameter is 1.  
  
 *pbData*  
 Is a pointer to the data value to be sent back to the client as a procedure return parameter.  
  
 *cbLen*  
 Is the actual length of the data to be returned. If the data type of the parameter specifies values of a constant length and does not allow null values (for example, *srvbit* or *srvint1*), *cbLen* is ignored. A value of 0 signifies zero-length data if *fNull* is FALSE.  
  
 *fNull*  
 Is a flag indicating whether the value of the return parameter is NULL. Set this flag to TRUE if the parameter should be set to NULL. The default value is FALSE. If *fNull* is set to TRUE, *cbLen* should be set to 0 or the function will fail.  
  
## Returns  
 If the parameter information was successfully set, SUCCEED is returned; otherwise, FAIL. FAIL is returned when  
  
-   the parameter is not a return parameter, or  
  
-   the *cbLen* argument is invalid.  
  
## Remarks  
 **Security Note** You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
