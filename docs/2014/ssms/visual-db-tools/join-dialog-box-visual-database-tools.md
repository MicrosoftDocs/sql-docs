---
title: "Join Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.ppg.joinline"
  - "vdtsql.chm:69638"
ms.assetid: 0d9516bb-4ad3-4fcf-bb77-93474dea698f
author: stevestein
ms.author: sstein
manager: craigg
---
# Join Dialog Box (Visual Database Tools)
  Use this dialog box to specify options for joining tables. To access this dialog, in the **Design** pane select a join line. Then in the **Properties** window click **Join Condition And Type**, and click the ellipses **(...)** that appear to the right of the property.  
  
 By default, related tables are joined using an inner join that creates a result set based on rows containing matching information in the join columns. By setting options in the **Join** dialog box, you can specify a join based on a different operator, and you can specify an outer join.  
  
 For more information about joining tables, see [Query with Joins &#40;Visual Database Tools&#41;](visual-database-tools.md).  
  
## Options  
  
|**Term**|**Definition**|  
|--------------|--------------------|  
|**Table**|The names of the tables or table-valued objects involved in the join. You cannot change the names of the tables here - this information is displayed for information only.|  
|**Column**|The names of the columns used for joining the tables. The operator in the operator list specifies the relationship between the data in the columns. You cannot change the names of the columns here - this information is displayed for information only.|  
|**Operator**|Specify the operator used to relate the join columns. To specify an operator other than equal (=), select it from the list. When you close the property page, the operator you selected will appear in the diamond graphic of the join line, as in the following:<br /><br /> ![Visual Database Tools icon](../../database-engine/media//dv3wbii.gif "Visual Database Tools icon")|  
|**All rows from \<table1>**|Specify that all the rows from the left table appear in the output, even if there are no corresponding matches in the right table. Columns with no matching data in the right table appear as null. Choosing this option is equivalent to specifying LEFT OUTER JOIN in the SQL statement.|  
|**All rows from \<table2>**|Specify that all the rows from the right table appear in the output, even if there are no corresponding matches in the left table. Columns with no matching data in the left table appear as null. Choosing this option is equivalent to specifying RIGHT OUTER JOIN in the SQL statement.|  
  
 Selecting both All **rows from \<table1>** and **All rows from \<table2>** is equivalent to specifying FULL OUTER JOIN in the SQL statement.  
  
 When you select an option to create an outer join, the diamond graphic in the join line changes to indicate that the join is a left outer, right outer, or full outer join.  
  
> [!NOTE]  
>  The words "left" and "right" do not necessarily correspond to the position of tables in the Diagram pane. "Left" refers to the table whose name appears to the left of the keyword JOIN in the SQL statement, and "right" refers to the table whose name appears to the right of the JOIN keyword. If you move tables in the **Diagram** pane, you do not change which table is considered left or right.  
  
## See Also  
 [Query with Joins &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
  
