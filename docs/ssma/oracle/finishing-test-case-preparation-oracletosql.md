---
title: "Finishing Test Case Preparation (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 32f38713-7ae4-48d3-980d-74cadc8545a0
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Finishing Test Case Preparation (OracleToSQL)
The wizard's final page displays the Test Case description and information about objects involved in the test. In addition, on this page you can set the test execution options.  
  
The **Test Case Information** section shows the Test Case name and description.  
  
The **Objects Selected To Be Tested** section contains the named list of tested objects grouped by object type.  
  
The **Objects Affected By Test That Will Be Analyzed** section displays the named list of objects which data changes should be compared after tested objects execution.  
  
## Test Case Settings  
In the **Test Case Settings** section you can set the following execution test options:  
  
### Stop test execution after first failure  
Specifies to break the test if an error occurs during test execution.  
  
-   If you choose **Yes**, test execution breaks if an error happens.  
  
-   If you choose **No**, test execution continues after an error.  
  
### Perform data rollback  
Enables automatic data rollback after test execution.  
  
-   If you choose **Yes**, data changes will be lost after test execution.  
  
-   If you choose **No**, all test execution data changes will be saved.  
  
### Auxiliary tables saving mode  
Defines the saving mode for auxiliary tables created during test execution. See the description of auxiliary tables in the [Running Test Cases &#40;OracleToSQL&#41;](../../ssma/oracle/running-test-cases-oracletosql.md) topic.  
  
-   If you select **Always Save**, auxiliary table data will always be stored for later use.  
  
-   If you select **Save if Table Comparison Failed**, auxiliary table data will be stored only if an error happens.  
  
-   If you select **Always Delete**, auxiliary tables always be deleted after test execution.  
  
-   If you select **Ask User if Table Comparison Failed**, the user can select the necessary action if an error happens.  
  
Click the **Finish** button to save the prepared Test Case into [Using Test Repositories (OracleToSQL)](https://msdn.microsoft.com/f941cce4-d3e3-4aeb-a88a-4f101a97a9f4).  
  
## See Also  
[Using Test Repositories &#40;OracleToSQL&#41;](../../ssma/oracle/using-test-repositories-oracletosql.md)  
[Running Test Cases &#40;OracleToSQL&#41;](../../ssma/oracle/running-test-cases-oracletosql.md)  
[Testing Migrated Database Objects &#40;OracleToSQL&#41;](../../ssma/oracle/testing-migrated-database-objects-oracletosql.md)  
  
