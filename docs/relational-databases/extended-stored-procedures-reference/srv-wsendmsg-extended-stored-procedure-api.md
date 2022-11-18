---
title: "srv_wsendmsg (Extended Stored Procedure API)"
description: Learn how srv_wsendmsg in the Extended Stored Procedure API can send a Unicode message to the client.
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_wsendmsg"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_wsendmsg
apitype: "DLLExport"
ms.assetid: f2153076-32c9-4a52-8e1b-fc9618153543
---
# srv_wsendmsg (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Sends a Unicode message to the client.  
  
## Syntax  
  
```  
  
int srv_wsendmsg(SRV_PROC *   
srvproc  
, int   
msgnum  
, int   
severity  
, WCHAR *   
message  
, int   
msglen  
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *Msgnum*  
 Is a 4-byte message number.  
  
 *Severity*  
 Specifies the severity of the error. A severity less than or equal to 10 is considered an informational message; otherwise, it is an error.  
  
 *message*  
 Is a pointer to a Unicode string to be sent to the client.  
  
 *msglen*  
 Specifies the length, in characters, of *message*.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 Use this function to send messages in Unicode. It is similar to **srv_sendmsg**, but the message it sends is a WCHAR string rather than type DBCHAR string. Note that message length is reported in characters rather than bytes, and *msglen* will never be equal to SRV_NULLTERM.  
  
 The function returns FAIL when  
  
-   The *msglen* given is not in the range of 0-32242.  
  
-   The *msglen* given is 0 but the message pointer is NULL.  
  
-   There is error when sending the error message through network.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_sendmsg &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-sendmsg-extended-stored-procedure-api.md)  
  
  
