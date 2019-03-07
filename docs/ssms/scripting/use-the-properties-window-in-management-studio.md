---
title: "Use the Properties Window in Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing properties"
  - "Properties window [SQL Server Management Studio]"
  - "complex properties [SQL Server Management Studio]"
ms.assetid: 903d4aca-f57c-43d9-a893-702eceaa7004
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use the Properties Window in Management Studio
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The Properties window describes the state of an item in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], such as a connection or a Showplan operator, and information about database objects such as tables, views, and designers.  
  
 You can use the Properties window to view the properties of the current connection. Many properties are read-only in the Properties window but can be changed elsewhere in the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For example, the Database property of a query is read-only in the Properties window, but can be changed on the tool bar.  
  
### To view properties using the Properties window  
  
1.  If the Properties window is not visible, click **Properties Window** on the **View** menu, or press F4.  
  
2.  Set the focus on the object that you want to view.  
  
3.  Look for a specific property in the Properties window.  
  
### To view connection properties of a query window  
  
1.  If the Properties window is not visible, click **Properties Window** on the **View** menu, or press F4.  
  
2.  In the Properties window, you can see all the connection properties.  
  
### To view the properties of a Showplan operator  
  
1.  On the **Query** menu, click **Include Actual Execution Plan**.  
  
2.  In the SQL Query Editor, type and execute a query.  
  
3.  If the Properties window is not visible, click **Properties Window** on the **View** menu, or press F4.  
  
4.  On the **Execution plan** tab of the SQL Query Editor click the icons of the operators to view information about the operators in the Properties window.  
  
## See Also  
 [Properties Window &#40;Management Studio&#41;](https://msdn.microsoft.com/library/6a9a1389-df8d-4cfc-928b-eccbf884a22d)  
  
  
