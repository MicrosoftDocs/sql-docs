---
title: "Generic Applications"
description: "Generic Applications"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "interoperability [ODBC], generic applications"
  - "interoperability [ODBC], levels"
  - "generic applications [ODBC]"
---
# Generic Applications
Generic applications sometimes perform a hard-coded task, such as a spreadsheet retrieving data from a database. They might also perform a variety of user-defined tasks, such as a generic query application allowing the user to enter and execute a SQL statement. What generic applications have in common is that they must work with a variety of different DBMSs and that the developer does not know beforehand what these DBMSs will be.  
  
 Therefore, generic applications need to be highly interoperable. The developer must make many choices, trading off interoperability for features, and must write code that expects drivers to support a wide range of functionality. While generic applications might be tuned to work with popular DBMSs, they rarely contain driver-specific or DBMS-specific code.
