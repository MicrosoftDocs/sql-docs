---
title: "Start SQL Server Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: 25ffaea6-0eee-4169-8dd0-1da417c28fc6
author: stevestein
ms.author: sstein
manager: craigg
---
# Start SQL Server Management Studio
  To begin this tutorial, let's take a look at [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## Opening SQL Server Management Studio  
  
#### To open SQL Server Management Studio  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], and then click **SQL Server Management Studio**.  
  
    > [!NOTE]  
    >  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is not installed by default. If [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] is unavailable, install it by running Setup. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] is not available with [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] Express is available as a free download from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=37075&clcid=0x409), but has a different user interface than is described in this tutorial.  
  
2.  In the **Connect to Server** dialog box, verify the default settings, and then click **Connect**. To connect, the **Server name** box must contain the name of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed. If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is a named instance, the **Server name** box should also contain the instance name in the format \<*computer_name*>\\<*instance_name*>.  
  
## Management Studio Components  
 [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] presents information in windows dedicated to specific types of information. Database information is shown in Object Explorer and document windows.  
  
-   Object Explorer is a tree view of all the database objects in a server. This can include the databases of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Object Explorer includes information for all servers to which it is connected. When you open [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you are prompted to connect Object Explorer to the settings that were last used. You can double-click any server in the Registered Servers component to connect to it, but you do not have to register a server to connect.  
  
-   The document window is the largest portion of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. The document windows can contain query editors and browser windows. By default, the Summary page is displayed, connected to the instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] on the current computer.  
  
## Showing Additional Windows  
  
#### To show the Registered Servers window  
  
1.  On the **View** menu, click **Registered Servers**.  
  
     The Registered Servers window appears above Object Explorer. Registered Servers lists servers which you manage frequently. You can add and remove servers from this list. The only servers listed are the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances on the computer where you are running [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  If your server does not appear, in Registered Servers, right-click **Database Engine**, and then click **Update Local Server Registration**.  
  
## Next Task in Lesson  
 [Connect with Registered Servers and Object Explorer](../object/object-explorer.md)  
  
  
