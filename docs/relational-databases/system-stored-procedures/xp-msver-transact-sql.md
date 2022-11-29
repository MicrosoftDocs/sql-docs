---
description: "xp_msver (Transact-SQL)"
title: "xp_msver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "xp_msver_TSQL"
  - "xp_msver"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xp_msver"
ms.assetid: 9264cf8c-92ba-45ad-b2d6-15d26d805a16
author: markingmyname
ms.author: maghan
---
# xp_msver (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns version information about [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. **xp_msver** also returns information about the actual build number of the server and information about the server environment. The information that **xp_msver** returns can be used within [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, batches, stored procedures, and so on, to enhance logic for platform-independent code.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
xp_msver [ optname ]  
```  
  
## Arguments  
 *optname*  
 Is the name of an option, and can be one of the following values.  
  
|Option/Column name|Description|  
|-------------------------|-----------------|  
|**ProductName**|Product name; for example, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**ProductVersion**|Product version.|  
|**Language**|The language version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Platform**|Operating-system name, manufacturer name, and chip family name for the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Comments**|Miscellaneous information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**CompanyName**|Company name that produces [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; for example, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Corporation.|  
|**FileDescription**|The operating system.|  
|**FileVersion**|Version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable.|  
|**InternalName**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] internal name for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; for example, SQLSERVR.|  
|**LegalCopyright**|Legal copyright information required for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; for example, Copyright© [!INCLUDE[msCoName](../../includes/msconame-md.md)] Corp. 1988-2005.|  
|**LegalTrademarks**|Legal trademark information required for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, [!INCLUDE[msCoName](../../includes/msconame-md.md)] is a registered trademark of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Corporation.|  
|**OriginalFilename**|File name executed at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup; for example, Sqlservr.exe.|  
|**PrivateBuild**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**SpecialBuild**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**WindowsVersion**|Version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows that is installed on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**ProcessorCount**|The number of processors in the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**ProcessorActiveMask**|Indicates the processors installed in the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are started and usable by [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows.|  
|**ProcessorType**|Processor type. Similar to **Platform**.|  
|**PhysicalMemory**|Amount in megabytes (MB) of RAM installed on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Product ID**|Product ID (PID) number. This is specified during installation. This number is located on a sticker on the original [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CD case.|  
  
## Return Code Values  
 1 (success)  
  
## Result Sets  
 **xp_msver**, without any parameters, returns a four-column result set that lists all the option values. **xp_msver**, for any parameter, returns the four-column result set with values for that option.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [General Extended Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/general-extended-stored-procedures-transact-sql.md)   
 [@@VERSION &#40;Transact-SQL&#41;](../../t-sql/functions/version-transact-sql-configuration-functions.md)  
  
  
