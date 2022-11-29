---
title: "sys.sp_rda_reauthorize_db (Transact-SQL) | Microsoft Docs"
description: Learn how to use sys.sp_rda_reauthorize_db to restore the authenticated connection between a local Stretch-enabled database and a remote database.
ms.custom: ""
ms.date: 07/25/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords: 
  - "sp_rda_reauthorize_db"
  - "sp_rda_reauthorize_db_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_rda_reauthorize_db stored procedure"
ms.assetid: f6f3e4b2-8c72-4d23-a5de-fe671ca5c5cd
author: markingmyname
ms.author: maghan
---
# sys.sp_rda_reauthorize_db (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Restores the authenticated connection between a local database enabled for Stretch and the remote database.  

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_rda_reauthorize_db @credential = @credential, @with_copy = @with_copy [ , @azure_servername = @azure_servername, @azure_databasename = @azure_databasename ]  
```  
  
## Arguments  
 @credential = *\@credential*  
 Is the database scoped credential associated with the local Stretch-enabled database.  
  
 @with_copy = *\@with_copy*  
 Specifies whether to make a copy of the remote data and connect to the copy (recommended). *\@with_copy* is bit.  
  
 @azure_servername = *\@azure_servername*  
 Specifies the name of the Azure server that contains the remote data. *\@azure_servername* is sysname.  
  
 @azure_databasename = *\@azure_databasename*  
 Specifies the name of the Azure database that contains the remote data. *\@azure_databasename* is sysname.  
  
## Return Code Values  
 0 (success) or >0 (failure)  
  
## Permissions  
 Requires db_owner permissions.  
  
## Remarks  
 When you run [sys.sp_rda_reauthorize_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database, this operation automatically resets the query mode to LOCAL_AND_REMOTE, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.  
  
## Example  
 The following example restores the authenticated connection between a local database enabled for Stretch and the remote database. It makes a copy of the remote data (recommended) and connects to the new copy.  
  
```sql  
DECLARE @credentialName nvarchar(128);   
SET @credentialName = N'<existing_database_scoped_credential_name>';   
EXEC sp_rda_reauthorize_db @credential = @credentialName, @with_copy = 1;  
  
```  
  
## See Also  
 [sys.sp_rda_deauthorize_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-rda-deauthorize-db-transact-sql.md)   
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
  
  
