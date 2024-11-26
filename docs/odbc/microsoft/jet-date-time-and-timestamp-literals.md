---
title: "Jet: Date, Time, and Timestamp Literals"
description: "Jet: Date, Time, and Timestamp Literals"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "literals [ODBC], SQL grammar"
  - "SQL grammar [ODBC], literals"
  - "date literals [ODBC]"
  - "timestamp literals [ODBC]"
  - "time literals [ODBC]"
---
# Jet: Date, Time, and Timestamp Literals
For maximum interoperability, applications should pass date literals in the ODBC canonical format using escape-clause syntax:  
  
-   For date literals, {d '*value*'}, where *value* is in the form "yyyy-mm-dd"  
  
-   For time literals, {t '*value*'}, where *value* is in the form "hh:mm:ss"  
  
 For timestamp literals {ts '*value*'}, where *value* is in the form "yyyy-mm-dd hh:mm:ss[.f...]".
