---
title: "srv_setcollen (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
api_name: 
  - "srv_setcollen"
api_location: 
  - "opends60.dll"
topic_type: 
  - "apiref"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_setcollen"
ms.assetid: 3c60f1c3-4562-463a-a259-12df172788bd
author: rothja
ms.author: jroth
manager: craigg
---
# srv_setcollen (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Specifies the current data length in bytes of a variable-length column or a column that allows NULL values.  
  
## Syntax  
  
```  
  
int srv_setcollen (  
SRV_PROC *  
srvproc  
,  
int   
column  
,  
int  
len   
);  
  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *column*  
 Indicates the number of the column for which the data length is being specified. Columns are numbered beginning with 1.  
  
 *len*  
 Indicates the length, in bytes, of the column data. A length of 0 means the column data value is null.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 Each column of the row must first be defined with **srv_describe**. The column data length is set by the last call to **srv_describe** or **srv_setcollen**. If variable-length data (null-terminated data) changes for a row, **srv_setcollen** must be used to set it to the new length before calling **srv_sendrow**. For a column that allows null values, **srv_describe** must have been called with *desttype* set to a data type that allows nulls (like SRVINTN) and null data is specified by calling **srv_setcollen** with *len* set to 0. Zero length data cannot be specified using the Extended Stored Procedure API.  
  
 Note that when the data type of the column is variable-length, *len* is not checked. This function returns FAIL if called for a fixed-length column.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_describe &#40;Extended Stored Procedure API&#41;](srv-describe-extended-stored-procedure-api.md)  
  
  
