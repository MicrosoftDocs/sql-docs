---
title: Set Trace Definition Defaults
titleSuffix: SQL Server Profiler
description: Discover how to use SQL Server Profiler to set up the templates that SQL Server and Analysis Services use by default for each provider or server.
author: markingmyname
ms.author: maghan
ms.date: 03/14/2017
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
---

# Set Trace Definition Defaults (SQL Server Profiler)

 [!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

The trace definition default is the default trace template that is used for each provider or server. You can set default trace templates for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
### To set trace definition defaults  
  
1.  On the **File** menu, select **Templates**, and then click **Edit Template.**  
  
2.  In the **Trace Template Properties** dialog box, on the **General**tab, select a server type from the **Select server type**list.  
  
3.  In the **Select template name**list, select the name of the template that you want to use as the trace definition default.  
  
4.  Select **Use as a default template for selected server type**.  
  
5.  If necessary, click the **Events Selection**tab to modify the template.  
  
6.  Click **Save**.  
  
## See Also  
 [SQL Server Profiler Templates](../../tools/sql-server-profiler/sql-server-profiler-templates.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
