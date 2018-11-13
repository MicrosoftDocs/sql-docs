---
title: "Shape Commands in General | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "shape commands [ADO]"
  - "data shaping [ADO], shape commands"
ms.assetid: 1fac7831-a187-4b15-9b43-aad380c5556c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Shape Commands in General
Data shaping defines the columns of a shaped **Recordset**, the relationships between the entities represented by the columns, and the manner in which the **Recordset** is populated with data.  
  
 A shaped **Recordset** can consist of the following types of columns.  
  
|Column type|Description|  
|-----------------|-----------------|  
|data|Fields from a **Recordset** returned by a query command to a data provider, table, or previously shaped **Recordset**.|  
|chapter|A reference to another **Recordset**, called a *chapter*. Chapter columns make it possible to define a *parent-child* relationship where the *parent* is the **Recordset** that contains the chapter column and the *child* is the **Recordset** represented by the chapter.|  
|aggregate|The value of the column is derived by executing an *aggregate function* on all the rows or a column of all the rows of a child **Recordset**. (See Aggregate Functions in the following topic, [Aggregate Functions, the CALC Function, and the NEW Keyword](../../../ado/guide/data/aggregate-functions-the-calc-function-and-the-new-keyword.md).)|  
|calculated expression|The value of the column is derived by calculating a Visual Basic for Applications expression on columns in the same row of the **Recordset**. The expression is the argument to the CALC function. (See Calculated Expression in the following topic, [Aggregate Functions, the CALC Function, and the NEW Keyword](../../../ado/guide/data/aggregate-functions-the-calc-function-and-the-new-keyword.md) and in [Visual Basic for Applications Functions](../../../ado/guide/data/visual-basic-for-applications-functions.md).)|  
|new|Empty, fabricated fields, which can be populated with data at a later time. The column is defined with the NEW keyword. (See NEW keyword in the following topic, [Aggregate Functions, the CALC Function, and the NEW Keyword](../../../ado/guide/data/aggregate-functions-the-calc-function-and-the-new-keyword.md).)|  
  
 A shape command can contain a clause that specifies a query command to an underlying data provider that will return a **Recordset** object. The query's syntax depends on the requirements of the underlying data provider. This will usually be SQL, although ADO does not require the use of any particular query language.  
  
 Shape commands can be issued by **Recordset** objects or by setting the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) property of the [Command](../../../ado/reference/ado-api/command-object-ado.md) object and then calling the [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method.  
  
 You could use a SQL JOIN clause to relate two tables; however, a hierarchical **Recordset** can represent the information more efficiently. Each row of a **Recordset** created by a JOIN repeats information redundantly from one of the tables. A hierarchical **Recordset** has only one parent **Recordset** for each of multiple child **Recordset** objects.  
  
 Shape commands can be nested. That is, the *parent-command* or *child-command* may itself be another shape command.  
  
 The shape provider always returns a client cursor, even when the user specifies a cursor location of **adUseServer**.  
  
 You can access the **Recordset** components of the shaped **Recordset** programmatically or through an appropriate visual control.  
  
 Microsoft provides a visual tool that generates shape commands (see the [Data Environment Designer](https://go.microsoft.com/fwlink/?LinkId=5689) in the Visual Basic 6 documentation) and another that displays hierarchical cursors (see "Using the Microsoft Hierarchical Flexgrid Control" in the Visual Basic 6 documentation).  
  
 For information about navigating a hierarchical **Recordset**, see [Accessing Rows in a Hierarchical Recordset](../../../ado/guide/data/accessing-rows-in-a-hierarchical-recordset.md).  
  
 For precise information about syntactically correct shape commands, see [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md).  
  
 This section contains the following topics.  
  
-   [Aggregate Functions, the CALC Function, and the NEW Keyword](../../../ado/guide/data/aggregate-functions-the-calc-function-and-the-new-keyword.md)  
  
-   [Issuing Commands to the Underlying Data Provider](../../../ado/guide/data/issuing-commands-to-the-underlying-data-provider.md)
