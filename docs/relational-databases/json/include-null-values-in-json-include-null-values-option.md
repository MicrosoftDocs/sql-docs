---
title: "Include Null Values in JSON - INCLUDE_NULL_VALUES Option"
description: "To include null values in the JSON output of the FOR JSON clause, specify the INCLUDE_NULL_VALUES option."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay
ms.date: 07/23/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "INCLUDE_NULL_VALUES (FOR JSON)"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Include Null Values in JSON - INCLUDE_NULL_VALUES Option

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

  To include null values in the JSON output of the `FOR JSON` clause, specify the `INCLUDE_NULL_VALUES` option.  

 If you don't specify the `INCLUDE_NULL_VALUES` option, the JSON output doesn't include properties for values that are null in the query results.  

## Examples

 The following example shows the output of the `FOR JSON` clause with and without the `INCLUDE_NULL_VALUES` option.  

|Without the `INCLUDE_NULL_VALUES` option|With the `INCLUDE_NULL_VALUES` option|  
|--------------------------------------------------|-----------------------------------------------|  
|`{    "name": "John",    "surname": "Doe" }`|`{    "name": "John",    "surname": "Doe",    "age": null,    "phone": null }`|  

 Here's another example of a `FOR JSON` clause with the `INCLUDE_NULL_VALUES` option.  

 **Query**  

```sql
SELECT name, surname  
FROM emp  
FOR JSON AUTO, INCLUDE_NULL_VALUES    
```  

 **Result**  

```json
[{
    "name": "John",
    "surname": null
}, {
    "name": "Jane",
    "surname": "Doe"
}] 
```  

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)

## Related content

- [SELECT - FOR clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
