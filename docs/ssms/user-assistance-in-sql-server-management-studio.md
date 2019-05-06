---
title: "User Assistance in SQL Server Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Help [SQL Server Management Studio]"
  - "SQL Server Management Studio [SQL Server], user assistance"
ms.assetid: 3c33a474-e507-4712-86fe-ae40e8370319
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# User Assistance in SQL Server Management Studio
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
User assistance is available in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] through the Help menu and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online. The Help menu in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] offers several different routes to information about [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It also provides access to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] community and MSDN Online resources not previously available from within the Help environment. In addition, the Help environment is now configurable to launch either within the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] environment or in an associated external window of its own.  
  
## The Help Interface  
The **Contents** and **Index** provide functionality and an interface already familiar to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] users. The other options are:  
  
-   **How Do I**  
  
    Provides a hierarchical set of linked pages containing useful topics related to common [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tasks. The content is organized by component and task, for example, Replication topics, and so on.  
  
-   **Search**  
  
    Searches for topics, with or without predefined filters. Search in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is a separate tabbed page. Users can refine their searches with one or more predefined topic type, language, or technology filters. By default, Search does not use any of the predefined filters, and only topics in the installed collections are searched.  
  
    Users can include online resources in their search by enabling online Help. For more information, see "MSDN Online and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Communities" later in this topic.  
  
-   **Dynamic Help**  
  
    Automatically displays links to relevant information while users work in the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment.  
  
-   **Help Favorites**  
  
    Stores user topic bookmarks for easy access later.  
  
Help on Help ( [!INCLUDE[msCoName](../includes/msconame_md.md)] Document Explorer Help) links users to the documentation about the Help Viewer, but the topics are in a collection separate from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online. For information about the Help Viewer, select **Help on Help** from the Help menu of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online.  
  
## MSDN Online and SQL Server Communities  
Help in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] also provides users ways to contact MSDN Online and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]-focused communities on the Web for information. You can:  
  
-   Access [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] communities from the How Do I page.  
  
-   Search MSDN Online and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] community sites.  
  
#### To access SQL Server-focused communities from the How Do I page  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], on the **Help** menu, click **How Do I**.  
  
2.  The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] **How Do I** page opens. In the Community Links sidebar, click the name of the community site you want to access.  
  
    > [!NOTE]  
    > The computer running [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] must have a direct connection to the Web.  
  
    Before you can search MSDN Online or the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] communities, you must enable online search.  
  
#### To enable online search  
  
1.  On the **Tools** menu, click **Options**. In the **Options** dialog box, expand the **Environment** and **Help** nodes if necessary, and then click **Online**.  
  
2.  In the **When loading Help content** area, select an online option.  
  
3.  In the **Search these providers** list, select the search providers you want to search, and clear those you don't.  
  
4.  If **Codezone Community** is one of your selected search providers, then in the **Codezone Community** list, select and clear items as appropriate.  
  
5.  Click **OK**.  
  
#### To search MSDN Online and SQL Server-focused communities from the Search page  
  
1.  On the **Help** menu, click **Search**.  
  
2.  Enter your search terms in the **Search for** text box, and then click **Search**.  
  
Whether or not you perform a search using the filters available (technology, language, and topic type), your search will now be run against all the search providers you selected.  
  
## Launching Help  
There are two ways to display Help from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. By default, when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online is opened from within [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], it opens in a document window external to the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment. This window is still associated with the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]; it can respond to some [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] events; and when you close [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], Books Online will close as well. Opening Books Online this way is particularly useful when you are using two monitors; you can drag the Books Online window to the second monitor, out of the way of work you are doing in the first one, but still easily referenced.  
  
You can also open Books Online as a document window inside [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]. This is preferable when you have limited screen space and want to take advantage of [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] and its ability to hide windows.  
  
> [!NOTE]  
> If you want Books Online to be completely independent of [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online from the **Start** menu, and it will not react to your actions in the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment, nor will it close if you exit [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
#### To configure Help and SQL Server Books Online to launch inside the Management Studio window  
  
1.  On the **Tools** menu, click **Options**, expand **Environment**, expand **Help**, and then click **General**.  
  
2.  In the **Show Help Using** box, click **Integrated Help Viewer**.  
  
