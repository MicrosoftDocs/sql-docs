---
title: "Color Coding in Query Editors"
description: Learn how text categories are color coded to help you more easily find specific text, and how you can configure a custom color scheme.
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Editor [SQL Server Management Studio], color coding"
  - "color coding [Query Editor]"
ms.assetid: 802882dc-c997-4e3f-8a01-994bb43169ae
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Color Coding in Query Editors

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The text entered in the code editors is assigned a category; each category is identified by a color. The colors help you quickly find text in your code. For example, comments stand out in dark green. The following table lists the most common colors. You can view the whole list of colors and their categories, and configure a custom color scheme by using the **Tools**, **Options** menu. For more information about how to change the default colors, see [Change Font Color, Size, and Style](./change-font-color-size-and-style.md).  
  
## Default Code Colors  
  
|Color|Category|  
|-----------|--------------|  
|Red|SQL string|  
|Dark green|Comment|  
|Black on silver background|SQLCMD command|  
|Magenta|System function|  
|Green|System table, view, or table-valued function. Also, the system schemas sys and INFORMATION_SCHEMA.|  
|Blue|Keyword|  
|Teal|Line numbers or template parameter|  
|Maroon|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure|  
|Dark gray|Operators|  
  
## Status Bar  
 You can configure registered servers or [!INCLUDE[ssDE](../../includes/ssde-md.md)] servers in Object Explorer to have different colors in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor status bar. This helps you identify which server each editor window is connected to when you have many windows open at the same time. For more information about setting status bar colors, see [Status Bar &#40;Database Engine Query Editor&#41;](./status-bar-database-engine-query-editor.md).  
  
 Some types of editors do not display the status bar, or do not support multiple colors.  
  
