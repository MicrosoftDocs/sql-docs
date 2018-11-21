---
title: "srv_describe (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_describe"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_describe"
ms.assetid: 2115600e-5ce7-4be0-9cd3-a1dd1fab0729
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_describe (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Defines the column name and source and destination data types for a specific column in a row.  
  
## Syntax  
  
```  
  
int srv_describe (  
SRV_PROC *  
srvproc  
,  
int  
colnumber  
,  
DBCHAR *  
column_name  
,  
int  
namelen  
,  
DBINT  
desttype  
,  
DBINT  
destlen  
,  
DBINT  
srctype  
,  
DBINT  
srclen  
,  
void *  
srcdata  
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection (in this case, the client sending the row). The structure contains all the information that the Extended Stored Procedure API library uses to manage communications and data between the application and the client.  
  
 *colnumber*  
 Is currently not supported. Columns must be described in order. All columns must be described before **srv_sendrow** is called.  
  
 *column_name*  
 Specifies the name of the column to which the data belongs. This parameter can be NULL because a column is not required to have a name.  
  
 *namelen*  
 Specifies the length, in bytes, of *column_name*. If *namelen* is SRV_NULLTERM, then *column_name* must be null-terminated.  
  
 *desttype*  
 Specifies the data type of the destination row column. This is the data type sent to the client. The data type must be specified even if the data is NULL, for more information, see [Data Types &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md).  
  
 *destlen*  
 Specifies the length, in bytes, of the data to be sent to the client. For fixed-length data types that do not allow null values, *destlen* is ignored. For variable-length data types and fixed-length data types that allow null values, *destlen* specifies the maximum length the destination data can be.  
  
 *srctype*  
 Specifies the data type of the source data.  
  
 *srclen*  
 Specifies the length, in bytes, of the source data. This value is ignored for fixed-length data types.  
  
 *srcdata*  
 Provides the source data address for a particular column. When **srv_sendrow** is called, it looks for the data for *colnumber* at *srcdata*. Therefore it should not be freed before a call to **srv_sendrow**. The source data address can be changed between calls to **srv_sendrow** by using **srv_setcoldata**. Memory allocated for *srcdata* should not be freed until the column data is replaced by another call to **srv_setcoldata**, or until **srv_senddone** is called.  
  
 If *desttype* is SRVDECIMAL or SRVNUMERIC, the *srcdata* parameter must be a pointer to a DBNUMERIC or DBDECIMAL structure with the precision and scale fields of the structure already set to the values you want. You can use DEFAULTPRECISION to specify a default precision, and DEFAULTSCALE to specify a default scale.  
  
## Returns  
 The number of the column described. The first column is column 1. If an error occurs, returns 0.  
  
## Remarks  
 The **srv_describe** function must be called once for each column in the row before the first call to **srv_sendrow**. The columns of a row can be described in any order.  
  
 To change the location and length of the source data in column rows before the complete result set has been sent, use **srv_setcoldata** and **srv_setcollen**, respectively.  
  
 For a description of data types and Extended Store Procedure API data type conversions, see[Data Types &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md).  
  
 If the column name in your application is in Unicode, you need to convert it to the multibyte code page of the server before calling **srv_describe**. For more information, see [Unicode Data and Server Code Pages](../../relational-databases/extended-stored-procedures-programming/unicode-data-and-server-code-pages.md).  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_sendrow &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-sendrow-extended-stored-procedure-api.md)   
 [srv_setutype &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-setutype-extended-stored-procedure-api.md)   
 [srv_setcoldata &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-setcoldata-extended-stored-procedure-api.md)  
  
  
