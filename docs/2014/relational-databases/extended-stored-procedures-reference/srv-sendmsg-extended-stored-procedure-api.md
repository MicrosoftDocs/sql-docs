---
title: "srv_sendmsg (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
api_name: 
  - "srv_sendmsg"
api_location: 
  - "opends60.dll"
topic_type: 
  - "apiref"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_sendmsg"
ms.assetid: efcb50b9-f8ff-4121-bf67-05830171b928
author: rothja
ms.author: jroth
manager: craigg
---
# srv_sendmsg (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Sends a message to the client.  
  
## Syntax  
  
```  
  
int srv_sendmsg (  
SRV_PROC *  
srvproc  
,  
int  
msgtype  
,  
DBINT  
msgnum  
,  
DBTINYINT  
class  
,   
DBTINYINT  
state  
,  
DBCHAR *  
rpcname  
,  
int   
rpcnamelen  
,  
DBUSMALLINT  
linenum  
,  
DBCHAR *  
message  
,  
int  
msglen   
);  
  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the language request). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *msgtype*  
 Is either SRV_MSG_INFO or SRV_MSG_ERROR, depending on whether the server is sending an informational or error message.  
  
 *msgnum*  
 Is a 4-byte message number.  
  
 *class*  
 Specifies the error severity. A severity less than or equal to 10 is considered an informational message.  
  
 *state*  
 Provides the error state number for the current message. The error state number provides information about the context of the error. Valid state numbers are from 0 through 255.  
  
 *rpcname*  
 Is currently not supported.  
  
 *rpcnamelen*  
 Is currently not supported.  
  
 *linenum*  
 Is the line number in the language command batch where the message applies. Line numbers start at 1. If *linenum* does not apply to the message, set to 0.  
  
 *message*  
 Is a pointer to the character string to be sent to the client.  
  
 *msglen*  
 Specifies the length, in bytes, of *message*. If *message* is null-terminated, set *msglen* to SRV_NULLTERM.  
  
## Returns  
 SUCCEED or FAIL  
  
## Remarks  
 This function sends error or informational messages to the client. It is called once for each message to be sent.  
  
 Messages can be sent to the client with **srv_sendmsg** in any order before or after all rows (if any) have been sent with **srv_sendrow**. All messages, if any, must be sent to the client before the completion status is sent with **srv_senddone**.  
  
 To send messages in Unicode, use **srv_wsendmsg** rather than **srv_sendmsg**.  
  
 For more information see [Unicode Data and Server Code Pages](../extended-stored-procedures-programming/unicode-data-and-server-code-pages.md).  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
