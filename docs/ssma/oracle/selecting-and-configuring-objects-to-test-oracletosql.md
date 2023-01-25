---
description: "Selecting and Configuring Objects to Test (OracleToSQL)"
title: "Selecting and Configuring Objects to Test (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Selection of Objects to Test,Parameter Comparison Settings"
ms.assetid: 29fb6542-5c1f-4b14-a822-87700beb7623
author: cpichuka 
ms.author: cpichuka 
---
# Selecting and Configuring Objects to Test (OracleToSQL)
At this step you select objects to test, and configure settings for comparing procedures' and functions' output parameters, as well as the return values of functions.  
  
## Selection of Objects to Test  
In the Oracle object tree located on the left side of the window, check the objects you want to invoke during the testing process. See the full list of testable objects in the [Testing Migrated Database Objects &#40;OracleToSQL&#41;](../../ssma/oracle/testing-migrated-database-objects-oracletosql.md) topic.  
  
If SSMA Tester does not support any of the objects selected for testing, you will see the link labeled **Some selected objects contain errors** under the objects tree. Click this link to view the reasons why these objects cannot be tested and to clear the selection of wrong objects.  
  
On the right side you can view several pages The **SQL** page shows the current object's definition. The **Parameters** page lists the parameters if the object is a stored procedure or a function. The **Properties** page shows additional characteristics of the object. See the description of **Parameter Comparisons** and **Call Values** pages below.  
  
## Parameter Comparison Settings  
Establish the comparison rules for output parameters and return values in the **Parameter Comparisons** page. You can make the following settings.  
  
### Use During Test Comparisons  
Enable using of the selected parameter in test results comparison.  
  
-   If you choose **True**, SSMA will compare the output value of this parameter after executing the procedure on Oracle with the corresponding value on SQL Server.
  
-   If you choose**False**, the parameter will be excluded from results verification.  
  
### Use Custom Scale  
For parameters of numeric data type, you can set a custom scale for the comparison.  
  
-   If you choose **True**, numeric values will be rounded according to the **Comparing Scale** value before they are compared.  
  
-   If you choose**False**, the numeric comparison will be exact.  
  
### Comparing Scale  
Available only if the **Use Custom Scale** option is set to **True**. This is the precision for numeric comparison.  
  
### Date Time Comparing  
Defines how date/time values are compared.  
  
-   If you select **Compare Whole Date**, full comparison of values from both platforms will be performed.  
  
-   If you select **Compare Only Date**, the time part will be ignored.  
  
-   If you select **Compare Only Time**, the date part will be ignored.  
  
-   If you select **Ignore Milliseconds**, the results will be compared up to seconds.  
  
-   If you select **Ignore Date and Milliseconds**, the result will be compared only by time part and ignoring fractional parts of a second.  
  
### Ignore Strings Case  
Controls the comparison's case sensitivity.  
  
-   If you choose **True**, the comparison will be case insensitive.  
  
-   If you choose **False**, the comparison will be case sensitive.  
  
### Ignore Trailing Spaces  
Controls how trailing spaces are treated during the comparison.  
  
-   If you choose **True**, the compared strings will be right-trimmed before comparing.  
  
-   If you choose **False**, the compared strings will preserve trailing whitespace.  
  
## Specify input values for procedures and functions (Call Values)  
You can specify input parameter values on the **Call Values** page. The **Add Call** button adds a new call with empty parameter values. The **Remove Calls** button removes the current call.  
  
## Next Step  
[Selecting and Configuring Affected Objects &#40;OracleToSQL&#41;](../../ssma/oracle/selecting-and-configuring-affected-objects-oracletosql.md)  
  
## See Also  
[Testing Migrated Database Objects &#40;OracleToSQL&#41;](../../ssma/oracle/testing-migrated-database-objects-oracletosql.md)  
  
