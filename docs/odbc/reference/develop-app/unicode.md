---
description: "Unicode"
title: "Unicode | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC]"
  - "Unicode [ODBC], about Unicode"
ms.assetid: 113e1c9a-8333-4805-86f2-e4b57ab816a5
author: David-Engel
ms.author: v-davidengel
---
# Unicode
Unicode defines encoding for characters in many languages.  
  
 For more information about the Unicode standard, see [The Unicode Consortium](https://www.unicode.org).  
  
 Unicode defines a universal character set. A Windows ANSI code page defines a character set, typically containing characters for one language. It may be more difficult to write an application that is required to use different code pages.  
  
 Unicode does not require a code page. Every code point is mapped to a single character in some language.  
  
 Currently, the only Unicode encoding that ODBC supports is UCS-2, which uses a 16-bit integer (fixed length) to represent a character. Unicode allows applications to work in different languages.  
  
 The ODBC 3.5 (or higher) Driver Manager is Unicode-enabled. This affects two major areas: function calls and string data types. The Driver Manager maps function string arguments and string data as required by the application and driver, both of which can be either Unicode-enabled or ANSI-enabled. These two areas are discussed in detail in the sections, [Unicode Function Arguments](../../../odbc/reference/develop-app/unicode-function-arguments.md) and [Unicode Data](../../../odbc/reference/develop-app/unicode-data.md).  
  
 The ODBC 3.5 (or higher) Driver Manager supports the use of a Unicode driver with both a Unicode application and an ANSI application. It also supports the use of an ANSI driver with an ANSI application. The Driver Manager provides limited Unicode-to-ANSI mapping for a Unicode application working with an ANSI driver.  
  
 This section contains the following topics.  
  
-   [Unicode Function Arguments](../../../odbc/reference/develop-app/unicode-function-arguments.md)  
  
-   [Unicode Data](../../../odbc/reference/develop-app/unicode-data.md)
