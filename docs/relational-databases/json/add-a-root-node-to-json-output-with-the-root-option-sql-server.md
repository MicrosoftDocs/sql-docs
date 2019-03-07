---
title: "Add a Root Node to JSON Output with the ROOT Option (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/02/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "ROOT (FOR JSON)"
ms.assetid: b9afa74a-f59f-483e-a178-42be2e9882c9
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Add a Root Node to JSON Output with the ROOT Option (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  To add a single, top-level element to the JSON output of the **FOR JSON** clause, specify the **ROOT** option.  
  
 If you don't specify the **ROOT** option, the JSON output doesn't include a root element.  
  
## Examples  
 The following table shows the output of the **FOR JSON** clause with and without the **ROOT** option.  
  
 The examples in the following table assume that the optional *RootName* argument is empty. If you provide a name for the root element, this value replaces the value **root** in the examples.  
  
 Without the **ROOT** option  
  
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
  
 With the **ROOT** option  
  
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
  
 Here's another example of a **FOR JSON** clause with the **ROOT** option. This example specifies a value for the optional *RootName* argument.  
  
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

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
 
## See Also  
 [FOR Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-for-clause-transact-sql.md)  
  
  
