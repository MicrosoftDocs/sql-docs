---
title: "Returning SQL_NO_DATA"
description: "Returning SQL_NO_DATA"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQL_NO_DATA [ODBC]"
  - "backward compatibility [ODBC], SQL_NO_DATA"
  - "compatibility [ODBC], SQL_NO_DATA"
---
# Returning SQL_NO_DATA
When an ODBC *2.x* application workingwith an ODBC *3.x* driver calls **SQLExecDirect**, **SQLExecute**, or **SQLParamData**, and a searched update or delete statement was executed but did not affect any rows at the data source, the ODBC *3.x* driver should return SQL_SUCCESS. When an ODBC *3.x* application working with an ODBC *3.x* driver calls **SQLExecDirect**, **SQLExecute**, or **SQLParamData** with the same result, the ODBC *3.x* driver should return SQL_NO_DATA.  
  
 If a searched update or delete statement in a batch of statements does not affect any rows at the data source, **SQLMoreResults** returns SQL_SUCCESS. It cannot return SQL_NO_DATA, because that would mean that there are no more results, not that there is a result from a searched update/delete that affected no rows.
