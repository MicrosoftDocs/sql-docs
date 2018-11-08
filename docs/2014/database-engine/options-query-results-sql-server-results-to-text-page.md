---
title: "Options (Query Results-SQL Server-Results to Text Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.QueryResults.SqlServer.SQLResultsToText"
ms.assetid: 2ccbdf17-e14f-42f1-a836-ca999a3432c9
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Query Results-SQL Server-Results to Text Page)
  Use this page to specify the options for displaying a query result set in text format. Changes to these options are applied only to new [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] queries. To change the options for the current queries, click **Query Options** on the **Query** menu, or right-click in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Query window, and select **Query Options**. In the **Query Options** dialog box, under **Results**, click **Text**.  
  
## UIElement List  
 **Output format**  
 By default the output is displayed in columns created by padding the results with spaces. Other options are to use commas, tabs, or spaces to separate columns. Select **Custom delimiter** from this drop-down list to specify a different delimiting character in the **Custom delimiter** text box.  
  
 **Custom delimiter**  
 Specify the character of your choice to separate columns. This text box is available only if you clicked Custom delimiter in the **Output format** drop-down list box.  
  
 **Include column headers in the result set**  
 Clear this check box if you do not want each column labeled with a column title.  
  
 **Include the query in the result set**  
 Select this check box to include the text of the query that is running in the results pane before the results of the query.  
  
 **Scroll as results are received**  
 Select this check box to keep the display focus on the most recently returned records at the end of the results set. Clear this check box to keep the display focus on the first rows received.  
  
 **Right align numeric values**  
 Select this check box to align numeric values to the right of the column. This can make it easier to review numbers with a fixed number of decimal places.  
  
 **Discard result after query executes**  
 Select this check box to discard the query results after they are displayed in the results pane of the query window.  
  
 **Display results in a separate tab**  
 Select this check box to display the result set in a new document window, instead of at the bottom of the query document window.  
  
 **Switch to results tab after the query executes**  
 Select this check box to automatically set the screen focus to the result set.  
  
 **Maximum number of characters displayed in each column**  
 This value defaults to 256. Increase this value to display larger result sets without truncation. The maximum value is 8,192.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
