---
description: "Running Test Cases (SybaseToSQL)"
title: "Running Test Cases (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/29/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Tester Component,Execution Steps"
ms.assetid: 195ffdef-cfde-4bf4-a3ae-e7402bb07972
author: cpichuka 
ms.author: cpichuka 
---

# Running Test Cases (SybaseToSQL)

When SSMA Tester runs a Test Case, it executes the objects selected for testing and creates a report about verification results. If the results are identical on both platforms, the test was successful. The correspondence of objects between Sybase and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is determined according to the schema-mapping settings for the current SSMA project.

A necessary requirement for a successful test is that all Sybase objects are converted and loaded into the target database. Also, the table data should be migrated so that the contents of the tables on both platforms are synchronized.

## Run Test Case

To run the prepared Test Case:

1. Click the **Run** button.
2. In the **Connect to Sybase** dialog box, enter the connection information, and then click **Connect**.

When the test is complete, the Test Case Report is created. Click the **Report** button to view the [Viewing Test Case Reports &#40;SybaseToSQL&#41;](../../ssma/sybase/viewing-test-case-reports-sybasetosql.md). The result of the test (Test Case Report) is automatically stored in the [Using Test Repositories &#40;SybaseToSQL&#41;](../../ssma/sybase/using-test-repositories-sybasetosql.md) for later use.

## Test Case Execution Steps

### Prerequisites

SSMA Tester checks if all prerequisites are met for the test execution before start of the test. If some conditions are not satisfied, an error message appears.

### Initialization

At this step, SSMA Tester creates auxiliary objects (tables, triggers, and views) both at the Sybase and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. They allow tracing changes made in the affected tables chosen for verification if table comparisons mode is **Changes only**.

Assume that the verified table is named `USER_TABLE`. For such a table, the following auxiliary objects are created in Sybase.

The following objects are created at Sybase in the `SSMATESTER2005db` or `SSMATESTER2008db` database and at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the `ssma_tester_changes` schema.

|Name|Type|Description|
|--------|--------|---------------|
|`USER_TABLE$Trg`|Trigger|Trigger auditing the changes in the verified table.|
|`USER_TABLE$Aud`|Table|Table where deleted and overwritten rows are saved.|
|`USER_TABLE$AudID`|Table|Table where new and changed rows are saved.|
|`USER_TABLE`|View|Simplified representation of the table modifications.|
|`USER_TABLE$new`|View|Simplified representation of inserted and overwritten rows.|
|`USER_TABLE$new_id`|View|Identification of inserted and changed rows.|
|`USER_TABLE$old`|View|Simplified representation of deleted and overwritten rows.|

The following object is created in the database of verified table at Sybase and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

|Name|Type|Description|
|--------|--------|---------------|
|`USER_TABLE$Trg`|Trigger|Trigger auditing the changes in the verified table.|

### Test Object Calls

At this step, SSMA Tester invokes each object selected for the testing, compares the results, and shows the report.

### Finalization

During the finalization SSMA Tester cleans up the auxiliary objects created at the **Initialization** step.

## Next Step

[Viewing Test Case Reports &#40;SybaseToSQL&#41;](../../ssma/sybase/viewing-test-case-reports-sybasetosql.md)

## See Also

[Selecting and Configuring Objects to Test &#40;SybaseToSQL&#41;](../../ssma/sybase/selecting-and-configuring-objects-to-test-sybasetosql.md)
[Selecting and Configuring Affected Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/selecting-and-configuring-affected-objects-sybasetosql.md)
[Testing Migrated Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/testing-migrated-database-objects-sybasetosql.md)
