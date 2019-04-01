---
title: "Execute SQL Task Editor (Result Set Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.executesqltask.resultset.f1"
helpviewer_keywords: 
  - "Execute SQL Task Editor"
ms.assetid: d27000c8-8d91-4e1c-b45e-bca9a3c12f6d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Execute SQL Task Editor (Result Set Page)
  Use the **Result Set** page of the **Execute SQL Task Editor** dialog to map the result of the SQL statement to new or existing variables. The options in this dialog box are disabled if **ResultSet** on the General page is set to **None**.  
  
 For more information about this task, see [Execute SQL Task](control-flow/execute-sql-task.md) and [Result Sets in the Execute SQL Task](../../2014/integration-services/result-sets-in-the-execute-sql-task.md).  
  
## Options  
 **Result Name**  
 After you have added a result set mapping set by clicking **Add**, provide a name for the result. Depending on the result set type, you must use specific result names.  
  
 If the result set type is **Single row**, you can use either the name of a column returned by the query or the number that represents the position of a column in the column list of a column returned by the query.  
  
 If the result set type is **Full result set** or **XML**, you must use 0 as the result set name.  
  
 **Related Topics**: [Result Sets in the Execute SQL Task](../../2014/integration-services/result-sets-in-the-execute-sql-task.md)  
  
 **Variable Name**  
 Map the result set to a variable by selecting a variable or click \<**New variable...**> to add a new variable by using the **Add Variable** dialog box.  
  
 **Add**  
 Click to add a result set mapping.  
  
 **Remove**  
 Select a result set mapping in the list and then click **Remove**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Execute SQL Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Execute SQL Task Editor &#40;Parameter Mapping Page&#41;](../../2014/integration-services/execute-sql-task-editor-parameter-mapping-page.md)   
 [Transact-SQL Reference &#40;Database Engine&#41;](/sql/t-sql/language-reference)  
  
  
