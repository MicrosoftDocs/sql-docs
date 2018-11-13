---
title: "Configure IntelliSense (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/02/2016"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Options [SQL Server Management Studio], IntelliSense"
  - "modifying IntelliSense options"
  - "IntelliSense [SQL Server], modifying options"
ms.assetid: 3ffc9f31-4efa-4c1a-a033-ed1dc48b065f
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure IntelliSense (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  Most [!INCLUDE[msCoName](../../includes/msconame-md.md)] IntelliSense options are on by default. You can turn off an IntelliSense option and instead invoke it through a menu command or keystroke combination.  
  
> [!IMPORTANT]  
>  Some changes will not take effect in your current editor session.  You must open a new Transact-SQL editor session to see the change.
  
### To turn statement completion options off by default  

> [!NOTE]
> SQL Data Warehouse does not support IntelliSense.
>
>
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Expand **Text Editor**, expand either **All Languages**, **Transact-SQL**, or **XML**, and then click **General**.  
  
3.  Clear the check boxes for the Statement completion options that you do not want, and then click **OK**.  
  
### To modify Transact-SQL IntelliSense options  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Expand **Text Editor**, expand **Transact-SQL**, and then click **IntelliSense**.  
  
3.  Clear the check boxes for the IntelliSense options that you do not want.  
  
4.  To change the script size at which IntelliSense features are disabled, select a size from the **Maximum script size** list.  
  
5.  To change the casing applied to function names in completion lists, select a casing specification from the **Casing for built-in function names** list.  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
  
