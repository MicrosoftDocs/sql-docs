---
title: "srv_senddone (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_senddone"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_senddone"
ms.assetid: 1fc4f1d5-56d4-43f6-b5e4-0c0cc295cba3
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_senddone (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Sends a result completion message to the client.  
  
## Syntax  
  
```  
  
int srv_senddone (  
SRV_PROC *  
srvproc  
,  
DBUSMALLINT   
status  
,  
DBUSMALLINT  
info  
,  
DBINT  
count   
);  
  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the language request). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *status*  
 Is a 2-byte field for various *status* flags. Multiple flags can be set by using the AND and OR logical operators with *status* flag values. The following table lists possible *status* flags.  
  
|Status flag|Description|  
|-----------------|-----------------|  
|SRV_DONE_COUNT|The *count* parameter contains a valid count.|  
|SRV_DONE_ERROR|The current client command received an error.|  
  
 *info*  
 Is a reserved, 2-byte field. Set this value to 0.  
  
 *count*  
 Is a 4-byte field used to indicate a count for the current result set. If the SRV_DONE_COUNT flag is set in the *status* field, *count* holds a valid count.  
  
## Returns  
 SUCCEED or FAIL  
  
## Remarks  
 A client request can cause the server to execute a number of commands and to return a number of result sets. For each result set, **srv_senddone** must return a result completion message to the client.  
  
 The *count* field indicates the number of rows affected by a command. If the *count* field contains a count, the SRV_DONE_COUNT flag should be set in the *status* field. This setting allows the client to distinguish between a *count* value of 0 and an unused *count* field.  
  
 Do not call **srv_senddone** from the SRV_CONNECT handler.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
