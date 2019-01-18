---
title: "ODBC Overview | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC]"
  - "ODBC [ODBC], about ODBC"
ms.assetid: 233315bd-2b7f-4b20-9978-e920e1ea9a07
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Overview
Open Database Connectivity (ODBC) is a widely accepted application programming interface (API) for database access. It is based on the Call-Level Interface (CLI) specifications from Open Group and ISO/IEC for database APIs and uses Structured Query Language (SQL) as its database access language.  
  
 ODBC is designed for maximum *interoperability* - that is, the ability of a single application to access different database management systems (DBMSs) with the same source code. Database applications call functions in the ODBC interface, which are implemented in database-specific modules called *drivers*. The use of drivers isolates applications from database-specific calls in the same way that printer drivers isolate word processing programs from printer-specific commands. Because drivers are loaded at run time, a user only has to add a new driver to access a new DBMS; it is not necessary to recompile or relink the application.  
  
 This section contains the following topics.  
  
-   [Why Was ODBC Created?](../../odbc/reference/why-was-odbc-created.md)  
  
-   [What Is ODBC?](../../odbc/reference/what-is-odbc.md)  
  
-   [ODBC and the Standard CLI](../../odbc/reference/odbc-and-the-standard-cli.md)
