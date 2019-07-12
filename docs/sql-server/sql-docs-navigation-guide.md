---
title: SQL Server docs navigation guide 
description: A guide for navigating the SQL Server technical documentation - explains such things as the hub page, the table of contents, the header, as well as how to use the breadcrumbs and the  how to use the version filter. 
ms.date: 07/11/2019
ms.prod: sql
ms.reviewer: ""
ms.custom: ""
ms.topic: conceptual
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = sqlallproducts-allversions"
---

# SQL Server docs navigation guide 

This topic provides some tips and tricks for navigating the SQL Server technical documentation space.  

## Hub page

The SQL Server hub page can be found at [https://aka.ms/sqldocs](https://aka.ms/sqldocs) and is the entry point for finding relevant SQL Server content.

The first section provides quick links to download the product, and tools, as well as deploy a SQL Server VM to Azure, and access the education center. The education center is a collection of SQL Server quickstarts, tutorials, and guides about the internals & architecture of SQL Server. 

![First section of the hub page](media/sql-server-docs-navigation-guide/sql-docs-hub-page-first-section.png)

The second section takes you to general overviews of technical documentation separated by platform. 

![Second section of the hub page](media/sql-server-docs-navigation-guide/sql-docs-hub-page-second-section.png)

The third section takes you directly to content you may find relevant depending on what you're trying to do with the SQL Server database engine. 

![Third section of the hub page](media/sql-server-docs-navigation-guide/sql-docs-hub-page-third-section.png)

The fourth and final sections provide additional content, such as Analysis Services, Reporting Services, and Machine Learning Services. This is also where you will find reference content such as Transact-SQL commands, and DMVs. 

![Fourth section of sql docs hubpage](media/sql-server-docs-navigation-guide/sql-docs-hub-page-fourth-section.png)

You can always navigate back to this page by going to [https://aka.ms/sqldocs](https://aka.ms/sqldocs), or you can select **SQL Docs** from the header at the top of every page within the SQL Server technical documentation set: 

![SQL docs in header](media/sql-server-docs-navigation-guide/sql-docs-in-header.png)


## Table of contents

The table of contents is the navigation pane found on the left side of each article:

![Table of contents](media/sql-server-docs-navigation-guide/sql-docs-table-of-contents.png)

Think of the table of contents (TOC) as being the index in a technical documentation book, where content is categorized into relevant sections. The categories in the table of contents match those on the hub page. The entire SQL Server documentation set can be located from within this table of contents. 

### Navigation hints
Entires in the TOC that have a `>` at the end of the entry indicate that you will be taken away from docs.microsoft.com. 

![Single carrots in toc](media/sql-server-docs-navigation-guide/single-carrots-in-sql-docs-toc.png)


Entries in the TOC that have a `>>` indicate that you will be taken to technical documentation with a different table of contents. 

![TOC navigation markers](media/sql-server-docs-navigation-guide/double-carrots-in-sql-docs-toc.png)

If you navigate to one of these pages, you can come back to the main SQL Server technical page, and table of contents, by selecting the "Welcome to SQL Server >>" entry found at the top of each of these table of contents. 

![Navigate back to SQL toc](media/sql-server-docs-navigation-guide/navigate-back-to-sql-toc.png)

### Filter box
You can search the content in the table of contents by using the filter search box at the top: 

![Use filter box](media/sql-server-docs-navigation-guide/sql-docs-toc-filter.gif)

## Header
The header is visible from everywhere within the SQL Server technical documentation space (which is all content found within https://docs.microsoft.com/sql). You can use this to quickly navigate to relevant content within the content set. Most of the links here will keep you within the SQL Server content set, but some (such as content for Azure SQL Database or Azure SQL Server VMs) may take you to the Azure content set, which will have a different header and table of contents. 

The categories found in the header match those on the hub page, and the table of contents:

![SQL Docs header](media/sql-server-docs-navigation-guide/sql-docs-header.gif)

## Version filter
The SQL Server technical documentation provides content for several supported versions of SQL Server. Features can vary between versions and flavors of SQL Server, and as such, sometimes the content itself can vary. 

You can use the version filter to ensure that you are seeing content for the appropriate version and flavor of SQL Server: 

![SQL Docs version filter](media/sql-server-docs-navigation-guide/sql-docs-version-filter.gif)

Selecting **SQL Server** > **Hide nothing** ensures that all content is visible, and that nothing is hidden behind the version filter. 

## Breadcrumbs

Breadcrumbs can be found below the header and above the table of contents, and indicate where the current article is located in the table of contents.  Not only does this help set the context to what type of content you're reading, but it also allows you to navigate back up the table of contents tree. 

![SQL Docs breadcrumbs](media/sql-server-docs-navigation-guide/sql-docs-bread-crumbs.gif)


## Article section navigation

The right-hand navigation pane allows you to quickly navigate to sections  within an article, as well as identify your location within the article.  

![Right-hand navigation](media/sql-server-docs-navigation-guide/sql-docs-right-hand-navigation.gif)

## Offline documentation

If you would like to view the SQL Server documentation on an offline system, you have two options to do so. You can either create a PDF wherever you are in the SQL Server technical documentation, or you can download the offline content using [SQL Server offline Help Viewer](sql-server-help-installation.md). 

If you'd like to create a PDF, select the **Download PDF** link found at the bottom of every table of contents.


![Download PDF](media/sql-server-get-help/download-pdf.png)


## Submit docs feedback

If you find something wrong within an article, you can submit feedback to the SQL Content team for that article by scrolling down to the bottom of the page and selecting **Content feedback**. 

![Git Issue content feedback](media/sql-server-get-help/git-issues.png)

You can also submit general documentation feedback and suggestions at [https://aka.ms/sqldocsfeedback](https://aka.ms/sqldocsfeedback). 

## Edit content

As long you have a GitHub account, you can edit the content yourself on docs.microsoft.com by following these steps:

1. Navigate to the page of interest.
1. Select the "Edit" button at the top right of the page.
1. Select the "Pencil" icon on the right.
1. Modify the text within the text box in [markdown](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) format.
1. Name your file change and provide a description.
1. Select "Propose file change".
1. Select "Create pull request" on the 'Comparing changes' page.
1. Select "Create pull request" on the 'Open a pull request' page.

![Edit SQL Docs](media/sql-server-docs-navigation-guide/edit-sql-docs.gif)

You can find detailed information about this at [https://aka.ms/editsqldocs](https://aka.ms/editsqldocs). 


## Next steps

- Get started with the [SQL Server technical documentation](sql-server-technical-documentation.md). 
- For more information about submitting feedback for or getting help with SQL Server, see the [Get help](sql-server-get-help.md) page. 
- To quickly access all the quickstarts and tutorials, go to the [SQL Server Education Center](../lp/sql-server/sql-education-center.md).
 
 
<!--
ffmpeg commands to convert mp4 to gif

ffmpeg -i breadcrumbs.mp4 -b:v 1024m -bufsize 1024m breadcrumbs.gif
ffmpeg -i edit-sql-docs-content.mp4 -b:v 1024m -bufsize 1024m edit-sql-docs-content.gif
ffmpeg -i header.mp4 -b:v 1024m -bufsize 1024m header.gif
ffmpeg -i right-hand-nav.mp4 -b:v 1024m -bufsize 1024m right-hand-nav.gif
ffmpeg -i toc-filter.mp4 -b:v 1024m -bufsize 1024m toc-filter.gif
ffmpeg -i version-filter.mp4 -b:v 1024m -bufsize 1024m version-filter.gif

>