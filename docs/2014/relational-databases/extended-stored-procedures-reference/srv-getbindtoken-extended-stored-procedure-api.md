---
title: "srv_getbindtoken (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
api_name: 
  - "srv_getbindtoken"
api_location: 
  - "opends60.dll"
topic_type: 
  - "apiref"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_getbindtoken"
ms.assetid: c947d011-08ac-4fb8-b925-3da6e0999277
author: rothja
ms.author: jroth
manager: craigg
---
# srv_getbindtoken (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Obtains a bind token of the transaction in the current client session that invokes the extended stored procedure.  
  
 The extended stored procedure can then use **sp_bindsession** to bind any new session it creates against the same server to the existing transaction so that the new session can share the same transaction lock space with the client session that invoked the extended stored procedure.  
  
## Syntax  
  
```  
  
int srv_getbindtoken (  
SRV_PROC*  
srvproc  
,  
char*  
bindtoken  
);  
  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains all the information that the Extended Stored Procedure API library uses to manage communications and data between the application and the client.  
  
 *bindtoken*  
 Is a pointer to a buffer where the bind token will be copied. The bind token is represented as a null-terminated string. The buffer you specify should be at least 255 bytes in length.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
  
### To bind an extended stored procedure session to the client session that called it so they share the same transaction lock space  
  
1.  The extended stored procedure calls **svr_getbindtoken** to get the bind token for the current transaction in the session. The token is returned in the given *bindtoken* parameter.  
  
2.  The extended stored procedure opens new session(s) against the same server. Inside that session, the extended stored procedure uses the bind token with **sp_bindsession** to bind the new session to the same transaction. The extended stored procedure can create multiple sessions and bind all the sessions to the same transaction.  
  
3.  A bound session is unbound when the external stored procedure returns or when **sp_bindsession** is called with an empty string.  
  
    > [!NOTE]  
    >  Only one bound session at a time can have access to a shared connection. If one session is currently executing a statement at the server or has results pending from the server, no other sessions sharing the same bound connection can gain access to the server until the current session has finished executing the current statement. If a session attempts to gain access to the connection while the server is busy, an error is returned to the conflicting session indicating the connection is in use and the session should retry later.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [sp_bindsession &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-bindsession-transact-sql)   
 [sp_getbindtoken &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-getbindtoken-transact-sql)  
  
  
