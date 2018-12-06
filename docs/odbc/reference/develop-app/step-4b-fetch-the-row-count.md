---
title: "Step 4b: Fetch the Row Count | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "fetches [ODBC], fetching row count"
  - "row count [ODBC]"
  - "application process [ODBC], fetching row count"
ms.assetid: 3af481b1-d694-446e-948d-e3a5edcad050
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 4b: Fetch the Row Count
The next step is to fetch the row count, as shown in the following illustration.  
  
 ![Shows fetching the row count](../../../odbc/reference/develop-app/media/pr15.gif "pr15")  
  
 If the statement executed in Step 3 was an **UPDATE**, **DELETE**, or **INSERT** statement, the application retrieves the count of affected rows with **SQLRowCount**. For more information, see [Determining the Number of Affected Rows](../../../odbc/reference/develop-app/determining-the-number-of-affected-rows.md).  
  
 The application now returns to step 3 to execute another statement in the same transaction or proceeds to step 5 to commit or roll back the transaction.
