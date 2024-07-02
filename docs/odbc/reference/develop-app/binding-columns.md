---
title: "Binding Columns"
description: "Binding Columns"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "result sets [ODBC], binding columns"
  - "binding columns [ODBC]"
---
# Binding Columns
Data fetched from the data source is returned to the application in variables that the application has allocated for this purpose. Before this can be done, the application must associate, or *bind*, these variables to the columns of the result set; conceptually, this process is the same as binding application variables to statement parameters. When the application binds a variable to a result set column, it describes that variable - address, data type, and so on - to the driver. The driver stores this information in the structure it maintains for that statement and uses the information to return the value from the column when the row is fetched.  
  
 This section contains the following topics.  
  
-   [Binding Result Set Columns](../../../odbc/reference/develop-app/binding-result-set-columns.md)  
  
-   [Using SQLBindCol](../../../odbc/reference/develop-app/using-sqlbindcol.md)
