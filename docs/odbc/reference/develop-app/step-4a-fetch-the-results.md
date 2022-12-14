---
description: "Step 4a: Fetch the Results"
title: "Step 4a: Fetch the Results | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application process [ODBC], fetching results"
  - "fetches [ODBC], fetching results"
ms.assetid: 77d30142-c774-473c-96fb-b364bb92ac60
author: David-Engel
ms.author: v-davidengel
---
# Step 4a: Fetch the Results
The next step is to fetch the results, as shown in the following illustration.  
  
 ![Shows fetching results in an ODBC application](../../../odbc/reference/develop-app/media/pr14.gif "pr14")  
  
 If the statement executed in "Step 3: Build and Execute an SQL Statement" was a **SELECT** statement or a catalog function, the application first calls **SQLNumResultCols** to determine the number of columns in the result set. This step is not necessary if the application already knows the number of result set columns, such as when the SQL statement is hard-coded in a vertical or custom application.  
  
 Next, the application retrieves the name, data type, precision, and scale of each result set column with **SQLDescribeCol**. Again, this is not necessary for applications such as vertical and custom applications that already know this information. The application passes this information to **SQLBindCol**, which binds an application variable to a column in the result set.  
  
 The application now calls **SQLFetch** to retrieve the first row of data and place the data from that row in the variables bound with **SQLBindCol**. If there is any long data in the row, it then calls **SQLGetData** to retrieve that data. The application continues to call **SQLFetch** and **SQLGetData** to retrieve additional data. After it has finished fetching data, it calls **SQLCloseCursor** to close the cursor.  
  
 For a complete description of retrieving results, see [Retrieving Results (Basic)](../../../odbc/reference/develop-app/retrieving-results-basic.md) and [Retrieving Results (Advanced)](../../../odbc/reference/develop-app/retrieving-results-advanced.md).  
  
 The application now returns to "Step 3: Build and Execute an SQL Statement" to execute another statement in the same transaction; or proceeds to "Step 5: Commit the Transaction" to commit or roll back the transaction.
