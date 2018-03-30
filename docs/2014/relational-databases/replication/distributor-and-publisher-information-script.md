---
title: "Distributor and Publisher Information Script | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Publishers [SQL Server replication], information scripts"
  - "Distributors [SQL Server replication], information scripts"
ms.assetid: 8622db47-c223-48fa-87ff-0b4362cd069a
caps.latest.revision: 11
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Distributor and Publisher Information Script
  This script uses system tables and replication stored procedures to answer questions commonly asked about objects at the Distributor and Publisher. The script can be used "as-is" and can also provide the basis for customized scripts. The script might require two modifications to run in your environment:  
  
-   Change the line `use AdventureWorks2012` to use the name of your publication database.  
  
-   Remove the comments (`--`) from the line `exec sp_helparticle @publication='<PublicationName>'` and replace \<PublicationName> with the name of a publication.  
  
```  
--********** Execute at the Distributor in the master database **********--  
  
USE master;  
go  
  
--Is the current server a Distributor?  
--Is the distribution database installed?  
--Are there other Publishers using this Distributor?  
EXEC sp_get_distributor  
  
--Is the current server a Distributor?  
SELECT is_distributor FROM sys.servers WHERE name='repl_distributor' AND data_source=@@servername;  
  
--Which databases on the Distributor are distribution databases?  
SELECT name FROM sys.databases WHERE is_distributor = 1  
  
--What are the Distributor and distribution database properties?  
EXEC sp_helpdistributor;  
EXEC sp_helpdistributiondb;  
EXEC sp_helpdistpublisher;  
  
--********** Execute at the Publisher in the master database **********--  
  
--Which databases are published for replication and what type of replication?  
EXEC sp_helpreplicationdboption;  
  
--Which databases are published using snapshot replication or transactional replication?  
SELECT name as tran_published_db FROM sys.databases WHERE is_published = 1;  
--Which databases are published using merge replication?  
SELECT name as merge_published_db FROM sys.databases WHERE is_merge_published = 1;  
  
--What are the properties for Subscribers that subscribe to publications at this Publisher?  
EXEC sp_helpsubscriberinfo;  
  
--********** Execute at the Publisher in the publication database **********--  
  
USE AdventureWorks2012;  
go  
  
--What are the snapshot and transactional publications in this database?   
EXEC sp_helppublication;  
--What are the articles in snapshot and transactional publications in this database?  
--REMOVE COMMENTS FROM NEXT LINE AND REPLACE <PublicationName> with the name of a publication  
--EXEC sp_helparticle @publication='<PublicationName>';  
  
--What are the merge publications in this database?   
EXEC sp_helpmergepublication;  
--What are the articles in merge publications in this database?  
EXEC sp_helpmergearticle; -- to return information on articles for a single publication, specify @publication='<PublicationName>'  
  
--Which objects in the database are published?  
SELECT name AS published_object, schema_id, is_published AS is_tran_published, is_merge_published, is_schema_published  
FROM sys.tables WHERE is_published = 1 or is_merge_published = 1 or is_schema_published = 1  
UNION  
SELECT name AS published_object, schema_id, 0, 0, is_schema_published  
FROM sys.procedures WHERE is_schema_published = 1  
UNION  
SELECT name AS published_object, schema_id, 0, 0, is_schema_published  
FROM sys.views WHERE is_schema_published = 1;  
  
--Which columns are published in snapshot or transactional publications in this database?  
SELECT object_name(object_id) AS tran_published_table, name AS published_column FROM sys.columns WHERE is_replicated = 1;  
  
--Which columns are published in merge publications in this database?  
SELECT object_name(object_id) AS merge_published_table, name AS published_column FROM sys.columns WHERE is_merge_published = 1;  
```  
  
## See Also  
 [Frequently Asked Questions for Replication Administrators](../../../2014/relational-databases/replication/frequently-asked-questions-for-replication-administrators.md)   
 [sp_get_distributor &#40;Transact-SQL&#41;](../Topic/sp_get_distributor%20\(Transact-SQL\).md)   
 [sp_helparticle &#40;Transact-SQL&#41;](../Topic/sp_helparticle%20\(Transact-SQL\).md)   
 [sp_helpdistributiondb &#40;Transact-SQL&#41;](../Topic/sp_helpdistributiondb%20\(Transact-SQL\).md)   
 [sp_helpdistpublisher &#40;Transact-SQL&#41;](../Topic/sp_helpdistpublisher%20\(Transact-SQL\).md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../Topic/sp_helpdistributor%20\(Transact-SQL\).md)   
 [sp_helpmergearticle &#40;Transact-SQL&#41;](../Topic/sp_helpmergearticle%20\(Transact-SQL\).md)   
 [sp_helpmergepublication &#40;Transact-SQL&#41;](../Topic/sp_helpmergepublication%20\(Transact-SQL\).md)   
 [sp_helppublication &#40;Transact-SQL&#41;](../Topic/sp_helppublication%20\(Transact-SQL\).md)   
 [sp_helpreplicationdboption &#40;Transact-SQL&#41;](../Topic/sp_helpreplicationdboption%20\(Transact-SQL\).md)   
 [sp_helpsubscriberinfo &#40;Transact-SQL&#41;](../Topic/sp_helpsubscriberinfo%20\(Transact-SQL\).md)   
 [sys.columns &#40;Transact-SQL&#41;](../Topic/sys.columns%20\(Transact-SQL\).md)   
 [sys.databases &#40;Transact-SQL&#41;](../Topic/sys.databases%20\(Transact-SQL\).md)   
 [sys.procedures &#40;Transact-SQL&#41;](../Topic/sys.procedures%20\(Transact-SQL\).md)   
 [sys.servers &#40;Transact-SQL&#41;](../Topic/sys.servers%20\(Transact-SQL\).md)   
 [sys.tables &#40;Transact-SQL&#41;](../Topic/sys.tables%20\(Transact-SQL\).md)   
 [sys.views &#40;Transact-SQL&#41;](../Topic/sys.views%20\(Transact-SQL\).md)  
  
  