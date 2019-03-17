---
title: "Create a Project | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], creating"
ms.assetid: 7897be19-365b-4b06-bcf0-8a669f67a673
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Create a Project
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can create one or more projects within an existing solution.  
  
## Create a new project and add it to a solution  
  
1.  In Solution Explorer, select the solution.  
  
2.  On the **File** menu, point to **Add**, and click **New Project**.  
  
3.  In the  **New Project** dialog box, click a type of project.  
  
    **Templates**  
    In the **Templates** box, select a template. A brief description of the selected project template appears beneath the **Templates** box.  
  
    **Name**  
    Enter the name of the script project you want to create. A folder with the same name as the project is also created in the location displayed in the **Location** field. For some projects, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] creates source and other supporting files and adds them to the new project folder.  
  
    > [!NOTE]  
    > For some project types, the **Name** text box is unavailable because specifying the location sets the name. For example, Web applications and Web services are located on a Web server and derive their name from the virtual directory specified on that server.  
  
    Names cannot contain the following characters:  
  
    -   Pound (#)  
  
    -   Percent (%)  
  
    -   Ampersand (&)  
  
    -   Asterisk (*)  
  
    -   Vertical bar (|)  
  
    -   Backslash (\\)  
  
    -   Colon (:)  
  
    -   Quotation mark (")  
  
    -   Less than (\<)  
  
    -   Greater than (>)  
  
    -   Question mark (?)  
  
    -   Forward slash (/)  
  
    -   Leading or trailing spaces (' ')  
  
    -   Names reserved for Microsoft Windows or MS-DOS, such as ("nul", "aux", "con", "com1", "lpt1", and so on)  
  
    **Location**  
    Enter the location where you want to create your project, or choose one from the list.  
  
    **Browse**  
    Displays the **Project Location** dialog box, allowing you to navigate to a new directory in which to save the project.  
  
    **Solution**  
    Select **Create new Solution** to create a solution in Solution Explorer. Select **Add to Solution** to add the new project to the solution currently open in Solution Explorer.  
  
    **Create directory for Solution**  
    Select to enable the **(Solution) Name** text box. This option creates a new directory of the name you choose for your project and solution.  
  
    **Solution Name**  
    Enter the name of the new solution in which you want your project to be created. By default, this field uses the same name as entered in the **Name** field.  
  
    **Note** To access this option, you must select both the **Create New Solution** in the **Solution** and the **Create directory for Solution** check boxes. Some project templates do not support this option, such as Web applications.  
  
    **Add to Source Control**  
    When this check box is selected, your source control application opens when you click **OK**. Complete any information required by your source control application to continue. You must have a source control client application installed to use this option.  
  
4.  Click **OK**.  
  
You can set a name for the script project, but the folder names are established by [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and cannot be changed. You can configure the drive and path specification for the common set of folders by using the **Add New Project** dialog box. Right-click the solution icon in **Solution Explorer**, and then click **Add**. The default location for script project folders is: C:\Documents and Settings\\*username*\My Documents\SQL Server Management Studio\Projects\\.  
  
## See Also

[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Add an Existing Project to a Solution](../../ssms/solution/add-an-existing-project-to-a-solution.md)  
[Add New Items to a Project](../../ssms/solution/add-new-items-to-a-project.md)  
[Add Existing Items to a Project](../../ssms/solution/add-existing-items-to-a-project.md)  
[Change the Default Location for Projects](../../ssms/solution/change-the-default-location-for-projects.md)  
[Remove or Delete an Item or Project](../../ssms/solution/remove-or-delete-an-item-or-project.md)  
[Delete a Solution](../../ssms/solution/delete-a-solution.md)  