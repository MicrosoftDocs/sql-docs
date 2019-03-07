---
title: "Viewing Test Case Reports (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 8da14323-9dd6-4019-bf79-3e8b972a9bc0
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Viewing Test Case Reports (OracleToSQL)
The Test Case Report shows the test verification results and general test information. In case of a test failure, information about any mismatched data in verified objects is also displayed.  
  
## Report Structure  
The top of the report shows these statistics:  
  
-   The total number of tested objects and the number of objects for which the test was successful.  
  
-   The total number of verified tables and foreign keys, and the number of tables and foreign keys successfully matched.  
  
-   The Start time, End time of the test case and the total time taken for execution.  
  
The rest of the report shows information in four categories:  
  
**Prerequisites Errors**  
Shows any errors that occurred at the **Prerequisites step.** Normally, it is skipped.  
  
**Initialization**  
Shows the status of execution as **Success** or **Failure**.  
  
**Test objects result**  
A comparison of results (success or failure) and the mismatches that SSMA Tester detected in case of failure.  
  
**Finalization**  
Shows the status of execution as **Success** or **Failure**.  
  
## See Also  
[Running Test Cases &#40;OracleToSQL&#41;](../../ssma/oracle/running-test-cases-oracletosql.md)  
[Testing Migrated Database Objects &#40;OracleToSQL&#41;](../../ssma/oracle/testing-migrated-database-objects-oracletosql.md)  
  
