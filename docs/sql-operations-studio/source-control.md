---
title: Source control in SQL Operations Studio (preview) | Microsoft Docs
description: Learn how to configure source control in SQL Operations Studio (preview).
ms.custom: "tools|sos"
ms.date: "11/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
#  Using source control in [!INCLUDE[name-sos](../includes/name-sos-short.md)]

[!INCLUDE[name-sos](../includes/name-sos-short.md)] supports Git for version/source control.


## Git support in [!INCLUDE[name-sos](../includes/name-sos-short.md)]

[!INCLUDE[name-sos](../includes/name-sos-short.md)] ships with a Git source control manager (SCM), but you still need to [install Git (version 2.0.0 or later)](https://git-scm.com/download) before these features are available. 



## Open an existing Git repository

1. Under the **File** menu, select **Open Folder...**
2. Browse to the folder that contains your files tracked by git, and click **Select Folder**. Subfolders in your local repository are okay to select here.


## Initialize a new git repository

1. Select **Source Control**, then select the git icon.

   ![Source control git icon](media/source-control/source-control.png)

1. Enter the path to the folder you want to initialize as a Git repository and press **Enter**.

   ![initialize Git repository](media/source-control/initialize-git-repository.png)

## Working with Git repositories

[!INCLUDE[name-sos](../includes/name-sos-short.md)] inherits its Git implementation from VS Code, but does not currently support additional SCM providers. For the details about working with Git after you open or initialize a repository, see [Git support in VS Code](https://code.visualstudio.com/docs/editor/versioncontrol#_git-support).


## Additional resources
- [Git documentation](https://git-scm.com/documentation)
