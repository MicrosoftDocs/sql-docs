---
title: "Query Options Results (Text Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.query.text.f1"
ms.assetid: fd2fb409-58f9-4ede-8349-ce007126b68d
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Query Options Results (Text Page)
  Use this page to specify the options for displaying a query result set in text format. The settings on this page also apply when **Results to File** is selected.  
  
 **Output format**  
 By default the output is displayed in columns created by padding the results with spaces. Other options are using commas, tabs, or spaces to separate columns. Select the **Custom delimiter** check box to specify a different delimiting character in the **Custom delimiter** box.  
  
 **Custom delimiter**  
 Specify the character of your choice to separate columns. This option is only available if the **Custom delimiter** check box is selected in the **Output format** box.  
  
 **Include column headers in the result set**  
 Clear this check box if you do not want each column labeled with a column title.  
  
 **Scroll as results are received**  
 Select this check box to keep the display focus on the most recently returned records at the bottom. Clear this check box to keep the display focus on the first rows received.  
  
 **Right align numeric values**  
 Select this check box to align numeric values to the right of the column. This can make it easier to review numbers with a fixed number of decimal places.  
  
 **Discard result after query executes**  
 Frees memory by discarding the query results after the screen display has received them.  
  
 **Display results in a separate tab**  
 Select this check box to display the result set in a new document window, instead of at the bottom of the query document window.  
  
 **Switch to results tab after the query executes**  
 Click to automatically set the screen focus to the result set.  
  
 **Maximum number of characters displayed in each column**  
 This value defaults to 256. Increase this value to display larger result sets without truncation.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
## Saving a text result set with headers  
 To save query results as a text file with headers, select the **Include column headers in the result set** check box and a delimited output format. Now the report file will contain headers when you click **Results to File** on the toolbar, or when you right-click any query results, click **Save Results As**, type a file name, and then click **Save**.  
  
  
