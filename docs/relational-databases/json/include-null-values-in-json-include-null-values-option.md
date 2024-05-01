---
title: "Include Null Values in JSON - INCLUDE_NULL_VALUES Option"
description: "Include Null Values in JSON - INCLUDE_NULL_VALUES Option"
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth
ms.date: 06/03/2020
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "INCLUDE_NULL_VALUES (FOR JSON)"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Include Null Values in JSON - INCLUDE_NULL_VALUES Option

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-serverless-pool-only.md)]

  To include null values in the JSON output of the **FOR JSON** clause, specify the **INCLUDE_NULL_VALUES** option.  
  
 If you don't specify the **INCLUDE_NULL_VALUES** option, the JSON output doesn't include properties for values that are null in the query results.  
  
## Examples  
 The following example shows the output of the **FOR JSON** clause with and without the **INCLUDE_NULL_VALUES** option.  
  
|Without the **INCLUDE_NULL_VALUES** option|With the **INCLUDE_NULL_VALUES** option|  
|--------------------------------------------------|-----------------------------------------------|  
|`{    "name": "John",    "surname": "Doe" }`|`{    "name": "John",    "surname": "Doe",    "age": null,    "phone": null }`|  
  
 Here's another example of a **FOR JSON** clause with the **INCLUDE_NULL_VALUES** option.  
  
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

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

> [!NOTE]
> Some of the video links in this section may not work at this time. Microsoft is migrating content formerly on Channel 9 to a new platform. We will update the links as the videos are migrated to the new platform.

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven-SQLServer2016/JSON-as-bridge-betwen-NoSQL-relational-worlds)

## See Also  
 [FOR Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-for-clause-transact-sql.md)  
  
  
