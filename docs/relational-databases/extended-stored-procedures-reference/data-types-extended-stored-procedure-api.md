---
title: "Data Types (Extended Stored Procedure API)"
description: Learn about how Extended Stored Procedure API data types can be expanded by including the Srv.h header file in your program.
author: VanMSFT
ms.author: vanto
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "extended stored procedures [SQL Server], data types"
  - "data types [SQL Server], extended stored procedures"
ms.assetid: 37fb86b9-8819-4387-bcdc-9616968e15ad
---
# Data Types (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 To use the Extended Stored Procedure API data types, include the Srv.h header file in your program.  
  
|Data type|SQL Server data type|Description|  
|---------------|--------------------------|-----------------|  
|SRVBIGBINARY|**binary**|**binary** data type, length 0 to 8000 bytes.|  
|SRVBIGCHAR|**char**|**character** data type, length 0 to 8000 bytes.|  
|SRVBIGVARBINARY|**varbinary**|Variable-length **binary** data type, length 0 to 8000 bytes.|  
|SRVBIGVARCHAR|**varchar**|Variable-length **character** data type, length 0 to 8000 bytes.|  
|SRVBINARY|**binary**|**binary** data type.|  
|SRVBIT|**Bit**|**bit** data type.|  
|SRVBITN|**bit null**|**bit** data type, null values allowed.|  
|SRVCHAR|**char**|**character** data type.|  
|SRVDATETIME|**datetime**|8-byte **datetime** data type.|  
|SRVDATETIM4|**smalldatetime**|4-byte **smalldatetime** data type.|  
|SRVDATETIMN|**datetime null**|**smalldatetime** or **datetime** data type, null values allowed.|  
|SRVDECIMAL|**decimal**|**decimal** data type.|  
|SRVDECIMALN|**decimal null**|**decimal** data type, null values allowed.|  
|SRVFLT4|**real**|4-byte **real** data type.|  
|SRVFLT8|**float**|8-byte **float** data type.|  
|SRVFLTN|**real** &#124; **float null**|**real** or **float** data type, null values allowed.|  
|SRVIMAGE|**image**|**image** data type.|  
|SRVINT1|**tinyint**|1-byte **tinyint** data type.|  
|SRVINT2|**smallint**|2-byte **smallint** data type.|  
|SRVINT4|**int**|4-byte **int** data type.|  
|SRVINTN|**tinyint** &#124; **smallint** &#124; **int null**|**tinyint**, **smallint**, or **int** data type, null values allowed.|  
|SRVMONEY4|**smallmoney**|4-byte **smallmoney** data type.|  
|SRVMONEY|**money**|8-byte **money** data type.|  
|SRVMONEYN|**money** &#124; **smallmoney null**|**smallmoney** or **money** data type, null values allowed.|  
|SRVNCHAR|**nchar**|Unicode **character** data type.|  
|SRVNTEXT|**ntext**|Unicode **text** data type.|  
|SRVNUMERIC|**numeric**|**numeric** data type.|  
|SRVNUMERICN|**numeric null**|**numeric** data type, null values allowed.|  
|SRVNVARCHAR|**nvarchar**|Unicode variable-length **character** data type.|  
|SRVTEXT|**text**|**text** data type.|  
|SRVVARBINARY|**varbinary**|Variable-length **binary** data type.|  
|SRVVARCHAR|**varchar**|Variable-length **character** data type.|  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
