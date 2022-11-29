---
description: "Query Parameters Dialog Box (Visual Database Tools)"
title: Query Parameters Dialog Box
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:69645"
  - "vdt.dlgbox.definequeryparameters"
ms.assetid: 31cdaee2-d7cd-4d64-a45f-924b27e8b1f0
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Query Parameters Dialog Box (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
This dialog allows you to enter values for the parameters defined in the query. This dialog box appears when you execute a query that contains parameters that require end-user input at run time.  
  
## Options  
**Name**  
Lists the parameters defined for the query being executed. If the query contains named parameters, the names appear in the list. If the query contains unnamed parameters, system-defined parameter names are listed for each parameter in the query.  
  
**Value**  
Enter the value for each parameter listed under **Name**. The value used most recently appears as the default parameter value.  
  
## Example  
The following query in the SQL Pane, opens the Query Parameters dialog box when executed in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
SELECT   FirstName, LastName  
FROM    Person.Person AS Lastname  
WHERE   (LastName = @Param1);  
```  
  
## See Also  
[Query with Parameters &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-parameters-visual-database-tools.md)  
  
