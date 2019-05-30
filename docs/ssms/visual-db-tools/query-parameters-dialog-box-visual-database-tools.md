---
title: "Query Parameters Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:69645"
  - "vdt.dlgbox.definequeryparameters"
ms.assetid: 31cdaee2-d7cd-4d64-a45f-924b27e8b1f0
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Query Parameters Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
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
  
