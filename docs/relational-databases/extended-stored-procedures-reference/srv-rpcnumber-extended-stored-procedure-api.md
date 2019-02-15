---
title: "srv_rpcnumber (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_rpcnumber"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_rpcnumber"
ms.assetid: 3094085e-fe9e-423d-bf87-7852352c2d26
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_rpcnumber (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns the number component for the current remote stored procedure call.  
  
## Syntax  
  
```  
  
int srv_rpcnumber ( SRV_PROC *  
srvproc   
)  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
## Returns  
 The number component for the current remote stored procedure. If the client does not use a number component when running the remote stored procedure or if there is no current remote stored procedure, it returns - 1.  
  
## Remarks  
 This function returns only the number component of the remote stored procedure. It does not include the optional specifiers for owner, remote stored procedure name, and database name.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
