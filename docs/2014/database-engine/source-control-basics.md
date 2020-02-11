---
title: "Source Control Basics | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "source controls [SQL Server Management Studio], providers"
  - "source controls [SQL Server Management Studio]"
  - "source controls [SQL Server Management Studio], about source controls"
  - "source controls [SQL Server Management Studio], clients"
ms.assetid: ca35b67a-104a-41fb-ac58-a61be06fe114
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Source Control Basics
  Source control refers to a system in which a central piece of server software stores and tracks file versions, and also controls access to files. A typical source control system includes a source control provider and two or more source control clients.  
  
## Source Control Benefits  
 Placing your files under source control makes it possible to  
  
-   Manage the process by which the control of items passes from one person to another. Source control providers support both shared and exclusive file access. If access to project files is exclusive, the source control provider allows only one user at a time to check files out and modify them. If access is shared, more than one user can check out the script file, and the source control provider provides a mechanism for merging the versions as they are checked in.  
  
-   Archive successive versions of source-controlled items. A source control provider stores the data that distinguishes one version of a source-controlled item from another. The provider stores the differences between versions, as well as crucial information about the version: when it was created, when it was modified, and by whom. When several people are working on the same file, they must use the same code page, so that versions can be accurately compared. Consequently, you can retrieve any version of a source-controlled item. You can also designate any version to be the latest version of that item.  
  
-   Maintain detailed historical and version information on source-controlled items. Source control stores the date and time on which the item was created, when it was checked out or checked in, and the user who performed the action.  
  
-   Collaborate across projects. File sharing makes it possible for multiple projects to share source-controlled items. Changes to a shared item are reflected in all the projects that share the item.  
  
-   Automate frequently repeated source control operations. A source control provider may define an interface from the command prompt that supports the key features of source control. You can use this interface in batch files to automate the source control tasks that you perform regularly.  
  
-   Recover from accidental deletions. You can restore the latest file version checked into source control.  
  
-   Conserve disk space on both the source control client and server. Some source control providers, such as [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe, support disk space conservation on the server by storing the latest version of a file and the differences between each version and the version that precedes or follows it. On the client, Visual SourceSafe supports disk space conservation. You can cloak folders and files so that they are not downloaded to your local disk.  
  
 File check outs, check ins, and other source control operations are actually accomplished through a source control client, such as [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. The client is designed to interact with the provider to make the provider's capabilities available to a distributed group of users. Using a source control client, users can browse the files stored by the provider; add and delete files; check files in and out; and retrieve copies of local files.  
  
> [!NOTE]  
>  This documentation assumes that you are using [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe as your source control provider. If you are using a different source control provider, you might see differences between this documentation and the software you are running. If you see differences, consult the documentation for your source control provider.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Task**|**Topic**|  
|Set Source Control options|[Set Source Control Options](../../2014/database-engine/set-source-control-options.md)|  
|Change source control Connections|[Change Source Control Connections](../../2014/database-engine/change-source-control-connections.md)|  
|Exclude files from source control|[Exclude Files from Source Control](../../2014/database-engine/exclude-files-from-source-control.md)|  
  
  
