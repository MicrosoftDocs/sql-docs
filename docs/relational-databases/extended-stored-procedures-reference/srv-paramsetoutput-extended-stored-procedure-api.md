---
title: "srv_paramsetoutput (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_paramsetoutput"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_paramsetoutput"
ms.assetid: f2810e19-e513-458b-8925-5756b6ee1313
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_paramsetoutput (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Sets the value of a return parameter. This function supersedes the **srv_paramset** function.  
  
## Syntax  
  
```  
  
int srv_paramsetoutput (  
SRV_PROC *  
srvproc  
,  
int  
n  
,  
BYTE *  
pbData  
,  
ULONG   
cbLen  
,  
BOOL  
fNull   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a handle for a client connection.  
  
 *n*  
 Is the ordinal number of the parameter to be set. The first parameter is 1.  
  
 *pbData*  
 Is a pointer to the data value to be sent back to the client as a procedure return parameter.  
  
 *cbLen*  
 Is the actual length of the data to be returned. If the data type of the parameter specifies values of a constant length and does not allow null values (for example, *srvbit* or *srvint1*), *cbLen* is ignored. A value of 0 signifies zero-length data if *fNull* is FALSE.  
  
 *fNull*  
 Is a flag indicating whether the value of the return parameter is NULL. Set this flag to TRUE if the parameter should be set to NULL. The default value is FALSE. If *fNull* is set to TRUE, *cbLen* should be set to 0 or the function will fail.  
  
## Returns  
 If the parameter information was successfully set, SUCCEED is returned; otherwise, FAIL. FAIL is returned when  
  
-   the parameter is not a return parameter, or  
  
-   the *cbLen* argument is invalid.  
  
## Remarks  
 **Security Note** You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
