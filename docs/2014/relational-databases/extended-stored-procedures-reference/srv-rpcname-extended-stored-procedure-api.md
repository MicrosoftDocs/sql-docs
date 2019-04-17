---
title: "srv_rpcname (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
api_name: 
  - "srv_rpcname"
api_location: 
  - "opends60.dll"
topic_type: 
  - "apiref"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_rpcname"
ms.assetid: 0a1424e4-3319-4836-b8d8-5e0344cc683f
author: rothja
ms.author: jroth
manager: craigg
---
# srv_rpcname (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns the procedure name component for the current remote stored procedure.  
  
## Syntax  
  
```  
  
DBCHAR * srv_rpcname (  
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
 Is a pointer to an integer variable that receives the length of the database name. If *len* is NULL, the length of the remote stored procedure name is not returned.  
  
## Returns  
 A DBCHAR pointer to the null-terminated string for the remote stored procedure name component of the current remote stored procedure. If there is not a current remote stored procedure, NULL is returned and *len* is set to -1.  
  
## Remarks  
 This function returns only the name of the remote stored procedure. It does not include the optional specifiers for owner, database name, and remote stored procedure number.  
  
 Because it is valid to call **srv_rpcname** when there is not a remote stored procedure (no informational error occurs), this function provides a method for determining whether a remote stored procedure exists.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
