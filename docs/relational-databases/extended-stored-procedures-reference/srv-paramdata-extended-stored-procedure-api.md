---
title: "srv_paramdata (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_paramdata"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_paramdata"
ms.assetid: 3104514d-b404-47c9-b6d7-928106384874
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_paramdata (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns the value of a remote stored procedure call parameter. This function has been superseded by the **srv_paraminfo** function.  
  
## Syntax  
  
```  
  
void * srv_paramdata (  
SRV_PROC *  
srvproc  
,  
int  
n   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information the Extended Stored Procedure library uses to manage communication and data between the application and the client.  
  
 *n*  
 Is the number of the parameter. The first parameter is number 1.  
  
## Returns  
 A pointer to the parameter value. If the *n*th parameter is NULL, there is no *n*th parameter, or there is no remote stored procedure, returns NULL. If the parameter value is a string, it might not be null-terminated. Use **srv_paramlen** to determine the length of the string.  
  
 This function returns the following values, if the parameter is one of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types. Pointer data includes whether the pointer for the data type is valid (VP), NULL, or not applicable (N/A), and the contents of the data pointed to.  
  
|New data types|Input data length|  
|--------------------|-----------------------|  
|BITN|**NULL:** VP, NULL<br /><br /> **ZERO:** VP, NULL<br /><br /> **>=255:** N/A<br /><br /> **<255:** N/A|  
|BIGVARCHAR|**NULL:** NULL, N/A<br /><br /> **ZERO:** VP, NULL<br /><br /> **>=255:** VP, 255 chars<br /><br /> **<255:** VP, actual data|  
|BIGCHAR|**NULL:** NULL, N/A<br /><br /> **ZERO:** VP, 255 spaces<br /><br /> **>=255:** VP, 255 chars<br /><br /> **<255:** VP, actual data + padding (up to 255)|  
|BIGBINARY|**NULL:** NULL, N/A<br /><br /> **ZERO:** VP, 255 0x00<br /><br /> **>=255:** VP, 255 bytes<br /><br /> **<255:** VP, actual data + padding (up to 255)|  
|BIGVARBINARY|**NULL:** NULL, N/A<br /><br /> **ZERO:** VP, 0x00<br /><br /> **>=255:** VP, 255 bytes<br /><br /> **<255:** VP, actual data|  
|NCHAR|**NULL:** NULL, N/A<br /><br /> **ZERO:** VP, 255 spaces<br /><br /> **>=255:** VP, 255 chars<br /><br /> **<255:** VP, actual data + padding (up to 255)|  
|NVARCHAR|**NULL:** NULL, N/A<br /><br /> **ZERO:** VP, NULL<br /><br /> **>=255:** VP, 255 chars<br /><br /> **<255:** VP, actual data|  
|NTEXT|**NULL:** N/A<br /><br /> **ZERO:** N/A<br /><br /> **>=255:** N/A<br /><br /> **\<255:** N/A|  
  
 \*   data is not null-terminated; no warning is issued on truncation for data >255 characters.  
  
## Remarks  
 If you know the parameter name, you can use **srv_paramnumber** to get the parameter number. To determine whether a parameter is NULL, use **srv_paramlen**.  
  
 When a remote stored procedure call is made with parameters, the parameters can be passed by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. If an error occurs, the SRV_RPC handler is still called, but it appears as if there were no parameters and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_rpcparams &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)  
  
  
