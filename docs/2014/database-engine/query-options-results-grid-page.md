---
title: "Query Options Results (Grid Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.query.grid.f1"
ms.assetid: 764bf435-3aab-4c62-b4e0-64fe020a5a95
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Query Options Results (Grid Page)
  Use this page to specify the options for displaying a query result set in grid format.  
  
## Options  
 **Include the query in the result set**  
 Returns the text of the query as part of the result set.  
  
 **Include column headers when copying or saving the results**  
 Include column headers (titles) when results are copied to the clipboard, or saved in a file. Clear this check box if you do not want saved or copied result data to have only the data, and not the column headings.  
  
 **Discard results after execution**  
 Free memory by discarding the query results after the screen display has received them.  
  
 **Display results in a separate tab**  
 Display the result set in a new document window, instead of at the bottom of the query document window.  
  
 **Switch to results tab after the query executes**  
 Automatically set the screen focus to the result set.  
  
 **Maximum Characters Retrieved**  
 **Non XML data**:  
  
 Enter a number from 1 through 65535 to specify the maximum number of characters that will be displayed in each cell.  
  
> [!NOTE]  
>  Specifying a large number of characters may cause data in the result set to appear truncated. The maximum number of characters displayed in each cell is dependent on the font size. When large result sets are returned, a high value in this box can cause [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to run low on memory and hinder system performance.  
  
 **XML data**:  
  
 Select **1 MB**, **2 MB**, or **5 MB**. Select **Unlimited** to retrieve all characters.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
