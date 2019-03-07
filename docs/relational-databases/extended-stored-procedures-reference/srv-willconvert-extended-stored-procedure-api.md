---
title: "srv_willconvert (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_willconvert"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_willconvert"
ms.assetid: 6f4db5fd-215a-461c-95e4-17697852733e
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_willconvert (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
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
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://www.microsoft.com/en-us/msrc?rtc=1).  
  
## See Also  
 [srv_convert &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-convert-extended-stored-procedure-api.md)  
  
  
