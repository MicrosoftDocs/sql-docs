---
title: "Replace Template Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.templates.replaceparameters.f1"
helpviewer_keywords: 
  - "template parameters [Template Explorer]"
  - "templates [Transact-SQL], replacing parameters"
  - "Replace (Query) Template Parameters dialog box"
  - "replacing template parameters"
ms.assetid: 1234aa14-3464-4a3e-922a-5cfb8fb23627
author: stevestein
ms.author: sstein
manager: craigg
---
# Replace Template Parameters
  Templates contain parameters that can be replaced by implementation-specific values each time the template is used. After opening a template in a code editor, you can replace the parameters with values relevant to your implementation.  
  
## Before You Begin  
 The **Specify Values for Template Parameters** dialog is a grid with three columns. The **Parameter** and **Type** columns are read-only and cannot be changed. Review the contents of the **Value** column, and change any of the defaults to values appropriate for your implementation.  
  
 To use this dialog box, you must have parameters in your script enclosed in angle brackets (`< >`) in the format `<`*parameter_name*`,` *data_type*`,` *default_value*`>`.  
  
## Replace Template Parameters  
 After opening the template in a code editor window:  
  
1.  On the **Query** menu, click **Specify Values for Template Parameters**.  
  
2.  In the **Specify Values for Template Parameters** dialog box, the **Values** column contains a suggested value for each parameter. Accept the value or replace it with a new value.  
  
3.  Click **OK** to close the **Replace Template Parameters** dialog box and modify the script in the query editor.  
  
## See Also  
 [Template Explorer](template-explorer.md)   
 [Open a Template](open-a-template.md)  
  
  
