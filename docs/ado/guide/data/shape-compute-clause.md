---
title: "Shape COMPUTE Clause | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "shape commands [ADO]"
  - "compute clause [ADO]"
  - "data shaping [ADO], COMPUTE clause"
ms.assetid: 3fdfead2-b5ab-4163-9b1d-3d2143a5db8c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Shape COMPUTE Clause
A shape COMPUTE clause generates a parent **Recordset**, whose columns consist of a reference to the child **Recordset**; optional columns whose contents are chapter, new, or calculated columns, or the result of executing aggregate functions on the child **Recordset** or a previously shaped **Recordset**; and any columns from the child **Recordset** listed in the optional BY clause.  
  
## Syntax  
  
```  
SHAPE child-command [AS] child-alias  
   COMPUTE child-alias [[AS] name], [appended-column-list]  
   [BY grp-field-list]  
```  
  
## Description  
 The parts of this clause are as follows:  
  
 *child-command*  
 Consists of one of the following:  
  
-   A query command within curly braces ("{}") that returns a child **Recordset** object. The command is issued to the underlying data provider, and its syntax depends on the requirements of that provider. This will typically be the SQL language, although ADO does not require any particular query language.  
  
-   The name of an existing shaped **Recordset**.  
  
-   Another shape command.  
  
-   The TABLE keyword, followed by the name of a table in the data provider.  
  
 *child-alias*  
 An alias used to refer to the **Recordset** returned by the *child-command.* The *child-alias* is required in the list of columns in the COMPUTE clause and defines the relation between the parent and child **Recordset** objects.  
  
 *appended-column-list*  
 A list in which each element defines a column in the generated parent. Each element contains either a chapter column, a new column, a calculated column, or a value resulting from an aggregate function on the child **Recordset**.  
  
 *grp-field-list*  
 A list of columns in the parent and child **Recordset** objects that specifies how rows should be grouped in the child.  
  
 For each column in the *grp-field-list,* there is a corresponding column in the child and parent **Recordset** objects. For each row in the parent **Recordset**, the *grp-field-list* columns have unique values, and the child **Recordset** referenced by the parent row consists solely of child rows whose *grp-field-list* columns have the same values as the parent row.  
  
 If the BY clause is included, the child **Recordset**'s rows will be grouped based on the columns in the COMPUTE clause. The parent **Recordset** will contain one row for each group of rows in the child **Recordset**.  
  
 If the BY clause is omitted, the entire child **Recordset** is treated as a single group and the parent **Recordset** will contain exactly one row. That row will reference the entire child **Recordset**. Omitting the BY clause allows you to compute "grand total" aggregates over the entire child **Recordset**.  
  
 For example:  
  
```  
SHAPE {select * from Orders} AS orders             COMPUTE orders, SUM(orders.OrderAmount) as TotalSales         
```  
  
 Regardless of which way the parent **Recordset** is formed (using COMPUTE or using APPEND), it will contain a chapter column that is used to relate it to a child **Recordset**. If you wish, the parent **Recordset** may also contain columns that contain aggregates (SUM, MIN, MAX, and so on) over the child rows. Both the parent and the child **Recordset** may contain columns that contain an expression on the row in the **Recordset**, as well as columns that are new and initially empty.  
  
## Operation  
 The *child-command* is issued to the provider, which returns a child **Recordset**.  
  
 The COMPUTE clause specifies the columns of the parent **Recordset**, which may be a reference to the child **Recordset**, one or more aggregates, a calculated expression, or new columns. If there is a BY clause, the columns it defines are also appended to the parent **Recordset**. The BY clause specifies how the rows of the child **Recordset** are grouped.  
  
 For example, assume you have a table, named Demographics, which consists of State, City, and Population fields. (The population figures in the table are provided solely as an example).  
  
|State|City|Population|  
|-----------|----------|----------------|  
|WA|Seattle|700,000|  
|OR|Medford|200,000|  
|OR|Portland|400,000|  
|CA|Los Angeles|800,000|  
|CA|San Diego|600,000|  
|WA|Tacoma|500,000|  
|OR|Corvallis|300,000|  
  
 Now, issue this shape command:  
  
```  
rst.Open  "SHAPE {select * from demographics} AS rs "  & _  
          "COMPUTE rs, SUM(rs.population) BY state", _  
           objConnection  
```  
  
 This command opens a shaped **Recordset** with two levels. The parent level is a generated **Recordset** with an aggregate column (`SUM(rs.population)`), a column referencing the child **Recordset** (`rs`), and a column for grouping the child **Recordset** (`state`). The child level is the **Recordset** returned by the query command (`select * from demographics`).  
  
 The child **Recordset** detail rows will be grouped by state, but otherwise in no particular order. That is, the groups will not be in alphabetical or numerical order. If you want the parent **Recordset** to be ordered, you can use the **Recordset Sort** method to order the parent **Recordset**.  
  
 You can now navigate the opened parent **Recordset** and access the child detail **Recordset** objects. For more information, see [Accessing Rows in a Hierarchical Recordset](../../../ado/guide/data/accessing-rows-in-a-hierarchical-recordset.md).  
  
## Resultant Parent and Child Detail Recordsets  
  
### Parent  
  
|SUM (rs.Population)|rs|State|  
|---------------------------|--------|-----------|  
|1,300,000|Reference to child1|CA|  
|1,200,000|Reference to child2|WA|  
|1,100,000|Reference to child3|OR|  
  
## Child1  
  
|State|City|Population|  
|-----------|----------|----------------|  
|CA|Los Angeles|800,000|  
|CA|San Diego|600,000|  
  
## Child2  
  
|State|City|Population|  
|-----------|----------|----------------|  
|WA|Seattle|700,000|  
|WA|Tacoma|500,000|  
  
## Child3  
  
|State|City|Population|  
|-----------|----------|----------------|  
|OR|Medford|200,000|  
|OR|Portland|400,000|  
|OR|Corvallis|300,000|  
  
## See Also  
 [Accessing Rows in a Hierarchical Recordset](../../../ado/guide/data/accessing-rows-in-a-hierarchical-recordset.md)   
 [Data Shaping Overview](../../../ado/guide/data/data-shaping-overview.md)   
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [Required Providers for Data Shaping](../../../ado/guide/data/required-providers-for-data-shaping.md)   
 [Shape APPEND Clause](../../../ado/guide/data/shape-append-clause.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)   
 [Value Property (ADO)](../../../ado/reference/ado-api/value-property-ado.md)   
 [Visual Basic for Applications functions](../../../ado/guide/data/visual-basic-for-applications-functions.md)
