---
title: "sp_changearticlecolumndatatype (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changearticlecolumndatatype"
  - "sp_changearticlecolumndatatype_TSQL"
helpviewer_keywords: 
  - "sp_changearticlecolumndatatype"
ms.assetid: 0db80e08-fb77-4d0c-aa41-455b13ffa9b4
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changearticlecolumndatatype (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the article column data type mapping for an Oracle publication. This stored procedure is executed at the Distributor on any database.  
  
> [!NOTE]  
>  The data type mappings between supported Publisher types are provided by default. Use **sp_changearticlecolumndatatype** only when overriding these default settings.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changearticlecolumndatatype [ @publication= ] 'publication'  
    [ @article = ] 'article'   
    [ @column = ] 'column'  
    [ , [ @type = ] 'type' ]  
    [ , [ @length = ] length ]  
    [ , [ @precision = ] precision ]  
    [ , [ @scale = ] scale ]  
    [ , [ @publisher = ] 'publisher'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the Oracle publication. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article. *article* is **sysname**, with no default.  
  
`[ @column = ] 'column'`
 Is the name of the column for which to change the data type mapping. *column* is **sysname**, with no default.  
  
`[ @type = ] 'type'`
 Is the name of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *type* is **sysname**, with a default of NULL.  
  
`[ @length = ] length`
 Is the length of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *length* is **bigint**, with a default of NULL.  
  
`[ @precision = ] precision`
 Is the precision of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type in the destination column. *precision* is **bigint**, with a default of NULL.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **Sp_changearticlecolumndatatype** is used to override the default data type mappings between supported Publisher types (Oracle and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]). To view these default data type mappings, execute [sp_getdefaultdatatypemapping](../../relational-databases/system-stored-procedures/sp-getdefaultdatatypemapping-transact-sql.md).  
  
 **sp_changearticlecolumndatatype** is only supported for Oracle Publishers. Executing this stored procedure against a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publication results in an error.  
  
 **sp_changearticlecolumndatatype** must be executed for each article column mapping that must be changed.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changearticlecolumndatatype**.  
  
## See Also  
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Data Type Mapping for Oracle Publishers](../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
