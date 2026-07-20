---
title: "Use OPENJSON with an Explicit Schema"
description: "Use OPENJSON with an Explicit Schema (SQL Server)"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay
ms.date: 07/23/2025
ms.service: sql
ms.topic: language-reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "OPENJSON, with explicit schema"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Use OPENJSON with an Explicit Schema

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

  Use `OPENJSON` with an explicit schema to return a table that's formatted as you specify in the `WITH` clause.  

 Here are some examples that use `OPENJSON` with an explicit schema. For more information, see [OPENJSON (Transact-SQL)](../../t-sql/functions/openjson-transact-sql.md).

## Example - Use the WITH clause to format the output

 The following query returns the results shown in the following table. Notice how the `AS JSON` clause causes values to be returned as JSON objects instead of scalar values in `col5` and `array_element`.

```sql
DECLARE @json NVARCHAR(MAX) =
N'{"someObject":   
    {"someArray":  
      [  
          {"k1": 11, "k2": null, "k3": "text"},  
          {"k1": 21, "k2": "text2", "k4": { "data": "text4" }},  
          {"k1": 31, "k2": 32},  
          {"k1": 41, "k2": null, "k4": { "data": false }}     
       ]  
    }  
 }'  

SELECT * FROM  
 OPENJSON(@json, N'lax $.someObject.someArray')  
WITH ( k1 int,   
        k2 varchar(100),  
        col3 varchar(6) N'$.k3',  
        col4 varchar(10) N'lax $.k4.data',  
        col5 nvarchar(MAX) N'lax $.k4' AS JSON, 
        array_element nvarchar(MAX) N'$' AS JSON  
 )  
```  

 **Results**  

|k1|k2|col3|col4|col5|array_element|  
|--------|--------|----------|----------|----------|--------------------|  
|11|*NULL*|"text"|*NULL*|*NULL*|{"k1": 11, "k2": null, "k3": "text"}|  
|21|"text2"|*NULL*|"text4"|{ "data": "text4" }|{"k1": true, "k2": "text2", "k4": { "data": "text4" } }|  
|31|"32"|*NULL*|*NULL*|*NULL*|{"k1": 31, "k2": 32 }|  
|41|*NULL*|*NULL*|false|{ "data": false }|{"k1": 41, "k2": null,       "k4": { "data": false }    }|  

## Example - Load JSON into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

 The following example loads an entire JSON object into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  

```sql
DECLARE @json NVARCHAR(MAX) = '{  
  "id" : 2,  
  "firstName": "John",  
  "lastName": "Smith",  
  "isAlive": true,  
  "age": 25,  
  "dateOfBirth": "2015-03-25T12:00:00",  
  "spouse": null  
  }';  

  INSERT INTO Person  
  SELECT *   
  FROM OPENJSON(@json)  
  WITH (id int,  
        firstName nvarchar(50), lastName nvarchar(50),   
        isAlive bit, age int,  
        dateOfBirth datetime2, spouse nvarchar(50))  
```  

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)

## Related content

- [OPENJSON (Transact-SQL)](../../t-sql/functions/openjson-transact-sql.md)
