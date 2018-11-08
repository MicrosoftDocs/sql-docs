---
title: "Finishing Test Case Preparation (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Tester Component,Test Case Settings"
ms.assetid: 8b2a49b0-4296-4f3f-9e56-323aa6a6fa8e
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Finishing Test Case Preparation (SybaseToSQL)
The wizard's final page displays the Test Case description and information about objects involved in the test. In addition, on this page you can set the test execution options.  
  
The **Test Case Information** section shows the Test Case name and description.  
  
The **Test Objects** section contains the named list of tested objects grouped by object type.  
  
The **Affected Objects to be Analyzed** section displays the named list of objects which data changes should be compared after tested objects execution.  
  
## Test Case Settings  
In the **Test Case Settings** section you can set the following execution test options:  
  
### Stop test execution after first failure  
Specifies to break the test if an error occurs during test execution.  
  
-   If you choose **Yes**, test execution breaks if an error happens.  
  
-   If you choose **No**, test execution continues after an error.  
  
### Perform data rollback  
Enable automatic data rollback after test execution.  
  
-   If you choose **Yes**, data changes will be lost after test execution.  
  
-   If you choose **No**, all test execution data changes will be saved.  
  
### Auxiliary tables saving mode  
Defines the saving mode for auxiliary tables created during test execution. See the description of auxiliary tables in the [Running Test Cases &#40;SybaseToSQL&#41;](../../ssma/sybase/running-test-cases-sybasetosql.md) topic.  
  
-   If you select **Always Save**, auxiliary table data will always be stored for later use.  
  
-   If you select **Save if Table Comparison Failed**, auxiliary table data will be stored only if an error happens.  
  
-   If you select **Always Delete**, auxiliary tables always be deleted after test execution.  
  
-   If you select **Ask User if Table Comparison Failed**, the user can select the necessary action if an error happens.  
  
Click the **Finish** button to save the prepared Test Case into [Using Test Repositories &#40;SybaseToSQL&#41;](../../ssma/sybase/using-test-repositories-sybasetosql.md).  
  
## See Also  
[Using Test Repositories &#40;SybaseToSQL&#41;](../../ssma/sybase/using-test-repositories-sybasetosql.md)  
[Running Test Cases &#40;SybaseToSQL&#41;](../../ssma/sybase/running-test-cases-sybasetosql.md)  
[Testing Migrated Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/testing-migrated-database-objects-sybasetosql.md)  
  
