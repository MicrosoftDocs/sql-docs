---
title: "srv_sendrow (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_sendrow"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_sendrow"
ms.assetid: a08f608a-10e6-4bff-9b48-0d02e8026cdb
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_sendrow (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Transmits a row of data to the client.  
  
## Syntax  
  
```  
  
int srv_sendrow ( SRV_PROC *  
srvproc   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the language request). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 The **srv_sendrow** function is called once for each row sent to the client. All rows must be sent to the client before any messages, status values, or completion statuses are sent with **srv_sendmsg**, **srv_status**, or **srv_senddone**.  
  
 Sending a row that has not had all its columns defined with **srv_describe** causes the Extended Stored Procedure API application to raise an informational error message and return FAIL to the client. In this case, the row is not sent.  
  
> [!NOTE]  
>  The Extended Stored Procedure API does not support sending compute rows to the client. Also, if a row containing **ntext**, **text**, or **image** data is sent to the client, the text pointer and text timestamp are not included.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_describe &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-describe-extended-stored-procedure-api.md)  
  
  
