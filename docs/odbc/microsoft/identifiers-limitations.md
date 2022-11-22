---
description: "Identifiers Limitations"
title: "Identifiers Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC desktop database drivers [ODBC]"
  - "desktop database drivers [ODBC]"
ms.assetid: b3466382-71cb-4f82-8318-092a8fcef3df
author: David-Engel
ms.author: v-davidengel
---
# Identifiers Limitations
If an identifier contains a space or a special symbol, the identifier must be enclosed in back quotes. A valid name is a string of no more than 64 characters, of which the first character must not be a space. Valid names cannot include control characters or the following special characters: ` &#124; # * ? [ ] . ! $ .  
  
 Do not use the reserved words listed in the SQL grammar in Appendix C of the *ODBC Programmer's Reference* (or the shorthand form of these reserved words) as identifiers (that is, table or column names), unless you surround the word in back quotes (`).
