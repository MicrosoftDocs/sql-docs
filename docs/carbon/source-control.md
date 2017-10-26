---
title: Source control in Carbon | Microsoft Docs
description: Learn how to configure source control in Carbon.
services: sql-database
author: stevestein
ms.author: sstein
manager: craigg
ms.reviewer: achatter, alayu, erickang, sanagama, sstein
ms.service: data-tools
ms.workload: data-tools
ms.prod: NEEDED
ms.topic: article
ms.date: 10/25/2017
---
#  Using source control in Carbon

The Carbon preview supports Git for version/source control.


## Git support in Carbon

Carbon ships with a Git source control manager (SCM) that leverages your computers Git installation, so you need to [install Git (version 2.0.0 or later)](https://git-scm.com/download) before these features are available. 

>**Note:** The Carbon documentation assumes you are already familiar with Git. If you are new to Git, the [git-scm](https://git-scm.com/documentation) website is a good place to start with a popular online [book](https://git-scm.com/book), Getting Started [videos](https://git-scm.com/video/what-is-git), and [cheat sheets](https://services.github.com/on-demand/downloads/github-git-cheat-sheet.pdf). 

![git overview](media/source-control/git-overview.png)

>**Tip:** Carbon works with any Git repository.  If you don't already have a private hosted Git provider, [Visual Studio Team Services](https://www.visualstudio.com/products/visual-studio-team-services-vs) is a great free option. [Sign up for VSTS](https://go.microsoft.com/fwlink/?LinkID=307137&campaign=o~msft~code~vc). IS THIS TRUE??


## Open an existing Git repository

1. Under the **File** menu, select **Open Folder...**
2. Browse to the folder that contains your git-tracked files (subfolders in your local repository are okay to select here), and click **Select Folder**.


## Initialize a new git repository

1. Select Source Control, then select the git icon that appears.

   ![Source control git icon](media/source-control/source-control.png)

1. Enter the path to the folder you want to initialize as a Git repository and press **Enter**.

   ![initialize Git repository](media/source-control/initialize-git-repository.png)

## Working with Git repositories

Carbon inherits its Git implementation from VS Code, but does not currently support additional SCM providers. For the details about working with Git after you open or initialize a repository, see [Git support in VS Code](https://code.visualstudio.com/docs/editor/versioncontrol#_git-support).

## Additional resources
- [Carbon Overview](overview.md)
- [Git documentation](https://git-scm.com/documentation)
