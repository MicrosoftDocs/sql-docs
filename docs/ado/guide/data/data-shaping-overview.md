---
title: "Data Shaping Overview | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "data shaping [ADO], overview"
ms.assetid: 4cb5fd29-4e56-46ac-ae48-a6771c321c0c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Shaping Overview
*Data shaping* means building hierarchical relationships between two or more logical entities in a query. The hierarchy can be seen in parent-child relationships between a record of one [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), and one or more records (also known as a chapter) of another **Recordset**. In a parent-child relationship, the parent **Recordset** contains the child **Recordset**. An example of such a hierarchical relationship is customers and orders. For every customer in a database, there can be zero or more orders. The hierarchical relationship can be recursive, meaning that grandchild records can be nested in a child record. In principle, a hierarchical record can be nested to any depth. In practice, ADO limits the recursion to a maximum of 512 **Recordset**s.  
  
 In general, columns of a shaped **Recordset** can contain data from a data provider such as Microsoft® SQL Server, references to another **Recordset**, values derived from a calculation on a single row of a **Recordset**, or values derived from an operation over a column of an entire **Recordset**. A column can also be newly fabricated and empty.  
  
 When you retrieve the value of a column that contains a reference to another **Recordset**, ADO automatically returns the actual **Recordset** represented by the reference. The reference to a **Recordset** is actually a reference to a subset of the child, called a *chapter*. A single parent can reference more than one child **Recordset**.  
  
 ADO support for data shaping enables you to query a data source and return a **Recordset** in which a (parent) record represents a (child) **Recordset**. In the customer-order scenario, you can use data shaping to retrieve customers' information as well as the orders placed by each customer in a single query. The resultant **Recordset** is also known as shaped **Recordset**.  
  
 In addition, data shaping in ADO allows you to create new **Recordset** objects without an underlying data source by using the **NEW** keyword to describe the fields of the parent and child **Recordsets**. The new **Recordset** object can then be populated with data and persistently stored. Developers can also perform various calculations or aggregations (for example, **SUM**, **AVG**, and **MAX**) on child fields. Data shaping can also create a parent **Recordset** from a child **Recordset** by grouping records in the child and placing one row in the parent for each group in the child.  
  
 Regular SQL allows you to retrieve data using **JOIN** syntax, but this can be inefficient and unwieldy because redundant parent data is repeated in each record returned for a given parent-child relationship. Data shaping can relate a single parent record in the parent **Recordset** to multiple child records in the child **Recordset**, avoiding the redundancy of a **JOIN**. Most people find the parent-child multiple **Recordset** programming model more natural and easier to work with than the single **Recordset JOIN** model.
