---
title: "srv_paramlen (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_paramlen"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_paramlen"
ms.assetid: d1fe92ff-cad6-4396-8216-125e5642e81e
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_paramlen (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns the data length of a remote stored procedure call parameter. This function has been superseded by the **srv_paraminfo** function.  
  
## Syntax  
  
```  
  
int srv_paramlen (  
SRV_PROC *  
srvproc  
,  
int  
n   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information that the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *n*  
 Indicates the number of the parameter. The first parameter is 1.  
  
## Returns  
 The actual length, in bytes, of the parameter data. If there is no *n*th parameter or there is no remote stored procedure, it returns -1. If the *n*th parameter is NULL, it returns 0.  
  
 This function returns the following values, if the parameter is one of the following [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] system data types.  
  
|New data types|Input data length|  
|--------------------|-----------------------|  
|**BITN**|**NULL:** 1<br /><br /> **ZERO:** 1<br /><br /> **>=255:** N/A<br /><br /> **<255:** N/A|  
|**BIGVARCHAR**|**NULL:** 0<br /><br /> **ZERO:** 1<br /><br /> **>=255:** 255<br /><br /> **<255:** actual *len*|  
|**BIGCHAR**|**NULL:** 0<br /><br /> **ZERO:** 255<br /><br /> **>=255:** 255<br /><br /> **<255:** 255|  
|**BIGBINARY**|**NULL:** 0<br /><br /> **ZERO:** 255<br /><br /> **>=255:** 255<br /><br /> **<255:** 255|  
|**BIGVARBINARY**|**NULL:** 0<br /><br /> **ZERO:** 1<br /><br /> **>=255:** 255<br /><br /> **<255:** actual *len*|  
|**NCHAR**|**NULL:** 0<br /><br /> **ZERO:** 255<br /><br /> **>=255:** 255<br /><br /> **<255:** 255|  
|**NVARCHAR**|**NULL:** 0<br /><br /> **ZERO:** 1<br /><br /> **>=255:** 255<br /><br /> **<255:** actual *len*|  
|**NTEXT**|**NULL:** -1<br /><br /> **ZERO:** -1<br /><br /> **>=255:** -1<br /><br /> **\<255:** -1|  
  
 \*   actual *len* = Length of multibyte character string (cch)  
  
## Remarks  
 Each remote stored procedure parameter has an actual and a maximum data length. For standard fixed-length data types that do not allow null values, the actual and maximum lengths are the same. For variable-length data types, the lengths can vary. For example, a parameter declared as **varchar(30)** can have data that is only 10 bytes long. The parameter's actual length is 10 and its maximum length is 30. The **srv_paramlen** function gets the actual data length, in bytes, of a remote stored procedure. To obtain the maximum data length of a parameter, use **srv_parammaxlen**.  
  
 When a remote stored procedure call is made with parameters, the parameters can be passed either by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. The SRV_RPC handler is still called, but it appears as if there were no parameters and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_paraminfo &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-paraminfo-extended-stored-procedure-api.md)   
 [srv_rpcparams &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)  
  
  
