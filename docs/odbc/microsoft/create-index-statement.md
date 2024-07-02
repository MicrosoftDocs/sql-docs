---
title: "CREATE INDEX Statement"
description: "CREATE INDEX Statement"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "CREATE INDEX [ODBC]"
  - "SQL grammar [ODBC], create index"
---
# CREATE INDEX Statement
The syntax of the CREATE INDEX statement is:  
  
 CREATE [UNIQUE] INDEX *index-name* ON *table-name* (*column-identifier* [ASC][DESC][, *column-identifier* [ASC][DESC]...]) WITH \<*index option list*>  
  
 where \<*index option list*> can be: PRIMARY &#124; DISALLOW NULL &#124; IGNORE NULL  
  
 Only the Microsoft Access driver uses the DISALLOW NULL and IGNORE NULL index options. The dBASE and Paradox drivers accept the syntax, but ignore the presence of either option.  
  
 When the Paradox driver is used, the CREATE INDEX statement creates Paradox primary key files and secondary files.  
  
 This statement is not supported by the Microsoft Excel or Text drivers.
