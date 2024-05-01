---
title: "Identifiers limitations"
description: "Identifiers limitations"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC desktop database drivers [ODBC]"
  - "desktop database drivers [ODBC]"
---
# Identifiers limitations

If an identifier contains a space or a special symbol, the identifier must be enclosed in back quotes. A valid name is a string of no more than 64 characters, of which the first character must not be a space. Valid names can't include control characters or the following special characters: `` ` ``, `|`, `#`, `*`, `?`, `[`, `]`, `.`, `!`, or `$`.

Don't use the reserved words listed in the SQL grammar in Appendix C of the *ODBC Programmer's Reference* (or the shorthand form of these reserved words) as identifiers (that is, table or column names), unless you surround the word in back quotes (`` ` ``).
