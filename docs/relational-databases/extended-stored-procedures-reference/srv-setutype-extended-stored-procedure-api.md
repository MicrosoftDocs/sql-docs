---
title: "srv_setutype (Extended Stored Procedure API)"
description: Learn about srv_setutype. srv_setutype sets the user-defined data type for a column in a row.
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_setutype"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_setutype
apitype: "DLLExport"
ms.assetid: 6160f15d-1b68-411e-ab6d-491ec288f264
---
# srv_setutype (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Sets the user-defined data type for a column in a row.  
  
## Syntax  
  
```  
  
int srv_setutype (  
SRV_PROC *  
srvproc  
,  
int   
column  
,   
DBINT  
user_type   
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *column*  
 Indicates which column to set. Columns are numbered beginning with 1.  
  
 *user_type*  
 Specifies the user-defined data type code.  
  
## Returns  
 SUCCEED or FAIL. Returns FAIL if the column does not exist.  
  
## Remarks  
 A column has two data types: its actual data type and its user-defined data type. The user-defined data type is used by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store the actual user-defined data type of the column, if any, and column description information, such as nullability and updatability, for the column.  
  
 The **srv_setutype** function can be called any time that *column* has been defined with **srv_describe** and before the last row has been sent.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://www.microsoft.com/msrc?rtc=1).  
  
## See Also  
 [srv_describe &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-describe-extended-stored-procedure-api.md)  
  
  
