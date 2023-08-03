---
title: "Full-Text Search and Semantic Search Functions (Transact-SQL)"
description: "Full-Text Search and Semantic Search Functions (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "semantic search [SQL Server], system functions"
dev_langs:
  - "TSQL"
---
# Full-Text Search and Semantic Search Functions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
