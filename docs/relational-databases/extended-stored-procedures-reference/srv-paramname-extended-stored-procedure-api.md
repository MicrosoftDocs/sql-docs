---
title: "srv_paramname (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_paramname"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_paramname"
ms.assetid: 1a53d707-7b06-49cc-a0df-ac727cfe953f
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_paramname (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns the name of a remote stored procedure call parameter.  
  
## Syntax  
  
```  
  
DBCHAR * srv_paramname (  
SRV_PROC * srvproc,intn, int *len );  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *n*  
 Indicates the number of the parameter. The first parameter is 1.  
  
 *len*  
 Provides a pointer to an **int** variable that contains the length, in bytes, of the parameter name. If *len* is NULL, the length of the remote stored procedure parameter name is not returned.  
  
## Returns  
 A pointer to a null-terminated character string that contains the parameter name. The length of the parameter name is stored in *len*. If there is no *n*th parameter or no remote stored procedure, it returns NULL, *len* is set to -1, and an informational error message is sent. If the parameter name is NULL, *len* is set to 0 and a null-terminated empty string is returned.  
  
## Remarks  
 This function gets the name of a remote stored procedure call parameter. When a remote stored procedure call is made with parameters, the parameters can be passed either by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. The SRV_RPC handler is still called, but it appears as if there were no parameters, and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_rpcparams &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)  
  
  
