---
title: "Quick Info (IntelliSense)"
description: Learn how to use the IntelliSense Quick Info option to display the complete declaration for any identifier in your code. In SQL Server Management Studio, the option is available in the Database Engine Editor and XML Query Editor.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Quick Info option [IntelliSense]"
  - "declarations [IntelliSense]"
  - "IntelliSense [SQL Server], Quick Info"
  - "identifier declarations [IntelliSense]"
ms.assetid: 3c8b59f4-1922-4bde-844f-5f2306514d96
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Quick Info (IntelliSense)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] IntelliSense **Quick Info** option displays the complete declaration for any identifier in your code. When you move the mouse pointer over an identifier, its declaration is displayed in a yellow pop-up window. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], **Quick Info** is available in the Database Engine and XML Query Editors.  
  
## Transact-SQL Quick Info  
 **Quick Info** displays two types of information in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor. When not in debug mode, **Quick Info** displays the expression declaration. When in debug mode, **Quick Info** instead displays the name of the expression and its current value.  
  
 In the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor, **Quick Info** is available only for those parts of the [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that IntelliSense supports. For example, if you move the mouse pointer over the identifier for an object that has a data type that IntelliSense does not support, the **Quick Info** pop-up window contains a message that states that the data type is not supported.  
  
## See Also  
 [Transact-SQL Syntax Supported by IntelliSense](./transact-sql-syntax-supported-by-intellisense.md)  
  
