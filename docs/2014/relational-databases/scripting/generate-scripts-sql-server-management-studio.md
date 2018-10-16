---
title: "Generate Scripts (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 9711c617-3c68-4e5a-aea3-befc64d51524
author: MightyPen
ms.author: genemi
manager: craigg
---
# Generate Scripts (SQL Server Management Studio)
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides two mechanisms for generating [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. You can create scripts for multiple objects by using the **Generate and Publish Scripts Wizard.**. You can also generate a script for individual objects or multiple objects by using the **Script as** menu in **Object Explorer**.  
  
1.  **Choose a method:**  [Generate and Publish Scripts Wizard](#GenPubScriptWiz), [Object Explorer Script As Menu](#OEScriptAsMenu)  
  
2.  **To use the Script As menu:**  [Script a Single Object](#ScriptSingleObject), [Script Two Objects Using Object Explorer](#ScriptTwoObjectsOE), [Script Two Objects Using Object Explorer Details](#ScriptTwoObjectsOED)  
  
## Before You Begin  
 Choose the mechanism that best meets your requirements.  
  
###  <a name="GenPubScriptWiz"></a> Generate and Publish Scripts Wizard  
 Use the **Generate and Publish Scripts Wizard** to create a [!INCLUDE[tsql](../../includes/tsql-md.md)] script for many objects. The wizard generates a script of all the objects in a database, or a subset of the objects that you select. The wizard has many options for your scripts, such as whether to include permissions, collation, constraints, and so on. For instructions on using the wizard, see [Generate and Publish Scripts Wizard](generate-and-publish-scripts-wizard.md).  
  
###  <a name="OEScriptAsMenu"></a> Object Explorer Script As Menu  
 You can use the **Object Explorer Script as** menu to script a single object, script multiple objects, or script multible statements for a single objects. You can choose one of several types of scripts; for example to create, alter, or drop the object. You can save the script in a Query Editor window, to a file, or to the Clipboard. The script is created in Unicode format.  
  
##  <a name="ScriptSingleObject"></a> To generate a script of a single object  
 **To script a single object**  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, and then expand the database containing the object to be scripted.  
  
3.  Expand the category of the object. For example, expand the **Tables** or **Views** node.  
  
4.  Right-click the object, point to **Script \<object type> as**, For example, point to **Script Table as**.  
  
5.  Point to the script type, such as **Create to** or **Alter to**.  
  
6.  Select the location to save the script, such as **New Query Editor Window** or **Clipboard**.  
  
##  <a name="ScriptTwoObjectsOE"></a> To generate a script of two objects using Object Explorer  
 **To script two objects using Object Explorer**  
  
 Sometimes you may want a script with multiple options, such as drop a procedure and then create a procedure, or create a table and then alter a table. The processes below for generating scripts of multiple objects also work if you need to create a script that references different types of objects, such as tables, views, and stored procedures.  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, and then expand the database containing the objects to be scripted.  
  
3.  Right-click the first object to be scripted, point to **Script \<object type> as**, and in the **Save as** selections chooses **New Query Editor Window** as the output destination.  
  
4.  Navigate to the second object you want to script.  
  
5.  Right-click the object, point to **Script \<object type> as**, and in the **Save as** selections chooses **Clipboard** as the output destination.  
  
6.  In the Query Editor window opened for the first object, paste the script for the second object from the clipboard.  
  
##  <a name="ScriptTwoObjectsOED"></a> To generate a script of two objects using Object Explorer Details  
 **To script two objects using Object Explorer Details**  
  
 You can use the **Object Explorer Details** pane to generate a script for mutliple objects of the same category.  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, and then expand the database containing the objects to be scripted.  
  
3.  Expand the category node of the types of object you want to script, such as the **Tables** node.  
  
4.  Open the **Object Explorer Details** pane by either selecting **F7**, or opening the **View** menu and selecting **Object Explorer Details**.  
  
5.  Left-click one of the objects you want to script.  
  
6.  Crtl + left-click the second object you want to script.  
  
7.  Right-click one of the selected objects, and select **Script \<object type> as**.  
  
  
