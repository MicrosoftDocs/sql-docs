---
description: "Perform an Incremental Load of Multiple Tables"
title: "Perform an Incremental Load of Multiple Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],multiple tables"
ms.assetid: 39252dd5-09c3-46f9-a17b-15208cfd336d
author: chugugrace
ms.author: chugu
---
# Perform an Incremental Load of Multiple Tables

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  In the topic, [Improving Incremental Loads with Change Data Capture](../../integration-services/change-data-capture/change-data-capture-ssis.md), the diagram illustrates a basic package that performs an incremental load on just one table. However, loading one table is not as common as having to perform an incremental load of multiple tables.  
  
 When you perform an incremental load of multiple tables, some steps have to be performed once for all the tables, and other steps have to be repeated for each source table. You have more than one option for implementing these steps in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]:  
  
-   Use a parent package and child packages.  
  
-   Use multiple Data Flow tasks in a single package.  
  
## Loading Multiple Tables by Using a Parent Package and Multiple Child Packages  
 You can use a parent package to perform those steps that only have to be done once. The child packages will perform those steps that have to be done for each source table.  
  
#### To create a parent package that performs those steps that only have to be done once  
  
1.  Create a parent package.  
  
2.  In the control flow, use an Execute SQL task or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expressions to calculate the endpoints.  
  
     For an example of how to calculate endpoints, see [Specify an Interval of Change Data](../../integration-services/change-data-capture/specify-an-interval-of-change-data.md).  
  
3.  If needed, use a For Loop container to delay execution until change data for the selected period is ready.  
  
     For an example of such a For Loop container, see [Determine Whether the Change Data Is Ready](../../integration-services/change-data-capture/determine-whether-the-change-data-is-ready.md).  
  
4.  Use multiple Execute Package tasks to execute child packages for each table to be loaded. Pass the endpoints calculated in the parent package to each child package by using Parent Package Variable configurations.  
  
     For more information, see [Execute Package Task](../../integration-services/control-flow/execute-package-task.md) and [Use the Values of Variables and Parameters in a Child Package](../../integration-services/packages/legacy-package-deployment-ssis.md#child).  
  
#### To create child packages to perform those steps that have to be done for each source table  
  
1.  For each source table, create a child package.  
  
2.  In the control flow, use a Script task or an Execute SQL task to assemble the SQL statement that will be used to query for changes.  
  
     For an example of how to assemble the query, see [Prepare to Query for the Change Data](../../integration-services/change-data-capture/prepare-to-query-for-the-change-data.md).  
  
3.  Use a single Data Flow task in each child package to load the change data and apply it to the destination. Configure the Data Flow as described in the following steps:  
  
    1.  In the data flow, use a source component to query the change tables for the changes that fall within the selected endpoints.  
  
         For an example of how to query the change tables, see [Retrieve and Understand the Change Data](../../integration-services/change-data-capture/retrieve-and-understand-the-change-data.md).  
  
    2.  Use a Conditional Split transformation to direct inserts, updates, and deletes to different outputs for appropriate processing.  
  
         For an example of how to configure this transformation to direct output, see [Process Inserts, Updates, and Deletes](../../integration-services/change-data-capture/process-inserts-updates-and-deletes.md).  
  
    3.  Use a destination component to apply the inserts to the destination. Use OLE DB Command transformations with parameterized UPDATE and DELETE statements to apply updates and deletes to the destination.  
  
         For an example of how to use this transformation to apply updates and deletes, see [Apply the Changes to the Destination](../../integration-services/change-data-capture/apply-the-changes-to-the-destination.md).  
  
## Loading Multiple Tables by Using Multiple Data Flow Tasks in a Single Package  
 Alternatively, you can use a single package that contains a separate Data Flow task for each source table to be loaded.  
  
#### To load multiple tables by using multiple Data Flow tasks in a single package  
  
1.  Create a single package.  
  
2.  In the control flow, use an Execute SQL Task or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expressions to calculate the endpoints.  
  
     For an example of how to calculate endpoints, see [Specify an Interval of Change Data](../../integration-services/change-data-capture/specify-an-interval-of-change-data.md).  
  
3.  If needed, use a For Loop container to delay execution until the change data for the selected interval is ready.  
  
     For an example of such a For Loop container, see [Determine Whether the Change Data Is Ready](../../integration-services/change-data-capture/determine-whether-the-change-data-is-ready.md).  
  
4.  Use a Script task or an Execute SQL task to assemble the SQL statement that will be used to query for changes.  
  
     For an example of how to assemble the query, see [Prepare to Query for the Change Data](../../integration-services/change-data-capture/prepare-to-query-for-the-change-data.md).  
  
5.  Use multiple Data Flow tasks to load the change data from each source table and apply it to the destination. Configure each Data Flow task as described in the following steps.  
  
    1.  In each data flow, use a source component to query the change tables for the changes that fall within the selected endpoints.  
  
         For an example of how to query the change tables, see [Retrieve and Understand the Change Data](../../integration-services/change-data-capture/retrieve-and-understand-the-change-data.md).  
  
    2.  Use a Conditional Split transformation to direct inserts, updates, and deletes to different outputs for appropriate processing.  
  
         For an example of how to configure this transformation to direct output, see [Process Inserts, Updates, and Deletes](../../integration-services/change-data-capture/process-inserts-updates-and-deletes.md).  
  
    3.  Use a destination component to apply the inserts to the destination. Use OLE DB Command transformations with parameterized UPDATE and DELETE statements to apply updates and deletes to the destination.  
  
         For an example of how to use this transformation to apply updates and deletes, see [Apply the Changes to the Destination](../../integration-services/change-data-capture/apply-the-changes-to-the-destination.md).  
  
  
