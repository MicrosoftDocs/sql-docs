---
title: "Full-Text Search and Semantic Search Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "semantic search [SQL Server], system functions"
ms.assetid: a61a3694-7604-4583-962e-fc30f771c6fa
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Full-Text Search and Semantic Search Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  This section describes the system functions related to full-text search and semantic search.  
  
## Full-Text Search Functions  
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)  
 Returns a table of zero, one, or more rows for those columns containing precise or fuzzy (less precise) matches for single words and phrases, the proximity of words within a certain distance of one another, or weighted matches.  
  
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)  
 Returns a table of zero, one, or more rows for those columns containing values that match the meaning, and not just the exact wording, of the text in the specified *freetext_string*.  
  
## Semantic Search Functions  
 [semantickeyphrasetable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/semantickeyphrasetable-transact-sql.md)  
 Returns a table with zero, one, or more rows for those key phrases associated with columns in the specified table.  
  
 [semanticsimilaritydetailstable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/semanticsimilaritydetailstable-transact-sql.md)  
 Returns a table of zero, one, or more rows of key phrases common across two documents (a source document and a matched document) whose content is semantically similar.  
  
 [semanticsimilaritytable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/semanticsimilaritytable-transact-sql.md)  
 Returns a table of zero, one, or more rows for those columns whose content is semantically similar to a specified document.  
  
  
