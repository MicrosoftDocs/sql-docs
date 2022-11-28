---
description: "Creating Test Cases (OracleToSQL)"
title: "Creating Test Cases (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Test Case Wizard"
ms.assetid: 22f38901-ec35-4707-a911-784e6ad8dafb
author: cpichuka 
ms.author: cpichuka 
---
# Creating Test Cases (OracleToSQL)
Use the Test Case Wizard to create a test. This wizard lets you create test cases by choosing tested and verified objects and by specifying the testing parameters.  
  
## Starting the Test Case Wizard  
To start the Test Case Wizard click **New Test Case...** from the **Tester** menu.  
  
When started, the wizard looks for schema SSMATESTER_ORACLE on the source Oracle server. It is the Tester extension schema used for storing auxiliary objects. If the Test Case Wizard cannot find SSMATESTER_ORACLE, it displays a dialog window that proposes to create the schema. (That situation usually happens during the first run of SSMA Tester.)  
  
If you get the dialog window, click **Yes** to create SSMATESTER_ORACLE schema on the source server. Note that you must have Oracle privileges to create a new user and create objects in the schema of this user.  
  
## Overview of Creating Test Cases Using the Wizard  
The process of creating a test case consists of five steps:  
  
1.  [Initializing Test Cases &#40;OracleToSQL&#41;](../../ssma/oracle/initializing-test-cases-oracletosql.md)  
  
2.  [Selecting and Configuring Objects to Test &#40;OracleToSQL&#41;](../../ssma/oracle/selecting-and-configuring-objects-to-test-oracletosql.md)  
  
3.  [Selecting and Configuring Affected Objects &#40;OracleToSQL&#41;](../../ssma/oracle/selecting-and-configuring-affected-objects-oracletosql.md)  
  
4.  [Customizing Calls Order &#40;OracleToSQL&#41;](../../ssma/oracle/customizing-calls-order-oracletosql.md)  
  
5.  [Finishing Test Case Preparation &#40;OracleToSQL&#41;](../../ssma/oracle/finishing-test-case-preparation-oracletosql.md)  
  
## See Also  
[Testing Migrated Database Objects &#40;OracleToSQL&#41;](../../ssma/oracle/testing-migrated-database-objects-oracletosql.md)  
  
