---
title: "Operation of Parameterized Commands | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "data shaping [ADO], parameterized commands"
  - "parameterized commands [ADO]"
ms.assetid: 4fae0d54-83b6-4ead-99cc-bcf532daa121
author: MightyPen
ms.author: genemi
manager: craigg
---
# Operation of Parameterized Commands
If you are working with a large child **Recordset**, especially compared to the size of the parent **Recordset**, but need to access only a few child chapters, you might find it more efficient to use a parameterized command.  
  
 A *non-parameterized command* retrieves both the entire parent and child **Recordsets**, appends a chapter column to the parent, and then assigns a reference to the related child chapter for each parent row.  
  
 A *parameterized command* retrieves the entire parent **Recordset**, but retrieves only the chapter **Recordset** when the chapter column is accessed. This difference in retrieval strategy can yield significant performance benefits.  
  
 For example, you can specify the following:  
  
```  
SHAPE {SELECT * FROM customer}   
   APPEND ({SELECT * FROM orders WHERE cust_id = ?}   
   RELATE cust_id TO PARAMETER 0)  
```  
  
 The parent and child tables have a column name in common, cust_id*.* The *child-command* has a "?" placeholder, to which the RELATE clause refers (that is, "...PARAMETER 0").  
  
> [!NOTE]
>  The PARAMETER clause pertains solely to the shape command syntax. It is not associated with either the ADO [Parameter](../../../ado/reference/ado-api/parameter-object.md) object or the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection.  
  
 When the parameterized shape command is executed, the following occurs:  
  
1.  The *parent-command* is executed and returns a parent **Recordset** from the Customers table.  
  
2.  A chapter column is appended to the parent **Recordset**.  
  
3.  When the chapter column of a parent row is accessed, the *child-command* is executed using the value of the customer.cust_id as the value of the parameter.  
  
4.  All rows in the data provider rowset created in step 3 are used to populate the child **Recordset**. In this example, that is all the rows in the Orders table in which the cust_id equals the value of customer.cust_id. By default, the child **Recordset**s will be cached on the client until all references to the parent **Recordset** are released. To change this behavior, set the **Recordset** [dynamic property](../../../ado/reference/ado-api/ado-dynamic-property-index.md) **Cache Child Rows** to **False**.  
  
5.  A reference to the retrieved child rows (that is, the chapter of the child **Recordset**) is placed in the chapter column of the current row of the parent **Recordset**.  
  
6.  Steps 3-5 are repeated when the chapter column of another row is accessed.  
  
 The **Cache Child Rows** dynamic property is set to **True** by default. The caching behavior varies depending upon the parameter values of the query. In a query with a single parameter, the child **Recordset** for a given parameter value will be cached between requests for a child with that value. The following code demonstrates this:  
  
```  
SCmd = "SHAPE {select * from customer} " & _  
         "APPEND({select * from orders where cust_id = ?} " & _  
         "RELATE cust_id TO PARAMETER 0) AS chpCustOrder"  
Rst1.Open sCmd, Cnn1  
Set RstChild = Rst1("chpCustOrder").Value  
Rst1.MoveNext      ' Next cust_id passed to Param 0, & new rs fetched   
                   ' into RstChild.  
Rst1.MovePrevious  ' RstChild now holds cached rs, saving round trip.  
```  
  
 In a query with two or more parameters, a cached child is used only if all the parameter values match the cached values.  
  
## Parameterized Commands and Complex Parent Child Relations  
 In addition to using parameterized commands to improve performance of an equi-join type hierarchy, parameterized commands can be used to support more complex parent-child relationships. For example, consider a Little League database with two tables: one consisting of the teams (team_id, team_name) and the other of games (date, home_team, visiting_team).  
  
 Using a non-parameterized hierarchy, there is no way to relate the teams and games tables in such a way that the child **Recordset** for each team contains its complete schedule. You can create chapters that contain the home schedule or the road schedule, but not both. This is because the RELATE clause limits you to parent-child relationships of the form (pc1=cc1) AND (pc2=pc2). So, if your command included "RELATE team_id TO home_team, team_id TO visiting_team", you would get only games where a team was playing itself. What you want is "(team_id=home_team) OR (team_id=visiting_team)", but the Shape provider does not support the OR clause.  
  
 To obtain the desired result, you can use a parameterized command. For example:  
  
```  
SHAPE {SELECT * FROM teams}   
APPEND ({SELECT * FROM games WHERE home_team = ? OR visiting_team = ?}   
        RELATE team_id TO PARAMETER 0,   
               team_id TO PARAMETER 1)   
```  
  
 This example exploits the greater flexibility of the SQL WHERE clause to get the result you need.  
  
> [!NOTE]
>  When using WHERE clauses, parameters can not use the SQL data types for text, ntext and image or an error will result that contains the following description: `Invalid operator for data type`.  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)
