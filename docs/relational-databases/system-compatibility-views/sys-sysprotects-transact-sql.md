---
description: "sys.sysprotects (Transact-SQL)"
title: "sys.sysprotects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysprotects"
  - "sys.sysprotects_TSQL"
  - "sys.sysprotects"
  - "sysprotects_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sysprotects compatibility view"
  - "sysprotects system table"
ms.assetid: 49c9658d-fb51-4c77-94a0-fba699b0102d
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysprotects (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about permissions that have been applied to security accounts in the database by using the GRANT and DENY statements.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the object to which these permissions apply.|  
|**uid**|**smallint**|ID of user or group to which these permissions apply. Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
|**action**|**tinyint**|Can have one of the following permissions:<br /><br /> 26 = REFERENCES<br /><br /> 178 = CREATE FUNCTION<br /><br /> 193 = SELECT<br /><br /> 195 = INSERT<br /><br /> 196 = DELETE<br /><br /> 197 = UPDATE<br /><br /> 198 = CREATE TABLE<br /><br /> 203 = CREATE DATABASE<br /><br /> 207 = CREATE VIEW<br /><br /> 222 = CREATE PROCEDURE<br /><br /> 224 = EXECUTE<br /><br /> 228 = BACKUP DATABASE<br /><br /> 233 = CREATE DEFAULT<br /><br /> 235 = BACKUP LOG<br /><br /> 236 = CREATE RULE|  
|**protecttype**|**tinyint**|Can have the following values:<br /><br /> 204 = GRANT_W_GRANT<br /><br /> 205 = GRANT<br /><br /> 206 = DENY|  
|**columns**|**varbinary(8000)**|Bitmap of columns to which these SELECT or UPDATE permissions apply.<br /><br /> Bit 0 = All columns.<br /><br /> Bit 1 = Permissions apply to that column.<br /><br /> NULL = No information.|  
|**grantor**|**smallint**|User ID of the user that issued the GRANT or DENY permissions. Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
