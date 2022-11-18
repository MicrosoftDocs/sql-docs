---
title: "srv_paramtype (Extended Stored Procedure API)"
description: Learn how srv_paramtype in the Extended Stored Procedure API returns the data type of a remote stored procedure call parameter.
author: VanMSFT
ms.author: vanto
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_paramtype"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_paramtype
apitype: "DLLExport"
ms.assetid: badc6d36-8a87-42b5-b28c-9c4f5ded8552
---
# srv_paramtype (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns the data type of a remote stored procedure call parameter.  
  
## Syntax  
  
```  
  
int srv_paramtype (  
SRV_PROC *  
srvproc  
,  
int  
n   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *n*  
 Indicates the number of the parameter. The first parameter is 1.  
  
## Returns  
 A token value for the data type of the parameter. For information about data types, see [Data Types &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md). If there is no *n*th parameter or if there is no remote stored procedure, it returns - 1.  
  
 This function returns the following values, if the parameter is one of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] data types.  
  
|New data types|Return value|  
|--------------------|------------------|  
|**BITN**|SRVBIT|  
|**BIGVARCHAR**|VARCHAR|  
|**BIGCHAR**|CHAR|  
|**BIGBINARY**|BINARY|  
|**BIGVARBINARY**|VARBINARY|  
|**NCHAR**|CHAR|  
|**NVARCHAR**|VARCHAR|  
|**NTEXT**|-1|  
  
## Remarks  
 When a remote stored procedure call is made with parameters, the parameters can be passed either by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. The SRV_RPC handler is still called, but it appears as if there were no parameters and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://www.microsoft.com/msrc?rtc=1).  
  
## See Also  
 [srv_paraminfo &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-paraminfo-extended-stored-procedure-api.md)   
 [srv_rpcparams &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)  
  
  
