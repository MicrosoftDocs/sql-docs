---
title: "DROP INDEX Statement | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "DROP INDEX [ODBC]"
  - "SQL grammar [ODBC], DROP INDEX"
ms.assetid: cd0ff767-9254-413b-bd1a-bed26c6774f5
author: MightyPen
ms.author: genemi
manager: craigg
---
# DROP INDEX Statement
When the Microsoft Access, dBASE, or Paradox driver is used, the syntax of the DROP INDEX statement is "DROP INDEX a on b" where "a" is the name of the index and "b" is the name of the table (not DROP INDEX *index-name*).  
  
 When the Paradox driver is used, the DROP INDEX statement deletes Paradox secondary index files.  
  
 The DROP INDEX statement is not supported for the Microsoft Excel or Text drivers.
