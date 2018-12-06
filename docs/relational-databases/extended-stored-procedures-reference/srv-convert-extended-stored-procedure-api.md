---
title: "srv_convert (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
apiname: 
  - "srv_convert"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_convert"
ms.assetid: 216b4a31-786e-4361-8a33-e5f6e9790f90
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# srv_convert (Extended Stored Procedure API)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Changes data from one data type to another.  
  
## Syntax  
  
```  
  
int srv_convert (  
SRV_PROC *  
srvproc  
,  
int  
srctype  
,  
void *  
src  
,  
DBINT  
srclen  
,  
int  
desttype  
,  
void *  
  dest  
,  
DBINT  
destlen  
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains all the control information that the Extended Stored Procedure API uses to manage communications and data between the application and the client. If the *srvproc* handle is supplied, it is passed to the Extended Stored Procedure API error handler function when an error occurs.  
  
 *srctype*  
 Specifies the data type of the data to be converted. This parameter can be any of the Extended Store Procedure API data types.  
  
 *src*  
 Is a pointer to the data to be converted. This parameter can be any of the Extended Store Procedure API data types.  
  
 *srclen*  
 Specifies the length, in bytes, of the data to be converted. If *srclen* is 0, **srv_convert** places a null value in the destination variable. Unless it is 0, this parameter is ignored for fixed-length data types, in which case the source data is assumed to be NULL. For data of the SRVCHAR data type, a length of -1 indicates the string is null-terminated.  
  
 *desttype*  
 Specifies the data type to convert the source to. This parameter can be any of the Extended Store Procedure API data types.  
  
 *dest*  
 Is a pointer to the destination variable that receives converted data. If this pointer is NULL, **srv_convert** calls the user-supplied error handler ,if any, and returns -1.  
  
 If *desttype* is SRVDECIMAL or SRVNUMERIC, the *dest* parameter must be a pointer to a DBNUMERIC or DBDECIMAL structure with the precision and scale fields of the structure already set to the desired values. You can use DEFAULTPRECISION to specify a default precision, and DEFAULTSCALE to specify a default scale.  
  
 *destlen*  
 Specifies the length, in bytes, of the destination variable. This parameter is ignored for fixed-length data types. For a destination variable of type SRVCHAR, the value of *destlen* must be the total length of the destination buffer space. A length of -1 for a destination variable of type SRVCHAR or SRVBINARY indicates there is sufficient space available. For a destination variable of type *srvchar*, a length of -1 causes the character string to be null-terminated.  
  
## Returns  
 The length of the converted data, in bytes, if the data type conversion succeeds. When **srv_convert** encounters a request for a conversion it does not support, it calls the developer-supplied error handler, if any, sets a global error number, and returns -1.  
  
## Remarks  
 The **srv_willconvert** function determines whether a particular conversion is allowed.  
  
 Converting to the approximate numeric data types SRVFLT4 or SRVFLT8 can result in some loss of precision. Converting from the approximate numeric data types SRVFLT4 or SRVFLT8 to SRVCHAR or SRVTEXT can also result in some loss of precision.  
  
 Converting to SRVFLT*x*, SRVINT*x*, SRVMONEY, SRVMONEY4, SRVDECIMAL, or SRVNUMERIC can result in overflow if the number is larger than the maximum value of the destination, or in underflow if the number is smaller than the minimum value of the destination. If overflow occurs when converting to SRVCHAR or SRVTEXT, the first character of the resulting value contains an asterisk (*) to indicate the error.  
  
 When converting SRVCHAR to SRVBINARY, **srv_convert** interprets SRVCHAR as hexadecimal, whether or not the string contains a leading 0. When converting SRVBINARY to SRVCHAR, **srv_convert** creates a hexadecimal string without a leading 0. In all other cases, a conversion to or from the SRVBINARY data type is a straight bit-copy.  
  
 In certain cases, it can be useful to convert a data type to itself. For example, converting SRVCHAR to SRVCHAR with a *destlen* of -1 adds a null terminator to a string.  
  
 For a description of data types and Extended Store Procedure API data type conversions, see [Data Types &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md).  
  
 The **srv_convert** function can fail for several reasons:  
  
-   The requested conversion is not available.  
  
-   The conversion resulted in truncation, overflow, or loss of precision in the destination variable.  
  
-   A syntax error occurred while converting a character string to a numeric data type.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## See Also  
 [srv_setutype &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-setutype-extended-stored-procedure-api.md)   
 [srv_willconvert &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-willconvert-extended-stored-procedure-api.md)  
  
  
