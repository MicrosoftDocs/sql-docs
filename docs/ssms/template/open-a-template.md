---
description: "Open a Template"
title: "Open a Template"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "templates [Transact-SQL], opening"
  - "opening templates"
ms.assetid: 605b0f4c-5ba1-4249-ad1c-6341df77cd7a
author: "markingmyname"
ms.author: "maghan"
---
# Open a Template
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
You can open a template from Template Explorer.  
  
## To open a template from Template Explorer  
You can open templates from the Template Explorer.  
  
1.  To open Template Explorer, on the **View** menu, click **Template Explorer**.  
  
2.  In the list of template categories, expand the category that contains the template you want to open.  
  
3.  There are three ways to open the template:  
  
    1.  Double-click the template to open it in a code editor window.  
  
    2.  Right-click the template and select **Open** to open it in a code editor window.  
  
    3.  Drag the template into a code editor window to add the template code to the contents of the editor window.  
  
After the template is open, use the **Specify Values for Template Parameters** dialog box from the **Query** menu to replace the template parameters with your values.  
  
If opening a template launches a new editor window, the window will open with the credentials of the current active connection. For example, if you are focused on an instance of the [!INCLUDE[ssDE](../../includes/ssde_md.md)] in Object Explorer when you open the CREATE DATABASE template, a new editor window will be opened using a connection to that instance. If there is no active connection, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] will present a login dialog.  
  
## See Also  
[Template Explorer](../../ssms/template/template-explorer.md)  
[Replace Template Parameters](../../ssms/template/replace-template-parameters.md)  
  
