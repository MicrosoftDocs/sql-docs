---
title: "sys.dm_repl_articles (Transact-SQL)"
description: sys.dm_repl_articles (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_repl_articles_TSQL"
  - "dm_repl_articles"
  - "dm_repl_articles_TSQL"
  - "sys.dm_repl_articles"
helpviewer_keywords:
  - "sys.dm_repl_articles dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: 794d514e-bacd-432e-a8ec-3a063a97a37b
---
# sys.dm_repl_articles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about database objects published as articles in a replication topology.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artcache_db_address**|**varbinary(8)**|In-memory address of the cached database structure for the publication database.|  
|**artcache_table_address**|**varbinary(8)**|In-memory address of the cached table structure for a published table article.|  
|**artcache_schema_address**|**varbinary(8)**|In-memory address of the cached article schema structure for a published table article.|  
|**artcache_article_address**|**varbinary(8)**|In-memory address of the cached article structure for a published table article.|  
|**artid**|**bigint**|Uniquely identifies each entry within this table.|  
|**artfilter**|**bigint**|ID of the stored procedure used to horizontally filter the article.|  
|**artobjid**|**bigint**|ID of the published object.|  
|**artpubid**|**bigint**|ID of the publication to which the article belongs.|  
|**artstatus**|**tinyint**|Bitmask of the article options and status, which can be the bitwise logical OR result of one or more of these values:<br /><br /> **1** = Article is active.<br /><br /> **8** = Include the column name in INSERT statements.<br /><br /> **16** = Use parameterized statements.<br /><br /> **24** = Both include the column name in INSERT statements and use parameterized statements.<br /><br /> For example, an active article using parameterized statements would have a value of 17 in this column. A value of 0 means that the article is inactive and no additional properties are defined.|  
|**arttype**|**tinyint**|Type of article:<br /><br /> **1** = Log-based article.<br /><br /> **3** = Log-based article with manual filter.<br /><br /> **5** = Log-based article with manual view.<br /><br /> **7** = Log-based article with manual filter and manual view.<br /><br /> **8** = Stored procedure execution.<br /><br /> **24** = Serializable stored procedure execution.<br /><br /> **32** = Stored procedure (schema only).<br /><br /> **64** = View (schema only).<br /><br /> **128** = Function (schema only).|  
|**wszArtdesttable**|**nvarchar(514)**|Name of published object at the destination.|  
|**wszArtdesttableowner**|**nvarchar(514)**|Owner of published object at the destination.|  
|**wszArtinscmd**|**nvarchar(510)**|Command or stored procedure used for inserts.|  
|**cmdTypeIns**|**int**|Call syntax for the insert stored procedure, and can be one of these values.<br /><br /> **1** = CALL<br /><br /> **2** = SQL<br /><br /> **3** = NONE<br /><br /> **7** = UNKNOWN|  
|**wszArtdelcmd**|**nvarchar(510)**|Command or stored procedure used for deletes.|  
|**cmdTypeDel**|**int**|Call syntax for the delete stored procedure, and can be one of these values.<br /><br /> **0** = XCALL<br /><br /> **1** = CALL<br /><br /> **2** = SQL<br /><br /> **3** = NONE<br /><br /> **7** = UNKNOWN|  
|**wszArtupdcmd**|**nvarchar(510)**|Command or stored procedure used for updates.|  
|**cmdTypeUpd**|**int**|Call syntax for the update stored procedure, and can be one of these values.<br /><br /> **0** = XCALL<br /><br /> **1** = CALL<br /><br /> **2** = SQL<br /><br /> **3** = NONE<br /><br /> **4** = MCALL<br /><br /> **5** = VCALL<br /><br /> **6** = SCALL<br /><br /> **7** = UNKNOWN|  
|**wszArtpartialupdcmd**|**nvarchar(510)**|Command or stored procedure used for partial updates.|  
|**cmdTypePartialUpd**|**int**|Call syntax for the partial update stored procedure, and can be one of these values.<br /><br /> **2** = SQL|  
|**numcol**|**int**|Number of columns in the partition for a vertically filtered article.|  
|**artcmdtype**|**tinyint**|Type of command currently being replicated, and can be one of these values.<br /><br /> **1** = INSERT<br /><br /> **2** = DELETE<br /><br /> **3** = UPDATE<br /><br /> **4** = UPDATETEXT<br /><br /> **5** = none<br /><br /> **6** = internal use only<br /><br /> **7** = internal use only<br /><br /> **8** = partial UPDATE|  
|**artgeninscmd**|**nvarchar(510)**|INSERT command template based on the columns included in the article.|  
|**artgendelcmd**|**nvarchar(510)**|DELETE command template, which can include the primary key or the columns included in the article, depending on the call syntax is used.|  
|**artgenupdcmd**|**nvarchar(510)**|UPDATE command template, which can include the primary key, updated columns, or a complete column list depending on the call syntax is used.|  
|**artpartialupdcmd**|**nvarchar(510)**|Partial UPDATE command template, which includes the primary key and updated columns.|  
|**artupdtxtcmd**|**nvarchar(510)**|UPDATETEXT command template, which includes the primary key and updated columns.|  
|**artgenins2cmd**|**nvarchar(510)**|INSERT command template used when reconciling an article during concurrent snapshot processing.|  
|**artgendel2cmd**|**nvarchar(510)**|DELETE command template used when reconciling an article during concurrent snapshot processing.|  
|**fInReconcile**|**tinyint**|Indicates whether an article is currently being reconciled during concurrent snapshot processing.|  
|**fPubAllowUpdate**|**tinyint**|Indicates whether the publication allows updating subscription.|  
|**intPublicationOptions**|**bigint**|Bitmap that specifies additional publishing options, where the bitwise option values are:<br /><br /> **0x1** - Enabled for peer-to-peer replication.<br /><br /> **0x2** - Publish only local changes.<br /><br /> **0x4** - Enabled for non-SQL Server Subscribers.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the publication database to call **dm_repl_articles**.  
  
## Remarks  
 Information is only returned for replicated database objects that are currently loaded in the replication article cache.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Replication Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/replication-related-dynamic-management-views-transact-sql.md)  
  
  

