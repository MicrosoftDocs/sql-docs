---
title: "Shape APPEND Clause | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "shape commands [ADO]"
  - "data shaping [ADO], APPEND clause"
  - "append clause [ADO]"
ms.assetid: f90fcf55-6b24-401d-94e1-d65bd24bd342
author: MightyPen
ms.author: genemi
manager: craigg
---
# Shape APPEND Clause
The shape command APPEND clause appends a column or columns to a **Recordset**. Frequently, these columns are chapter columns, which refer to a child **Recordset**.  
  
## Syntax  
  
```  
SHAPE [parent-command [[AS] parent-alias]] APPEND column-list  
```  
  
## Description  
 The parts of this clause are as follows:  
  
 *parent-command*  
 Zero or one of the following (you can omit the *parent-command* completely):  
  
-   A provider command enclosed in braces ("{}") that returns a **Recordset** object. The command is issued to the underlying data provider, and its syntax depends on the requirements of that provider. This will typically be the SQL language, although ADO does not require any particular query language.  
  
-   Another shape command embedded in parentheses.  
  
-   The TABLE keyword, followed by the name of a table in the data provider.  
  
 *parent-alias*  
 An optional alias that refers to the parent **Recordset**.  
  
 *column-list*  
 One or more of the following:  
  
-   An aggregate column.  
  
-   A calculated column.  
  
-   A new column created by using the NEW clause.  
  
-   A chapter column. A chapter column definition is enclosed in parentheses ("()"). See the following syntax.  
  
```  
SHAPE [parent-command [[AS] parent-alias]]  
   APPEND (child-recordset [ [[AS] child-alias]   
      RELATE parent-column TO child-column | PARAMETER param-number, ... ])  
   [[AS] chapter-alias]   
   [, ... ]  
```  
  
## Remarks  
 *child-recordset*  
 -   A provider command enclosed in braces ("{}") that returns a **Recordset** object. The command is issued to the underlying data provider, and its syntax depends on the requirements of that provider. This will typically be the SQL language, although ADO does not require any particular query language.  
  
-   Another shape command embedded in parentheses.  
  
-   The name of an existing shaped **Recordset**.  
  
-   The TABLE keyword, followed by the name of a table in the data provider.  
  
 *child-alias*  
 An alias that refers to the child **Recordset**.  
  
 *parent-column*  
 A column in the **Recordset** returned by the *parent-command.*  
  
 *child-column*  
 A column in the **Recordset** returned by the *child-command*.  
  
 *param-number*  
 See [Operation of Parameterized Commands](../../../ado/guide/data/operation-of-parameterized-commands.md).  
  
 *chapter-alias*  
 An alias that refers to the chapter column appended to the parent.  
  
> [!NOTE]
>  The *"parent-column* TO *child-column"* clause is actually a list, where each relation defined is separated by a comma  
  
> [!NOTE]
>  The clause after the APPEND keyword is actually a list, where each clause is separated by a comma and defines another column to be appended to the parent.  
  
## Remarks  
 When you construct provider commands from user input as part of a SHAPE command, SHAPE will treat the user-supplied a provider command as an opaque string and pass them faithfully to the provider. For example, in the following SHAPE command,  
  
```  
SHAPE {select * from t1} APPEND ({select * from t2} RELATE k1 TO k2)  
```  
  
 SHAPE will execute two commands: `select * from t1` and (`select * from t2 RELATE k1 TO k2)`. If the user supplies a compound command that consists of multiple provider commands separated by semicolons, SHAPE is not able to distinguish the difference. So in the following SHAPE command,  
  
```  
SHAPE {select * from t1; drop table t1} APPEND ({select * from t2} RELATE k1 TO k2)  
```  
  
 SHAPE executes `select * from t1; drop table t1` and (`select * from t2 RELATE k1 TO k2),` not realizing that `drop table t1` is a separate and in this case, dangerous, provider command. Applications must always validate the user input to prevent such potential hacker attacks from occurring.  
  
 This section contains the following topics.  
  
-   [Operation of Non-Parameterized Commands](../../../ado/guide/data/operation-of-non-parameterized-commands.md)  
  
-   [Operation of Parameterized Commands](../../../ado/guide/data/operation-of-parameterized-commands.md)  
  
-   [Hybrid Commands](../../../ado/guide/data/hybrid-commands.md)  
  
-   [Intervening Shape COMPUTE Clauses](../../../ado/guide/data/intervening-shape-compute-clauses.md)  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)
