---
title: "srv_willconvert (Extended Stored Procedure API)"
description: Learn how srv_willconvert determines whether a specific data type conversion is available within the ODS Library.
author: VanMSFT
ms.author: vanto
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_willconvert"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_willconvert
apitype: "DLLExport"
ms.assetid: 6f4db5fd-215a-461c-95e4-17697852733e
---
# srv_willconvert (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Determines whether a specific data type conversion is available within the ODS Library.  
  
## Syntax  
  
```  
  
BOOL srv_willconvert (  
int  
srctype  
,  
int  
desttype   
);  
```  
  
## Arguments  
 *srctype*  
 Indicates the data type of the data to be converted. This parameter can be any of the Extended Stored Procedure API data types.  
  
 *desttype*  
 Indicates the data type to which the source data is converted. This parameter can be any of the Extended Stored Procedure API data types.  
  
## Returns  
 TRUE if the data type conversion is supported; FALSE if the data type conversion is not supported.  
  
## Remarks  
 For a description of each data type, see [Data Types &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md).  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://www.microsoft.com/msrc?rtc=1).  
  
## See Also  
 [srv_convert &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-convert-extended-stored-procedure-api.md)  
  
  
