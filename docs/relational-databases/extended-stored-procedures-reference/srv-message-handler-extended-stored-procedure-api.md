---
title: "srv_message_handler (Extended Stored Procedure API)"
description: Learn about srv_message_handler and how it calls the installed Extended Stored Procedure API message handler.
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "srv_message_handler"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_message_handler
apitype: "DLLExport"
ms.assetid: 41bcd057-436f-4fa8-8293-fc8057a30877
---
# srv_message_handler (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Calls the installed Extended Stored Procedure API message handler. This function is usually used to call [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from an extended stored procedure to log an error (defined by the extended stored procedure) in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log file or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log.  
  
## Syntax  
  
```  
  
int srv_message_handler (  
SRV_PROC *  
srvproc  
,  
int  
errornum  
,  
BYTE   
severity  
,  
BYTE  
state  
,  
int  
oserrnum  
,  
char *  
errtext  
,  
int  
errtextlen  
,  
char *  
oserrtext  
,  
int  
oserrtextlen  
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The *srvproc* parameter contains information that is used to manage communication and data between the application and the client.  
  
 *errornum*  
 Is an error number defined by the extended stored procedure. This number must be from 50,001 through 2,147,483,647.  
  
 *severity*  
 Is a standard [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] severity value for the error. This number must be from 0 through 24.  
  
 *state*  
 Is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] state value for the error.  
  
 *oserrnum*  
 Is the operating-system error number. This argument is ignored.  
  
 *errtext*  
 Is the description of the extended stored procedure error *errornum*.  
  
 *errtextlen*  
 Is the length of the extended stored procedure error string *errtext*.  
  
 *oserrtext*  
 Is the description of the operating-system error *oserrnum*. This argument is ignored.  
  
 *oserrtextlen*  
 Is the length of the operating-system error string *oserrtext*.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 The **srv_message_handler** function enables an extended stored procedure to integrate with the centralized error logging and reporting features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] alerts can be established for events from extended stored procedures, and SQL Server Agent will monitor for these alert conditions.  
  
 If the error message is longer, it is truncated to 412 bytes.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://msdn.microsoft.com/security/).  
  
  
