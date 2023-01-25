---
title: "Configure IntelliSense (SQL Server Management Studio)"
description: Most IntelliSense options are on by default. Learn how you can turn off an IntelliSense option and invoke it instead through a menu command or keystroke combination.
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Options [SQL Server Management Studio], IntelliSense"
  - "modifying IntelliSense options"
  - "IntelliSense [SQL Server], modifying options"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: "01/25/2022"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Configure IntelliSense (SQL Server Management Studio)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Most [!INCLUDE[msCoName](../../includes/msconame-md.md)] IntelliSense options are on by default. You can turn off an IntelliSense option and instead invoke it through a menu command or keystroke combination.  
  
> [!IMPORTANT]  
>  Some changes will not take effect in your current editor session.  You must open a new Transact-SQL editor session to see the change.
  
### To turn statement completion options off by default  

> [!NOTE]
> Azure Synapse Analytics does not support IntelliSense.
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Expand **Text Editor**, expand either **All Languages**, **Transact-SQL**, or **XML**, and then click **General**.  
  
3.  Clear the check boxes for the Statement completion options that you do not want, and then click **OK**.  
  
### To modify Transact-SQL IntelliSense options  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Expand **Text Editor**, expand **Transact-SQL**, and then click **IntelliSense**.  
  
3.  Clear the check boxes for the IntelliSense options that you do not want.  
  
4.  To change the script size at which IntelliSense features are disabled, select a size from the **Maximum script size** list.  
  
5.  To change the casing applied to function names in completion lists, select a casing specification from the **Casing for built-in function names** list.  
  
6.  Select **OK**.
  
  
