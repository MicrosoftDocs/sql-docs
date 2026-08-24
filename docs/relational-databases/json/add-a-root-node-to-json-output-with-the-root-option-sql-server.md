---
title: "Add a Root Node to JSON Output with the ROOT Option"
description: "To add a single, top-level element to the JSON output of the FOR JSON clause, specify the ROOT option."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay
ms.date: 07/23/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "ROOT (FOR JSON)"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Add a Root Node to JSON Output with the ROOT Option

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

  To add a single, top-level element to the JSON output of the `FOR JSON` clause, specify the `ROOT` option.  

 If you don't specify the `ROOT` option, the JSON output doesn't include a root element.  

## Examples

 The following table shows the output of the `FOR JSON` clause with and without the `ROOT` option.  

 The examples in the following table assume that the optional *RootName* argument is empty. If you provide a name for the root element, this value replaces the value `root` in the examples.  

 **Without the `ROOT` option:**

```json
{  
   <<json properties>>  
}  
```  

```json
[  
   <<json array elements>>  
]  
```  

 **With the `ROOT` option:**

```json
{   
  "root": {  
   <<json properties>>  
 }  
}  
```  

```json
{   
  "root": [  
   << json array elements >>  
  ]  
}  
```  

 Here's another example of a `FOR JSON` clause with the `ROOT` option. This example specifies a value for the optional `RootName` argument.  

 **Query**  

```sql
SELECT TOP 5   
       BusinessEntityID As Id,  
       FirstName, LastName,  
       Title As 'Info.Title',  
       MiddleName As 'Info.MiddleName'  
   FROM Person.Person  
   FOR JSON PATH, ROOT('info')
```  

 **Result**  

```json
{
    "info": [{
        "Id": 1,
        "FirstName": "Ken",
        "LastName": "Sánchez",
        "Info": {
            "MiddleName": "J"
        }
    }, {
        "Id": 2,
        "FirstName": "Terri",
        "LastName": "Duffy",
        "Info": {
            "MiddleName": "Lee"
        }
    }, {
        "Id": 3,
        "FirstName": "Roberto",
        "LastName": "Tamburello"
    }, {
        "Id": 4,
        "FirstName": "Rob",
        "LastName": "Walters"
    }, {
        "Id": 5,
        "FirstName": "Gail",
        "LastName": "Erickson",
        "Info": {
            "Title": "Ms.",
            "MiddleName": "A"
        }
    }]
}
```  

 **Result (without root)**  

```json
[{
    "Id": 1,
    "FirstName": "Ken",
    "LastName": "Sánchez",
    "Info": {
        "MiddleName": "J"
    }
}, {
    "Id": 2,
    "FirstName": "Terri",
    "LastName": "Duffy",
    "Info": {
        "MiddleName": "Lee"
    }
}, {
    "Id": 3,
    "FirstName": "Roberto",
    "LastName": "Tamburello"
}, {
    "Id": 4,
    "FirstName": "Rob",
    "LastName": "Walters"
}, {
    "Id": 5,
    "FirstName": "Gail",
    "LastName": "Erickson",
    "Info": {
        "Title": "Ms.",
        "MiddleName": "A"
    }
}]
```  

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)

## Related content

- [SELECT - FOR clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
