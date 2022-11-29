---
description: "Add a Root Node to JSON Output with the ROOT Option (SQL Server)"
title: "Add a Root Node to JSON Output with the ROOT Option"
ms.date: 06/03/2020
ms.service: sql
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "ROOT (FOR JSON)"
ms.assetid: b9afa74a-f59f-483e-a178-42be2e9882c9
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth
ms.custom: seo-dt-2019
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Add a Root Node to JSON Output with the ROOT Option (SQL Server)
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sqlserver2016-asdb.md)]

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

> [!NOTE]
> Some of the video links in this section may not work at this time. Microsoft is migrating content formerly on Channel 9 to a new platform. We will update the links as the videos are migrated to the new platform.

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven-SQLServer2016/JSON-as-bridge-betwen-NoSQL-relational-worlds)
 
## See Also  
 [FOR Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-for-clause-transact-sql.md)  
  
  
