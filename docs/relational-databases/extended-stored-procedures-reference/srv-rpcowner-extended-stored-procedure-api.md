---
title: "srv_rpcowner (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_rpcowner"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_rpcowner"
ms.assetid: e81a60e6-14ea-47bc-a11c-3d7635344447
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_rpcowner (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns the owner component for the current remote stored procedure.  
  
## Syntax  
  
```  
  
DBCHAR * srv_rpcowner (  
SRV_PROC *  
srvproc  
,  
int *  
len   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *len*  
 Is a pointer to an integer variable that receives the length of the owner name. The parameter *len* can be NULL, in which case the length of the owner component is not returned.  
  
## Returns  
 A DBCHAR pointer to the null-terminated owner component for the current remote stored procedure. If there is no current remote stored procedure, NULL is returned and *len* is set to - 1.  
  
## Remarks  
 This function returns only the owner component of the remote stored procedure. It does not include the optional specifiers for name, remote stored procedure name, and remote stored procedure number.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
