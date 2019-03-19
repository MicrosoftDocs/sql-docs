---
title: "srv_paramset (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
api_name: 
  - "srv_paramset"
api_location: 
  - "opends60.dll"
topic_type: 
  - "apiref"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_paramset"
ms.assetid: 2a509206-a1b8-4b20-b0a2-ef680cef7bd8
author: rothja
ms.author: jroth
manager: craigg
---
# srv_paramset (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Sets the value of a remote stored procedure call return parameter. This function has been superseded by the **srv_paramsetoutput** function.  
  
## Syntax  
  
```  
  
int srv_paramset (  
SRV_PROC *  
srvproc  
,  
int  
n  
,   
void *  
data  
,  
int  
len   
);  
  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the handle that received the remote stored procedure call). The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *n*  
 Indicates the number of the parameter to set. The first parameter is 1.  
  
 *data*  
 Is a pointer to the data value to be sent back to the client as the remote stored procedure return parameter.  
  
 *len*  
 Specifies the actual length of the data to be returned. If the data type of the parameter is of a constant length and does not allow null values (for example, *srvbit* or *srvint1*), *len* is ignored.  
  
## Returns  
 SUCCEED if the parameter value was successfully set; otherwise, FAIL. FAIL is returned when there is no current remote stored procedure, when there is no *n*th remote stored procedure parameter, when the parameter is not a return parameter, and when the *len* argument is not legal.  
  
 If *len*is 0, it returns NULL. Setting *len* to 0 is the only way to return NULL to the client.  
  
 This function returns the following values, if the parameter is one of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] data types.  
  
|New data types|Return data length|  
|--------------------|------------------------|  
|`BITN`|**NULL:** *len* = 0, data = IG, RET = 0<br /><br /> **ZERO:** N/A<br /><br /> **>=255:** N/A<br /><br /> **<255:** N/A|  
|`BIGVARCHAR`|**NULL:** *len* = 0, data = IG, RET = 1<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = max8k, data = valid, RET = 0<br /><br /> **<255:** *len* = <8k, data = valid, RET = 1|  
|`BIGCHAR`|**NULL:** *len* = 0, data = IG, RET = 1<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = max8k, data = valid, RET = 0<br /><br /> **<255:** *len* = <8k, data = valid, RET = 1|  
|`BIGBINARY`|**NULL:** *len* = 0, data = IG, RET = 1<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = max8k, data = valid, RET = 0<br /><br /> **<255:** *len* = <8k, data = valid, RET = 1|  
|`BIGVARBINARY`|**NULL:** *len* = 0, data = IG, RET = 1<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = max8k, data = valid, RET = 0<br /><br /> **<255:** *len* = <8k, data = valid, RET = 1|  
|NCHAR|**NULL:** *len* = 0, data = IG, RET = 1<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = max8k, data = valid, RET = 0<br /><br /> **<255:** *len* = <8k, data = valid, RET = 1|  
|NVARCHAR|**NULL:** *len* = 0, data = IG, RET = 1<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = max8k, data = valid, RET = 0<br /><br /> **<255:** *len* = <8k, data = valid, RET = 1|  
|`NTEXT`|**NULL:** *len* = IG, data = IG, RET = 0<br /><br /> **ZERO:** *len* = IG, data = IG, RET = 0<br /><br /> **>=255:** *len* = IG, data = IG, RET = 0<br /><br /> **\<255:** *len* = IG, data = IG, RET = 0|  
|RET = Return value of srv_paramset||  
|IG = Value will be ignored||  
|valid = Any valid pointer to data||  
  
## Remarks  
 Parameters contain data passed between clients and the application with remote stored procedures. The client can specify certain parameters as return parameters. These return parameters can contain values that the Open Data Services server application passes back to the client. Using return parameters is analogous to passing parameters by reference.  
  
 You cannot set the return value for a parameter that wasn't invoked as a return parameter. You can use **srv_paramstatus** to determine how the parameter was invoked.  
  
 This function sets the return value for a parameter but it does not actually send the return value to the client. All return parameters, whether their return values have been set with **srv_paramset** or not, are automatically sent to the client when **srv_senddone** is called with the status flag SRV_DONE_FINAL set.  
  
 When a remote stored procedure call is made with parameters, the parameters can be passed either by name or by position (unnamed). If the remote stored procedure call is made with some parameters passed by name and some passed by position, an error occurs. The SRV_RPC handler is still called, but it appears as if there were no parameters, and **srv_rpcparams** returns 0.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_paramsetoutput &#40;Extended Stored Procedure API&#41;](srv-paramsetoutput-extended-stored-procedure-api.md)  
  
  
