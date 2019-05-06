---
title: "Execute SQL Task Editor (Parameter Mapping Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.executesqltask.parametermapping.f1"
helpviewer_keywords: 
  - "Execute SQL Task Editor"
ms.assetid: 8ebe020a-7921-46b2-8823-398748f379b2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Execute SQL Task Editor (Parameter Mapping Page)
  Use the **Parameter Mapping** page of the **Execute SQL Task Editor** dialog box to map variables to parameters in the SQL statement.  
  
 To learn about this task, see [Execute SQL Task](control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md).  
  
## Options  
 **Variable Name**  
 After you have added a parameter mapping by clicking **Add**, select a system or user-defined variable from the list or click \<**New variable...**> to add a new variable by using the **Add Variable** dialog box.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)  
  
 **Direction**  
 Select the direction of the parameter. Map each variable to an input parameter, output parameter, or a return code.  
  
 **Data Type**  
 Select the data type of the parameter. The list of available data types is specific to the provider selected in the connection manager used by the task.  
  
 **Parameter Name**  
 Provide a parameter name.  
  
 Depending on the connection manager type that the task uses, you must use numbers or parameter names. Some connection manager types require that the first character of the parameter name is the \@ sign , specific names like \@Param1, or column names as parameter names.  
  
 **Related Topics:** [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md)  
  
 **Parameter Size**  
 Provide the size of parameters that have variable length, such as strings and binary fields.  
  
 This setting ensures that the provider allocates sufficient space for variable-length parameter values.  
  
 **Add**  
 Click to add a parameter mapping.  
  
 **Remove**  
 Select a parameter mapping in the list and then click **Remove**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Execute SQL Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Execute SQL Task Editor &#40;Result Set Page&#41;](../../2014/integration-services/execute-sql-task-editor-result-set-page.md)   
 [Transact-SQL Reference &#40;Database Engine&#41;](/sql/t-sql/language-reference)  
  
  
