---
title: "Object Dependencies | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.common.objectdependencies.f1"
ms.assetid: c63d1160-3f3d-45df-99be-6fe081125fb5
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Object Dependencies
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Some database objects have dependencies upon other database objects. For example, views and stored procedures depend upon the existence of tables that contain the data returned by the view or procedure. The **Object Dependencies (General Page)** for the current object lists both the database objects that must be present for the object to function properly and the objects that depend upon the selected object. An object that references another object in its definition and that definition is stored in the system catalog is called a *referencing entity*. An object that is referred to by another object is called a *referenced entity*.  
  
The **Object Dependencies (Advanced Page)** for the current object lists the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects that depend on the object. The objects may be stored on different servers.  
  
Use this dialog box to understand the dependencies before changing or deleting the selected object.  
  
## UIElement List  
**Objects that depend on** _\<selected object>_  
Clicking this button displays a list of those objects that are dependency-tracked and that depend on the selected object.  
  
**Objects on which** _\<selected object>_ **depends**  
Clicking this button displays a list of those objects that are dependency-tracked, on which the selected object depends.  
  
**Dependencies**  
If **Objects that depend on** *<selected object>* is clicked, this displays an hierarchical view of objects that depend on the selected object. If **Objects on which** *<selected object>* **depends** is clicked, this displays an hierarchical view of objects on which the selected object depends.  
  
**Name**  
Displays the name of the object selected in the **Dependencies** tree view above.  
  
**Type**  
Displays the type of the object selected in the **Dependencies** tree view above.  
  
**Last Sync Time**  
> [!NOTE]  
> This option is available only on the **Advanced** page.  
  
Specifies the time and date when the dependency information was last updated.  
  
**Dependency type**  
> [!NOTE]  
> This option is available only on the **General** page.  
  
Displays the type of dependency between two objects. Can be one of the following:  
  
-   Schema-bound dependency  
  
    Is a relationship between two objects that prevents the referenced object from being dropped or modified as long as the referencing object exists. A schema-bound dependency is created when a view or user-defined function is created by using the WITH SCHEMABINDING clause, or when a table references another object through a CHECK or DEFAULT constraint or in the definition of a computed column.  
  
-   Non-schema-bound dependency  
  
    Is a relationship between two objects that does not prevent the referenced object from being dropped or modified.  
  
-   Not available or Unresolved Entity  
  
    Indicates the dependency type cannot be determined. This occurs only when the selected object is on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)].  
  
