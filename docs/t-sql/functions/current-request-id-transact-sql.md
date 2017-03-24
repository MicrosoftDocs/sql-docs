---
title: "CURRENT_REQUEST_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CURRENT_REQUEST_ID_TSQL"
  - "CURRENT_REQUEST_ID"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CURRENT_REQUEST_ID"
ms.assetid: 949f6e5f-bf5f-49d6-a763-c443d1d51fe2
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CURRENT_REQUEST_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the ID of the current request within the current session.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CURRENT_REQUEST_ID()  
```  
  
## Return Types  
 **smallint**  
  
## Remarks  
 To find exact information about the current session and current request, use @@SPID and CURRENT_REQUEST_ID(), respectively.  
  
## See Also  
 [@@SPID &#40;Transact-SQL&#41;](../../t-sql/functions/spid-transact-sql.md)  
  
  