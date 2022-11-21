---
title: Edit a Breakpoint Location
description: Learn how to move a breakpoint in a Transact-SQL script file to another location in the script, or to a different script. 
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, breakpoint location"
ms.assetid: 5c28e411-0377-4210-a7ce-2a5c13dcdf74
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 12/04/2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Edit a Breakpoint Location

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The breakpoint location specifies the line and character where the breakpoint resides in a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file. You can edit the breakpoint location to move the breakpoint to another location in the script, or to a different script.

## Editing a Location

When you edit a breakpoint location, the breakpoint moves to the new location, carrying with it all of the existing properties, such as a hit count or condition.  

#### To Edit a Breakpoint Location

1. In the editor window, right-click the breakpoint glyph, and then click **Location** on the shortcut menu.  
  
     -or-  
  
     In the **Breakpoints** window, right-click the breakpoint glyph, and then click **Location** on the shortcut menu.  
  
2. In the **File Breakpoint** dialog box, edit **File** to specify a new file, **Line** to specify a new line, or **Character** to specify a new location within the line. If the new file you specify is already open in a query editor window, the breakpoint is moved to that editor window. If the file is not opened, a new editor window is opened, the file is loaded in, and the breakpoint moved to the new location.  
  
     The **Allow the source code to be different from the original version** option has no effect when debugging [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## See Also

- [Specify a Hit Count](./specify-a-hit-count.md)
- [Specify a Breakpoint Action](./specify-a-breakpoint-action.md)
- [Specify a Breakpoint Condition](./specify-a-breakpoint-condition.md)
- [Specify a Breakpoint Filter](./specify-a-breakpoint-filter.md)