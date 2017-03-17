---
title: "Index JSON data | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2016-06-01"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "JSON, indexing JSON data"
  - "indexing JSON data"
ms.assetid: ced241e1-ff09-4d6e-9f04-a594a9d2f25e
caps.latest.revision: 9
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Index JSON data
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Database indexes improve the performance of your filter and sort operations. Without indexes, SQL Server has to perform a full table scan every time you query data.  
  
 JSON is  not a built-in data type in SQL Server 2016, and SQL Server 2016 does not have custom JSON indexes. However, you can optimize your queries over JSON documents by using standard indexes.  
  
## Index JSON properties by using computed columns  
 When you store JSON data in SQL Server, typically you want to filter or sort query results by properties of the JSON documents.  
  
 In this example, the AdventureWorks SalesOrderHeader table has an “Info” column that contains various information about sales orders - for example, information about customer, sales person, shipping/billing addresses, and so forth. You want to use values from the Info column to filter sales orders for a customer. Here's the query that you want to optimize by using an index.  
  
```tsql  
SELECT SalesOrderNumber,OrderDate,JSON_VALUE(Info,'$.Customer.Name') AS CustomerName
FROM Sales.SalesOrderHeader
WHERE JSON_VALUE(Info,'$.Customer.Name')=N'Aaron Campbell' 
```  
  
 If you want to speed up your filters or ORDER BY clauses over a property in a JSON document, you can use the same indexes that you're using on  other columns. However, you can't directly reference properties in the JSON documents. First you have to create a "virtual column" that returns the values that you want to use for filtering. Then you have to create an index on that virtual column.  
  
 The following example creates a computed column that can be used for indexing and then creates an index on that column. This example creates a column that exposes the customer name, which is stored in the $.Customer.Name path in JSON documents.  
  
```tsql  
ALTER TABLE Sales.SalesOrderHeader
ADD vCustomerName AS JSON_VALUE(Info,'$.Customer.Name')

CREATE INDEX idx_soh_json_CustomerName
ON Sales.SalesOrderHeader(vCustomerName)  
```  
  
 The computed column is not persisted. It does not occupy additional space in the table. It's computed only when the index needs to be rebuilt.  
  
 It's important that you create the computed column with the same expression that you plan to use in your queries - in this example, `JSON_VALUE(Info, '$.Customer.Name')`.  
  
 You don’t have to rewrite your queries. If you use expressions with the JSON_VALUE function, SQL Server sees that there's an equivalent computed column with the same expression and applies an index if possible. Here's the  execution plan for the query in this example.  
  
 ![Execution plan](../../relational-databases/json/media/jsonindexblog1.png "Execution plan")  
  
 Instead of a full table scan, SQL Server uses an index seek into the non-clustered index and finds the rows that satisfy the specified conditions. Then it uses a key lookup in the SalesOrderHeader table to fetch other columns that are referenced in the query -  in this example, SalesOrderNumber and OrderDate.  
  
 You can avoid this additional lookup in the table if you add required columns in the JSON index. You can add these columns as standard included columns, as shown in the following example.  
  
```tsql  
CREATE INDEX idx_soh_json_CustomerName
ON Sales.SalesOrderHeader(vCustomerName)
INCLUDE(SalesOrderNumber,OrderDate)
```  
  
 In this case SQL Server doesn't read additional data from the SalesOrderHeader table because everything it needs is included in the non-clustered JSON index. This is a ,good way to combine JSON and column data in queries and to create optimal indexes for your workload.  
  
## JSON indexes are collation-aware indexes  
 An important feature of JSON indexes is that they are collation-aware. The result of the JSON_VALUE function is a text value that inherits its collation from the input expression. Therefore, values in the index are ordered using the   collation rules defined in the source columns.  
  
 To demonstrate this, the following example creates a simple collection table with a primary key and JSON content.  
  
```tsql  
CREATE TABLE JsonCollection
 (
  id INT IDENTITY CONSTRAINT PK_JSON_ID PRIMARY KEY,
  json NVARCHAR(MAX) COLLATE SERBIAN_CYRILLIC_100_CI_AI
  CONSTRAINT [Content should be formatted as JSON]
  CHECK(ISJSON(json)>0)
 ) 
```  
  
 The preceding command specifies the Serbian Cyrillic collation for the JSON column. The following example populates the table and creates an index on the name property.  
  
```tsql  
INSERT INTO JsonCollection
VALUES
(N'{"name":"Иво","surname":"Андрић"}'),
(N'{"name":"Андрија","surname":"Герић"}'),
(N'{"name":"Владе","surname":"Дивац"}'),
(N'{"name":"Новак","surname":"Ђоковић"}'),
(N'{"name":"Предраг","surname":"Стојаковић"}'),
(N'{"name":"Михајло","surname":"Пупин"}'),
(N'{"name":"Борислав","surname":"Станковић"}'),
(N'{"name":"Владимир","surname":"Грбић"}'),
(N'{"name":"Жарко","surname":"Паспаљ"}'),
(N'{"name":"Дејан","surname":"Бодирога"}'),
(N'{"name":"Ђорђе","surname":"Вајферт"}'),
(N'{"name":"Горан","surname":"Бреговић"}'),
(N'{"name":"Милутин","surname":"Миланковић"}'),
(N'{"name":"Никола","surname":"Тесла"}')
GO  
ALTER TABLE JsonCollection
ADD vName AS JSON_VALUE(json,'$.name')

CREATE INDEX idx_name
ON JsonCollection(vName)
```  
  
 The preceding commands create a standard index on the computed column vName, which represents the value from the JSON $.name property. In the Serbian Cyrillic code page, the order of the letters is ‘А’,’Б’,’В’,’Г’,’Д’,’Ђ’,’Е’, etc. The order of items in the index is compliant with Serbian Cyrillic rules because the result of the JSON_VALUE function inherits its collation from the source column. The following example queries this collection and sorts the results by name.  
  
```tsql  
SELECT JSON_VALUE(json,'$.name'),*
FROM JsonCollection
ORDER BY JSON_VALUE(json,'$.name')
```  
  
 If you look at the actual execution plan, you see that it uses sorted values from the non-clustered index.  
  
 ![Execution plan](../../relational-databases/json/media/jsonindexblog2.png "Execution plan")  
  
 Although the query has an ORDER BY clause, the execution plan doesn't use a Sort operator. The JSON index is already ordered according to Serbian Cyrillic rules. Therefore SQL Server can use the non-clustered index where results are already sorted.  
  
 However, if we change collation of the order by expression  - for example, if we put `COLLATE French_100_CI_AS_SC` after the JSON_VALUE function - we get a different query execution plan.  
  
 ![Execution plan](../../relational-databases/json/media/jsonindexblog3.png "Execution plan")  
  
 Since the order of values in the index is not compliant with French collation rules, SQL Server can't use the index to order results. Therefore, it adds a Sort operator that sorts results using French collation rules.  
  
  