---
title: "Generate Scripts (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 9711c617-3c68-4e5a-aea3-befc64d51524
author: stevestein
ms.author: sstein
ms.reviewer: "mathoma" 
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Generate Scripts (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides two mechanisms for generating [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. You can create scripts for multiple objects by using the **Generate and Publish Scripts Wizard**. You can also generate a script for individual objects or multiple objects by using the **Script as** menu in **Object Explorer**.  

For a detailed Tutorial on scripting various objects using SQL Server Management Studio (SSMS), please see [Tutorial: Scripting in SSMS](https://docs.microsoft.com/sql/ssms/tutorials/scripting-ssms).

  
## Before You Begin  
 Choose the mechanism that best meets your requirements.  
  
###  <a name="GenPubScriptWiz"></a> Generate and Publish Scripts Wizard  
 Use the **Generate and Publish Scripts Wizard** to create a [!INCLUDE[tsql](../../includes/tsql-md.md)] script for many objects. The wizard generates a script of all the objects in a database, or a subset of the objects that you select. The wizard has many options for your scripts, such as whether to include permissions, collation, constraints, and so on. For instructions on using the wizard, see [Generate and Publish Scripts Wizard](../../relational-databases/scripting/generate-and-publish-scripts-wizard.md).  
  
###  <a name="OEScriptAsMenu"></a> Object Explorer Script As Menu  
 You can use the **Object Explorer Script as** menu to script a single object, script multiple objects, or script multiple statements for a single object. You can choose one of several types of scripts; for example to create, alter, or drop the object. You can save the script in a Query Editor window, to a file, or to the Clipboard. The script is created in Unicode format.  
  
##  <a name="ScriptSingleObject"></a> To generate a script of a single object  
 **To script a single object**  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, and then expand the database containing the object to be scripted.  
  
3.  Expand the category of the object. For example, expand the **Tables** or **Views** node.  
  
4.  Right-click the object, point to **Script \<object type> as**, For example, point to **Script Table as**.  
  
5.  Point to the script type, such as **Create to** or **Alter to**.  
  
6.  Select the location to save the script, such as **New Query Editor Window** or **Clipboard**.  

    ![Scripting table](media/generate-scripts-sql-server-management-studio/scripttable.png)
  
  
 You can use the **Object Explorer Details** pane to generate a script for multiple objects of the same category.  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, and then expand the database containing the objects to be scripted.  
  
3.  Expand the category node of the types of object you want to script, such as the **Tables** node.  
  
4.  Open the **Object Explorer Details** pane by either selecting **F7**, or opening the **View** menu and selecting **Object Explorer Details**.  
  
5.  Left-click one of the objects you want to script.  
  
6.  Ctrl + left-click the second object you want to script.  
  
7.  Right-click one of the selected objects, and select **Script \<object type> as**.  

    ![Object Explorer](media/generate-scripts-sql-server-management-studio/objectexplorerdetails.png)
  
  
